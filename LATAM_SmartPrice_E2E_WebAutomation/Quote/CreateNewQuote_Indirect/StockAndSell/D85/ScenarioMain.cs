using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Quote;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Reports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Chrome;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D85
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
        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber)
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
            try
            {
               
                
               
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject);
                Thread.Sleep(4000);

                DecreaseQuantity(retriveSmartPriceData,test,cs,totalFinancialCalculation,ScenarioId,ds,resultLog,testCaseNumber);
                Thread.Sleep(4000);

                resultLog.ExportToSqlServerDB(ds,test,ScenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               

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

        private void DecreaseQuantity(List<DSAPageObject>retriveSmartPriceData,TestCase testCase, Constant constant,TotalFinancialCalculation totalFinancialCalculation,string ScenarioId,DSAPageObject dsa,ResultLogging resultLog,int testCaseNumber)
        {
           
            Product productObj = new Product(WebDriver);
            
           
            string runId = RunId;

            //if (Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value")) - testCase.Quantity > 0)
            //  {

            //    DecreaseQuantityForSingleProduct(testCase.Quantity, constant.IncreaseQuantity(i), constant.QuantityLabel, QuantityChangeType.AnyProduct);
            //    RefreshGuidanceAndGenerateReport(totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId, "1", null);
            // }
            //else
            Thread.Sleep(4000);
            productObj.IncreaseQuantity(testCase,constant);
            Thread.Sleep(4000);
            RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId,"1", null,constant,testCase,true);
                 constant.jse.ExecuteScript("window.scrollBy(0,-200)");
                 productObj.DecreaseQuantityForSingleProduct(testCase.Quantity,QuantityChangeType.AnyProduct);
                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId,"2", null,constant,testCase);
            
               
        }
        
        
    }
}

                           
    




