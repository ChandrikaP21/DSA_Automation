using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.IO;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using System.Globalization;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D03.ScenarioMain
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
       



        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber,string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ds.country = country;
            try
            {


                string draftQuote = "";
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
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }
               
                if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//without module id and with out component ID
                {
                    UpGradeComponent(cs,ds,WebDriver);

                }
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//With Module ID
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

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.ComponentId))// With Module ID and Service ID
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
                if (application.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, country, dsaQuoteSummaryPage);

                }
                if (application.Contains("Goal"))
                {
                   
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId,test.ApprovalType,quoteSummaryObject,test);
                }

                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test,ScenarioId, retriveSmartPriceData);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();
            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException ex)
            {

                throw ex;
            }
            
            catch (Exception e)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(e.Message, true);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }

        }
        public void ClickOnComponentTab()
        {
            ConfigurationPage config = new ConfigurationPage(WebDriver);
            try
            {
                
                for (int i= 0;i < 4;i++)
                {
                    try
                    {
                        WebDriver.ScrollIntoView(config.BySystemMenu);
                        config.SystemMenu.Click();
                        break;
                    }
                    catch(Exception)
                    {
            
                    }
                }
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to click on Component Tab, Please Check ClickOnComponentTab() in ScenarioMain Class",ex);
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
           List<Translate>translateLanguage= LoadDeserializeObject();
            
            try
            {
              
                configurationPage.ClickOnConfigItem(ds.OrderCode);

                new WebDriverWait(WebDriver,TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(constant.BySummary));
       //         WebDriver.ScrollIntoView(constant.SummaryMenu);
               
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
               
               
                ClickOnComponentTab();

                foreach (IWebElement moduleTitle in configurationPage.ComponentModuleTitle)
                {
                    bool isclicked = false;
                    bool isFound = false;
                    string text = "";
                    if (moduleTitle.Displayed)
                    {
                        moduleHeaderText = moduleTitle.Text;//<label>[11]processor<span>11</span>
                        ModuleId = moduleTitle.FindElement(By.XPath("./span[1]")).Text;//Find Module ID

                        string[] moduleText = moduleTitle.Text.Split(']');
                        for (int i = 0; i < moduleText.Length; i++)
                        {
                            ModuleId = moduleText[i].Replace("[", "");
                            text = moduleText[i + 1].Trim();
                            moduleHeaderText = moduleText[i + 1].Replace(" ", "");
                            if (moduleHeaderText.Contains("("))
                                {
                                string[] Text = moduleHeaderText.Split('(');
                                moduleHeaderText = Text[0];
                            }
                            
                           // moduleHeaderText =  ds.TranslateToExpectedLanguage(translateLanguage,moduleHeaderText,ds.country);
                           moduleHeaderText= RemoveDietaries(moduleHeaderText);
                            

                            break;
                        }
                        try
                        {
                            configurationPage.FindComponentTitle(moduleHeaderText.Replace("\r\n", "")).Click();//click on the component title
                        }
                        catch(Exception ex)
                        {
                            moduleHeaderText = moduleTitle.FindElement(configurationPage.ByNonUsComponentTitle).GetAttribute("innerText");
                           moduleTitle.FindElement(configurationPage.ByNonUsComponentTitle).Click();
                        }

                        // SelectListPrice(constant,configurationPage, ref componentListPrice);
                        configurationPage.SelectListPrice(ref componentListPrice,ds.country);

                        Actions action = new Actions(WebDriver);

                        //     Constant.jse.ExecuteScript("window.scrollBy(0,-300)");
                        List<IWebElement> NoOfRows = WebDriver.FindElements(By.XPath("//table//tr[starts-with(@id,'productConfig_') and contains(@id,'" + text + "_options_') and @class='dds__w-100 dds__mb-0 " + text + "']")).ToList();
                       
                        foreach (IWebElement td in NoOfRows)
                        {
                            if (td.Enabled && td.Displayed && td.Text != "")
                            {
                                string currentListPrice = td.FindElement(By.XPath("./td/div/div/span")).Text;
                                if (constant.IsElementPresent(configurationPage.ByServiceOptionClickBasedOnListPrice(currentListPrice)))
                                {
                                         Thread.Sleep(3000);
                                        if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(currentListPrice,ds.country)) > componentListPrice)
                                        {
                                            componentListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(currentListPrice, ds.country));
                                            
                                            configurationPage.ServiceOptionClickBasedOnListPrice(currentListPrice).FirstOrDefault(x=>x.Displayed).Click();//clik on radio button
                                            Thread.Sleep(8000);                                                              // ComponentTitle = ComponentTitleBasedOnListPrice(serviceOption.Text);
                                                                                                                             //GreenTextElement    //need to be implemented later                                                                                                                                                                                                         //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;

                                            action.MoveToElement(constant.ClickOnTitleWithModuleId(ModuleId,moduleHeaderText));

                                            
                                            constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);",constant.GetTitleWithModuleId(ModuleId,moduleHeaderText));
                                            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(configurationPage.ByComponentTitle(moduleHeaderText)));
                                            //to minimize the component
                                            string ComponentTitle = configurationPage.FindComponentTitle(moduleHeaderText).Text;
                                            ds.AddComponent(ModuleId, moduleHeaderText, ComponentTitle, componentListPrice, 0, true, "0");
                                        
                                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Component is upgraded", true);
                                        //WebDriverUtils.ScrollIntoView(WebDriver, configurationPage.ByComponentTitle(moduleHeaderText));
                                        
                                        //Console.WriteLine("Component is upgraded");
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
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to upgrade component,Please check  UpGradeComponent(), D03, ScenarioMain Class", ex);
            }
         constant.ClickOnConfigurationViewQuote();
        }

        private string ChangeModuleHeaderText(string country, string moduleHeaderText,string TranslatedText)
        {
            if(country!="UNITED States")
            {
                moduleHeaderText = TranslatedText;
            }
            return moduleHeaderText;
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
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
         //       locator.jse.ExecuteScript("window.scrollBy(0, 800)");
              

                configPage.RetriveBaseServiceDetails(locator, ds, ComponentTitle, componentListPrice, test.ModuleId, test.OfferingId, ds.PnrLog.ServiceType);// add service ID 
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(configPage.BySystemMenu));
                ClickOnComponentTab();

                try
                {
                    if (!string.IsNullOrEmpty(test.ModuleId))
                    {
                     
                       new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByClickOnHeaderModule(test.ModuleId)));
                        ClickOnComponentModuleId(test.ModuleId);
                        
                        ComponentTitle = locator.ClickOnTitleWithModuleId(test.ModuleId).GetAttribute("innerText");

                        string headerText = configPage.Moduletext(test.ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        string moduleTitle = headerText.Replace(" ", "").Replace("/", "");
                        configPage.SelectListPrice(ref componentListPrice,ds.country);
                        
                        Actions action = new Actions(WebDriver);

                        locator.jse.ExecuteScript("window.scrollBy(0,-300)");
                        List<IWebElement> NoOfRows = WebDriver.FindElements(By.XPath("//table//tr[starts-with(@id,'productConfig_') and contains(@id,'" + headerText + "_options_') and @class='dds__w-100 dds__mb-0 " + headerText + "']")).ToList();
                        foreach (IWebElement td in NoOfRows)
                        {
                            if (td.Enabled && td.Displayed && td.Text != "")
                            {
                                string currentListPrice = td.FindElement(By.XPath("./td/div/div/span")).Text;
                                if (locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(currentListPrice)))
                                {
                                    Thread.Sleep(3000);
                                    if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(currentListPrice,ds.country)) > componentListPrice)
                                    {
                                        componentListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(currentListPrice,ds.country));
                                        configPage.ServiceOptionClickBasedOnListPrice(currentListPrice).FirstOrDefault(x => x.Displayed).Click();//clik on radio button
                                        Thread.Sleep(8000);                                                              // ComponentTitle = ComponentTitleBasedOnListPrice(serviceOption.Text);
                                                                                                                         //GreenTextElement    //need to be implemented later                                                                                                                                                                                                         //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;

                                        action.MoveToElement(locator.ClickOnTitleWithModuleId(test.ModuleId, moduleTitle));


                                        locator.jse.ExecuteScript("arguments[0].scrollIntoView(true);", locator.GetTitleWithModuleId(test.ModuleId, moduleTitle));
                                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(configPage.ByComponentTitle(moduleTitle)));
                                        WebDriverUtils.ScrollIntoView(WebDriver, locator.ByClickOnHeaderModule(test.ModuleId));
                                        locator.ClickOnTitleWithModuleId(test.ModuleId).Click();//to monimize the component

                                        string ComponentHeaderTitle = configPage.FindComponentTitle(moduleTitle).Text;
                                        ds.AddComponent(test.ModuleId, headerText, ComponentHeaderTitle, componentListPrice, 0, true, "0");
                                        //Console.WriteLine("Component is upgraded");
                                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Component is upgraded", true);
                                        
                                        break;
                                    }
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
                    WebDriverUtils.TakeSnapShot(WebDriver);
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                  throw  new ShowStopperException("Unable to Upgrade Component,Please Check UpGradeComponent()-With Module Id",ex);
                    //throw new NoSuchElementException(@"[ModuleId is not in present on the Sevice Configuration Screen]", locator.ClickOnServicesWithModuleId(test.ModuleId));
                }
                Thread.Sleep(15000);

              locator.ClickOnConfigurationViewQuote();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void ClickOnComponentModuleId(string moduleId)
        { Constant locator = new Constant(WebDriver);
            try
            {
                locator.ClickOnTitleWithModuleId(moduleId).Click();//click on Module ID associated with the service
            }
            catch(Exception  ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                new ShowStopperException("Unable to click on ModuleId, Exit D03 Component Upgrade", ex);
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
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                locator.jse.ExecuteScript("window.scrollBy(0, 1200)");
                //int x= locator.ServiceMenu.Location.X;
                // int y= locator.ServiceMenu.Location.Y;
                // locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])",x,y);
                WebDriver.ScrollIntoView(configPage.BySystemMenu);
                locator.ServiceMenu.Click();


                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, null, null);//base

                WebDriverUtils.ScrollIntoView(WebDriver, configPage.BySystemMenu);
                
                configPage.SystemMenu.Click();

                if (locator.IsElementPresent(locator.ByClickOnHeaderModule(ModuleId)))
                {
                    locator.ClickOnTitleWithModuleId(ModuleId).Click();//click on Module ID 
              //      locator.jse.ExecuteScript("window.scrollBy(0,600)");
                    if (locator.IsElementPresent(locator.BySelectService(OfferingId)))
                    {
                        locator.SelectServiceId(OfferingId).Click();

                        Thread.Sleep(10000);
                        ds.ServiceHeaderText = configPage.Moduletext(ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                                                                                    //moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                        ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(OfferingId).Text,ds.country));
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
                locator.ClickOnConfigurationViewQuote();
            }
            catch (ShowStopperException ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw ex;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to upgrade Component with  Module Id and Service Id. refer Upgrade component", ex);
            }
        }
    }
}
    

                                         
    
  
        
            

      
