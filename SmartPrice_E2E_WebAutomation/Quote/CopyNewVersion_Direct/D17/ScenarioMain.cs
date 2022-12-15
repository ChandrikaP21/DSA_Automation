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
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D17
{
    public class ScenarioMain :CopyNewQuote_Direct.D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public  ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain() : base()
        {


        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        /// <summary>
        /// Copy and Create new version of a quote.
        /// </summary>
        /// <returns></returns>
        public string CopyAndCreateNewVersion()
        {
            Constant constant = new Constant(WebDriver);
            DSAPageObject quoteObject1 = new DSAPageObject(WebDriver);
            constant.MoreActionsDropdown.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCopyQuote));
            quoteObject1.SelectCopyNewVewrsion.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='quoteCreate_title_draftquote']")));// xpath change earlier h2 now h1
            string draftQuoteNumber = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_title_draftquote']")).FindElement(By.XPath("//small[1]")).Text;
            Console.WriteLine(draftQuoteNumber);
            //      Console.WriteLine(".......Quote is copied........");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("New version of a quote is  created . Quote {0}", draftQuoteNumber), true);

            return draftQuoteNumber;
        }

        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber)
        {
            WebDriver = new ChromeDriver();

            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Constant constant = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            try
            {
               
                
                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, constant);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds, constant);

                }

                if (!constant.IsElementPresent(constant.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, null, scenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, scenarioId);

                Console.WriteLine(test.TestCaseTitle);
             
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(e.Message, true);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, constant, resultLog, test, null,retriveSmartPriceData);
            }
        }
    }
}
    
           
     
        
            

      
