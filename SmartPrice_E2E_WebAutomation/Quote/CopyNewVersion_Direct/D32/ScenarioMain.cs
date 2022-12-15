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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D32
{
    public class ScenarioMain : D17.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }

        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        {
            TestURL = URL;
            WebDriver = driver;
        }
        /// <summary>
        /// Show Standard Partner discount for copy version of a quote
        /// </summary>
        /// <param name="test"></param>
        /// <param name="scenarioId"></param>
        /// <param name="testCaseNumber"></param>


        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObj = new DSAPageObject(WebDriver);
       //     Constant cs = new Constant();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            //CustomerClass customerClass = new CustomerClass();
            //CustomerPage customerPageObj = new CustomerPage();
            Constant locator = new Constant(WebDriver);
            SmartPricePopUpClass SpPopUpObject = new SmartPricePopUpClass(WebDriver);
            ResultLogging resultLog = new ResultLogging();

            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            try
            {

                
                string withOutVersionQuoteNo = "";


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

                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D16.ScenarioMain d16scenarioMain = new CopyNewQuote_Direct.D16.ScenarioMain();

                List<PNRAPILog> sp1 = new List<PNRAPILog>();
                List<PNRAPILog> sp2 = new List<PNRAPILog>();
                int index = 0;
                d16scenarioMain.VerifyStandardpartnerDiscount(resultLog, dsaObj, locator, SpPopUpObject, sp1, sp2, withOutVersionQuoteNo, odb, testCaseNumber, totalFinancialCalculation, scenarioId, test.StandardPartnerDiscountType, test.ProductIdentificationData,false,null, retriveSmartPriceData);


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
        
            

      
