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

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D36
{
    class ScenarioMain : D34.ScenarioMain
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
           
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
            WebDriver = new ChromeDriver();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04.ScenarioMain d04 = new CopyNewQuote_Direct.D04.ScenarioMain();
            try
            {
                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);


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
                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, scenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData, ds.ServiceModules, scenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, scenarioId);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,null, ds, totalFinancialCalculation, cs, resultLog, test);
            }





        }
        
    }
}
            
        
        
            

      
