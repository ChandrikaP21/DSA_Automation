using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.GOAL;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using java.awt;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using System.Globalization;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Pages.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using System.IO;
using System.Configuration;
using System.Drawing;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D01
{
    public class ScenarioMain 
    {
        public readonly string TestUrl;
        public DSAQuoteSummaryObject quoteSummaryObject;
        public GoalObject goalDetails;
        public IWebDriver WebDriver;
        public string _scenarioId;
        
        public ScenarioMain(string URL)
        {
            TestUrl = URL;
            //retriveSmartPriceData = new List<DSAPageObject>();
            //calculatedValue = new PNRAPICalulationCompare();
           
            WebDriver = new ChromeDriver();
            //ChromeOptions options = new ChromeOptions();
            //options.PageLoadStrategy = PageLoadStrategy.None;
            //WebDriver.Manage().Cookies.DeleteAllCookies();
        }
        public ScenarioMain(string URL,IWebDriver webDriver)
        {
            TestUrl = URL;
          
            WebDriver = webDriver;
            


        }
        public ScenarioMain(string URL, IWebDriver webDriver,string scenarioId)
        {
            TestUrl = URL;

            WebDriver = webDriver;
            _scenarioId = scenarioId;


        }

        public ScenarioMain()
        {
            
        }
        public List<DSAPageObject> RefreshGuidanceCompareAndGenerateReport(List<DSAPageObject> retriveSmartPriceDetailsList,TotalFinancialCalculation totalFinancialCalculation, DSAPageObject ds, ResultLogging resultLog, string scenarioId, int testCaseNumber, string refreshId, CustomerClass customerObj, Constant constant, TestCase test,bool isindirect=false,PNRAPILog pNRAPILog1=null,PNRAPILog pnrapilog2=null)
        {
            RetriveDataFromDSAScreen(retriveSmartPriceDetailsList, totalFinancialCalculation, scenarioId, ds);
            SmartPriceCalculation(retriveSmartPriceDetailsList, totalFinancialCalculation);
   
            CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceDetailsList, null, null);
            if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
            {
                DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

            }
            if (StaticBriefCase.ApplicationType.Contains("Goal"))
            {
                SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
            }
            if (isindirect==true)
            resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceDetailsList, totalFinancialCalculation, scenarioId, testCaseNumber, refreshId, customerObj,isindirect,"",null,null,false,pNRAPILog1,pnrapilog2,quoteSummaryObject,goalDetails);
            else
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceDetailsList, totalFinancialCalculation, scenarioId, testCaseNumber, refreshId, customerObj, null, null, isindirect, false,quoteSummaryObject,goalDetails);
            resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceDetailsList);
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

                if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) -
                    (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice,dsaObjects.country) == "" ?
                    "0" :GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice,dsaObjects.country)))) ==
                    Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() ==
                    GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice,dsaObjects.country))
                {
                    dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = true;
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
                if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue) == "" ? "0" : GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue,dsaObjects.country))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue,dsaObjects.country))
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
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2)).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummaryPricingModifier,dsaObjects.country))
                {
                    dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalPricingModifierSummaryLevel ,";

                }

                // Compare List Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummaryTotalListPrice,dsaObjects.country))
                {
                    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                }
                else
                {
                    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = false;
                    dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalListpriceSummaryLevel ,";

                }
                // Compare Selling Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() ==GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummarySellingPrice,dsaObjects.country))
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
               
                constant.ExpandProductfromShippingGroup(WebDriver);
                while (constant.Count < constant.NoOfProducts.Count)
                {

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
     protected   static string RemoveDietaries(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            stringBuilder.Replace("’", "");


            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
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
            catch (ArithmeticException ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Error found in SmartPriceCalculationForPricePointChange() in ScenarioMain Class, Erros: " + ex.Message, true);
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                Bedrock.Utilities.SimpleLogger.LogMessage("Error found in SmartPriceCalculationForPricePointChange() in ScenarioMain Class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Error found in SmartPriceCalculationForPricePointChange() in ScenarioMain Class" + ex.StackTrace);
            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);

        }
        public DateTime CurrentDateAndTime()
        {
            DateTime currentDateTime = DateTime.Now;
            return currentDateTime;
        }
        public bool FindControlOnPage( By Control, string Scrollvalue,Constant constant)
        {
            
           constant.jse.ExecuteScript("window.scrollBy(0, "+ Scrollvalue + ")");

            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
                DSAQuoteSearchPage dSAQuoteSearchObj = new DSAQuoteSearchPage(WebDriver);
                DSAPageObject dsobj = new DSAPageObject(WebDriver);
                WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(40);
                
                
                WebDriver.Manage().Window.Maximize();
                NavigateToDSA();
                
                CloseReleasePopUp();
               new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(dSAQuoteSearchObj.MenuQuoteSearch));

                if (constant.IsElementPresent(dSAQuoteSearchObj.MenuQuoteSearch))
               
          //      new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(dSAQuoteSearchObj.MenuQuoteSearch));
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(".................Application is loaded and click on Quote Menu ..................", true);

               
            }
            
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Can not load DSA-URL:"+TestUrl, true);
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException("Can not Load DSA, Please Check LoadDSA() in ScenarioMain Class",ex);
                throw exception;
               
            }
           
        }

        private void NavigateToDSA()
        {
            try
            {
                WebDriver.Navigate().GoToUrl(TestUrl);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.Id("menu_home")));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to navigate to DSA ,Please CheckInnerException", ex);
            }

            
        }

        private void CloseReleasePopUp()
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'wm-shoutout')]/div[2]")));
                if (constant.IsElementPresent(By.XPath("//*[contains(@id,'wm-shoutout')]/div[2]")))
                {
                    List<IWebElement> li = WebDriver.FindElements(By.XPath("//*[contains(@id,'wm-shoutout')]/div[2]")).ToList();
                    for (int i = li.Count - 1; i >= 0; i--)
                    {
                        li[i].Click();
                    }


                }
                
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("TimeOut Exception" + ex);

            }

        }

        /// <summary>
        /// Authenticate Browser PopUp using userName and Password
        /// </summary>
        public void Alert()
        {
            WebDriverWait wait = new WebDriverWait(WebDriver,TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.SetAuthenticationCredentials("userName","Password");
        }
        public void GetBrokenURL_Link()
        {
            string actualTitle = WebDriver.Title;
            string expected = "ss";
               
           
        }
        private void SetWindowSize()
        {
            var size = WebDriver.Manage().Window.Size;

            WebDriver.Manage().Window.Size = new Size(2000,1000);
        }

        

        public void SearchQuote(DSAPageObject dSA,Constant constant)
        {
            
            DSAQuoteSearchPage dSAQuoteSearchObj= new DSAQuoteSearchPage(WebDriver);
            DSAQuoteSummaryPage quoteSummarrayObj = new DSAQuoteSummaryPage(WebDriver);
           try
            {

                //Thread.Sleep(6000);
                //       WebDriverUtils.WaitForElementDisplayed(WebDriver, dSAQuoteSearchObj.BySearchQuote, TimeSpan.FromSeconds(5));
                //new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(dSA.ByClickOnBUId));
                WebDriverUtils.WaitForElementDisplayed(WebDriver, dSA.ByClickOnBUId, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                //CloseReleasePopUp();
             dSA.ChangeCountry(dSA.country);
              CloseReleasePopUp();
                constant.jse.ExecuteScript("arguments[0].click()", dSAQuoteSearchObj.clickOnQuote);
                WebDriverUtils.WaitForElementDisplayed(WebDriver, dSAQuoteSearchObj.BySearchQuote, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
               
                
                dSAQuoteSearchObj.SearchQuote.Click();
            
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(dSAQuoteSearchObj.ByQuoteSearchPage));
                dSAQuoteSearchObj.SearchQuoteNumber.SendKeys(dSA.ActualQuote.ToString());
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(dSAQuoteSearchObj.QuoteSearchButton));
                Thread.Sleep(1000);
                constant.jse.ExecuteScript("arguments[0].click()", dSAQuoteSearchObj.QuoteSearchButton);
         //       new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(dSAQuoteSearchObj.ByQuoteSearchPage));
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Quote {0} loaded", dSA.ActualQuote), true);
            }
           

            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Error occured during search quote ,Please check SearchQuote : " + ex.Message, true);
            }


        }
        

        public void SearchDraftQuote(DSAPageObject dSA,Constant constant)
        {
            try
            {
                    WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                     wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(constant.ByDraftQuoteMenu));
                    constant.jse.ExecuteScript("arguments[0].click();", constant.DraftQuoteMenu);
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(constant.BydraftquoteSearchLink));
                  
                    constant.jse.ExecuteScript("arguments[0].click()", constant.draftquoteSearchLink);
                    
                    
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
                    constant.SearchDraftQuoteNumber.SendKeys(dSA.ActualQuote.ToString());
                  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(constant.ByQuoteSearchButton));
                constant.jse.ExecuteScript("arguments[0].click()", constant.QuoteSearchButton);
                       
                    WebDriverWait wait1 = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                    wait1.Until(ExpectedConditions.ElementIsVisible(constant.ByDraftQuoteNumber));
                
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);

            }

          //  Console.WriteLine("............Search button is clicked..........");
        }
        public string CopyAndCreateNewQuote()
        {
            DSAQuoteSummaryPage dsaSummaryObj = new DSAQuoteSummaryPage(WebDriver);
            Constant constant = new Constant(WebDriver);
            //constant.MoreActionsDropdown.Click();
            dsaSummaryObj.MoreActions.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCopyQuote));
            constant.CopyAsNewQuote.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_title_draftquote']")));// xpath change earlier h2 now h1
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
            Product product = new Product(WebDriver);
     //       constant.ExpandProductfromShippingGroup(WebDriver);
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetching Products & Financials"), true);
            try
            {
              
                int productCount = product.NoOfProducts.Count;
                
                    while (constant.Count < productCount)
                    {
                    IWebElement[] element = constant.DSAConfiguration.ToArray();
                    DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                        IsProductPresent();

                //        constant.jse.ExecuteScript("window.scrollBy(0,500)");

                    ClickOnProductConfiguration(constant.Count);
                   //constant.jse.ExecuteScript("arguments[0].scrollIntoView();", element[constant.Count]);
                    //if(constant.IsElementPresent(constant.ByConfiguration))
                    //element[constant.Count].Click();
                        Thread.Sleep(7000);
                        ClickOnDiscountField(constant.Count);
                        if (new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(constant.DiscountField(constant.Count))).Enabled)
                        {
                            retriveSmartPriceData.Add(drobj.RetriveDatafromDsaScreen(constant.Count, constant, ds, null, totalFinancialCalculation, ServiceHeaderText, serviceListPrice, servicModule, ScenarioId));

                            Thread.Sleep(1000);

                        HideConfiguration(constant.Count);
                        
                        
                        Thread.Sleep(3000);
                            constant.Count++;
                        }

                    

                    }
                
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                constant.Count = 0;
            }

        }
        
        private void IsProductPresent()
        {
            Constant constant = new Constant(WebDriver);
            if (constant.IsElementPresent(constant.ByProduct))
            {
                try
                {
                    constant.Product.Click();
                }
                catch (Exception ex)
                {
                    try
                    {
                        Actions action = new Actions(WebDriver);
                        action.MoveToElement(constant.Product);
                        action.Click().Build().Perform();
                    }
                    catch (Exception exc)
                    {
                        WebDriverUtils.TakeSnapShot(WebDriver);
                        new Bedrock.ExceptionHandlingBlock.ShowStopperException("Unable to click on product", exc);
                    }
                }
                Thread.Sleep(6000);
            }
        }

        public void ClickOnDiscountField(int count)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                WebDriver.ScrollIntoView(constant.DiscountField(count));
                if (constant.IsElementPresent(constant.ByDiscountField(count)))
                {
                    
                    WebDriverUtils.ScrollIntoView(WebDriver, constant.ByDiscountField(count));
                    WebDriverUtils.WaitForElementVisible(WebDriver, constant.ByDiscountField(count), 2);
                    constant.jse.ExecuteScript("window.scrollBy(0,-200)");
                    constant.DiscountField(count).Click();
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Discount is element is not present", true);
                }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to click on DIscount field", ex);
            }
            //}
            Thread.Sleep(7000);
        }

        public void ClickOnProductConfiguration(int count)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                if (!constant.IsElementPresent(constant.ByIsConfigurationTabExpanded(count)))
                {
                    WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));

                    wait.Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMoreConfiguration(count)));

                    WebDriverUtils.ScrollIntoView(WebDriver, constant.ByViewMoreConfiguration(count));

                    constant.ViewConfiguration(count).Click();
                   

                }
                
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException("Unable to click on product configuration",ex);
                throw exception;
            }
           
        }
        public void HideConfiguration(int count)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                
                if (constant.IsElementPresent(constant.ByIsConfigurationTabExpanded(constant.Count)))
                {
                    WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));

                    wait.Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMoreConfiguration(count)));

                    WebDriverUtils.ScrollIntoView(WebDriver, constant.ByViewMoreConfiguration(count));
                    constant.ViewConfiguration(constant.Count).Click();
                    //constant.jse.ExecuteScript("arguments[0].click;", constant.Configuration(constant.Count));
                }
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException("Unable to click on product configuration", ex);
                throw exception;
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
                        //item.comparisionObjects.CalUnitSellingPrice = item.UnitListPrice.Replace("$", "").Replace(",", "");
                        item.comparisionObjects.CalUnitSellingPrice = GenericHelper.ConvertCurrencyIntoString(item.UnitListPrice,item.country);
                    }
                    else
                    {
                        item.comparisionObjects.CalUnitSellingPrice = (item.PnrLog.ListPricePerUnit-(item.PnrLog.ListPricePerUnit * Convert.ToDecimal(/*item.comparisionObjects.CalDiscountedPricePerUnit*/item.DiscountInputField) / 100)).ToString();
                    }
                    
                    item.comparisionObjects.CalTotalSellingPrice = (Convert.ToDecimal(item.comparisionObjects.CalUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
                    //      item.comparisionObjects.CalListPrice = Convert.ToDecimal(item.TotalListPrice.Replace("$", "").Replace(",", ""));
                    item.comparisionObjects.CalListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(item.UnitListPrice,item.country))*Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity);

                    item.comparisionObjects.TotalActualSmartPriceRevenue = item.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                    
                    
                    item.comparisionObjects.CalculatedTotalPricingModifier = item.CalculatedActualSPCompFinancials.PricingModifier;
                    foreach (DSAPageObject accessory in item.Accessories)
                    {
                        accessory.TiedId = item.TiedId;
                        CalculateGuidanceandFinanacials(accessory, obj,financialCalculation);
                        accessory.comparisionObjects.CalTotalSellingPrice= (Convert.ToDecimal(accessory.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
                        accessory.comparisionObjects.CalListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(accessory.TotalListPrice,item.country));
                        accessory.comparisionObjects.TotalActualSmartPriceRevenue = accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        accessory.comparisionObjects.CalculatedTotalPricingModifier = accessory.comparisionObjects.TotalActualSmartPriceRevenue / accessory.comparisionObjects.CalTotalSellingPrice;


                        item.comparisionObjects.CalTotalSellingPrice += accessory.comparisionObjects.CalTotalSellingPrice;
                        item.comparisionObjects.CalListPrice += accessory.comparisionObjects.CalListPrice;
                        item.comparisionObjects.TotalActualSmartPriceRevenue += accessory.CalculatedActualSPCompFinancials.SmartPriceRevenue;
                        item.comparisionObjects.CalculatedTotalPricingModifier = item.comparisionObjects.TotalActualSmartPriceRevenue / item.comparisionObjects.CalTotalSellingPrice;
                        //  item.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;;
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Accessory SmartPrice Financials Calculated."), true);
                    }
                    
                    financialCalculation.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;
                    financialCalculation.CalculatedTotalListpriceSummaryLevel += item.comparisionObjects.CalListPrice;
                    financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel += item.comparisionObjects.TotalActualSmartPriceRevenue;
                    financialCalculation.CalculatedTotalPricingModifierSummaryLevel = financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel / Convert.ToDecimal(financialCalculation.CalculatedTotalSellingpriceSummaryLevel);

                }
            }

            catch (ArithmeticException ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.StackTrace);

            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknown error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknow error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.StackTrace);

            }
            // Console.WriteLine("....... SmartPriceCalculation.......");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("SmartPrice Financials Calculated."), true);



        }

        /// <summary>
        /// 
        /// </summary>
        public void CompareSmartPrice(List<DSAPageObject> retriveSmartPriceData,TotalFinancialCalculation totalFinancialCalculation,List <ServicesModule> servicesModules=null,string scenarioId = null,bool isinDirect = false)
        {
            //SmartPriceCalculation(retriveSmartPriceData,totalFinancialCalculation,servicesModules, scenarioId);//Calulation
            try
            {
                foreach (DSAPageObject dsaObjects in retriveSmartPriceData)
                {
                    CompareFinancialGuidance(dsaObjects, dsaObjects.comparisionObjects.CalDiscountedPricePerUnit);
                    foreach (DSAPageObject compareAccessory in dsaObjects.Accessories)
                    {
                        CompareFinancialGuidance(compareAccessory);
                    }
                    if (!string.IsNullOrEmpty(dsaObjects.StandardpartnerDiscountText) && isinDirect == true)
                    {
                        string[] text = dsaObjects.StandardpartnerDiscountText.Split(':');
                        decimal discountText = Convert.ToDecimal(text[0].Replace("%", ""));//code changed due to australia code breaking down
                        if (discountText == Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%", "")))
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Recommeded discount percentage is equal to standard partner discount", true);
                        }
                        else if (discountText < Convert.ToDecimal(dsaObjects.RecommenededDiscount.Replace("%", "")))
                        {
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Recommeded discount percentage is greater than standard partner discount", true);
                        }
                    }
                    if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) -
                        Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice, dsaObjects.country)
                        == "" ? "0" : GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice, dsaObjects.country))) 
                        == Constant.MinimalDifferences
                        || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() == 
                        GenericHelper.ConvertCurrencyIntoString(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummeryTotalSellingPrice, dsaObjects.country)))
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
                    if (Math.Abs(Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue, dsaObjects.country) == "" ? "0" : GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue, dsaObjects.country))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.comparisionObjects.TotalActualSmartPriceRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummerySmartPriceRevenue, dsaObjects.country)))
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
                    if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2)).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummaryPricingModifier, dsaObjects.country))
                    {
                        dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsPricingModifierMatchSummaryLevel = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalPricingModifierSummaryLevel ,";

                    }

                    // Compare List Price at SummaryLevel.
                    if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummaryTotalListPrice, dsaObjects.country))
                    {
                        dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                    }
                    else
                    {
                        dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = false;
                        dsaObjects.StatusComments = dsaObjects.StatusComments + "TotalListpriceSummaryLevel ,";

                    }
                    // Compare Selling Price at SummaryLevel.
                    if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.SummarySellingPrice, dsaObjects.country))
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
            catch (ArithmeticException ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.StackTrace);

            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknown error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknow error found in Calculate () in SmartPriceFinancialsCalculator class" + ex.StackTrace);

            }
            

           
            
            
        }
        public void CompareFinancialGuidance(DSAPageObject dsaObjects,string calculatedDiscount="")
             {
                try 
                {
                    if (dsaObjects.QuoteNumber == dsaObjects.PnrLog.QuoteNumber.ToString() && dsaObjects.SKUNumber == dsaObjects.PnrLog.SKUNum.ToString())
                    {
                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.RecommenededSellingPrice, dsaObjects.country))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.RecommenededSellingPrice,dsaObjects.country))
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedSellingPrice ,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.CompAnchorSellingPrice,dsaObjects.country))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.CompAnchorSellingPrice,dsaObjects.country))
                        {

                            dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSellingPrice,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.FloorSellingPrice,dsaObjects.country))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.FloorSellingPrice,dsaObjects.country))
                        {

                            dsaObjects.comparisionObjects.IsFloorSellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorSellingPrice,";

                        }
                        //Compare SmartPrice_Revenue
                        if (Math.Abs(Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.RecommendedSmartPricRevenue,dsaObjects.country))) == Constant.MinimalDifferences
                            || Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.RecommendedSmartPricRevenue,dsaObjects.country))
                        {

                            dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommenededSPRevenue";

                        }

                        if (Math.Abs(Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.CompAnchorSmartPricRevenue, dsaObjects.country))) == Constant.MinimalDifferences || Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.CompAnchorSmartPricRevenue,dsaObjects.country))
                        {

                            dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSPRevenue,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.FloorSmartPricRevenue, dsaObjects.country))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.FloorSmartPricRevenue,dsaObjects.country))
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
                    if (!string.IsNullOrEmpty(calculatedDiscount))
                    {
                        if (Math.Abs(Convert.ToDecimal(calculatedDiscount) - Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(dsaObjects.DiscountInputField, dsaObjects.country))) == Constant.MinimalDifferences || calculatedDiscount == GenericHelper.ConvertCurrencyIntoString(dsaObjects.DiscountInputField, dsaObjects.country))
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

                    
                    if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.ModifiedRevenue, 2).ToString() == GenericHelper.ConvertCurrencyIntoString(dsaObjects.LineItemSummaryModifiedRevenue,dsaObjects.country))
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
            catch (ArithmeticException ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic Error: "+ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in CompareFinancialGuidance class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in CompareFinancialGuidance class" + ex.StackTrace);

            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknown Error: " + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknown error found in Calculate () in CompareFinancialGuidance class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unknow error found in Calculate () in CompareFinancialGuidance class" + ex.StackTrace);

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

            try
            {
                item.PnrLog = odb.RetriveSmartPriceDetails(item.QuoteNumber, item.SKUNumber, item);//pulling from pnr apilog
                                                                                                   //calculation
                if (item.PnrLog.DiscountedPricePerUnit == Convert.ToDecimal(item.LineItemUnitSellingPrice))
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
            catch(ArithmeticException ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Error found in CalculateGuidanceandFinanacials() in ScenarioMain Class, Erros: "+ex.Message, true);
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message,true);
                Bedrock.Utilities.SimpleLogger.LogMessage("Error found in CalculateGuidanceandFinanacials() in ScenarioMain Class" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Error found in CalculateGuidanceandFinanacials() in ScenarioMain Class"+ex.StackTrace);
            }
            

        }

        public void RemoveAllProductsExceptOneWithBaseService(Constant element, string OrderCode)
        {
            DSAQuoteSummaryPage dsaSummaryObject = new DSAQuoteSummaryPage(WebDriver);
            try
            {
                int i = 0;
                bool IsproductFound = false;
                List<IWebElement> viewmore = element.DSAConfiguration.ToList();
                if (viewmore.Count>1)
                {


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
                            action.MoveToElement(element.DSAConfiguration[i]);
                            action.Click().Build().Perform();
                            try
                            {
                                if (element.DSAOrderCode(i).Displayed == true && !element.DSAOrderCode(i).Text.Equals(OrderCode))
                                {

                                    string ordercode = element.DSAOrderCode(i).Text;
                                    WebDriverUtils.ScrollIntoView(WebDriver, element.ByRemoveProduct(i));
                                    element.RemoveProduct(i).Click();
                                    WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                                    WebDriver.WaitingForSpinner(dsaSummaryObject.spinner);
                                    //element.ExpandProductfromShippingGroup(WebDriver);//this code will be removed once the 0504 code is deployed.
                                    element.GoToAddItemPage();
                                    //     new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.InvisibilityOfElementLocated(element.ByDSAOrderCode(i)));
                                    //Console.WriteLine(ordercode + "is removed");
                                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("{0} is removed", ordercode), true);

                                    viewmore = element.DSAConfiguration.ToList();
                                    Thread.Sleep(2000);
                                    IsproductFound = true;
                                    i = 0;

                                }
                                else
                                {
                                    if (element.IsElementPresent(element.ByHideConfiguration(i)))
                                    {
                                        element.Configuration(i).Click();

                                    }
                                    i++;

                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        catch (Exception ex)
                        {
                            WebDriverUtils.TakeSnapShot(WebDriver);
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                            throw new ShowStopperException("Unable to remove Product,Please refer RemoveAllProductsExceptOneWithBaseService()", ex);
                        }

                    }
                }
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage($"Unexpected error is showing in RemoveAllProductsExceptOneWithBaseService(){ex.Message}");
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
                            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.InvisibilityOfElementLocated(element.ByDSAOrderCode(i)));
                        }
                    
                    
                    
                }
                else
                {
                    RemoveAllProductsExceptOneWithBaseService(element, orderCode);
                }
               

            }


            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                
            }

        }
        public List<Translate> LoadDeserializeObject()
        {
            List<Translate> list = TranslateContentLanguage.SealizeObejectToXMLString();
            return list;
        }
        

        public  void ExecuteTestCases(TestCase test,string scenarioId,int testCaseNumber,string country=null, List<string> application = null)
        {


            WebDriver = new ChromeDriver();
            //ScenarioId = scenarioId;
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData= new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            string draftQuote = "";
            ds.country = country;
            try
            {
				 LogintoDSAandSearchQuote(test, out draftQuote,ds);
                
                if (application == null || application.Contains("SmartPrice"))
                {
                    RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, "D01", ds);
                    SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                    CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation, null, "D01");
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, "D01", testCaseNumber);
                    //resultLog.ExportToSqlServerDB(ds, test, "D01", retriveSmartPriceData);
                    //resultLog.ExportToSummaryTable(test, "D01");
                    resultLog.ExportToSqlServerDB(ds, test, "D01", retriveSmartPriceData);
                    
                }
                if (application.Contains("GoalLite"))
                {
                    D01Flow goalFlow = new D01Flow(WebDriver);
                    goalFlow.ValidateD01GoalLite();
                    //GOAL.ExecuteGoalTestCases();
                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }

                Console.WriteLine(test.TestCaseTitle);
              
            }
            catch(Bedrock.ExceptionHandlingBlock.ShowStopperException ex)
            {
                throw ex;
                
            }
            catch (Exception e)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Can not proceed with D01: Please check D01 ExecuteTestCases(), ScenarioMain Class");
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb,ds,totalFinancialCalculation,cs,resultLog,test);
            }
        }
        public void SubmitAndApproveGoalRequest(string testCaseTitle, string scenarioId, string approvalType, DSAQuoteSummaryObject quoteSummaryObject, TestCase test, bool IsIncreasedQuantity = false, bool IsdecreaseQuantity = false)
        {
            try
            {
                DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                DSAPageObject dsaobject = new DSAPageObject(WebDriver);
                GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
                DSAQuoteSummaryPage summaryPage = new DSAQuoteSummaryPage(WebDriver);
                GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
                D01Flow goalFlow = new D01Flow(WebDriver);

                 goalFlow.SubmitGoalRequest();
                var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
                quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();
                dsaQuoteSummaryPage.NavigateWindow(1);
                goalPage.LoginToGOAL();
                if (Constant.URL_Prod.ToLower().Contains("g4") || Constant.URL_Prod.ToLower().Contains("g2"))
                {
                    var uiProfile = goalHomePage.NavigateToAdministration().GetUIProfile(test.Approver);
                    //goalHomePage.NavigateToManagePolicies().ExpandGoalApprovalMatrix();


                    if (test.ApprovalType.ToLower().Contains("auto"))
                    {
                        if (test.ApprovalType.ToLower().Contains("denial")) 
                         AutoDenyMatrix(quoteSummaryObject,StaticBriefCase.ApplicationType[0]);
                    }
                    goalHomePage.PerformAddNewApprovalMatrix(test.AffinityAccountGroupID, test.ProductType, uiProfile, quoteSummaryObject);
                    goalHomePage.ExpandGoalLiteAmerApprovalAssignments().PerformAddNewApprovalAssignments(test.AffinityAccountGroupID, test.ProductType, test.Approver);

                }
                string goalId = summaryPage.GetGoalId();

                goalHomePage.EditAndChangeAllTheMandateField(scenarioId, testCaseTitle,TestDataReader.MaxQuantity);
                if (approvalType == "Manual Approval")
                {
                    goalHomePage.ManualApproval(goalId);
                }
                if (approvalType == "Manual Deny")
                {
                    goalHomePage.DenyApproval(goalId);
                }
                if (approvalType == "Auto Deny")
                {
                    //goalHomePage.ManualApproval(goalId);
                }
                summaryPage.NavigateWindow(0);
                
                summaryPage.PickGoalIdFromList(goalId,quoteSummaryObject.DsaQuantity);
                summaryPage.IsGoalDealIdAttached(goalId);
                summaryPage.ValidateQuoteExpiryDate();

            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException ex)
            {
                ex.LogException();


            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }



        }
        public void SubmitGoalLite(TestCase test, DSAPageObject ds, string country, DSAQuoteSummaryPage dsaQuoteSummaryPage)
        {
            GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            string goalID = "";
            try
            {
                string approvalType = test.ApprovalType;
                // DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                if (country != "UNITED STATES")
                {
                    dsaQuoteSummaryPage.RemoveSFDCDealId();
                    WebDriverUtils.ScrollIntoView(WebDriver, ds.ByQuoteName);
                    ds.ClickOnQuoteName();
                }

                dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
                dsaQuoteSummaryPage.GetFloorOrDAMThresholdMessage();
                dsaQuoteSummaryPage.SaveQuote();
                dsaQuoteSummaryPage.ValidateContinueToCheckOut("false", "Disabled");
                dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
                var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
                quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();
                //IWebDriver goalChromDriver=new ChromeDriver();

                goalPage.LoginToGOAL();

                //if (!CustomConfigurationProvider.GetKey("Environment").ToLower().Contains("prod"))//needs to chanage
                //{
                if (Constant.URL_Prod.ToLower().Contains("g4") || Constant.URL_Prod.ToLower().Contains("g2"))
                {
                    var uiProfile = goalHomePage.NavigateToAdministration().GetUIProfile(test.Approver);
                    goalHomePage.NavigateToManagePolicies().ExpandGoalLiteAmerApprovalMatrix();
                  
                     
                    if (test.ApprovalType.ToLower().Contains("auto"))
                    {
                        if (test.ApprovalType.ToLower().Contains("approve")) AutoApprovalMatrix(quoteSummaryObject);
                        else AutoDenyMatrix(quoteSummaryObject);
                    }
                    
                        goalHomePage.PerformAddNewApprovalMatrix(test.AffinityAccountGroupID, test.ProductType, uiProfile, quoteSummaryObject);
                        goalHomePage.ExpandGoalLiteAmerApprovalAssignments().PerformAddNewApprovalAssignments(test.AffinityAccountGroupID, test.ProductType, test.Approver);
                    

                }
                //Goal login and goal matrix set-up before submitting goal lite

                WebDriver.Navigate().GoToUrl(dsaUrl);

                dsaQuoteSummaryPage.SubmitGoalLiteRequest("d02", test.TestCaseTitle);
                goalID = dsaQuoteSummaryPage.GetGoalLiteID();
                dsaQuoteSummaryPage.ValidateGoalLiteIDGenerated();

                goalPage.ReloadGoalApp().NavigateToManageDeals().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID);
                goalDetails = goalHomePage.GetDetailsInGoalPage();
                if (Constant.URL_Prod.ToLower().Contains("g4") || Constant.URL_Prod.ToLower().Contains("g2"))
                {
                    goalDetails.AffinityAccountGroupID = test.AffinityAccountGroupID;

                    goalDetails.Approver = test.Approver/*CustomConfigurationProvider.GetKey("UserName")*/;

                }
                goalDetails.ApprovalStatus = test.ApprovalType;
                goalDetails.ProductType = test.ProductType;
                goalHomePage.ValidateDSAQuoteDetails(quoteSummaryObject);
                goalHomePage.OpenWorkflowPolicy();
                Dictionary<string, string> financialsFromWorkflow = goalHomePage.GetWorkflowCondition();

                goalHomePage.GetApprover(goalDetails);
                ValidateFinancials(financialsFromWorkflow, goalDetails);

                goalPage.ReloadGoalApp().NavigateToManageDeals().NavigateToMyQuote().OpenGoalLite(goalID, true).NavigateToExpire().BusinessCaseJustification().CommitOk();
                if (Constant.URL_Prod.ToLower().Contains("g4") || Constant.URL_Prod.ToLower().Contains("g2"))
                {
                    //if (!CustomConfigurationProvider.GetKey("Environment").ToLower().Contains("prod"))
                    //{
                    PerformApproval(test.ApprovalType);
                    if (!test.ApprovalType.ToLower().Contains("auto"))
                        goalHomePage.ExpireManualApprovalPolicy(CustomConfigurationProvider.GetKey("UserName"));
                }

                WebDriver.Navigate().GoToUrl(dsaUrl);
                dsaQuoteSummaryPage.ValidateContinueToCheckOut("true", "Enabled");
            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // goalPage.ReloadGoalApp().NavigateToManageDeals().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID).NavigateToMoreAction().NavigateToExpire().BusinessCaseJustification();
            }

        }
        public void ValidateFinancials(Dictionary<string, string> financialsFromWorkflow, GoalObject goalDetails)
        {

            ValidateResultsAndLog(goalDetails.QuoteMarginInPercentage, financialsFromWorkflow["lowerMargin"], financialsFromWorkflow["upperMargin"], "Margin");
            ValidateResultsAndLog(goalDetails.ListPrice, financialsFromWorkflow["lowerRevenu"], financialsFromWorkflow["upperRevenu"], "Revenu");
            ValidateResultsAndLog(goalDetails.TotalDOLinPercentage, financialsFromWorkflow["lowerDol"], financialsFromWorkflow["upperDol"], "DOL");
        }

        public void PerformApproval(string approvalType)
        {
            switch (approvalType)
            {
                case "Manual Approval":
                    new GoalHomePage(WebDriver).ApproveGoalLite();
                    break;
                case "Manual Deny":
                    new GoalHomePage(WebDriver).DenyGoalLite();
                    break;
                default:
                    break;
            }
        }
        public void AutoApprovalMatrix(DSAQuoteSummaryObject quoteSummaryObject, string applicationType="goal")
        {
            try
            {
                GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
                if(applicationType=="goallite")
                goalPage.LoginToGOAL().NavigateToManagePolicies().ExpandGoalLiteAmerAutoApprovalMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);
                else
                    goalPage.LoginToGOAL().NavigateToManagePolicies().ExpandGoalLiteAmerAutoApprovalMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);
            }
            catch (ShowStopperException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not click Administartion", ex);
            }
           

        }
        public void AutoDenyMatrix(DSAQuoteSummaryObject quoteSummaryObject, string applicationType = "goal")
        {
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            GoalHomePage homePage = new GoalHomePage(WebDriver);
            if (applicationType == "goallite")
                goalPage.LoginToGOAL().NavigateToManagePolicies().ExpandGoalLiteAmerAutoDenyMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);
            else
                homePage.NavigateToManagePolicies().ExpandGoalAutoDenyMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);
            
        }
        public void ExpireGLApprovalMatrix()
        {
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().ExpireManualApprovalPolicy("RAHUL_GOWDA");
        }
        public void ValidateResultsAndLog(string financial, string minRange, string maxRange, string value)
        {
            decimal fin = Convert.ToDecimal(financial);
            decimal min_Range = Convert.ToDecimal(minRange);
            decimal max_Range = Convert.ToDecimal(maxRange);
            bool result = true;
            result &= (fin >= min_Range && fin <= max_Range);
            if (result) EMEA_SmartPrice_E2E_WebAutomation.Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format($@"{value} value lies between given range {min_Range} - {max_Range}"), true);
            else
                EMEA_SmartPrice_E2E_WebAutomation.Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(string.Format($@"{value} value fails to lie between given range {min_Range} - {max_Range}"), true);
        }
        public void LogintoDSAandSearchQuote(TestCase test, out string draftQuote,DSAPageObject ds)
        {
            draftQuote = "";
            try
            {
                OracleDBConnection odb = new OracleDBConnection();
                //DSAPageObject ds = new DSAPageObject(WebDriver);
                Constant cs = new Constant(WebDriver);
                DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
                DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
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
               
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber,TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond),true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }
            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException ex)
            {
                throw ex;

            }

            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unexpected error occured ,Please check LogintoDSAandSearchQuote(TestCase test, out string draftQuote,DSAPageObject ds) : " + ex.Message,true);
            }
            finally
            {

            }
        }


    }
}



    



        
    


