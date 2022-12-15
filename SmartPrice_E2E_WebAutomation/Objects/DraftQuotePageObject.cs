using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SeleniumExtras.WaitHelpers;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Objects
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
        public  string DraftQuoteNumber => WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_title_draftquote']/small[1]")).Text.ToString();
        //Selling price
        
        public string SummarySellingPriceValue => WebDriver.FindElement(By.Id("quoteCreate_summary_sellingPrice")).Text;
        //Total List price
        public string SummaryTotalListPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_listPrice")).Text;
        // Total Margin
        //*[@id='quoteCreate_summary_marginSummary']/label[1]
        public string SummaryTotalMarginLabel => "SummaryTotalMarginLabel"; /*WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_summary_marginSummary']/label[1]")).Text*/
        public string SummaryTotalMarginValue => WebDriver.FindElement(By.Id("quoteCreate_summary_marginValue")).Text;
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
                string[] text = WebDriver.FindElement(By.XPath("//span[@id='currentCustomer_context']")).Text.Split('-');
                dsobj.LocalChannelId = text[0];
                dsobj.SummarySellingPrice = SummarySellingPriceValue;
                dsobj.SummaryTotalMargin = SummaryTotalMarginValue;
                dsobj.SummaryPricingModifier = SummaryPricingModifierValue;
                dsobj.SummaryTotalListPrice = SummaryTotalListPrice;
                
                dsobj.Quantity = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_quantity_0_"+count+"']")).GetAttribute("value");
                dsobj.Title = constant.ProductTitle.Text;
                dsobj.UnitListPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitPrice_0_" + count + "']")).Text;
                dsobj.TotalListPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalListPrice_0_" + count + "']")).Text;
                dsobj.LineItemUnitSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_" + count + "']")).GetAttribute("value"); ;
                //dsobj.TotalSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_pricingInformation_sellingPrice_0_" + count + "']")).Text;
                dsobj.TotalSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalSellingPrice_0_"+count+"']")).GetAttribute("value");
                dsobj.LineItemUnitDiscount = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_dol_0_" + count + "']")).Text;
                dsobj.TotalDiscount = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_totalDiscountPercent_0_" + count + "']")).Text;
                //Product level...
                dsobj.LineItemSummeryTotalSellingPrice = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_pricingTotals_sellingPrice_0_" + count + "']")).Text;
                dsobj.LineItemSummerySmartPriceRevenue = WebDriver.FindElement(By.Id("quoteCreate_LI_smartPriceRevenue_0_" + count)).Text;
                dsobj.LineItemSummeryPricingModifier = WebDriver.FindElement(By.Id("quoteCreate_LI_pricingModifier_0_" + count)).Text;
                dsobj.LineItemSummaryModifiedRevenue = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_"+count+"']/following::div/span[@id='quoteCreate_LI_rawCompRevenue_0_"+count+"']")).Text;
                dsobj.LineItemSummaryUpsellServicesModifiedRevenue = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_"+count+ "']/following::div/span[@id='quoteCreate_LI_servicesIncentive_0_" + count+"']")).Text;
                dsobj.LineItemSummaryWithServicesIncentive = WebDriver.FindElement(By.XPath("//div[@id='PricingSummary_0_"+count+ "']/following::div/span[@id='quoteCreate_LI_commModifier_0_" + count+"']")).Text;
               
                dsobj.LineItemSmartRevenue = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitCompRevenue_0_" + count + "']")).Text;

                dsobj.LineItemUnitMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_PI_unitMargin_0_" + count + "']")).Text;
                dsobj.TotalMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_itemMarginPercentage_0_" + count + "']")).Text;
                dsobj.TotalLineItemMargin = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_margin_0_" + count + "']")).Text;
                
                dsobj.RecommenededSellingPrice = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[1]")).Text;
                dsobj.RecommenededDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]")).Text;
                dsobj.RecommendedSmartPricRevenue = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[3]")).Text;
                dsobj.RecommendedPricingModifier = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[4]")).Text;

                dsobj.CompAnchorSellingPrice = WebDriver.FindElement(By.XPath("//smart-price-guidance//tr[2]/td[2]")).Text;
                dsobj.CompAnchorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//tr[2]/td[3]")).Text;
                dsobj.CompAnchorSmartPricRevenue = WebDriver.FindElement(By.XPath("//smart-price-guidance//tr[2]/td[4]")).Text;
                dsobj.CompAnchorPricingModifier = WebDriver.FindElement(By.XPath("//smart-price-guidance//tr[2]/td[5]")).Text;

                dsobj.FloorSellingPrice = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[1]")).Text;
                dsobj.FloorDiscount = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]")).Text;
                dsobj.FloorPricingModifier = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[4]")).Text;
                dsobj.FloorSmartPricRevenue = WebDriver.FindElement(By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[3]")).Text;
                dsobj.StandardpartnerDiscountText =dsa.PnrLog.StandardPartnerDiscount;
              if(!string.IsNullOrEmpty(dsa.cstTime))
                {
                    dsobj.cstTime = dsa.cstTime;
                }
                
                if (WebDriver.FindElements(By.XPath("//smart-price-guidance")).Count > 0)
                {
                    var close = WebDriver.FindElement(By.XPath("//smart-price-guidance//i[1]"));//close the smartprice guidance pop up
                    if (close.Displayed)
                    {
                        new Actions(WebDriver).Click(close).Perform();
                    }
                }
                dsobj.DiscountInputField = product.DiscountField(count).GetAttribute("value");
                int itemIndex = count + 1;
                RetriveOrderCodeAndSku(dsobj,count,itemIndex);




                cs.jse.ExecuteScript("window.scrollBy(0,900)");
                if (scenarioId == "D03")
                {
              //    RetriveComponentDetails(dsa, constant);
                }
                if (scenarioId == "D05" || scenarioId == "D06" || scenarioId == "D07"||scenarioId=="D03"||scenarioId == "D04")
                {
                    if (dsobj.OrderCode == dsa.OrderCode)
                    {
                        RetriveServiceDetails(dsa, constant, totalFinancialCalculation);// needs to update xpath for configtable
                    }

                }
                
                cs.jse.ExecuteScript("window.scrollBy(0,-900)");
                var viewMoreClose = WebDriver.FindElement(By.XPath("//a[@id='toggleMoreLess_0_" + constant.Count + "']/i[@class='icon-ui-expand']"));
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='toggleMoreLess_0_" + constant.Count + "']/i[@class='icon-ui-expand']")));
                cs.jse.ExecuteScript("arguments[0].click();", viewMoreClose);
                

                
               
                if (dsa.AccessoryModules.Count > 0 && dsobj.OrderCode==dsa.OrderCode)
                {
                    
                    foreach(IWebElement accessoryObject in constant.ViewMoreAccessory)
                    {
                        accessoryObject.Click();
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
                    
                    


                    

                    count++;
                }

                itemIndex++;
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dsobj;
        }

        public void RetriveOrderCodeAndSku(DSAPageObject dsobj,int count,int itemIndex)
        {
            Constant constant = new Constant(WebDriver);
            if (constant.IsElementPresent(By.Id("quoteCreate_LI_orderCode_0_" + count)))
            {
                dsobj.OrderCode = WebDriver.FindElement(By.Id("quoteCreate_LI_orderCode_0_" + count)).Text;
            }
            else
            {
                Console.WriteLine("Ordercode is not there");
                foreach(IWebElement skunum in constant.SkuLabel)
                {
                    if(skunum.Displayed)
                    {
                        dsobj.SKUNumber = skunum.Text;
                    }
                    
                }
                //dsobj.SKUNumber = WebDriver.FindElement(By.XPath("(//div/label[text()='Sku Number']/following::div)["+count+"]")).Text;
            }
            var a = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_title_draftquote']/small[1]")).Text;
            //constant.DraftQuote.Length
            string[] str = a.Split('.');
            string withOutVersionQuoteNo = str[0];
            dsobj.QuoteNumber = withOutVersionQuoteNo;
            //product description
            string productDescription = constant.ProductDescriptin(count).GetAttribute("innerText");
            
            if (string.IsNullOrEmpty(dsobj.SKUNumber))
            {
                
                dsobj.SKUNumber = RetriveSkunumber(constant, itemIndex, dsobj, productDescription,count);
            }

            //     dsobj.SKUNumber = WebDriver.FindElement(By.XPath("//*[@id='lineitem_config_block_0_" + count + "']/div[1]/div[2]/div/div[2]/div/table/tbody[2]/tr[2]/td[3]")).Text;

            //       Constant.jse.ExecuteScript("arguments[0].click()", constant.ProductConfiguration(count));
            //  Console.WriteLine(dsobj.QuoteNumber+ ", "+dsobj.SKUNumber);
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Fetch completed for {0}", dsobj.SKUNumber), true);

        }

        public string RetriveSkunumber(Constant cs,int itemIndex,DSAPageObject dsobj,string productDescription,int count)
        {
            try
            {
                Product productobj = new Product(WebDriver);
                 productDescription=CheckPoductConfigurationText(productDescription);
                
                foreach (IWebElement element in cs.FindSkus(productDescription))
                {
                    if (cs.IsElementPresent(cs.ByFindSkus(productDescription)))
                    {
                        cs.jse.ExecuteScript("arguments[0].click()", element);
                        dsobj.SKUNumber = WebDriver.FindElement(By.XPath("//*[starts-with(@id,'lineitem_config_block_0_" + count + "_1_0') and contains(@id,'_body')]/div[2]/table/tbody/tr[2]/td[3]")).GetAttribute("innerText");
                    }
                }
                //if (cs.IsElementPresent(cs.ByFindSku(productDescription,itemIndex)))
                //{
                //    //cs.FindSku(productDescription).Click();
                //    //         productobj.ScrollInto(cs.FindSku(productDescription),10);
                //    cs.jse.ExecuteScript("arguments[0].click()", cs.FindSku(productDescription, itemIndex));
                //    dsobj.SKUNumber = WebDriver.FindElement(By.XPath("//*[starts-with(@id,'lineitem_config_block_0_" + count + "_1_0') and contains(@id,'_body')]/div[2]/table/tbody/tr[2]/td[3]")).GetAttribute("innerText");
                //}
                //else
                //{
                //    cs.jse.ExecuteScript("arguments[0].click()", cs.FindSku(productDescription, itemIndex));
                //    dsobj.SKUNumber = WebDriver.FindElement(By.XPath("//*[starts-with(@id,'lineitem_config_block_0_0_" + count + "_') and contains(@id,'_body')]/div[2]/table/tbody/tr[2]/td[3]")).GetAttribute("innerText");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //}
            return dsobj.SKUNumber;
        }

        private string CheckPoductConfigurationText(string productDescription)
        {
            
            Constant cs = new Constant(WebDriver);
            
            if (cs.IsElementPresent(cs.ByFindSkus(productDescription))) return productDescription;
            string[] text = productDescription.Split('-');
            return CheckPoductConfigurationText(text[0].Trim());


        }

      

        public void AddAndRetriveAccessories(int count, Constant accessoryXpath,string moduleId,int index)
        {
            DSAPageObject tiedAccessory = new DSAPageObject();
            try
            {
                foreach(var sku in accessoryXpath.SkuNumber)
                {
                    if(sku.Displayed)
                    {
                        tiedAccessory.SKUNumber = sku.Text;
                        break;
                    }
                }
                
                tiedAccessory.QuoteNumber = accessoryXpath.DraftQuote.Substring(0, accessoryXpath.DraftQuote.Length - 2);
                //dsobj.accessories.AccessoryTitle = constant.AccessoryTitle
              //   tiedAccessory.UnitListPrice = accessoryXpath.UnitListPrice_Accessory(count,index).Text;
                tiedAccessory.UnitListPrice = accessoryXpath.UnitListPrice_Accessorylist.Find(x=>x.Displayed).Text;
                tiedAccessory.LineItemUnitDiscount = accessoryXpath.unitDiscount_Accessory.Find(x => x.Displayed).Text;
                tiedAccessory.LineItemUnitDiscount = accessoryXpath.unitDiscount_Accessory.Find(x => x.Displayed).Text;
                tiedAccessory.LineItemUnitSellingPrice = accessoryXpath.UnitSellingPrice_Accessory.Find(x => x.Displayed).GetAttribute("value");
                tiedAccessory.LineItemSmartRevenue = accessoryXpath.UnitSmartPriceRevenue_Accessory.Find(x => x.Displayed).Text;
                tiedAccessory.LineItemUnitMargin = accessoryXpath.UnitSmartPriceRevenue_Accessory.Find(x => x.Displayed).Text;
                tiedAccessory.TotalListPrice= accessoryXpath.TotalListPrice_Accessory.Find(x => x.Displayed).Text;
                tiedAccessory.TotalDiscount = accessoryXpath.TotalDiscount_Accesory.Find(x => x.Displayed).Text;
                tiedAccessory.TotalSellingPrice = accessoryXpath.TotalSellingPrice_Accesory.Find(x => x.Displayed).Text;
                tiedAccessory.TotalMargin = accessoryXpath.TotalMargin_Accessory.Find(x => x.Displayed).Text;

                accessoryXpath.jse.ExecuteScript("window.scrollBy(0,-300)");
                accessoryXpath.DOLPercentage.Find(x => x.Displayed).Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(accessoryXpath.ByRecommenededSellingPrice));
                tiedAccessory.RecommenededSellingPrice = accessoryXpath.RecommendedSellingPrice.Text;
                tiedAccessory.RecommenededDiscount = accessoryXpath.RecommenededDiscount.Text;
                tiedAccessory.RecommendedSmartPricRevenue = accessoryXpath.RecommendedSmartPricRevenue.Text;
                tiedAccessory.RecommendedPricingModifier = accessoryXpath.RecommendedPricingModifier.Text;

                tiedAccessory.CompAnchorSellingPrice = accessoryXpath.CompAnchorSellingPrice.Text;
                tiedAccessory.CompAnchorDiscount = accessoryXpath.CompAnchorDiscount.Text;
                tiedAccessory.CompAnchorSmartPricRevenue = accessoryXpath.CompAnchorSmartPricRevenue.Text;
                tiedAccessory.CompAnchorPricingModifier = accessoryXpath.CompAnchorPricingModifier.Text;

                tiedAccessory.FloorSellingPrice = accessoryXpath.FloorSellingPrice.Text;
                tiedAccessory.FloorDiscount = accessoryXpath.FloorDiscount.Text;
                tiedAccessory.FloorSmartPricRevenue = accessoryXpath.FloorSmartPricRevenue.Text;
                tiedAccessory.FloorPricingModifier = accessoryXpath.FloorPricingModifier.Text;




                var close = WebDriver.FindElement(By.XPath("//smart-price-guidance//i[1]"));
                close.Click();

               


                dsobj.Accessories.Add(tiedAccessory);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Fetech completed for Accessory "+ tiedAccessory.SKUNumber, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    for (int i =1; i <constant.configTable(service.ID).Count; i++)
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
          //          new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[text()='" + service.Title + ":']")));
                    //title.Click();



                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
     
      
    }
}
