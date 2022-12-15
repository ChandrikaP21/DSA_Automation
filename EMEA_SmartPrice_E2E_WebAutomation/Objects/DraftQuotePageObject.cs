using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using System.Collections;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using System.Threading;
using System.Linq;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
  public class DraftQuotePageObject
        
    {
        public IWebDriver WebDriver;
        public DraftQuotePageObject(IWebDriver driver)
        {
            WebDriver = driver;
        }
        //LogInPage login;
        
        //DraftQuoteNumber

        #region By
        public By BydraftquoteNumber => By.XPath("(//*[@id='quoteCreate_title_draftquote'])[2]");
        public By ExpirationdateInputDateField => By.XPath("//input[@placeholder='Expiration Date']");
        public By ByCurrentDatePicker => By.XPath("//div[@class='common-datepicker-header']/span[@class='common-datepicker-label']");
        public By ByFindCalenderGrid => By.XPath("//mat-calendar[@id='mat-datepicker-0']");
        public By ByGetDate => By.XPath("//table[@class='mat-calendar-table']/tbody/tr/td/button[@aria-pressed='true']");
        public By ByRecommenededSellingPrice => By.XPath("//smart-price-guidance//table/tbody/tr/td[text()='Recommended']/following-sibling::td[1]");
        public By ByRecommenededDiscount => By.XPath("//smart-price-guidance//table/tbody/tr/td[text()='Recommended']/following-sibling::td[2]");
        public By ByRecommendedSmartPricRevenue => By.XPath("//smart-price-guidance//table/tbody/tr/td[text()='Recommended']/following-sibling::td[3]");
        public By ByRecommendedPricingModifier => By.XPath("//smart-price-guidance//table/tbody/tr/td[text()='Recommended']/following-sibling::td[4]");

        public By ByCompAnchorSellingPrice => By.XPath("//smart-price-guidance//table/tbody/tr[2]/td[2]");
        public By ByCompAnchorDiscount => By.XPath("//smart-price-guidance//table/tbody/tr[2]/td[3]");
        public By ByCompAnchorSmartPricRevenue => By.XPath("//smart-price-guidance//table/tbody/tr[2]/td[4]");
        public By ByCompAnchorPricingModifier => By.XPath("//smart-price-guidance//table/tbody/tr[2]/td[5]");

        public By ByFloorSellingPrice => By.XPath("//smart-price-guidance//table/tbody/tr[3]/td[2]");
        public By ByFloorDiscount => By.XPath("//smart-price-guidance//table/tbody/tr[3]/td[3]");
        public By ByFloorSmartPricRevenue => By.XPath("//smart-price-guidance//table/tbody/tr[3]/td[4]");
        public By ByFloorPricingModifier => By.XPath("//smart-price-guidance//table/tbody/tr[3]/td[5]");
        public By ByExpandAll(string ExpandAndCollapse)=>By.XPath("(//quote-create-line-item-configuration/quote-line-item-configuration/div/div[1]/div/div/button[contains(text(),'"+ ExpandAndCollapse + "')])[1]");
        public By skutext => By.XPath("//table/tbody/tr/td[contains(text(),'210-')]");


        #endregion
        public List<IWebElement> Getskus => WebDriver.FindElements(skutext).ToList();
        public string DraftQuoteNumber => WebDriver.FindElement(BydraftquoteNumber).Text.ToString();

        //Selling price

        //     public string SummarySellingPriceValue => WebDriver.FindElement(By.Id("quoteCreate_summary_sellingPrice")).Text;
        public string SummarySellingPriceValue => WebDriver.FindElement(By.Id("pricingSummary_sellingPrice")).Text;
        //Total List price
    //    public string SummaryTotalListPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_listPrice")).Text;
        public string SummaryTotalListPrice => WebDriver.FindElement(By.Id("pricingSummary_listPrice")).Text;
        // Total Margin
        //*[@id='quoteCreate_summary_marginSummary']/label[1]
        public string SummaryTotalMarginLabel => "SummaryTotalMarginLabel"; /*WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_summary_marginSummary']/label[1]")).Text*/
    //    public string SummaryTotalMarginValue => WebDriver.FindElement(By.Id("quoteCreate_summary_marginValue")).Text;
        public string SummaryTotalMarginValue => WebDriver.FindElement(By.Id("pricingSummary_margin")).Text;
        
        //Pricing Modifier
        public string SummaryPricingModifierLabel => "SummaryPricingModifierLabel"; /*WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_with_pricing_modifier']/label[1]")).Text*/
          public string SummaryPricingModifierValue => WebDriver.FindElement(By.Id("quoteCreate_GI_with_pricing_modifier")).Text;
        
        //First Product
        public string clickOnProduct => "//div[@ class='pull-left title-div' and starts-with(@id,quoteCreate_LI_0_)]";
        //public  IWebElement firstproduct => WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_0_0']"));
        //Discount table 
        public string discountsTable => "//*[@id='quoteCreate_LI_dolPercentage_0_']";
        public IWebElement DiscountsTable => WebDriver.FindElement(By.Id("quoteCreate_LI_dolPercentage_0_0"));
        //LineItem...
        public string LineItemUnitDiscountLabel => "LineItemUnitDiscountLabel";/* WebDriver.FindElement(By.XPath("//*[@class='data-group smartPriceGuidanceContainer input']/label[1]")).Text*/

        public string LineItemUnitSellingPriceLabel => " LineItemUnitSellingPriceLabel"; /*WebDriver.FindElement(By.XPath("//*[@class='pd-0']/div[5]/label[1]")).Text;*/


        public string LineItemSmartRevenueLabel => "LineItemSmartRevenueLabel";/*WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitCompRevenueGroup_0_0']/label[1]")).Text;*/

        public string LineItemUnitMarginLabel => "LineItemUnitMarginLabel"; /*WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitMarginGroup_0_0']/label[1]")).Text;*/


        public string quntityLineItemDiscountLabel => "quntityLineItemDiscountLabel";/* WebDriver.FindElement(By.XPath("//line-item-quantity/div[1]/div[2]//div[3]/label[1]")).Text;*/

        public string quntityLineItemSellingPriceLabel => "quntityLineItemSellingPriceLabel"; /*WebDriver.FindElement(By.XPath("//line-item-quantity/div[1]/div[2]//div[4]/label[1]")).Text;*/

        public string quantityLineItemMarginLabel => "quantityLineItemMarginLabel";/* WebDriver.FindElement(By.XPath("line-item-quantity/div[1]/div[2]//div[5]/label[1]")).Text;*/

        public string quantityLineItemTotalMarginLabel => "quantityLineItemTotalMarginLabel"; /*WebDriver.FindElement(By.XPath("line-item-quantity/div[1]/div[2]//div[6]/label[1]")).Text;*/

        public IWebElement firstConfigProduct => WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_CS_grid_0_0']/div[1]/div[1]/div[1]/div[1]"));

        public static string SKUNumber = string.Empty;
        // WebDriver.FindElement(By.XPath("//table[@class='config-option-sku-table']/tbody[2]/tr[2]/td[2]")).Text;

        /// LineItem Summary 

        public string LineItemSummeryTotalSellingPriceLabel => "LineItemSummeryTotalSellingPriceLabel";/* WebDriver.FindElement(By.XPath("//line-item-summary/div[@id='PricingSummary_0_0']/div[1]/label[1]")).Text;*/

        public string LineItemSummerySmartPriceRevenueLabel => "LineItemSummerySmartPriceRevenueLabel"; /*WebDriver.FindElement(By.XPath("//line-item-summary/div[@id='PricingSummary_0_0']/div[4]/div[2]/label[1]")).Text;*/

        public string LineItemSummeryPricingModifierLabel => "LineItemSummeryPricingModifierLabel";/* WebDriver.FindElement(By.XPath("//line-item-summary/div[@id='PricingSummary_0_0']/div[4]/div[4]/label[1]")).Text;*/



        //Store R, C,F...
        public string RecommenededSellingPriceLabel => "RecommenededSellingPriceLabel";/* WebDriver.FindElement(By.XPath("//*[@class='table mg-left-10']/thead/tr[1]/th[2]")).Text;*/
        public string RecommenededDiscountLabel => "RecommenededDiscountLabel";/* WebDriver.FindElement(By.XPath("//*[@class='table mg-left-10']/thead/tr[1]/th[3]")).Text;*/

        public string RecommendedSmartPricRevenueLabel => "RecommendedSmartPricRevenueLabel";/* WebDriver.FindElement(By.XPath("//*[@class='table mg-left-10']/thead/tr[1]/th[4]")).Text;*/

        public string RecommendedPricingModifierLabel => "RecommendedPricingModifierLabel";/* WebDriver.FindElement(By.XPath("//*[@class='table mg-left-10']/thead/tr[1]/th[5]")).Text;*/

        //CompAnchor elements..
        public string CompAnchorSellingPriceLabel => "CompAnchorSellingPriceLabel";/* RecommenededSellingPriceLabel;*/

        public string CompAnchorDiscountLabel => "CompAnchorDiscountLabel" /*RecommenededDiscountLabel*/;

        public string CompAnchorSmartPricRevenueLabel => "CompAnchorSmartPricRevenueLabel";/* RecommendedSmartPricRevenueLabel*/

        public string CompAnchorPricingModifierLabel => "CompAnchorPricingModifierLabel";/* RecommendedPricingModifierLabel;*/

        //Floor elements..
        public string FloorSellingPriceLabel => "FloorSellingPriceLabel"; /*RecommenededSellingPriceLabel*/

        public string FloorDiscountLabel => "FloorDiscountLabel"; /*RecommenededDiscountLabel;*/

        public string FloorSmartPricRevenuelabel => "FloorSmartPricRevenuelabel";/*RecommendedSmartPricRevenueLabel;*/

        public string FloorPricingModifierLabel => "FloorPricingModifierLabel";/* RecommendedPricingModifierLabel;*/

        //Product--Click on the Show more ..
        public IWebElement ViewMore => WebDriver.FindElement(By.XPath("//*[@id='toggleMoreLess_0_1']/i[1]"));
        public IWebElement ExpandAllTheConfiguration(string expandCollapseText) => WebDriver.FindElement(ByExpandAll(expandCollapseText));
        
        public IWebElement DsaRecommenededSellingPrice => WebDriver.FindElement(ByRecommenededSellingPrice);
        public IWebElement DsaRecommenededDiscount => WebDriver.FindElement(ByRecommenededDiscount);
        public IWebElement DsaRecommendedSmartPricRevenue => WebDriver.FindElement(ByRecommendedSmartPricRevenue);
        public IWebElement DsaRecommendedPricingModifier => WebDriver.FindElement(ByRecommendedPricingModifier);

        public IWebElement DsaCompAnchorSellingPrice => WebDriver.FindElement(ByCompAnchorSellingPrice);
        public IWebElement DsaCompAnchorDiscount => WebDriver.FindElement(ByCompAnchorDiscount);
        public IWebElement DsaCompAnchorSmartPricRevenue => WebDriver.FindElement(ByCompAnchorSmartPricRevenue);
        public IWebElement DsaCompAnchorPricingModifier => WebDriver.FindElement(ByCompAnchorPricingModifier);

        public IWebElement DsaFloorSellingPrice => WebDriver.FindElement(ByFloorSellingPrice);
        public IWebElement DsaFloorDiscount => WebDriver.FindElement(ByFloorDiscount);
        public IWebElement DsaFloorPricingModifier => WebDriver.FindElement(ByFloorPricingModifier);
        public IWebElement DsaFloorSmartPricRevenue => WebDriver.FindElement(ByFloorSmartPricRevenue);
        //click on the 
        


        ////SmartPrice Guidance
        //public static List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();

        DSAPageObject dsobj; 
        public DSAPageObject  RetriveDatafromDsaScreen(int count,Constant constant,DSAPageObject dsa, TestCase test,TotalFinancialCalculation totalFinancialCalculation=null, string ServiceHeaderText=null,string serviceListPrice=null,List<ServicesModule> serviceModule=null,string scenarioId=null)
        {
            dsobj=new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            Product product = new Product(WebDriver);
            try
            {
                dsobj.LocalChannelId=GetLocalChannelId();
               dsobj.SummarySellingPrice= GetSummarySellingPrice();
               dsobj.SummaryTotalMargin= GetSummaryTotalMargin();
               dsobj.SummaryPricingModifier= GetSummaryPricingModifier();
                dsobj.SummaryTotalListPrice=GetSummaryTotalListPrice();
                
               
                dsobj.country = dsa.country;
                dsobj.Quantity = GetQuantity(count);
                dsobj.Title = GetTitle(count);
                dsobj.UnitListPrice = GetUnitListPrice(count);
                dsobj.TotalListPrice = GetTotalListPrice(count);
                dsobj.LineItemUnitSellingPrice = GetLineItemUnitSellingPrice(count);

                dsobj.TotalSellingPrice = GetTotalSellingprice(count);
                dsobj.LineItemUnitDiscount = GetLineItemUnitDiscount(count);
                dsobj.TotalDiscount = GetTotalDiscount(count);
                //Product level...
                dsobj.LineItemSummeryTotalSellingPrice = GetLineItemSummeryTotalSellingPrice(count);
                dsobj.LineItemSummerySmartPriceRevenue = GetLineItemSmartRevenue(count);
                dsobj.LineItemSummeryPricingModifier = GetLineItemSummeryPricingModifiere(count);
                dsobj.LineItemSummaryModifiedRevenue = GetLineItemSummaryModifiedRevenue(count);
                dsobj.LineItemSummaryUpsellServicesModifiedRevenue = GetLineItemSummaryUpsellServicesModifiedRevenue(count);
                dsobj.LineItemSummaryWithServicesIncentive = GetLineItemSummaryWithServicesIncentive(count);

                dsobj.LineItemSmartRevenue = GetLineItemSmartRevenue(count);

                dsobj.LineItemUnitMargin = GetLineItemUnitMargin(count);
                dsobj.TotalMargin = GetMarginPercentage(count);
                dsobj.TotalLineItemMargin = GetTotalLineItemMargin(count);
                
                
                dsobj.RecommenededSellingPrice =GetRecommendedSellingPrice();
                dsobj.RecommenededDiscount = GetRecommendedDiscount();
                dsobj.RecommendedSmartPricRevenue =GetRecommendedSmartPricRevenue();
                dsobj.RecommendedPricingModifier = GetRecommendedPricingModifier();

                dsobj.CompAnchorSellingPrice = GetCompAnchorSellingPrice();
                dsobj.CompAnchorDiscount = GetCompAnchorDiscount();
                dsobj.CompAnchorSmartPricRevenue = GetCompAnchorSmartPricRevenue();
                dsobj.CompAnchorPricingModifier = GetCompAnchorPricingModifier();

                dsobj.FloorSellingPrice = GetFloorSellingPrice();
                dsobj.FloorDiscount = GetFloorDiscount();
                dsobj.FloorPricingModifier = GetFloorPricingModifier();
                dsobj.FloorSmartPricRevenue = GetFloorSmartPricRevenue();

                dsobj.StandardpartnerDiscountText =dsa.PnrLog.StandardPartnerDiscount;

              if(!string.IsNullOrEmpty(dsa.cstTime))
                {
                    dsobj.cstTime = dsa.cstTime;
                }
                CloseSmartPricePopUp();
                Thread.Sleep(2000);
                dsobj.DiscountInputField = GetDiscountFieldValue(count);
                int itemIndex = count + 1;
                RetriveOrderCodeAndSku(dsobj,count,itemIndex);
                cs.jse.ExecuteScript("window.scrollBy(0,900)");                     
                {
              //    RetriveComponentDetails(dsa, constant);
                }
                if (serviceModule!=null)
                {
                    
                 RetriveServiceDetails(dsa, constant, totalFinancialCalculation);// needs to update xpath for configtable
                  

                }
                
                cs.jse.ExecuteScript("window.scrollBy(0,-900)");

                
                if (dsa.AccessoryModules.Count > 0)
                {
                    
                    foreach (IWebElement accessoryObject in constant.ViewMoreAccessory)
                    {
                        try
                        {
                            try
                            {
                                accessoryObject.Click();
                            }
                            catch(Exception ex)
                            {
                                
                                throw new ShowStopperException("unable to click on accessoryObject",ex);

                            }
                            foreach (var accessoryModuleobj in dsa.AccessoryModules)
                            {
                                int index = 0;
                                foreach (IWebElement element in constant.listOfServiceId(accessoryModuleobj.ID))
                                {
                                    if (element.Displayed)
                                    {
                                        WebDriverUtils.ScrollIntoView(WebDriver, element);
                                        element.Click();
                                    }
                                }

                                AddAndRetriveAccessories(count, constant, accessoryModuleobj.ID, index);
                                index++;
                            }
                        }
                        catch (ShowStopperException ex)
                        {
                            throw ex;
                        }
                        
                    }
                    
                    count++;
                }

                itemIndex++;
                
            }
            catch (ShowStopperException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dsobj;
        }

        private string GetDiscountFieldValue(int count)
        {
            string text = "";
            try
            {
                Product product = new Product(WebDriver);
               text=  product.DiscountField(count).GetAttribute("value");
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get GetDiscountFieldValue:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get GetDiscountFieldValue:Error-" + ex.StackTrace);
            }

        
            return text;
        }

        public void CloseSmartPricePopUp()
        {
            try
            {
                if (WebDriver.FindElements(By.XPath("//smart-price-guidance")).Count > 0)
                {
                    var close = WebDriver.FindElement(By.XPath("//smart-price-guidance//i[1]"));//close the smartprice guidance pop up
                    if (close.Displayed)
                    {
                        close.Click();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private string GetSummaryTotalMargin()
        {
            string SummaryTotalMargin = SummaryTotalMarginValue;
            return SummaryTotalMargin;
        }

        private string GetLocalChannelId ()
        {
            string localChannelId = "";
            try
            {
                string[] text = WebDriver.FindElement(By.XPath("//span[@id='currentCustomer_context']")).Text.Split('-');
                 localChannelId = text[0];
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get LocalChannelId:Error-"+ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get LocalChannelId:Error-" + ex.StackTrace);
            }
           
            return localChannelId;
        }
        public string GetMonthAndYear()
        {
            string year,month = "";
            string monthandYear = "";
            try
            {
                string[] monthAndYear = WebDriver.FindElement(ByCurrentDatePicker).GetAttribute("value").Split(' ');
               
                month = monthAndYear[0];
                year = monthAndYear[1];
                monthandYear = string.Format("{0}{1}",month,year);
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.StackTrace);
            }

            return monthandYear;

        }
        public string GetDate()
        {
            string date = "";
            try
            {
                date = WebDriver.FindElement(ByGetDate).GetAttribute("value");
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.StackTrace);
            }
            return date;

        }
        public string GetQuoteExpirationDate()
        {
            string date = "";
            try
            {
                date = string.Format("{0}{1}",GetDate(), GetMonthAndYear());
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.StackTrace);
            }
            return date;

        }
        public string GetQuantity(int count)
        {
            string quantity = "";
            try
            {
                quantity=WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_quantity_0_" + count + "']")).GetAttribute("value");
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Quantity:Error-" + ex.StackTrace);
            }
            return quantity;

        }
        public string GetTitle(int count)
        {
            string title = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                title = constant.ProductTitle.Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get title,Check GetTitle() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get title,Check GetTitle() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return title;

        }
        public string GetUnitListPrice(int count)
        {
            string unitListPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                unitListPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitPrice_0_" + count + "']")).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get UnitListPrice:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get UnitListPrice:Error-" + ex.StackTrace);
            }
            return unitListPrice;

        }
        public string GetTotalListPrice(int count)
        {
            string totalListPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                totalListPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalListPrice_0_" + count + "']")).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get Total List Price:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get UnitTotal List Price:Error-" + ex.StackTrace);
            }
            return totalListPrice;

        }
        public string GetLineItemUnitSellingPrice(int count)
        {
            string lineItemUnitSellingPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemUnitSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_" + count + "']")).GetAttribute("value");
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get LineItemUnitSellingPrice,Check GetLineItemUnitSellingPrice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  Get LineItemUnitSellingPrice,Check GetLineItemUnitSellingPrice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemUnitSellingPrice;

        }
        public string GetTotalSellingprice(int count)
        {
            string totalSellingPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                totalSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalSellingPrice_0_" + count + "']")).GetAttribute("value");
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get TotalSellingprice,Check GetTotalSellingprice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get TotalSellingprice,Check GetTotalSellingprice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return totalSellingPrice;

        }
        public string GetLineItemUnitDiscount(int count)
        {
            string lineItemUnitDiscount = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemUnitDiscount = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_dol_0_" + count + "']")).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount,Check GetLineItemUnitDiscount() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount,Check GetLineItemUnitDiscount() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemUnitDiscount;

        }
        public string GetTotalDiscount(int count)
        {
            string totalDiscount = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                totalDiscount = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalDiscountPercent_0_" + count + "']")).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get totalDiscount,Check GetTotalDiscount() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get totalDiscount,Check GetTotalDiscount() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return totalDiscount;

        }
         public string GetLineItemSummeryTotalSellingPrice(int count)
        {
            string lineItemSummeryTotalSellingPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSummeryTotalSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_pricingTotals_sellingPrice_0_" + count + "']")).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get getineItemSummeryTotalSellingPrice,Check GetLineItemSummeryTotalSellingPrice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get getLineItemSummeryTotalSellingPrice,Check GetLineItemSummeryTotalSellingPrice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummeryTotalSellingPrice;

        }
        public string GetLineItemSummerySmartPriceRevenue(int count)
        {
            string lineItemSummeryTotalSellingPrice = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                dsobj.LineItemSummerySmartPriceRevenue = WebDriver.FindElement(By.Id("quoteCreate_LI_smartPriceRevenue_0_" + count)).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSummerySmartPriceRevenue,Check GetLineItemSummerySmartPriceRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSummerySmartPriceRevenue,Check GetLineItemSummerySmartPriceRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummeryTotalSellingPrice;

        }
        public string GetLineItemSummeryPricingModifiere(int count)
        {
            string lineItemSummeryPricingModifier = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSummeryPricingModifier = WebDriver.FindElement(By.Id("quoteCreate_LI_pricingModifier_0_" + count)).Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummeryPricingModifier,Check GetLineItemSummeryPricingModifier() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummeryPricingModifier,Check GetLineItemSummeryPricingModifier() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummeryPricingModifier;

        }

        public string GetLineItemSummaryModifiedRevenue(int count)
        {
            string lineItemSummaryModifiedRevenue = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSummaryModifiedRevenue = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_" + count + "']/following::div/span[@id='quoteCreate_LI_rawCompRevenue_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummaryModifiedRevenue,Check GetLineItemSummaryModifiedRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummaryModifiedRevenue,Check GetLineItemSummaryModifiedRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummaryModifiedRevenue;

        }
        public string GetLineItemSummaryUpsellServicesModifiedRevenue(int count)
        {
            string lineItemSummaryUpsellServicesModifiedRevenue = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSummaryUpsellServicesModifiedRevenue = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_" + count + "']/following::div/span[@id='quoteCreate_LI_servicesIncentive_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummaryUpsellServicesModifiedRevenue,Check GetLineItemSummaryUpsellServicesModifiedRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemSummaryUpsellServicesModifiedRevenue,Check GetLineItemSummaryUpsellServicesModifiedRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummaryUpsellServicesModifiedRevenue;

        }
        public string GetLineItemSummaryWithServicesIncentive(int count)
        {
            string lineItemSummaryUpsellServicesModifiedRevenue = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSummaryUpsellServicesModifiedRevenue = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_" + count + "']/following::div/span[@id='quoteCreate_LI_commModifier_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSummaryUpsellServicesModifiedRevenue,Check GetLineItemSummaryWithServicesIncentive() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSummaryUpsellServicesModifiedRevenue,Check GetLineItemSummaryWithServicesIncentive() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSummaryUpsellServicesModifiedRevenue;

        }


        public string GetLineItemSmartRevenue(int count)
        {
            string lineItemSmartRevenue = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemSmartRevenue = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitCompRevenue_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSmartRevenue,Check GetLineItemSmartRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemSmartRevenue,Check GetLineItemSmartRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemSmartRevenue;

        }
       
        public string GetLineItemUnitMargin(int count)
        {
            string lineItemUnitMargin = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                lineItemUnitMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitMargin_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemUnitMargin,Check GetLineItemUnitMargin() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get lineItemUnitMargin,Check GetLineItemUnitMargin() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return lineItemUnitMargin;

        }
        public string GetMarginPercentage(int count)
        {
            string totalMargin = "";
            Constant constant = new Constant(WebDriver);
            try
            {
                totalMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_itemMarginPercentage_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemMarginPercentage,Check GetMarginPercentage() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemMarginPercentage,Check GetMarginPercentage() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return totalMargin;

        }
        public string GetTotalLineItemMargin(int count)
        {
            string totalLineItemMargin = "";
            Constant constant = new Constant(WebDriver);
            try
            {
               totalLineItemMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_margin_0_" + count + "']")).Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get totalLineItemMargin,Check GetTotalLineItemMargin() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get totalLineItemMargin,Check GetTotalLineItemMargin() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return totalLineItemMargin;

        }
        public string GetRecommendedSellingPrice()
        {
            string recommenededSellingPrice = "";
           
            try
            {
                recommenededSellingPrice = DsaRecommenededSellingPrice.Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommenededSellingPrice in the smartPriceGuidance ,Check GetRecommendedSellingPrice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommenededSellingPrice in the smartPriceGuidance,Check GetRecommendedSellingPrice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return recommenededSellingPrice;

        }
        public string GetRecommendedDiscount()
        {
            string recommenededDiscount = "";
            
            try
            {
                recommenededDiscount = DsaRecommenededDiscount.Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommenededDiscount in the smartPriceGuidance ,Check GetRecommendedDiscount() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommenededDiscount in the smartPriceGuidance,Check GetRecommendedDiscount() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return recommenededDiscount;

        }
        public string GetRecommendedSmartPricRevenue()
        {
            string recommendedSmartPricRevenue = "";

            try
            {
               recommendedSmartPricRevenue = DsaRecommendedSmartPricRevenue.Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedSmartPricRevenue in the smartPriceGuidance ,Check GetRecommendedSmartPricRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedSmartPricRevenue in the smartPriceGuidance,Check GetRecommendedSmartPricRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return recommendedSmartPricRevenue;

        }
        public string GetRecommendedPricingModifier()
        {
            string recommendedPricingModifier = "";

            try
            {
                recommendedPricingModifier = DsaRecommendedPricingModifier.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the smartPriceGuidance ,Check GetRecommendedSmartPricRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the smartPriceGuidance,Check GetRecommendedSmartPricRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return recommendedPricingModifier;

        }
        public string GetCompAnchorSellingPrice()
        {
            string compAnchorSellingPrice = "";

            try
            {
                compAnchorSellingPrice = DsaCompAnchorSellingPrice.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get compAnchorSellingPrice in the sp guidance ,Check GetCompAnchorSellingPrice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get compAnchorSellingPrice in the sp guidance,Check GetCompAnchorSellingPrice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return compAnchorSellingPrice;

        }
        public string GetCompAnchorDiscount()
        {
            string compAnchorDiscount = "";

            try
            {
               compAnchorDiscount = DsaCompAnchorDiscount.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the compAnchorDiscount ,Check GetCompAnchorDiscount() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the compAnchorDiscount,Check GetCompAnchorDiscount() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return compAnchorDiscount;

        }
        public string GetCompAnchorSmartPricRevenue()
        {
            string compAnchorSmartPricRevenue = "";

            try
            {
               compAnchorSmartPricRevenue = DsaCompAnchorSmartPricRevenue.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the compAnchorDiscount ,Check GetCompAnchorSmartPricRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get recommendedPricingModifier in the compAnchorDiscount,Check GetCompAnchorSmartPricRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return compAnchorSmartPricRevenue;

        }

        public string GetCompAnchorPricingModifier()
        {
            string compAnchorPricingModifier = DsaCompAnchorPricingModifier.Text;

            try
            {
             compAnchorPricingModifier = DsaCompAnchorPricingModifier.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get compAnchorPricingModifier in the sp guidance pop up ,Check GetCompAnchorPricingModifier() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get compAnchorPricingModifier in the sp guidance pop up,Check GetCompAnchorPricingModifier() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return compAnchorPricingModifier;

        }
        public string GetFloorSellingPrice()
        {
            string floorSellingPrice = DsaCompAnchorPricingModifier.Text;

            try
            {
                floorSellingPrice = DsaFloorSellingPrice.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorSellingPrice in the Sp gudance pop up ,Check GetFloorSellingPrice() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorSellingPrice in the Sp gudance pop up,Check GetFloorSellingPrice() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return floorSellingPrice;

        }
        public string GetFloorDiscount()
        {
            string floorDiscount = DsaCompAnchorPricingModifier.Text;

            try
            {
                floorDiscount = DsaFloorDiscount.Text;


            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorDiscount in the SmartPriceGuidance ,Check GetFloorDiscount() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorDiscount in the smartPriceGuidance,Check GetFloorDiscount() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return floorDiscount;

        }
        public string GetFloorPricingModifier()
        {
            string floorPricingModifier = "";

            try
            {
                floorPricingModifier = DsaFloorPricingModifier.Text;

            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorDiscount in the SmartPriceGuidance ,Check GetFloorPricingModifier() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get floorDiscount in the smartPriceGuidance,Check GetFloorPricingModifier() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return floorPricingModifier;

        }

        public string GetFloorSmartPricRevenue()
        {
            string floorSmartPricRevenue = DsaCompAnchorPricingModifier.Text;

            try
            {
                floorSmartPricRevenue = DsaFloorSmartPricRevenue.Text;
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get FloorSmartPricRevenue in the SmartPriceGuidance ,Check GetFloorSmartPricRevenue() in DraftPageObject Class :Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get FloorSmartPricRevenue in the smartPriceGuidance,Check GetFloorSmartPricRevenue() in DraftPageObject Class:Error-" + ex.StackTrace);
            }
            return floorSmartPricRevenue;

        }
        


  



      

        private string GetSummaryTotalListPrice()
        {
            string TotalListPrice = "";
            try
            {
                 TotalListPrice = SummaryTotalListPrice;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  GetSummaryTotalListPrice:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  GetSummaryTotalListPrice:Error-" + ex.StackTrace);
            }
            
            return TotalListPrice;
        }

        private string GetSummaryPricingModifier( )
        {
            string SummaryPricingModifier = "";
            try
            {
                 SummaryPricingModifier = SummaryPricingModifierValue;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummaryPricingModifier:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummaryPricingModifier:Error-" + ex.StackTrace);
            }


            return SummaryPricingModifier;
        }

        private string GetSSummaryTotalMargin()
        {
            string SummaryTotalMargin = "";
            try
            { 
            SummaryTotalMargin = SummaryTotalMarginValue;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummaryPricingModifier:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummaryPricingModifier:Error-" + ex.StackTrace);
            }

            return SummaryTotalMargin;
        }

        private string GetSummarySellingPrice()
        {
            string SummarySellingPrice = "";
            try
            {
              SummarySellingPrice = SummarySellingPriceValue;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummarySellingPrice:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get SummarySellingPrice:Error-" + ex.StackTrace);
            }

            return SummarySellingPrice;

        }

        public void RetriveOrderCodeAndSku(DSAPageObject dsobj,int count,int itemIndex)
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                RetriveOrderCode(count);
                
                var a = DraftQuoteNumber;
                
                string[] str = a.Split('.');
                string withOutVersionQuoteNo = str[0];
                dsobj.QuoteNumber = withOutVersionQuoteNo;
               
                string productDescription = constant.ProductDescriptin(count).GetAttribute("innerText");

                if (string.IsNullOrEmpty(dsobj.SKUNumber))
                {

                    dsobj.SKUNumber = RetriveSkunumber(constant, itemIndex, dsobj, productDescription, count);
                }
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetch completed for {0}", dsobj.SKUNumber), true);
            }
            catch(ShowStopperException ex)
            {
                throw ex;
            }

            

        }

        private void RetriveOrderCode(int count)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                if (constant.IsElementPresent(By.Id("quoteCreate_LI_orderCode_0_" + count)))
                {
                    dsobj.OrderCode = WebDriver.FindElement(By.Id("quoteCreate_LI_orderCode_0_" + count)).Text;
                }
                else
                {
                    Console.WriteLine("Ordercode is not there");
                    foreach (IWebElement skunum in constant.SkuLabel)
                    {
                        if (skunum.Displayed)
                        {
                            dsobj.SKUNumber = skunum.Text;
                            dsobj.OrderCode = dsobj.SKUNumber;
                        }

                    }
                    //dsobj.SKUNumber = WebDriver.FindElement(By.XPath("(//div/label[text()='Sku Number']/following::div)["+count+"]")).Text;
                }
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("OrderCode/sku is not found.Please check  'RetriveOrderCode()' in'DraftQuotePageObject' Class "+ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("OrderCode/sku is not found.Please check  'RetriveOrderCode()' in'DraftQuotePageObject' Class " + ex.StackTrace);
            }
        }

        public void RetriveOrderCodeAndSkuForAccessory(DSAPageObject dsobj, int count, int itemIndex)
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                GetAccessoryOrderCode(count);
              
                var a = DraftQuoteNumber;
                //constant.DraftQuote.Length
                string[] str = a.Split('.');
                string withOutVersionQuoteNo = str[0];
                dsobj.QuoteNumber = withOutVersionQuoteNo;
                //product description
                string productDescription = constant.ProductDescriptin(count).GetAttribute("innerText");

                if (string.IsNullOrEmpty(dsobj.SKUNumber))
                {

                    dsobj.SKUNumber = RetriveSkunumber(constant, itemIndex, dsobj, productDescription, count);
                }

                
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetch completed for {0}", dsobj.SKUNumber), true);
            }
            
            catch (Exception ex)
            {
                new ShowStopperException("Unable to find  accessory SKU/OrderCode",ex);
            }

        }

      

        private void GetAccessoryOrderCode(int count)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                if (constant.IsElementPresent(By.Id("2_Sku_skus_0_" + count)))
                {
                    dsobj.OrderCode = WebDriver.FindElement(By.Id("2_Sku_skus_0_" + count)).Text;
                }
                else
                {
                    Console.WriteLine("Ordercode is not there");
                    foreach (IWebElement skunum in constant.AccessorySKuLabel)
                    {
                        if (skunum.Displayed)
                        {
                            dsobj.SKUNumber = skunum.Text;
                            dsobj.OrderCode = dsobj.SKUNumber;
                        }

                    }
                    //dsobj.SKUNumber = WebDriver.FindElement(By.XPath("(//div/label[text()='Sku Number']/following::div)["+count+"]")).Text;
                }
            }
            catch(Exception ex)
            {

            }
        }

        public string RetriveSKU(int count)
        {
            string skunum = "";
            try
            {
                 //int item = count + 1;
                Constant cs = new Constant(WebDriver);

                if (cs.IsElementPresent(ByExpandAll("Expand All")))
                {
                    ExpandAll("Expand All");
                    foreach (var getSKU in Getskus)
                    {
                        if (getSKU.Displayed)
                            skunum = getSKU.Text;
                    }

                }
                if (cs.IsElementPresent(ByExpandAll("Collapse All")))
                {
                    ExpandAll("Collapse All");
                }
            }

            catch (Exception ex)
            {
                throw new ShowStopperException("Unable to find Sku ,Please Check'RetriveSKU()','DraftQuotePage Class' ", ex); 
            }
            
            return skunum;
        }


        public DraftQuotePageObject ExpandAll(string Text)
        {
            WebDriverUtils.ScrollIntoView(WebDriver,ByExpandAll(Text));
            ExpandAllTheConfiguration(Text).Click();
           return new DraftQuotePageObject(WebDriver);
        }
        public string RetriveSkunumber(Constant cs,int itemIndex,DSAPageObject dsobj,string productDescription,int count)
        {
            try
            {
                Product productobj = new Product(WebDriver);
                 productDescription=CheckPoductConfigurationText(productDescription);
                
                
                if(string.IsNullOrEmpty(dsobj.SKUNumber))
                {
                    dsobj.SKUNumber= RetriveSKU(count);

                }
            }
            catch (ShowStopperException ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message,true);
            }
            
            //}
            return dsobj.SKUNumber;
        }

        private string CheckPoductConfigurationText(string productDescription)
        {
            
            Constant cs = new Constant(WebDriver);
            
            if (cs.IsElementPresent(cs.ByFindSkus(productDescription))) return productDescription;
            string[] text = productDescription.Split('-');
            return text[0].Trim();


        }
        private string GetAccesoryUnitListPrice()
        {
            string UnitListPrice = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                UnitListPrice = accessoryXpath.UnitListPrice_Accessorylist.Find(x => x.Displayed).Text; 
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get UnitListPrice:Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get UnitListPrice:Error-" + ex.StackTrace);
            }

            return UnitListPrice;

        }
        private string GetAccessoryLineItemUnitDiscount()
        {
            string lineItemUnitDiscount = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                lineItemUnitDiscount = accessoryXpath.unitDiscount_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitDiscount():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitDiscount():Error-" + ex.StackTrace);
            }

            return lineItemUnitDiscount;

        }
        private string GetAccessoryLineItemUnitSellingPrice()
        {
            string lineItemUnitSellingPrice = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                lineItemUnitSellingPrice = accessoryXpath.UnitSellingPrice_Accessory.Find(x => x.Displayed).GetAttribute("value");
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitSellingPrice():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitDiscount():Error-" + ex.StackTrace);
            }

            return lineItemUnitSellingPrice;

        }
        private string GetAccessoryLineItemSmartRevenue()
        {
            string lineItemSmartRevenue = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                lineItemSmartRevenue = accessoryXpath.UnitSmartPriceRevenue_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemSmartRevenue():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemSmartRevenue():Error-" + ex.StackTrace);
            }

            return lineItemSmartRevenue;

        }
        private string GetAccessoryLineItemUnitMargin()
        {
            string lineItemUnitMargin = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                lineItemUnitMargin = accessoryXpath.UnitSmartPriceRevenue_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitMargin():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryLineItemUnitMargin():Error-" + ex.StackTrace);
            }

            return lineItemUnitMargin;

        }
        private string GetAccessoryTotalListPrice()
        {
            string totalListPrice = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                totalListPrice = accessoryXpath.TotalListPrice_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalListPrice():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalListPrice():Error-" + ex.StackTrace);
            }

            return totalListPrice;

        }
        private string GetAccessoryTotalDiscount()
        {
            string totalDiscount = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                totalDiscount = accessoryXpath.TotalListPrice_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalDiscount():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalDiscount():Error-" + ex.StackTrace);
            }

            return totalDiscount;

        }

        private string GetAccessoryTotalSellingPrice()
        {
            string totalSellingPrice = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                totalSellingPrice = accessoryXpath.TotalListPrice_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalSellingPrice():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalSellingPrice():Error-" + ex.StackTrace);
            }

            return totalSellingPrice;

        }

        private string GetAccessoryTotalMargin()
        {
            string totalMargin = "";
            Constant accessoryXpath = new Constant(WebDriver);
            try
            {
                totalMargin = accessoryXpath.TotalMargin_Accessory.Find(x => x.Displayed).Text;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalMargin():Error-" + ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to  get LineItemUnitDiscount.Please refer GetAccessoryTotalMargin():Error-" + ex.StackTrace);
            }

            return totalMargin;

        }
       
      

        public void AddAndRetriveAccessories(int count, Constant accessoryXpath,string moduleId,int index)
        {
            DSAPageObject tiedAccessory = new DSAPageObject(WebDriver);
            DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
            try
            {
                RetriveSKU(count);
                RetriveOrderCodeAndSkuForAccessory(tiedAccessory, count, index);
          //      tiedAccessory.SKUNumber = WebDriver.FindElement(By.XPath("//div[@aria-hidden='false']//table//th[text()='SKU']/ancestor::table//td[3]")).Text;
                tiedAccessory.QuoteNumber = dsobj.QuoteNumber;
                //dsobj.accessories.AccessoryTitle = constant.AccessoryTitle
                //   tiedAccessory.UnitListPrice = accessoryXpath.UnitListPrice_Accessory(count,index).Text;
                tiedAccessory.UnitListPrice = GetAccesoryUnitListPrice();
                tiedAccessory.LineItemUnitDiscount = GetAccessoryLineItemUnitDiscount();
                tiedAccessory.LineItemUnitSellingPrice = GetAccessoryLineItemUnitSellingPrice();
                tiedAccessory.LineItemSmartRevenue = GetAccessoryLineItemSmartRevenue();
                tiedAccessory.LineItemUnitMargin = GetAccessoryLineItemUnitMargin();
                tiedAccessory.TotalListPrice = GetAccessoryTotalListPrice();
                tiedAccessory.TotalDiscount = GetAccessoryTotalDiscount();
                tiedAccessory.TotalSellingPrice = GetAccessoryTotalSellingPrice();
                tiedAccessory.TotalMargin = GetAccessoryTotalMargin();

                WebDriverUtils.ScrollIntoView(WebDriver, accessoryXpath.ByDOLPercentage);
               
                accessoryXpath.DOLPercentage.Find(x => x.Displayed).Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(drobj.ByRecommenededSellingPrice));
                tiedAccessory.RecommenededSellingPrice = GetRecommendedSellingPrice();
                tiedAccessory.RecommenededDiscount = GetRecommendedDiscount();
                tiedAccessory.RecommendedSmartPricRevenue = GetRecommendedSmartPricRevenue();
                tiedAccessory.RecommendedPricingModifier = GetRecommendedPricingModifier();

                tiedAccessory.CompAnchorSellingPrice = GetCompAnchorSellingPrice();
                tiedAccessory.CompAnchorDiscount = GetCompAnchorDiscount();
                tiedAccessory.CompAnchorSmartPricRevenue = GetCompAnchorSmartPricRevenue();
                tiedAccessory.CompAnchorPricingModifier = GetCompAnchorPricingModifier();

                tiedAccessory.FloorSellingPrice = GetFloorSellingPrice();
                tiedAccessory.FloorDiscount = GetFloorDiscount();
                tiedAccessory.FloorSmartPricRevenue = GetFloorSmartPricRevenue();
                tiedAccessory.FloorPricingModifier = GetFloorPricingModifier();

               CloseSmartPricePopUp();
                dsobj.Accessories.Add(tiedAccessory);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Fetech completed for Accessory "+ tiedAccessory.SKUNumber, true);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to fetch accessory details",true);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to fetch accessory details"+ex.Message);
            }
            
        }
               
        public void RetriveComponentDetails(DSAPageObject product,Constant constant)
        {
            Actions action = new Actions(WebDriver);
            try
            {
                foreach (var componentModule in product.ComponentModules)
                {
                    foreach (var component in componentModule.Components)
                    {
                        IWebElement title = WebDriver.FindElement(By.XPath("//*[text()='" + componentModule.Title + ":']"));
                   //     action.MoveToElement(WebDriver.FindElement(By.XPath("//*[text()='" + a.Title + ":']")));
                       constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);", title);
                 //       action.ClickAndHold();
                        
                        constant.jse.ExecuteScript("arguments[0].click();", title);
                  //      WebDriver.FindElement(By.XPath("//*[text()='"+a.Title+":']")).Click();
                        
                        decimal sellingPrice = 0;
                        for (int i = 2; i <= constant.configTable(componentModule.ID).Count; i++)
                        {
                            DSAPageObject componentObject = new DSAPageObject();
                            componentObject.SKUNumber = constant.ServiceConfigSku(componentModule.ID,i).Text;
                            componentObject.UnitListPrice = constant.ServiceUnitListPrice(componentModule.ID,i).Text;
                            //serviceObject.LineItemUnitSellingPrice = constant.ServiceUnitSellingPrice(i).Text;



                            if (Convert.ToDecimal(componentObject.UnitListPrice.Replace("$","").Replace(",",""))== component.ListPrice)
                            {
                                sellingPrice = Convert.ToDecimal(constant.ServiceUnitSellingPrice(componentModule.ID,i).Text.Replace("$", ""));
                                component.SellingPrice += sellingPrice;
                                dsobj.ComponentModules.Add(componentModule);

                            }
                           

                           


                        }

                        constant.jse.ExecuteScript("arguments[0].click();", title);

                    }
                    
                }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Console.WriteLine(ex.Message);
            }
          
            
            
        }
        public void RetriveServiceDetails(DSAPageObject product, Constant constant, TotalFinancialCalculation totalFinancialCalculation)
        
            {
            try
            {


                foreach (ServicesModule service in product.ServiceModules)
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
                    IWebElement title = null;

                    foreach (IWebElement element in constant.listOfServiceId(service.ID))
                    {
                        if (element.Displayed)
                        {
                            WebDriverUtils.ScrollIntoView(WebDriver,element);
                            element.Click();
                        }
                    }
               

             //       IWebElement title = WebDriver.FindElement(By.XPath("//*[starts-with(@id, 'lineitem_config_block_0_0_"+service.ID+"_') and contains(@id, '_mainItem_headingOne')]"));//needs to update service title
                    //jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", title.Location.X, title.Location.Y);
                    //jse.ExecuteScript("window.scrollBy(0, -250)");
                    //title.Click();
              //         Constant.jse.ExecuteScript("arguments[0].click();", title);
                    //     RetriveServices(constant, totalFinancialCalculation,service.Services);

                    //     string TotalServiceSellingPrice = "";
                    decimal TotalServiceListPrice = 0;
                    decimal TotalsellingPrice = 0;
                    DSAPageObject serviceObject = new DSAPageObject();
                    for (int i =1; i <=constant.configTable(service.ID).Count; i++)
                    {

                        serviceObject.SKUNumber = constant.ServiceConfigSku(service.ID, i).Text;
                        serviceObject.UnitListPrice = constant.ServiceUnitListPrice(service.ID,i).Text;
                        serviceObject.LineItemUnitSellingPrice = constant.ServiceUnitSellingPrice(service.ID,i).Text;
                        TotalServiceListPrice += Convert.ToDecimal(serviceObject.UnitListPrice.Replace("$", "").Replace(",", ""));
                        TotalsellingPrice += Convert.ToDecimal(serviceObject.LineItemUnitSellingPrice.Replace("$", "").Replace(",", ""));

                        //serviceObject.LineItemUnitSellingPrice = constant.ServiceUnitSellingPrice(i).Text;
                    }

                    foreach (Service list in service.Services)
                    {
                        if (list.ListPrice == TotalServiceListPrice)
                        {
                            // sellingPrice = Convert.ToDecimal(constant.ServiceUnitSellingPrice(i).Text.Replace("$", ""));
                            if (list.IsUpsell)
                            {
                                dsobj.UpsellServiceSellingPrice += TotalsellingPrice;
                            }
                        }
                        else
                        {
                            Console.WriteLine("listprice is not matching");
                        }
                        totalFinancialCalculation.CalculatedTotalServicesSellingPrice += TotalsellingPrice;


                    }
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[starts-with(@id,'lineitem_config_block')]/span[text()='"+service.ID+"']")));
                    WebDriver.FindElement(By.XPath("//*[starts-with(@id,'lineitem_config_block')]/span[text()='"+service.ID+"']")).Click();
                   



                }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage(ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage(ex.StackTrace);
            }
        }
     
      
    }
}
