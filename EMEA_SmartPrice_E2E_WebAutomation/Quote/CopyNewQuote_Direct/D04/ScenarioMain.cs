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
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04
{
   public class ScenarioMain : D01.ScenarioMain
    {
        public string TestURL;
       
     
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;


        }
        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        {
            

        }
        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {
                WebDriver = new ChromeDriver();
                OracleDBConnection odb = new OracleDBConnection();
                DSAPageObject ds = new DSAPageObject(WebDriver);
                Constant cs = new Constant(WebDriver);
                ConfigurationPage accessoryLocator = new ConfigurationPage(WebDriver);
                TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                ResultLogging resultLog = new ResultLogging();
                DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
                DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
                List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                string draftQuote = "";
            ds.country = country;
            try
            {

                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    SearchQuote(ds, cs);
                    ds.OrderCode = test.ProductIdentificationData;
                    ds.PnrLog.ServiceType = "1";
                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(10), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }
                //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);

                switch (test.UpdateAccessories)
                {
                    case "ADD":
                        if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.AccessoriesId))
                        {
                            AddAccessories(cs, test, ds,WebDriver);
                            //AddAccessories(cs, test,ds, retriveSmartPriceData);
                            
                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.AccessoriesId))
                        {
                            bool IsModuleId = true;
                            AddAccessories(cs, test, ds, IsModuleId,WebDriver);
                        }
                        if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.AccessoriesId))
                        {
                            bool IsAccessorId = true;
                            bool IsModuleId = true;
                            AddAccessories(cs, test, ds, IsModuleId, IsAccessorId,WebDriver);

                        }
                        RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, scenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                        SmartPriceCalculation(retriveSmartPriceData,totalFinancialCalculation);
                        CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules, scenarioId);
                        if (application.Contains("GoalLite"))
                        {
                            DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                            SubmitGoalLite(test, ds, country, dsaQuoteSummaryPage);

                        }
                        if (application.Contains("Goal"))
                        {
                            
                            SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType,quoteSummaryObject,test);
                        }
                        resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber,quoteSummaryObject,goalDetails);
                        resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
                        Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                        //WebDriver.Close();

                        break;

                }
            }
            catch (ShowStopperException e)
            {
                throw e;


            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D04", e);
                exec.LogException();

            }
            finally
                {
                    CustomGarbageCollector customeGc = new CustomGarbageCollector();
                    customeGc.NullifyUnusedObject(WebDriver,odb, ds, totalFinancialCalculation, cs, resultLog, test,null,retriveSmartPriceData);
                }
            




        }
        /// <summary>
        /// Module id with accessoryid
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="accessorylocator"></param>
        public void AddAccessories(Constant element, TestCase test, DSAPageObject ds,bool IsModuleIdModuleId,bool IsAccessorId,IWebDriver driver)
        {
            WebDriver = driver;
            ConfigurationPage accessorylocator = new ConfigurationPage(WebDriver);
            string ServiceTitle = "";
            string accessoryTtitle = "";
            //string ServiceId = "";
            decimal ServiceListPrice = 0;
            try
            {
                accessorylocator.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                
                //WebDriver.ScrollIntoView(element.ByServicesMenu);
                //element.ServiceMenu.Click();
                
                accessorylocator.RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, ds.PnrLog.ServiceType, test.ModuleId);
               
                   

                    try
                    {
                          accessorylocator.ClickOnAccesooryTab();
                      
                            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                            element.jse.ExecuteScript("window.scrollBy(0,300)");

                          
                            if (element.SelectServiceId(test.AccessoriesId).Displayed)
                            {
                                element.SelectServiceId(test.AccessoriesId).Click();

                                Thread.Sleep(5000);
                                ds.ServiceHeaderText = accessorylocator.Moduletext(test.ModuleId).Text;
                                var accessorytitle = ds.ServiceHeaderText.Replace(" ", "");
                                decimal accessoryListPrice = 0;
                                try
                                {
                                    accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPrice(test.AccessoriesId).Text,ds.country));
                                }
                                catch (Exception ex)
                                {
                                    string listPrice = GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPriceAfterDiscount(test.AccessoriesId).Text,ds.country);
                                    string[] price = listPrice.Split('(');
                                    accessoryListPrice = Convert.ToDecimal(price[0]);
                                }

                            accessoryTtitle = accessorylocator.FindComponentTitle(accessorytitle).Text;

                            ds.AddAccessories(test.ModuleId, ds.ServiceHeaderText, accessoryTtitle, accessoryListPrice, 0, true, ds.PnrLog.ServiceType);
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Accessory is added",true);
                               
                                element.jse.ExecuteScript("window.scrollBy(0,-200)");
                                element.MinimizeText(accessoryTtitle).Click();
                            }
                    element.ClickOnConfigurationViewQuote();
                }
                catch (ShowStopperException ex)
                {   
                    throw ex;
                }
                catch (Exception ex)
                    {
                    WebDriverUtils.TakeSnapShot(WebDriver);
                    throw new ShowStopperException("Unable to add accessory, Module id with accessoryid", ex);
                    }

                
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
        }
        /// <summary>
        /// With Module Id
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="accessorylocator"></param>
        public void AddAccessories(Constant element, TestCase test, DSAPageObject ds, bool IsModuleIdModuleId,IWebDriver driver)
        {
            WebDriver = driver;
            ConfigurationPage accessorylocator = new ConfigurationPage(WebDriver);
            try
            {
                string ServiceTitle = "";
                string ServiceId = "";
                decimal ServiceListPrice = 0;
                decimal accessoryListPrice = 0;
                string accessoryTtitle = "";
                accessorylocator.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                

                accessorylocator.RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId);
               
                    try
                    {
                       accessorylocator.ClickOnAccesooryTab();
                         if (accessorylocator.SearchByModuleId(test.ModuleId).Displayed)
                        {
                            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                            Thread.Sleep(1000);
                            ds.ServiceHeaderText = accessorylocator.Moduletext(test.ModuleId).Text;
                            string accessoryHeaderText = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                           
                            List<IWebElement> inputSelection = WebDriver.FindElements(By.XPath("//*[starts-with(@id,'productConfig_"+ ds.ServiceHeaderText + "') and contains(@id,'options')]/td[1]/div[2]/label/span")).ToList();
                            

                            for (int i = 0; i < inputSelection.Count; i++)
                            {
                                if (inputSelection[i].Displayed/* && element.IsElementPresent(By.XPath("(//table//input[@type='radio'or @type='checkbox']//span)[" + i + "]"))*/)
                                {
                                    inputSelection[i].Click();
                                    Thread.Sleep(5000);

                                    int uiControlId = i + 1;
                                    string input = "";
                                    if (!element.IsElementPresent(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[2]//span[@class='greenText']")))
                                    {
                                        IWebElement locator = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[" + uiControlId + "]"));
                                        accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.Text,ds.country));
                                    }

                                    else
                                    {
                                        string list = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[" + uiControlId + "]//span[@class='greenText']")).Text.Replace("$", "").Replace(",", "");
                                        string[] price = GenericHelper.ConvertCurrencyIntoString(list,ds.country).Trim().Split('(');
                                        accessoryListPrice = Convert.ToDecimal(price[0]);
                                    }
                                    

                                    break;
                                    //}
                                }
                            }
                            WebDriverUtils.ScrollIntoView(WebDriver, element.ByClickOnHeaderModule(test.ModuleId));
                            element.ClickOnTitleWithModuleId(test.ModuleId).Click();//to monimize the component

                             accessoryTtitle = accessorylocator.FindComponentTitle(accessoryHeaderText).Text;
                            
                            

                            
                            ds.AddAccessories(test.ModuleId, accessoryHeaderText, accessoryTtitle, accessoryListPrice, 0, true, null);
                            // Console.WriteLine("Accessory is added..");
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Accessory is added", true);
                            //      locator.MinimizeServiceMenu(ComponentTitle).Click();
                            element.jse.ExecuteScript("arguments[0].click();", element.MinimizeServiceMenu(accessoryTtitle));
                            element.jse.ExecuteScript("window.scrollBy(0,200)");






                       }
                        else
                        {
                            //Console.WriteLine("ModuleId is not present. Accessories can not be added");
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("ModuleId is not present. Accessories can not be added", true);
                        }
                    }
                    catch (Exception ex)
                    {
                    WebDriverUtils.TakeSnapShot(WebDriver);
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                    throw new ShowStopperException("Unable to add Accessory with module Id, refer AddAccessories() in ScenarioMain Class ", ex);
                        // Console.WriteLine(ex.Message);

                    }

             
                Thread.Sleep(15000);

                element.ClickOnConfigurationViewQuote();

            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }


        }
        /// <summary>
        /// Without module id and accessory Id
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="accessorylocator"></param>
        public void AddAccessories(Constant constant, TestCase test, DSAPageObject ds,IWebDriver driver)
        {
            WebDriver = driver;
            ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
            try
            {

                configurationPage.ClickOnConfigItem(ds.OrderCode);

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(constant.ByServicesMenu));
          //      WebDriverUtils.ScrollIntoView(WebDriver, constant.ServiceMenu);
          ////      constant.jse.ExecuteScript("window.scrollBy(0, 900)");
                //constant.ServiceMenu.Click();
                string serviceTitle = "";
                decimal ServiceListPrice = 0;
                decimal accessoryListPrice = 0;
                string ModuleId = "";
                string ServiceId = "";
                string servicedesc = "";
                string accessoryModuleText = "";
                constant.jse.ExecuteScript("window.scrollBy(0, 200)");
                // Find all base service listprice,title,module id..
                configurationPage.RetriveBaseServiceDetails(constant, ds, serviceTitle, ServiceListPrice, null, null, ds.PnrLog.ServiceType);
                //if (constant.IsElementPresent(constant.ByAccessoriesMenu))
                //{
                //    constant.jse.ExecuteScript("window.scrollBy(0, -200)");//........ Channel id 20 and 77 modification . for other Channel this comment line should  be  removed.
                    if(constant.IsElementPresent(constant.ByAccessoriesMenu))
                    {
                    configurationPage.ClickOnAccesooryTab();
                    }
                    else

                    {
                        IWebElement text = WebDriver.FindElement(By.XPath("//span[text()='Unknown']"));
                        WebDriverUtils.ScrollIntoView(WebDriver, By.XPath("//span[text()='Unknown']"));
                        text.Click();
                        
                    }
                    
                    //constant.AccessoriesMenu.Click();
                    int k = 0;
                    //find all non base services listprice, title, module id
                    foreach (var accessory in constant.NoneElement)
                    {

                        bool isclicked = false;
                        bool isFound = false;
                        if (accessory.Displayed == true && accessory.Text == "None" || accessory.Text == "None Selected")
                        {
                            int l = k + 1;

                            string text = WebDriver.FindElement(By.XPath("(//*[text()='None' or text()='None Selected'])[" + l + "]//preceding::label[1]/span[1]")).Text;
                            //    string[] str = text.Split(']');
                            ModuleId = /*str[0].Replace("[","")*/text;
                            string accessoryHeaderText = WebDriver.FindElement(By.XPath("(//*[text()='None' or text()='None Selected'])[" + l + "]//preceding::label[1]/span[2]")).Text;
                            //     accessoryModuleText = str[1].Replace("(Non-Tied)", "").Trim();       
                            //Constant.jse.ExecuteScript("window.scrollBy(0, 200)");
                            accessoryModuleText = accessoryHeaderText.Replace(" ", "").Trim();
                            accessory.Click();

                         accessoryModuleText = RemoveDietaries(accessoryModuleText);


                        //if (constant.IsElementPresent(By.XPath("//input[starts-with(@id,'productConfig_" + ds.ServiceHeaderText + "_selection_0')]")))//to check none
                        //{
                        //  Find List Price which is not null.
                        int listPrice = 0;
                            foreach (IWebElement ele in constant.ServiceListPrice(accessoryModuleText))
                            {


                                if (!ele.Size.IsEmpty && GenericHelper.ConvertCurrencyIntoString(ele.Text,ds.country) != "0.00" && ele.Enabled && ele.Displayed && constant.IsElementPresent(configurationPage.ByServiceOptionClickBasedOnListPrice(ele.Text)))
                                {
                                    //ele.Click();
                                    try
                                    {

                                        // change to accessory list price
                                        string a = ele.Text;
                                        IWebElement accessoryOption = ele.FindElement(By.XPath("./ancestor::td/preceding::td[5]"));

                                        accessoryOption.Click();
                                        Thread.Sleep(4000);
                                        int index = listPrice + 1;

                                        if (!constant.IsElementPresent(By.XPath("(//*[contains(@id,'productConfig_0_"+ accessoryModuleText + "_listPrice_')])[2]//span[@class='greenText']")))
                                        {
                                            IWebElement element = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')])[" + index + "]"));
                                            accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.Text,ds.country));
                                        }

                                        else
                                        {
                                            string list = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')])[" + index + "]//span[@class='greenText']")).Text.Replace("$", "").Replace(",", "");
                                            string[] price = GenericHelper.ConvertCurrencyIntoString(list,ds.country).Trim().Split('(');
                                            accessoryListPrice = Convert.ToDecimal(price[0]);
                                        }
                                        //  IWebElement element = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')])[" + index+ "]"));
                                        //try
                                        //{

                                        //    accessoryListPrice = Convert.ToDecimal(element.Text.Replace("$", "").Replace(",", ""));
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    string list = element.Text.Replace("$", "").Replace(",", "");
                                        //    string[] price = list.Split('n');
                                        //    accessoryListPrice = Convert.ToDecimal(price[0]);
                                        //}
                                        constant.jse.ExecuteScript("window.scrollBy(0,200)");
                                        string accessoryTitle = configurationPage.FindComponentTitle(accessoryModuleText).Text;

                                        ds.AddAccessories(ModuleId, accessoryHeaderText, accessoryTitle, accessoryListPrice, 0, true, ds.PnrLog.ServiceType);
                                        //   dsaObjectsList.Add(ds);
                                        //      new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementIsVisible(configurationPage.ByComponentTitle(accessoryModuleText)));
                                        //Actions action = new Actions(WebDriver);
                                        //action.MoveToElement(configurationPage.FindComponentTitle(accessoryModuleText));
                                        //new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementIsVisible(configurationPage.ByComponentTitle(accessoryModuleText)));
                                        //action.Click().Build().Perform();//NEEDS TO CHECK IT AGAIN
                                        //     configurationPage.FindComponentTitle(accessoryModuleText).Click();//to monimize the component
                                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Accessory is added", true);
                                        //Console.WriteLine("Accessory is added");
                                        isclicked = true;
                                        isFound = true;

                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                                    }

                                }

                                listPrice++;

                            }




                        }
                        else
                        {
                            isclicked = true;
                        }
                        if (!isclicked && accessory.Displayed)
                        {
                            constant.jse.ExecuteScript("arguments[0].click();", accessory);

                        }
                        if (isFound)
                        {
                            break;
                        }
                        k++;
                    }
                //}
                //else
                //{
                //    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Accessorry tab is not Present",true);
                //}

                Thread.Sleep(5000);

                constant.ClickOnConfigurationViewQuote();

            }
            catch (ShowStopperException ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                throw ex;
            }
            catch (Exception ex)
            {
                 WebDriverUtils.TakeSnapShot(WebDriver);

                throw new ShowStopperException("Unable to auto add accesory in ScenarioMain class", ex);
                 

            }






        }
    }
}
            
        
        
            

      
