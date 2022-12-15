using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D33
{
    public class ScenarioMain : CopyNewQuote_Direct.D01.ScenarioMain
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
        public ScenarioMain() : base()
        {
            

        }
        public void ExecuteTestCases(TestCase test, string ScenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

           
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsaObject = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>(); 
            
            ResultLogging resultLog = new ResultLogging();
            try
            {
               
                LoadDSA();
                if(test.IsCustomerIdentificationQueryBased=="Y")
                {
                   customerClass.OldEndUserAccountCustomerId=odb.RetriveEnduserAccountCustomerIdWithoutQuoteNumber(test.CustomerIdentificationQuery);
                }
                else
                {
                    customerClass.OldEndUserAccountCustomerId = test.CustomerIdentificationData;
                }
                dsaObject.CreateNewDSAQuote(customerClass.OldEndUserAccountCustomerId,country);
                

                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, dsaObject);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData, null,ScenarioId);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, dsaObject, dsaObject.country, dsaQuoteSummaryPage);

                }
                
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(dsaObject,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(dsaObject, test, ScenarioId, retriveSmartPriceData);

                Console.WriteLine(test.TestCaseTitle);
                //WebDriver.Close();
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
               
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(e.Message, true);
                Bedrock.Utilities.SimpleLogger.LogMessage(e.Message);
              
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObject, totalFinancialCalculation,null, resultLog, test);
            }
        }
       

    }
}



    



        
    


