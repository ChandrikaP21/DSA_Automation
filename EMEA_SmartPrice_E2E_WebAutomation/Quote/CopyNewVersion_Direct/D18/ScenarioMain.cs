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
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D18
{
 public  class ScenarioMain:D17.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
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

       

        


        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
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
            string draftquote = "";
            if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
            {
                    draftquote = CopyAndCreateNewVersion();
            }
                if (test.IsProductSelectionAuto == "Y")
                {
                    productObject.OrderCode = odb.RetriveOrderCode(test.ProductIdentificationQuery);
                }
                else
                {
                    productObject.OrderCode = test.ProductIdentificationData;
                }

                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

            RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, null,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
            CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioId,testCaseNumber,quoteSummaryObject,goalDetails);
             resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D18", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null,retriveSmartPriceData);
            }
        }
    }
           
        }
        
            

      
