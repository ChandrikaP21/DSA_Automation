using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using OpenQA.Selenium.Chrome;

namespace EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA
{
    public class DSAQuoteSummaryPage : DriverHelper
    {
        private IWebDriver WebDriver { get; }
        public DSAQuoteSummaryPage(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        #region--Elements--

        public IWebElement MoreActions => WebDriver.FindElement(_MoreActions);
        protected By _MoreActions => By.XPath("//button[contains(.,'More Actions')]");
        public By ByCheckContinueToCheckOutButton(string flag) => By.XPath("//button[@id='continueToCheckoutButton' and @aria-disabled='"+ flag + "']");
        public IWebElement CheckContinueToCheckOutButton(string flag) => WebDriver.FindElement(ByCheckContinueToCheckOutButton(flag));
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

        protected By ByDAMThreshholdMessage(string text) => By.XPath("//*[contains(text(),'"+text+"')]");
        public IWebElement DAMThreshholdMessage(string text) => WebDriver.FindElement(ByDAMThreshholdMessage(text));
        public IWebElement SaveQuoteBtn => WebDriver.FindElement(SaveQuoteButton);
        protected By SaveQuoteButton => By.Id("quoteCreate_saveQuote");
        public IWebElement GoalLiteRequestLink => WebDriver.FindElement(_GoalLiteRequestLink);
        protected By _GoalLiteRequestLink => By.Id("QuoteDetails_goalLiteApprovalStatus");
        public IWebElement GoalRequestLink => WebDriver.FindElement(_GoalRequestLink);
        protected By _GoalRequestLink => By.XPath("//goal-deal//*[contains(text(),' GOAL Deal ID ')]/following::*[contains(text(),'New Request')]");
        public IWebElement GoalLiteID => WebDriver.FindElement(_GoalLiteID);
        //protected By _GoalLiteID => By.Id("workspace_lastBC");
        protected By _GoalLiteID => By.Id("goalLiteDealId");//Xpath updated , in g2 xpath changed
     
        public IWebElement GoalID => WebDriver.FindElement(_GoalID);
        protected By _GoalID => By.Id("goalLiteDealId");

        protected By _pickFromList => By.LinkText("Pick from list");
        public IWebElement PickFromList => WebDriver.FindElement(_pickFromList);

        protected By _selectapprovedGoalId(string goalId) => By.XPath("//*[text()='" + goalId + "']/../div//button[contains(text(),' Select ')]");
        public IWebElement SelectApproveGoalId(string goalId) => WebDriver.FindElement(_selectapprovedGoalId(goalId));
        //*[contains(text(),'G0059327492-1')]
        protected By _goalIdInDsa(string goalId) => By.XPath("//*[contains(text(),'" + goalId + "')]");
        public IWebElement GoalIdInDsa(string goalId) => WebDriver.FindElement(_goalIdInDsa(goalId));

        public By _selectGoalDeal => By.XPath("//*[contains(@id,'element_0')]/div/div/goal-deal-ag-data-grid/div/ag-grid-angular/div/div[2]/div[2]/div[3]/div[2]/div");
        public IWebElement SelectGoalDeal => WebDriver.FindElement(_selectGoalDeal);
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
        protected By _SmartPricePopOver => By.XPath("//*[@class='dds__popover dds__popover--bottom']");
        public IWebElement QuoteNumber => WebDriver.FindElement(_QuoteNumber);
        protected By _QuoteNumber => By.XPath("//*[@id='quoteNumber']");
    //    public IWebElement QuoteVersionNumber => WebDriver.FindElement(By.Id("quoteVersionsDropdownId"));
       
             public IWebElement QuoteVersionNumber => WebDriver.FindElement(By.Id("quoteVersionsBtn"));
        public IWebElement RecommendedDiscount => WebDriver.FindElement(By.XPath("//td[normalize-space()='Recommended']/../td[3]"));
        public IWebElement RecommendedPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='Recommended']/../td[2]"));
        public IWebElement CompAnchorDiscount => WebDriver.FindElement(By.XPath("//td[normalize-space()='CompAnchor']/../td[3]"));
        public IWebElement CompAnchorPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='CompAnchor']/../td[2]"));
        public IWebElement FloorDiscount => WebDriver.FindElement(By.XPath("//smart-price-guidance//table/tbody/tr[3]/td[3]"));
        public IWebElement FloorPrice => WebDriver.FindElement(By.XPath("//td[normalize-space()='Floor']/../td[2]"));
        public IWebElement QuoteCreateListPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_listPrice"));
        public IWebElement QuoteCreateSellingPrice => WebDriver.FindElement(By.Id("quoteCreate_summary_sellingPrice"));
        public IWebElement QuoteCreateDiscountPercent => WebDriver.FindElement(By.Id("quoteCreate_summary_discountPercent"));
        public IWebElement QuoteCreateMarginValue => WebDriver.FindElement(By.Id("quoteCreate_summary_marginValue"));
        public IWebElement QuoteCreateApplyRecPrice => WebDriver.FindElement(_QuoteCreateApplyRecPrice);
        protected By _QuoteCreateApplyRecPrice => By.Id("quoteCreate_quoteSolutionRecommendedPrice");
        protected By manualDiscountTextBox => By.Id("quoteCreate_globalDiscountPercentage");
        protected By SolutionNameBy => By.Id("quoteDetail_items_header_s");
        public By spinner => By.XPath("//*[@id='busy - indicator']");
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
        protected By _QuoteSummaryProductDiscount => By.XPath(".//*[contains(@id,'trigger_item_summary_discount')]");
        protected By _QuoteSummaryProductUnitPrice => By.XPath(".//*[contains(@id,'item_summary_unitCost')]");
        protected By _QuoteSummaryProductSellingPrice => By.XPath(".//*[contains(@id,'item_summary_sellingPrice')]");
        protected By _QuoteSummaryProductQuantity => By.XPath(".//*[contains(@id,'item_summary_quantity')]");
        protected By _QuoteSummaryProductPriceModifier => By.XPath(".//*[contains(@id,'item_price_modifier')]");



