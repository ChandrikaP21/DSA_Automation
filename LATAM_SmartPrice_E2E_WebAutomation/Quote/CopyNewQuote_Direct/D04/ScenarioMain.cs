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
using SmartPrice_E2E_WebAutomation.Utilities;
using SmartPrice_E2E_WebAutomation.Helper;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D04
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
        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
              WebDriver = new ChromeDriver();
                OracleDBConnection odb = new OracleDBConnection();
                DSAPageObject ds = new DSAPageObject(WebDriver);
                Constant cs = new Constant(WebDriver);
                ConfigurationPage accessoryLocator = new ConfigurationPage(WebDriver);
                TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
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
                    CopyAndCreateNewQuote();
                }
                //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);

                switch (test.UpdateAccessories)
                {
                    case "ADD":
                        if (string.IsNullOrEmpty(test.ModuleId))
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
                        // SmartPriceCalculation();
                        CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules, scenarioId);
                        resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId);
                        resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
                        resultLog.ExportToSummaryTable(test, scenarioId);
                        Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                        //WebDriver.Close();

                        break;

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            //string ServiceId = "";
            decimal ServiceListPrice = 0;
            try
            {
                accessorylocator.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                element.jse.ExecuteScript("window.scrollBy(0, 800)");
                //      Thread.Sleep(3000);

                element.ServiceMenu.Click();
                //      Constant.jse.ExecuteScript("window.scrollBy(0, 400)");
                accessorylocator.RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, ds.PnrLog.ServiceType, test.ModuleId);
               
                    try
                    {
                        element.AccessoriesMenu.Click();
                    }
                    catch(Exception ex)
                    {
                        WebDriver.FindElement(By.XPath("//*[@id='body - content']/div[2]/app-root/app-product-configurepoc/div[2]/div/div/div[1]/div/product-configure-poc/div/div/div/div[4]/div[3]/div/div[2]/div[4]/tabset/ul/li[4]/a/span")).Click();
                    }

                    
                    

                    try
                    {
                        if (accessorylocator.SearchByModuleId(test.ModuleId).Displayed)
                        {
                            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                            element.jse.ExecuteScript("window.scrollBy(0,300)");

                            //       constant.CheckAccessories(test).Click();
                            if (element.SelectServiceId(test.AccessoriesId).Displayed)
                            {
                                element.SelectServiceId(test.AccessoriesId).Click();

                                Thread.Sleep(5000);
                                ds.ServiceHeaderText = accessorylocator.Moduletext(test.ModuleId).Text;
                                var service = ds.ServiceHeaderText.Replace(" ", "");
                                decimal accessoryListPrice = 0;
                                try
                                {
                                    accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPrice(test.AccessoriesId).Text));
                                }
                                catch (Exception ex)
                                {
                                    string listPrice = GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPriceAfterDiscount(test.AccessoriesId).Text);
                                    string[] price = listPrice.Split('(');
                                    accessoryListPrice = Convert.ToDecimal(price[0]);
                                }

                                ServiceTitle = element.ServiceTitle(test.AccessoriesId).Text.Replace("\r\nView More on Dell.com", "");
                                ds.AddAccessories(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, accessoryListPrice, 0, true, ds.PnrLog.ServiceType);
                                Console.WriteLine("Accessories is added...........");
                                element.jse.ExecuteScript("window.scrollBy(0,-200)");
                                element.MinimizeText(ServiceTitle).Click();
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Moduleid is not present");
                            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("ModuleId is not present. Accessories can not be added", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                    }
                //}
                //else
                //{
                //    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Accessorry tab is not Present", true);
                //}

                //      new WebDriverWait(WebDriver, TimeSpan.FromSeconds(7)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCurrentQuote));
                element.CurrentQuote.Click();
                Thread.Sleep(20000);
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
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                element.jse.ExecuteScript("window.scrollBy(0, 800)");


                element.ServiceMenu.Click();
                element.jse.ExecuteScript("window.scrollBy(0, 200)");

                accessorylocator.RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId);
                //     Constant.jse.ExecuteScript("window.scrollBy(0, -200)");
                if (element.IsElementPresent(element.ByAccessoriesMenu))
                {
                    element.AccessoriesMenu.Click();
                    try
                    {
                        if (accessorylocator.SearchByModuleId(test.ModuleId).Displayed)
                        {
                            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                            ds.ServiceHeaderText = accessorylocator.Moduletext(test.ModuleId).Text;
                            string accessoryHeaderText = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                            List<IWebElement> inputSelection = WebDriver.FindElements(By.XPath("//table//input[@type='radio'or @type='checkbox']")).ToList();


                            for (int i = 0; i < inputSelection.Count; i++)
                            {
                                if (inputSelection[i].Displayed && element.IsElementPresent(By.XPath("(//table//input[@type='radio'or @type='checkbox'])[" + i + "]")))
                                {
                                    inputSelection[i].Click();
                                    Thread.Sleep(5000);

                                    int uiControlId = i + 1;
                                    string input = "";
                                    if (!element.IsElementPresent(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[2]//span[@class='greenText']")))
                                    {
                                        IWebElement locator = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[" + uiControlId + "]"));
                                        accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.Text));
                                    }

                                    else
                                    {
                                        string list = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryHeaderText + "_listPrice_')])[" + uiControlId + "]//span[@class='greenText']")).Text.Replace("$", "").Replace(",", "");
                                        string[] price = GenericHelper.ConvertCurrencyIntoString(list).Trim().Split('(');
                                        accessoryListPrice = Convert.ToDecimal(price[0]);
                                    }
                                    //string input = WebDriver.FindElement(By.XPath("(//input[@type='radio'or @type='checkbox'])[" + i + "]//../following::td[4]/div/div/span[1]")).GetAttribute("innerText");
                                    //try
                                    //{
                                    //    input = WebDriver.FindElement(By.XPath("(//input[@type='radio'or @type='checkbox'])[" + i + "]//../following::td[4]/div/div/span[2]")).GetAttribute("innerText");
                                    //    accessoryListPrice = Convert.ToDecimal(input.Replace("$", ""));
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    input = WebDriver.FindElement(By.XPath("(//input[@type='radio'or @type='checkbox'])[" + i + "]//../following::td[4]/div/div/span[2]")).GetAttribute("innerText");
                                    //    string listPrice = input.Replace("$", "").Replace(",", "");
                                    //    string[] price = listPrice.Split('(');
                                    //    accessoryListPrice = Convert.ToDecimal(price[0]);
                                    //}

                                    break;
                                    //}
                                }
                            }


                            IWebElement elemen = WebDriver.FindElement(By.XPath("(//label[@id='productConfig_option_" + test.ModuleId + "']/../../../div[3]//div[@class='module-desc'])[2]"));
                            accessoryTtitle = elemen.Text;
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

                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                        // Console.WriteLine(ex.Message);

                    }

                    //      Constant.jse.ExecuteScript("window.scrollBy(0,600)");
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Accessorry tab is not Present", true);
                }


                Thread.Sleep(15000);

                element.CurrentQuote.Click();
                Thread.Sleep(20000);


                
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

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(constant.ByServicesMenu));
                WebDriverUtils.ScrollIntoView(WebDriver, constant.ServiceMenu);
          //      constant.jse.ExecuteScript("window.scrollBy(0, 900)");
                constant.ServiceMenu.Click();
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
                        WebDriverUtils.ScrollIntoView(WebDriver, constant.AccessoriesMenu);
                        constant.AccessoriesMenu.Click();
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




                            //if (constant.IsElementPresent(By.XPath("//input[starts-with(@id,'productConfig_" + ds.ServiceHeaderText + "_selection_0')]")))//to check none
                            //{
                            //  Find List Price which is not null.
                            int listPrice = 0;
                            foreach (IWebElement ele in constant.ServiceListPrice(accessoryModuleText))
                            {


                                if (!ele.Size.IsEmpty && GenericHelper.ConvertCurrencyIntoString(ele.Text) != "0.00" && ele.Enabled && ele.Displayed && constant.IsElementPresent(configurationPage.ByServiceOptionClickBasedOnListPrice(ele.Text)))
                                {
                                    //ele.Click();
                                    try
                                    {

                                        // change to accessory list price
                                        string a = ele.Text;
                                        IWebElement accessoryOption = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')]/div/div/span//following::td/input)[" + listPrice + "]"));

                                        accessoryOption.Click();
                                        Thread.Sleep(4000);
                                        int index = listPrice + 1;

                                        if (!constant.IsElementPresent(By.XPath("(//*[contains(@id,'productConfig_0_WorkFromAnywhereSolutions_listPrice_')])[2]//span[@class='greenText']")))
                                        {
                                            IWebElement element = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')])[" + index + "]"));
                                            accessoryListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.Text));
                                        }

                                        else
                                        {
                                            string list = WebDriver.FindElement(By.XPath("(//*[contains(@id,'productConfig_0_" + accessoryModuleText + "_listPrice_')])[" + index + "]//span[@class='greenText']")).Text.Replace("$", "").Replace(",", "");
                                            string[] price = GenericHelper.ConvertCurrencyIntoString(list).Trim().Split('(');
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

                constant.CurrentQuote.Click();
                Thread.Sleep(20000);

            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }






        }
    }
}
            
        
        
            

      
