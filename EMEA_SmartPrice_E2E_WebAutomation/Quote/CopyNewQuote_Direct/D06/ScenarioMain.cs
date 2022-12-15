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
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06
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

        



        public void ExecuteTestcases(TestCase test, string ScenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
           Constant  cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            ds.country = country;
            string draftQuote = "";
            try
            {

               

                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    //odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
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
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(10), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
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
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                if (application.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, country, dsaQuoteSummaryPage);

                }
                if (application.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, ScenarioId, testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceData);

                //WebDriver.Close();
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                ShowStopperException exec = new ShowStopperException("Exception Occured in D06", e);
                exec.LogException();

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
            WebDriver = locator.WebDriver;
            try
            {
                ConfigurationPage configPage = new ConfigurationPage(WebDriver);
                string ServiceTitle = "";
                decimal ServiceListPrice = 0;
                string ServiceId = "";
                
                configPage.ClickOnConfigItem(ds.OrderCode);
                if (locator.IsElementPresent(locator.ByServicesMenu))
                {
                    //    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(25)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                    //    try
                    //    {
                    //        WebDriverUtils.ScrollIntoView(WebDriver, locator.ByServicesMenu);
                    //        locator.ServiceMenu.Click();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        WebDriverUtils.ScrollIntoView(WebDriver, locator.ByServicesMenu);
                    //    }







                    configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId, ds.PnrLog.ServiceType);// add service ID 
                    try
                    {
                        if (locator.IsElementPresent(locator.ByClickOnServiceModule(test.ModuleId)))
                        {
                            WebDriverUtils.ScrollIntoView(WebDriver, locator.ByClickOnServiceModule(test.ModuleId));
                            
                            ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;

                            if (!ServiceTitle.Contains("None") && !ServiceTitle.Contains("None Selected") && !ServiceTitle.Contains("No"))
                            {
                                locator.ClickOnServicesWithModuleId(test.ModuleId).Click();//click on Module ID associated with the service
                                string[] str = ServiceTitle.Split('[');
                                for (int i = 1; i < str.Length; i++)
                                {
                                    ServiceId += "[" + str[i];
                                }
                                ds.ServiceHeaderText = configPage.Moduletext(test.ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                                ds.ServiceHeaderText = RemoveDietaries(ds.ServiceHeaderText);
                                string moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                                locator.jse.ExecuteScript("window.scrollBy(0,800)");
                                //To find the ServiceList Price based on the service type
                                if (locator.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()=' " + ServiceId + " '])[2]")))
                                {
                                    ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(" " + ServiceId + " ").Text,ds.country));
                                }
                                else
                                {
                                    ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPriceCheckBox(" " + ServiceId + " ").Text, ds.country));
                                }

                                locator.jse.ExecuteScript("window.scrollBy(0,-300)");
                                decimal servicelistPrice = 0;
                                foreach (IWebElement listPrice in configPage.AllServiceListPrice(moduleTitle).Where(x => x.Displayed))
                                {

                                    if (listPrice.Enabled && listPrice.Displayed)
                                    {

                                        //if (locator.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(listPrice.Text)))
                                        //{
                                            try
                                            {
                                                if (Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text,ds.country)) > ServiceListPrice)
                                                {
                                                    locator.jse.ExecuteScript("window.scrollTo(arguments[0],arguments[1])", configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).FirstOrDefault(x => x.Displayed).Location.X, configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).FirstOrDefault(x => x.Displayed).Location.Y);
                                                    locator.jse.ExecuteScript("window.scrollBy(0, -250)");
                                                    servicelistPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(listPrice.Text,ds.country));
                                                    try
                                                    {
                                                        configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).FirstOrDefault(x=>x.Displayed).Click();//clik on radio button

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        listPrice.FindElement(By.XPath("./ancestor::tr/td/div[2]")).Click();
                                                    }

                                                    Thread.Sleep(6000);
                                                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByViewQuote));                                                                  // ServiceTitle = ServiceTitleBasedOnListPrice(serviceOption.Text);
                                                    //configPage.ServiceOptionClickBasedOnListPrice(listPrice.Text).Click();//minimize the text;
                                                    Actions action = new Actions(WebDriver);
                                                    action.MoveToElement(locator.ClickOnServicesWithModuleId(test.ModuleId));

                                                    ServiceTitle = locator.ClickOnServicesWithModuleId(test.ModuleId).Text;
                                                    ds.UpgradeService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, servicelistPrice, 0, true, ds.PnrLog.ServiceType);
                                                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Upgrade service is completed", true);
                                                    
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
                                        //}
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
                        WebDriverUtils.TakeSnapShot(WebDriver);
                        throw new ShowStopperException("Unable to upgrade service",ex);
                        //throw new NoSuchElementException(@"[ModuleId is not in present on the Sevice Configuration Screen]", locator.ClickOnServicesWithModuleId(test.ModuleId));
                    }
                    //Thread.Sleep(15000);
                }
                locator.ClickOnConfigurationViewQuote();
                Thread.Sleep(5000);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage($"{ex.Message}{ex.StackTrace}");
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
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(locator.ByServicesMenu));
                //WebDriverUtils.ScrollIntoView(WebDriver, locator.ByServicesMenu);
                //locator.ServiceMenu.Click();
               

                configPage.RetriveBaseServiceDetails(locator, ds, ServiceTitle, ServiceListPrice, ModuleId, OfferingId);//base
               
                    WebDriverUtils.ScrollIntoView(WebDriver,locator.ByClickOnServiceModule(ModuleId));
                    locator.ClickOnServicesWithModuleId(ModuleId).Click();//click on Module ID associated with the service
                    
                   
                        locator.SelectServiceId(OfferingId).Click();

                        Thread.Sleep(10000);
                        string headerText = configPage.Moduletext(ModuleId).Text;//ds.ServiceHeaderText = locator.ServiceHeaderText.Text;
                        moduleTitle = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");
                        if (locator.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()='" + OfferingId + "'])[2]")))
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPrice(OfferingId).Text,ds.country));
                        }
                        else
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(locator.ConfigServiceListPriceCheckBox(OfferingId).Text,ds.country));
                        }
                        ServiceTitle = locator.ServiceTitle(OfferingId).Text;

                        locator.jse.ExecuteScript("window.scrollBy(0,-200)");
                        ds.UpgradeService(ModuleId, headerText, ServiceTitle, ServiceListPrice, 0, true, null);
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Upgrade service is completed", true);
                        WebDriverUtils.ScrollIntoView(WebDriver, locator.MinimizeText(ServiceTitle));


                        locator.ClickOnServicesWithModuleId(ModuleId).Click();


                   
              
            }
            catch (ShowStopperException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException(" Unable to Upgarde Service with Module Id and Service Id", ex);
            }

            locator.ClickOnConfigurationViewQuote();
            

        }
    }
}


 
        
            

      
