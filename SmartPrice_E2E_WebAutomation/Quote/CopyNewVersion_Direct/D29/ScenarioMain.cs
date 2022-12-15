using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.Quote;

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
using System.Globalization;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D29
{
    public class ScenarioMain : D17.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }

        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        {
            TestURL = URL;
            WebDriver = driver;
        }



        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
            
                WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
           Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            try
            {

                //SFDCChangeClass sfdcDealIdCheck = new SFDCChangeClass();



                
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                ds.QuoteNumber= CopyAndCreateNewVersion();
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain d13Scenario = new SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain();
                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    d13Scenario.RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, scenarioId, null, null, null);
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,odb, ds, totalFinancialCalculation, cs, resultLog, test,null ,retriveSmartPriceData);
            }
        }
  
    }
    
}
        
            

      
