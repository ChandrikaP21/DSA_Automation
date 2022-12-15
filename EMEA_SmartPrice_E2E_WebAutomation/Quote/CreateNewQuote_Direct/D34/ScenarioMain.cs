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
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D34
{
  public class ScenarioMain:D33.ScenarioMain
    {
        public string TestURL;
        public IWebDriver webDriver;
        public ITakesScreenshot screenshot;
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
         


        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
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
                    IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased,customerClass.OldEndUserAccountCustomerId,test.CustomerIdentificationQuery,test.CustomerIdentificationData,country);
                    productObject.OrderCode=IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery,test.ProductIdentificationQuery,test.ProductIdentificationData);
                    productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,country, LoadDeserializeObject());

                    RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,scenarioId, dsa);
                    SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                    CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, dsa, dsa.country, dsaQuoteSummaryPage);

                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(dsa, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(dsa, test, scenarioId, retriveSmartPriceData);



                    }
                    catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                    {
                        throw showStopperEx;
                    }
                    catch (Exception ex)
                    {
                        Bedrock.Utilities.SimpleLogger.LogMessage(ex.Message);
                        Bedrock.Utilities.SimpleLogger.LogMessage(ex.StackTrace);
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

        public void IdentifyCustomerAndCreateQuote(string isCustomerIdentificationQueryBased, string oldEndUserAccountCustomerId, string customerIdentificationQuery, string customerIdentificationData,string country="")
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dSAPageObject = new DSAPageObject(WebDriver);
           dSAPageObject.ChangeCountry(country);
            if (isCustomerIdentificationQueryBased == "Y")
            {
              oldEndUserAccountCustomerId = odb.RetriveEnduserAccountCustomerIdWithoutQuoteNumber(customerIdentificationQuery);
            }
            else
            {
               oldEndUserAccountCustomerId = customerIdentificationData;
            }
            dSAPageObject.CreateNewDSAQuote(oldEndUserAccountCustomerId,country);
        }
    }
            
        }
        
            

      
