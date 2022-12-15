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
using System.IO;

using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D109
{
   public  class ScenarioMain: ReSell.D91.ScenarioMain
    {
      
        

        

        public ScenarioMain(string URL) : base(URL)
        { }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {

                WebDriver = new ChromeDriver();
                List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                     Constant cs = new Constant(WebDriver);
                 ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    
                    CustomerClass customerClass = new CustomerClass();
                    ResultLogging resultLog = new ResultLogging();
                    Product productobject = new Product(WebDriver);

            try
            {

                
                
                
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject,true);
                //   SmartPrice_E2E_WebAutomation.Quote.D06.ScenarioMain d06Scenario = new D06.ScenarioMain();
                string runId = RunId;
                //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeServices = new CopyNewQuote_Direct.D06.ScenarioMain();
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain d07UpgradeServices = new CopyNewQuote_Direct.D07.ScenarioMain();
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {

                    d06UpgradeServices.UpGradeServices(cs, test, ds);
                    string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", customerClass, cs, test,true);
                    d07UpgradeServices.DownGradeServices(cs, test, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "2", customerClass, cs, test,true);
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    d07UpgradeServices.DownGradeServices(cs, test.ModuleId, test.OfferingId, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", customerClass, cs, test,true);

                }



                resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, scenarioId);
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
        
            

      
