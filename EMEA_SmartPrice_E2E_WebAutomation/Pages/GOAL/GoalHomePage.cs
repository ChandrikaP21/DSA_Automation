using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.GOAL;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Pages.GOAL
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
        protected By _GoalLite => By.XPath("//*[@title='GOAL Lite']");
        public IWebElement GOAL_LiteFolder => WebDriver.FindElement(_GoalLite);
        public By _MyQuotes => By.XPath("//*[@title='My Quotes  '][2]");
        public IWebElement MyQuotes=> WebDriver.FindElement(_MyQuotes);
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
        public IWebElement WorkflowTab => WebDriver.FindElement(By.XPath("//*[@id='WorkspaceTabs_TABS']//a[contains(@class,'tab WorkspaceTabs_6')]"));
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
        public IWebElement DH_QuoteMarginInCurrency => WebDriver.FindElement(By.XPath("//input[@id='form2_Margin']"));
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
        public IWebElement ApprovalMatrix => WebDriver.FindElement(By.XPath("//*[@title='Approval Matrix']"));
        public IWebElement GLAmerApprovalAssignments => WebDriver.FindElement(By.XPath("//*[@title='GLAmer Approval Assignments']"));
        public IWebElement GLAmerAutoApprovalMatrix => WebDriver.FindElement(By.XPath("//*[@title='GL AutoApproval Matrix']"));
        public IWebElement GLAmerAutoDenyMatrix => WebDriver.FindElement(By.XPath("//*[@title='GLAutoDeny Matrix']"));
    
        public IWebElement GoalAutoDenyMatrix => WebDriver.FindElement(By.XPath("//*[@title='GOALAutoDeny Matrix']"));
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

        public By _Action => By.XPath("//button[@title='Table Actions']");
        public IWebElement ActionButton => WebDriver.FindElement(_Action);

        public By ByCheckApprover => By.XPath("//span[text()='Approve']/../../following::td[1]/div/span");
        public IWebElement CheckApprover => WebDriver.FindElement(ByCheckApprover);

        public By _Expire => By.XPath("//*[@id='expire' and @title='Expire the deal']");
        public IWebElement Expire => WebDriver.FindElement(_Expire);
        public By _BusinessCaseJustification => By.XPath("//textarea[contains(@id,'ApprovableComments')]");
        public IWebElement BusinessCaseJustificationText => WebDriver.FindElement(_BusinessCaseJustification);
        //button[@id='Button' and text()='OK']
        public By _Ok => By.XPath("//button[@id='Button' and text()='OK']");
        public List<IWebElement> Ok => WebDriver.FindElements(_Ok).ToList();

        #endregion -- GoalLite details --
        #region ----Goal Details---

        public By _submitted => By.XPath("//div[text()='Submitted']");
        public IWebElement Submitted => WebDriver.FindElement(_submitted);
        public By _table => By.XPath("//*[@id='EntityList_inner']");
        public IWebElement Table => WebDriver.FindElement(_table);

        public By _goalid(string goalId) => By.XPath("//span[@title='GOAL Deal Id' and text()='"+ goalId + "']");
        public IWebElement GoalId(string goalId) => WebDriver.FindElement(_goalid(goalId));
        public By _approved(string goalId)=>By.XPath("//*[@title='GOAL Deal Id' and contains(text(),'"+goalId+"')]/../../../td/div/span[@title='GOAL Deal Status']");
        public IWebElement ApprovedGoal(string goalId) => WebDriver.FindElement(_approved(goalId));
        public By _approveOnBehalf => By.XPath("//button[text()='Approve on behalf']");
        public IWebElement ApproveOnBehalf => WebDriver.FindElement(_approveOnBehalf);
        public By _denyOnBehalf => By.XPath("//button[text()='Deny on behalf']");
        public IWebElement DenyOnBehalf => WebDriver.FindElement(_denyOnBehalf);
        
        public By _comment => By.XPath("//textarea[contains(@id,'COMMENT')]");
        public IWebElement Comment => WebDriver.FindElement(_comment);
        public By _mydeals => By.XPath("//div[text()='My Deals']");
        public IWebElement Mydeals => WebDriver.FindElement(_mydeals);
        

        public By _edit => By.XPath("//*[@id='editButton']");
        public IWebElement Edit => WebDriver.FindElement(_edit);

        public By _goalDealName => By.Id("form1_GoalName");
        public IWebElement GoalDealName => WebDriver.FindElement(_goalDealName);

        public By _quotes => By.XPath("//*[@id='workspace_body']//span[contains(text(),'Quotes')]");
        public IWebElement Quotes => WebDriver.FindElement(_quotes);

        public By _expandProduct => By.XPath("//*[@id='LineItemList_inner$x0_0']/div/div/span[1]");
        public IWebElement ExpandProduct => WebDriver.FindElement(_expandProduct);

        public By _validateMaxQuantity => By.XPath("//table/tr[2]/td[count(//table/tbody/tr/th[@vcolid='validateMaxQty']/preceding::th)-2]");
      
        public IWebElement ValidateMaxQuantity => WebDriver.FindElement(_validateMaxQuantity);
        public By _maxQuantity => By.XPath("//table/tr[2]/td[count(//table/tbody/tr/th[@vcolid='MaximumQuantity']/preceding::th)-2]");
     
        public IWebElement MaxQuantity => WebDriver.FindElement(_maxQuantity);
        public By _validityPeriod => By.Id("validity01_LinkLauncher");
        public IWebElement ValidityPeriod => WebDriver.FindElement(_validityPeriod);

        public By _startDate => By.Id("(//input[contains(@id,'VxDatePicker_Field_')])[1]");
        public IWebElement StartDate => WebDriver.FindElement(_startDate);
        public By _endDate => By.Id("(//input[contains(@id,'VxDatePicker_Field_')])[2]");
        public IWebElement EndDate => WebDriver.FindElement(_endDate);

        public By _dealType => By.Id("form1_DealType_control");
        public IWebElement DealType => WebDriver.FindElement(_dealType);
        public By _dealTypeOption => By.Id("form1_DealType_options_0");
        public IWebElement DealTypeOption => WebDriver.FindElement(_dealTypeOption);

        public By _validityDialogClose => By.XPath("//*[@id='validity01_Dialog-close-icon']");
        public IWebElement ValidityDialogClose => WebDriver.FindElement(_validityDialogClose);
      
        public By _routeToMarket => By.Id("form1_RouteToMarket_control");
        public IWebElement RouteToMarket => WebDriver.FindElement(_routeToMarket);
        public By _routeToMarketOption => By.Id("form1_RouteToMarket_options_0");
        public IWebElement RouteToMarketOption => WebDriver.FindElement(_routeToMarketOption);
        public By _refreshAndSave => By.Id("priceButton");
        public IWebElement RefreshAndSave => WebDriver.FindElement(_refreshAndSave);
        public By _submitButton => By.Id("submitButton");
        public IWebElement SubmitButton => WebDriver.FindElement(_submitButton);

        public By _pricingMethodology => By.Id("form1_PricingMethodology_control");
        public IWebElement PricingMethodology => WebDriver.FindElement(_pricingMethodology);
        
            public By _pricingMethodologyOption => By.Id("form1_PricingMethodology_options_0");
        public IWebElement PricingMethodologyOption => WebDriver.FindElement(_pricingMethodologyOption);

        public By _buyingstartegy => By.Id("form1_DealInfo_BuyingStrategy_control");
        public IWebElement Buyingstartegy => WebDriver.FindElement(_buyingstartegy);

        public By _buyingstartegyOption => By.Id("form1_DealInfo_BuyingStrategy_options_0");
        public IWebElement BuyingstartegyOption => WebDriver.FindElement(_buyingstartegyOption);

        public By _paymentTerms => By.Id("form1_PaymentTerm_control");
        public IWebElement PaymentTerms => WebDriver.FindElement(_paymentTerms);

        public By _paymentTermsOption => By.Id("form1_PaymentTerm_options_0");
        public IWebElement PaymentTermsOption => WebDriver.FindElement(_paymentTermsOption);

        public By _shippingTerms => By.Id("form1_ShipTerm_control");
        public IWebElement ShippingTerms => WebDriver.FindElement(_shippingTerms);

        public By _shippingTermsOption => By.Id("form1_ShipTerm_options_0");
        public IWebElement ShippingTermsOption => WebDriver.FindElement(_shippingTermsOption);

        public By _businessCase => By.XPath("(//input[contains(@name,'ApprovableComments')]/following::textarea)");
        public List<IWebElement> BusinessCase => WebDriver.FindElements(_businessCase).ToList();

        public By _okSubmit => By.XPath("//*[@id='Button'and text()='OK']");
        public IWebElement OkSubmit => WebDriver.FindElement(_okSubmit);

        public By _goalTableExists => By.Id("EntityList_inner");
        public IWebElement GoalTableExists => WebDriver.FindElement(_goalTableExists);

        
        public By _manuaReview => By.Id("form2_ManualReview");
        public IWebElement ManualReview => WebDriver.FindElement(_manuaReview);

        #endregion
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
                WebDriver.WaitForElementDisplayed(_ManageDeals, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                ManageDeals.Click();
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not  Navigate To ManageDeals", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToGoalLiteFolder()
        {
            try
            {
                WebDriver.WaitForElementDisplayed(_GoalLite, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                GOAL_LiteFolder.Click();
            }
            catch (Exception)
            {
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToMyQuote()
        {
            try
            {
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
                WebDriver.WaitForElementDisplayed(_MyQuotes, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                MyQuotes.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception)
            {
            }
            return new GoalHomePage(WebDriver);
        }

        public GoalHomePage NavigateToManagePolicies()
        {
            try
            {
                WebDriver.WaitForElementDisplayed(_ManagePolicies, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                ManagePolicies.Click();
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not navigate to ManagePolicies", ex);
            }
            
            return new GoalHomePage(WebDriver);
        }
        //public GoalHomePage NavigateToAction()
        //{
            
            
        //}
        public GoalHomePage NavigateToExpire()
        {
            //ActionButton.Click();
            //IWebElement element = WebDriver.FindElement(By.XPath("(//td[@class='VDropDown_option'])[3]"));
            //element.Click();
            WebDriver.WaitForElementDisplayed(_Expire, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            Expire.Click();
            IAlert alert = WebDriver.SwitchTo().Alert();
            alert.Accept();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage BusinessCaseJustification()
        {
            
            WebDriver.WaitForElementDisplayed(_BusinessCaseJustification, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            WebElementUtil.Set(BusinessCaseJustificationText,"Test");
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GetApprover(GoalObject goalObj)
        {
            try
            {
                
                WebDriver.WaitForElementDisplayed(ByCheckApprover, TimeSpan.FromSeconds(3));
                goalObj.Approver = CheckApprover.Text; 


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage CommitOk()
        {

            try
            {
                WebDriver.WaitForElementDisplayed(_Ok, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                foreach(var displayObj in Ok )
                {
                    if (displayObj.Displayed)
                        if (displayObj.Text == "OK")
                            displayObj.Click(); 
                }
            }
            catch (Exception ex)
            {

            }
            return new GoalHomePage(WebDriver);
        }

        public GoalHomePage NavigateToAdministration()
        {
            try
            {
                WebDriver.WaitForElementDisplayed(_Administration, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                Administration.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {
                
                throw new ShowStopperException("Could not click Administartion",ex);
            }
           
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandAllQuotesInGoalLiteInbox()
        {
            try
            {
                ExpandFolder(GOAL_LiteFolderExpand);
                ExpandFolder(AllQuotesFolderExpand);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not ExpandAllQuotesInGoalLiteInbox", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage SelectAllQuotesInGoalLiteInbox()
        {
            try
            {
                WebDriver.WaitingForSpinner(spinner);
                ExpandFolder(GOAL_LiteFolderExpand);
                WebDriver.WaitingForSpinner(spinner2);
                AllQuotesFolder.Click();
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not  Select All Quotes In GoalLite Inbox", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage OpenGoalLite(string goalLiteId,bool IsSelectClick=false)
       {
            try
            {
                //*[@title='GOAL Deal Id'  and text()='GL000000505149-1']
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
                var t = GetGoalLiteIDElement(goalLiteId);
                if (IsSelectClick)
                {
                    WebDriver.PerformRightClick(GetGoalLiteIDElement(goalLiteId));
                }
                else
                { WebDriver.PerformDoubleClick(GetGoalLiteIDElement(goalLiteId)); }

                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not Open GoalLite", ex);
            }
            return new GoalHomePage(WebDriver);
        }

        public IWebElement GetGoalLiteIDElement(string goalLiteId)
        {
            var ele = By.XPath($@"//*[@title='GOAL Deal Id'  and text()='{goalLiteId}']");
            WebDriver.WaitForElementDisplayed(ele, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            return WebDriver.FindElement(ele);
        }
        public GoalObject GetDetailsInGoalPage()
        {
            GoalObject goalObject = new GoalObject();
            try
            {
                
                goalObject.ProductDetails = new List<ProductDetails>();
                goalObject.QuoteNumber = DH_QuoteNumber.GetAttribute("value").Split('-')[1].Replace("/", ".");
                goalObject.CustomerID = DH_CustomerID.GetAttribute("value");
                goalObject.ListPrice = GenericHelper.ConvertCurrencyIntoString(DH_ListPrice.GetAttribute("value"));
                goalObject.TotalNSP = GenericHelper.ConvertCurrencyIntoString(DH_TotalNSP.GetAttribute("value"));
                goalObject.TotalNSPinUSD = GenericHelper.ConvertCurrencyIntoString(DH_TotalNSPinUSD.GetAttribute("value"));
                goalObject.TotalDOLinPercentage = DH_TotalDOLinPercentage.GetAttribute("value");
                try
                {

                    goalObject.QuoteMarginInCurrency = GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInCurrency.GetAttribute("value").Replace("-", ""));
                    goalObject.QuoteMarginInPercentage = GenericHelper.ConvertCurrencyIntoString(DH_QuoteMarginInPercentage.GetAttribute("value").Replace("-", ""));
                    //goalObject.CombinedDAMQuoteLevel = GenericHelper.ConvertCurrencyIntoString(DH_CombinedDAMQuoteLevel.GetAttribute("value"));
                    //goalObject.ExceedingAmountQuoteLevel = GenericHelper.ConvertCurrencyIntoString(DH_ExceedingAmountQuoteLevel.GetAttribute("value"));

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
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not getDetails in GoalPage", ex);
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
            QuotesTab.Click();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage NavigateToWorkflowTab()
        {
            WorkflowTab.Click();
            
            return new GoalHomePage(WebDriver);
        }
        #endregion -- Actions --     
        public bool ValidateDSAQuoteDetails(DSAQuoteSummaryObject quoteSummaryObject)
        {
            bool result = true;
            try
            {
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
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not ValidateDSAQuoteDetails", ex);
            }
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
            try
            {
                WebDriver.WaitForElementDisplayed(_approveBtn, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                ApproveBtn.Click();
                EnterBusinessCase();
                BusinessCaseTextAreaOkbtn.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not approve GoalLite, check ApproveGoalLite() in GoalHomePage Class", ex);
            }
        }

        private void EnterBusinessCase()
        {
            WebDriver.WaitForWebElement(_textArea, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
            WebDriver.WaitForElementDisplayed(_denyBtn, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            DenyBtn.Click();
            EnterBusinessCase();
            BusinessCaseTextAreaOkbtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
        }

        public GoalHomePage ExpandGoalLiteAmerApprovalMatrix()
        {
            try
            {
                ExpandFolder(WorkflowFolderExpand);
                GLAmerApprovalMatrix.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not click GLAmerApprovalMatrix", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandGoalApprovalMatrix()
        {
            try
            {
                ExpandFolder(WorkflowFolderExpand);
                ApprovalMatrix.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not click GLApprovalMatrix", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ExpandGoalLiteAmerApprovalAssignments()
         {
            try
            {
                ExpandFolder(WorkflowFolderExpand);
                GLAmerApprovalAssignments.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not Expand GoalLiteAmerApprovalAssignments", ex);
            }
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
        public GoalHomePage ExpandGoalAutoDenyMatrix()
        {
            ExpandFolder(WorkflowFolderExpand);
            GoalAutoDenyMatrix.Click();
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
        public GoalHomePage ExpandGoalAutoApprovalMatrix()
        {
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            ExpandFolder(WorkflowFolderExpand);
            ApprovalMatrix.Click();
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
            WebDriver.WaitForElementEnabled(_GL_AM_LowerDOL, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
            SetInputDetails(quoteSummaryObject);
            AddApprovalLevel(uiProfile);
            PerformGoalApprovalFormSubmition();
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalPerformAddNewApprovalMatrix(string accountGroupNameID, string productType, string uiProfile, DSAQuoteSummaryObject quoteSummaryObject)
        {
            try
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
                WebDriver.WaitForElementEnabled(_GL_AM_LowerDOL, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                SetInputDetails(quoteSummaryObject);
                AddApprovalLevel(uiProfile);
                PerformGoalApprovalFormSubmition();
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Unable to add new Approval Matrix,refer GoalPerformAddNewApprovalMatrix() in GoalHomePage class", ex);
            }
           
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
            try
            {
                GLAmerApprovalMatrix_NewBtn.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
                WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not  PerformAddNewApprovalAssignments", ex);
            }
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
            try
            {
                GLAmerApprovalMatrix_NewBtn.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
                WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not add autoApprovalMatrix", ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage PerformAddNewAutoDenyMatrix(string giiHierarchyID, string accountGroupID, string productType, DSAQuoteSummaryObject quoteSummaryObject)
        {
            GLAmerApprovalMatrix_NewBtn.Click();
            WebDriver.WaitingForSpinner(spinner);
            WebDriver.WaitingForSpinner(spinner2);
            WebDriver.WaitForElementDisplayed(By.XPath("//div[@class='VStandardHeader_title']"), TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
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
            try
            {
                WebDriver.WaitForElement(_Admin_SearchBox, 5);
                Admin_SearchBox.Set(userName);
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
                System.Threading.Thread.Sleep(5000);
                uiProfile = userName;
                var _uiProfile = UIProfile.Text;
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not click Administartion", ex);
            }

           
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
            try
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
            }
            catch(Exception ex)
            {
                throw new ShowStopperException(ex.Message,ex);
            }
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage EditGoal()
        {
           
            try
            {
                Thread.Sleep(3000);
               // WebDriver.FindElement(By.XPath("//div/button[@id='editButton']")).Click();
                Edit.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_goalDealName));
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer EditGoal() in GoalHomePage Class----"+ex.Message,true);
                
            }
            return new GoalHomePage(WebDriver);

        }
        public GoalHomePage ClickValidityPeriod(string date)
        {

            try
            {
                ValidityPeriod.Click();
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer ClickValidityPeriod() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage UpdateEndDate(string date)
        {

            try
            {
                EndDate.SendKeys(date);
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer ClickValidityPeriod() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ChangeValidity(string date)
        {

            try
            {
                ClickValidityPeriod(date).UpdateEndDate(date);
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer ChangeValidity() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage EnterMaxQuantity(string quantity)
        {
            
            try
            {
                ClickOnQuotesTab().SelectValidMaxQuantityCheckBox().EnterQuantity(quantity);
                CommitOk().CloseValidityPopUp();
                    
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer EnterMaxQuantity() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage CloseValidityPopUp()
        {

            try
            {
                ValidityDialogClose.Click();
                Thread.Sleep(4000);
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer CloseValidityPopUp() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage SelectValidMaxQuantityCheckBox()
        {
            
            try
            {
                ValidateMaxQuantity.Click();
                Thread.Sleep(4000);
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer SelectValidMaxQuantityCheckBox() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage EnterQuantity(string quantity)
        {
          
            try
            {
                MaxQuantity.SendKeys(quantity);
                Thread.Sleep(4000);
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer SelectValidMaxQuantityCheckBox() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ClickOnQuotesTab()
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                Thread.Sleep(3000);
                Quotes.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_quotes));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer ClickOnQuotesTab() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage EnterGoalDealName(string scenarioId,string title)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                Thread.Sleep(3000);
                GoalDealName.SendKeys(constant.GoalBusinessCasetext(scenarioId, title));
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_dealType));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to click on Edit:Refer EnterGoalDealName() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalDealType()
        {
            try
            {
                
                Thread.Sleep(2000);
                DealType.Click();
                Thread.Sleep(3000);
                DealTypeOption.Click();

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_routeToMarket));
           
            }
           catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalDealType() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalRouteToMarket()
        {
            try
            {
                Thread.Sleep(2000);
                RouteToMarket.Click();
                Thread.Sleep(3000);
                RouteToMarketOption.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_pricingMethodology));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalRouteToMarket() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalPricingMethodology()
        {
            try
            {
                Thread.Sleep(2000);
                PricingMethodology.Click();
                Thread.Sleep(3000);
                PricingMethodologyOption.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_buyingstartegy));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalPricingMethodology() in GoalHomePage Class", true);
            }
           
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalBuyingstartegy()
        {
            try
            {
                Thread.Sleep(2000);
                Buyingstartegy.Click();
                Thread.Sleep(3000);
                BuyingstartegyOption.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_paymentTerms));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalBuyingstartegy() in GoalHomePage Class", true);
            }
           
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalPaymentTerms()
        {
            try
            {
                Thread.Sleep(2000);
                PaymentTerms.Click();
                Thread.Sleep(3000);
                PaymentTermsOption.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_shippingTerms));
            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalPaymentTerms() in GoalHomePage Class", true);
            }

            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalShippingTerms()
        {
            try
            {
                Thread.Sleep(2000);
                ShippingTerms.Click();
                Thread.Sleep(3000);
                ShippingTermsOption.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_refreshAndSave));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalShippingTerms() in GoalHomePage Class", true);
            }

            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalSaveAndRefresh()
        {
            try
            {
                RefreshAndSave.Click();
                Thread.Sleep(3000);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_submitButton));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalSaveAndRefresh() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalSubmit()
        {
            try
            {
                SubmitButton.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_businessCase));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer GoalSubmit() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage EnterBusinessCaseInTextArea()
        {
            try
            {
                foreach(var business in BusinessCase)
                {
                    if(business.Text=="")
                    business.SendKeys("AutomationTest");
                }
                //BusinessCase.SendKeys("AutomationTest");
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_okSubmit));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer EnterBusinessCaseInTextArea() in GoalHomePage Class", true);
            }
           
            
            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage ClickOnOkButton()
        {
            try
            {
                OkSubmit.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_goalTableExists));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer ClickOnOkButton() in GoalHomePage Class", true);
            }
            
            return new GoalHomePage(WebDriver);
        }
        
        public GoalHomePage GoalManualReview()
        {
            try
            {   if(ManualReview.GetAttribute("value")!="true")
                  ManualReview.Set(true);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(_refreshAndSave));
            }
            catch (Exception)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unable to select :Refer ClickOnOkButton() in GoalHomePage Class", true);
            }

            return new GoalHomePage(WebDriver);
        }
        public GoalHomePage GoalComment()
        {
            try
            {
                Comment.SendKeys("Automation Test** Please ignore");
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_Ok));
                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on ApproveOnBehalf button .Check 'GoalApproveOnBehalf(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
        }
        public GoalHomePage GoalApproveOnBehalf(string goalId)
        {
            try
            {
                Thread.Sleep(2000);
                ApproveOnBehalf.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_comment));
                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on ApproveOnBehalf button .Check 'GoalApproveOnBehalf(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
        }
        public GoalHomePage GoalDenyOnBehalf(string goalId)
        {
            try
            {
                Thread.Sleep(2000);
                DenyOnBehalf.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_comment));
                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on ApproveOnBehalf button .Check 'GoalApproveOnBehalf(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
        }
        
        public GoalHomePage GoalNavigateToMyDeal(string goalId)
        {
            try
            {
                Mydeals.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_goalTableExists));
                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on ApproveOnBehalf button .Check 'GoalApproveOnBehalf(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
        }
        public GoalHomePage CheckGoalStatus(string goalId)
        {
            try
            {
                
                string goalStatusMessage = "";
                Constant constant = new Constant(WebDriver);
                if (constant.IsElementPresent(_approved(goalId)))
                    if (ApprovedGoal(goalId).Text.Trim() == "Approved")
                    {
                        goalStatusMessage = "Goal Is " + ApprovedGoal(goalId).Text;
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(goalStatusMessage, true);
                    }
                   
                if (ApprovedGoal(goalId).Text.Trim() == "Denied")
                {
                    goalStatusMessage = "Goal Is " + ApprovedGoal(goalId).Text;
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(goalStatusMessage, true);
                }
                else
                {

                    goalStatusMessage = "Goal Is " + ApprovedGoal(goalId).Text;
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(goalStatusMessage, true);
                }


                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on ApproveOnBehalf button .Check 'GoalApproveOnBehalf(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
        }
        /// <summary>
        ///Goal- Manual Deny D6002
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public GoalHomePage ManualApproval(string goalId)
        {
            try
            {
                NavigateToSubmittedDeals(goalId).DoubleClickGoalId(goalId).EditGoal().GoalApproveOnBehalf(goalId).GoalComment().CommitOk().GoalNavigateToMyDeal(goalId).CheckGoalStatus(goalId);
                GoalNavigateToMyDeal(goalId);
            }
            catch (Exception)
            {
                
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unexpected error in GoalHomePage Class", true);
            }
            return new GoalHomePage(WebDriver);
        }
        /// <summary>
        ///Goal- Manual Deny D6003
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public GoalHomePage DenyApproval(string goalId)
        {
            try
            {
                NavigateToSubmittedDeals(goalId).DoubleClickGoalId(goalId).EditGoal().GoalDenyOnBehalf(goalId).GoalComment().CommitOk().GoalNavigateToMyDeal(goalId).CheckGoalStatus(goalId);
                GoalNavigateToMyDeal(goalId);
            }
            catch (Exception ex)
            {
                Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperException = new Bedrock.ExceptionHandlingBlock.ShowStopperException(ex.Message);
                showStopperException.LogException();
                
            }
            return new GoalHomePage(WebDriver);
        }
        /// <summary>
        ///Goal- Create Policy-D6004
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public GoalHomePage GoalCreatePolicy(string goalId)
        {
            try
            {
                NavigateToSubmittedDeals(goalId).DoubleClickGoalId(goalId).EditGoal().GoalDenyOnBehalf(goalId).GoalComment().CommitOk().GoalNavigateToMyDeal(goalId).CheckGoalStatus(goalId);
                GoalNavigateToMyDeal(goalId);
            }
            catch (Exception ex)
            {
                Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperException = new Bedrock.ExceptionHandlingBlock.ShowStopperException(ex.Message);
                showStopperException.LogException();

            }
            return new GoalHomePage(WebDriver);
        }

        private GoalHomePage DoubleClickGoalId(string goalId)
        {
            try
            {
                Actions action = new Actions(WebDriver);
                //action.MoveToElement(GoalId(goalId)).Build().Perform();
                action.DoubleClick(GoalId(goalId)).Perform();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_edit));
                
            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to doubleclick .Check 'DoubleClickGoalId(string goalId)' GoalHomePage Class for more information ", true);

            }
            return new GoalHomePage(WebDriver);
        }
        private GoalHomePage NavigateToSubmittedDeals(string goalId)
        {
            try
            {
                Submitted.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(_table));
                return new GoalHomePage(WebDriver);

            }
            catch (Exception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Unable to click on Submit.Check 'NavigateToSubmittedDeals(string goalId)' GoalHomePage Class for more information ", true);
                return new GoalHomePage(WebDriver);
            }
            
        }

        public GoalHomePage EditAndChangeAllTheMandateField(string scenarioId, string title,string quantity)
        {
            try
            {
                EditGoal().EnterGoalDealName(scenarioId, title).GoalDealType().GoalRouteToMarket().GoalPricingMethodology().GoalBuyingstartegy().GoalPaymentTerms().GoalShippingTerms().GoalManualReview();
                if (!string.IsNullOrEmpty(quantity))
                EnterMaxQuantity(quantity);
                if (!string.IsNullOrEmpty(TestDataReader.ValidDate))
                    ChangeValidity(TestDataReader.ValidDate);
                GoalSaveAndRefresh().GoalSubmit().EnterBusinessCaseInTextArea().ClickOnOkButton();
            }
            catch(Exception )
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Unexpected error in GoalHomePage Class", true);
            }
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
            try
            {
                NavigateToWorkflowTab();
                LOBApprovalLevel1.Click();
                WebDriver.WaitingForSpinner(spinner);
                WebDriver.WaitingForSpinner(spinner2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new GoalHomePage(WebDriver);
        }
        public Dictionary<string,string> GetWorkflowCondition()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                string condition = WorkflowCondition.Text;
                //string condtion = "· FinancialAnalyst: Approval required for margin range -99,999,999,999.00%..999,999,999,999,999.00% revenue range -9,999,999,999,999,999.00 USD..999,999,999,999,999,999.00 USD and DOL range -999,999,999,999.00%..999,999,999,999.00%";
                string[] stringSeparators1 = new string[] { "margin range", "revenue range", "DOL range" };
                string[] stringSeparators2 = new string[] { ".." };
                string[] stringSeparators3 = new string[] {" " };
                string[] firstNames1 = condition.Split(stringSeparators1, StringSplitOptions.None);
                
                dic.Add("lowerMargin", GenericHelper.ConvertCurrencyIntoString(firstNames1[1].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[0]));
                dic.Add("upperMargin", GenericHelper.ConvertCurrencyIntoString(firstNames1[1].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[1]));
                dic.Add("lowerRevenu", GenericHelper.ConvertCurrencyIntoString(firstNames1[2].ToLower().Replace("usd", "").Replace("and", "").Trim().Split(stringSeparators2, StringSplitOptions.None)[0]));
                dic.Add("upperRevenu", GenericHelper.ConvertCurrencyIntoString(firstNames1[2].ToLower().Replace("usd", "").Replace("and", "").Trim().Split(stringSeparators2, StringSplitOptions.None)[1]));
                dic.Add("lowerDol", GenericHelper.ConvertCurrencyIntoString(firstNames1[3].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[0]));
                dic.Add("upperDol", GenericHelper.ConvertCurrencyIntoString(firstNames1[3].Trim().Replace("%", "").Split(stringSeparators2, StringSplitOptions.None)[1].Split(stringSeparators3, StringSplitOptions.None)[0]));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dic;
        }

    }
}
