using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.Globalization;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D16
{
    public class ScenarioMain : D01.ScenarioMain
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




        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObj = new DSAPageObject(WebDriver);
          
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            
            Constant locator = new Constant(WebDriver);
            SmartPricePopUpClass SpPopUpObject = new SmartPricePopUpClass(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            string draftQuote = "";
            dsaObj.country = country;
            try
            {
               
               
                string withOutVersionQuoteNo = "";


                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(dsaObj, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(dsaObj, locator);
                    
                }
                else
                {
                    dsaObj.ActualQuote = test.BaseQuoteIdentificationDATA;
                    dsaObj.OrderCode = test.ProductIdentificationData;
                    SearchQuote(dsaObj, locator);
                    string[] str = dsaObj.ActualQuote.Split('.');
                    withOutVersionQuoteNo = str[0];
                    

                }
                if (!locator.IsElementPresent(locator.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(20), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                    string[] str1 = draftQuote.Split('.');
                    dsaObj.QuoteNumber = str1[0];

                }
               
                List<PNRAPILog> sp1 = new List<PNRAPILog>();
                List<PNRAPILog> sp2 = new List<PNRAPILog>();
                int index = 0;
                VerifyStandardpartnerDiscount( resultLog,  dsaObj,  locator,  SpPopUpObject, sp1, sp2, dsaObj.QuoteNumber,  odb,  testCaseNumber,  totalFinancialCalculation, scenarioId,test.StandardPartnerDiscountType,test.ProductIdentificationData,false,null,retriveSmartPriceData,test,true,application);

               


            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D16", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObj, totalFinancialCalculation, null, resultLog,test);
            }

        }

        public void VerifyStandardpartnerDiscount(ResultLogging resultLog, DSAPageObject dsaObj, Constant locator, SmartPricePopUpClass spPopUpObject, List<PNRAPILog> sp1, List<PNRAPILog> sp2, string withOutVersionQuoteNo, OracleDBConnection odb, int testCaseNumber, TotalFinancialCalculation totalFinancialCalculation, string scenarioId,string standardPartnerDiscountType, string productIdentificationData,bool isIndirect,CustomerClass customerObj,List<DSAPageObject> retriveSmartPriceData,TestCase testCase,bool IsStandardPartnerDiscount=false,List<string> application=null)
        {
            int index = 0;
            if (standardPartnerDiscountType == "AutoSelect")
            {

                ShowStandardPartnerDiscount(resultLog, dsaObj, locator, spPopUpObject, sp1, sp2, "", odb, 0, withOutVersionQuoteNo, totalFinancialCalculation, scenarioId, testCaseNumber,isIndirect,customerObj, retriveSmartPriceData,IsStandardPartnerDiscount,testCase);
            }
            if (standardPartnerDiscountType == "SpecificProduct")
            {
                if (!string.IsNullOrEmpty(productIdentificationData))
                {
                    index = Convert.ToInt32(dsaObj.getOrderCodeAttributeName(productIdentificationData).Substring(dsaObj.getOrderCodeAttributeName(productIdentificationData).Length - 1));
                }
                ShowStandardPartnerDiscount(resultLog, dsaObj, locator, spPopUpObject, sp1, sp2, productIdentificationData, odb, index, withOutVersionQuoteNo, totalFinancialCalculation, scenarioId, testCaseNumber,isIndirect, customerObj, retriveSmartPriceData,IsStandardPartnerDiscount,testCase);
            }
        }
        public void ClickOnStandardPartnerDiscount()
        {
            SmartPricePopUpClass spPopUpObject = new SmartPricePopUpClass(WebDriver);
            try
            {
                spPopUpObject.standardPartnerShowDiscount.Click();
            }
            catch (Exception ex)
            {
                throw new ShowStopperException("Unable to click on showsstopperException", ex);
               // Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unable to click standard partner discount {0}",ex.Message));
            }
        }

        public void ShowStandardPartnerDiscount(ResultLogging resultLog,DSAPageObject dsaObj, Constant locator, SmartPricePopUpClass spPopUpObject, List<PNRAPILog> sp1, List<PNRAPILog> sp2,string orderCode,OracleDBConnection odb,int index,string withOutVersionQuoteNo,TotalFinancialCalculation totalFinancialCalculation, string scenarioId,int testCaseNumber,bool isIndirect,CustomerClass customerObj,List<DSAPageObject> retriveSmartPriceData,bool IsStandardPartnerDiscount,TestCase testCase)
        {
            WebDriver = dsaObj.WebDriver;
            try
            {   
                if(locator.IsElementPresent(locator.ByViewMoreConfiguration(index)))
                {
                    //locator.ViewConfiguration(index).Click();
                    ClickOnProductConfiguration(index);



                }
                if (string.IsNullOrEmpty(dsaObj.SKUNumber))
                {
                    DraftQuotePageObject draftQuoteobj = new DraftQuotePageObject(WebDriver);
                    draftQuoteobj.RetriveOrderCodeAndSku(dsaObj, 0, 1);

                 HideConfiguration(index);


                }

                 sp1.Add(odb.RetriveSmartPriceDetails(withOutVersionQuoteNo, dsaObj.SKUNumber, dsaObj));
                
                if((sp1.Count < 1 && !isIndirect)? (sp1[1].IsDirect == "true" && sp1[1].IsPartner == "false"):( sp1[0].IsDirect == "true" && sp1[0].IsPartner == "false"))
                {
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByDiscountField(index)));
                    WebDriverUtils.ScrollIntoView(WebDriver, locator.ByDiscountField(index));
                    //Actions action = new Actions(WebDriver);
                    //action.MoveToElement(locator.DiscountField).Click();

                    //action.Perform();
                    if (locator.IsElementPresent(locator.ByDiscountField(index)))
                    {
                        locator.DiscountField(index).Click();
                    }
      
                    dsaObj.PnrLog.StandardPartnerDiscount = Constant.StandardPartnerDiscountText + Constant.showDiscountText;
                    if (spPopUpObject.standardDiscountTextWithShowDiscount.Text.Replace("\r\n","") == dsaObj.PnrLog.StandardPartnerDiscount)
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Standard Partner Message is showed", true);

                        spPopUpObject.standardPartnerShowDiscount.Click();
                        Thread.Sleep(3000);
                        if (!locator.IsElementPresent(spPopUpObject.ByshowDiscountvalue))
                        {
                            dsaObj.StandardpartnerDiscountText = spPopUpObject.standardPartnerShowDiscount.Text.Replace("%","");
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Standard Partner Message is not showed", true);

                            //Retrive  SP2 data from pnrapilog after showing discount
                            sp2.Add(odb.RetriveSmartPriceDetails(withOutVersionQuoteNo, dsaObj.SKUNumber, dsaObj));
                            //Compare two list ..
                            CompareTwoSmartPriceList(sp1, sp2, dsaObj);

                        }
                        
                        else
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Show Discount Message is showed", true);
                        }
                    }
                    
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Standard Partner Message is not showed", true);
                    }
                   
                    
                }
                else if (isIndirect==true)
                {
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByDiscountField(index)));
                    Actions action = new Actions(WebDriver);
                    action.MoveToElement(locator.DiscountField(index)).Click();

                    action.Perform();
                    if (locator.IsElementPresent(locator.ByDiscountField(index)))
                    {
                        locator.DiscountField(index).Click();
                    }
                    dsaObj.PnrLog.StandardPartnerDiscount = spPopUpObject.standardPartnerShowDiscount.Text;
                }
                else
                {
                    string Message = "Can not proceed  with this test  as a partner quote :Quote-" + withOutVersionQuoteNo + "IsDirect:" + sp1[0].IsDirect + "IsPartner:" + sp1[0].IsPartner;
                }
                DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                drobj.CloseSmartPricePopUp();//close the smartprice guidance pop up
               
               // List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
             //   RetriveDataFromDSAScreenForSpecificPricePoint(totalFinancialCalculation, scenarioId, dsaObj, null, null, null, locator, retriveSmartPriceData, null);
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,scenarioId,dsaObj);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                //          SmartPriceCalculationForPricePointChange(totalFinancialCalculation, dsaObj, locator, retriveSmartPriceData, scenarioId,IsStandardPartnerDiscount);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(testCase, dsaObj,dsaObj.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(testCase.TestCaseTitle, scenarioId, testCase.ApprovalType, quoteSummaryObject, testCase);
                }



                ResultLogging result = new ResultLogging();
                if (isIndirect)
                {
                    CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation, null, null, true);
                    if(sp1.Count>1)
                    result.GenerateReportInCSVIndirect(dsaObj, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", customerObj, true, null, sp1, sp2, IsStandardPartnerDiscount, sp1[0], sp2[0],quoteSummaryObject,goalDetails);
                    else
                        result.GenerateReportInCSVIndirect(dsaObj, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", customerObj, true,null,sp1, sp2,IsStandardPartnerDiscount,null,null,quoteSummaryObject,goalDetails);
                }
                else
                {
                    CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
                    result.GenerateReportInCSV(dsaObj, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", null, sp1, sp2, isIndirect, IsStandardPartnerDiscount,quoteSummaryObject,goalDetails);
                }
                resultLog.ExportToSqlServerDB(dsaObj, testCase, scenarioId, retriveSmartPriceData);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ShowStopperException exception= new ShowStopperException("Error occured in ShowStandardPartnerDiscount() in ScenarioMain class", ex);
                exception.LogException();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObj, totalFinancialCalculation, null, resultLog);
            }


        }

        private DSAPageObject CompareTwoSmartPriceList(List<PNRAPILog> sp1, List<PNRAPILog> sp2, DSAPageObject dsobj)
        {
            try
            {
                List<string> compareList = new List<string>();
                if (sp1[0].StandardPartnerDiscount != sp2[0].StandardPartnerDiscount)
                {
                    dsobj.comparisionObjects.IsStandardPartnerDiscount = true;

                }
                if (sp1[0].IsDirect != sp2[0].IsDirect)
                {
                    dsobj.comparisionObjects.IsDirect = true;

                }
                if (sp1[0].IsPartner != sp2[0].IsPartner)
                {
                    dsobj.comparisionObjects.IsPartner = true;

                }
                if (sp1[0].IsRegistered == sp2[0].IsRegistered)
                {
                    dsobj.comparisionObjects.IsResgistered = true;

                }
                if (Convert.ToDecimal(sp2[0].SmartPriceGuidance.RecommendedDOLPercentage_Final) == Convert.ToDecimal(dsobj.StandardpartnerDiscountText))
                {
                    dsobj.comparisionObjects.IsStandardPartnerDiscount = true;
                }
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(" Copmarision error occured in CompareTwoSmartPriceList() in ScenarioMain class: "+ex.Message, true);
            }
            return dsobj;
        }

        /// <summary>
        /// Calculate all the 5 pricing point for D13-PriceChange Scenario and store it in a list.
        /// </summary>
        /// <param name="totalFinancialCalculation"></param>
        /// <param name="ds"></param>
        private void SmartPriceCalculationForPricePointChange(TotalFinancialCalculation financialCalculation, DSAPageObject ds,Constant constant,List<DSAPageObject> retriveSmartPriceData,string scenarioId,bool IsstandardPartnerDiscount=false)
        {

            try
            {
                OracleDBConnection obj = new OracleDBConnection();


                foreach (DSAPageObject item in retriveSmartPriceData)
                {
                    int i = 0;
                    item.TiedId = constant.RandomNumber(100, 1000).ToString();
                    CalculateGuidanceandFinanacials(item, obj, financialCalculation, null, "D13");
                    //calculate unit selling price
                    if (ds.IsdiscountModified)
                    {

                        item.comparisionObjects.CalUnitSellingPrice = Math.Round((100 - Convert.ToDecimal(ds.comparisionObjects.CalDiscountedPricePerUnit)) * Convert.ToDecimal(item.UnitListPrice.Replace("$","").Replace(",","")) / 100,2).ToString();
                    }
                     else
                    {
                        item.comparisionObjects.CalUnitSellingPrice = "NA";
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
            catch (ArithmeticException arEx)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Arithmatic Exception is occured", true);
                //Console.WriteLine(ex.Message);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
                //Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                ShowStopperException exce = new ShowStopperException("UnExpected error came in SmartPriceCalculationForPricePointChange(),ScenarioMain class",ex);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                exce.LogException();
              
                //Console.WriteLine(ex.Message);
            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);

        }

        private void PublishReport( DSAPageObject ds,ResultLogging resultLog,TotalFinancialCalculation totalFinancialCalculation,string scenarioId,int testCaseNumber,TestCase test, List<DSAPageObject> retriveSmartPriceDataList,string pricePoint)
        {
            if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
            {
                DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

            }
            if (StaticBriefCase.ApplicationType.Contains("Goal"))
            {
                SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
            }
            resultLog.GenerateReportInCSV(ds, retriveSmartPriceDataList, totalFinancialCalculation, scenarioId, testCaseNumber, null, null,true, pricePoint,false,false,quoteSummaryObject,goalDetails);
            resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceDataList);


            Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
        }

        private void PricePointCalculation(DSAPageObject ds, TestCase test,List<DSAPageObject> pricePointList,int j)
        {
            try
            {
                
                AboveRecommended(ds,j);
                pricePointList.Add(ds);
                Thread.Sleep(4000);
              
                BetweenCompAnchorAndFloor(ds,j);
                pricePointList.Add(ds);
                Thread.Sleep(4000);
                BelowFloor(ds,j);
                pricePointList.Add(ds);
                Thread.Sleep(4000);
                AtCompAnchor(ds,j);
                pricePointList.Add(ds);
                Thread.Sleep(4000);
                BetweenRecommendedAndCompAnchor(ds,j);
                pricePointList.Add(ds);
                Thread.Sleep(4000);
                //close Guidance pop up

            }
            catch(Exception ex)
            {

            }
        }

        public void BetweenCompAnchorAndFloor(DSAPageObject ds,int j)
        {
            try
            {
                Scroll(ds.ByDiscountField(j), ds, j);
                ds.DiscountField(j).Click();
                ds.FloorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]")).Text.Replace("%", "");
                ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
                decimal cfPriceChange = (Convert.ToDecimal(ds.CompAnchorDiscount) + Convert.ToDecimal(ds.FloorDiscount)) / 2;

                ClearAndUpdateField(ds, cfPriceChange.ToString(), j);
                Thread.Sleep(2000);
            }
           
            catch (Exception ex)
            {
               throw new ShowStopperException("Unable to update discount,Please check BetweenCompAnchorAndFloor() in ScenarioMain Class ",ex);
            }

        }

        public void BelowFloor(DSAPageObject ds,int j)
        {
            try
            {
                Scroll(ds.ByDiscountField(j), ds, j);
                ds.DiscountField(j).Click();
                ds.FloorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]")).Text.Replace("%", "");
                decimal belowFloorPriceChange = (Convert.ToDecimal(ds.RecommenededDiscount) + 2);

                ClearAndUpdateField(ds, belowFloorPriceChange.ToString(), j);
                Thread.Sleep(6000);
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Unable to update discount,Please check BelowFloor() in ScenarioMain Class ", ex);
            }
        }

        public void AtCompAnchor(DSAPageObject ds,int j)
        {
            try
            {
                Scroll(ds.ByDiscountField(j), ds, j);
                ds.DiscountField(j).Click();
                ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");

                ClearAndUpdateField(ds, ds.CompAnchorDiscount.ToString(), j);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Unable to update discount,Please check AtCompAnchor() in ScenarioMain Class ", ex);
            }
            
        }

        public string BetweenRecommendedAndCompAnchor(DSAPageObject ds,int j)
        {
            decimal rcPriceChange;
            try
            {
                Scroll(ds.ByDiscountField(j), ds, j);
                ds.DiscountField(j).Click();
                ds.RecommenededDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
                ds.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
                 rcPriceChange = (Convert.ToDecimal(ds.RecommenededDiscount) + Convert.ToDecimal(ds.CompAnchorDiscount)) / 2;

                ClearAndUpdateField(ds, rcPriceChange.ToString(), j);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Unable to update discount,Please check BetweenRecommendedAndCompAnchor() in ScenarioMain Class ", ex); 
            }
            
            return rcPriceChange.ToString();
        }

        public string AboveRecommended(DSAPageObject ds,int j)
        {
            decimal aboveRecommndedPriceChange;
            try
            {
                Scroll(ds.ByDiscountField(j), ds, j);
                ds.DiscountField(j).Click();
                ds.comparisionObjects.CalDiscountedPricePerUnit = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text.Replace("%", "");
                 aboveRecommndedPriceChange = Convert.ToDecimal(ds.comparisionObjects.CalDiscountedPricePerUnit) - 2;
                ds.comparisionObjects.CalDiscountedPricePerUnit = aboveRecommndedPriceChange.ToString();
                ClearAndUpdateField(ds, aboveRecommndedPriceChange.ToString(), j);
                Thread.Sleep(2000);
                Console.WriteLine("AboveReommended :" + ds.DiscountField(j).GetAttribute("value"));
                ds.IsdiscountModified = true;
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Unable to update discount,Please check BetweenRecommendedAndCompAnchor() in ScenarioMain Class ", ex); 
            }

           
            return aboveRecommndedPriceChange.ToString();
        }

        public void ClearAndUpdateField(DSAPageObject ds,string priceChangeValue,int j)
        {
            try
            {
                ds.DiscountField(j).Clear();
                ds.DiscountField(j).SendKeys(priceChangeValue);

                CloseGuidancePopUp(ds);
                Thread.Sleep(3000);
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Unable to enter discount,Please check ClearAndUpdateField() in ScenarioMain Class ", ex);
            }





        }

        public void CloseGuidancePopUp(DSAPageObject ds)
        {
            try
            {
                var close = WebDriver.FindElement(By.XPath("//smart-price-guidance//i[1]"));//close the smartprice guidance pop up
                if (close.Displayed)
                {
                    new Actions(WebDriver).Click(close).Perform();
                }
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to close guidance", true);
            }
        }

        public void Scroll(By xpath,DSAPageObject ds,int j)
        {
            Constant element = new Constant(WebDriver);
            WebDriverWait wait = new WebDriverWait(WebDriver,TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            wait.Until(ExpectedConditions.ElementToBeClickable(ds.ByDiscountField(j)));
            try
            {
                ds.DiscountField(j).Click();

            }
           catch(Exception ex)
            {
                element.jse.ExecuteScript("window.scrollBy(0,-600)", "");
                ds.DiscountField(j).Click();
            }
            
        
           
        }
        private void RetriveDataFromDSAScreenForSpecificPricePoint(TotalFinancialCalculation totalFinancialCalculation, string scenarioId, DSAPageObject ds, string ServiceHeaderText, List<ServicesModule> servicModule, string serviceListPrice,Constant constant,List<DSAPageObject> retriveSmartPriceData,TestCase test)
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
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_LI_productDescription_0_" + constant.Count + "']")));
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
                        WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));

                        wait.Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMore(constant.Count)));

                        // Constant.jse.ExecuteScript("arguments[0].click()", constant.ViewMore(constant.Count));

                        constant.ViewMore(constant.Count).Click();

                    }
                    constant.jse.ExecuteScript("window.scrollBy(0, -500)");
                    Thread.Sleep(7000);
                    if (new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(constant.DiscountField(constant.Count))).Enabled)
                    {
                        //          Constant.jse.ExecuteScript("arguments[0].click()", constant.DiscountField);
                        constant.jse.ExecuteScript("window.scrollBy(0,200)");
                        constant.DiscountField(constant.Count).Click();
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
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                ShowStopperException showStopperException = new ShowStopperException("Error Found in RetriveDataFromDSAScreenForSpecificPricePoint() in ScenarioMain Class ", ex);
                showStopperException.LogException();
            }
            finally
            {
                constant.Count = 0;
            }
        }
        public void RetriveDataFromDSAScreen(TotalFinancialCalculation totalFinancialCalculation, bool isProduct,DSAPageObject ds, Constant constant, ResultLogging resultLog,int testCaseNumber,TestCase test,string ScenarioId, string ServiceHeaderText = null, List<ServicesModule> servicModule = null, string serviceListPrice = null)
        {
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
                
                    int index = 0;
                    if (!string.IsNullOrEmpty(test.ProductIdentificationData))
                    {
                        string getOrderCodeAttributeName = WebDriver.FindElement(By.XPath("//*[contains(text(),'"+test.ProductIdentificationData+"')]")).GetAttribute("id");

                         index = Convert.ToInt32(getOrderCodeAttributeName.Substring(getOrderCodeAttributeName.Length - 1));
                    }
                   
                    DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                    CustomerPage customerPage = new CustomerPage(WebDriver);
                        constant.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", constant.DiscountField(count).Location.X, constant.DiscountField(count).Location.Y);
                    
                        AboveRecommended(ds, index);
              //      string discountedIndex = index.ToString();
                        Thread.Sleep(4000);
                        D13DiscountChange(resultLog, testCaseNumber, test, GreaterThanR, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant);
                        count++;
                   

                    BetweenRecommendedAndCompAnchor(ds,index);
                   // customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    D13DiscountChange(resultLog, testCaseNumber, test, BetweenRAndC, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant);

                        BetweenCompAnchorAndFloor(ds,index);
                   // customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    D13DiscountChange(resultLog, testCaseNumber, test, BetweenCAndF, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant);
                        

                        BelowFloor(ds,index);
                    //customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    D13DiscountChange(resultLog, testCaseNumber, test, LessThanFloor, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant);
                        
                        AtCompAnchor(ds,index);
                    //customerPage.RefreshSmartPriceButton.Click();
                    Thread.Sleep(4000);
                    D13DiscountChange(resultLog, testCaseNumber, test, AtC, totalFinancialCalculation, ScenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, drobj, constant);
                        


                        Thread.Sleep(4000);




                        //   int i = constant.Count + 1;




                        constant.Count++;
                        //   i++;

                        //   Console.WriteLine("........Retrived smart price details from Dsa Screen.........");
                    


                }
                catch (ShowStopperException ex)
                {
                    // Console.WriteLine(ex.Message);
                    throw ex;
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
        public void CompareSmartPrice(TotalFinancialCalculation totalFinancialCalculation, List<DSAPageObject> retriveSmartPriceDataList, List<ServicesModule> servicesModules = null, string scenarioId = null,bool isinDirect=false,bool IsStandardPartnerDiscount=false)
        {
            //SmartPriceCalculation(totalFinancialCalculation, servicesModules, scenarioId);//Calulation
            try
            {
                foreach (DSAPageObject dsaObjects in retriveSmartPriceDataList)
                {
                    CompareFinancialGuidance(dsaObjects);
                    foreach (DSAPageObject compareAccessory in dsaObjects.Accessories)
                    {
                        CompareFinancialGuidance(compareAccessory);
                    }
                    if (!string.IsNullOrEmpty(dsaObjects.StandardpartnerDiscountText) && isinDirect == true)
                    {
                        string[] text = dsaObjects.StandardpartnerDiscountText.Split(':');
                        decimal discountText = Convert.ToDecimal(text[1].Replace("%", ""));
                        if (discountText == Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%", "")))
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Recommeded discount percentage is equal to standard partner discount", true);
                        }
                        else if (discountText < Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%", "")))
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Recommeded discount percentage is greater than standard partner discount", true);
                        }
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
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Financials compared with DSAs."), true);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unexpected Error found in CompareSmartPrice() in ScenarioMain class" + ex.Message,true);
            }

            //  Console.WriteLine(".........SmartPrice Gudance Comparision is completed........");
          

        }
        public void CompareFinancialGuidance(DSAPageObject dsaObjects,bool IsStandardPartnerDiscount)
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
                    if (dsaObjects.comparisionObjects.CalUnitSellingPrice!= "NA")
                    {
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
                    }
                    if (dsaObjects.comparisionObjects.CalDiscountedPricePerUnit!=null)
                    {
                        if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalDiscountedPricePerUnit) - Convert.ToDecimal(dsaObjects.DiscountInputField.Replace("$", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalDiscountedPricePerUnit.ToString() == dsaObjects.DiscountInputField.Replace("$", "").Replace(",", ""))
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
            catch(ShowStopperException exception)
            {
                throw exception;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                ShowStopperException showStopperException = new ShowStopperException("Error Found in CompareFinancialGuidance() in ScenarioMain Class ",ex);
                showStopperException.LogException();
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

        private void D13DiscountChange(ResultLogging resultLog, int testCaseNumber, TestCase test, string aboveRecommended, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, DSAPageObject ds, string serviceListPrice, List<ServicesModule> servicModule, string ServiceHeaderText, DraftQuotePageObject drobj, Constant constant)
        {
            try
            {
                List<DSAPageObject> retriveSmartPriceDataList = new List<DSAPageObject>();

                //     RetriveDataFromDSAScreen(totalFinancialCalculation, scenarioId, ds, ServiceHeaderText, servicModule, serviceListPrice);
                RetriveDataFromDSAScreenForSpecificPricePoint(totalFinancialCalculation, scenarioId, ds, ServiceHeaderText = null, servicModule = null, serviceListPrice = null, constant, retriveSmartPriceDataList, test);

                SmartPriceCalculationForPricePointChange(totalFinancialCalculation, ds, constant, retriveSmartPriceDataList, scenarioId);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceDataList, null, null);

                PublishReport(ds, resultLog, totalFinancialCalculation, scenarioId, testCaseNumber, test, retriveSmartPriceDataList, aboveRecommended);
            }
            catch(ShowStopperException exception)
            {
                throw exception;
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                ShowStopperException showStopperException = new ShowStopperException("Error Found in CompareFinancialGuidance() in ScenarioMain Class ", ex);
                showStopperException.LogException();
            }
            Thread.Sleep(3000);
        }
    }
    
}
        
            

      
