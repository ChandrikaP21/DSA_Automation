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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D59_D60
{
    public class ScenarioMain : D49.ScenarioMain
    {
        public string TestURL;
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
        /// <summary>
        /// Steps To Reproduce
        /// 1.Search for a quote and  retrive customer related information.
        /// 2. copy as new quote and retrive all customer and sfdc details from pnrapilog.
        /// 3. 
        /// </summary>
        /// <param name="test"></param>
        /// <param name="scenarioId"></param>
        /// <param name="testCaseNumber"></param>

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
      //      CustomerClass customerClass = new CustomerClass();
            CustomerPage customerPage = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            ds.country = country;
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
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj = new CustomerClass();
                CustomerClass customerObj1 = new CustomerClass();
               
                CustomerClass customerObj2 = new CustomerClass();

                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                //odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
                if (customerObj1 != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                string draftquote = "";
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(5), true);
                    draftquote = draftQuotePageObject.DraftQuoteNumber;
                }

                odb.RetriveEndUserInformation(draftquote, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                string[] str = draftquote.Split('.');
                string withOutVersionQuoteNo = str[0];
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11_12 = new CopyNewQuote_Direct.D11_D12.ScenarioMain();
                d11_12.ChangeSFDCId(retriveSmartPriceData,withOutVersionQuoteNo, customerObj, test.IsSFDCIdentificationDataAutomatic,ds, test.SFDCIdentificationQuery,test.SFDCIdentificationData, totalFinancialCalculation,scenarioId,testCaseNumber,test,false);
                

               
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D59_D60", e);
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
        
            

      
