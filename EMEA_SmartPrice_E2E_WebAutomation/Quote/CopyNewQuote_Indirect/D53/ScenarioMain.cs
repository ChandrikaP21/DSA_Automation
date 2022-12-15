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
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D53
{
    public class ScenarioMain :D49.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
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



        public void ExecuteTestcases(TestCase test, string ScenarioID,int testCaseNumber, string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ConfigurationPage configpage = new ConfigurationPage(WebDriver);
            CustomerPage customerPage = new CustomerPage(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            ds.country = country;
            try
            {

              
                //   ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
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

                }
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();

                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
                if (customerObj1 != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                string draftquote = "";
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(5), true);
                    draftquote = draftQuotePageObject.DraftQuoteNumber;
                }
                odb.RetriveEndUserInformation(draftquote, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);
                odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);

                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D05.ScenarioMain d05 = new CopyNewQuote_Direct.D05.ScenarioMain();
                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                bool IsOfferingId = true;

                switch (test.UpdateServices)
                {

                    case "ADD":
                        if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))
                        {
                           d05.AddService(cs,ds);

                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))
                        {
                            d05.AddService(cs, test, ds);

                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))
                        {
                            d05.AddService(cs, test, ds, IsOfferingId);
                        }
                       
                        break;
                      
                }


                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioID, ds, ds.ServiceHeaderText, ds.ServiceModules);
                // SmartPriceCalculation();
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData,ds.ServiceModules);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioID, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioID,testCaseNumber,"",customerObj,null,null,true,false,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioID, retriveSmartPriceData);

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D53", e);
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




