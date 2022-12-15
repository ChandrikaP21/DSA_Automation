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
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;

using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D106
{
    class ScenarioMain : ReSell.D91.ScenarioMain
    {
        public string TestURL;
     
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

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
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04.ScenarioMain d04 = new CopyNewQuote_Direct.D04.ScenarioMain();
            CustomerClass customerClass = new CustomerClass();
            Product productobject = new Product(WebDriver);
            ds.country = country;
            try
            {

                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds, test, customerClass, productobject, true,true);

                Thread.Sleep(3000);
                if (string.IsNullOrEmpty(test.ModuleId))
                {
                    d04.AddAccessories(cs, test, ds,WebDriver);
                }
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.AccessoriesId))
                {
                    bool IsModuleId = true;
                    d04.AddAccessories(cs, test, ds, IsModuleId,WebDriver);
                }
                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.AccessoriesId))
                {
                    bool IsAccessorId = true;
                    bool IsModuleId = true;
                    d04.AddAccessories(cs, test, ds, IsModuleId, IsAccessorId,WebDriver);

                }
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData, ds.ServiceModules, scenarioId);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "1", customerClass, true, "", true,false,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);

            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D106", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }

            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, cs, resultLog, test);
            }




        }
       
      
    }
}
            
        
        
            

      
