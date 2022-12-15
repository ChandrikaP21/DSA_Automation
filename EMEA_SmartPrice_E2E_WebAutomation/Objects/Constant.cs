using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Configuration;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using OpenQA.Selenium.Support.UI;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
    public class Constant 
    {
        public IWebDriver WebDriver;
       
        public Constant(IWebDriver driver)
        {
            WebDriver = driver;
        }

        public Constant()
        {
            
        }

        public int Count=0;
        public int accessoriesCount = 0;
        //public Constant()
        //{
        //    Count = 0;
        //}
        
        public static string URL_G4 => ConfigurationManager.AppSettings["GE2"].ToString();
        public static string URL_G2 => ConfigurationManager.AppSettings["GE4"].ToString();
        public static string URL_Prod => ConfigurationManager.AppSettings["PROD"].ToString();
        public readonly static string StandardPartnerDiscountText = "Standard Partner Discount:";
        public readonly static string showDiscountText = "Show Discount";
        public readonly static string DAMThreshhold = "SmartPrice Floor/DAM threshold exceeded, pricing approval is required";
        public string GoalBusinessCasetext(string scenarioId,string scenarioTitle)=>"#AUTOMATION TEST#"+ currentdate +scenarioId+"-"+ scenarioTitle+ "**PLEASE IGNORE ** NO ACTION NEEDED***";
        public static decimal MinimalDifferences => Convert.ToDecimal(0.01);
        public string randomNumber = string.Empty;
        public string currentdate => CurrentDateAndTime().ToString().Replace(":", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("AM", "").Replace("PM", "");
        private readonly Random _random = new Random();
        
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public int GenerateTestCaseNumber(int i)
        {
            return i++;
        }
        public DateTime CurrentDateAndTime()
        {
            DateTime currentDateTime = DateTime.Now;
            return currentDateTime;
        }
        public Dictionary<string, string> GetCultureInfo(string Country)
        { 
            string LanguageInfo="";
            Dictionary<string, string> cultureInfo = new Dictionary<string, string>();
            cultureInfo.Add("US","en-US");
            cultureInfo.Add("UK", "en-UK");
            //cultureInfo.Add("FinLand")
            return cultureInfo;
            
        }
        public static List<DSAPageObject> StoreOrderDetails = new List<DSAPageObject>();
        //public static IJavaScriptExecutor jse => (IJavaScriptExecutor)WebDriver;
        public  IJavaScriptExecutor jse => (IJavaScriptExecutor)WebDriver;
        #region
        public Dictionary<string, string> accessories;
        public Dictionary<string, string> services;
        public string GetAccessories(string key)
        {
            accessories = new Dictionary<string, string>();
            accessories.Add("5113", "Monitors");
            accessories.Add("7380", "AdaptersandCables");
            accessories.Add("8080", "DockingSolutions");
            return accessories[key];
        }
        public string GetServices(string key)
        {
            services = new Dictionary<string, string>();
            services.Add("29", "Hardware Support Services");
            services.Add("30", "Extended Battery Service");
            services.Add("33", "Accidental Damage");
            services.Add("159", "Keep Your Hard Drive");
            services.Add("348", "Need help installing? We can help!");
            services.Add("432", "Installation Additional Services");
            services.Add("1007", "Dell Services: Extended Battery Services");
            return services[key];
        }
        #endregion
        #region By
        public By ByMenuLabel => By.Id(/*"menu_main_label"*/"masthead_menu_title");

        public By MenuQuoteSearch => By.XPath("//span[text()='Quotes']]");
       // public By MenuQuoteSearch => By.XPath("//button[@id='menu_quoteSearch' and starts-with(text(),' Quote ')]");
        public By BySearchQuotelink => By.XPath("//*[@id='buttonDropdownPrimary']/ul/li[3]/app-menu-item[1]/li/ul/li[3]/a");
        public By ByDraftQuoteMenu => By.XPath("//*[text()='Menu']");
        public By BydraftquoteSearchLink => By.XPath("//*[@href='https://sales.dell.com/#/quote/search/']");
        public By ByAddItem => By.XPath("//button[@id='quoteCreate_additem_top_header_']");
      
        public By ByDraftSerchInputField => By.XPath("//input[@id='quoteSearch_draftQuoteNumber' and @name='draftQuoteNumber']");

        public By ByQuoteSearchButton => By.Id("quoteSearch_searchButton");

        public By BySendAnywayButton => By.XPath(/*"//button[@id='proceed-button']"*/"//*[text()='Send anyway']");

        public By ByMoreAction => By.Id("moreActionsDropdownId");

        public By ByCopyQuote => By.XPath("//ul[@id='buttonDropdownSecondary']/li/button[@id='btnCopyAsQuote']");
        
   //     public By ByListOfProducts => By.XPath("//*[starts-with(@id,'quoteCreate_LI_0_')]");//Xpath got changed
        public By ByListOfProducts => By.XPath("//*[starts-with(@id,'quoteCreate_LI_productDescription_0_')]");
        //   public By ByProduct => By.XPath("//*[@aria-label='Item 1' and @id='quoteCreate_LI_0_"+Count+"']");
  //      public By ByProduct => By.CssSelector("#quoteCreate_LI_0_" + Count + ".k-icon.k-i-arrow-s");
        public By ByProduct => By.XPath("//button[@aria-controls='quoteCreate_accordion_0_0_accordion' and @aria-expanded='false']");
        public By ByRemoveProduct(int count) => By.XPath("//*[@id='quoteCreate_LI_removeItem_0_"+count+"']");
        public By ByRemoveProductBasedOnOrderCode(string OrderCode) =>By.XPath("//div[contains(text(),'" + OrderCode + "')]/../../../../../../../../../parent::div/div/div/span/a[text()=' Copy ']/../following::span[1]");
        public By ByRemoveProductBasedOnOrderCode(int sequence) => By.XPath("//*[@id='quoteCreate_LI_removeItem_0_"+ sequence + "']");
        public By ByDSAOrderCode(int count) => By.XPath("//*[@id='quoteCreate_LI_orderCode_0_"+count+"' or @id='quoteCreate_LI_skuNumber_0_"+count+"']");
        public By ByProductServiceConfigText => By.XPath("//*[starts-with(@id,'productConfig_option_0_')]");
        public By ByServiceHeaderTexAutoSelect(string ServiceHeaderText) => By.XPath("(//span[text()='" + ServiceHeaderText + "']/preceding::span[1])[3]");
        public By ByServiceIdforAutoSelectElement(string id)=>By.XPath("(//*[@id='productConfig_"+ id + "_desc_0']/text()[2])[2]");
        public By BySelectServiceElement(string text,int i=0) => By.XPath("(//*[@id='productConfig_" + text + "_selection_"+i+"'])[2]");
        public By ByMinimizeServiceText(string text) => By.XPath("(//*[text()='" + text + "'])[2]/../div[2]");
        public By ByserviceTitle(string text) => By.XPath("(//*[@id='productConfig_" + text + "_desc_1'])[2]");
        public By ByListOfServiceListPrice(string text) => By.XPath("//*[starts-with(@id,'productConfig_0_"+text+"_listPrice_')]/div/div/span[1]");
        public By ByIsProductExpanded => By.CssSelector("#quoteCreate_LI_0_" + Count + ".k-icon.k-i-arrow-n");
        //     public By ByViewMore => By.XPath("//*[@id='toggleMoreLess_0_" + Count + "']/i[1]");
        public By ByViewMore (int count)=> By.XPath("//*[@id='toggleMoreLess_0_" + count + "']/span[text()=' View Configurations ']");
        public By ByHideConfiguration(int count) => By.XPath("//*[@id='toggleMoreLess_0_" + count + "']/span[ @class='dds__more-less__button--less' and text()=' Configuration ']/following::i[@class='dds__icon dds__ml-2 dds__icon--chevron-up']");
        public By ByViewMoreConfiguration(int count)=> By.XPath("//*[@id='toggleMoreLess_0_"+count+"']/span[@class='dds__more-less__button--more' and contains(text(),'Configuration Details')]");

        public By ByIsConfigurationTabExpanded(int count) => By.XPath("//quote-create-line-item-configuration");
        public IWebElement IsConfigurationTabExpanded(int count) => WebDriver.FindElement(By.XPath("ByIsConfigurationTabExpanded"));
        public By ByViewMoreConfiguration1(int count) => By.XPath("//*[@id='toggleMoreLess_0_" + count + "']/span[@class='dds__more-less__button--more' and text()='Configuration']/following::i[@class='dds__icon dds__ml-2 dds__icon--chevron-down']");
        public By ByConfiguration => By.XPath("//*[starts-with(@id,'toggleMoreLess_0_')]/span[@class='dds__more-less__button--more' and text()=' Configuration ']");
        public By ByViewLess=> By.XPath("//i[@class='icon-ui-expand']");
  //      public By ByViewMoreAccessory => By.XPath("//div[@id='2_LI_NTSKU_Sku_collapsible_uStDXHdwI0acpWob6N-wiQ_0_0_"+accessoriesCount+"']/a[@class='viewMore__child__nonTied' and @id='toggleMoreLess_0_0']/i[@class='icon-ui-expand']");
        public By ByViewMoreAccessory => By.XPath("//button[starts-with(@id,'toggleMoreLess_')]/span[text()='View More']");
        //public By BySkuNumber(string moduleId) =>By.XPath("//*[@id='lineitem_config_block_0_0_"+moduleId+"_0_body']/div[2]/table/tbody/tr[2]/td[3]");
        public By BySkuNumber => By.XPath("//div[@aria-hidden='false']//table//th[text()='SKU']/ancestor::table//td[3]");
        public By ByProductConfig(int itemIndex) => By.XPath("(//*[starts-with(@id,'lineitem_config_block_0_0_"+itemIndex+"_') and contains(@id,'_header')])[1]");//click on base sku//xpath changed
        public By ByFindSku(string prodDesc,int count) => By.XPath("(//div[text()='] Base: 'or text()='] " + prodDesc + ": '])["+count+"]");
        public By ByFindSkus(string prodDesc) => By.XPath("//div[contains(text(),'Base')or or text()='] " + prodDesc + " ']");
        public By ByDiscountField(int count) => By.XPath("//*[@id='quoteCreate_LI_dolPercentage_0_" + count + "']"); 
        public By ByHwSupportedServices => By.XPath("//*[@id='lineitem_config_block_0_" + Count + "']/div[35]/div/div[2]/div[2]/a");
        public By ByAddProductTextBox => By.XPath("//input[@id='quoteCreate_addText_top']");
        public By ByAddProduct => By.XPath("//button[@id='quoteCreate_addButton_top']");
        public By BySkuLabel=>By.XPath("//div[starts-with(@id,'quoteCreate_LI_skuNumber_')]");
        public By ByAccessorySKuLabel => By.XPath("//*[starts-with(@id,'2_Sku_skus_0_')]");
        public By ByServiceId(string Id) => By.XPath("//span[text()='"+Id+"']");
        public By ByProductDescription(int count) => By.XPath("//div[@id='quoteCreate_LI_productDescription_0_" + count + "']");
        public By ByAddQuantity(int i) => By.XPath("//div[@id='quoteCreate_LI_PI_quantity_0_"+i+"']/input[@id='quoteCreate_LI_quantity_0_"+i+"']");
        public By ByQuantityLabel => By.XPath("//*[@id='quoteCreate_accordion_0_" + Count + "']/div[4]/div/div[1]/div[2]/quote-create-line-item-quantity/line-item-quantity/div/div[1]/div[1]/span");
        public By ByDellIcon => By.XPath("//*[@id='DellIcon']");
        public By ByOrderCode(string OrderCode) => By.XPath("//*[contains(text(),'Order Code')]/following-sibling::div[contains(text(),'" + OrderCode + "')]");
        public By ByQuantityChangeSpecificOrder(string OrderCode) => By.XPath("//label[contains(text(),'Order Code')]/following-sibling::div[contains(text(),'" + OrderCode + " ')]/../../../../../../../../../div/div/div[2]/quote-create-line-item-quantity/line-item-quantity//input[1]");

        public By ByApplyMode => By.XPath("//*[@class='col-md-9 mg-top-20']/div[2]/child::*/div/button/span");
        public By ByApplyChangesMenuItemObj => By.XPath("//button[@id='priceModeDropdown']/span");
        public By BySelectManual => By.XPath("//*[@id='quoteCreate_saveOrder2' and text()=' Manual ']");
        public By BySelectStandard => By.XPath("//*[@id='quoteCreate_saveOrder1' and text()=' Standard ']");
        public By ByClickOntheStandard => By.XPath("//*[@id='quoteCreate_lineHeader']/div/div/pricing-mode-toggler/div/div/button");
        public By ByApplyChanges => By.XPath("//*[text()=' Apply Changes ']");
        public By ByProductTitle => By.XPath("//*[@id='quoteCreate_LI_productDescription_0_" + Count + "']");
        public By ByDraftQuoteNumber => By.XPath("//*[@id='quoteCreate_title_draftquote']/small[1]");
        public By ByConfigItem => By.XPath("//*[@id='quoteCreate_LI_configItem_0_" + Count + "']");
        public By ByConfigItemForSpecificOrderCode(string OrderCode) => By.XPath("//label[contains(text(),'Order Code')]/following-sibling::div[contains(text(),'" + OrderCode + "')]/../../../../../../../../../parent::div/div/div/span/a[@id='quoteCreate_LI_configItem_0_" + Count + "']");
        public By ByAccessoriesMenu => By.XPath("//*[text()='Accessories']");
        public By ByExtendService(string  serviceId) => By.XPath("//*[text()='"+serviceId+"'])[2]");
        public By ByServicesMenu => By.XPath("(//*[text()='Service'])[1]");
        public By BySummary => By.XPath("(//*[text()='Summary'])[1]");

        public By BySystemMenu => By.XPath("(//*[text()='System'])[1]");
        TestCase testCase = new TestCase();
        public By ByClickOnAccessories(string test, string accessoriesName) => By.XPath("//div[@class='col-md-3 offset-0']/div/label[@id='productConfig_option_" + test + "']/../../following::div/div/div/div/div/div/a[@id='productConfig_option_0_" + accessoriesName + "_toggle']");
        public By ByClickOnServices(string test, string serviceName) => By.XPath("//div[@class='col-md-3 offset-0']/div/label[@id='productConfig_option_" + test + "']/../../following::div/div/div/div/div/div/a[@id='productConfig_option_0_" + serviceName + "_toggle']");
        public By ByClickOnServiceModule(string test) => By.XPath("(//label[@id='productConfig_option_"+test+ "']/ancestor::div[@class='dds__row dds__mx-0']/div[2]//button[1])[2]");
        public By ByClickOnHeaderModule(string test) => By.XPath("(//label[@id='productConfig_option_" + test + "']/../../../div[2]//button[1])[2]");
        public By ByClickOnHeaderModule(string test,string headerText) => By.XPath("(//label[@id='productConfig_option_"+test+"']/../../../div[2]//button[contains(@id,'"+ headerText + "')])[2]");
        public By ByGetTitleWithModule(string test, string headerText) => By.XPath("(//label[@id='productConfig_option_" + test + "']/../../../div[2]//button[contains(@id,'" + headerText + "')]/div[2])[2]");

        public By ByCheckAccessory(string accessoriesName) => By.XPath("//*[@id='productConfig_" + accessoriesName + "_desc_0']/text()[2]");
        public By BySelectAccessory(string accessoryId) => By.XPath("//*[text()='" + accessoryId + "']/preceding-sibling::td/input[1]");
        public By BySelectService(string serviceId) => By.XPath("(//table/tbody/tr/td/span[contains(text(),'"+serviceId+"')])[2]");
        public By ByViewQuote => By.XPath("//button[@id='productConfig_viewQuote']");
        public By ByProductViewQuote => By.XPath("//button[@id='products_viewquote']");
        
        //      public By ByViewQuote => By.XPath("//button[@id='products_viewquote']"); 
        public By ByCurrentQuote => By.XPath("//*[@id='menu_currentQuote']");
        public By ByServieTitle(string serviceId) => By.XPath("(//*[text()='"+ serviceId + "'])[2]");
        public By ByMinimizeText( string text) => By.XPath("(//*[@class='module-desc' and text()='"+text+"'])[2]");
        public By ByUnitListPriceAccessory(int count,int index) => By.XPath("//*[@id='2_Sku_PI_unitPrice_0_"+index+"_"+count+"']");
        public By ByUnitListPriceForAccessory => By.XPath("//*[starts-with(@id,'2_Sku_PI_unitPrice_')]");
        public By ByUnitDiscountAccessory => By.XPath("//*[starts-with(@id,'2_Sku_PI_dol_0_')]");
        public By ByUnitCostAccessory => By.XPath("//non-tied-sku-qty/div/label[text()='Cost:']/following::div[1]");
        public By ByUnitSellingPriceAcessory=> By.XPath("//div[starts-with(@id,'2_Sku-PI_unitSellingPrice_')]//input[starts-with(@id,'quoteCreate_LI_unitSellingPrice_')]");

        public By ByUnitSmartPriceRevenue => By.XPath("//*[starts-with(@id,'2_Sku_PI_unitCompRevenue_0_')]");
        public By ByUnitMarginAccessory=> By.XPath("//*[starts-with(@id,'2_Sku_PI_unitMargin_0_')]");
        public By ByQuantityAccessory=> By.XPath("//*[starts-with(@id,'quoteCreate_LI_quantity_0_')]");
        public By ByTotalListPriceAccessory=> By.XPath("//*[starts-with(@id,'2_totalListPrice_0_')]");
        public By ByTotalCostAccessory => By.XPath("//*[starts-with(@id,'quoteCreate_accordionMore_')]/div[3]/div[1]/quote-create-non-tied-sku/non-tied-sku/div[1]/quote-create-non-tied-sku-qty/non-tied-sku-qty/div/div[3]/div");

        public By ByTotalDiscountAccessory => By.XPath("//*[starts-with(@id,'2_totalDiscountValue_0_')]");
        public By ByTotalSellingPriceAccessory => By.XPath("//*[starts-with(@id,'2_Sku_Quantity_0_')]");
        public By ByTotalMarginAccessory => By.XPath("//*[starts-with(@id,'2_Sku_Quantity_0_')]");

        public By ByDOLPercentage => By.XPath("//div[starts-with(@id,'quoteCreate_Sku_PI_unitPercent2_0_')]/input[starts-with(@id,'quoteCreate_LI_dolPercentage_0_')]");

        public By ByRecommenededSellingPrice = By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[1]");
        public By ByRecommenededDiscount = By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[2]");
        public By ByRecommendedSmartPricRevenue = By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[3]");
        public By ByRecommendedPricingModifier = By.XPath("//smart-price-guidance//td[text()=' Recommended ']/following-sibling::td[4]");

        public By ByCompAnchorSellingPrice = By.XPath("//smart-price-guidance//tr[2]/td[2]");
        public By ByCompAnchorDiscount = By.XPath("//smart-price-guidance//tr[2]/td[3]");
        public By ByCompAnchorSmartPricRevenue = By.XPath("//smart-price-guidance//tr[2]/td[4]");
        public By ByCompAnchorPricingModifier = By.XPath("//smart-price-guidance//tr[2]/td[5]");

        public By ByFloorSellingPrice = By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[1]");
        public By ByFloorDiscount = By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[2]");
        public By ByFloorPricingModifier = By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[4]");
        public By ByFloorSmartPricRevenue = By.XPath("//smart-price-guidance//td[text()=' Floor ']/following-sibling::td[3]");
        public By ByAccessoryTitle => By.XPath("//*[starts-with(@id,'2_Sku_cssiNumber_0_0_')]");
        public By ByNone => By.XPath("//*[text()='None' or text()='None Selected']");
        public By ByServiceConfig(int i) => By.XPath("(//input[@type='radio'])["+i+"]");
        public By ByServiceConfigText(int i) => By.XPath("(//input[@type='radio'])["+i+"]/following::td[1]");
        public By ByServiceHeaderText => By.XPath("(//table/thead/tr/th/span)[3]");
        public By ByServiceElement => By.XPath("//div[@id='quoteCreate_LI_CS_grid_0_0']/div//div[contains(text(),'Service')]/../div[2]/a[1]");
   //     public By ByConfigTable => By.XPath("//table[@class='config-option-sku-table']/tbody");
        public By ByConfigTable(string moduleId) => By.XPath("//div[starts-with(@id,'lineitem_config_block_0_0_"+moduleId+"_') and contains(@id,'_body')]/div[2]/table/tbody");
        //public By ByServiceConfigSku (int i)=> By.XPath("//table[@class='config-option-sku-table']/tbody/tr/td[count(//table[@class='config-option-sku-table']/tbody/tr/th[text()='SKU']/preceding-sibling::th)+1]");
        public By ByServiceConfigSku(string moduleId,int i) => By.XPath("//div[starts-with(@id,'lineitem_config_block_0_0_" + moduleId + "_') and contains(@id,'_body')]/div[2]/table/tbody["+i+"]/tr[2]/td[3]");
        public By ByServiceUnitListPrice(string moduleId,int i) => By.XPath("//div[starts-with(@id,'lineitem_config_block_0_0_" + moduleId + "_') and contains(@id,'_body')]/div[2]/table/tbody["+i+"]/tr[2]/td[4]");
        public By ByServiceUnitCost(string moduleId,int i) => By.XPath("//div[starts-with(@id,'lineitem_config_block_0_0_" + moduleId + "_') and contains(@id,'_body')]/div[2]/table/tbody["+i+"]/tr[2]/td[5]");
        public By ByServiceUnitSellingPrice(string moduleId,int i) => By.XPath("//div[starts-with(@id,'lineitem_config_block_0_0_" + moduleId + "_') and contains(@id,'_body')]/div[2]/table/tbody[" + i + "]/tr[2]/td[8]");

        public By ByConfigServiceListPrice(string serviceId) => By.XPath("(//*[contains(text(),'"+serviceId+"')]/../../td[5])[2]");
        public By ByConfigServiceListPriceCheckBox(string serviceId) => By.XPath("(//*[contains(text(),'" + serviceId + "')]/ancestor::tr/td[contains(@id,'listPrice')])[2]");
        public By ByConfigServiceListPriceAfterDiscount(string serviceId)=> By.XPath("(//*[contains(text(),'"+serviceId+ "')]/ancestor::tr/td[contains(@id,'listPrice')]//div/span[@class='greenText'])[2]");
        
        #endregion
        #region TiedAccesoriesWebElement
        public IWebElement UnitListPrice_Accessory(int count,int index) => WebDriver.FindElement(ByUnitListPriceAccessory(count,index));
        public List<IWebElement> UnitListPrice_Accessorylist => WebDriver.FindElements(ByUnitListPriceForAccessory).ToList();
        public List<IWebElement> unitDiscount_Accessory => WebDriver.FindElements(ByUnitDiscountAccessory).ToList();
       
        public List<IWebElement> UnitCost_Accessory => WebDriver.FindElements(ByUnitCostAccessory).ToList();// write List
        public List<IWebElement> UnitSellingPrice_Accessory => WebDriver.FindElements(ByUnitSellingPriceAcessory).ToList();
        public List<IWebElement>  UnitSmartPriceRevenue_Accessory=> WebDriver.FindElements(ByUnitSmartPriceRevenue).ToList();
        public List<IWebElement> UnitMargin_Accessory => WebDriver.FindElements(ByUnitMarginAccessory).ToList();
        public List<IWebElement> TotalListPrice_Accessory => WebDriver.FindElements(ByTotalListPriceAccessory).ToList();
        public List<IWebElement> TotalCostPrice_Accessory => WebDriver.FindElements(ByTotalCostAccessory).ToList();
        public List<IWebElement> TotalDiscount_Accesory => WebDriver.FindElements(ByTotalDiscountAccessory).ToList();
        public List<IWebElement> TotalSellingPrice_Accesory => WebDriver.FindElements(ByTotalSellingPriceAccessory).ToList();
        public List<IWebElement> TotalMargin_Accessory => WebDriver.FindElements(ByTotalMarginAccessory).ToList();

        public List<IWebElement> DOLPercentage => WebDriver.FindElements(ByDOLPercentage).ToList();

        public IWebElement RecommendedSellingPrice => WebDriver.FindElement(ByRecommenededSellingPrice);
        public IWebElement RecommenededDiscount => WebDriver.FindElement(ByRecommenededDiscount);
        public IWebElement RecommendedSmartPricRevenue => WebDriver.FindElement(ByRecommendedSmartPricRevenue);
        public IWebElement RecommendedPricingModifier => WebDriver.FindElement(ByRecommendedPricingModifier);


        public IWebElement CompAnchorSellingPrice => WebDriver.FindElement(ByCompAnchorSellingPrice);
        public IWebElement CompAnchorDiscount => WebDriver.FindElement(ByCompAnchorDiscount);
        public IWebElement CompAnchorSmartPricRevenue => WebDriver.FindElement(ByCompAnchorSmartPricRevenue);
        public IWebElement CompAnchorPricingModifier => WebDriver.FindElement(ByCompAnchorPricingModifier);

        public IWebElement FloorSellingPrice => WebDriver.FindElement(ByFloorSellingPrice);
        public IWebElement FloorDiscount => WebDriver.FindElement(ByFloorDiscount);
        public IWebElement FloorSmartPricRevenue => WebDriver.FindElement(ByFloorSmartPricRevenue);
        public IWebElement FloorPricingModifier => WebDriver.FindElement(ByFloorPricingModifier);
       // public IWebElement ViewMore => WebDriver.FindElement(ByViewMore);
        public List<IWebElement> ViewMoreAccessory => WebDriver.FindElements(ByViewMoreAccessory).ToList();
        public List<IWebElement> SkuNumber=> WebDriver.FindElements(BySkuNumber).ToList();
        public List<IWebElement> ServiceElemenet => WebDriver.FindElements(ByServiceElement).ToList();
        public List<IWebElement> configTable(string moduleId) =>  WebDriver.FindElements(ByConfigTable(moduleId)).ToList();
        
        public IWebElement ServiceConfigSku( string moduleId,int i)=> WebDriver.FindElement(ByServiceConfigSku(moduleId,i));
        public IWebElement ServiceUnitListPrice(string moduleId,int i) => WebDriver.FindElement(ByServiceUnitListPrice(moduleId,i));
        public IWebElement ConfigServiceListPrice(string text) => WebDriver.FindElement(ByConfigServiceListPrice(text));
        public IWebElement ConfigServiceListPriceCheckBox(string text) => WebDriver.FindElement(ByConfigServiceListPriceCheckBox(text));
        public IWebElement ConfigServiceListPriceAfterDiscount(string text) => WebDriver.FindElement(ByConfigServiceListPriceAfterDiscount(text));
        public IWebElement ServiceUnitCost(string moduleId,int i) => WebDriver.FindElement(ByServiceUnitCost(moduleId,i));
        public IWebElement ServiceUnitSellingPrice(string moduleId,int i) => WebDriver.FindElement(ByServiceUnitSellingPrice(moduleId,i));
        public IWebElement ServiceConfigText(int i) => WebDriver.FindElement(ByServiceConfigText(i));
        public IWebElement ServiceHeaderText => WebDriver.FindElement(ByServiceHeaderText);
        public IWebElement ViewQuote => WebDriver.FindElement(ByViewQuote);
        public IWebElement ProductViewQuote => WebDriver.FindElement(ByProductViewQuote);

        #endregion
        #region
        public IWebElement ServiceMenu =>WebDriver.FindElement(ByServicesMenu);
        public IWebElement SummaryMenu => WebDriver.FindElement(BySummary);
        #endregion
        #region 
        public IWebElement MenuButton => WebDriver.FindElement(ByMenuLabel);

        public IWebElement clickOnQuote => WebDriver.FindElement(MenuQuoteSearch);
        public IWebElement ClickOnSearchQuote => WebDriver.FindElement(BySearchQuotelink);//Click on the  again Quote link when no quote is found .
        public IWebElement DraftQuoteMenu => WebDriver.FindElement(ByDraftQuoteMenu);
        public IWebElement draftquoteSearchLink => WebDriver.FindElement(BydraftquoteSearchLink);

        public IWebElement AddItem => WebDriver.FindElement(ByAddItem);
       
        public IWebElement SearchDraftQuoteNumber => WebDriver.FindElement(ByDraftSerchInputField);

        public IWebElement QuoteSearchButton => WebDriver.FindElement(ByQuoteSearchButton);

        public IWebElement SendAnywayButton => WebDriver.FindElement(BySendAnywayButton);

        public IWebElement MoreActionsDropdown => WebDriver.FindElement(ByMoreAction);

        public IWebElement CopyAsNewQuote => WebDriver.FindElement(ByCopyQuote);

        public List<IWebElement> NoOfProducts => WebDriver.FindElements(ByListOfProducts).ToList();

        public IWebElement Product=> WebDriver.FindElement(ByProduct);
        public IWebElement IsProductExpanded => WebDriver.FindElement(ByIsProductExpanded);

        public IWebElement ViewMore(int count) => WebDriver.FindElement(ByViewMore(count));
        public IWebElement ViewLess=> WebDriver.FindElement(ByViewLess);
        public IWebElement Configuration(int count) => WebDriver.FindElement(ByHideConfiguration(count));
        public IWebElement ViewConfiguration(int count) => WebDriver.FindElement(ByViewMoreConfiguration(count));
        public List<IWebElement> DSAConfiguration => WebDriver.FindElements(ByConfiguration).ToList();

        public  IWebElement ProductConfiguration(int itemIndex) => WebDriver.FindElement(ByProductConfig(itemIndex));
        public IWebElement FindSku(string productDescription,int count) => WebDriver.FindElement(ByFindSku(productDescription,count));
        public List<IWebElement> FindSkus(string prodDesc) => WebDriver.FindElements(ByFindSkus(prodDesc)).ToList();
        public IWebElement DiscountField(int count) =>WebDriver.FindElement(ByDiscountField(count));
        public IWebElement HardwaresupportObject => WebDriver.FindElement(ByHwSupportedServices);
        public IWebElement AddProductTextBoxObject=> WebDriver.FindElements(ByAddProductTextBox).FirstOrDefault();
        public IWebElement AddProductButtonbject => WebDriver.FindElements(ByAddProduct).FirstOrDefault();
        public List<IWebElement> SkuLabel => WebDriver.FindElements(BySkuLabel).ToList();
        public List<IWebElement> AccessorySKuLabel => WebDriver.FindElements(ByAccessorySKuLabel).ToList();
        public List<IWebElement> listOfServiceId(string Id) => WebDriver.FindElements(ByServiceId(Id)).ToList();
        public IWebElement ProductDescriptin(int count) => WebDriver.FindElement(ByProductDescription(count));
        public IWebElement IncreaseQuantity(int i) => WebDriver.FindElement(ByAddQuantity(i));
        public IWebElement DellIcon => WebDriver.FindElement(ByDellIcon);
        public IWebElement OrderCode(TestCase test) => WebDriver.FindElement(ByOrderCode(test.OrderCode));
        public IWebElement OrderCode(string ordercode) => WebDriver.FindElement(ByOrderCode(ordercode));
        public IWebElement QuantitySpecificOrder(TestCase test) => WebDriver.FindElement(ByQuantityChangeSpecificOrder(test.OrderCode));
      //  public List<IWebElement> ApplyChanges => WebDriver.FindElements(ByApplyMode).ToList();
        public IWebElement ApplyChangesItem => WebDriver.FindElement(ByApplyChangesMenuItemObj);
        public IWebElement SelectManual => WebDriver.FindElement(BySelectManual);
        public IWebElement ClickOntheManualandStandard => WebDriver.FindElement(ByClickOntheStandard);
        public IWebElement SelectStandard => WebDriver.FindElement(BySelectStandard);
        public IWebElement ApplyChanges => WebDriver.FindElement(ByApplyChanges);

        public IWebElement QuantityLabel => WebDriver.FindElement(ByQuantityLabel);

        public IWebElement ProductTitle => WebDriver.FindElement(ByProductTitle);
        public string DraftQuote => WebDriver.FindElement(ByDraftQuoteNumber).Text;
       public IWebElement ConfigItem=> WebDriver.FindElement(ByConfigItem);
        public IWebElement ConfigItemForSpecificOrderCode(string test) => WebDriver.FindElement(ByConfigItemForSpecificOrderCode(test));
        public IWebElement AccessoriesMenu => WebDriver.FindElements(ByAccessoriesMenu).FirstOrDefault();
        public IWebElement ClickOnAccessories(TestCase test) => WebDriver.FindElements(ByClickOnAccessories(test.ModuleId.ToString(), GetAccessories(test.ModuleId))).LastOrDefault();
        public IWebElement ClickOnServicesWithModuleId(string ModuleId) => WebDriver.FindElement(ByClickOnServiceModule(ModuleId));
        public IWebElement ClickOnTitleWithModuleId(string ModuleId,string headerText) => WebDriver.FindElement(ByClickOnHeaderModule(ModuleId,headerText));
        public IWebElement ClickOnTitleWithModuleId(string ModuleId) => WebDriver.FindElement(ByClickOnHeaderModule(ModuleId));
        public IWebElement GetTitleWithModuleId(string ModuleId, string headerText) => WebDriver.FindElement(ByGetTitleWithModule(ModuleId, headerText));

        public IWebElement CheckAccessories(TestCase test) => WebDriver.FindElements(ByCheckAccessory(GetAccessories(test.ModuleId))).LastOrDefault();
        public IWebElement ExtendService(TestCase test) =>WebDriver.FindElement(ByExtendService(test.ServiceId));
        public IWebElement SelectAccessory(TestCase test) => WebDriver.FindElements(BySelectAccessory(test.AccessoriesId.ToString())).LastOrDefault();
        public IWebElement SelectServiceId(string OfferingId) => WebDriver.FindElements(BySelectService(OfferingId)).LastOrDefault();
        public IWebElement CurrentQuote => WebDriver.FindElement(ByCurrentQuote);
        public IWebElement ServiceTitle(string OfferingId) => WebDriver.FindElement(ByServieTitle(OfferingId));
        public IWebElement MinimizeText(string test) => WebDriver.FindElement(ByMinimizeText(test));
        public List<IWebElement> AccessoryTitle => WebDriver.FindElements(ByAccessoryTitle).ToList();
        public List<IWebElement> NoneElement => WebDriver.FindElements(ByNone).ToList();
        public IWebElement ServiceConfig(int i) => WebDriver.FindElement(ByServiceConfig(i));

        public IWebElement RemoveProduct(string OrderCode) => WebDriver.FindElement(ByRemoveProductBasedOnOrderCode(OrderCode));
        public IWebElement RemoveProduct(int sequence) => WebDriver.FindElement(ByRemoveProductBasedOnOrderCode(sequence));
        public IWebElement DSAOrderCode(int productCount) =>WebDriver.FindElement(ByDSAOrderCode(productCount));

        public List<IWebElement>  ProductServiceConfigText=>WebDriver.FindElements(ByProductServiceConfigText).ToList();
        //Serach for the module ID 
        public IWebElement ModuleIDforAutoSelectElement(string ServiceHeaderText) => WebDriver.FindElement(ByServiceHeaderTexAutoSelect(ServiceHeaderText));
        public IWebElement ServiceIdforAutoSelectElement(string serviceId) => WebDriver.FindElement(ByServiceIdforAutoSelectElement(serviceId));
        //random service element
        public IWebElement SelectServiceElement(string text,int i) => WebDriver.FindElement(BySelectServiceElement(text,i));
        public IWebElement MinimizeServiceMenu(string text) => WebDriver.FindElement(ByMinimizeServiceText(text));
        public IWebElement ServiceTitlle(string text) => WebDriver.FindElement(ByserviceTitle(text));
      //  public IWebElement ServiceListPrice(string text,int i) => WebDriver.FindElement(ByListOfServiceListPrice(text,i));
        public List<IWebElement> ServiceListPrice(string text) => WebDriver.FindElements(ByListOfServiceListPrice(text)).ToList();
        #endregion
        #region
        public bool IsElementPresent(By locatorKey)
        {
            try
            {
                WebDriver.FindElement(locatorKey);
                return true;
            }
            catch (OpenQA.Selenium.NoSuchElementException ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
        }
        //public Boolean IsElementSelected(By locatorKey)
        //{
        //    try
        //    {
        //        WebDriver.FindElement(locatorKey).Selected;

        //    }
        //    catch (OpenQA.Selenium.ElementNotSelectableException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
        public void ClickOnConfigurationViewQuote()
        {
            try
            {
                if (IsElementPresent(ByViewQuote))
                    ViewQuote.Click();
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.InvisibilityOfElementLocated(ByViewQuote));
            }
            catch(Exception ex)
            {
                throw new ShowStopperException("Unable to click on View QUote in configuration page , Please refer ClickOnConfigurationViewQuote() in Constant class", ex);
            }
        }
        public Boolean IsElementVisible(String cssLocator)
        {
            return WebDriver.FindElement(By.CssSelector(cssLocator)).Displayed;
        }
        public void ExpandProductfromShippingGroup(IWebDriver driver)
        {
             WebDriver= driver;
            try
            {
                Thread.Sleep(1000);
                if (IsElementPresent(By.XPath("//*[@id='quoteCreate_groupAccordion_0_header']//button[@aria-expanded='false']/div[2]/i")))
                {
                    WebDriverUtils.ScrollIntoView(WebDriver, By.XPath("//*[@id='quoteCreate_groupAccordion_0_header']/button/div[2]/i"));
                    IWebElement clickElement = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_groupAccordion_0_header']/button/div[2]/i"));
                    jse.ExecuteScript("arguments[0].click()", clickElement);

                    Thread.Sleep(3000);
                }
            }
            catch(Exception ex)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void GoToAddItemPage()
        {
            Product product = new Product(WebDriver);
            ShippingGroup shiippingGrp = new ShippingGroup(WebDriver);
            try
            {
              if(IsElementPresent(ByAddItem))
                {
                    AddItem.Click();
                    WebDriverUtils.WaitForElementVisible(WebDriver, product.ByOrderCodeInputField,5);
                    product.ProductviewQuote.Click();
                    WebDriverUtils.WaitForElementVisible(WebDriver, shiippingGrp.ByShippingGroup, 3);
                }
            }
            catch(Exception ex)
            {

            }


        }
        public Constant HideConfiguration(int index)
        {
            try
            {

                if (IsElementPresent(ByHideConfiguration(index)))
                {
                    Configuration(index).Click();
                }
                else if(IsElementPresent(ByViewMoreConfiguration(index)))
                {
                    ViewConfiguration(index).Click();
                }
                else
                {
                    jse.ExecuteScript("arguments[0].scrollIntoView();", Configuration(index));
                }      
                

            }

            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unable to click on HideConfiguration {0}",ex.Message));
                Bedrock.Utilities.SimpleLogger.LogMessage(string.Format("Unable to click on HideConfiguration {0}", ex.StackTrace));
            }

            return new Constant(WebDriver);
        }
    #endregion
}
}
