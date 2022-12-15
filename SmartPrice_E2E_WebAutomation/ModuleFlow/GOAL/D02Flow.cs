using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Helper;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Pages.DSA;
using SmartPrice_E2E_WebAutomation.Pages.GOAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL
{
    public class D02Flow : DriverHelper
    {
        private IWebDriver WebDriver;
        public D02Flow(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        public void ValidateD02GoalLite(string approvalType)
        {
            //test1();
            //ExpireGLApprovalMatrix();
            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
            dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
            dsaQuoteSummaryPage.SaveQuote();
            dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
            var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
            DSAQuoteSummaryObject quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();

            //Goal login and goal matrix set-up before submitting goal lite
            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL();
            GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
            var uiProfile = goalHomePage.NavigateToAdministration().GetUIProfile("RAMA_JAYANTHI");
            goalHomePage.NavigateToManagePolicies().ExpandGoalLiteAmerApprovalMatrix().PerformAddNewApprovalMatrix("100042614", "ESG", uiProfile, quoteSummaryObject);
            goalHomePage.ExpandGoalLiteAmerApprovalAssignments().PerformAddNewApprovalAssignments("100042614", "ESG", "RAMA_JAYANTHI");

            WebDriver.Navigate().GoToUrl(dsaUrl);

            dsaQuoteSummaryPage.SubmitGoalLiteRequest();
            string goalID = dsaQuoteSummaryPage.GetGoalLiteID();
            dsaQuoteSummaryPage.ValidateGoalIDGenerated();
            goalPage.ReloadGoalApp().NavigateToManageDeals().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID)
            .GetDetailsInGoalPage();
            goalHomePage.ValidateDSAQuoteDetails(quoteSummaryObject);
            PerformApproval(approvalType);
            if (!approvalType.ToLower().Contains("auto")) goalHomePage.ExpireManualApprovalPolicy(CustomConfigurationProvider.GetKey("UserName"));
            WebDriver.Navigate().GoToUrl(dsaUrl);

            dsaQuoteSummaryPage.ValidateContinueToCheckOut();

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
        public void test1()
        {
            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
            dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
            dsaQuoteSummaryPage.SaveQuote();
            dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
            var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
            DSAQuoteSummaryObject quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();

            //AutoApprovalMatrix(quoteSummaryObject);
            AutoDenyMatrix(quoteSummaryObject);
            WebDriver.Navigate().GoToUrl(dsaUrl);


            dsaQuoteSummaryPage.SubmitGoalLiteRequest();
            string goalID = dsaQuoteSummaryPage.GetGoalLiteID();
            dsaQuoteSummaryPage.ValidateGoalIDGenerated();

            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().NavigateToManagePolicies().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID)
            .GetDetailsInGoalPage();
            GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
            goalHomePage.ValidateDSAQuoteDetails(quoteSummaryObject); ;
        }
        public void test()
        {
            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
            dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
            dsaQuoteSummaryPage.SaveQuote();
            dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
            var dsaUrl = dsaQuoteSummaryPage.GetQuoteUrl();
            DSAQuoteSummaryObject quoteSummaryObject = dsaQuoteSummaryPage.QuoteSummaryObject();
            dsaQuoteSummaryPage.SubmitGoalLiteRequest();
            string goalID = dsaQuoteSummaryPage.GetGoalLiteID();
            dsaQuoteSummaryPage.ValidateGoalIDGenerated();

            GoalLoginPage goalPage = new GoalLoginPage(WebDriver);
            goalPage.LoginToGOAL().NavigateToManagePolicies().SelectAllQuotesInGoalLiteInbox().ExpandAllQuotesInGoalLiteInbox().OpenGoalLite(goalID)
            .GetDetailsInGoalPage();
            GoalHomePage goalHomePage = new GoalHomePage(WebDriver);
            goalHomePage.ValidateDSAQuoteDetails(quoteSummaryObject); ;
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
    }
}
