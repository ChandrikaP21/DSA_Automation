using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using SeleniumExtras.WaitHelpers;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D91
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

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {


                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    DSAPageObject dsa = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerClass customerClass = new CustomerClass();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    Product productobj = new Product(WebDriver);
                   dsa.country = country;
                      try
                     {
                         
                        
                        
                        LoadDSA();
                        dsa= CreateQuoteRetriveAndCompareCustomerDetails(dsa,test,customerClass,productobj,true);
                        

                        RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, scenarioId, testCaseNumber, "1", customerClass, cs, test, true);

                          resultLog.ExportToSqlServerDB(dsa, test, scenarioId, retriveSmartPriceData);
                       }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D91", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, dsa, totalFinancialCalculation, cs, resultLog, test,productobj);
            }
        }

       
    }
            
        }
        
            

      
