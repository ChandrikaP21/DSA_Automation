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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06
{
    public class ScenarioMain : D01.ScenarioMain
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

        



        public void ExecuteTestcases(TestCase test, string ScenarioId, int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
           Constant  cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            try
            {

               

                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds, cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds, cs);

                    ds.PnrLog.ServiceType = "1";
                }

                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewQuote();
                }
                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);

                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeServices(cs, test, ds);

                    }
                    else
                    {
                        Console.WriteLine("Product is not available , Services  can not be added.");

                    }
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeServices(cs, test.ModuleId, test.OfferingId, ds);

                    }
                    else
                    {

                        Console.WriteLine("Product is not available , Services can not be added.");


                    }
                }



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules);
                // SmartPriceCalculation();
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, ScenarioId, testCaseNumber, RunId);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);


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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null, retriveSmartPriceData);
            }

        }
        /// <summary>
        /// Upgrade Service with Module ID 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void UpGradeServices(Constant locator, TestCase test, DSAPageObject ds)
        {
            try
            {
                ConfigurationPage configPage = new ConfigurationPage(WebDriver);
                string ServiceTitle = "";
                decimal ServiceListPrice = 0;
                string ServiceId = "";

                configPage.ClickOnConfigItem(ds.OrderCode);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                locator.jse.ExecuteScript("window.scrollBy(0, 1000)");

                locator.ServiceMenu.Click();
                locator.jse.ExecuteScript("window.scrollBy(0, 200)");

                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId, ds.PnrLog.ServiceType);// add service ID 
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

                            locator.jse.ExecuteScript("window.scrollBy(0,-300)");
                            decimal servicelistPrice = 0;
                            foreach (IWebElement listPrice in configPage.AllServiceListPrice(moduleTitle).Where(x => x.Displayed))
                            {

                                if (listPrice.Enabled && listPrice.Displayed)
                                {

                                    if (locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(listPrice.Text)))
                                    {
                                        try
                                        {
                                            if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text)) > ServiceListPrice)
                                            {
                                                locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Location.X, configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Location.Y);
                                                locator.jse.ExecuteScript("window.scrollBy(0, -250)");
                                                servicelistPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text));
                                                configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//clik on radio button
                                                Thread.Sleep(6000);
                                                //     new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementIsVisible(ByClickOnServiceModule(ModuleId)));                                                                  // ServiceTitle = ServiceTitleBasedOnListPrice(serviceOption.Text);
                                                //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;
                                                Actions action = new Actions(WebDriver);
                                                action.MoveToElement(locator.ClickOnServicesWithModuleId(test.ModuleId));

                                                ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;
                                                ds.UpgradeService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, servicelistPrice, 0, true, ds.PnrLog.ServiceType);
                                                Console.WriteLine(" Upgrade service is completed");
                                                locator.jse.ExecuteScript("window.scrollBy(0,-400)");
                                                //servicelistPrice = Convert.ToDecimal(listPrice.Text.Replace("$", "").Replace(",", ""));
                                                //locator.jse.ExecuteScript("arguments[0].click();", locator.MinimizeServiceMenu(ServiceTitle));
                                                locator.MinimizeServiceMenu(ServiceTitle).Click();

                                                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("service is upgraded", true);

                                                break;
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("There is no list price to upgrade");

                                        }
                                    }
                                }

                            }
                            if (servicelistPrice < ServiceListPrice && servicelistPrice == 0)
                            {
                                Console.WriteLine("There is no list price to upgrade");
                            }

                        }
                        else
                        {
                            // select service which has listprice non zero.
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw new NoSuchElementException(@"[ModuleId is not in present on the Sevice Configuration Screen]", locator.ClickOnServicesWithModuleId(test.ModuleId));
                }
                //Thread.Sleep(15000);

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
        public void UpGradeServices(Constant locator, string ModuleId, string OfferingId, DSAPageObject ds)

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

                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, ModuleId, OfferingId);//base
                if (locator.IsElementPresent(locator.ByClickOnServiceModule(ModuleId)))
                {
                    locator.ClickOnServicesWithModuleId(ModuleId).Click();//click on Module ID associated with the service
                    locator.jse.ExecuteScript("window.scrollBy(0,600)");
                    if (locator.IsElementPresent(locator.BySelectService(OfferingId)))
                    {
                        locator.SelectServiceId(OfferingId).Click();

                        Thread.Sleep(10000);
                        string headerText = configPage.Moduletext(ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");
                        if (locator.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()='" + OfferingId + "'])[2]")))
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(OfferingId).Text));
                        }
                        else
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPriceCheckBox(OfferingId).Text));
                        }
                        ServiceTitle = locator.ServiceTitle(OfferingId).Text;

                        locator.jse.ExecuteScript("window.scrollBy(0,-200)");
                        ds.UpgradeService(ModuleId, headerText, ServiceTitle, ServiceListPrice, 0, true, null);
                        Console.WriteLine(" Upgrade service is completed");
                        locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", locator.MinimizeText(ServiceTitle).Location.X, locator.MinimizeText(ServiceTitle).Location.Y);
                        locator.jse.ExecuteScript("window.scrollBy(0, -250)");
                        //serviceTitle.Click();
                        locator.MinimizeText(ServiceTitle).Click();


                    }
                    else
                    {
                        Console.WriteLine("Service id is not present on the screen");
                    }
                }
                else
                {
                    Console.WriteLine("Module Id is not present on the screen");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



            locator.CurrentQuote.Click();
            Thread.Sleep(20000);






        }
    }
}


 
        
            

      
