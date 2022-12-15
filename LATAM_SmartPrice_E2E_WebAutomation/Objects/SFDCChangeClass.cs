using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SmartPrice_E2E_WebAutomation.Objects
{
  public class SFDCChangeClass
      
    {
        public IWebDriver WebDriver;
        public SFDCChangeClass(IWebDriver driver)
        {
            WebDriver = driver;
        }
        public string SFDCDealId { get; set; }
        public string OpportunityId { get; set; }
        public string  MyProperty { get; set; }
       public readonly string sfdcMessageText= "SmartPrice: Your SFDC opportunity is marked as Lost/Cancelled. You may not be receiving the best possible SmartPrice guidance and compensation. Please update your SFDC opportunity Stage before placing an order.";

        #region By
        public By BySfdcDealIdElement => By.XPath("//*[@id='quoteCreate_dealId']");
        public By BySfdcDealIdText => By.XPath("//*[text()=' SFDC Deal Id ']");
        public By BySoldToCustomer => By.Id("quoteCreate_soldToCustomer_header");
        public By BySfdcMessage => By.XPath("//*[@id='sfdcNotificationMessages_0']/p");
        #endregion
        #region WebElement
        public IWebElement SfdcDealIdElement => WebDriver.FindElement(BySfdcDealIdElement);
        public IWebElement SfdcDealIdText => WebDriver.FindElement(BySfdcDealIdText);
        public IWebElement SoldToCustomer => WebDriver.FindElement(BySoldToCustomer);
        public IWebElement SFDCMessage => WebDriver.FindElement(BySfdcMessage);
        #endregion

        

    }
}
