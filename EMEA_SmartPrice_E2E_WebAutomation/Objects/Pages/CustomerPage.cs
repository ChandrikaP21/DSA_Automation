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
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages
{
   public class CustomerPage
    {
        public IWebDriver WebDriver;
        public CustomerPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        public  string countryName_us = "UNITED STATES";
        
        #region By
        public By ByCustomerName => By.XPath("//div[@class='customer-header']/h3[@id='currentCustomer_currentCustomer']");
        public By ByClickOnCustomerLinkold => By.XPath("//global-side-nav//button//span[text()='Customers']");
        public By ByClickOnCustomerLink => By.XPath("//sidenav-mfe-integration//button//span[text()='Customers']");
        
        public By BySearchCustomer => By.XPath("//a[@id='menu_smartSearch']/span[contains(text(),'Search Customers')]");
        public By BySearchCustomerID_DCN=>By.XPath("//input[@id='uniqueIdSearch_customerNumber'and @type='text']");//uniqueIdentifierSearch_customerId
        public By BySearchCustomerID_DCN_NonUSCountry => By.XPath("//input[@id='uniqueIdentifierSearch_customerId'and @type='text']");
        public By BySearchCustomerButton_NonUSCountry => By.XPath("//button[@id='customerSearch_searchAction']");
        public By ByFindCustomerButton = By.XPath("//button[@id='uniqueIdSearch_uniqueIdSearch' and @type='submit' and text()=' Find Customer ']");
        public By ByUseInQuote => By.XPath("//*[@id='customerDetails_useQuote']");
        public By ByChangeCustomerPopUpButton => By.XPath("//*[@id='_btn_ok']");
        public By ByRefreshSmartPriceButton => By.XPath("//button[@id='quoteCreate_updateSmartPrice']");
        private By ByChangeSoldToCustomer=>By.XPath("//*[@id='quoteCreate_changeSoldToCustomer']");
        private By ByChangeCustomerPopUp => By.XPath("//button[@id='_btn_ok' and contains(text(),'Change Customer')]");
        private By ByChangeCustomerInputBox => By.XPath("//input[@id='quoteCreate_customerNumber']");
        private By ByUseInDraftQuote => By.XPath("");
        #endregion
        #region WebElement
        public IWebElement ClickOnCustomerLink => WebDriver.FindElement(ByClickOnCustomerLink);
        public IWebElement SearchCustomerMenuItems => WebDriver.FindElement(BySearchCustomer);
        public IWebElement SearchCustomerID_DCN=> WebDriver.FindElement(BySearchCustomerID_DCN);
        public IWebElement SearchCustomerID_DCN_NonUsCountry => WebDriver.FindElement(BySearchCustomerID_DCN_NonUSCountry);
        public IWebElement FindCustomerButton=> WebDriver.FindElement(ByFindCustomerButton);
        public IWebElement FindCustomer_NonUsCountry => WebDriver.FindElement(BySearchCustomerButton_NonUSCountry);
        public IWebElement GetCustomerName => WebDriver.FindElement(ByCustomerName);
        public IWebElement UseInQuote => WebDriver.FindElement(ByUseInQuote);
        public IWebElement ChangeCustomerPopUpButton => WebDriver.FindElement(ByChangeCustomerPopUpButton);
        public IWebElement RefreshSmartPriceButton => WebDriver.FindElement(ByRefreshSmartPriceButton);
        private IWebElement ChangeSoldToCustomerElement => WebDriver.FindElement(ByChangeSoldToCustomer);
        private IWebElement ChangeCustomerAcceptPopUp => WebDriver.FindElement(ByChangeCustomerPopUp);
        private IWebElement ChangeCustomerInputBox => WebDriver.FindElement(ByChangeCustomerInputBox);
        private IWebElement UseInDraftQuote => WebDriver.FindElement(ByUseInDraftQuote);
        private By ByAddReSellException => By.XPath("//reseller-exception");
        private IWebElement AddReSellException => WebDriver.FindElement(ByAddReSellException);
        private By ByReasonCode => By.XPath("//reseller-exception");
        private IWebElement ReasonCode => WebDriver.FindElement(ByReasonCode);
        private By ByCustomerAreaSelection => By.XPath("//*[@id='customers-tab-section']");
        private IWebElement CustomerAreaSelection => WebDriver.FindElement(ByCustomerAreaSelection);
    
        private By BySelectResellExcption => By.XPath("//reseller-exception//select");
        private IWebElement SelectResellExcption => WebDriver.FindElement(BySelectResellExcption);

        #endregion
        public CustomerPage ClickOnCustomerTextInnav()
        {
            try
            {
                ClickOnCustomerLink.Click();
               
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable click on the customerLink",ex);
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage SelectCustomer()
        {
            try
            {
               new WebDriverWait(WebDriver,TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySearchCustomer));
               SearchCustomerMenuItems.Click();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable  to select 'customer' under 'Customers Nav'");
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage EnterCustomerID_DCNForNonUSCountry(string customerid)
        {
            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySearchCustomerID_DCN_NonUSCountry));
                SearchCustomerID_DCN_NonUsCountry.SendKeys(customerid);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable  to enter  'customerid' in 'Customers searchBox'");
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage FindCustomerID_DCNForNonUSCountry()
        {
            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySearchCustomerButton_NonUSCountry));
                FindCustomer_NonUsCountry.Click();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable  to click  'customerid' in 'Customers searchBox'",ex);
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage SearchCustomerID_DCNForUS(string customerId)
        {
            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySearchCustomerID_DCN));
                SearchCustomerID_DCN.SendKeys(customerId);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable  to enter  'customerid' in 'Customers searchBox'", ex);
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage FindCustomerButtonforUS()
        {
            try
            {
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(ByFindCustomerButton));
                FindCustomerButton.Click();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable  to enter  'customerid' in 'Customers searchBox'", ex);
            }
            return new CustomerPage(WebDriver);
        }

        
        

        public CustomerPage ChangeSoldToCustomer()
        {
            try
            {
                DSAQuoteSummaryPage dsSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                ChangeSoldToCustomerElement.Click();
                WebDriver.WaitingForSpinner(dsSummaryPage.spinner);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to ChangeSoldToCustomer", ex);
            }
            return new CustomerPage(WebDriver);
        }
        /// <summary>
        /// code is not working
        /// </summary>
        /// <returns></returns>
        public CustomerPage SelectReasonCode()
        {
            try
            {
                Constant cs = new Constant(WebDriver);
                if (cs.IsElementPresent(ByAddReSellException))
                {
                    SelectResellExcption.Click();
                    SelectElement select = new SelectElement(SelectResellExcption);
                    select.SelectByIndex(4);
                    Thread.Sleep(4000);
                }
               
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage(ex.Message);
            }
            return new CustomerPage(WebDriver);
        }
        public CustomerPage ProvideResSellerException()
        {
            try
            {
                Constant cs = new Constant(WebDriver);
                if (cs.IsElementPresent(ByAddReSellException))
                {
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(ByAddReSellException));
                    AddReSellException.Click();
           //         new WebDriverWait(WebDriver, TimeSpan.FromSeconds(4)).Until(ExpectedConditions.ElementIsVisible(ByCustomerAreaSelection));
                    SelectReasonCode();
                }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to click ReSeller is not Applicable" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to click ReSeller is not Applicable" + ex.StackTrace);
            }

            return new CustomerPage(WebDriver);
        }
        public CustomerPage AcceptChangeCustomerPopUp()
        {
            try
            {


                DSAQuoteSummaryPage dsSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                ChangeCustomerAcceptPopUp.Click();

                WebDriver.WaitingForSpinner(dsSummaryPage.spinner);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to AcceptChangeCustomerPopUp", ex);
            }
            return new CustomerPage(WebDriver);
        }
          public CustomerPage CustomerSearchInQuoteLevel(string customerId)
        {
            try
            {
                DSAQuoteSummaryPage dsSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                DSAPageObject dsobj = new DSAPageObject(WebDriver);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                ChangeCustomerInputBox.SendKeys(customerId);
                dsobj.ClickOnQuoteName();
                WebDriver.WaitingForSpinner(dsSummaryPage.spinner);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(4));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Error in  CustomerSearchInQuoteLevel()", ex);
            }
            return new CustomerPage(WebDriver);
        }
        public void ChangeCustomer(string NewEndUserAccountCustomerId,string country= "UNITED STATES",bool isIndirect=true)
        {

            try
            {
                Constant locator = new Constant(WebDriver);

                if (country != "UNITED STATES")
                {
                    ChangeSoldToCustomer().AcceptChangeCustomerPopUp().CustomerSearchInQuoteLevel(NewEndUserAccountCustomerId);
                }
                else
                {
                    ClickOnCustomerLink.Click();
                    Thread.Sleep(3000);
                    SearchCustomerMenuItems.Click();
                    Thread.Sleep(3000);



                    if (country == "UNITED STATES")
                    {
                        SearchCustomerID_DCN.SendKeys(NewEndUserAccountCustomerId);
                    }
                    else
                        SearchCustomerID_DCN_NonUsCountry.SendKeys(NewEndUserAccountCustomerId);
                    if (country == "UNITED STATES")
                    {
                        FindCustomerButton.Click();
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(ByUseInQuote));
                        UseInQuote.Click();
                    }
                    else FindCustomer_NonUsCountry.Click();

                    ChangeCustomerPopUpButton.Click();
                    //       string changedCustomerName= customerPageObj.GetCustomerName.Text;
                    Thread.Sleep(5000);
                }
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Customer number is changed", true);

            }
            catch (ShowStopperException ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw ex;

            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to change the customer,Please refer ChangeCustomer() in CustomerPage Class"+ex.Message);

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
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldAccountCustomerId :" +object1.OldAccountCustomerId, true);
                    object1.IsAccountCustomerIdMatched = "false";
                }
                if (object1.OldAccountId == object2.OldAccountId)
                {
                    object1.IsAccountIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldAccountId :" + object1.OldAccountId, true);
                    object1.IsAccountIdMatched = "false";
                }
                if (object1.OldEndUserAccountCustomerId == object2.OldEndUserAccountCustomerId)
                {
                    object1.IsEndUserAccountCustomerIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldEndUserAccountCustomerId :" + object1.OldEndUserAccountCustomerId, true);
                    object1.IsEndUserAccountCustomerIdMatched = "false";
                }
                if (object1.OldEndUserAccountId == object2.OldEndUserAccountId)
                {
                    object1.IsEndUserAccountIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("OldEndUserAccountId :" + object1.OldEndUserAccountId, true);
                    object1.IsEndUserAccountIdMatched = "false";
                }
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(string.Format("Unable to compare: ",true), true);
                Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unable to compare: {0}{1}",ex.Message,ex.StackTrace));
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
