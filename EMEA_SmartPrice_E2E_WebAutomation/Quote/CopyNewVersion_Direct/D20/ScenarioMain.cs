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
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D20
{
    class ScenarioMain : D17.ScenarioMain
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
                OracleDBConnection odb = new OracleDBConnection();
                DSAPageObject ds = new DSAPageObject(WebDriver);
                Constant cs = new Constant(WebDriver);
                List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                ResultLogging resultLog = new ResultLogging();
            EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04.ScenarioMain d04upgradeComponent = new CopyNewQuote_Direct.D04.ScenarioMain();
            ds.country = country;
            try
                {
                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds, cs);
                    ds.OrderCode = test.ProductIdentificationData;
                    ds.PnrLog.ServiceType = "1";
                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                

                switch (test.UpdateAccessories)
                {
                    case "ADD":
                        if (string.IsNullOrEmpty(test.ModuleId))
                        {
                            d04upgradeComponent.AddAccessories(cs, test, ds,WebDriver);
                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.AccessoriesId))
                        {
                            bool IsModuleId = true;
                            d04upgradeComponent.AddAccessories(cs, test, ds, IsModuleId,WebDriver);
                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.AccessoriesId))
                        {
                            bool IsAccessorId = true;
                            bool IsModuleId = true;
                            d04upgradeComponent.AddAccessories(cs, test, ds, IsModuleId, IsAccessorId,WebDriver);

                        }
                        RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                        SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                        CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules, scenarioId);
                        if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                        {
                            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                            SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

                        }
                        if (StaticBriefCase.ApplicationType.Contains("Goal"))
                        {
                            SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                        }
                        resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber,quoteSummaryObject,goalDetails);
                        resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceData);
                        
                        break;

                }
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D20", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null ,retriveSmartPriceData);
                
            }




        }
        
    }
}
            
        
        
            

      
