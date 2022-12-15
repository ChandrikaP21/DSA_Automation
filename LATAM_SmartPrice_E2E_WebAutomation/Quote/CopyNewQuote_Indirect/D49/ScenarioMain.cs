using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D49
{
    public class ScenarioMain : CopyNewQuote_Direct.D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
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


        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            CustomerClass customerObj = new CustomerClass();
            
            CustomerPage customerPage = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            try
            {
                
                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, cs);

                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds, cs);

                }
                
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                if (customerObj != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {   
                  string draftquote= CopyAndCreateNewQuote();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                  odb. RetriveEndUserInformation(draftquote, customerObj2);
                }
                customerPage.CompareCustomerDetails(customerObj1, customerObj2,customerObj);
                
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation, null, null);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData ,null, scenarioId);
                resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId,null,customerObj,true,"",null,null);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
    }
}



    



        
    