        #endregion--Elements--
        #region--ElementActions--

        private DSAQuoteSummaryPage ClickMoreActions()
        {
            try
                {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(30));
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementIsVisible(_MoreActions));
                MoreActions.Click();
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(10));
                //      WebDriver.WaitingForSpinner(spinner);
           }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException("Unable to click on ClickMoreActions",ex);
                throw exception;

            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        private DSAQuoteSummaryPage ClickCopyAsNewQuote()
        {
            try
            {
              
              
                Console.WriteLine("Performing copy as new quote");
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(15));
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(_copyAsNewQuote));
                CopyAsNewQuote.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(15));
            }
            catch(Exception ex)
            {
                Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException("copy as a new quote", ex);
                throw exception;
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage PerformCopyAsNewQuote()
        {
            try
            {
                ClickMoreActions().ClickCopyAsNewQuote();
                return new DSAQuoteSummaryPage(WebDriver);
            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException exception)
            {
                throw exception;
            }
        }
        public DSAQuoteSummaryPage ClickCopyQuoteAsNewVersion()
        {
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            CopyQuoteAsNewVersion.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage RemoveSFDCDealId()
        {
            Constant locator = new Constant(WebDriver);

           
            SFDCChangeClass sfdcDealIdCheck = new SFDCChangeClass(WebDriver);
            if (locator.IsElementPresent(sfdcDealIdCheck.BySfdcDealIdElement))
            {
                try
                {
                    if (!string.IsNullOrEmpty(sfdcDealIdCheck.SfdcDealIdElement.GetAttribute("value")))
                    {
                        WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                        sfdcDealIdCheck.SfdcDealIdElement.Clear();
                        WebDriver.WaitingForSpinner(spinner);
                        WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                    }
                }
                catch (Exception ex)
                {

                    throw new ShowStopperException("Unable to remove SFDC deal id, refer  RemoveSFDCDealId()  in DSAQuoteSummaryPage class", ex);
                }
            }
          
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage GetFloorOrDAMThresholdMessage()
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(1));
                WebDriver.WaitForElementDisplayed(ByDAMThreshholdMessage(Constant.DAMThreshhold), TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(DAMThreshholdMessage(Constant.DAMThreshhold).GetAttribute("innerText"),true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage SaveQuote()
        {
            try
            {
                Console.WriteLine("Performing save quote");
                WebDriver.WaitingForSpinner(spinner);
                //WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                //SaveQuoteButton
                WebDriver.WaitForElementDisplayed(SaveQuoteButton, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                //WebDriver.waitForWaitAnimationToLoad();
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        if (WebDriver.FindElement(By.Id("quoteCreate_saveQuote")).IsElementVisible())
                        {
                            new DSAQuoteSummaryPage(WebDriver).SaveQuoteBtn.Click();
                            Thread.Sleep(3000);
                            break;
                        }
                    }
                    catch
                    {
                        Thread.Sleep(2000);
                    }
                }
                WebDriver.waitForWaitAnimationToLoad();
                Console.WriteLine("Quote saved sucessfully");
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Unable to save quote , please chcek SaveQuote() in DSAQuoteSummaryPage", ex);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage GetQuoteNumber(out string quoteNumberWithVersion)
        {
            quoteNumberWithVersion = "";
            try
            {

                WebDriver.WaitForElementDisplayed(_QuoteNumber, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                quoteNumberWithVersion = QuoteNumber.Text + "." + QuoteVersionNumber.Text.Replace("Version", "").Trim(' ').Substring(0, 1);
                
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                throw new ShowStopperException("Unable to submit GoalLite request", ex);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage SubmitGoalRequest(string scenarioId = "", string title = "")
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                //GoalLiteID = "";
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Performing goal submission",true);
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitForElementDisplayed(_GoalRequestLink, TimeSpan.FromSeconds(10));
                WebDriver.ScrollIntoView(_GoalRequestLink);
                GoalRequestLink.Click();
               
               
                WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitingForSpinner(spinner);
                Console.WriteLine("Goal got submitted");
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                throw new ShowStopperException("Unable to submit GoalLite request", ex);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage SubmitGoalLiteRequest(string scenarioId="",string title="")
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                //GoalLiteID = "";
                Console.WriteLine("Performing goal lite submission");
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitForElementDisplayed(_GoalLiteRequestLink, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                WebDriver.ScrollIntoView(_GoalLiteRequestLink);
                GoalLiteRequestLink.Click();
                WebDriver.WaitForElementDisplayed(_GoalLitePopUp, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                WebElementUtil.Select(GoalLiteDiscountReason).SelectByIndex(2);
                WebElementUtil.Set(GoalLitePopUpJustification, constant.GoalBusinessCasetext(scenarioId, title));
                GoalLitePopUpSubmitBtn.Click();
                if (WebDriver.FindElements(_GoalLiteApprovalWarning).Count > 0)
                {
                    Console.WriteLine("Goal Lite request got failed with below warning");
                    Console.WriteLine(WebDriver.FindElement(_GoalLiteApprovalWarning).Text);
                    Console.WriteLine("Performing Goal Lite request again!");
                    GoalLiteRequestLink.Click();
                    WebDriver.WaitForElementDisplayed(_GoalLitePopUp, TimeSpan.FromSeconds(10));
                    WebElementUtil.Select(GoalLiteDiscountReason).SelectByIndex(2);
                    WebElementUtil.Set(GoalLitePopUpJustification, constant.GoalBusinessCasetext(scenarioId, title));
                    GoalLitePopUpSubmitBtn.Click();
                }

                WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitingForSpinner(spinner);
                Console.WriteLine("Goal Lite got submitted");
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Unable to SubmitGoalLiteRequest,please referSubmitGoalLiteRequest() in DSAQuoteSummaryPage class ", ex);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public DSAQuoteSummaryPage ValidateGoalLiteIDGenerated()
        {
            try
            {
                string goalLiteID = GetGoalLiteID();
                string pattern = @"\b[GL]\w\d{2,13}-\d";
                Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = rg.Match(goalLiteID);
                if (!match.Success) Console.WriteLine("GoalLite ID is not in the given format");
                GoalLiteStatus.Text.Contains("Submitted");
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Inavlid goalLiteId, please refer ValidateGoalLiteIDGenerated() in DSAQuoteSummaryPage class  ", ex);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public DSAQuoteSummaryPage ValidateGoalIDGenerated()
        {
            string goalID = GetGoalId();
            string pattern = @"\b[G]\w\d{2,13}-\d";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = rg.Match(goalID);
            if (!match.Success) Console.WriteLine("GoalLite ID is not in the given format");
            //GoalLiteStatus.Text.Contains("Submitted");
            return new DSAQuoteSummaryPage(WebDriver);
        }
        /// <summary>
        /// Edit goal and change all the mandate fields.
        /// </summary>
        /// <returns></returns>
       

        public string GetGoalLiteID()
        {   

            WebDriver.WaitForElementDisplayed(_GoalLiteID, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            var b = GoalLiteID.Displayed;
            Console.WriteLine(GoalLiteID.Text);
            return GoalLiteID.Text;
        }
        public void NavigateWindow(int index)
        {
            List<string> list = new List<string>(WebDriver.WindowHandles);
            WebDriver.SwitchTo().Window(list[index]);
            Thread.Sleep(2000);
        }
        public string GetGoalId()
        {
            string goalId = "";
            try
            {
                goalId = GoalLiteID.Text;
                Thread.Sleep(4000);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(GoalLiteID.Text, true);
                //      Console.WriteLine(GoalID.Text);
                
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(ex.Message,true);
                throw new ShowStopperException("Could not get Goal Id,Please check GetGoalId(), class-DSAQuoteSummaryPage", ex);
            }
            return goalId;
        }
        public DSAQuoteSummaryPage GoalPickFromList()
        {
            try
            {
                PickFromList.Click();
                
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            return new DSAQuoteSummaryPage(WebDriver);

        }
        public DSAQuoteSummaryPage ValidateQuoteExpiryDate()
        {
            DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
            
            return new DSAQuoteSummaryPage(WebDriver);

        }
        public DSAQuoteSummaryPage GoalSelectApproveGoalId(string goalId)
        {
            try
            {
                Constant obj = new Constant(WebDriver);
                if (obj.IsElementPresent(_selectGoalDeal))
                {
                    if (obj.IsElementPresent(_selectapprovedGoalId(goalId)))
                    {
                        SelectApproveGoalId(goalId).Click();
                    }
                }
            }
            catch (Exception ex)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            return new DSAQuoteSummaryPage(WebDriver);
        }
        public void PickGoalIdFromList(string goalId,string dsaQuantity)
        {
            try
            {
                if (!string.IsNullOrEmpty(TestDataReader.MaxQuantity))
                {
                    GoalPickFromList();
                    GoalSelectApproveGoalId(goalId);
                }
                 else if(Convert.ToInt32(TestDataReader.MaxQuantity)<Convert.ToInt32(dsaQuantity))
                {
                    GoalPickFromList();
                    GoalSelectApproveGoalId(goalId);
                }
                else if (Convert.ToInt32(TestDataReader.MaxQuantity)>Convert.ToInt32(dsaQuantity))
                {   
                    GoalPickFromList();
                    try
                    {
                        Constant cs = new Constant(WebDriver);
                        WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_selectGoalDeal));
                    }
                    catch(Exception ex)
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Max Quantity is higher than dsa quantity, Unable to add goal Id ",true);
                    }
                }


            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
        }
        public bool IsGoalDealIdAttached(string goalId)
        {
            try
            {
                Constant obj = new Constant(WebDriver);
                if (obj.IsElementPresent(_goalIdInDsa(goalId)))
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("GoalId Is attached to the product",true);
                    return true;
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("GoalId is not attached to the product", true);
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                return false;
            }
            
        }
        public DSAQuoteSummaryPage ApplySmartPriceLessthanFloorPrice()
        {
            try
            {

                WebDriver.WaitingForSpinner(spinner);
                //WebDriver.waitForWaitAnimationToLoad();
                WebDriver.WaitForElementDisplayed(_DiscountTextbox, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                WebDriver.ScrollIntoView(_DiscountTextbox);
                //WebDriver.ScrollIntoViewByValue(0, -300);
                WebDriver.FindElement(_DiscountTextbox).Click();
                int t = Convert.ToInt32(GetFloorDiscount().Split('.')[0]) + 1;
                //DiscountTextBox.Clear();
                WebElementUtil.Set(WebDriver.FindElement(_DiscountTextbox), t.ToString());
                DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                WebDriver.ScrollIntoView(By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_0']"));
                //WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_" + count + "']"));
                WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_0']")).Click();
                //DiscountTextbox.SendKeys((Convert.ToInt32(GetFloorDiscount().Split(".")[0]) + 1).ToString());

                //WebDriver.ScrollIntoView(_QuoteCreateApplyRecPrice);
                ////WebDriver.ScrollIntoViewByValue(0, -300);
                //QuoteCreateApplyRecPrice.Click();

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
            WebDriver.WaitForElementDisplayed(_DiscountTextbox, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            DiscountTextbox.Click();
            return new DSAQuoteSummaryPage(WebDriver);
        }

        public DSAQuoteSummaryPage ApplyDiscountBelowFloorPriceForAllProducts()
        {
            try
            {
                
                
                //WebDriver.WaitingForSpinner(spinner);
                //WebDriver.waitForWaitAnimationToLoad();
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
               //     QuoteCreateApplyRecPrice.Click();

                    WebDriver.WaitingForSpinner(spinner);
                    WebDriver.waitForWaitAnimationToLoad();
                }
            }
            catch(Exception ex)
            {
               throw new ShowStopperException("Unable to apply discount, Please refer ApplyDiscountBelowFloorPriceForAllProducts(), in DSAQuoteSummaryPageClass", ex);
            }

            return new DSAQuoteSummaryPage(WebDriver);
        }

        public string GetFloorDiscount()
        {
            WebDriver.WaitForElementDisplayed(_SmartPricePopOver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            return FloorDiscount.Text;
        }
        public string GetFloorPrice => FloorPrice.Text;
        public string GetCompAnchorPrice()
        {
            WebDriver.WaitForElementDisplayed(_SmartPricePopOver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
            DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
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
            dSAQuoteSummaryObject.DsaQuantity=drobj.GetQuantity(0);
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
            try
            {
               
                productDetails.ProductDescription = quoteProduct.FindElement(_QuoteSummaryProductDescription).Text;
                productDetails.UnitListPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductUnitListPrice).Text);
                productDetails.DiscountInCurrency = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductDiscount).Text.Split('/')[0].Trim());
                productDetails.DiscountInPercantage = quoteProduct.FindElement(_QuoteSummaryProductDiscount).Text.Split('/')[1].Trim().Replace("%", "");
                productDetails.Quantity = quoteProduct.FindElement(_QuoteSummaryProductQuantity).Text;
                productDetails.UnitPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductUnitPrice).Text);
                productDetails.SellingPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(_QuoteSummaryProductSellingPrice).Text);
                productDetails.PriceModifier = quoteProduct.FindElement(_QuoteSummaryProductPriceModifier).Text;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productDetails;
        }


        public string GetQuoteUrl()
        {
            return WebDriver.Url;
        }


        public void ValidateContinueToCheckOut(string flag,string flagstatus)
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                if (constant.IsElementPresent(ByCheckContinueToCheckOutButton(flag)))
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Continue to CheckOut Button :" + flagstatus, true);

                else
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Continue to CheckOut Button :" + flagstatus, true);
            }
           
            catch (Exception ex)
            {
                throw new ShowStopperException("Unable to Validate ContinueToCheckOut button", ex);
            }

            

        }



        #endregion--ElementActions--






    }
}
