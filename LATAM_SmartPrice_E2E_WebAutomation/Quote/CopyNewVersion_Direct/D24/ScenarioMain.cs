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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D24
{
    public class ScenarioMain : D17.ScenarioMain
    {
        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public void ExecuteTestcases(TestCase test,string scenarioid,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            try
            {
                
               // ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
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
                    CopyAndCreateNewVersion();
                }
                Product productObject = new Product(WebDriver);
                productObject.IncreaseQuantityforMultipleScenario(test,cs);
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioid,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioid,testCaseNumber,RunId);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null, retriveSmartPriceData);
            }
        }

     
      }                              
    
}



