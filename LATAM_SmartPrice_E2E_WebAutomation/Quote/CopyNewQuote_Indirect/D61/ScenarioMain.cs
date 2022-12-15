using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.Quote;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.Globalization;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D61
{
    public class ScenarioMain :D49.ScenarioMain
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


        public void ExecuteTestcases(TestCase test,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
           Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            
            CustomerPage customerPageObj = new CustomerPage(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();


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
                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
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
                    ds.QuoteNumber = CopyAndCreateNewQuote();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                    odb.RetriveEndUserInformation(ds.QuoteNumber, customerObj2);
                }
                customerPageObj.CompareCustomerDetails(customerObj1, customerObj2,customerObj);
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain d13 = new CopyNewQuote_Direct.D13.ScenarioMain();
                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    d13.RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, "D61", null,null,null,true,customerObj);
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
     
    }
    
}
        
            

      
