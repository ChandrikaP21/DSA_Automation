using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;

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

using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D80
{
    class ScenarioMain : D78.ScenarioMain
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
        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
           
            CustomerClass customerClass = new CustomerClass();
            Product productobject = new Product(WebDriver);
            try
            {


                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds, test, customerClass, productobject);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04.ScenarioMain d04 = new CopyNewQuote_Direct.D04.ScenarioMain();
                Thread.Sleep(4000);
                if (string.IsNullOrEmpty(test.ModuleId))
                {
                    d04.AddAccessories(cs, test, ds,WebDriver);
                }
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.AccessoriesId))
                {
                    bool IsModuleId = true;
                    d04.AddAccessories(cs, test, ds, IsModuleId,WebDriver);
                }
                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.AccessoriesId))
                {
                    bool IsAccessorId = true;
                    bool IsModuleId = true;
                    d04.AddAccessories(cs, test, ds, IsModuleId, IsAccessorId,WebDriver);

                }
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData,ds.ServiceModules, scenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, scenarioId);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);

            }
            catch(Exception ex)
            {

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, cs, resultLog, test);
            }







        }
       
    }
}
            
        
        
            

      
