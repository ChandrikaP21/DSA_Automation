using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
    

namespace SmartPrice_E2E_WebAutomation.Objects.Pages
{
   public class CustomerPage
    {
        public IWebDriver WebDriver;
        public CustomerPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        #region By
        public By ByCustomerName => By.XPath("//div[@class='customer-header']/h3[@id='currentCustomer_currentCustomer']");
        public By ByClickOnCustomerLink => By.XPath("//button[@id='menu_smartSearch' and text()=' Customer ']");
        public By BySearchCustomerID_DCN=>By.XPath("//input[@id='uniqueIdSearch_customerNumber'and @type='text']");
        public By ByFindCustomerButton = By.XPath("//button[@id='uniqueIdSearch_uniqueIdSearch' and @type='submit' and text()=' Find Customer ']");
        public By ByUseInQuote => By.XPath("//*[@id='customerDetails_useQuote']");
        public By ByChangeCustomerPopUpButton => By.XPath("//*[@id='_btn_ok']");
        public By ByRefreshSmartPriceButton => By.XPath("//button[@id='quoteCreate_updateSmartPrice']");
        #endregion
        #region WebElement
        public IWebElement ClickOnCustomerLink => WebDriver.FindElement(ByClickOnCustomerLink);
        public IWebElement SearchCustomerID_DCN=> WebDriver.FindElement(BySearchCustomerID_DCN);
        public IWebElement FindCustomerButton=> WebDriver.FindElement(ByFindCustomerButton);
        public IWebElement GetCustomerName => WebDriver.FindElement(ByCustomerName);
        public IWebElement UseInQuote => WebDriver.FindElement(ByUseInQuote);
        public IWebElement ChangeCustomerPopUpButton => WebDriver.FindElement(ByChangeCustomerPopUpButton);
        public IWebElement RefreshSmartPriceButton => WebDriver.FindElement(ByRefreshSmartPriceButton);
        #endregion
        public void ChangeCustomer(string NewEndUserAccountCustomerId)
        {

            try
            {
                Constant locator = new Constant(WebDriver);

                locator.MenuButton.Click();
                Thread.Sleep(3000);
                ClickOnCustomerLink.Click();
                Thread.Sleep(3000);
                SearchCustomerID_DCN.SendKeys(NewEndUserAccountCustomerId);
                FindCustomerButton.Click();
                Thread.Sleep(2000);
                UseInQuote.Click();
                ChangeCustomerPopUpButton.Click();
                //       string changedCustomerName= customerPageObj.GetCustomerName.Text;
                Thread.Sleep(5000);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public CustomerClass CompareCustomerDetails(CustomerClass object1,CustomerClass object2,CustomerClass customerObject)
        {
            try
            {
                if (object1.OldAccountCustomerId == object2.OldAccountCustomerId)
                {
                    object1.IsAccountCustomerIdMatched = "true";

                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldAccountCustomerId :" +object2.OldAccountCustomerId, true);
                    object1.IsAccountCustomerIdMatched = "false";
                }
                if (object1.OldAccountId == object2.OldAccountId)
                {
                    object1.IsAccountIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldAccountId :" + object2.OldAccountId, true);
                    object1.IsAccountIdMatched = "false";
                }
                if (object1.OldEndUserAccountCustomerId == object2.OldEndUserAccountCustomerId)
                {
                    object1.IsEndUserAccountCustomerIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldEndUserAccountCustomerId :" + object2.OldEndUserAccountCustomerId, true);
                    object1.IsEndUserAccountCustomerIdMatched = "false";
                }
                if (object1.OldEndUserAccountId == object2.OldEndUserAccountId)
                {
                    object1.IsEndUserAccountIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldEndUserAccountId :" + object2.OldEndUserAccountId, true);
                    object1.IsEndUserAccountIdMatched = "false";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                customerObject.OldAccountCustomerId = object1.OldAccountCustomerId;
                customerObject.NewAccountCustomerId = object2.OldAccountCustomerId;
                customerObject.IsAccountCustomerIdMatched = object1.IsAccountCustomerIdMatched;

                customerObject.OldAccountId = object1.OldAccountId;
                customerObject.NewAccountId = object2.OldAccountId;
                customerObject.IsAccountIdMatched = object1.IsAccountIdMatched;

                customerObject.OldEndUserAccountCustomerId = object1.OldEndUserAccountCustomerId;
                customerObject.NewEndUserAccountCustomerId = object2.OldEndUserAccountCustomerId;
                customerObject.IsEndUserAccountCustomerIdMatched = object1.IsEndUserAccountCustomerIdMatched;

                customerObject.OldEndUserAccountId = object1.OldAccountCustomerId;
                customerObject.NewEndUserAccountId = object2.OldEndUserAccountId;
                customerObject.IsEndUserAccountIdMatched= object1.IsEndUserAccountIdMatched;

            }
            return customerObject;
        }

    }
}
