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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D103
{
    public class ScenarioMain : StockAndSell.D78.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObj = new DSAPageObject(WebDriver);
       //     Constant cs = new Constant();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            //CustomerPage customerPageObj = new CustomerPage();
            Constant locator = new Constant(WebDriver);
            SmartPricePopUpClass SpPopUpObject = new SmartPricePopUpClass(WebDriver);
            Product productObject = new Product(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            dsaObj.country = country;

            try
            {

                
                string withOutVersionQuoteNo = "";


                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(dsaObj,test,customerClass,productObject,true,true);
                
                dsaObj.QuoteNumber = dsaObj.draftquotnumber.Text;
                string[] str = dsaObj.QuoteNumber.Split('.');
                withOutVersionQuoteNo = str[0];



                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D16.ScenarioMain d16scenarioMain = new CopyNewQuote_Direct.D16.ScenarioMain("",WebDriver);

                List<PNRAPILog> sp1 = new List<PNRAPILog>();
                List<PNRAPILog> sp2 = new List<PNRAPILog>();
                
                d16scenarioMain.VerifyStandardpartnerDiscount(resultLog, dsaObj, locator, SpPopUpObject, sp1, sp2, withOutVersionQuoteNo, odb, testCaseNumber, totalFinancialCalculation, scenarioId, test.StandardPartnerDiscountType, test.ProductIdentificationData,true,customerClass,retriveSmartPriceData,test);



            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D103", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObj, totalFinancialCalculation,null, resultLog, test);
            }
        }

    }
    
}
        
            

      
