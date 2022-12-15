using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Pages.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL
{
    public class D01Flow : DriverHelper
    {
        private IWebDriver WebDriver;
        public D01Flow(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        public void ValidateD01GoalLite()
        {

            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
            DSAPageObject dsaobject = new DSAPageObject(WebDriver);
            //dsaQuoteSummaryPage.ApplySmartPriceLessthanFloorPrice();
            dsaQuoteSummaryPage.ApplyDiscountBelowFloorPriceForAllProducts();
            dsaQuoteSummaryPage.RemoveSFDCDealId();
            dsaobject.ClickOnQuoteName();
            dsaQuoteSummaryPage.SaveQuote();
            dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
            dsaQuoteSummaryPage.SubmitGoalLiteRequest();
            dsaQuoteSummaryPage.ValidateGoalLiteIDGenerated();
        }
        public void SubmitGoalRequest()
        {
            try
            {
                DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                DSAPageObject dsaobject = new DSAPageObject(WebDriver);
               
                
                dsaQuoteSummaryPage.SaveQuote();
                dsaQuoteSummaryPage.GetQuoteNumber(out string quoteNumber);
                dsaQuoteSummaryPage.SubmitGoalRequest();
                
            }
            catch(ShowStopperException ex)
            {
                
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                
            }


        }
   
    }
}
