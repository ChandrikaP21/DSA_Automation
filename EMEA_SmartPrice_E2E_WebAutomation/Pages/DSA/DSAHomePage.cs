using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA
{
    public class DSAHomePage : DriverHelper
    {
        private IWebDriver WebDriver { get; }
        public DSAHomePage(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        #region -- Elements --
        public IWebElement CatalogLargeEnterpriseAccountsSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usle1f"));
        public IWebElement CatalogPublicSelect => WebDriver.FindElement(By.Id("button_select_cacpu1f"));
        public IWebElement CatalogMediumBusinessSelect => WebDriver.FindElement(By.Id("button_select_usmb1f"));
        public IWebElement CatalogPreferredHigherEducationSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usphied1f"));
        public IWebElement CatalogOfflineUsGlobal500Select => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usglob1f"));
        public IWebElement CatalogHealthcareSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_ushea1f"));
        public IWebElement CatalogPreferredK12EducationSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uspk121f"));
        //public IWebElement CatalogEmergingBusinessSelect =>
        //    WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_useb1f"));
        public IWebElement CatalogEmergingBusinessSelect => WebDriver.FindElement(By.XPath("//*[@id='button_select_cacsb1f' or @id='customerSetSelect_Grid_select_useb1f']"));
        public IWebElement CatalogMajorHigherEducationSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_ushied1f"));
        public IWebElement CatalogMajorK12EducationSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usk121f"));
        public IWebElement CatalogMajorStateAndLocalGovtSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usslg1f"));
        public IWebElement CatalogPreferredStateAndLocalGovtSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uspslg1f"));
        public IWebElement CatalogOfflineChannelPartnersSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uschn1f"));
        public IWebElement CatalogConsumerSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usdhs1f"));
        public IWebElement CatalogMppSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usmpp1f"));
        public IWebElement DSACatalogsmallBusinessSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usbsd1f"));
        public IWebElement DSACatalogMediumBusinessSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usbsd1f"));
        public IWebElement DSACatalogsmallBusinessChatSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uschat1f"));

        //public IWebElement CatalogInsideCorpSelect
        //{
        //    get
        //    {
        //        WebDriver.WaitForElementDisplayed(By.XPath("//td[contains(text(),'Inside Corp')]/..//a"),
        //            TimeSpan.FromSeconds(10));

        //        return WebDriver.FindElement(By.XPath("//td[contains(text(),'Inside Corp')]/..//a"));
        //    }
        //}

        public IWebElement CatalogRetpSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usreta1f"));
        public IWebElement CatalogRetcSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usretb1f"));
        public IWebElement CatalogCentralCorpSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uscc1f"));
        public IWebElement CatalogEastCorpSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_upec1f"));
        public IWebElement CatalogAcquisitionCorpSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usac1f"));
        public IWebElement CatalogArbSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usdarb1f"));
        public IWebElement CatalogAcquisitionPubK12Select => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usapk1f"));
        public IWebElement CatalogAcquisitionPubOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usapo1f"));
        public IWebElement CatalogCentralPubK12Select => WebDriver.FindElement(By.Id("button_select_uscpk1f"));
        public IWebElement CatalogWestCorpSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uswc1f"));
        public IWebElement CatalogUnallocatedchannelSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usuc1f"));
        public IWebElement CatalogCentralPubOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uscpo1f"));
        public IWebElement CatalogEastPubK12Select => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usepk1f"));
        public IWebElement CatalogEastPubOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usepo1f"));
        public IWebElement CatalogInsidePubOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_isipo1f"));
        public IWebElement CatalogWestPubK12Select => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uswpk1f"));
        public IWebElement CatalogWestPubOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uswpo1f"));
        public IWebElement CatalogProductionSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usintr1f"));
        public IWebElement DSACatalogstagingSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uspilot1f"));
        public IWebElement CatalogIDSSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uslata1f"));
        public IWebElement CatalogConsumerARBSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usdarb1f"));
        public IWebElement CatalogCommercialARBSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uscarb1f"));
        public IWebElement CatalogLATAMSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_uslatb1f"));
        public IWebElement CatalogFederalSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usfeda1f"));
        public IWebElement CatalogFederalOtherSelect => WebDriver.FindElement(By.Id("customerSetSelect_Grid_select_usfedb1f"));
        public IWebElement MainMenu => WebDriver.FindElement(_menu);
        private By _menu = By.XPath("//main-menu//button//span[contains(.,'Menu')]/..");
        public IWebElement LnkAddSolution => WebDriver.FindElement(By.Id("menu_solutionAdd_link"));
        public IWebElement LinkSearchQuote => WebDriver.FindElement(By.Id("menu_quoteSearch"));
        public IWebElement QuoteNumberTextBox => WebDriver.FindElement(By.Id("quoteSearch_quoteNumber"));
        public IWebElement searchQuoteNumberButton => WebDriver.FindElement(By.Id("quoteSearch_searchButton"));
        public IWebElement SearchPerson => WebDriver.FindElement(By.Id("menu_personSearch"));
        public By SolutionGrid => By.Id("grid-solutions-list");
        //public IWebElement OscSolutionsGrid1 =>
        //    WebDriver.FindElement(SolutionGrid, TimeSpan.FromMinutes(TwoMinWaitTime));
        public IWebElement UpgradeSolutionBtn => WebDriver.FindElement(By.Id("quoteCreate_upgradeSolution"));
        public IWebElement DTCPAgreementIDRadioBtn => WebDriver.FindElement(By.Id("quoteCreate_dctpExpansion"));
        public IWebElement AgreementIDTextBox => WebDriver.FindElement(By.Id("quoteCreate_upgradeSolutionTag"));
        public IWebElement AddSolutionBtn => WebDriver.FindElement(By.Id("quoteCreate_quoteSolution"));
        public IWebElement ChangeCustomer => WebDriver.FindElement(By.XPath("//a[@id= 'quoteCreate_changeBillToCustomer']"));
        public IWebElement ConfirmChange => WebDriver.FindElement(By.XPath("//button[@id= '_btn_ok']"));
        public IWebElement DSACustomerNumTextBox => WebDriver.FindElement(By.Name("customernumber"));
        public IWebElement DSAFranceREL => WebDriver.FindElement(By.Id("button_select_frrel01f"));
        public IWebElement DSAGermanyREL => WebDriver.FindElement(By.Id("button_select_derel01f"));
        public IWebElement DSAUnitedKingdomCBG => WebDriver.FindElement(By.Id("button_select_ukcbg01f"));
        #endregion  -- Elements --


        #region -- Methods --

        public DSAHomePage ClickMainMenu()
        {
            WebDriver.waitForWaitAnimationToLoad();
            WebDriverUtils.WaitForElement(WebDriver, _menu);
            MainMenu.Click();
            WebDriverUtils.WaitForElementVisible(WebDriver, _menu, 10);
            WebDriver.WaitForElement(_menu, TimeSpan.FromSeconds(5));
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            //WebDriver.waitForWaitAnimationToLoad();
            return new DSAHomePage(WebDriver);
        }

        public DSAQuoteSummaryPage SearchQuote(string quoteNumber)
        {
            Console.WriteLine("Search by quote number is in progress!");
            LinkSearchQuote.Click();
            //WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            QuoteNumberTextBox.Set(quoteNumber);
            searchQuoteNumberButton.Click();
            //WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            Console.WriteLine("Quote Loaded sucessfully");
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage SearchQuoteWithQuery()
        {
            string quteonumber = "query returns value";
            SearchQuote(quteonumber);
            return new DSAQuoteSummaryPage(WebDriver);
        }












        #endregion -- Methods --
    }
}
