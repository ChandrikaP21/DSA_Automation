using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Pages.DSA;
using SmartPrice_E2E_WebAutomation.Helper;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace SmartPrice_E2E_WebAutomation.Pages.GOAL
{
    public class GoalHomePage : DriverHelper
    {
        private IWebDriver WebDriver { get; }
        public GoalHomePage(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        #region -- Elements --
        public IWebElement ManageDeals => WebDriver.FindElement(_ManageDeals);
        protected By _ManageDeals => By.XPath("//a[@title='Manage Deals']");
        public IWebElement ManagePolicies => WebDriver.FindElement(_ManagePolicies);
        protected By _ManagePolicies => By.XPath("//a[@title='Manage Policies']");
        public IWebElement Administration => WebDriver.FindElement(_Administration);
        protected By _Administration => By.XPath("//a[@title='Administration']");
        public IWebElement GOAL_LiteFolder => WebDriver.FindElement(By.XPath("//*[@title='GOAL Lite']"));
        public IWebElement GOAL_LiteFolderExpand => WebDriver.FindElement(By.XPath("//*[@title='GOAL Lite']/../../a[1]"));
        public IWebElement AllQuotesFolder => WebDriver.FindElement(By.XPath("//*[@title='All Quotes']"));
        public IWebElement AllQuotesFolderExpand => WebDriver.FindElement(By.XPath("//*[@title='All Quotes']/../../a[1]"));
        public By spinner => By.Id("MainStatus");
        public By spinner2 => By.Id("MainStatus_TEXT");//Initializing table...
        #region -- GoalLite details --
        public IWebElement EditBtn => WebDriver.FindElement(By.Id("editButton"));
        public IWebElement CloseBtn => WebDriver.FindElement(By.Id("closeButton"));
        public IWebElement ApproveBtn => WebDriver.FindElement(_approveBtn);
        private By _approveBtn = By.Id("approveButton");
        public IWebElement DenyBtn => WebDriver.FindElement(By.Id("denyButton"));
        private By _denyBtn = By.Id("denyButton");
        public IWebElement WithdrawBtn => WebDriver.FindElement(By.Id("withdrawButton"));
        public IWebElement RefreshBtn => WebDriver.FindElement(By.Id("refreshButton"));
        public IWebElement DealHeaderTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//span[contains(.,'Deal Header')]"));
        public IWebElement QuotesTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//a[contains(@class,'tab WorkspaceTabs_2')]"));
        public IWebElement WorkflowTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//a[contains(@class,'tab WorkspaceTabs_5')]"));
        public IWebElement LOBApprovalLevel1 => WebDriver.FindElement(By.XPath("//*[@id='LOBApprovalLevel1']"));
        public IWebElement WorkflowCondition => WebDriver.FindElement(By.XPath("//*[@id='workflow_VxWorkflow_Map_NODEINFO']"));
        //Deal Header ==> DH
        public IWebElement DH_QuoteNumber => WebDriver.FindElement(By.Id("form1_QuoteNumber"));
        public IWebElement DH_CustomerID => WebDriver.FindElement(By.XPath("//input[contains(@id,'customerdetails_CustomerID')]"));
        public IWebElement DH_ListPrice => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_ListPrice')]"));
        public IWebElement DH_TotalNSP => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_TotalNetSellingPrice')]"));
        public IWebElement DH_TotalNSPinUSD => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_TotalNetSellingPriceUSD')]"));
        public IWebElement DH_TotalDOLinPercentage => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_TotalDOL')]"));
        public IWebElement DH_CombinedDAMQuoteLevel => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_CombinedDAMQuoteLevel')]"));
        public IWebElement DH_ExceedingAmountQuoteLevel => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_ExceedingQuoteLevel')]"));
        public IWebElement DH_QuoteMarginInCurrency => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_Margin')]"));
        public IWebElement DH_QuoteMarginInPercentage => WebDriver.FindElement(By.XPath("//input[contains(@id,'form2_MarginPercent')]"));
        public IWebElement ProductDetailsTable => WebDriver.FindElement(By.XPath("//*[@id='LineItemList']//table[contains(@class,'body')]"));
        //public IWebElement QuotesTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//span[contains(.,'Quotes')]"));
        public IWebElement AttachmentsTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//span[contains(.,'Attachments')]"));
        public IWebElement LOBSummaryTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//span[contains(.,'LOB Summary')]"));
        public IWebElement ChangeHistoryTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//span[contains(.,'Change History')]"));
        public IWebElement BusinessCaseTextArea => WebDriver.FindElement(_textArea);
        private By _textArea = By.XPath("//textarea[contains(@id,'VxApprovableComments')]");
        public IWebElement BusinessCaseTextAreaOkbtn => WebDriver.FindElement(_textAreaOkBtn);
        private By _textAreaOkBtn = By.XPath("//button[normalize-space()='OK']");

        //*[@title='ESG']/../../a[1]
        //*[@title=' GLAmer Approval Matrix']

        //
        public IWebElement WorkflowFolderExpand => WebDriver.FindElement(By.XPath("//*[@title='Workflow']/../../a[1]"));
        public IWebElement GLAmerApprovalMatrix => WebDriver.FindElement(By.XPath("//*[@title=' GLAmer Approval Matrix']"));
        public IWebElement GLAmerApprovalAssignments => WebDriver.FindElement(By.XPath("//*[@title='GLAmer Approval Assignments']"));
        public IWebElement GLAmerAutoApprovalMatrix => WebDriver.FindElement(By.XPath("//*[@title='GL AutoApproval Matrix']"));
        public IWebElement GLAmerAutoDenyMatrix => WebDriver.FindElement(By.XPath("//*[@title='GLAutoDeny Matrix']"));
        public IWebElement GLAmerApprovalMatrix_NewBtn => WebDriver.FindElement(By.XPath("//button[@id='newButton']"));
        public IWebElement AccountGroupName => WebDriver.FindElement(By.XPath("//input[@id='DISPLAY_AccountGroupID']"));
        public IWebElement ProductHierarchySearch => WebDriver.FindElement(By.XPath("//img[@id='id2']"));
        public IWebElement ProductHierarchySearchInAssignment => WebDriver.FindElement(By.XPath("//img[@id='id3']"));
        public IWebElement iframe => WebDriver.FindElement(By.XPath("//*[@id='PageDialog - content']/iframe"));
        public IWebElement iframeTable => WebDriver.FindElement(By.Name("PageDialog-iframe"));
        public IWebElement iframeTable2 => WebDriver.FindElement(By.Name("content"));
        public IWebElement iframeTable3 => WebDriver.FindElement(By.Name("listFrame"));
        private By _checkbox => By.XPath("//*[@title='ESG']/../../td/input");
        private By _okModalBtn => By.XPath("//button[@id='OKModalButton']");
        private By _approvalLevel1 => By.XPath("//input[@id='DISPLAY_ApprovalLevel1']");
        public IWebElement GL_ApprovalMatrix_SubmitBtn => WebDriver.FindElement(_submitBtn);
        private By _submitBtn => By.XPath("//button[@id='submit2Button']");
        public IWebElement GL_ApprovalMatrix_CommnetTxt => WebDriver.FindElement(_commentTxt);
        private By _commentTxt => By.XPath("//td/textarea[@id='comment']");
        public IWebElement GL_ApprovalMatrix_CommnetOkBtn => WebDriver.FindElement(By.XPath("//button[@id='okButton']"));
        public IWebElement GL_AM_LowerRevenue => WebDriver.FindElement(By.XPath("//*[@id='LowerRevenue']"));
        public IWebElement GL_AM_UpperRevenue => WebDriver.FindElement(By.XPath("//input[@id='UpperRevenue']"));
        public IWebElement GL_AM_LowerMargin => WebDriver.FindElement(By.XPath("//input[@id='LowerMargin']"));
        public IWebElement GL_AM_UpperMargin => WebDriver.FindElement(By.XPath("//input[@id='UpperMargin']"));
        public IWebElement GL_AM_LowerDOL => WebDriver.FindElement(By.XPath("//input[@id='LowerDOL']"));
        private By _GL_AM_LowerDOL => By.XPath("//input[@id='LowerDOL']");
        public IWebElement GL_AM_UpperDOL => WebDriver.FindElement(By.XPath("//input[@id='UpperDOL']"));
        public IWebElement GL_AA_UserNameTxtBox => WebDriver.FindElement(By.XPath("//input[@id='DISPLAY_NameID']"));
        public IWebElement Validity_EndDate => WebDriver.FindElement(By.XPath("//input[@id='VValidity.to']"));
        public IWebElement Validity_StartDate => WebDriver.FindElement(By.XPath("//input[@id='VValidity.from']"));


        #endregion -- GoalLite details --

        #region -- Adminstration tab --

        public IWebElement Admin_SearchBox => WebDriver.FindElement(_Admin_SearchBox);
        protected By _Admin_SearchBox => By.XPath("//input[@id='UserList_inner_SEARCHBOX']");

        public IWebElement UIProfile => WebDriver.FindElement(By.XPath($@"//tr[contains(@id,'UserList_inner')]//td[3][contains(.,'{uiProfile}')]/../td[6]"));

        //tr[contains(@id,'UserList_inner')]//td[3][contains(.,'RAMA_JAYANTHI')]/../td[6]
        string uiProfile = "";
        string loggedInUser = "";


        #endregion
        #region -- AutoApproval tab --

        public IWebElement Admin_SearchBox1 => WebDriver.FindElement(_Admin_SearchBox);
        protected By _Admin_SearchBox1 => By.XPath("//input[@id='UserList_inner_SEARCHBOX']");

        public IWebElement GIIHierarchyID => WebDriver.FindElement(By.XPath("//*[@id='DISPLAY_GIIHierarchyID']"));
        public IWebElement AccountGroupID => WebDriver.FindElement(By.XPath("//input[@id='DISPLAY_AccountGroupID']"));



        #endregion

        #region ApprovalMatrix
        //div[contains(text(),'Created By')]
        public By CreateBy => By.XPath("//div[contains(text(),'Created By')]");
        public By Status => By.XPath("//div[contains(text(),'Status')]");
        public By CreateByFilter => By.XPath("//div[@title='Created By']//div[@class='th-filter sprite sprite-icon_unfiltered']");
        public By StatusFilter => By.XPath("//div[contains(text(),'Status')]");
        public By CreateByFilterTextbox => By.XPath("//input[@id='PolicyTable_GLAmerApprovalThresholds_26_FILTERSELECTOR_SEARCHBOX']");
        public By CreateByFilteredResult => By.XPath($@"//span[@title='{loggedInUser}']/..");
        public By CreateByFilterPopup => By.XPath("//*[@id='PolicyTable_GLAmerApprovalThresholds_FILTER']");
        public By CreateByFilterPopupOkBtn => By.XPath("//tbody//tr//button[1]");
        public By ApproveStatusPolicy => By.XPath("//span[@title='Approved']");

        public IWebElement MassExpire => WebDriver.FindElement(By.XPath("//a[@id='massExpire']"));
        public IWebElement CommentTextArea => WebDriver.FindElement(By.XPath("//textarea[contains(@name,'massedit')]"));
        public IWebElement OkButton => WebDriver.FindElement(By.XPath("//button[@id='applyButton']"));
        public IWebElement ColumnCheck => WebDriver.FindElement(By.XPath("//div[@class='column-manager-icon sprite sprite-column_manager']"));
        ReadOnlyCollection<IWebElement> ColumnElements => WebDriver.FindElements(By.XPath("//ul[@id='column-manager-menu']/li/a"));

        #endregion

        #endregion -- Elements --
        #region -- Actions --
        public void ExpandFolder(IWebElement ele)
        {
            if (ele.GetAttribute("class").Contains("plus")) ele.Click();
        }
        public GoalHomePage CollapseFolder(IWebElement ele)
        {
            if (ele.GetAttribute("class").Contains("minus")) ele.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToManageDeals()
        {
            try
            {
                WebDriver.WaitForElementDisplayed(_ManageDeals, TimeSpan.FromSeconds(10));
                ManageDeals.Click();
            }
            catch (Exception)
            {
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToManagePolicies()
        {
            WebDriver.WaitForElementDisplayed(_ManagePolicies, TimeSpan.FromSeconds(10));
            ManagePolicies.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToAdministration()
        {
            WebDriver.WaitForElementDisplayed(_Administration, TimeSpan.FromSeconds(10));
            Administration.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandAllQuotesInGoalLiteInbox()
        {
            ExpandFolder(GOAL_LiteFolderExpand);
            ExpandFolder(AllQuotesFolderExpand);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage SelectAllQuotesInGoalLiteInbox()
        {
            WebDriver.WaitingForSpinner(spinner);
            ExpandFolder(GOAL_LiteFolderExpand);
            WebDriver.WaitingForSpinner(spinner2);
            AllQuotesFolder.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage OpenGoalLite(string goalLiteId)
        {
            //*[@title='GOAL Deal Id'  and text()='GL000000505149-1']
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            var t = GetGoalLiteIDElement(goalLiteId);
            WebDriver.PerformDoubleClick(GetGoalLiteIDElement(goalLiteId));
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public IWebElement GetGoalLiteIDElement(string goalLiteId)
        {
            var ele = By.XPath($@"//*[@title='GOAL Deal Id'  and text()='{goalLiteId}']");
            WebDriver.WaitForElementDisplayed(ele, TimeSpan.FromSeconds(5));
            return WebDriver.FindElement(ele);
        }
        public GoalObject GetDetailsInGoalPage()
        {
            GoalObject goalObject = new GoalObject();
            goalObject.ProductDetails = new List<ProductDetails>();
            goalObject.QuoteNumber = DH_QuoteNumber.GetAttribute("value").Split('-')[1].Replace("/", ".");
            goalObject.CustomerID = DH_CustomerID.GetAttribute("value");
            goalObject.ListPrice = GenericHelper.ConvertCurrencyIntoString(DH_ListPrice.GetAttribute("value"));
            goalObject.TotalNSP = GenericHelper.ConvertCurrencyIntoString(DH_TotalNSP.GetAttribute("value"));
            goalObject.TotalNSPinUSD = GenericHelper.ConvertCurrencyIntoString(DH_TotalNSPinUSD.GetAttribute("value"));
            goalObject.TotalDOLinPercentage = DH_TotalDOLinPercentage.GetAttribute("value");
            try
            {
                goalObject.CombinedDAMQuoteLevel = GenericHelper.ConvertCurrencyIntoString(DH_CombinedDAMQuoteLevel.GetAttribute("value"));
                goalObject.ExceedingAmountQuoteLevel = GenericHelper.ConvertCurrencyIntoString(DH_ExceedingAmountQuoteLevel.GetAttribute("value"));
                goalObject.QuoteMarginInCurrency = GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInCurrency.GetAttribute("value"));
                goalObject.QuoteMarginInPercentage = GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInPercentage.GetAttribute("value"));
            }
            catch (Exception)
            {
                goalObject.CombinedDAMQuoteLevel = "";
                goalObject.ExceedingAmountQuoteLevel = "";
                goalObject.QuoteMarginInCurrency = "";
                goalObject.QuoteMarginInPercentage = "";
            }
            NavigateToQuotesTab();
            foreach (IWebElement quoteProduct in ProductDetailsTable.FindElements(By.XPath(".//tr[contains(@id,'LineItemList')]")))
            {
                goalObject.ProductDetails.Add(GetProductDetails(quoteProduct));
            }
            return goalObject;
        }

        private ProductDetails GetProductDetails(IWebElement quoteProduct)
        {
            ProductDetails productDetails = new ProductDetails();
            productDetails.ProductDescription = quoteProduct.FindElement(By.XPath(".//td[3]")).Text;
            productDetails.Quantity = quoteProduct.FindElement(By.XPath(".//td[4]")).Text;
            productDetails.UnitListPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(By.XPath(".//td[5]")).Text);
            productDetails.UnitPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(By.XPath(".//td[9]")).Text);
            productDetails.SellingPrice = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(By.XPath(".//td[10]")).Text);
            productDetails.DiscountInPercantage = quoteProduct.FindElement(By.XPath(".//td[13]")).Text.Trim().Replace("%", "").Trim();

            //productDetails.DiscountInCurrency = GenericHelper.ConvertCurrencyIntoString(quoteProduct.FindElement(By.XPath(".//td[9]")).Text);
            //productDetails.PriceModifier = quoteProduct.FindElement(By.XPath(".//td[]")).Text;
            return productDetails;
        }

        public GoalHomePage NavigateToDealHeaderTab()
        {
            DealHeaderTab.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToQuotesTab()
        {
            WorkflowTab.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToWorkflowTab()
        {
            QuotesTab.Click();
            return new GoalHomePage(WebDriver);
        }
        #endregion -- Actions --     
        public bool ValidateDSAQuoteDetails(DSAQuoteSummaryObject quoteSummaryObject)
        {
            bool result = true;
            try
            {
                NavigateToDealHeaderTab();
            }
            catch (Exception)
            {
            }
            ValidateResultsAndLog(DH_QuoteNumber.GetAttribute("value").Split('-')[1].Replace("/", "."), quoteSummaryObject.QuoteNumber, "Quote Number");
            ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_ListPrice.GetAttribute("value")), quoteSummaryObject.QuoteListPrice, "Quote List Price");
            ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_TotalNSPinUSD.GetAttribute("value")), quoteSummaryObject.QuoteSellingPrice, "Quote Selling Price");
            //ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInCurrency.GetAttribute("value")), quoteSummaryObject.QuoteTotalMargin, "QuoteTotalMargin");
            ValidateResultsAndLog(DH_TotalDOLinPercentage.GetAttribute("value"), quoteSummaryObject.QuoteDiscountInPercentage, "QuoteDiscountInPercentage");
            //ValidateResultsAndLog(DH_QuoteMarginInPercentage.GetAttribute("value"), quoteSummaryObject.QuoteTotalMarginPercentage, "QuoteTotalMarginPercentage");

            return result;
        }

        public void ValidateResultsAndLog(string actual, string expected, string variable = "")
        {
            bool result = true;
            result &= actual.Contains(expected);
            if (result) Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format($@"{variable} from DSA page {actual} is same as GoalLite page"), true);
            else
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(string.Format($@"{variable} from DSA page {actual} is not same as GoalLite page"), true);
            //return result;
        }
        public void ApproveGoalLite()
        {
            WebDriver.WaitForElementDisplayed(_approveBtn, TimeSpan.FromSeconds(5));
            ApproveBtn.Click();
            EnterBusinessCase();
            BusinessCaseTextAreaOkbtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
        }

        private void EnterBusinessCase()
        {
            WebDriver.WaitForWebElement(_textArea, TimeSpan.FromSeconds(5));
            var textboxs = WebDriver.FindElements(_textArea);
            foreach (var textbox in textboxs)
            {
                if (textbox.Displayed)
                {
                    textbox.Set("Automation");
                    break;
                }
            }
        }

        public void DenyGoalLite()
        {
            WebDriver.WaitForElementDisplayed(_denyBtn, TimeSpan.FromSeconds(5));
            DenyBtn.Click();
            EnterBusinessCase();
            BusinessCaseTextAreaOkbtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
        }

        public GoalHomePage ExpandGoalLiteAmerApprovalMatrix()
        {
            ExpandFolder(WorkflowFolderExpand);
            GLAmerApprovalMatrix.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandGoalLiteAmerApprovalAssignments()
         {
            ExpandFolder(WorkflowFolderExpand);
            GLAmerApprovalAssignments.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandGoalLiteAmerAutoDenyMatrix()
        {
            ExpandFolder(WorkflowFolderExpand);
            GLAmerAutoDenyMatrix.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandGoalLiteAmerAutoApprovalMatrix()
        {
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            ExpandFolder(WorkflowFolderExpand);
            GLAmerAutoApprovalMatrix.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage PerformAddNewApprovalMatrix(string accountGroupNameID, string productType, string uiProfile, DSAQuoteSummaryObject quoteSummaryObject)
        {
            GLAmerApprovalMatrix_NewBtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            AccountGroupName.Set(accountGroupNameID);
            //AccountGroupName.SendKeys(Keys.Enter);
            ProductHierarchySearch.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            IReadOnlyCollection<string> WindowHandles = WebDriver.WindowHandles;
            ProductHierarchySearch.Click();
            SelectProductType(productType);
            WebDriver.WaitForElementEnabled(_GL_AM_LowerDOL, TimeSpan.FromSeconds(5));
            SetInputDetails(quoteSummaryObject);
            AddApprovalLevel(uiProfile);
            PerformGoalApprovalFormSubmition();
            return new GoalHomePage(WebDriver);
        }

        private void SetInputDetails(DSAQuoteSummaryObject quoteSummaryObject, int limit = 10)
        {
            SetLimit(GL_AM_LowerRevenue, quoteSummaryObject.QuoteSellingPrice, false, limit);
            SetLimit(GL_AM_UpperRevenue, quoteSummaryObject.QuoteSellingPrice, limit: limit);
            SetLimit(GL_AM_LowerMargin, quoteSummaryObject.QuoteTotalMarginPercentage, false, limit);
            SetLimit(GL_AM_UpperMargin, quoteSummaryObject.QuoteTotalMarginPercentage, limit: limit);
            SetLimit(GL_AM_LowerDOL, quoteSummaryObject.QuoteDiscountInPercentage, false, limit);
            SetLimit(GL_AM_UpperDOL, quoteSummaryObject.QuoteDiscountInPercentage, limit: limit);
            SetDate();
        }

        public GoalHomePage PerformAddNewApprovalAssignments(string accountGroupNameID, string productType, string username)
        {
            GLAmerApprovalMatrix_NewBtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(5));
            AccountGroupName.Set(accountGroupNameID);
            //AccountGroupName.SendKeys(Keys.Enter);
            ProductHierarchySearch.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            ProductHierarchySearchInAssignment.Click();
            SelectProductType(productType);
            GL_AA_UserNameTxtBox.Set(username);
            ProductHierarchySearch.Click();
            SetDate();
            //AddApprovalLevel(uiProfile);
            PerformGoalApprovalFormSubmition();
            return new GoalHomePage(WebDriver);
        }

        private void SetDate()
        {
            DateTime today = DateTime.Today;
            //Validity_StartDate.Set(DateTime.Today.ToString("MM/dd/yyyy"));
            Validity_EndDate.Set(DateTime.Today.ToString("MM/dd/yyyy"));
        }

        public GoalHomePage PerformAddNewAutoApprovalMatrix(string giiHierarchyID, string accountGroupID, string productType, DSAQuoteSummaryObject quoteSummaryObject)
        {
            GLAmerApprovalMatrix_NewBtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(5));
            GIIHierarchyID.Set(giiHierarchyID);
            GL_AM_LowerRevenue.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            ProductHierarchySearchInAssignment.Click();
            SelectProductType(productType);
            AccountGroupID.Set(accountGroupID);
            System.Threading.Thread.Sleep(2000);
            Actions action = new Actions(WebDriver);
            //action.SendKeys(Keys.ArrowDown).Perform();
            action.SendKeys(Keys.Enter).Perform();

            SetInputDetails(quoteSummaryObject);
            PerformGoalApprovalFormSubmition();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage PerformAddNewAutoDenyMatrix(string giiHierarchyID, string accountGroupID, string productType, DSAQuoteSummaryObject quoteSummaryObject)
        {
            GLAmerApprovalMatrix_NewBtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(5));
            GIIHierarchyID.Set(giiHierarchyID);
            GL_AM_LowerRevenue.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            ProductHierarchySearchInAssignment.Click();
            SelectProductType(productType);
            AccountGroupID.Set(accountGroupID);
            System.Threading.Thread.Sleep(2000);
            Actions action = new Actions(WebDriver);
            //action.SendKeys(Keys.ArrowDown).Perform();
            action.SendKeys(Keys.Enter).Perform();

            SetInputDetails(quoteSummaryObject, 2);
            PerformGoalApprovalFormSubmition();
            return new GoalHomePage(WebDriver);
        }

        private void SetLimit(IWebElement element, string value, bool upper = true, int limit = 10)
        {
            int modifiedValue = 0;
            if (Int32.TryParse(value.Split('.')[0], out int j))
            {
                Console.WriteLine(j);
                modifiedValue = upper ? j + limit : j - limit;
            }
            else
            {
                Console.WriteLine("String could not be parsed in SetLowerLimit method");
            }
            element.Set(modifiedValue.ToString());
        }

        public IWebElement GetCheckBoxElement(string productType)
        {
            return WebDriver.FindElement(By.XPath("//*[@title='ESG']/../../td/input"));
        }
        public void SelectProductType(string productType)
        {
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(10));
            IReadOnlyCollection<string> WindowHandles = WebDriver.WindowHandles;
            try
            {
                WebDriver.SwitchTo().Frame(0);
                WebDriver.SwitchTo().Frame(0);
                WebDriver.SwitchTo().Frame(0);
                //WebDriver.SwitchTo().Frame(iframeTable).SwitchTo().Frame(iframeTable2).SwitchTo().Frame(iframeTable3);
                //WebDriver.SwitchTo().Frame(iframeTable2);
                //WebDriver.SwitchTo().Frame(iframeTable3);
                WebDriver.WaitForElementVisible(_checkbox, 2);
            }
            catch (Exception)
            {
            }
            //WebDriver.SwitchTo().Frame(iframeTable3);
            WebDriver.WaitForElementVisible(_checkbox, 2);
            WebDriver.WaitForElement(_checkbox, TimeSpan.FromSeconds(10));
            GetCheckBoxElement(productType).Set(true);
            System.Threading.Thread.Sleep(2000);
            ClickModalOkBtn();
        }
        public void ClickModalOkBtn()
        {
            WebDriver.SwitchTo().DefaultContent();
            //WebDriver.SwitchTo().ParentFrame();
            WebDriver.SwitchTo().Frame(0);
            WebDriver.SwitchTo().Frame(0);
            WebDriver.WaitForElementVisible(_okModalBtn, 2);
            WebDriver.FindElement(_okModalBtn).Click();
            WebDriver.SwitchTo().DefaultContent();
        }

        public void AddApprovalLevel(string role, int level = 1)
        {
            GetApprovalLevelElement(level).Click();
            GetApprovalLevelElement(level).Set(role);
        }

        private IWebElement GetApprovalLevelElement(int level)
        {
            return WebDriver.FindElement(By.XPath($@"//input[@id='DISPLAY_ApprovalLevel{level}']"));
        }
        public void PerformGoalApprovalFormSubmition()
        {
            WebDriver.SwitchTo().DefaultContent();
            GL_ApprovalMatrix_SubmitBtn.Click();
            System.Threading.Thread.Sleep(2000);
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            WebDriver.SwitchTo().Frame(0);
            WebDriver.SwitchTo().Frame(0);
            WebDriver.WaitForElement(_commentTxt, 5);
            WebDriver.FindElement(_commentTxt).Set("Automation Comment Submission");
            GL_ApprovalMatrix_CommnetOkBtn.Click();
            WebDriver.SwitchTo().DefaultContent();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
        }

        public string GetUIProfile(string userName)
        {
            WebDriver.WaitForElement(_Admin_SearchBox, 5);
            Admin_SearchBox.Set(userName);
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            System.Threading.Thread.Sleep(5000);
            uiProfile = userName;
            var _uiProfile = UIProfile.Text;
            return UIProfile.Text;
        }
        public bool ValidateDSAQuoteProductDetails(DSAQuoteSummaryObject quoteSummaryObject)
        {
            bool result = true;
            ValidateResultsAndLog(DH_QuoteNumber.GetAttribute("value").Split('-')[1].Replace("/", "."), quoteSummaryObject.QuoteNumber, "Quote Number");
            ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_ListPrice.GetAttribute("value")), quoteSummaryObject.QuoteListPrice, "Quote List Price");
            ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_TotalNSPinUSD.GetAttribute("value")), quoteSummaryObject.QuoteSellingPrice, "Quote Selling Price");
            ValidateResultsAndLog(GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInCurrency.GetAttribute("value")), quoteSummaryObject.QuoteTotalMargin, "QuoteTotalMargin");
            ValidateResultsAndLog(DH_TotalDOLinPercentage.GetAttribute("value"), quoteSummaryObject.QuoteDiscountInPercentage, "QuoteDiscountInPercentage");
            ValidateResultsAndLog(DH_QuoteMarginInPercentage.GetAttribute("value"), quoteSummaryObject.QuoteTotalMarginPercentage, "QuoteTotalMarginPercentage");

            return result;
        }
        public void PerformExpireApproveStatusPolicy()
        {
            foreach (var item in WebDriver.FindElements(ApproveStatusPolicy))
            {
                WebDriver.PerformRightClick(item);
                MassExpire.Click();
                System.Threading.Thread.Sleep(5000);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                WebDriver.SwitchTo().Frame(0);
                WebDriver.SwitchTo().Frame(0);

                CommentTextArea.Set("Automation");
                OkButton.Click();
                WebDriver.SwitchTo().Alert().Accept();
            }


        }
        public GoalHomePage ExpireManualApprovalPolicy(string loggerInUser)
        {
            this.loggedInUser = loggerInUser.ToUpper();
            NavigateToManagePolicies();
            ExpandGoalLiteAmerApprovalMatrix();
            WebDriver.WaitForElementDisplayed(CreateBy, TimeSpan.FromSeconds(5));
            UncheckColumnOptionsExceptStatusAndCreatedBy();
            WebDriver.FindElement(CreateByFilter).Click();
            WebDriver.WaitForElementDisplayed(CreateByFilterPopup, TimeSpan.FromSeconds(5));
            WebDriver.FindElement(CreateByFilterTextbox).Set(loggerInUser.ToUpper());
            WebDriver.PerformDoubleClick(WebDriver.FindElement(CreateByFilteredResult));
            WebDriver.FindElement(CreateByFilterPopupOkBtn).Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            PerformExpireApproveStatusPolicy();
            return new GoalHomePage(WebDriver);
        }



        public GoalHomePage UncheckColumnOptionsExceptStatusAndCreatedBy()
        {
            string[] r = new[] { "status", "created by", "account group id", "product hierarchy id" };
            ColumnCheck.Click();
            for (int i = 2; i < ColumnElements.Count; i++)
            {
                if (!r.Any(c => ColumnElements[i].Text.ToLower().Contains(c)))
                {
                    ColumnElements[i].Click();
                }
            }
            return new GoalHomePage(WebDriver);
        }

        public GoalHomePage OpenWorkflowPolicy()
        {
            NavigateToWorkflowTab();
            LOBApprovalLevel1.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            return new GoalHomePage(WebDriver);
        }
        public Dictionary<string,string> GetWorkflowCondition()
        {
            string condition = WorkflowCondition.Text;
            //string condtion = "· FinancialAnalyst: Approval required for margin range -99,999,999,999.00%..999,999,999,999,999.00% revenue range -9,999,999,999,999,999.00 USD..999,999,999,999,999,999.00 USD and DOL range -999,999,999,999.00%..999,999,999,999.00%";
            string[] stringSeparators1 = new string[] { "margin range", "revenue range", "DOL range" };
            string[] stringSeparators2 = new string[] { ".." };
            string[] firstNames1 = condition.Split(stringSeparators1, StringSplitOptions.None);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("lowerMargin", GenericHelper.ConvertCurrencyIntoString(firstNames1[1].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[0]));
            dic.Add("upperMargin", GenericHelper.ConvertCurrencyIntoString(firstNames1[1].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[1]));
            dic.Add("lowerRevenu", GenericHelper.ConvertCurrencyIntoString(firstNames1[2].ToLower().Replace("usd", "").Replace("and", "").Trim().Split(stringSeparators2, StringSplitOptions.None)[0]));
            dic.Add("upperRevenu", GenericHelper.ConvertCurrencyIntoString(firstNames1[2].ToLower().Replace("usd", "").Replace("and", "").Trim().Split(stringSeparators2, StringSplitOptions.None)[1]));
            dic.Add("lowerDol", GenericHelper.ConvertCurrencyIntoString(firstNames1[3].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[0]));
            dic.Add("upperDol", GenericHelper.ConvertCurrencyIntoString(firstNames1[3].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[1]));
            return dic;
        }

    }
}
