using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using OpenQA.Selenium;
using WindowsInput;

namespace EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA
{
    public class DSAQuoteSearchPage
    {
        public IWebDriver driver;
        #region Constructor
        public DSAQuoteSearchPage(IWebDriver webDriver) => this.driver = webDriver;
        #endregion




        #region By
        public By ByQuoteSearchPage => By.XPath("//label[@for='quoteSearch_quoteNumber']");
        public By MenuQuoteSearch => By.XPath("//*[@id='side-nav-one']/nav/ul/app-menu-item/li/button/span[contains(text(),'Quotes')]");
        public By BySearchQuote => By.XPath("//*[@id='menu_quoteSearch']/span");
        public By BySearchQuoteNumber => By.XPath("//input[@id='quoteSearch_quoteNumber']");
        public By ByDraftSearchQuoteNumber => By.XPath("//input[@id='quoteSearch_draftQuoteNumber']");
        public By ByQuoteSearchButton => By.XPath("//button[@id='quoteSearch_searchButton']");
        #endregion

        #region IWebElement
        public IWebElement QuoteSearchPage => driver.FindElement(ByQuoteSearchPage);
        public IWebElement clickOnQuote => driver.FindElement(MenuQuoteSearch);
        public IWebElement SearchQuote => driver.FindElement(BySearchQuote);
        public IWebElement SearchQuoteNumber => driver.FindElement(BySearchQuoteNumber);
        public IWebElement SearchDraftQuoteNumber => driver.FindElement(BySearchQuoteNumber);
        public IWebElement QuoteSearchButton => driver.FindElement(ByQuoteSearchButton);

        #endregion
        #region Methods
        public DSAQuoteSearchPage  Zoom(string Number)
        {
            driver.WaitForPageLoad(TimeSpan.FromSeconds(3));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.body.style.zoom='"+ Number + "%'");
            driver.WaitForPageLoad(TimeSpan.FromSeconds(3));
            return new DSAQuoteSearchPage(driver);
        }
        #endregion
    }
}
