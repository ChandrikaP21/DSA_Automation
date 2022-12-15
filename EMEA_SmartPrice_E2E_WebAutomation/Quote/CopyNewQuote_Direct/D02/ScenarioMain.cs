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
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Pages.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D02.ScenarioMain
{
    public class ScenarioMain : D01.ScenarioMain
    {
        public string TestURL;
       

        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;


        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain(string URL, IWebDriver driver, string scenarioId) : base(URL, driver,scenarioId)
        {
            TestURL = URL;
            WebDriver = driver;
            _scenarioId = scenarioId;

        }
        public ScenarioMain() : base()
        {

        }

        //Constant cs = new Constant(WebDriver);



        public new void ExecuteTestCases(TestCase test,string scenarioId, int testCaseNumber,string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
            {

                string draftQuote = "";
                LogintoDSAandSearchQuote(test, out draftQuote,ds);

                if (test.IsIdentifiedByQuery == "Y") 
                odb.RetriveOrderCode(test.BaseProductionIdentificationQuery);
                int count = 0;
                count = cs.NoOfProducts.Count;
                productObject.OrderCode = test.ProductIdentificationData;
                if (!string.IsNullOrEmpty(productObject.OrderCode) || !string.IsNullOrEmpty(test.SystemSearch) || !string.IsNullOrEmpty(test.SAndPSearch))
                {
                    productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch, country, LoadDeserializeObject());

                    if (count < cs.NoOfProducts.Count)
                    {
                        RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, null, ds);
                        SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                        CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);


                        if (application.Contains("GoalLite"))
                        {
                            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                            SubmitGoalLite(test,ds,country, dsaQuoteSummaryPage);
                          
                        }
                        if (application.Contains("Goal"))
                        {
                           
                            SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId,test.ApprovalType,quoteSummaryObject,test);
                        }


                        resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, "d02", testCaseNumber, quoteSummaryObject, goalDetails);
                        resultLog.ExportToSqlServerDB(ds, test, "d02", retriveSmartPriceData);
                        
                    }
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Product is not able to add ,exit--" + scenarioId, true);
                    }
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Product is empty, not able to add,exit--" + scenarioId, true);
                }
                
                

            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D02", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

   

     
    }

}




