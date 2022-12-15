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

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D39
{
   public  class ScenarioMain:D38.ScenarioMain
    {
       // public string TestURL;
       //// public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
       // public ScenarioMain(string URL ):base(URL)
       // {
       //     TestURL = URL;

       // }

       // Constant cs = new Constant();

        public ScenarioMain(string URL) : base(URL)
        { }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
           
                        
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject();
                     Constant cs = new Constant(WebDriver);
                    ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    Product productObject = new Product(WebDriver);
                    CustomerClass customerClass = new CustomerClass();
                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();

            try
            {

                ResultLogging resultLog = new ResultLogging();
                ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                LoadDSA();
                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);
               
                string runId = RunId;
                //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeServices = new CopyNewQuote_Direct.D06.ScenarioMain();
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain d07UpgradeServices = new CopyNewQuote_Direct.D07.ScenarioMain();
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {

                    d06UpgradeServices.UpGradeServices(cs, test, ds);
                    string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceDetailsList,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);

                    d07UpgradeServices.DownGradeServices(cs, test, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceDetailsList,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "2", null, cs, test);
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    d07UpgradeServices.DownGradeServices(cs, test.ModuleId, test.OfferingId, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceDetailsList,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);

                }



                resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceDetailsList);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs,null, test);
            }
        }
        
    }
  }
        
            

      
