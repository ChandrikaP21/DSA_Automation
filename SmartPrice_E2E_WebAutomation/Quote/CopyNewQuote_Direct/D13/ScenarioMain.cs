using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.Quote;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.Globalization;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13
{
    public class ScenarioMain :D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;
           

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        {
           

        }

       

        public void ExecuteTestcases(TestCase test,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
          Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceDataList = new List<DSAPageObject>();


            try
            {

                
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                ds.QuoteNumber= CopyAndCreateNewQuote();
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];

                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    RetriveDataFromDSAScreen(retriveSmartPriceDataList,totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, "D13", null, null, null);
                    
                }


                //if (test.PriceChangeType == "AllProducts")
                //{
                //    if(cs.NoOfProducts.Count>5)
                //    {
                //        RemoveAllProductsExceptOneWithBaseService(cs, null, cs.NoOfProducts.Count);
                //        DateTime currentSystemTime = DateTime.Now;
                //        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                //        ds.cstTime = TimeZoneInfo.ConvertTime(currentSystemTime, cstZone).ToString();
                //        Thread.Sleep(5000);
                //        customerPageObj.RefreshSmartPriceButton.Click();
                //        Thread.Sleep(5000);
                //    }
                    
                //    CompareSmartPrice(totalFinancialCalculation); 
                    
                //}


                //if (test.PriceChangeType == "ForSpecificProduct")
                //{
                    
                //    RetriveDataFromDSAScreen(totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, scenarioId, null, null, null);

                //}






               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs,resultLog, test);
            }
        }
        /// <summary>
        /// Calculate all the 5 pricing point for D13-PriceChange Scenario and store it in a list.
        /// </summary>
        /// <param name="totalFinancialCalculation"></param>
        /// <param name="ds"></param>
        private void SmartPriceCalculationForPricePointChange(TotalFinancialCalculation financialCalculation, DSAPageObject ds,Constant constant,List<DSAPageObject> retriveSmartPriceData,string scenarioId)
        {

            try
            {
                OracleDBConnection obj = new OracleDBConnection();


                foreach (DSAPageObject item in retriveSmartPriceData)
                {
                    int i = 0;
                    item.TiedId = constant.RandomNumber(100, 1000).ToString();
                    CalculateGuidanceandFinanacials(item, obj, financialCalculation, null,scenarioId);
                    //calculate unit selling price
                    if (ds.IsdiscountModified)
                    {

                        item.comparisionObjects.CalUnitSellingPrice = Math.Round((100 - Convert.ToDecimal(ds.comparisionObjects.CalDiscountedPricePerUnit)) * Convert.ToDecimal(item.UnitListPrice.Replace("$","").Replace(",","")) / 100,2).ToString();
                    }
                     else
                    {
                        item.comparisionObjects.CalUnitSellingPrice = Math.Round((100 - Convert.ToDecimal(item.DiscountInputField)) * Convert.ToDecimal(item.UnitListPrice.Replace("$", "").Replace(",", "")) / 100, 2).ToString();
                    }
                    item.comparisionObjects.CalTotalSellingPrice = (Convert.ToDecimal(item.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));

                    item.comparisionObjects.TotalActualSmartPriceRevenue = item.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                    item.comparisionObjects.CalListPrice = Convert.ToDecimal(item.TotalListPrice.Replace("$", "").Replace(",", ""));

                    item.comparisionObjects.CalculatedTotalPricingModifier = item.CalculatedActualSPCompFinancials.PricingModifier;
             
                    foreach (DSAPageObject accessory in item.Accessories)
                    {
                        accessory.TiedId = item.TiedId;
                        CalculateGuidanceandFinanacials(accessory, obj, financialCalculation);
                        accessory.comparisionObjects.CalTotalSellingPrice = (Convert.ToDecimal(accessory.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
                        accessory.comparisionObjects.CalListPrice = Convert.ToDecimal(accessory.TotalListPrice.Replace("$", "").Replace(",", ""));
                        accessory.comparisionObjects.TotalActualSmartPriceRevenue = accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        accessory.comparisionObjects.CalculatedTotalPricingModifier = accessory.comparisionObjects.TotalActualSmartPriceRevenue / accessory.comparisionObjects.CalTotalSellingPrice;


                        item.comparisionObjects.CalTotalSellingPrice += accessory.comparisionObjects.CalTotalSellingPrice;
                        item.comparisionObjects.CalListPrice += accessory.comparisionObjects.CalListPrice;
                        item.comparisionObjects.TotalActualSmartPriceRevenue += accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        item.comparisionObjects.CalculatedTotalPricingModifier = item.comparisionObjects.TotalActualSmartPriceRevenue / item.comparisionObjects.CalTotalSellingPrice;
                        //  item.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;;

                    }
                   

                    financialCalculation.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;
                    financialCalculation.CalculatedTotalListpriceSummaryLevel += item.comparisionObjects.CalListPrice;
                    financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel += item.comparisionObjects.TotalActualSmartPriceRevenue;
                    financialCalculation.CalculatedTotalPricingModifierSummaryLevel = financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel / Convert.ToDecimal(financialCalculation.CalculatedTotalSellingpriceSummaryLevel);

                    i++;


                }
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                //Console.WriteLine(ex.Message);
            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);

        }

        private void PublishReport( DSAPageObject ds,ResultLogging resultLog,TotalFinancialCalculation totalFinancialCalculation,string scenarioId,int testCaseNumber,TestCase test, List<DSAPageObject> retriveSmartPriceDataList,string pricePoint,string runId,bool isIndirect=false,CustomerClass customerClass=null,bool IsStandardPartnerDiscount=false)
        {
             
            resultLog.GenerateReportInCSV(ds, retriveSmartPriceDataList, totalFinancialCalculation, scenarioId, testCaseNumber, runId, null,customerClass,true, pricePoint, isIndirect, IsStandardPartnerDiscount);
         //   resultLog.ExportToSqlServerDB(ds, test, scenarioId);


            Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
        }

       

        public void BetweenCompAnchorAndFloor(DSAPageObject ds,int j)
        {
            Scroll(ds.ByDiscountField(j), ds, j);
            ds.DiscountField(j).Click();
            ds.FloorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]")).Text.Replace("%","");
            ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%","");
            decimal cfPriceChange = (Convert.ToDecimal(ds.CompAnchorDiscount) + Convert.ToDecimal(ds.FloorDiscount)) / 2;
            
            ClearAndUpdateField(ds, cfPriceChange.ToString(),j);
            Thread.Sleep(2000);

        }

        public void BelowFloor(DSAPageObject ds,int j)
        {
            Scroll(ds.ByDiscountField(j), ds, j);
            ds.DiscountField(j).Click();
            ds.FloorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]")).Text.Replace("%", "");
            decimal belowFloorPriceChange = (Convert.ToDecimal(ds.RecommenededDiscount)+2);
            
            ClearAndUpdateField(ds, belowFloorPriceChange.ToString(),j);
            Thread.Sleep(6000);
        }

        public void AtCompAnchor(DSAPageObject ds,int j)
        {
            Scroll(ds.ByDiscountField(j), ds, j);
            ds.DiscountField(j).Click();
            ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
          
            ClearAndUpdateField(ds, ds.CompAnchorDiscount.ToString(),j);
            Thread.Sleep(2000);
        }

        public string BetweenRecommendedAndCompAnchor(DSAPageObject ds,int j)
        {
            Scroll(ds.ByDiscountField(j), ds, j);
            ds.DiscountField(j).Click();
            ds.RecommenededDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
            ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
            decimal rcPriceChange = (Convert.ToDecimal(ds.RecommenededDiscount)+Convert.ToDecimal(ds.CompAnchorDiscount))/2;
            
            ClearAndUpdateField(ds,rcPriceChange.ToString(),j);
            Thread.Sleep(2000);
            return rcPriceChange.ToString();
        }

        public string AboveRecommended(DSAPageObject ds,int j)
        {
            Scroll(ds.ByDiscountField(j), ds, j);
            ds.DiscountField(j).Click();
            ds.comparisionObjects.CalDiscountedPricePerUnit = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%","");
            decimal aboveRecommndedPriceChange = Convert.ToDecimal(ds.comparisionObjects.CalDiscountedPricePerUnit) -2;
            ds.comparisionObjects.CalDiscountedPricePerUnit = aboveRecommndedPriceChange.ToString();
            ClearAndUpdateField(ds,aboveRecommndedPriceChange.ToString(),j);
            Thread.Sleep(2000);
            Console.WriteLine("AboveReommended :" +ds.DiscountField(j).GetAttribute("value"));
            ds.IsdiscountModified = true;
            return aboveRecommndedPriceChange.ToString();
        }

        public void ClearAndUpdateField(DSAPageObject ds,string priceChangeValue,int j)
        {
            ds.DiscountField(j).Clear();
            ds.DiscountField(j).SendKeys(priceChangeValue);
            try
            {
                CloseGuidancePopUp(ds);

                
            }
            catch(Exception ex)
            {

            }
            Thread.Sleep(3000);
        }

        public void CloseGuidancePopUp(DSAPageObject ds)
        {
            var close = WebDriver.FindElement(By.XPath("//smart-price-guidance//i[1]"));//close the smartprice guidance pop up
            if (close.Displayed)
            {
                new Actions(WebDriver).Click(close).Perform();
            }
        }

        public void Scroll(By xpath,DSAPageObject ds,int j)
        {
            Constant constant = new Constant(WebDriver);
            WebDriverWait wait = new WebDriverWait(WebDriver,TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementToBeClickable(ds.ByDiscountField(j)));
            try
            {
                ds.DiscountField(j).Click();

            }
           catch(Exception ex)
            {
                constant.jse.ExecuteScript("window.scrollBy(0,-600)", "");
                ds.DiscountField(j).Click();
            }
            
        
           
        }
        public void RetriveDataFromDSAScreenForSpecificPricePoint(TotalFinancialCalculation totalFinancialCalculation, string scenarioId, DSAPageObject ds, string ServiceHeaderText, List<ServicesModule> servicModule, string serviceListPrice,Constant constant,List<DSAPageObject> retriveSmartPriceData,TestCase test)
        {
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetching Products & Financials"), true);
            try
            {

                while (constant.Count < constant.NoOfProducts.Count)
                {


                    //   int i = constant.Count + 1;
                    DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                    if (!constant.IsElementPresent(constant.ByProduct))
                    {
                        constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);", WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_productDescription_0_" + constant.Count + "']")));
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_LI_productDescription_0_" + constant.Count + "']")));
                    }
                    //        Constant.jse.ExecuteScript("window.scrollBy(0, 14");


                    if (constant.IsElementPresent(constant.ByProduct))
                    {
                        try
                        {
                            constant.Product.Click();
                        }
                        catch (Exception ex)
                        {
                            Actions action = new Actions(WebDriver);
                            action.MoveToElement(constant.Product);
                            action.Click().Build().Perform();
                        }

                    }
                    Thread.Sleep(6000);

                    ///   bool isControlFound = FindControlOnPage(constant.ByViewMore(constant.Count), "400", constant);
                    constant.jse.ExecuteScript("window.scrollBy(0,500)");
                    if (constant.IsElementPresent(constant.ByViewMore(constant.Count)) /*&&isControlFound*/)
                    {
                        WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));

                        wait.Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMore(constant.Count)));

                        // Constant.jse.ExecuteScript("arguments[0].click()", constant.ViewMore(constant.Count));

                        constant.ViewMore(constant.Count).Click();

                    }
                    constant.jse.ExecuteScript("window.scrollBy(0, -500)");
                    Thread.Sleep(7000);
                    if (new WebDriverWait(WebDriver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementToBeClickable(constant.DiscountField)).Enabled)
                    {
                        //          Constant.jse.ExecuteScript("arguments[0].click()", constant.DiscountField);
                        constant.jse.ExecuteScript("window.scrollBy(0,200)");
                        constant.DiscountField.Click();
                    }
                    Thread.Sleep(7000);
                    retriveSmartPriceData.Add(drobj.RetriveDatafromDsaScreen(constant.Count, constant, ds,test,totalFinancialCalculation, ServiceHeaderText, serviceListPrice, servicModule,scenarioId));
                    //    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(constant.Count++.ToString(), true);
                    
                    Thread.Sleep(5000);


                    constant.Count++;
                    //   i++;

                    //   Console.WriteLine("........Retrived smart price details from Dsa Screen.........");
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                constant.Count = 0;
            }
        }
        public void RetriveDataFromDSAScreen(List<DSAPageObject> retriveSmartPriceDataList,TotalFinancialCalculation totalFinancialCalculation, bool isProduct,DSAPageObject ds, Constant constant, ResultLogging resultLog,int testCaseNumber,TestCase test,string ScenarioId, string ServiceHeaderText = null, List<ServicesModule> servicModule = null, string serviceListPrice = null,bool IsIndirect=false,CustomerClass customerClass=null)
        {
            WebDriver = ds.WebDriver;
            if (isProduct == true)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Updating discout field"), true);
                try
                {
                    int count = 0;
                    string GreaterThanR = "AboveRecommended";
                    string BetweenCAndF = "BetweenCompAnchorAndFloor";
                    string BetweenRAndC = "BetweeRecommendedAndCompAnchor";
                    string LessThanFloor = "BelowFloor";
                    string AtC = "AtCompAnchor";
                    string runId = RunId;
                    int index = 0;
                    if (!string.IsNullOrEmpty(test.ProductIdentificationData))
                    {
                        //string getOrderCodeAttributeName = WebDriver.FindElement(By.XPath("//*[contains(text(),'"+test.ProductIdentificationData+"')]")).GetAttribute("id");

                         index = Convert.ToInt32(ds.getOrderCodeAttributeName(test.ProductIdentificationData).Substring(ds.getOrderCodeAttributeName(test.ProductIdentificationData).Length - 1));
                    }
                   
                    DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                    CustomerPage customerPage = new CustomerPage(WebDriver);
                    WebDriverUtils.ScrollIntoView(WebDriver, constant.ByDiscountField);
               //         constant.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", constant.DiscountField.Location.X, constant.DiscountField.Location.Y);
                    
                        AboveRecommended(ds, index);
              //      string discountedIndex = index.ToString();
                    
                        Thread.Sleep(4000);
                     
                        D13DiscountChange(retriveSmartPriceDataList,resultLog, testCaseNumber, test, GreaterThanR, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant, runId,IsIndirect, customerClass);
                        count++;
                    EmptyTheList(retriveSmartPriceDataList);

                    BetweenRecommendedAndCompAnchor(ds,index);
                   // customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                   
                    D13DiscountChange(retriveSmartPriceDataList,resultLog, testCaseNumber, test, BetweenRAndC, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant, runId, IsIndirect, customerClass);
                    EmptyTheList(retriveSmartPriceDataList);
                    BetweenCompAnchorAndFloor(ds,index);
                   // customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    
                    D13DiscountChange(retriveSmartPriceDataList,resultLog, testCaseNumber, test, BetweenCAndF, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant,runId, IsIndirect, customerClass);

                    EmptyTheList(retriveSmartPriceDataList);
                    BelowFloor(ds,index);
                    //customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                   
                    D13DiscountChange(retriveSmartPriceDataList,resultLog, testCaseNumber, test, LessThanFloor, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant,runId, IsIndirect, customerClass);
                    EmptyTheList(retriveSmartPriceDataList);
                    AtCompAnchor(ds,index);
                    //customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    
                    D13DiscountChange(retriveSmartPriceDataList,resultLog, testCaseNumber, test, AtC, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant,runId, IsIndirect, customerClass);
                        


                    Thread.Sleep(4000);




                        //   int i = constant.Count + 1;




                        constant.Count++;
                        //   i++;

                        //   Console.WriteLine("........Retrived smart price details from Dsa Screen.........");
                    


                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                }
                finally
                {
                    constant.Count = 0;
                }
            }

        }

        private void EmptyTheList(List<DSAPageObject> retriveSmartPriceDataList)
        {
            if (retriveSmartPriceDataList.Count > 0)
            {
                retriveSmartPriceDataList.RemoveAt(0);
            }
        }

        public void CompareSmartPrice( string calculatedDiscountPrice,TotalFinancialCalculation totalFinancialCalculation, List<DSAPageObject> retriveSmartPriceDataList, List<ServicesModule> servicesModules = null, string scenarioId = null)
        {
            //SmartPriceCalculation(totalFinancialCalculation, servicesModules, scenarioId);//Calulation

            foreach (DSAPageObject dsaObjects in retriveSmartPriceDataList)
            {
                CompareFinancialGuidance(dsaObjects,calculatedDiscountPrice);
                foreach (DSAPageObject compareAccessory in dsaObjects.Accessories)
                {
                    CompareFinancialGuidance(compareAccessory);
                }

                if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) - Convert.ToDecimal(dsaObjects.LineItemSummeryTotalSellingPrice == "" ? "0" : dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() == dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = true;
                }

                else
                {
                    dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalSellingPrice ,";

                }

                //Total list price is not showing in Line item summary level, hence no need of comparision.
                //if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalListPrice) - Convert.ToDecimal(dsaObjects.TotalListPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalListPrice.ToString() == dsaObjects.TotalListPrice.Replace("$", "").Replace(",", ""))
                //{
                //    dsaObjects.comparisionObjects.IsTotalListPriceMatch = true;
                //}
                if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.LineItemSummerySmartPriceRevenue == "" ? "0" : dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2).ToString() == dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalActualSmartPriceRevenue ,";

                }

                if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.CalculatedTotalPricingModifier, 2) - Convert.ToDecimal(dsaObjects.LineItemSummeryPricingModifier)) == Constant.MinimalDifferences || Math.Round(dsaObjects.comparisionObjects.CalculatedTotalPricingModifier, 2).ToString() == dsaObjects.LineItemSummeryPricingModifier)
                {
                    dsaObjects.comparisionObjects.IsPricingModifierMatch = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalPricingModifier ,";

                }
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2)).ToString() == dsaObjects.SummaryPricingModifier.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalPricingModifierSummaryLevel ,";

                }

                // Compare List Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == dsaObjects.SummaryTotalListPrice.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalListpriceSummaryLevel ,";

                }
                // Compare Selling Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() == dsaObjects.SummarySellingPrice.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsSellingPricMatchSummaryLevel = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsSellingPricMatchSummaryLevel = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalSellingpriceSummaryLevel ,";

                }

            }

            //  Console.WriteLine(".........SmartPrice Gudance Comparision is completed........");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Financials compared with DSAs."), true);

        }
        public void CompareFinancialGuidance(DSAPageObject dsaObjects,string calculatedDiscountPrice)
        {
            try
            {
                if (dsaObjects.QuoteNumber == dsaObjects.PnrLog.QuoteNumber.ToString() && dsaObjects.SKUNumber == dsaObjects.PnrLog.SKUNum.ToString())
                {
                    if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2) - Convert.ToDecimal(dsaObjects.RecommenededSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2).ToString() == dsaObjects.RecommenededSellingPrice.Replace("$", "").Replace(",", ""))
                    {
                        dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedSellingPrice ,";

                    }
                    if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2) - Convert.ToDecimal(dsaObjects.CompAnchorSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2).ToString() == dsaObjects.CompAnchorSellingPrice.Replace("$", "").Replace(",", ""))
                    {

                        dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSellingPrice,";

                    }
                    if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2) - Convert.ToDecimal(dsaObjects.FloorSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2).ToString() == dsaObjects.FloorSellingPrice.Replace("$", "").Replace(",", ""))
                    {

                        dsaObjects.comparisionObjects.IsFloorSellingPriceMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorSellingPrice,";

                    }
                    //Compare SmartPrice_Revenue
                    if (Math.Abs(Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.RecommendedSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences
                        || Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.RecommendedSmartPricRevenue.Replace("$", "").Replace(",", ""))
                    {

                        dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommenededSPRevenue";

                    }

                    if (Math.Abs(Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.CompAnchorSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.CompAnchorSmartPricRevenue.Replace("$", "").Replace(",", ""))
                    {

                        dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSPRevenue,";

                    }
                    if (Math.Abs(Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.FloorSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.FloorSmartPricRevenue.Replace("$", "").Replace(",", ""))
                    {

                        dsaObjects.comparisionObjects.IsFloorSmartPriceRevenueMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsFloorSmartPriceRevenueMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorSPRevenue,";

                    }
                    //Compare Pricing Modifier
                    string CalculatedRecommendedPricingModifier = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier, 2).ToString();
                    if (CalculatedRecommendedPricingModifier.Length == 1)
                    {
                        CalculatedRecommendedPricingModifier = CalculatedRecommendedPricingModifier + ".00";
                    }


                    if (Math.Abs(Convert.ToDecimal(CalculatedRecommendedPricingModifier) - Convert.ToDecimal(dsaObjects.RecommendedPricingModifier)) == Constant.MinimalDifferences || CalculatedRecommendedPricingModifier.ToString() == dsaObjects.RecommendedPricingModifier)
                    {

                        dsaObjects.comparisionObjects.IsRecommendedPricingModifierMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsRecommendedPricingModifierMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedPricingModifier,";

                    }
                    string CalculatedCompAnchorPricingModifier = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier, 2).ToString();
                    if (CalculatedCompAnchorPricingModifier.Length == 1)
                    {
                        CalculatedCompAnchorPricingModifier = CalculatedCompAnchorPricingModifier + ".00";
                    }


                    if (Math.Abs(Convert.ToDecimal(CalculatedCompAnchorPricingModifier) - Convert.ToDecimal(dsaObjects.CompAnchorPricingModifier)) == Constant.MinimalDifferences || CalculatedCompAnchorPricingModifier == dsaObjects.CompAnchorPricingModifier)
                    {

                        dsaObjects.comparisionObjects.IsCompAnchorPricingModifierMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsCompAnchorPricingModifierMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorPricingModifier,";

                    }
                    string CalculatedFloorPricingModifier = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier, 2).ToString();
                    if (CalculatedFloorPricingModifier.Length == 1)
                    {
                        CalculatedFloorPricingModifier = CalculatedFloorPricingModifier + ".00";
                    }
                    if (Math.Abs(Convert.ToDecimal(CalculatedFloorPricingModifier) - Convert.ToDecimal(dsaObjects.FloorPricingModifier)) == Constant.MinimalDifferences || CalculatedFloorPricingModifier == dsaObjects.FloorPricingModifier)
                    {

                        dsaObjects.comparisionObjects.IsFloorPricingModifierMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsFloorPricingModifierMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorPricingModifier,";

                    }

                    if (Math.Abs(Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final),2) - Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%", ""))) == Constant.MinimalDifferences || Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final), 2) == Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%","")))
                    {

                        dsaObjects.comparisionObjects.IsRecommendedDOLPercentage_Final = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsRecommendedDOLPercentage_Final = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedDOLPercentage_Final,";  

                    }
                    if (Math.Abs(Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2) - Convert.ToDecimal(dsaObjects.CompAnchorDiscount.Replace("%", ""))) == Constant.MinimalDifferences || (Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2) == Convert.ToDecimal(dsaObjects.CompAnchorDiscount.Replace("%", ""))))
                    {

                        dsaObjects.comparisionObjects.IsCompAnchorDOLPercentage_Final = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsCompAnchorDOLPercentage_Final = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorDOLPercentage_Final,";

                    }
                    if (Math.Abs(Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final), 2) - Convert.ToDecimal(dsaObjects.FloorDiscount.Replace("%", ""))) == Constant.MinimalDifferences || (Math.Round(Convert.ToDecimal(dsaObjects.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final), 2) == Convert.ToDecimal(dsaObjects.FloorDiscount.Replace("%", ""))))
                    {

                        dsaObjects.comparisionObjects.IsFloorDOLPercentage_Final = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsFloorDOLPercentage_Final = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorDOLPercentage_Final,";

                    }

                    if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalUnitSellingPrice) - Convert.ToDecimal(dsaObjects.LineItemUnitSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalUnitSellingPrice.ToString() == dsaObjects.LineItemUnitSellingPrice.Replace("$", "").Replace(",", ""))
                    {
                        dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = true;
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(dsaObjects.comparisionObjects.CalUnitSellingPrice))
                        {
                            dsaObjects.comparisionObjects.IsUnitSellingPriceMatch = "false";
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "UnitSellingPrice ,";
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsUnitSellingPriceMatch = "";
                        }

                    }
                    if (Math.Abs(Convert.ToDecimal(calculatedDiscountPrice) - Convert.ToDecimal(dsaObjects.DiscountInputField.Replace("$", ""))) == Constant.MinimalDifferences || calculatedDiscountPrice == dsaObjects.DiscountInputField.Replace("$", "").Replace(",", ""))
                    {
                        dsaObjects.comparisionObjects.IsDiscountedPricePerUnitMatch = "true";
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(dsaObjects.comparisionObjects.CalUnitSellingPrice))
                        {
                            dsaObjects.comparisionObjects.IsDiscountedPricePerUnitMatch = "false";
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "DiscountInputField ,";
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsDiscountedPricePerUnitMatch = "";
                        }

                    }

                    ////Total list price is not showing in Line item summary level, hence no need of comparision.
                    ////if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalListPrice) - Convert.ToDecimal(dsaObjects.TotalListPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalListPrice.ToString() == dsaObjects.TotalListPrice.Replace("$", "").Replace(",", ""))
                    ////{
                    ////    dsaObjects.comparisionObjects.IsTotalListPriceMatch = true;
                    ////}
                    //if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2).ToString() == dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))
                    //    {
                    //        dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = true;
                    //    }
                    //else
                    //{
                    //    dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = false;
                    //    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalActualSmartPriceRevenue ,";

                    //}

                    //if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.CalculatedTotalPricingModifier, 2) - Convert.ToDecimal(dsaObjects.LineItemSummeryPricingModifier)) == Constant.MinimalDifferences || Math.Round(dsaObjects.comparisionObjects.CalculatedTotalPricingModifier, 2).ToString() == dsaObjects.LineItemSummeryPricingModifier)
                    //{
                    //    dsaObjects.comparisionObjects.IsPricingModifierMatch = true;
                    //}
                    //else
                    //{
                    //    dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = false;
                    //    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalPricingModifier ,";

                    //}
                    //-----------------------------------------------------------At Summary level---------------------------
                    // Compare Pricing Modifier at SummaryLevel.

                    //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalPricingModifier, 2)).ToString() == dsaObjects.SummaryPricingModifier.Replace("$", "").Replace(",", ""))
                    //{
                    //    dsaObjects.comparisionObjects.IsTotalPricingModifierMatch = true;
                    //}
                    //// Compare List Price at SummaryLevel.
                    //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == dsaObjects.SummaryTotalListPrice.Replace("$", "").Replace(",", ""))
                    //{
                    //    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                    //}
                    //// Compare Selling Price at SummaryLevel.
                    //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() == dsaObjects.SummarySellingPrice.Replace("$", "").Replace(",", ""))
                    //{
                    //    dsaObjects.comparisionObjects.IsSellingPricMatchSummaryLevel = true;
                    //}

                    //---------------------------------------------------
                    if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.ModifiedRevenue, 2).ToString() == dsaObjects.LineItemSummaryModifiedRevenue.Replace("$", "").Replace(",", ""))
                    {
                        dsaObjects.comparisionObjects.IsModifiedRevenueMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsModifiedRevenueMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "ModifiedRevenue ,";

                    }
                    if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.UpsellModifiedRevenue, 2).ToString() == dsaObjects.LineItemSummaryUpsellServicesModifiedRevenue)
                    {
                        dsaObjects.comparisionObjects.IsUpSellServicesModifiedRevenueMatch = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsUpSellServicesModifiedRevenueMatch = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "UpsellModifiedRevenue ,";

                    }
                    if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.WithServicesIncentive, 2).ToString() == dsaObjects.LineItemSummaryWithServicesIncentive)
                    {
                        dsaObjects.comparisionObjects.IsWithServicesIncentiveMatch = true;
                    }
                }
                else
                {
                    dsaObjects.comparisionObjects.IsWithServicesIncentiveMatch = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "WithServicesIncentive ,";

                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                dsaObjects.PnrLog.ProductUnitRecommendedgPrice = Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2);
                dsaObjects.PnrLog.ProductUnitCompAnchorPrice = Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2);
                dsaObjects.PnrLog.ProductUnitFloorPrice = Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2);
                dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2);
                dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2);
                dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2);
                dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier, 2);
                dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier, 2);
                dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier, 2);

            }
        }

        private void D13DiscountChange(List<DSAPageObject> retriveSmartPriceDataList, ResultLogging resultLog, int testCaseNumber, TestCase test, string aboveRecommended, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, DSAPageObject ds, string serviceListPrice, List<ServicesModule> servicModule, string ServiceHeaderText, DraftQuotePageObject drobj, Constant constant,string runId,bool isIndirect=false,CustomerClass customerClass=null)
        {
            
            
       //     RetriveDataFromDSAScreen(totalFinancialCalculation, scenarioId, ds, ServiceHeaderText, servicModule, serviceListPrice);
            RetriveDataFromDSAScreenForSpecificPricePoint(totalFinancialCalculation, scenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null,constant, retriveSmartPriceDataList,test);
       
            SmartPriceCalculationForPricePointChange(totalFinancialCalculation, ds, constant, retriveSmartPriceDataList,scenarioId);
            CompareSmartPrice(ds.comparisionObjects.CalDiscountedPricePerUnit,totalFinancialCalculation, retriveSmartPriceDataList,null, null );
           
            PublishReport(ds, resultLog, totalFinancialCalculation, scenarioId, testCaseNumber, test, retriveSmartPriceDataList, aboveRecommended,runId,isIndirect,customerClass);
            Thread.Sleep(3000);
        }
    }
    
}
        
            

      
