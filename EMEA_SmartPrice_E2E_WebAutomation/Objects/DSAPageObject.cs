using System;
using System.Collections.Generic;
using System.Text;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using OpenQA.Selenium.Support.UI;

using System.Threading;
using OpenQA.Selenium.Interactions;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
   
   public class DSAPageObject:Product
    {
        public IWebDriver Driver;
        public DSAPageObject(IWebDriver driver):base(driver)
        {
            Driver = driver;
        }
        public DSAPageObject()
        {
           
        }
        
        public string URL = string.Empty;
        public  string SummarySellingPrice = string.Empty;
        public  string SummaryTotalMargin = string.Empty;
        public   string SummaryPricingModifier = string.Empty;
        public  string SummaryTotalListPrice = string.Empty;
        public string SummaryModifiedRevenue = string.Empty;
        public string Quantity = string.Empty;
      //  public string SKUNumber = string.Empty;
        public decimal HardwareSupportServices = 0;
        public string QuoteNumber = string.Empty;
        public string QuoteVersionNumber = string.Empty;
        //public string OrderCode = string.Empty;
        public string LocalChannelId = string.Empty;
        public string ProductLob = string.Empty;

        public string TiedId = string.Empty;
        public string ProductId = string.Empty;

        public string Title = string.Empty;
        public string UnitListPrice = string.Empty;
        public string UnitCost = string.Empty;
        public string TotalListPrice = string.Empty;
        //LineItem...
        public string LineItemUnitDiscount = string.Empty;
        public string LineItemUnitSellingPrice = string.Empty;
        public  string LineItemSmartRevenue = string.Empty;
        public string LineItemUnitMargin = string.Empty;
        public  string TotalDiscount = string.Empty;
        public  string TotalSellingPrice = string.Empty;
        public  string TotalLineItemMargin = string.Empty;
        public string TotalMargin = string.Empty;
        public string DiscountInputField = string.Empty;
       
        /// LineItem Summary 
        public  string LineItemSummeryTotalSellingPrice = string.Empty;
        public  string LineItemSummerySmartPriceRevenue = string.Empty;//actual revenue
        public  string LineItemSummeryPricingModifier = string.Empty;
        public string WithDFSIncentive = string.Empty;
        public string LineItemSummaryModifiedRevenue = string.Empty;
        public string LineItemSummaryUpsellServicesModifiedRevenue = string.Empty;
        public string LineItemSummaryWithServicesIncentive = string.Empty;
        public decimal UpsellServiceSellingPrice = 0;

        //Store R, C,F...
        public  string RecommenededSellingPrice = string.Empty;
        public  string RecommenededDiscount = string.Empty;
        public  string RecommendedSmartPricRevenue = string.Empty;
        public  string RecommendedPricingModifier = string.Empty;
        //CompAnchor elements..
        public  string CompAnchorSellingPrice = string.Empty;
        public  string CompAnchorDiscount = string.Empty;
        public  string CompAnchorSmartPricRevenue = string.Empty;
       public  string CompAnchorPricingModifier = string.Empty;
        //Floor elements..
        public  string FloorSellingPrice = string.Empty;
        public  string FloorDiscount = string.Empty;
        public  string FloorSmartPricRevenue = string.Empty;
        public  string FloorPricingModifier = string.Empty;
        public PNRAPILog PnrLog = new PNRAPILog();
        public PNRAPICalulationCompare comparisionObjects = new PNRAPICalulationCompare();
        public SmartPriceFinancials CalculatedActualSPCompFinancials = new SmartPriceFinancials();
        public SmartPriceFinancials CalculatedRecommendedSPCompFinancials = new SmartPriceFinancials();
        public SmartPriceFinancials CalculatedCompAnchorSPCompFinancials = new SmartPriceFinancials();
        public SmartPriceFinancials CalculatedFloorSPCompFinancials = new SmartPriceFinancials();
        public SmartPriceFinancials CalculatedModifiedServices = new SmartPriceFinancials();
        public string StatusComments = string.Empty;
        public static TimeSpan runDate = TimeSpan.Zero;
        public string FilePath = string.Empty;
        public string TestCasesStatus = string.Empty;
        public  string ActualQuote = string.Empty;
        //public Constant constant = new Constant();
        public List<DSAPageObject> Accessories = new List<DSAPageObject>();
       // public List<Service> services = new List<Service>();
       
        public TotalFinancialCalculation totalfinancials = new TotalFinancialCalculation();
        public string ServiceModifier = string.Empty;
        public string ServiceName=string.Empty;
        public string ConfigServiceListPrice = string.Empty;
        public string ServiceHeaderText = string.Empty;
        public string ServiceTitle = string.Empty;

        public string CreatedDate = string.Empty;
        public string ValidOppportunity { get; set; }

        public string ValidOppportunitySuiteDecision { get; set; }
       public string OpportunityStatus { get; set; }
        public string InValidOppportunity { get; set; }
        public string DealSize { get; set; }
        public string QuoteSize { get; set; }
        public string IsSfdcMessageDisplayed = "No";
        public string SfdcMessageDisplayed = string.Empty;

        public string cstTime = "";
        public bool IsdiscountModified = false;
        public string StandardpartnerDiscountText = "";

        public string country = "";
        public string OrderCodeSKUXpath = "";
        
        //     public Accessories accessories=new Accessories();

        #region Method
            public DSAPageObject ClickOnSelectCountryMenu(string countryName,ref bool isCountryPresent)
            {
            try
            {
                Constant constant = new Constant(WebDriver);
                if (constant.IsElementPresent(ByClickOnBUId))
                    if (!constant.IsElementPresent(ByCountrySelected(countryName)))
                    {
                        //ClickOnBUId.Click();
                        WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(3));
                        ClickOnBUId.Click();

                        WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(4));
                        isCountryPresent = true;

                    }
                    else
                    {
                        isCountryPresent = false;
                    }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Error-Unableto clik on country Menu", ex);
            }
            return new DSAPageObject(WebDriver);
            }
        public DSAPageObject SelectCountryBasedOnInput(string countryName)
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(3));
                SelectCountry(countryName).Click();
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to click on the Country"+ ex,true);
            }
            
            return new DSAPageObject(WebDriver);
        }
        public void ChangeCountry(string country)
            {
            try
            {
                bool IsrequiredCountryPreselected = false;

                ClickOnSelectCountryMenu(country, ref IsrequiredCountryPreselected);
                if(IsrequiredCountryPreselected)
                    {
                    SelectCountryBasedOnInput(country);
                    ButtonOK.Click();
                }
                
                Thread.Sleep(6000);
          //      new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(ByCountrySelected(country)));
                    
            }
            
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to Change country", true);
                Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperException = new Bedrock.ExceptionHandlingBlock.ShowStopperException("Unable to Change country", ex);
                throw showStopperException;
                
            }



            }
        
        public void CreateNewDSAQuote(string customerId,string Country= "UNITED STATES")
        {
            try
            {
               
                CustomerPage customerPageObject = new CustomerPage(Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));

                customerPageObject.ClickOnCustomerTextInnav();
                Thread.Sleep(4000);
                customerPageObject.SelectCustomer();
          //      wait.Until(ExpectedConditions.ElementIsVisible(customerPageObject.BySearchCustomerID_DCN_NonUSCountry));
                if (Country != "UNITED STATES")
                {
                    
                    customerPageObject.EnterCustomerID_DCNForNonUSCountry(customerId);
                    Thread.Sleep(2000);
                    customerPageObject.FindCustomerID_DCNForNonUSCountry();
                }
                else
                {
                    customerPageObject.SearchCustomerID_DCNForUS(customerId);
                    customerPageObject.FindCustomerButtonforUS();
                }

                DSACreateQuote(wait);
                wait.Until(ExpectedConditions.ElementIsVisible(ByLocalChanndlId));

                Thread.Sleep(5000);
            }
            catch(ShowStopperException ex)
            {
               
                ex.LogException();
                throw ex;
            }
        }

        private DSAPageObject DSACreateQuote(WebDriverWait wait)
        {
            bool isClicked = false;
            for (int i = 0; i < 3; i++)
            {
                try
                {

                    wait.Until(ExpectedConditions.ElementIsVisible(ByCreateQuote));
                    Thread.Sleep(5000);
                    CreateQuote.Click();
                    isClicked = true;
                   
                }
                catch (Exception ex)
                {
                    isClicked = false;
                }
                if (!isClicked)
                {
                    break;
                }
            }

            if (isClicked)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to click on Create New Quote", true);
            }

            return new DSAPageObject(WebDriver);
        }

        public DSAPageObject ClickOnQuoteName()
        {
            try
            {
                DSAQuoteSummaryPage dsSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                QuoteName.Click();
                WebDriver.WaitingForSpinner(dsSummaryPage.spinner);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));

            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage($"Unable to click on QuoteName {ex}");
            }
            return new DSAPageObject(WebDriver);
        }
         public DSAPageObject EnterEndCustomerId(string endCustomerId)
        {
            try
            {
                EndCustomerDetails.SendKeys(endCustomerId);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                ShowStopperException exception = new ShowStopperException("Unable to enter EndCustomerNumber,Please Check ", ex);
                
                throw exception;
            }
           
            return new DSAPageObject(WebDriver);
        }
        public DSAPageObject ClickQuoteName()
        {
            try
            {
                QuoteName.Click();
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                ShowStopperException exception = new ShowStopperException("Unable to click on quote field,Please Check ", ex);

                throw exception;
            }

            return new DSAPageObject(WebDriver);
        }
        #endregion


        #region By
        public By BySelectCopyNewVewrsion => By.XPath("//button[@id='btnCopyAsVersion']");
        public By ByCreateQuote => By.XPath("//button[@id='customerDetails_createQuote']");
        public By ByLocalChanndlId => By.XPath("//span[@id='currentCustomer_context']");
        public By ByDraftQuoteNumber => By.XPath("(//*[@id='quoteCreate_title_draftquote'])[2]");
        public By ByEndUserCustomerNumber => By.XPath("//*[@id='quoteCreate_endUserCustomerNumber']");
        public By ByQuoteName => By.XPath("//input[@name='quotename']");
        public By ByClickOnBUId => By.XPath("//button[@id='buContext']");
        private By ByCountrySelected(string Country) => By.XPath("//*[@id='buContext' and contains(text(),'"+Country+"')]");
        private By BySelectCountry(string countryName) => By.XPath("//button[contains(.,'"+ countryName + "')]");
        private By ByButtonOk => By.XPath("//button[@id='_btn_ok']");
        #endregion
        #region WebElement
        public IWebElement SelectCopyNewVewrsion => WebDriver.FindElement(BySelectCopyNewVewrsion);
        public IWebElement CreateQuote => Driver.FindElement(ByCreateQuote);
        public IWebElement LocalChannelIdElement=> WebDriver.FindElement(ByLocalChanndlId);
        public IWebElement EndCustomerDetails => WebDriver.FindElement(ByEndUserCustomerNumber);
        public IWebElement draftquotnumber => WebDriver.FindElement(ByDraftQuoteNumber);
        public IWebElement QuoteName => WebDriver.FindElement(ByQuoteName);
        private IWebElement ClickOnBUId => WebDriver.FindElement(ByClickOnBUId);
       public IWebElement CountrySelected(string country) => WebDriver.FindElement(ByCountrySelected(country));
        private IWebElement SelectCountry(string countryName) => WebDriver.FindElement(BySelectCountry(countryName));
        private IWebElement ButtonOK => WebDriver.FindElement(ByButtonOk);
        //constant.DraftQuote.Length
        // dsobj.QuoteNumber = a.Substring(0, constant.DraftQuote.Length - 2);
        #endregion



    }
}