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
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare;
using Guidance = EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare.Guidance;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D58
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
        /// 1.search and Open Actual Quote 
        /// 2. Retrive all customer information and sfdc details  from the latest pnrapilog calls and store it in object 1
        /// 3. copy as a new quote and retrive all the customer and sfdc related information store in object 2
        /// 4.Compare both object 1 and obect2 and generate calculate,compare and generate the CSV report .
        /// 5.if (IsCustomerInSameSegment == "Y")--->{If (Customer Identification==Query based)--->Retrive endCustomer Details from pnrapilog in Same segment .else--->get endcustomerId from the xml}.
        /// 6.if (IsCustomerInSameSegment == "N")---->{If (Customer Identification==Query based)--->Retrive endCustomer Details from pnrapilog in cross segment. else--->get endcustomerId from the xml}.
        /// 7.Change Customer.
        /// 8.In order to check whether customer is changed, compare latest  with draft quote information.
        /// 9. calculate and compare the financials and generate report.
        /// 10. Refreash smart price guidance manually then calculate and compare and generate report.
        /// </summary>
        /// <param name="test"></param>
        /// <param name="scenarioId"></param>
        /// <param name="testCaseNumber"></param>


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            
            CustomerPage customerPageObj = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            ds.country = country;
            try
            {
                bool isIndirect = true;

                
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                   // ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();

                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
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
                customerPageObj.CompareCustomerDetails(customerObj1, customerObj2, customerObj);
                odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);


                string[] str = draftquote.Split('.');
                string withOutVersionQuoteNo = str[0];
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10.ScenarioMain d10 = new CopyNewQuote_Direct.D10.ScenarioMain();
               d10.UpdateCustomer(retriveSmartPriceData,withOutVersionQuoteNo, totalFinancialCalculation,ds,resultLog,scenarioId,test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber,test.CustomerIdentificationQuery, test.CustomerIdentificationData,test.CrossSegmenId,customerObj,test,isIndirect,pnrapilog1,pnrapilog2);
                
                
               
               
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D58", e);
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
        
            

      
