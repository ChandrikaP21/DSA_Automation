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
using SeleniumExtras.WaitHelpers;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D91
{
  public class ScenarioMain: StockAndSell.D78.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain():base()
        {
            

        }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {


                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    DSAPageObject dsa = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerClass customerClass = new CustomerClass();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    Product productobj = new Product(WebDriver);

                    try
                     {
                         
                        
                        
                        LoadDSA();
                        dsa= CreateQuoteRetriveAndCompareCustomerDetails(dsa,test,customerClass,productobj,true);
                        

                        RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, scenarioId, testCaseNumber, RunId, "1", customerClass, cs, test, true);
                  
                        resultLog.ExportToSqlServerDB(dsa, test, scenarioId,retriveSmartPriceData);
                        resultLog.ExportToSummaryTable(test, scenarioId);
                        Console.WriteLine(test.TestCaseID);

                     }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        
                    }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, dsa, totalFinancialCalculation, cs, resultLog, test,productobj);
            }
        }

       
    }
            
        }
        
            

      
