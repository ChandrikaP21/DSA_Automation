using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;

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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D22

{
   public  class ScenarioMain:Quote.CopyNewVersion_Direct.D17.ScenarioMain
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



        public void ExecuteTestcases(TestCase test,string ScenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();

            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
                    {
                       
                        LoadDSA();

                        if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                        {
                            odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                            SearchQuote(ds,cs);
                        }
                        else
                        {
                            ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                           ds.OrderCode = test.ProductIdentificationData;
                           SearchQuote(ds,cs);

                            ds.PnrLog.ServiceType = "1";
                        }

                        if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                        {
                            CopyAndCreateNewVersion();
                        }
                         RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                    EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeService = new EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain();
                

                        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                        {
                            if (test.ProductIdentificationData != null)
                            {
                            d06UpgradeService.UpGradeServices(cs, test, ds);

                            }
                            else
                            {
                                Console.WriteLine("Product is not available , Services  can not be added.");

                            }
                        }
                
                        if (!string.IsNullOrEmpty(test.ModuleId)&& !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                        {
                            if (test.ProductIdentificationData != null)
                            {
                        d06UpgradeService.UpGradeServices(cs, test.ModuleId,test.OfferingId, ds);

                            }
                            else
                            {

                                Console.WriteLine("Product is not available , Services can not be added.");


                            }
                        }



                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, ScenarioId, ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceData);


                //WebDriver.Close();
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D22", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null, retriveSmartPriceData);

            }

        }
      
    }
  }
        
            

      
