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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D09
{
    public class ScenarioMain : D01.ScenarioMain
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
        public ScenarioMain() : base()
        {
            

        }
    
        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            //foreach (Scenario testCase in TestDataReader._scenarios[2].TestCases)
            //{
            //    foreach (TestCase test in testCase.TestCases)
            //    {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            try
            {
                
                
                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds,cs);

                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewQuote();
                }
                Product productObject = new Product(WebDriver);
                productObject.DecreaseQuantityforMultipleScenario(test, cs);

                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,null,ds);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioId, retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null ,retriveSmartPriceData);
            }
        }

        
    }
}

                           
    




