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

using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D96
{
   public  class ScenarioMain: StockAndSell.D78.ScenarioMain
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

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                     Constant cs = new Constant(WebDriver);
                     ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                   
                    CustomerClass customerClass = new CustomerClass();
                    ResultLogging resultLog = new ResultLogging();
                    Product productobject = new Product(WebDriver);
            ds.country = country;
            try
            {

                
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject,true);
                //   SmartPrice_E2E_WebAutomation.Quote.D06.ScenarioMain d06Scenario = new D06.ScenarioMain();
                
                //RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeServices = new CopyNewQuote_Direct.D06.ScenarioMain();
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain d07UpgradeServices = new CopyNewQuote_Direct.D07.ScenarioMain();
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {

                    d06UpgradeServices.UpGradeServices(cs, test, ds);
                    string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", customerClass, cs, test,true);
                    d07UpgradeServices.DownGradeServices(cs, test, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "2", customerClass, cs, test,true);
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    DownGradeServices(cs, test.ModuleId, test.OfferingId, ds);
                    RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", customerClass, cs, test,true);

                }



                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
                //WebDriver.Close();
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D96", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
        /// <summary>
        /// Upgrade Service with Module ID 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void DownGradeServices(Constant locator, TestCase test, DSAPageObject ds,string upgradedListPrice)
        {
            ConfigurationPage configPage = new ConfigurationPage(WebDriver);
            string ServiceTitle = "";
            decimal ServiceListPrice = 0;
            string ServiceId = "";
            bool isdowngrade = false;
            configPage.ClickOnConfigItem(ds.OrderCode);
            
      //      Constant.jse.ExecuteScript("window.scrollBy(0, 800)");
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
           locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", locator.ServiceMenu.Location.X, locator.ServiceMenu.Location.Y);
            locator.jse.ExecuteScript("window.scrollBy(0, -200)");
            locator.ServiceMenu.Click();


            configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId);
            try
            {
                locator.ClickOnServicesWithModuleId(test.ModuleId).Click();//click on Module ID associated with the service
                ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;
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
                    ServiceListPrice = Convert.ToDecimal(locator.ConfigServiceListPrice(" " + ServiceId + " ").Text.Replace("$", "").Replace(",", ""));
                }
                else
                {

                    ServiceListPrice = Convert.ToDecimal(locator.ConfigServiceListPriceCheckBox(" " + ServiceId + " ").Text.Replace("$", "").Replace(",", ""));
                }

                foreach (IWebElement listPrice in configPage.AllServiceListPrice(moduleTitle))
                {
                    
                        if (listPrice.Enabled && listPrice.Displayed && locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(listPrice.Text)))
                        { 
                        if (Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", "")) < ServiceListPrice)
                        {
                            //Constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);", listPrice);
                            //Constant.jse.ExecuteScript("window.scrollBy(0, -200)");
                            Thread.Sleep(3000);
                            ServiceListPrice = Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", ""));
                            ServiceOptionClickBasedOnListPrice(configPage,listPrice, 1);
                              
                            Thread.Sleep(2000);
                      //      Actions actions = new Actions(WebDriver);
                       //     actions.MoveToElement(locator.ClickOnServicesWithModuleId(test.ModuleId));

                            locator.jse.ExecuteScript("window.scrollBy(0, -200)");                       
                            // ServiceTitle = ServiceTitleBasedOnListPrice(serviceOption.Text);
                            //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;
                            ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;

                            //Constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);", locator.MinimizeServiceMenu(ServiceTitle));
                            //Constant.jse.ExecuteScript("window.scrollBy(0, -200)");
                            ds.DowngradeService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                            Console.WriteLine("Downgrade the service..");
                            //      Constant.jse.ExecuteScript("arguments[0].click();", locator.MinimizeServiceMenu(ServiceTitle));
                            isdowngrade = true;
                            break;
                        }
                        }
                    

                }
                if(ServiceListPrice>0||ServiceListPrice==0 && !isdowngrade)
                {
                    Console.WriteLine("There is no service to to downgrade...");
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
            Constant locator = new Constant(WebDriver);
            try
            {
                Thread.Sleep(3000);
                configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).FirstOrDefault(x => x.Displayed).Click();//clik on radio button
                
            }
            catch(ElementClickInterceptedException ex)
            {
                Console.WriteLine(ex.Message);
                if (tries < 4)
                {
                    Console.WriteLine("Scroll Attempted..");
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
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                locator.jse.ExecuteScript("window.scrollBy(0, 800)");

                locator.ServiceMenu.Click();
                locator.jse.ExecuteScript("window.scrollBy(0, 200)");

                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, ModuleId, OfferingId);
                
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
        
            

      
