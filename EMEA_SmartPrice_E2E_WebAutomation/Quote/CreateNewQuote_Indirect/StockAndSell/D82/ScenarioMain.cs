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
using System.IO;

using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D82
{
   public  class ScenarioMain: D78.ScenarioMain
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


        public void ExecuteTestcases(TestCase test,string ScenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();

            DSAPageObject ds = new DSAPageObject(WebDriver);
                     Constant cs = new Constant(WebDriver);
                     
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                     CustomerClass customerClass = new CustomerClass();
                ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
                    {
                        
                        

                Product productobject = new Product(WebDriver);
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject);
                Thread.Sleep(4000);
                // RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
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
                //Module ID with Service id is not applicable for now ..
                      /*  if (!string.IsNullOrEmpty(test.ModuleId)&& !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                        {
                            if (test.ProductIdentificationData != null)
                            {
                                UpGradeServices(cs, test.ModuleId,test.OfferingId, ds);

                            }
                            else
                            {

                                Console.WriteLine("Product is not available , Services can not be added.");


                            }
                        }*/



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
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


            }
                    catch (ShowStopperException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        //WebDriverUtils.TakeSnapShot(WebDriver);
                        throw new ShowStopperException("Unexpected error occured in D82", ex);
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
        
            

      
