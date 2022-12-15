using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
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
        #region Methods
        public SFDCChangeClass ClickSFDCField()
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                WebDriver.ScrollIntoView(BySfdcDealIdElement);
                SfdcDealIdElement.Click();
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to click on SFDC field in DSA. Error:" +ex.Message);
            }
            return new SFDCChangeClass(WebDriver);
        }
        
            public SFDCChangeClass EnterSFDCField(string sfdcdealId)
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                SfdcDealIdElement.SendKeys(sfdcdealId);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(" Error: Unable to enter sfdc deal id, refer EnterSFDCField(), ", ex);
            }
            return new SFDCChangeClass(WebDriver);
        }
        public SFDCChangeClass ClearSFDCField()
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                SfdcDealIdElement.Clear();
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(" Error: Unable to enter sfdc deal id, refer ClearSFDCField(), ", ex);
            }
          
            return new SFDCChangeClass(WebDriver);
        }
        
        #endregion



    }
}
