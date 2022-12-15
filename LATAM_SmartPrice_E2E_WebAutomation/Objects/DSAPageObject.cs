using System;
using System.Collections.Generic;
using System.Text;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using OpenQA.Selenium.Support.UI;

using System.Threading;

namespace SmartPrice_E2E_WebAutomation.Objects
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
        //     public Accessories accessories=new Accessories();

        #region Method
        public void CreateNewDSAQuote(string customerId)
        {
            try
            {
                CustomerPage customerPageObject = new CustomerPage(Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                customerPageObject.ClickOnCustomerLink.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(customerPageObject.BySearchCustomerID_DCN));
                customerPageObject.SearchCustomerID_DCN.SendKeys(customerId);
                wait.Until(ExpectedConditions.ElementIsVisible(customerPageObject.ByFindCustomerButton));
                customerPageObject.FindCustomerButton.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(ByCreateQuote));
                CreateQuote.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(ByLocalChanndlId));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[text()='Create Quote']")));
                Thread.Sleep(2000);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion


        #region By
        public By BySelectCopyNewVewrsion => By.XPath("//button[@id='btnCopyAsVersion']");
        public By ByCreateQuote => By.XPath("//button[@id='customerDetails_createQuote']");
        public By ByLocalChanndlId => By.XPath("//span[@id='currentCustomer_context']");
        public By ByDraftQuoteNumber => By.XPath("//h1[@id='quoteCreate_title_draftquote']/small[1]");
        public By ByEndUserCustomerNumber => By.XPath("//*[@id='quoteCreate_endUserCustomerNumber']");
        #endregion
        #region WebElement
        public IWebElement SelectCopyNewVewrsion => WebDriver.FindElement(BySelectCopyNewVewrsion);
        public IWebElement CreateQuote => WebDriver.FindElement(ByCreateQuote);
        public IWebElement LocalChannelIdElement=> WebDriver.FindElement(ByLocalChanndlId);
        public IWebElement EndCustomerDetails => WebDriver.FindElement(ByEndUserCustomerNumber);
        public IWebElement draftquotnumber => WebDriver.FindElement(ByDraftQuoteNumber);
        //constant.DraftQuote.Length
       // dsobj.QuoteNumber = a.Substring(0, constant.DraftQuote.Length - 2);
        #endregion



    }
}