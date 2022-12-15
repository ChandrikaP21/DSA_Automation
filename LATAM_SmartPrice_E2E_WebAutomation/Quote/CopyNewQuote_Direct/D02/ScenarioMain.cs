using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using SmartPrice_E2E_WebAutomation.Pages.DSA;
using SmartPrice_E2E_WebAutomation.Pages.GOAL;
using SmartPrice_E2E_WebAutomation.Helper;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D02.ScenarioMain
{
    public class ScenarioMain : D01.ScenarioMain
    {
        public string TestURL;
        private DSAQuoteSummaryObject quoteSummaryObject;
        private GoalObject goalDetails;

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
        public ScenarioMain() : base()
        {

        }

        //Constant cs = new Constant(WebDriver);



        public new void ExecuteTestCases(TestCase test, int testCaseNumber, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();

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

            }
                string draftquote = "";
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                     draftquote = CopyAndCreateNewQuote();
                }

                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, null, ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();

                if (application.Contains("Goal"))
                {
                    try
                    {
                        string approvalType = test.ApprovalType;
                        DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                        
                        
                        dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
                        dsaQuoteSummaryPage.SaveQuote();
                        dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
                        var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
                        quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();
                         //IWebDriver goalChromDriver=new ChromeDriver();
                        GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
                        goalPage.LoginToGOAL();
                        GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
                        if (!CustomConfigurationProvider.GetKey("Environment").ToLower().Contains("prod"))
                        {
                            var uiProfile = goalHomePage.NavigateToAdministration().GetUIProfile(test.Approver);
                            goalHomePage.NavigateToManagePolicies().ExpandGoalLiteAmerApprovalMatrix();
                            if (test.ApprovalType.ToLower().Contains("auto"))
                            {
                                if (test.ApprovalType.ToLower().Contains("approve")) AutoApprovalMatrix(quoteSummaryObject);
                                else AutoDenyMatrix(quoteSummaryObject);
                            }
                            goalHomePage.PerformAddNewApprovalMatrix(test.AffinityAccountGroupID, test.ProductType, uiProfile, quoteSummaryObject);
                            goalHomePage.ExpandGoalLiteAmerApprovalAssignments().PerformAddNewApprovalAssignments(test.AffinityAccountGroupID, test.ProductType, test.Approver);
                        }
                        //Goal login and goal matrix set-up before submitting goal lite

                        WebDriver.Navigate().GoToUrl(dsaUrl);

                        dsaQuoteSummaryPage.SubmitGoalLiteRequest();
                        string goalID = dsaQuoteSummaryPage.GetGoalLiteID();
                        dsaQuoteSummaryPage.ValidateGoalIDGenerated();
                        goalPage.ReloadGoalApp().NavigateToManageDeals().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID);
                        goalDetails = goalHomePage.GetDetailsInGoalPage();
                        goalDetails.AffinityAccountGroupID = test.AffinityAccountGroupID;
                        goalDetails.ApprovalStatus = test.ApprovalType;
                        goalDetails.Approver = CustomConfigurationProvider.GetKey("UserName");
                        goalDetails.ProductType = test.ProductType;

                        goalHomePage.ValidateDSAQuoteDetails(quoteSummaryObject);
                        goalHomePage.OpenWorkflowPolicy();
                        Dictionary<string, string> financialsFromWorkflow = goalHomePage.GetWorkflowCondition();
                        ValidateFinancials(financialsFromWorkflow,goalDetails);
                        if (!CustomConfigurationProvider.GetKey("Environment").ToLower().Contains("prod"))
                        {
                            PerformApproval(test.ApprovalType);
                            if (!test.ApprovalType.ToLower().Contains("auto")) 
                                goalHomePage.ExpireManualApprovalPolicy(CustomConfigurationProvider.GetKey("UserName"));
                        }
                        WebDriver.Navigate().GoToUrl(dsaUrl);
                        dsaQuoteSummaryPage.ValidateContinueToCheckOut();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    
                }
                if (application.Contains("Goal"))
                {
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, "d02", testCaseNumber, RunId, quoteSummaryObject, goalDetails);
                    resultLog.ExportToSqlServerDB(ds, test, "d02", retriveSmartPriceData);
                    resultLog.ExportToSummaryTable(test, "d02");
                }
                else
                {
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, "d02", testCaseNumber, RunId);
                    resultLog.ExportToSqlServerDB(ds, test, "d02", retriveSmartPriceData);
                    resultLog.ExportToSummaryTable(test, "d02");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

        private void ValidateFinancials(Dictionary<string, string> financialsFromWorkflow, GoalObject goalDetails)
        {
            ValidateResultsAndLog(goalDetails.QuoteMarginInPercentage, financialsFromWorkflow["lowerMargin"], financialsFromWorkflow["upperMargin"],  "Margin");
            ValidateResultsAndLog(goalDetails.ListPrice, financialsFromWorkflow["lowerRevenu"], financialsFromWorkflow["upperRevenu"],  "Revenu");
            ValidateResultsAndLog(goalDetails.TotalDOLinPercentage, financialsFromWorkflow["lowerDol"], financialsFromWorkflow["upperDol"],  "DOL");
        }

        private void PerformApproval(string approvalType)
        {
            switch (approvalType)
            {
                case "Manual Approval":
                    new GoalHomePage(WebDriver).ApproveGoalLite();
                    break;
                case "Manual Deny":
                    new GoalHomePage(WebDriver).DenyGoalLite();
                    break;
                default:
                    break;
            }
        }
        public void AutoApprovalMatrix(DSAQuoteSummaryObject quoteSummaryObject)
        {
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().NavigateToManagePolicies().ExpandGoalLiteAmerAutoApprovalMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);

        }
        public void AutoDenyMatrix(DSAQuoteSummaryObject quoteSummaryObject)
        {
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().NavigateToManagePolicies().ExpandGoalLiteAmerAutoDenyMatrix().PerformAddNewAutoApprovalMatrix("11-United States", "SB COMMERCIAL", "ESG", quoteSummaryObject);

        }
        public void ExpireGLApprovalMatrix()
        {
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().ExpireManualApprovalPolicy("RAHUL_GOWDA");
        }
        public void ValidateResultsAndLog(string financial, string minRange, string maxRange, string value)
        {
            decimal fin = Convert.ToDecimal(financial);
            decimal min_Range = Convert.ToDecimal(minRange);
            decimal max_Range = Convert.ToDecimal(maxRange);
            bool result = true;
            result &= (fin >= min_Range && fin <= max_Range);
            if (result) Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format($@"{value} value lies between given range {min_Range} - {max_Range}"), true);
            else
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(string.Format($@"{value} value fails to lie between given range {min_Range} - {max_Range}"), true);
        }
    }

}




