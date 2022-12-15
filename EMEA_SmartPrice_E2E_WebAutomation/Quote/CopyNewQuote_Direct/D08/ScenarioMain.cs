using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Chrome;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D08
{
    public class ScenarioMain : D01.ScenarioMain
    {
        private object summaryObject;

      

        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
           
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        { }
        public void ExecuteTestcases(TestCase test,string scenarioid,int testCaseNumber, string country = null, List<string> application=null)
        {
            WebDriver = new ChromeDriver();
            
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            ds.country = country;
            try
            {

                string draftQuote = "";
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
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }
                productObject.IncreaseQuantityforMultipleScenario(test, cs);

                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, scenarioid, ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);

                if (application.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, country, dsaQuoteSummaryPage);


                }
                if (application.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioid, test.ApprovalType, quoteSummaryObject, test);
                }

                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioid, testCaseNumber, quoteSummaryObject, goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, scenarioid, retriveSmartPriceData);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);

                //WebDriver.Close();

            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D08", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null, retriveSmartPriceData);
            }
        }

        

      }                              
    
}



