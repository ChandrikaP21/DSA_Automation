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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D75
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


        public void ExecuteTestcases(TestCase test,int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
          Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            CustomerPage customerPageObj = new CustomerPage(WebDriver);
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
                    ds.OrderCode = test.ProductIdentificationData;
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
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    ds.QuoteNumber = CopyAndCreateNewVersion();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                    
                }
                
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                odb.RetriveEndUserInformation(withOutVersionQuoteNo, customerObj2);
                customerPageObj.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                odb.RetriveSFDCDetails(withOutVersionQuoteNo, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain d13 = new CopyNewQuote_Direct.D13.ScenarioMain();
                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    d13.RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, "D75",null, null, null,isIndirect,customerObj,pnrapilog1,pnrapilog2);
                    
                }

            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unexpected Error Occured :{0}{1}", e.Message, e.StackTrace));

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation,cs, resultLog, test);
            }
        }
     
    }
    
}
        
            

      
