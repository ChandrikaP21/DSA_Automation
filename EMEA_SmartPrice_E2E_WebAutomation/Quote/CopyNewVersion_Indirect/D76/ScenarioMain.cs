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
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D76
{
    public class ScenarioMain : CopyNewVersion_Direct.D17.ScenarioMain
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



        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObj = new DSAPageObject(WebDriver);
            //     Constant cs = new Constant();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            Constant locator = new Constant(WebDriver);
            SmartPricePopUpClass SpPopUpObject = new SmartPricePopUpClass(WebDriver);
            CustomerPage customerPage = new CustomerPage(WebDriver);
            dsaObj.country = country;

            try
            {
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D16.ScenarioMain d16 = new CopyNewQuote_Direct.D16.ScenarioMain();
               
                string withOutVersionQuoteNo = "";
                bool isIndirect = true;

          
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
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();
                odb.RetriveEndUserInformation(dsaObj.ActualQuote, customerObj1);
                odb.RetriveSFDCDetails(dsaObj.ActualQuote, pnrapilog1);
                if (customerObj1 != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                if (locator.IsElementPresent(locator.ByMoreAction))
                {
                    dsaObj.QuoteNumber = CopyAndCreateNewVersion();
                    string[] str = dsaObj.QuoteNumber.Split('.');
                    withOutVersionQuoteNo = str[0];

                }
                else
                {
                    string[] str = dsaObj.ActualQuote.Split('.');
                    withOutVersionQuoteNo = str[0];
                }
                odb.RetriveEndUserInformation(withOutVersionQuoteNo, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                odb.RetriveSFDCDetails(withOutVersionQuoteNo, pnrapilog2);
                dsaObj.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);



                List<PNRAPILog> sp1 = new List<PNRAPILog>() { pnrapilog1};
                List<PNRAPILog> sp2 = new List<PNRAPILog>() { pnrapilog2};
                int index = 0;

                d16.VerifyStandardpartnerDiscount(resultLog, dsaObj, locator, SpPopUpObject, sp1, sp2, withOutVersionQuoteNo, odb, testCaseNumber, totalFinancialCalculation, scenarioId, test.StandardPartnerDiscountType, test.ProductIdentificationData,isIndirect,customerObj,retriveSmartPriceData,test,true);

            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unexpected Error Occured :{0}{1}",e.Message,e.StackTrace));

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObj, totalFinancialCalculation, locator, resultLog, test);
            }
        }
    }

        
    
}
        
            

      
