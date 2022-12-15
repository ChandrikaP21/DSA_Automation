using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.Objects.Quote;

namespace SmartPrice_E2E_WebAutomation.Quote.D01
{
    public class ScenarioMain : DriverHelper
    {
        public readonly string TestUrl;
        public static List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
        PNRAPICalulationCompare calculatedValue = new PNRAPICalulationCompare();
        Constant constant = new Constant();
        public ScenarioMain(string URL)
        {
            TestUrl = URL;

        }
        /// <summary>
        /// open the respective URL
        /// Click on the Menu  and search for the quote 
        /// 
        /// </summary>
        public void LoadDSA()
        {
            WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            WebDriver.Manage().Window.Maximize();
            WebDriver.Navigate().GoToUrl(TestUrl);
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            var a = constant.ByMenuLabel;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(constant.ByMenuLabel));
            Constant.jse.ExecuteScript("arguments[0].click();", constant.MenuButton);
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(constant.clickOnQuote));
            Console.WriteLine(".................Application is loaded and click on Menu..................");

        }
        public void SearchQuote(DSAPageObject dSA)
        {

            Constant.jse.ExecuteScript("arguments[0].click()", constant.clickOnQuote);
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20));
            constant.SearchQuoteNumber.SendKeys(dSA.ActualQuote.ToString());
            Constant.jse.ExecuteScript("arguments[0].click()", constant.QuoteSearchButton);
            //if (WebDriver.Url.Contains("g4"))
            //{
            //    Constant.jse.ExecuteScript("arguments[0].click()", constant.SendAnywayButton);
            //}
            WebDriverWait wait1 = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
            wait1.Until(ExpectedConditions.ElementToBeClickable(constant.ByMoreAction));

            Console.WriteLine("............Search button is clicked..........");
        }
        public void CopyAndCreateNewQuote()
        {
            constant.MoreActionsDropdown.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCopyQuote));
            constant.CopyAsNewQuote.Click();
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[@id='quoteCreate_title_draftquote']")));
            string draftQuoteNumber = WebDriver.FindElement(By.XPath("//h2[@id='quoteCreate_title_draftquote']")).FindElement(By.XPath("//small[1]")).Text;
            Console.WriteLine(draftQuoteNumber);
            Console.WriteLine(".......Quote is copied........");
        }

        public  void RetriveDataFromDSAScreen(TotalFinancialCalculation totalFinancialCalculation,string ServiceHeaderText=null, List<ServicesModule> servicModule = null,string serviceListPrice=null)
        {
            // int count = 0;
            try
            {
                while (constant.Count < constant.NoOfProducts.Count)
                {

                    DraftQuotePageObject drobj = new DraftQuotePageObject();
                    Constant.jse.ExecuteScript("window.scrollBy(0, 400)");
                    if (constant.IsElementPresent(constant.ByProduct))
                    {
                        //click on the  Product 

                        //IWebElement ele1 = WebDriver.FindElement(By.CssSelector("#quoteCreate_LI_0_"+constant.Count+".k-icon.k-i-arrow-s"));
                        //if (ele1.Enabled)
                        //{
                        //    ele1.Click();
                        //}
                        constant.Product.Click();

                        //new Actions(WebDriver).Click(constant.Product).Perform();
                    }

                    //if (constant.IsProductExpanded != null && constant.ViewMore.Enabled)
                    //{
                    if (constant.IsElementPresent(constant.ByViewMore(constant.Count)))
                    {
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementToBeClickable(constant.ByViewMore(constant.Count)));

                        //Constant.jse.ExecuteScript();
                        constant.ViewMore(constant.Count).Click();
                        // new Actions(WebDriver).Click(constant.ViewMore).Perform();
                        Console.WriteLine("view more is clicked....");
                    }
                    //      Constant.jse.ExecuteScript("window.scrollBy(0, -400)");
                    constant.DiscountField.Click();
                    //}


                    //   new Actions(WebDriver).Click(constant.DiscountField).Build().Perform();//To click on the input type discount field.

                    //  Constant.jse.ExecuteScript("arguments[0].click()", constant.ProductConfiguration);

                    retriveSmartPriceData.Add(drobj.RetriveDatafromDsaScreen(constant.Count, constant, totalFinancialCalculation, ServiceHeaderText, serviceListPrice,servicModule));

                    // }


                  constant.Count++;

                    Console.WriteLine("........Retrived smart price details from Dsa Screen.........");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                constant.Count = 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void SmartPriceCalculation(TotalFinancialCalculation financialCalculation,List<ServicesModule> servicesModules=null)
        {
            try
            {
                OracleDBConnection obj = new OracleDBConnection();
                
            
                foreach (DSAPageObject item in retriveSmartPriceData)
                {
                   
                    CalculateGuidanceandFinanacials(item, obj, financialCalculation, servicesModules);
                    item.comparisionObjects.CalTotalSellingPrice = (Convert.ToDecimal(item.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));

                    item.comparisionObjects.TotalActualSmartPriceRevenue = Convert.ToDecimal(item.LineItemSmartRevenue.Replace("$", "").Replace(",", ""));
                    item.comparisionObjects.CalListPrice = Convert.ToDecimal(item.TotalListPrice.Replace("$", "").Replace(",", ""));
                    
                    item.comparisionObjects.CalculatedTotalPricingModifier = item.comparisionObjects.TotalActualSmartPriceRevenue / Convert.ToDecimal(item.comparisionObjects.CalTotalSellingPrice);
                    foreach (DSAPageObject accessory in item.Accessories)
                    {
                        CalculateGuidanceandFinanacials(accessory, obj,financialCalculation);
                        item.comparisionObjects.CalTotalSellingPrice += (Convert.ToDecimal(accessory.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));

                        item.comparisionObjects.TotalActualSmartPriceRevenue += Convert.ToDecimal(accessory.LineItemSmartRevenue.Replace("$", "").Replace(",", ""));
                        item.comparisionObjects.CalculatedTotalListpriceSummaryLevel += Convert.ToDecimal(accessory.TotalListPrice.Replace("$", "").Replace(",", ""));
                        item.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;

                    }
                    
                    financialCalculation.CalculatedTotalSellingpriceSummaryLevel += item.comparisionObjects.CalTotalSellingPrice;
                    financialCalculation.CalculatedTotalListpriceSummaryLevel += item.comparisionObjects.CalListPrice;
                    financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel += item.comparisionObjects.TotalActualSmartPriceRevenue;
                    financialCalculation.CalculatedTotalPricingModifierSummaryLevel = financialCalculation.CalculatedTotalActualSmartPriceRevenueSummaryLevel / Convert.ToDecimal(financialCalculation.CalculatedTotalSellingpriceSummaryLevel);

                    

                   
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
                Console.WriteLine("....... SmartPriceCalculation.......");
            


        }

        


        /// <summary>
        /// 
        /// </summary>
        public void CompareSmartPrice(TotalFinancialCalculation totalFinancialCalculation,List<ServicesModule> servicesModules=null)
        {
            SmartPriceCalculation(totalFinancialCalculation, servicesModules);

            foreach (DSAPageObject dsaObjects in retriveSmartPriceData)
            {
                CompareFinancialGuidance(dsaObjects);
                foreach (DSAPageObject compareAccessory in dsaObjects.Accessories)
                {
                    CompareFinancialGuidance(compareAccessory);
                }

                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2)).ToString() == dsaObjects.SummaryPricingModifier.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsTotalPricingModifierMatch = true;
                }
                // Compare List Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == dsaObjects.SummaryTotalListPrice.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                }
                // Compare Selling Price at SummaryLevel.
                if (Math.Abs(Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() == dsaObjects.SummarySellingPrice.Replace("$", "").Replace(",", ""))
                {
                    dsaObjects.comparisionObjects.IsSellingPricMatchSummaryLevel = true;
                }

            }

            Console.WriteLine(".........SmartPrice Gudance Comparision is completed........");
        }
        public void CompareFinancialGuidance(DSAPageObject dsaObjects)
             {
                try 
                {

                    if (dsaObjects.QuoteNumber == dsaObjects.PnrLog.QuoteNumber.ToString() && dsaObjects.SKUNumber == dsaObjects.PnrLog.SKUNum.ToString())
                    {



                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2) - Convert.ToDecimal(dsaObjects.RecommenededSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2).ToString() == dsaObjects.RecommenededSellingPrice.Replace("$", "").Replace(",", ""))
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedSellingPrice ,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2) - Convert.ToDecimal(dsaObjects.CompAnchorSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2).ToString() == dsaObjects.CompAnchorSellingPrice.Replace("$", "").Replace(",", ""))
                        {

                            dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsCompAnchorsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSellingPrice,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2) - Convert.ToDecimal(dsaObjects.FloorSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2).ToString() == dsaObjects.FloorSellingPrice.Replace("$", "").Replace(",", ""))
                        {

                            dsaObjects.comparisionObjects.IsFloorSellingPriceMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsSpRecommendedsellingPriceMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorSellingPrice,";

                        }
                        //Compare SmartPrice_Revenue
                        if (Math.Abs(Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.RecommendedSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences
                            || Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.RecommendedSmartPricRevenue.Replace("$", "").Replace(",", ""))
                        {

                            dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsRecommenededSmartPriceRevenueMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommenededSPRevenue";

                        }

                        if (Math.Abs(Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.CompAnchorSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.CompAnchorSmartPricRevenue.Replace("$", "").Replace(",", ""))
                        {

                            dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorSPRevenue,";

                        }
                        if (Math.Abs(Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.FloorSmartPricRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.FloorSmartPricRevenue.Replace("$", "").Replace(",", ""))
                        {

                            dsaObjects.comparisionObjects.IsFloorSmartPriceRevenueMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsFloorSmartPriceRevenueMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorSPRevenue,";

                        }
                        //Compare Pricing Modifier
                        string CalculatedRecommendedPricingModifier = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier, 2).ToString();
                        if (CalculatedRecommendedPricingModifier.Length == 1)
                        {
                            CalculatedRecommendedPricingModifier = CalculatedRecommendedPricingModifier + ".00";
                        }

                        if (Math.Abs(Convert.ToDecimal(CalculatedRecommendedPricingModifier) - Convert.ToDecimal(dsaObjects.RecommendedPricingModifier)) == Constant.MinimalDifferences || CalculatedRecommendedPricingModifier.ToString() == dsaObjects.RecommendedPricingModifier)
                        {

                            dsaObjects.comparisionObjects.IsRecommendedPricingModifierMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsRecommendedPricingModifierMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "RecommendedPricingModifier,";

                        }
                        string CalculatedCompAnchorPricingModifier = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier, 2).ToString();
                        if (CalculatedCompAnchorPricingModifier.Length == 1)
                        {
                            CalculatedCompAnchorPricingModifier = CalculatedCompAnchorPricingModifier + ".00";
                        }


                        if (Math.Abs(Convert.ToDecimal(CalculatedCompAnchorPricingModifier) - Convert.ToDecimal(dsaObjects.CompAnchorPricingModifier)) == Constant.MinimalDifferences || CalculatedCompAnchorPricingModifier == dsaObjects.CompAnchorPricingModifier)
                        {

                            dsaObjects.comparisionObjects.IsCompAnchorPricingModifierMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsCompAnchorPricingModifierMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "CompAnchorPricingModifier,";

                        }
                        string CalculatedFloorPricingModifier = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier, 2).ToString();
                        if (CalculatedFloorPricingModifier.Length == 1)
                        {
                            CalculatedFloorPricingModifier = CalculatedFloorPricingModifier + ".00";
                        }
                        if (Math.Abs(Convert.ToDecimal(CalculatedFloorPricingModifier) - Convert.ToDecimal(dsaObjects.FloorPricingModifier)) == Constant.MinimalDifferences || CalculatedFloorPricingModifier == dsaObjects.FloorPricingModifier)
                        {

                            dsaObjects.comparisionObjects.IsFloorPricingModifierMatch = true;
                        }
                        else
                        {
                            dsaObjects.comparisionObjects.IsFloorPricingModifierMatch = false;
                            dsaObjects.StatusComments = dsaObjects.StatusComments + "FloorPricingModifier,";

                        }
                        if (Math.Abs(Convert.ToDecimal(dsaObjects.comparisionObjects.CalTotalSellingPrice) - Convert.ToDecimal(dsaObjects.TotalSellingPrice.Replace("$", "").Replace(",", ""))) == Constant.MinimalDifferences || dsaObjects.comparisionObjects.CalTotalSellingPrice.ToString() == dsaObjects.TotalSellingPrice.Replace("$", "").Replace(",", ""))
                        {
                            dsaObjects.comparisionObjects.IsTotalSellingPriceMatch = true;
                        }
                        if (Math.Abs(Math.Round(dsaObjects.CalculatedActualSPCompFinancials.SmartPriceRevenue, 2) - Convert.ToDecimal(dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))) == Convert.ToDecimal(0.01) || Math.Round(dsaObjects.CalculatedActualSPCompFinancials.SmartPriceRevenue, 2).ToString() == dsaObjects.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", ""))
                        {
                            dsaObjects.comparisionObjects.IsActualSmartPriceRevenueMatch = true;
                        }

                        if (Math.Abs(Math.Round(dsaObjects.CalculatedActualSPCompFinancials.PricingModifier, 2) - Convert.ToDecimal(dsaObjects.LineItemSummeryPricingModifier)) == Constant.MinimalDifferences || Math.Round(dsaObjects.CalculatedActualSPCompFinancials.PricingModifier, 2).ToString() == dsaObjects.LineItemSummeryPricingModifier)
                        {
                            dsaObjects.comparisionObjects.IsPricingModifierMatch = true;
                        }
                        //-----------------------------------------------------------At Summary level---------------------------
                        // Compare Pricing Modifier at SummaryLevel.

                        //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalPricingModifier, 2)).ToString() == dsaObjects.SummaryPricingModifier.Replace("$", "").Replace(",", ""))
                        //{
                        //    dsaObjects.comparisionObjects.IsTotalPricingModifierMatch = true;
                        //}
                        //// Compare List Price at SummaryLevel.
                        //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalListpriceSummaryLevel, 2)).ToString() == dsaObjects.SummaryTotalListPrice.Replace("$", "").Replace(",", ""))
                        //{
                        //    dsaObjects.comparisionObjects.IsListPricMatchSummaryLevel = true;
                        //}
                        //// Compare Selling Price at SummaryLevel.
                        //if (Math.Abs(Math.Round(calculatedValue.CalculatedTotalSellingpriceSummaryLevel, 2)).ToString() == dsaObjects.SummarySellingPrice.Replace("$", "").Replace(",", ""))
                        //{
                        //    dsaObjects.comparisionObjects.IsSellingPricMatchSummaryLevel = true;
                        //}

                        //---------------------------------------------------
                        if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.ModifiedRevenue, 2).ToString() == dsaObjects.LineItemSummaryModifiedRevenue.Replace("$","").Replace(",",""))
                        {
                            dsaObjects.comparisionObjects.IsModifiedRevenueMatch = true;
                        }
                        if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.UpsellModifiedRevenue, 2).ToString() == dsaObjects.LineItemSummaryUpsellServicesModifiedRevenue)
                        {
                            dsaObjects.comparisionObjects.IsUpSellServicesModifiedRevenueMatch = true;
                        }
                        if (Math.Round(dsaObjects.CalculatedActualSPCompFinancials.WithServicesIncentive, 2).ToString() == dsaObjects.LineItemSummaryWithServicesIncentive)
                        {
                            dsaObjects.comparisionObjects.IsWithServicesIncentiveMatch = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    dsaObjects.PnrLog.ProductUnitRecommendedgPrice = Math.Round(dsaObjects.PnrLog.ProductUnitRecommendedgPrice, 2);
                    dsaObjects.PnrLog.ProductUnitCompAnchorPrice = Math.Round(dsaObjects.PnrLog.ProductUnitCompAnchorPrice, 2);
                    dsaObjects.PnrLog.ProductUnitFloorPrice = Math.Round(dsaObjects.PnrLog.ProductUnitFloorPrice, 2);
                    dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, 2);
                    dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, 2);
                    dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.SmartPriceRevenue, 2);
                    dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedRecommendedSPCompFinancials.PricingModifier, 2);
                    dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedCompAnchorSPCompFinancials.PricingModifier, 2);
                    dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier = Math.Round(dsaObjects.CalculatedFloorSPCompFinancials.PricingModifier, 2);
                    
                }
            }
        public void CalculateGuidanceandFinanacials(DSAPageObject item, OracleDBConnection odb,TotalFinancialCalculation financialCalculation,List<ServicesModule>servicesModules=null)
        {

            item.PnrLog = odb.RetriveSmartPriceDetails(item.QuoteNumber, item.SKUNumber,item);//pulling from pnr apilog
                                                                                         //calculation
            item.CalculatedActualSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity),
                 item.PnrLog.DiscountedPricePerUnit * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));
            //Below code throwing exception because modified revenue value is not calculated correctly.
            //  item.CalculatedActualSPCompFinancials = SmartPriceFinancialsCalculator.CalculateModifiedServices(item.PnrLog.SmartPriceGuidance, item);

            item.PnrLog.ProductUnitRecommendedgPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final) / 100);

            item.CalculatedRecommendedSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
                  item.PnrLog.ProductUnitRecommendedgPrice);

            item.PnrLog.ProductUnitCompAnchorPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final) / 100);

            item.CalculatedCompAnchorSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
              item.PnrLog.ProductUnitCompAnchorPrice);

            item.PnrLog.ProductUnitFloorPrice = item.PnrLog.ListPricePerUnit - (item.PnrLog.ListPricePerUnit * Convert.ToDecimal(item.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final) / 100);
            item.CalculatedFloorSPCompFinancials = SmartPriceFinancialsCalculator.Calculate(item.PnrLog.SmartPriceGuidance, item.PnrLog.ListPricePerUnit,
                         item.PnrLog.ProductUnitFloorPrice);

        item.CalculatedModifiedServices = SmartPriceFinancialsCalculator.CalculateModifiedServices(item.CalculatedActualSPCompFinancials,Convert.ToDecimal(item.LineItemUnitSellingPrice),item.UpsellServiceSellingPrice ,financialCalculation.CalculatedTotalServicesSellingPrice,item.PnrLog.AppliedPolicies);

            //item.comparisionObjects.CalTotalSellingPrice += (Convert.ToDecimal(item.LineItemUnitSellingPrice) * Convert.ToInt32(item.PnrLog.SmartPriceGuidance.Quantity));

            //item.comparisionObjects.TotalActualSmartPriceRevenue += Convert.ToDecimal(item.LineItemSmartRevenue.Replace("$", "").Replace(",", ""));
            //item.comparisionObjects.CalculatedTotalListpriceSummaryLevel += Convert.ToDecimal(item.TotalListPrice.Replace("$", "").Replace(",", ""));



        }


        public void ExecuteTestCases(TestCase test)
        {

            //foreach (Scenario testCase in TestDataReader._scenarios)
            //{
            //    foreach (TestCase test in testCase.TestCases)
            //    {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            try
            {
                Constant cs = new Constant();
                ResultLogging resultLog = new ResultLogging();
                ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                mainScenario.LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    mainScenario.SearchQuote(ds);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    mainScenario.SearchQuote(ds);

                }


                mainScenario.CopyAndCreateNewQuote();
                mainScenario.RetriveDataFromDSAScreen(totalFinancialCalculation);
                mainScenario.CompareSmartPrice(totalFinancialCalculation);
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation);
                resultLog.ExportToSqlServerDB(ds);

                Console.WriteLine(test.TestCaseTitle);
                //WebDriver.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
        }
       

    }
}



    



        
    


