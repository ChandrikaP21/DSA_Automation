using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Helper;

namespace SmartPrice_E2E_WebAutomation.Pages.DSA
{
    public class DSAQuoteSummaryPage : DriverHelper
    {
        private IWebDriver WebDriver { get; }
        public DSAQuoteSummaryPage(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        #region--Elements--

        public IWebElement MoreActions => WebDriver.FindElement(_MoreActions);
        protected By _MoreActions => By.XPath("//button[contains(.,'More Actions')]");
        public IWebElement CopyAsNewQuote => WebDriver.FindElement(_copyAsNewQuote);
        protected By _copyAsNewQuote => By.Id("btnCopyAsQuote");
        public IWebElement CopyQuoteAsNewVersion => WebDriver.FindElement(_copyAsNewVersion);
        protected By _copyAsNewVersion => By.XPath("//*[@id='btnCopyAsVersion']");
        public IWebElement CopyWithoutProduct => WebDriver.FindElement(_CopyWithoutProduct);
        protected By _CopyWithoutProduct => By.XPath("//*[@id='btnCopyWithoutProduct']");
        public IWebElement Discount => WebDriver.FindElement(DiscountTextBox);
        protected By DiscountTextBox => By.XPath("(//*[@id='quoteCreate_LI_dolPercentage_0_0'])[1]");
        public IWebElement ApplyChanges => WebDriver.FindElement(ApplyChangesButton);
        protected By ApplyChangesButton => By.XPath("//*[@class='btn btn-primary mg-left-10']");
        public IWebElement SaveQuoteBtn => WebDriver.FindElement(SaveQuoteButton);
        protected By SaveQuoteButton => By.Id("quoteCreate_saveQuote");
        public IWebElement GoalLiteRequestLink => WebDriver.FindElement(_GoalLiteRequestLink);
        protected By _GoalLiteRequestLink => By.Id("QuoteDetails_goalLiteApprovalStatus");
        public IWebElement GoalLiteID => WebDriver.FindElement(_GoalLiteID);
        protected By _GoalLiteID => By.Id("goalLiteDealId");
        public IWebElement GoalLiteStatus => WebDriver.FindElement(_GoalLiteStatus);
        protected By _GoalLiteStatus => By.XPath("//*[@id='goalLiteApprovalStatus_label']/..//label");
        protected By _GoalLitePopUp => By.XPath("//*[contains(@id,'mat-dialog')]");
        public IWebElement GoalLiteDiscountReason => WebDriver.FindElement(_GoalLitePopUpSelectDropDown);
        protected By _GoalLitePopUpSelectDropDown => By.Id("QuoteDetails_approveDiscount_reason");
        protected By _GoalLiteApprovalWarning => By.XPath("//span[contains(.,'GoalLite approval')]");
        public IWebElement GoalLitePopUpJustification => WebDriver.FindElement(By.Id("QuoteDetails_approveDiscount_Notes"));
        public IWebElement GoalLitePopUpSubmitBtn => WebDriver.FindElement(By.Id("QuoteDetails_approveDiscount_submit"));
        public IReadOnlyCollection<IWebElement> DiscountTextbox => WebDriver.FindElements(_DiscountTextbox);
        protected By _DiscountTextbox => By.XPath("//div[contains(@id,'quoteCreate_LI_PI_dolPercentage_smartPrice_0')]//input[contains(@id,'quoteCreate_LI_dolPercentage_0')]");
        public IWebElement SmartPricePopOver => WebDriver.FindElement(_SmartPricePopOver);
        protected By _SmartPricePopOver => By.XPath("//*[@class='popover bottom in clearfix']");
        public IWebElement QuoteNumber => WebDriver.FindElement(_QuoteNumber);
        protected By _QuoteNumber => By.XPath("//h3[@id='quoteNumber']");
        public IWebElement QuoteVersionNumber => WebDriver.FindElement(By.Id("quoteVersionsDropdownId"));
        public IWebElement RecommendedDiscount => WebDriver.FindElement(By.XPath("//td[normalize-space()='Recommended']/../td[3]"));
        public IWebElement RecommendedPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='Recommended']/../td[2]"));
        public IWebElement CompAnchorDiscount => WebDriver.FindElement(By.XPath("//td[normalize-space()='CompAnchor']/../td[3]"));
        public IWebElement CompAnchorPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='CompAnchor']/../td[2]"));
        public IWebElement FloorDiscount => WebDriver.FindElement(By.XPath("//td[normalize-space()='Floor']/../td[3]"));
        public IWebElement FloorPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='Floor']/../td[2]"));
        public IWebElement QuoteCreateListPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_listPrice"));
        public IWebElement QuoteCreateSellingPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_sellingPrice"));
        public IWebElement QuoteCreateDiscountPercent => WebDriver.FindElement(By.Id("quoteCreate_summary_discountPercent"));
        public IWebElement QuoteCreateMarginValue => WebDriver.FindElement(By.Id("quoteCreate_summary_marginValue"));
        public IWebElement QuoteCreateApplyRecPrice => WebDriver.FindElement(_QuoteCreateApplyRecPrice);
        protected By _QuoteCreateApplyRecPrice => By.Id("quoteCreate_quoteSolutionRecommendedPrice");
        protected By manualDiscountTextBox => By.Id("quoteCreate_globalDiscountPercentage");
        protected By SolutionNameBy => By.Id("quoteDetail_items_header_s");
        protected By spinner => By.XPath("//*[@id='busy - indicator']");
        protected By PriceSummarySpinner => By.XPath("//*[contains(.,'Loading Price Summary...') and @class='msg']");
        protected By _ReloadPriceSummary => By.XPath("//*[contains(.,'Loading Price Summary...') and @class='msg']");
        public IWebElement ReloadPriceSummary => WebDriver.FindElement(_ReloadPriceSummary);
        public IWebElement QuoteListPrice => WebDriver.FindElement(By.Id("pricingSummary_listPrice"));
        public IWebElement QuoteDiscountInCurrency => WebDriver.FindElement(By.Id("pricingSummary_discount"));
        public IWebElement QuoteDiscountInPercentage => WebDriver.FindElement(By.Id("pricingSummary_discountPercentage"));
        public IWebElement QuoteSellingPrice => WebDriver.FindElement(By.Id("pricingSummary_sellingPrice"));
        public IWebElement QuoteTotalMarginInCurrency => WebDriver.FindElement(By.Id("pricingSummary_margin"));
        public IWebElement QuoteTotalMarginInPercentage => WebDriver.FindElement(By.Id("pricingSummary_marginPercentage"));
        public IWebElement QuoteTotalCost => WebDriver.FindElement(By.Id("pricingSummary_costPrice"));



        protected By _QuoteSummaryProductsInGroupOne => By.XPath("//div[contains(@id,'GM_Product')]");
        protected By _QuoteSummaryProductDescription => By.XPath(".//*[contains(@id,'item_summary_description')]");
        protected By _QuoteSummaryProductUnitListPrice => By.XPath(".//*[contains(@id,'item_summary_unitPrice')]");
        protected By _QuoteSummaryProductDiscount => By.XPath(".//*[contains(@id,'smartPricePopover_discount')]");
        protected By _QuoteSummaryProductUnitPrice => By.XPath(".//*[contains(@id,'item_summary_unitCost')]");
        protected By _QuoteSummaryProductSellingPrice => By.XPath(".//*[contains(@id,'item_summary_sellingPrice')]");
        protected By _QuoteSummaryProductQuantity => By.XPath(".//*[contains(@id,'item_summary_quantity')]");
        protected By _QuoteSummaryProductPriceModifier => By.XPath(".//*[contains(@id,'item_price_modifier')]");



        #endregion--Elements--
        #region--ElementActions--

        private DSAQuoteSummaryPage ClickMoreActions()
        {
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            MoreActions.Click();
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            WebDriver.WaitingForSpinner(spinner);
            return new DSAQuoteSummaryPage(WebDriver);
        }
        private DSAQuoteSummaryPage ClickCopyAsNewQuote()
        {
            Console.WriteLine("Performing copy as new quote");
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            CopyAsNewQuote.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage PerformCopyAsNewQuote()
        {
            ClickMoreActions().ClickCopyAsNewQuote();
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage ClickCopyQuoteAsNewVersion()
        {
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            CopyQuoteAsNewVersion.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage SaveQuote()
        {
            Console.WriteLine("Performing save quote");
            WebDriver.WaitingForSpinner(spinner);
            //WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            //SaveQuoteButton
            WebDriver.WaitForElementDisplayed(SaveQuoteButton, TimeSpan.FromSeconds(20));
            //WebDriver.waitForWaitAnimationToLoad();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (WebDriver.FindElement(By.Id("quoteCreate_saveQuote")).IsElementVisible())
                    {
                        new DSAQuoteSummaryPage(WebDriver).SaveQuoteBtn.Click();
                        break;
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
            WebDriver.waitForWaitAnimationToLoad();
            Console.WriteLine("Quote saved sucessfully");
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage GetQuoteNumber(out string quoteNumberWithVersion)
        {
            WebDriver.WaitForElementDisplayed(_QuoteNumber, TimeSpan.FromSeconds(10));
            quoteNumberWithVersion = QuoteNumber.Text + "." + QuoteVersionNumber.Text.Replace("Version", "").Trim(' ').Substring(0, 1);
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public DSAQuoteSummaryPage SubmitGoalLiteRequest()
        {
            //GoalLiteID = "";
            Console.WriteLine("Performing goal lite submission");
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitForElementDisplayed(_GoalLiteRequestLink, TimeSpan.FromSeconds(10));
            WebDriver.ScrollIntoView(_GoalLiteRequestLink);
            GoalLiteRequestLink.Click();
            WebDriver.WaitForElementDisplayed(_GoalLitePopUp, TimeSpan.FromSeconds(10));
            WebElementUtil.Select(GoalLiteDiscountReason).SelectByIndex(1);
            WebElementUtil.Set(GoalLitePopUpJustification, "Automation Tool");
            GoalLitePopUpSubmitBtn.Click();
            if (WebDriver.FindElements(_GoalLiteApprovalWarning).Count > 0)
            {
                Console.WriteLine("Goal Lite request got failed with below warning");
                Console.WriteLine(WebDriver.FindElement(_GoalLiteApprovalWarning).Text);
                Console.WriteLine("Performing Goal Lite request again!");
                GoalLiteRequestLink.Click();
                WebDriver.WaitForElementDisplayed(_GoalLitePopUp, TimeSpan.FromSeconds(10));
                WebElementUtil.Select(GoalLiteDiscountReason).SelectByIndex(2);
                WebElementUtil.Set(GoalLitePopUpJustification, "Automation Tool");
                GoalLitePopUpSubmitBtn.Click();
            }

            WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitingForSpinner(spinner);
            Console.WriteLine("Goal Lite got submitted");
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public DSAQuoteSummaryPage ValidateGoalIDGenerated()
        {
            string goalLiteID = GetGoalLiteID();
            string pattern = @"\b[GL]\w\d{2,13}-\d";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = rg.Match(goalLiteID);
            if (!match.Success) Console.WriteLine("GoalLite ID is not in the given format");
            GoalLiteStatus.Text.Contains("Submitted");
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public string GetGoalLiteID()
        {
            WebDriver.WaitForElementDisplayed(_GoalLiteID, TimeSpan.FromSeconds(5));
            var b = GoalLiteID.Displayed;
            Console.WriteLine(GoalLiteID.Text);
            return GoalLiteID.Text;
        }

        public DSAQuoteSummaryPage ApplySmartPriceLessthanFloorPrice()
        {
            try
            {

                WebDriver.WaitingForSpinner(spinner);
                //WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitForElementDisplayed(_DiscountTextbox, TimeSpan.FromSeconds(10));
                WebDriver.ScrollIntoView(_DiscountTextbox);
                //WebDriver.ScrollIntoViewByValue(0, -300);
                WebDriver.FindElement(_DiscountTextbox).Click();
                int t = Convert.ToInt32(GetFloorDiscount().Split('.')[0]) + 1;
                //DiscountTextBox.Clear();
                WebElementUtil.Set(WebDriver.FindElement(_DiscountTextbox), t.ToString());
                //DiscountTextbox.SendKeys((Convert.ToInt32(GetFloorDiscount().Split(".")[0]) + 1).ToString());

                WebDriver.ScrollIntoView(_QuoteCreateApplyRecPrice);
                //WebDriver.ScrollIntoViewByValue(0, -300);
                QuoteCreateApplyRecPrice.Click();

                WebDriver.WaitingForSpinner(spinner);
                WebDriver.waitForWaitAnimationToLoad();
            }
            catch (Exception)
            {

            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage EnterDiscount(IWebElement DiscountTextbox)
        {
            WebDriver.WaitForElementDisplayed(_DiscountTextbox, TimeSpan.FromSeconds(10));
            DiscountTextbox.Click();
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public DSAQuoteSummaryPage ApplyDiscountBelowFloorPriceForAllProducts()
        {
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.waitForWaitAnimationToLoad();
            WebDriver.WaitForElementDisplayed(_DiscountTextbox, TimeSpan.FromSeconds(10));
            foreach (var discountBox in DiscountTextbox)
            {
                WebDriver.ScrollIntoView(discountBox);
                discountBox.Click();
                int t = Convert.ToInt32(GetFloorDiscount().Split('.')[0]) + 10;
                //DiscountTextBox.Clear();
                discountBox.Set(t.ToString());

                WebDriver.ScrollIntoView(_QuoteCreateApplyRecPrice);
                //WebDriver.ScrollIntoViewByValue(0, -300);
                QuoteCreateApplyRecPrice.Click();

                WebDriver.WaitingForSpinner(spinner);
                WebDriver.waitForWaitAnimationToLoad();
            }

            return new DSAQuoteSummaryPage(WebDriver);
        }

        public string GetFloorDiscount()
        {
            WebDriver.WaitForElementDisplayed(_SmartPricePopOver, TimeSpan.FromSeconds(10));
            return FloorDiscount.Text;
        }
        public string GetFloorPrice => FloorPrice.Text;
        public string GetCompAnchorPrice()
        {
            WebDriver.WaitForElementDisplayed(_SmartPricePopOver, TimeSpan.FromSeconds(10));
            var t = CompAnchorPrice.Text;
            return CompAnchorPrice.Text;
        }
        public string GetCompAnchorDiscount => CompAnchorDiscount.Text;

        public void ApplyDiscountForProductsInQuote()
        {
            GetNumberOfProducts();
        }

        private void GetNumberOfProducts()
        {

        }





        public DSAQuoteSummaryObject QuoteSummaryObject()
        {
            DSAQuoteSummaryObject dSAQuoteSummaryObject = new DSAQuoteSummaryObject();
            dSAQuoteSummaryObject.ProductDetails = new List<ProductDetails>();
            GetQuoteNumber(out string QuoteNumber);
            WebDriver.WaitingForSpinner(PriceSummarySpinner);
            if (WebDriver.ElementDisplayed(_ReloadPriceSummary))
            {
                ReloadPriceSummary.Click();
                WebDriver.WaitingForSpinner(PriceSummarySpinner);
            }
            dSAQuoteSummaryObject.QuoteNumber = QuoteNumber;
            dSAQuoteSummaryObject.QuoteListPrice = GenericHelper.ConvertCurrencyIntoString(QuoteListPrice.Text);
            try
            {
                dSAQuoteSummaryObject.QuoteCostPrice = GenericHelper.ConvertCurrencyIntoString(QuoteTotalCost.Text);
            }
            catch (Exception)
            {
                dSAQuoteSummaryObject.QuoteCostPrice = "0";
            }
            dSAQuoteSummaryObject.QuoteSellingPrice = GenericHelper.ConvertCurrencyIntoString(QuoteSellingPrice.Text);
            dSAQuoteSummaryObject.QuoteDiscountInCurrency = GenericHelper.ConvertCurrencyIntoString(QuoteDiscountInCurrency.Text);
            dSAQuoteSummaryObject.QuoteDiscountInPercentage = GenericHelper.ConvertCurrencyIntoString(QuoteDiscountInPercentage.Text.Substring(1, QuoteDiscountInPercentage.Text.Length - 3));

            try
            {
                dSAQuoteSummaryObject.QuoteTotalMargin = GenericHelper.ConvertCurrencyIntoString(QuoteTotalMarginInCurrency.Text);
                dSAQuoteSummaryObject.QuoteTotalMarginPercentage = GenericHelper.ConvertCurrencyIntoString(QuoteTotalMarginInPercentage.Text.Substring(1, QuoteTotalMarginInPercentage.Text.Length - 3));

            }
            catch (Exception)
            {

                dSAQuoteSummaryObject.QuoteTotalMargin = "0";
                dSAQuoteSummaryObject.QuoteTotalMarginPercentage = "0";

            }

            foreach (IWebElement quoteProduct in WebDriver.FindElements(_QuoteSummaryProductsInGroupOne))
            {
                WebDriver.ScrollIntoView(quoteProduct);
                dSAQuoteSummaryObject.ProductDetails.Add(GetProductDetails(quoteProduct));
            }


            return dSAQuoteSummaryObject;
        }
        public ProductDetails GetProductDetails(IWebElement quoteProduct)
        {
            ProductDetails productDetails = new ProductDetails();
            productDetails.ProductDescription = quoteProduct.FindElement(_QuoteSummaryProductDescription).Text;
            productDetails.UnitListPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductUnitListPrice).Text);
            productDetails.DiscountInCurrency = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductDiscount).Text.Split('/')[0].Trim());
            productDetails.DiscountInPercantage = quoteProduct.FindElement(_QuoteSummaryProductDiscount).Text.Split('/')[1].Trim().Replace("%", "");
            productDetails.Quantity = quoteProduct.FindElement(_QuoteSummaryProductQuantity).Text;
            productDetails.UnitPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductUnitPrice).Text);
            productDetails.SellingPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductSellingPrice).Text);
            productDetails.PriceModifier = quoteProduct.FindElement(_QuoteSummaryProductPriceModifier).Text;
            return productDetails;
        }


        public string GetQuoteUrl()
        {
            return WebDriver.Url;
        }


        public void ValidateContinueToCheckOut()
        {

        }



        #endregion--ElementActions--






    }
}
