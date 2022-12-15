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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07
{
   public  class ScenarioMain:D06.ScenarioMain
    {
       // public string TestURL;
       //// public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
       // public ScenarioMain(string URL ):base(URL)
       // {
       //     TestURL = URL;

       // }

       // Constant cs = new Constant();

        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        { }



        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
            ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
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

                    }

            if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
            {
                CopyAndCreateNewQuote();
            }
                        RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
            string runId = RunId;
            if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                    {
                        if (test.ProductIdentificationData != null)
                        {
                    if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                    {
                            
                        UpGradeServices(cs, test, ds);
                        string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                        RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);
                        DownGradeServices(cs, test, ds);
                        RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "2", null, cs, test);
                    }

                }
                        else
                        {
                            Console.WriteLine("Product is not available , Accessories  can not be added.");

                        }
                    }
                
                    if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                    {
                        if (test.ProductIdentificationData != null)
                        {
                            DownGradeServices(cs, test.ModuleId,test.OfferingId, ds);
                            RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", null, cs, test);
                }
                        else
                        {

                            Console.WriteLine("Product is not available , Accessories  can not be added.");


                        }
                    }


                
                    resultLog.ExportToSqlServerDB(ds,test,scenarioId, retriveSmartPriceData);
                    resultLog.ExportToSummaryTable(test, scenarioId);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null,retriveSmartPriceData);
            }
        }
        /// <summary>
        /// Upgrade Service with Module ID 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void DownGradeServices(Constant locator, TestCase test, DSAPageObject ds)
        {
            ConfigurationPage configPage = new ConfigurationPage(WebDriver);
            string ServiceTitle = "";
            decimal ServiceListPrice = 0;
            string ServiceId = "";
            bool isdowngrade = false;
            configPage.ClickOnConfigItem(ds.OrderCode);
            
      //      locator.jse.ExecuteScript("window.scrollBy(0, 800)");
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
           locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", locator.ServiceMenu.Location.X, locator.ServiceMenu.Location.Y);
            locator.jse.ExecuteScript("window.scrollBy(0, -200)");
            locator.ServiceMenu.Click();
           

           configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId);
            try
            {
                if (locator.IsElementPresent(locator.ByClickOnServiceModule(test.ModuleId)))
                {
                    locator.ClickOnServicesWithModuleId(test.ModuleId).Click();//click on Module ID associated with the service
                    ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;

                    if (!ServiceTitle.Contains("None") || !ServiceTitle.Contains("None Selected") || !ServiceTitle.Contains("No"))
                    {
                        string[] str = ServiceTitle.Split('[');
                        for (int i = 1; i < str.Length; i++)
                        {
                            ServiceId += "[" + str[i];
                        }
                        ds.ServiceHeaderText = configPage.Moduletext(test.ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        string moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                        locator.jse.ExecuteScript("window.scrollBy(0,800)");
                        //To find the ServiceList Price based on the service type

                        if (locator.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()=' " + ServiceId + " '])[2]")))
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(" " + ServiceId + " ").Text));
                        }
                        else
                        {

                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPriceCheckBox(" " + ServiceId + " ").Text));
                        }

                        foreach (IWebElement listPrice in configPage.AllServiceListPrice(moduleTitle))
                        {

                            if (listPrice.Enabled && listPrice.Displayed && locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(listPrice.Text)))
                            {
                                if (Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", "")) < ServiceListPrice)
                                {
                                    locator.jse.ExecuteScript("arguments[0].scrollIntoView(true);", listPrice);
                                    locator.jse.ExecuteScript("window.scrollBy(0, -200)");
                                    Thread.Sleep(3000);
                                    ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text));
                                    ServiceOptionClickBasedOnListPrice(configPage, listPrice, 1);

                                    Thread.Sleep(2000);
                                    //      Actions actions = new Actions(WebDriver);
                                    //     actions.MoveToElement(locator.ClickOnServicesWithModuleId(test.ModuleId));

                                    locator.jse.ExecuteScript("window.scrollBy(0, -200)");
                                    // ServiceTitle = ServiceTitleBasedOnListPrice(serviceOption.Text);
                                    //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;
                                    ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;

                                    //locator.jse.ExecuteScript("arguments[0].scrollIntoView(true);", locator.MinimizeServiceMenu(ServiceTitle));
                                    //locator.jse.ExecuteScript("window.scrollBy(0, -200)");
                                    ds.DowngradeService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                                    Console.WriteLine("Downgrade the service..");
                                    //      locator.jse.ExecuteScript("arguments[0].click();", locator.MinimizeServiceMenu(ServiceTitle));
                                    isdowngrade = true;
                                    break;
                                }
                            }


                        }
                        if (ServiceListPrice > 0 || ServiceListPrice == 0 && !isdowngrade)
                        {
                            Console.WriteLine("There is no service to to downgrade...");
                        }
                    }
                    else
                    {
                        // needs to add logic for if nothing is selected.
                    }
                }





                
                
                //}
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw new NoSuchElementException(@"[ModuleId is not in present on the Sevice Configuration Screen]", locator.ClickOnServicesWithModuleId(test.ModuleId));
            }
            

            locator.CurrentQuote.Click();
            Thread.Sleep(20000);
















        }

        private void ServiceOptionClickBasedOnListPrice(ConfigurationPage configPage,IWebElement listPrice, int tries)
        {
            try
            {
                Thread.Sleep(3000);
                configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//clik on radio button
                
            }
            catch(ElementClickInterceptedException ex)
            {
                Console.WriteLine(ex.Message);
                if (tries < 4)
                {
                    Console.WriteLine("Scroll Attempted..");
                    Constant locator = new Constant(WebDriver);
                    locator.jse.ExecuteScript("window.scrollBy(0,-100)");
                    ServiceOptionClickBasedOnListPrice(configPage,listPrice, tries + 1);
                }
                else
                    throw ex;
            }
            catch(Exception ex)
            {
                throw;
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
        public void DownGradeServices(Constant locator, string ModuleId,string OfferingId, DSAPageObject ds)
        {
            ConfigurationPage configPage = new ConfigurationPage(WebDriver);
            try
            {
                string ServiceTitle = "";
                decimal ServiceListPrice = 0;
                string moduleTitle = "";

                configPage.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                locator.jse.ExecuteScript("window.scrollBy(0, 800)");

                locator.ServiceMenu.Click();
                locator.jse.ExecuteScript("window.scrollBy(0, 200)");

                configPage. RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, ModuleId, OfferingId);
                
                if (locator.IsElementPresent(locator.ByClickOnServiceModule(ModuleId)))
                {
                    locator.ClickOnServicesWithModuleId(ModuleId).Click();//click on Module ID associated with the service
                    Thread.Sleep(2000);
                    locator.jse.ExecuteScript("window.scrollBy(0,600)");
                    if (locator.IsElementPresent(locator.BySelectService(OfferingId)))
                    {
                        locator.SelectServiceId(OfferingId).Click();
                        Thread.Sleep(10000);
                        ds.ServiceHeaderText = configPage.Moduletext(ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                        ServiceListPrice = Convert.ToDecimal(locator.ConfigServiceListPrice(OfferingId).Text.Replace("$", "").Replace(",", ""));
                        ServiceTitle = locator.ServiceTitle(OfferingId).Text.Replace("Dell Recommended"," ").Trim();
                        locator.jse.ExecuteScript("arguments[0].scrollIntoView(true);", locator.ServiceTitle(OfferingId));
                        locator.jse.ExecuteScript("window.scrollBy(0,-200)");
                        //    locator.MinimizeText(ServiceTitle).Click();
                        ds.DowngradeService(ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                        Console.WriteLine("Downgrade the service..");
                    }
                    else
                    {
                        Console.WriteLine("Service ID is not present");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Module ID is not present");
                }
                
                Thread.Sleep(5000);
                locator.CurrentQuote.Click();
                Thread.Sleep(20000);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }









        }
    }
  }
        
            

      
