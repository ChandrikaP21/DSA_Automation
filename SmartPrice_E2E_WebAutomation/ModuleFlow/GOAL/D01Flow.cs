using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Pages.DSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL
{
    public class D01Flow : DriverHelper
    {
        private IWebDriver WebDriver;
        public D01Flow(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        public void ValidateD01GoalLite()
        {

            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
            //dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
            dsaQuoteSummaryPage.ApplyDiscountBelowFloorPriceForAllProducts();
            dsaQuoteSummaryPage.SaveQuote();
            dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
            dsaQuoteSummaryPage.SubmitGoalLiteRequest();
            dsaQuoteSummaryPage.ValidateGoalIDGenerated();
        }
    }
}
