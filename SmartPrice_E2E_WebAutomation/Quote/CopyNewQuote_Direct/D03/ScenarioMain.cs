using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.IO;
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.Helper;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D03.ScenarioMain
{
    public class ScenarioMain :D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;
            

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        {
           

        }
       



        public void ExecuteTestcases(TestCase test, string ScenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            try
            {

               

                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                    ds.PnrLog.ServiceType = "1";
                }

                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewQuote();
                }

               //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//without module id and with out component ID
                {
                    UpGradeComponent(cs, ds,WebDriver);

                }
                if (test.ModuleId!= "" && string.IsNullOrEmpty(test.ComponentId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeComponent(cs, test, ds, WebDriver);

                    }
                    else
                    {
                        //Console.WriteLine("Product is not available , Services  can not be added.");
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);
                    }
                }

                if (test.ModuleId !="" && !string.IsNullOrEmpty(test.ComponentId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeComponent(cs, test.ModuleId, test.ComponentId, ds, WebDriver);

                    }
                    else
                    {

                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);


                    }
                }



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules,ScenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioId, retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }

        }
        /// <summary>
        /// Auotomatic component upgrade
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="ds"></param>
        /// <param name="configurationPage"></param>
        public void UpGradeComponent(Constant constant, DSAPageObject ds,IWebDriver driver)
        {
            WebDriver = driver;
            ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
            try
            {

                configurationPage.ClickOnConfigItem(ds.OrderCode);

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(constant.BySummary));
                WebDriver.ScrollIntoView(constant.SummaryMenu);
                constant.ServiceMenu.Click();
                string moduleHeaderText = "";
                decimal ServiceListPrice = 0;
                decimal listPrice = 0;
                string ModuleId = "";
                string ServiceId = "";
                string servicedesc = "";
                string serviceTitle = "";
                decimal componentListPrice = 0;
                // Find all base service listprice,title,module id..
                configurationPage.RetriveBaseServiceDetails(constant, ds, serviceTitle, ServiceListPrice, null, null, ds.PnrLog.ServiceType);
               
                WebDriver.ScrollIntoView(configurationPage.BySystemMenu);
                configurationPage.SystemMenu.Click();// Click on System Tab

                foreach (IWebElement moduleTitle in configurationPage.ComponentModuleTitle)
                {
                    bool isclicked = false;
                    bool isFound = false;
                    if (moduleTitle.Displayed)
                    {
                        moduleHeaderText = moduleTitle.Text;
                        ModuleId = moduleTitle.FindElement(By.XPath("//span[1]")).Text;//Find Module ID
                        string[] moduleText = moduleTitle.Text.Replace(" ", "").Split(']');
                        for (int i = 0; i < moduleText.Length; i++)
                        {
                            ModuleId = moduleText[i].Replace("[", "");
                            moduleHeaderText = moduleText[i + 1];
                            break;
                        }
                        configurationPage.FindComponentTitle(moduleHeaderText).Click();//click on the component title

                        // SelectListPrice(constant,configurationPage, ref componentListPrice);
                        configurationPage.SelectListPrice(ref componentListPrice);

                        Actions action = new Actions(WebDriver);

                   //     Constant.jse.ExecuteScript("window.scrollBy(0,-300)");
                        foreach (IWebElement list in configurationPage.AllServiceListPrice(moduleHeaderText))
                        {

                            if (list.Enabled && list.Displayed)
                            {
                                action.MoveToElement(list);

                                if (constant.IsElementPresent(configurationPage.ByServiceOptionClickBasedOnListPrice(list.Text)))
                                {
                                    Thread.Sleep(3000);
                                    if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(list.Text)) > componentListPrice)
                                    {
                                        componentListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(list.Text));
                                        configurationPage.ServiceOptionClickBasedOnListPrice(list.Text).Click();//clik on radio button
                                        Thread.Sleep(8000);                                                              // ComponentTitle = ComponentTitleBasedOnListPrice(serviceOption.Text);
                                        //GreenTextElement    //need to be implemented later                                                                                                                                                                                                         //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;
                                        
                                        action.MoveToElement(constant.ClickOnServicesWithModuleId(ModuleId));

                                        //ComponentTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;
                                        //      Constant.jse.ExecuteScript("window.scrollBy(0,-400)");
                                 //      Constant.jse.ExecuteScript("window.scrollBy(0,400)");
                                        //componentListPrice = Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", ""));
                                        constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);",moduleTitle.FindElement(By.XPath("//span[1]")));
                                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementIsVisible(configurationPage.ByComponentTitle(moduleHeaderText)));
                                        configurationPage.FindComponentTitle(moduleHeaderText).Click();//to monimize the component
                                        string ComponentTitle = configurationPage.FindComponentTitle(moduleHeaderText).Text;
                                        ds.AddComponent(ModuleId, moduleHeaderText, ComponentTitle, componentListPrice, 0, true, "0");
                                        //Console.WriteLine("Component is upgraded");
                                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Component is upgraded", true);
                                        isFound = true;
                                        break;
                                    }
                                }
                            }

                        }


                    }
                    if(isFound)
                    {
                        break;
                    }
                }
            }



            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }


            constant.CurrentQuote.Click();
            Thread.Sleep(20000);
        }
        
      



        /// <summary>
        /// With ModuleId
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void UpGradeComponent(Constant locator, TestCase test, DSAPageObject ds, IWebDriver driver)
        {
            WebDriver = driver;
            try
            {
                
                ConfigurationPage configPage = new ConfigurationPage(WebDriver);
                string ComponentTitle = "";
                decimal componentListPrice = 0;
                

                configPage.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
         //       locator.jse.ExecuteScript("window.scrollBy(0, 800)");
                WebDriverUtils.ScrollIntoView(WebDriver, locator.ByServicesMenu);
                locator.ServiceMenu.Click();//click on Service Tab

                configPage.RetriveBaseServiceDetails(locator, ds, ComponentTitle, componentListPrice, test.ModuleId, test.OfferingId, ds.PnrLog.ServiceType);// add service ID 
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(configPage.BySystemMenu));
                WebDriver.ScrollIntoView(configPage.BySystemMenu);
                configPage.SystemMenu.Click();

                // Click on System Tab
          //      locator.jse.ExecuteScript("window.scrollBy(0, 200)");
                try
                {
                    if (!string.IsNullOrEmpty(test.ModuleId))
                    {
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(6)).Until(ExpectedConditions.ElementIsVisible(locator.ByClickOnServiceModule(test.ModuleId)));
                        locator.ClickOnServicesWithModuleId(test.ModuleId).Click();//click on Module ID associated with the service
                        ComponentTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;

                        string headerText = configPage.Moduletext(test.ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        string moduleTitle = headerText.Replace(" ", "").Replace("/", "");
                        configPage.SelectListPrice(ref componentListPrice);
                        
                        Actions action = new Actions(WebDriver);

                        locator.jse.ExecuteScript("window.scrollBy(0,-300)");
                        foreach (IWebElement listPrice in configPage.AllServiceListPrice(moduleTitle))
                        {


                            action.MoveToElement(listPrice);

                            if (listPrice.Enabled && listPrice.Displayed && locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(listPrice.Text)))
                            {
                                Thread.Sleep(3000);
                                if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text)) > componentListPrice)
                                {
                                    string compolistPrice = listPrice.Text;
                                    
                                    configPage.ServiceOptionClickBasedOnListPrice(listPrice).Click();//clik on radio button
                                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementIsVisible(locator.ByClickOnServiceModule(test.ModuleId)));                                                                  // ComponentTitle = ComponentTitleBasedOnListPrice(serviceOption.Text);
                                    
                                    action.MoveToElement(locator.ClickOnServicesWithModuleId(test.ModuleId));
                                    componentListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text));
                                    //ComponentTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;
                                    locator.jse.ExecuteScript("window.scrollBy(0,-400)");
                                    //componentListPrice = Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", ""));
                                    IWebElement elemen = WebDriver.FindElement(By.XPath("(//label[@id='productConfig_option_" + test.ModuleId + "']/../../../div[3]//div[@class='module-desc'])[2]"));
                                    ComponentTitle = elemen.Text;
                                    //      locator.MinimizeServiceMenu(ComponentTitle).Click();
                                    locator.jse.ExecuteScript("arguments[0].click();", locator.MinimizeServiceMenu(ComponentTitle));

                                    ds.AddComponent(test.ModuleId, headerText, ComponentTitle, componentListPrice, 0, true, "0");
                                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Component is upgraded", true);
                                    break;
                                }
                            }
                        }





                    }
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Module Id is not present", true);
                    }
                }
                
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                    //throw new NoSuchElementException(@"[ModuleId is not in present on the Sevice Configuration Screen]", locator.ClickOnServicesWithModuleId(test.ModuleId));
                }
                Thread.Sleep(15000);

                locator.CurrentQuote.Click();
                Thread.Sleep(20000);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Upgarde Service with Module Id and Service Id
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="ModuleId"></param>
        /// <param name="OfferingId"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void UpGradeComponent(Constant locator, string ModuleId, string OfferingId, DSAPageObject ds,IWebDriver driver)
        {
            WebDriver = driver;
            ConfigurationPage configPage = new ConfigurationPage(WebDriver);
            try
            {
                string ServiceTitle = "";
                decimal ServiceListPrice = 0;
                string moduleTitle = "";

                configPage.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                locator.jse.ExecuteScript("window.scrollBy(0, 1200)");
                //int x= locator.ServiceMenu.Location.X;
                // int y= locator.ServiceMenu.Location.Y;
                // locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])",x,y);
                WebDriver.ScrollIntoView(configPage.BySystemMenu);
                locator.ServiceMenu.Click();


                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, null, null);//base
                
                locator.jse.ExecuteScript("arguments[0].scrollIntoView(true);",configPage.SystemMenu);
                locator.jse.ExecuteScript("window.scrollBy(0, 600)");
                configPage.SystemMenu.Click();

                if (locator.IsElementPresent(locator.ByClickOnServiceModule(ModuleId)))
                {
                    locator.ClickOnServicesWithModuleId(ModuleId).Click();//click on Module ID associated with the service
                    locator.jse.ExecuteScript("window.scrollBy(0,600)");
                    if (locator.IsElementPresent(locator.BySelectService(OfferingId)))
                    {
                        locator.SelectServiceId(OfferingId).Click();

                        Thread.Sleep(10000);
                        ds.ServiceHeaderText = configPage.Moduletext(ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                                                                                    //moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                        ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(OfferingId).Text));
                        ServiceTitle = locator.ServiceTitle(OfferingId).Text;

                        locator.jse.ExecuteScript("window.scrollBy(0,-200)");
                    //    locator.MinimizeText(ServiceTitle).Click();
                        ds.AddComponent(ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, null);
                        Console.WriteLine("Component is upgraded");
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Component is upgraded", true);
                    }
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Service id is not present on the screen", true);
                        //Console.WriteLine("Service id is not present on the screen");
                    }
                }
                else
                {
                    //Console.WriteLine("Module Id is not present on the screen");
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Module Id is not present on the screen", true);
                }
                Thread.Sleep(15000);
                locator.CurrentQuote.Click();
                Thread.Sleep(20000);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
    

                                         
    
  
        
            

      
