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
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D33
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
        public void ExecuteTestCases(TestCase test, string ScenarioId,int testCaseNumber)
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
                dsaObject.CreateNewDSAQuote(customerClass.OldEndUserAccountCustomerId);
                

                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, dsaObject);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData, null,ScenarioId);
                resultLog.GenerateReportInCSV(dsaObject,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
                resultLog. ExportToSqlServerDB(dsaObject, test,ScenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test,ScenarioId);

                Console.WriteLine(test.TestCaseTitle);
                //WebDriver.Close();
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
                customeGc.NullifyUnusedObject(WebDriver, odb, dsaObject, totalFinancialCalculation,null, resultLog, test);
            }
        }
       

    }
}



    



        
    


