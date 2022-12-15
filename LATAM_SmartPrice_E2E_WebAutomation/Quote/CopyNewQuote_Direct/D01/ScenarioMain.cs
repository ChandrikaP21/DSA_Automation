using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;


namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D01
{
    public class ScenarioMain 
    {
        public readonly string TestUrl;
        //public static List<DSAPageObject> retriveSmartPriceData;
        //PNRAPICalulationCompare calculatedValue;
       
        public IWebDriver WebDriver;
        
        public ScenarioMain(string URL)
        {
            TestUrl = URL;
            //retriveSmartPriceData = new List<DSAPageObject>();
            //calculatedValue = new PNRAPICalulationCompare();
            WebDriver = new ChromeDriver();
            
        }
        public ScenarioMain(string URL,IWebDriver webDriver)
        {
            TestUrl = URL;
          
            WebDriver = webDriver;
            
        }
        public ScenarioMain()
        {
            
        }
        public List<DSAPageObject> RefreshGuidanceCompareAndGenerateReport(List<DSAPageObject> retriveSmartPriceDetailsList,TotalFinancialCalculation totalFinancialCalculation, DSAPageObject ds, ResultLogging resultLog, string scenarioId, int testCaseNumber, string RunId, string refreshId, CustomerClass customerObj, Constant constant, TestCase test,bool isindirect=false)
        {
            //List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            RetriveDataFromDSAScreenForSpecificPricePoint(totalFinancialCalculation, scenarioId, ds, null, null, null, constant, retriveSmartPriceDetailsList, test);
            SmartPriceCalculationForPricePointChange(totalFinancialCalculation, ds, constant, retriveSmartPriceDetailsList, scenarioId);
            CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceDetailsList, null, null);
            resultLog.GenerateReportInCSV(ds, retriveSmartPriceDetailsList, totalFinancialCalculation, scenarioId, testCaseNumber, RunId, refreshId, customerObj, null, null,isindirect);
            return retriveSmartPriceDetailsList;
        }
        public void CompareSmartPrice(TotalFinancialCalculation totalFinancialCalculation, List<DSAPageObject> retriveSmartPriceDataList, List<ServicesModule> servicesModules = null, string scenarioId = null)
        {
            //SmartPriceCalculation(totalFinancialCalculation, servicesModules, scenarioId);//Calulation

            foreach (DSAPageObject dsaObjects in retriveSmartPriceDataList)
            {
                CompareFinancialGuidance(dsaObjects);
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
        public void RetriveDataFromDSAScreenForSpecificPricePoint(TotalFinancialCalculation totalFinancialCalculation, string scenarioId, DSAPageObject ds, string ServiceHeaderText, List<ServicesModule> servicModule, string serviceListPrice, Constant constant, List<DSAPageObject> retriveSmartPriceData, TestCase test)
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
                    retriveSmartPriceData.Add(drobj.RetriveDatafromDsaScreen(constant.Count, constant, ds, test, totalFinancialCalculation, ServiceHeaderText, serviceListPrice, servicModule, scenarioId));
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
        public void SmartPriceCalculationForPricePointChange(TotalFinancialCalculation financialCalculation, DSAPageObject ds, Constant constant, List<DSAPageObject> retriveSmartPriceData, string scenarioId)
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

                        item.comparisionObjects.CalUnitSellingPrice = Math.Round((100 - Convert.ToDecimal(ds.comparisionObjects.CalDiscountedPricePerUnit)) * Convert.ToDecimal(item.UnitListPrice.Replace("$", "").Replace(",", "")) / 100, 2).ToString();
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
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                //Console.WriteLine(ex.Message);
            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);

        }
        public DateTime CurrentDateAndTime()
        {
            DateTime currentDateTime = DateTime.Now;
            return currentDateTime;
        }
        public string RunId=> CurrentDateAndTime().ToString().Replace(":", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("AM", "").Replace("PM", "");
        public bool FindControlOnPage( By Control, string Scrollvalue,Constant constant)
        {
            
           constant.jse.ExecuteScript("window.scrollBy(0, "+ Scrollvalue + ")");

            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMore(constant.Count)));

           if(ExpectedConditions.ElementToBeClickable(constant.ByViewMore(constant.Count))!=null)
                return true;
            else
                return FindControlOnPage(Control, Scrollvalue,constant);
        }


        /// <summary>
        /// open the respective URL
        /// Click on the Menu  and search for the quote 
        /// 
        /// </summary>
        public void LoadDSA()
        {
            try {
                Constant constant = new Constant(WebDriver);
                WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                int relativemaxwidth = 1280;
                int relativemaxheight = 800;
                var size = WebDriver.Manage().Window.Size;
                if (size.Height < relativemaxheight && size.Width < relativemaxwidth)
                {
                    WebDriver.Manage().Window.Maximize();
                }

                WebDriver.Navigate().GoToUrl(TestUrl);
          //      WebDriverUtils.WaitForPageLoad(WebDriver, TimeSpan.FromSeconds(20));
                WebDriverUtils.waitForWaitAnimationToLoad(WebDriver);
                if (WebDriver.ElementDisplayed(constant.ByMenuLabel)==false)
                {
                    WebDriver.Navigate().Refresh();
                }
                
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
                //      var a = constant.ByMenuLabel;
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(constant.ByMenuLabel));
                //Constant.jse.ExecuteScript("document.body.style.zoom='50%'");
                //Thread.Sleep(2000);
                constant.jse.ExecuteScript("arguments[0].click();", constant.MenuButton);
                // if (constant.IsElementPresent(constant.MenuQuoteSearch)) 
                // constant.clickOnQuote.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(constant.clickOnQuote));
                //    Console.WriteLine(".................Application is loaded and click on Menu..................");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
        public void SearchQuote(DSAPageObject dSA,Constant constant)
        {
           try
            {

                constant.jse.ExecuteScript("arguments[0].click()", constant.clickOnQuote);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(constant.BySearchQuoteNumber));
                int i = 0;
               //while(OracleDBConnection.retriveQuoteList.Count>i)
               // {
                    constant.SearchQuoteNumber.SendKeys(dSA.ActualQuote.ToString());
                    constant.jse.ExecuteScript("arguments[0].click()", constant.QuoteSearchButton);
                    WebDriverWait wait1 = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
                    try
                    {

                        wait1.Until(ExpectedConditions.ElementIsVisible(constant.ByMoreAction));
                        Thread.Sleep(2000);
                        //break;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("MoreAction is not visible"); 
                        constant.jse.ExecuteScript("arguments[0].click();", constant.MenuButton);
                        Thread.Sleep(3000);
                        constant.jse.ExecuteScript("arguments[0].click()", constant.ClickOnSearchQuote);
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
                        i++;
                    }
                    
                //}


                WebDriverWait wait2 = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));

                if (!constant.IsElementPresent(constant.ByMoreAction))
                {
                    SearchDraftQuote(dSA, constant);
                }
                else
                {
                    wait2.Until(ExpectedConditions.ElementToBeClickable(constant.ByMoreAction));
                }
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Quote {0} loaded", dSA.ActualQuote), true);
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message,true);
                Console.WriteLine(ex.Message);
                
            }

           // Console.WriteLine("............Search button is clicked..........");
        }
        public void SearchDraftQuote(DSAPageObject dSA,Constant constant)
        {
            try
            {
                    WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
                     wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(constant.ByDraftQuoteMenu));
                    constant.jse.ExecuteScript("arguments[0].click();", constant.DraftQuoteMenu);
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(constant.BydraftquoteSearchLink));
                  
                    constant.jse.ExecuteScript("arguments[0].click()", constant.draftquoteSearchLink);
                    
                    
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
                    constant.SearchDraftQuoteNumber.SendKeys(dSA.ActualQuote.ToString());
                    constant.jse.ExecuteScript("arguments[0].click()", constant.QuoteSearchButton);
                       
                    WebDriverWait wait1 = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
                    wait1.Until(ExpectedConditions.ElementIsVisible(constant.ByDraftQuoteNumber));
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);

            }

          //  Console.WriteLine("............Search button is clicked..........");
        }
        public string CopyAndCreateNewQuote()
        {
            Constant constant = new Constant(WebDriver);
            constant.MoreActionsDropdown.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCopyQuote));
            constant.CopyAsNewQuote.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_title_draftquote']")));// xpath change earlier h2 now h1
            string draftQuoteNumber = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_title_draftquote']")).FindElement(By.XPath("//small[1]")).Text;
            Console.WriteLine(draftQuoteNumber);
            //      Console.WriteLine(".......Quote is copied........");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("New Quote is  created . Quote {0}", draftQuoteNumber), true);
            
            return draftQuoteNumber;
        }
        

        public void RetriveDataFromDSAScreen(List<DSAPageObject> retriveSmartPriceData,TotalFinancialCalculation totalFinancialCalculation, string ScenarioId, DSAPageObject ds, string ServiceHeaderText = null, List<ServicesModule> servicModule = null, string serviceListPrice = null)
        {
            WebDriver = ds.WebDriver;
            Constant constant = new Constant(WebDriver);
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetching Products & Financials"), true);
            try
            {
                int productCount = constant.NoOfProducts.Count;
                while (constant.Count < productCount)
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
                        WebDriver.ScrollIntoView(constant.DiscountField);
                        if (constant.IsElementPresent(constant.ByDiscountField))
                        {
                            constant.DiscountField.Click();
                        }
                        else
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Discount is element is not present",true);
                        }
                        //}
                        Thread.Sleep(7000);
                        retriveSmartPriceData.Add(drobj.RetriveDatafromDsaScreen(constant.Count, constant, ds, null, totalFinancialCalculation, ServiceHeaderText, serviceListPrice, servicModule, ScenarioId));
                        //    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(constant.Count++.ToString(), true);

                        Thread.Sleep(5000);


                        constant.Count++;
                    }
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
        
        /// <summary>
        /// 
        /// </summary>
        public void SmartPriceCalculation(List<DSAPageObject> retriveSmartPriceData, TotalFinancialCalculation financialCalculation,List<ServicesModule> servicesModules=null,string scenarioId=null)
        {
            try
            {
                OracleDBConnection obj = new OracleDBConnection();
                Constant constant = new Constant(WebDriver);
            
                foreach (DSAPageObject item in retriveSmartPriceData)
                {
                    item.TiedId = constant.RandomNumber(100, 1000).ToString();
                    CalculateGuidanceandFinanacials(item, obj, financialCalculation, servicesModules,scenarioId);
                    if(item.DiscountInputField.ToString()=="0")
                    {
                        item.comparisionObjects.CalUnitSellingPrice = item.UnitListPrice.Replace("$", "").Replace(",", "");
                    }
                    else
                    {
                        item.comparisionObjects.CalUnitSellingPrice = (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(/*item.comparisionObjects.CalDiscountedPricePerUnit*/item.DiscountInputField) / 100).ToString();
                    }
                    
                    item.comparisionObjects.CalTotalSellingPrice = (Convert.ToDecimal(item.comparisionObjects.CalUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
                    //      item.comparisionObjects.CalListPrice = Convert.ToDecimal(item.TotalListPrice.Replace("$", "").Replace(",", ""));
                    item.comparisionObjects.CalListPrice = Convert.ToDecimal(item.UnitListPrice.Replace("$", "").Replace(",", ""))*Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity);

                    item.comparisionObjects.TotalActualSmartPriceRevenue = item.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                    
                    
                    item.comparisionObjects.CalculatedTotalPricingModifier = item.CalculatedActualSPCompFinancials.PricingModifier;
                    foreach (DSAPageObject accessory in item.Accessories)
                    {
                        accessory.TiedId = item.TiedId;
                        CalculateGuidanceandFinanacials(accessory, obj,financialCalculation);
                        accessory.comparisionObjects.CalTotalSellingPrice= (Convert.ToDecimal(accessory.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
                        accessory.comparisionObjects.CalListPrice = Convert.ToDecimal(accessory.TotalListPrice.Replace("$", "").Replace(",", ""));
                        accessory.comparisionObjects.TotalActualSmartPriceRevenue = accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        accessory.comparisionObjects.CalculatedTotalPricingModifier = accessory.comparisionObjects.TotalActualSmartPriceRevenue / accessory.comparisionObjects.CalTotalSellingPrice;


                        item.comparisionObjects.CalTotalSellingPrice += accessory.comparisionObjects.CalTotalSellingPrice;
                        item.comparisionObjects.CalListPrice += accessory.comparisionObjects.CalListPrice;
                        item.comparisionObjects.TotalActualSmartPriceRevenue += accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        item.comparisionObjects.CalculatedTotalPricingModifier = item.comparisionObjects.TotalActualSmartPriceRevenue / item.comparisionObjects.CalTotalSellingPrice;
                        //  item.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;;
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Accessory SmartPrice Financials Calculated."), true);
                    }
                    //foreach(ComponentModule prod in item.ComponentModules)
                    //{
                    // foreach(Component component in prod.Components)
                    //    {
                    //        item.comparisionObjects.CalTotalSellingPrice += component.SellingPrice;
                    //    }
                    //}
                    
                    financialCalculation.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;
                    financialCalculation.CalculatedTotalListpriceSummaryLevel += item.comparisionObjects.CalListPrice;
                    financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel += item.comparisionObjects.TotalActualSmartPriceRevenue;
                    financialCalculation.CalculatedTotalPricingModifierSummaryLevel = financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel / Convert.ToDecimal(financialCalculation.CalculatedTotalSellingpriceSummaryLevel);

                    

                   
                }
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                //Console.WriteLine(ex.Message);
            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);



        }

        /// <summary>
        /// 
        /// </summary>
        public void CompareSmartPrice(List<DSAPageObject> retriveSmartPriceData,TotalFinancialCalculation totalFinancialCalculation,List <ServicesModule> servicesModules=null,string scenarioId = null)
        {
            //SmartPriceCalculation(retriveSmartPriceData,totalFinancialCalculation,servicesModules, scenarioId);//Calulation
            
            foreach (DSAPageObject dsaObjects in retriveSmartPriceData)
            {
                CompareFinancialGuidance(dsaObjects);
                foreach (DSAPageObject compareAccessory in dsaObjects.Accessories)
                {
                    CompareFinancialGuidance(compareAccessory);
                }

                if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) - Convert.ToDecimal(dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$","").Replace(",","") == "" ? "0" : dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() == dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))
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
                if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$","").Replace(",","") == "" ? "0" : dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2).ToString() == dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))
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
        public void CompareFinancialGuidance(DSAPageObject dsaObjects)
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

                    //    if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) - Convert.ToDecimal(dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() == dsaObjects.LineItemSummeryTotalSellingPrice.Replace("$", "").Replace(",", ""))
                    //    {
                    //        dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = true;
                    //    }
                   
                    //    else
                    //    {
                    //        dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = false;
                    //        dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalSellingPrice ,";

                    //    }
                    
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
                    if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.ModifiedRevenue, 2).ToString() == dsaObjects.LineItemSummaryModifiedRevenue.Replace("$","").Replace(",",""))
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
        public void CalculateGuidanceandFinanacials(DSAPageObject item, OracleDBConnection odb,TotalFinancialCalculation financialCalculation,List<ServicesModule>servicesModules=null, string scenarioId=null)
        {
            

            item.PnrLog = odb.RetriveSmartPriceDetails(item.QuoteNumber, item.SKUNumber,item);//pulling from pnr apilog
                                                                                         //calculation
            if(item.PnrLog.DiscountedPricePerUnit==Convert.ToDecimal(item.LineItemUnitSellingPrice))
            item.CalculatedActualSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity),
                 item.PnrLog.DiscountedPricePerUnit * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));//need to change item.PnrLog.DiscountedPricePerUnit to calculate the selling price.
            else
                item.CalculatedActualSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity),
                Convert.ToDecimal(item.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));//need to change item.PnrLog.DiscountedPricePerUnit to calculate the selling price.

            item.PnrLog.ProductUnitRecommendedgPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final) / 100);

            item.CalculatedRecommendedSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
                  item.PnrLog.ProductUnitRecommendedgPrice);

            item.PnrLog.ProductUnitCompAnchorPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final) / 100);

            item.CalculatedCompAnchorSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
              item.PnrLog.ProductUnitCompAnchorPrice);

            item.PnrLog.ProductUnitFloorPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final) / 100);
            item.CalculatedFloorSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
                         item.PnrLog.ProductUnitFloorPrice);
            
             item.CalculatedModifiedServices = SmartPriceFinancialsCalculator.CalculateModifiedServices(item.CalculatedActualSPCompFinancials, Convert.ToDecimal(item.LineItemUnitSellingPrice), item.UpsellServiceSellingPrice, financialCalculation.CalculatedTotalServicesSellingPrice, item.PnrLog.ServiceModifier);
           
            Thread.Sleep(4000);
            

        }
        public void RemoveAllProductsExceptOneWithBaseService(Constant element, string OrderCode)
        {

            try
            {
                int i = 0;
                bool IsproductFound = false;
                List<IWebElement> viewmore = WebDriver.FindElements(By.XPath("//*[text()=' View More']")).ToList();
                //   List<IWebElement> orderCodeAndSku = WebDriver.FindElements(By.XPath("//*[text()='Sku Number' or text()='Order Code']")).ToList();
                string ordercodeid = "";
                string removeItem = "";
                while (i < viewmore.Count)
                {
                    try
                    {
                        ordercodeid = "quoteCreate_LI_orderCode_0_" + i + "";
                        removeItem = "quoteCreate_LI_removeItem_0_" + i + "";
                        Actions action = new Actions(WebDriver);
                        action.MoveToElement(viewmore[i]);
                        action.Click().Build().Perform();
                        if (element.DSAOrderCode(i).Displayed == true && !element.DSAOrderCode(i).Text.Equals(OrderCode))
                        {

                            string ordercode = element.DSAOrderCode(i).Text;
                            element.jse.ExecuteScript("window.scrollBy(0,-1000)");
                            element.RemoveProduct(i).Click();
                            //     element.RemoveProduct(ordercode).Click();

                            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.InvisibilityOfElementLocated(element.ByDSAOrderCode(i)));
                            //Console.WriteLine(ordercode + "is removed");
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("{0} is removed", ordercode), true);
                            viewmore = WebDriver.FindElements(By.XPath("//*[text()=' View More']")).ToList();
                            Thread.Sleep(2000);
                            IsproductFound = true;
                            i = 0;

                        }
                        else
                        {
                            if (element.IsElementPresent(element.ByViewLess))
                            {
                                element.ViewLess.Click();

                            }
                            i++;

                        }

                    }
                    catch (Exception ex)
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                        //Console.WriteLine(ex.Message);
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RemoveAllProductsExceptOneWithBaseService(Constant element,string orderCode,int productnumber)
        {
            try
            {
                if (string.IsNullOrEmpty(orderCode))
                {
                    int i = element.NoOfProducts.Count;
                    
                        while (i > productnumber)// 2>5 ??
                        {
                            i--;
                            element.RemoveProduct(i).Click();
                            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.InvisibilityOfElementLocated(element.ByDSAOrderCode(i)));
                        }
                    
                    
                    
                }
                else
                {
                    RemoveAllProductsExceptOneWithBaseService(element, orderCode);
                }
               

            }


            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                
            }

        }
        

        public  void ExecuteTestCases(TestCase test,int testCaseNumber, List<string> application=null)
        {


            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData= new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            string draftQuote = "";
            try
            {
				 LogintoDSAandSearchQuote(test, out draftQuote);

                if (application == null || application.Contains("SmartPrice"))
                {
                    RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, "D01", ds);
                    CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation, null, "D01");
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, "D01", testCaseNumber, RunId);
                    resultLog.ExportToSqlServerDB(ds, test, "D01", retriveSmartPriceData);
                    resultLog.ExportToSummaryTable(test, "D01");
                }
                if (application.Contains("Goal"))
                {
                    D01Flow goalFlow = new D01Flow(WebDriver);
                    goalFlow.ValidateD01GoalLite();
                    //GOAL.ExecuteGoalTestCases();
                }

                Console.WriteLine(test.TestCaseTitle);
              
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(e.Message, true);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb,ds,totalFinancialCalculation,cs,resultLog,test);
            }
        }

        public void LogintoDSAandSearchQuote(TestCase test, out string draftQuote)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
            LoadDSA();

            if (test.IsBaseQuoteIdentificationQueryBased == "Y")
            {
                odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                SearchQuote(ds, cs);
            }
            else
            {
                ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                SearchQuote(ds, cs);
            }
            draftQuote = "";
            if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
            {
                draftQuote = CopyAndCreateNewQuote();
            }
        }


    }
}



    



        
    


