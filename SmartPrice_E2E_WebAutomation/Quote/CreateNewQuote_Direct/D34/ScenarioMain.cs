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
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D34
{
  public class ScenarioMain:D33.ScenarioMain
    {
        public string TestURL;
        public IWebDriver webDriver;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain():base()
        {
            

        }

       
        
        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {

                    WebDriver = new ChromeDriver();
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject dsa = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerClass customerClass = new CustomerClass();
                    Product productObject = new Product(WebDriver);
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>(); 
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                      try
                     {
                        
                  
                    LoadDSA();
                    IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased,customerClass.OldEndUserAccountCustomerId,test.CustomerIdentificationQuery,test.CustomerIdentificationData);
                    productObject.OrderCode=IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery,test.ProductIdentificationQuery,test.ProductIdentificationData);
                    productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

                    RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, null, dsa);
                    SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                    CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                    resultLog.GenerateReportInCSV(dsa, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId);
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
                        customeGc.NullifyUnusedObject(WebDriver, odb, dsa, totalFinancialCalculation, cs, resultLog, test);
                    }
        }

        public string IdentifyOrderCodeUsingQuery(string isIdentifiedByQuery,string productIdentificationQuery,string productIdentificationData)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsa = new DSAPageObject(WebDriver);
            string orderCode = "";
            string[] text = dsa.LocalChannelIdElement.Text.Split('-');
            dsa.LocalChannelId = text[0].Trim();
            if (isIdentifiedByQuery == "Y")
            {
                orderCode = odb.RetriveOrderCode(productIdentificationQuery, dsa.LocalChannelId);
            }
            else
            {
                orderCode = productIdentificationData;

            }
            return orderCode;
        }

        public void IdentifyCustomerAndCreateQuote(string isCustomerIdentificationQueryBased, string oldEndUserAccountCustomerId, string customerIdentificationQuery, string customerIdentificationData)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dSAPageObject = new DSAPageObject(WebDriver);
            if (isCustomerIdentificationQueryBased == "Y")
            {
              oldEndUserAccountCustomerId = odb.RetriveEnduserAccountCustomerIdWithoutQuoteNumber(customerIdentificationQuery);
            }
            else
            {
               oldEndUserAccountCustomerId = customerIdentificationData;
            }
            dSAPageObject.CreateNewDSAQuote(oldEndUserAccountCustomerId);
        }
    }
            
        }
        
            

      
