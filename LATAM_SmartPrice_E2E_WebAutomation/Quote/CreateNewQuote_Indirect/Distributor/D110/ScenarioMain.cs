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
using SeleniumExtras.WaitHelpers;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Chrome;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D110
{
    public class ScenarioMain : StockAndSell.D78.ScenarioMain
    {
        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        /// <summary>
        /// Increase quantity based on the user input on addition to the existing quantity.
        ///e.g quantiy=1,given Quantity to increase=5, increaseQuantity=1+5=6;
        /// </summary>
        /// <param name="test"></param>
        /// <param name="scenarioid"></param>
        /// <param name="testCaseNumber"></param>
        public void ExecuteTestcases(TestCase test,string scenarioid,int testCaseNumber)
        {

             WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            try
            {
                
               // ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                LoadDSA();
                
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productObject, true,true);


                productObject.IncreaseQuantity(test, cs);
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,scenarioid,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioid,testCaseNumber,RunId,"",customerClass,true,"",true);
                resultLog.ExportToSqlServerDB(ds,test,scenarioid,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test,scenarioid);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

 
       

      }                              
    
}



