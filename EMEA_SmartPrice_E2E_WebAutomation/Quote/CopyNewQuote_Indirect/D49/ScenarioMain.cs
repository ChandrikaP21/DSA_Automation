using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D49
{
    public class ScenarioMain : CopyNewQuote_Direct.D01.ScenarioMain
    {
        public string TestURL;
        private object scenarioid;

        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
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


        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber,string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            CustomerClass customerObj = new CustomerClass();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            CustomerPage customerPage = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            PNRAPILog pnrApiLog = new PNRAPILog();
            string draftQuote = "";
            ds.country = country;
            try
            {
                
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
                
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();
                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
                if (customerObj != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(5), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }
                odb.RetriveEndUserInformation(draftQuote, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2,customerObj);
                odb.RetriveSFDCDetails(draftQuote, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);

                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds);
                
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation, null, null);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData ,null, scenarioId);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber,null,customerObj,true,"",null,null,false,pnrapilog1,pnrapilog2);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);

            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D49", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
    }
}



    



        
    


