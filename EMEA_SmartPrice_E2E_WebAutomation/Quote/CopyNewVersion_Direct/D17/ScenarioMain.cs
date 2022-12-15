using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D17
{
    public class ScenarioMain :CopyNewQuote_Direct.D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public  ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain() : base()
        {


        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        /// <summary>
        /// Copy and Create new version of a quote.
        /// </summary>
        /// <returns></returns>
        public string CopyAndCreateNewVersion()
        {
            string draftQuoteNumber = "";
            try
            {
                //Constant constant = new Constant(WebDriver);
              
               
               
                ClickOnMoreActionMenu();
            
                SelectCopyAsNewVersion();
                draftQuoteNumber=SelectDraftQuoteNumber();
                
                
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("New version of a quote is  created . Quote {0}", draftQuoteNumber), true);
            }
            catch(ShowStopperException ex)
            {
                throw ex;
            }
            return draftQuoteNumber;
        }

        private string SelectDraftQuoteNumber()
        {
            string draftQuoteNumber = "";
            try
            {
                DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_title_draftquote']")));
                WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(10), true);
                draftQuoteNumber = draftQuotePageObject.DraftQuoteNumber;
                Console.WriteLine(draftQuoteNumber);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(string.Format("Unable to select as SelectDraftQuoteNumber:{0} ", ex));
            }
            
           
            return draftQuoteNumber;
        }

        private void SelectCopyAsNewVersion()
        {
            try
            {
                DSAPageObject quoteObject1 = new DSAPageObject(WebDriver);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(quoteObject1.BySelectCopyNewVewrsion));
                quoteObject1.SelectCopyNewVewrsion.Click();
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(string.Format("Unable to select as CopyAsNewVersion:{0} ", ex));
            }
            
        }

        private void ClickOnMoreActionMenu()
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                constant.MoreActionsDropdown.Click();
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(string.Format("Unable to Click on MoreAction:{0} ",ex));
            }
           
        }

        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();

            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Constant constant = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ds.country = country;

            try
            {
               
                
                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, constant);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds, constant);

                }

                if (!constant.IsElementPresent(constant.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, null, scenarioId);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);

                Console.WriteLine(test.TestCaseTitle);
             
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D17", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, constant, resultLog, test, null,retriveSmartPriceData);
            }
        }
    }
}
    
           
     
        
            

      
