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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D62
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


        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObj = new DSAPageObject(WebDriver);
            //     Constant cs = new Constant();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();

            Constant locator = new Constant(WebDriver);
            SmartPricePopUpClass SpPopUpObject = new SmartPricePopUpClass(WebDriver);
            CustomerPage customerPage = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D16.ScenarioMain d16 = new CopyNewQuote_Direct.D16.ScenarioMain();
            ResultLogging resultLog = new ResultLogging();
            try
            {
               
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
                odb.RetriveEndUserInformation(dsaObj.ActualQuote, customerObj1);
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
                    dsaObj.QuoteNumber = CopyAndCreateNewQuote();
                    string[] str = dsaObj.QuoteNumber.Split('.');
                    withOutVersionQuoteNo = str[0];

                }
                else
                {
                    string[] str = dsaObj.ActualQuote.Split('.');
                    withOutVersionQuoteNo = str[0];
                }
                odb.RetriveEndUserInformation(withOutVersionQuoteNo, customerObj2);
                
                customerPage.CompareCustomerDetails(customerObj1, customerObj2,customerObj);

                
                List<PNRAPILog> sp1 = new List<PNRAPILog>();
                List<PNRAPILog> sp2 = new List<PNRAPILog>();
                int index = 0;

                d16.VerifyStandardpartnerDiscount(resultLog, dsaObj, locator, SpPopUpObject, sp1, sp2, withOutVersionQuoteNo, odb, testCaseNumber, totalFinancialCalculation, scenarioId, test.StandardPartnerDiscountType, test.ProductIdentificationData,isIndirect,customerObj,retriveSmartPriceData);




            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObj, totalFinancialCalculation, locator, resultLog, test);
            }
        }
    }

        
    
}
        
            

      
