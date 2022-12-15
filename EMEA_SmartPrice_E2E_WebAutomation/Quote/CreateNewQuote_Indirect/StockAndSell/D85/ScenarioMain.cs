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
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D85
{
    public class ScenarioMain : D78.ScenarioMain
    {
        public string TestURL;
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        /// <summary>
        /// Decrease the quantity by given input.If  quantity >1 then decrease else increase the quantity by 10 then decrease by given input.
        /// </summary>
        /// <param name="test"></param>
        /// <param name="ScenarioId"></param>
        /// <param name="testCaseNumber"></param>
        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productobject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
            {
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject);
                Thread.Sleep(4000);

                DecreaseQuantity(retriveSmartPriceData,test,cs,totalFinancialCalculation,ScenarioId,ds,resultLog,testCaseNumber);
                Thread.Sleep(4000);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D85", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

        private void DecreaseQuantity(List<DSAPageObject>retriveSmartPriceData,TestCase testCase, Constant constant,TotalFinancialCalculation totalFinancialCalculation,string ScenarioId,DSAPageObject dsa,ResultLogging resultLog,int testCaseNumber)
        {
           
            Product productObj = new Product(WebDriver);
            
           
           

            //if (Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value")) - testCase.Quantity > 0)
            //  {

            //    DecreaseQuantityForSingleProduct(testCase.Quantity, constant.IncreaseQuantity(i), constant.QuantityLabel, QuantityChangeType.AnyProduct);
            //    RefreshGuidanceAndGenerateReport(totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId, "1", null);
            // }
            //else
            Thread.Sleep(4000);
            productObj.IncreaseQuantity(testCase,constant);
            Thread.Sleep(4000);
            RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber,"1", null,constant,testCase,true);
                 constant.jse.ExecuteScript("window.scrollBy(0,-200)");
                 productObj.DecreaseQuantityForSingleProduct(testCase.Quantity,QuantityChangeType.AnyProduct);
                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber,"2", null,constant,testCase);
            
               
        }
        
        
    }
}

                           
    




