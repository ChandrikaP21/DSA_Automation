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
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D23
{
   public  class ScenarioMain:D17.ScenarioMain
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

        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        { }


        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {

                WebDriver = new ChromeDriver();
                OracleDBConnection odb = new OracleDBConnection();
                DSAPageObject ds = new DSAPageObject(WebDriver);
                    Constant cs = new Constant(WebDriver);
                ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
        List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
        ResultLogging resultLog = new ResultLogging();
                try
                {
                        
                        
                        
                    LoadDSA();

                    if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                    {
                        odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                        SearchQuote(ds,cs);
                    }
                    else
                    {
                        ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                        ds.OrderCode = test.ProductIdentificationData;
                        SearchQuote(ds,cs);

                    }

            if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
            {
                CopyAndCreateNewVersion();
            }
            RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
            string runId = RunId;
            SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain d07UpgradeService = new SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain();
            SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeService = new SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain();
            if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                    {
                        if (test.ProductIdentificationData != null)
                        {
                    d06UpgradeService.UpGradeServices(cs, test, ds);
                    string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);
                    d07UpgradeService.DownGradeServices(cs, test, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "2", null, cs, test);

                }
                        else
                        {
                            Console.WriteLine("Product is not available , Accessories  can not be added.");

                        }
                    }
                
                    if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                    {
                        if (test.ProductIdentificationData != null)
                        {
                    d07UpgradeService.DownGradeServices(cs, test.ModuleId,test.OfferingId, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);
                }
                        else
                        {

                            Console.WriteLine("Product is not available , Accessories  can not be added.");


                        }
                    }


            RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId,ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                    resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioId,testCaseNumber,RunId);
                    resultLog.ExportToSqlServerDB(ds,test,scenarioId,retriveSmartPriceData);
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
            customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null,retriveSmartPriceData);

        }
        }
       
    }
  }
        
            

      
