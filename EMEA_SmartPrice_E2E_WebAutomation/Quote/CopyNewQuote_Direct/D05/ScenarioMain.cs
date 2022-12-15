using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;

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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D05
{
    public class ScenarioMain :D01.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL,IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
       
        public ScenarioMain() : base()
        {
            

        }
       


        public void ExecuteTestcases(TestCase test, string ScenarioID,int testCaseNumber,string country = null, List<string> application = null)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
             string  draftQuote = "";
            ds.country = country;
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
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(5), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }

                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                bool IsOfferingId = true;
         //       cs.ExpandProductfromShippingGroup(WebDriver);
                switch (test.UpdateServices)
                {

                    case "ADD":
                        if (string.IsNullOrEmpty(test.ModuleId))
                        {
                            AddService(cs, ds);

                        }
                        if (test.ModuleId != "" && string.IsNullOrEmpty(test.OfferingId))
                        {
                            AddService(cs, test, ds);

                        }
                        if (test.ModuleId != "" && !string.IsNullOrEmpty(test.OfferingId))
                        {
                            AddService(cs, test, ds, IsOfferingId);
                        }
                       
                        break;
                        
                }


                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioID, ds, ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules);
                if (application.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, country, dsaQuoteSummaryPage);

                }
                if (application.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioID, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioID,testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioID, retriveSmartPriceData);

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                ShowStopperException exec = new ShowStopperException("Exception Occured in D05", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null,retriveSmartPriceData);
            }
        }
        /// <summary>
        /// Remove already added services based on module ID and service ID.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        public void RemoveServiceWithServiceId(Constant element, TestData_Module test, DSAPageObject ds)
        {
            ClickOnConfigItem(element, ds);
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
            element.jse.ExecuteScript("window.scrollBy(0, 800)");
            //      Thread.Sleep(3000);

            element.ServiceMenu.Click();
            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
            element.jse.ExecuteScript("window.scrollBy(0,300)");
            //       constant.CheckAccessories(test).Click();
            if (element.NoneElement != null)
            {
                element.SelectServiceId(test.OfferingId).Click();
            }
            element.NoneElement.FirstOrDefault().Click();




        }
        public void RemoveServiceWithModuleId(Constant element, TestData_Module test, DSAPageObject ds)
        {

            ClickOnConfigItem(element, ds);
            element.jse.ExecuteScript("window.scrollBy(0, 600)");
            Thread.Sleep(3000);
            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
            element.ServiceMenu.Click();
            element.ClickOnServicesWithModuleId(test.ModuleId).Click();
            element.jse.ExecuteScript("window.scrollBy(0,300)");
            ds.ServiceHeaderText = element.ServiceHeaderText.Text.Replace(" ", "").Replace("/", "").Replace("?", "").Replace("!", "");
            if (element.IsElementPresent(By.XPath("(//*[@id='productConfig_" + ds.ServiceHeaderText + "_selection_1'])[2]")))//
            {
                WebDriver.FindElement(By.XPath("(//*[@id='productConfig_" + ds.ServiceHeaderText + "_selection_1'])[2]")).Click();
                ds.ConfigServiceListPrice = element.ConfigServiceListPrice(ds.ServiceHeaderText).Text.Replace("$", "").Replace(",", "");
                element.MinimizeText(ds.ServiceHeaderText).Click();
            }
            Thread.Sleep(15000);
            // new WebDriverWait(WebDriver, TimeSpan.FromSeconds(7)).Until(ExpectedConditions.ElementToBeClickable(constant.ByCurrentQuote));
            element.CurrentQuote.Click();
            Thread.Sleep(20000);
            //new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(constant.ByDraftQuoteNumber));




        }

        /// <summary>
        /// Add Services based on Module ID and service ID(Sku number)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        public void AddService(Constant element, TestCase test, DSAPageObject ds, bool IsOfferingId)
        {
            string ServiceTitle = "";
            //string ServiceId = "";
            decimal ServiceListPrice = 0;
            try
            {
                ClickOnConfigItem(element, ds);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
               // WebDriverUtils.ScrollIntoView(WebDriver, element.ByServicesMenu);
                //      Thread.Sleep(3000);

                //element.ServiceMenu.Click();
                
                RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, ds.PnrLog.ServiceType, test.ModuleId);
                element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                if(element.IsElementPresent(element.BySelectService(test.OfferingId)))
                {
                    element.SelectServiceId(test.OfferingId).Click();

                    Thread.Sleep(6000);
                    ds.ServiceHeaderText = element.GetServices(test.ModuleId);
                    var service = element.GetServices(test.ModuleId).Replace(" ", "");
                    if (element.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()='" + test.OfferingId + "'])[2]")))
                    {
                        ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPrice(test.OfferingId).Text,ds.country));
                    }
                    else
                    {
                        ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(element.ConfigServiceListPriceCheckBox(test.OfferingId).Text,ds.country));
                    }
                    ServiceTitle = element.ServiceTitle(test.OfferingId).Text;
                    ds.AddService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Service " + ds.ServiceHeaderText + " is added ", true);
                    element.jse.ExecuteScript("window.scrollBy(0,-200)");
                }

                element.ClickOnConfigurationViewQuote();
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to Add Service with module Id and service id  ,Please chcek AddService() in Scenario Main Class,Main Clas-D05", ex);
            }
            
        }
        /// <summary>
        /// Add Services with Module Id 
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="test"></param>
        /// <param name="ds"></param>
        
        public void AddService(Constant element, TestCase test, DSAPageObject ds)
        {
            try
            {
                string ServiceTitle = "";
                string ServiceId = "";
                decimal ServiceListPrice = 0;

                ClickOnConfigItem(element, ds);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                //WebDriverUtils.ScrollIntoView(WebDriver, element.ByServicesMenu);

                //element.ServiceMenu.Click();
                RetriveBaseServiceDetails(element, ds, ServiceTitle, ServiceListPrice, test.ModuleId, test.OfferingId);

                element.ClickOnServicesWithModuleId(test.ModuleId).Click();
           

                ds.ServiceHeaderText = element.ServiceHeaderText.Text;
                string ServiceHeaderText = element.ServiceHeaderText.Text.Replace(" ", "").Replace("/", "").Replace("?","").Replace("!","");
                ConfigurationPage configPage = new ConfigurationPage(WebDriver);
                foreach (IWebElement ele in element.ServiceListPrice(ServiceHeaderText))
                {


                    if (!ele.Size.IsEmpty && GenericHelper.ConvertCurrencyIntoString(ele.Text,ds.country) != "0.00" && ele.Enabled && ele.Displayed && element.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(ele.Text)))
                    {
                        //ele.Click();


                        string a = ele.Text;
                        IWebElement ServiceOption = WebDriver.FindElement(By.XPath("(//*[text()='" + ele.Text + "']/../../../../td[1])[2]"));

                        ServiceOption.Click();
                        Thread.Sleep(6000);
                        ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(a));
                        element.jse.ExecuteScript("window.scrollBy(0,200)");

                        element.jse.ExecuteScript("arguments[0].scrollIntoView(true);", element.GetTitleWithModuleId(test.ModuleId, ServiceHeaderText));
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(configPage.ByComponentTitle(ServiceHeaderText)));
                        configPage.FindComponentTitle(ServiceHeaderText).Click();//to monimize the component
                        ServiceTitle = configPage.FindComponentTitle(ServiceHeaderText).Text;

                        ds.AddService(test.ModuleId, ds.ServiceHeaderText, ServiceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Service " + ds.ServiceHeaderText + " is added ", true);


                        break;
                    }



                }

                element.ClickOnConfigurationViewQuote();
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to Add Service with module Id ,Please check AddService() in Scenario Main Class,Main Clas-D05", ex);
            }

        }

        public void ClickOnConfigItem(Constant constant, DSAPageObject ds)
        {
            try
            {
                //     constant.IsElementPresent(By.XPath("//*[text()=' " + test.ProductIdentificationData + " ']"));
                constant.jse.ExecuteScript("window.scrollBy(0, -1100)");
                Thread.Sleep(2000);
                constant.ConfigItem.Click();
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("unable to  click  On Prouct config", ex);
            }
        }
        public void RetriveBaseServiceDetails(Constant constant, DSAPageObject ds, string serviceTitle, decimal ServiceListPrice, string ServiceType, string ModuleId = null, string ServiceId = null)
        {
            try
            {
                List<IWebElement> service = constant.ProductServiceConfigText.Where(x => x.Displayed).ToList();
                foreach (var availableService in service)//find all non none element
                {

                    if (availableService.Text != "None" && availableService.Text != "None Selected" && !availableService.Text.StartsWith("No"))
                    {

                        serviceTitle = availableService.Text;

                        string OfferingId = "";


                        availableService.Click();
                        string[] str = serviceTitle.Split('[');
                        for (int i = 1; i < str.Length; i++)
                        {
                            OfferingId += "[" + str[i];
                        }
                        OfferingId = " " + OfferingId + " ";
                        Thread.Sleep(3000);
                        IWebElement element = WebDriver.FindElement(By.XPath("(//*[text()='" + serviceTitle + "']//preceding::label[1])[2]"));
                        string text = element.FindElement(By.XPath("./span[2]")).Text;
                  //      string text = WebDriver.FindElement(By.XPath("(//*[text()='"+ serviceTitle + "']//preceding::label[1])[2]")).Text;
                        string[] moduleText = element.Text.Split(']');
                        ModuleId = moduleText[0].Replace("[","");
                  //      ds.ServiceHeaderText = moduleText[1].Replace("(Non-Tied)", "").Trim();
                        ds.ServiceHeaderText = text.Replace("(Non-Tied)", "").Trim();
                        string headerText = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");
                        
                        headerText= RemoveDietaries(headerText);
                        //ds.ServiceHeaderText = constant.ServiceHeaderText.Text;
                        constant.jse.ExecuteScript("window.scrollBy(0,300)");

                        ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                        configurationPage.FindStrikeOfListPrice(OfferingId,ref ServiceListPrice, ds.country);
                        ds.AddService(ModuleId, ds.ServiceHeaderText, serviceTitle, ServiceListPrice, 0, false, ServiceType);
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Base Service details are retrived", true);
                        constant.jse.ExecuteScript("arguments[0].click();", availableService);
                        
                        constant.jse.ExecuteScript("window.scrollBy(0,-300)");

                    }

                }
            }
            catch (Exception ex)
            {
                 WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to retrive base service details",ex);
            }



        }
        /// <summary>
        /// Auto adding service
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="ds"></param>
        /// <param name="configPage"></param>
        public void AddService(Constant constant, DSAPageObject ds)
        {
            try
            {
                WebDriver = constant.WebDriver;
                ClickOnConfigItem(constant, ds);
                ConfigurationPage configPage = new ConfigurationPage(WebDriver);
                Thread.Sleep(5000);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(constant.ByServicesMenu));
                ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
                configurationPage.ClickOnServiceTab();
                string serviceTitle = "";
                decimal ServiceListPrice = 0;
                string ModuleId = "";
                string ServiceId = "";
                string servicedesc = "";

                // Find all base service listprice,title,module id..
                RetriveBaseServiceDetails(constant, ds, serviceTitle, ServiceListPrice, null, null, ds.PnrLog.ServiceType);
                //find all non base services listprice, title, module id
                foreach (var service in constant.NoneElement)
                {
                    bool isclicked = false;
                    bool isFound = false;
                    int k = 0;
                    if (service.Displayed == true && service.Text == "None" || service.Text == "None Selected")
                    {
                       
                        service.Click();
                   
                        servicedesc = constant.ServiceHeaderText.Text;
                        ds.ServiceHeaderText = constant.ServiceHeaderText.Text.Replace(" ", "").Replace("/", "");
                        ds.ServiceHeaderText = ds.ServiceHeaderText.Split('-')[0];
                        ds.ServiceHeaderText= RemoveDietaries(ds.ServiceHeaderText);
                     
                        ModuleId = WebDriver.FindElement(By.XPath("(//label[starts-with(@id,'productConfig_option_')]/span[text()='"+ servicedesc + "']/../span[1])[2]")).Text;


                        //if (constant.IsElementPresent(By.XPath("//input[starts-with(@id,'productConfig_" + ds.ServiceHeaderText + "_selection_0')]")))//to check none
                        //{
                        //  Find List Price which is not null...
                        foreach (IWebElement ele in constant.ServiceListPrice(ds.ServiceHeaderText))
                        {


                            if (!ele.Size.IsEmpty && GenericHelper.ConvertCurrencyIntoString(ele.Text,ds.country) != "0.00" && ele.Enabled && ele.Displayed && constant.IsElementPresent(configPage.ByServiceOptionClickBasedOnListPrice(ele.Text)))
                            {
                                //ele.Click();

                                
                                string a = ele.Text;
                                
                                try
                                {
                                    IWebElement ServiceOption = ele.FindElement(By.XPath("./../../../../td[1]/div[2]//label"));

                                    WebDriverUtils.WebDriverElementToBeClickable(WebDriver, ServiceOption);
                                    ServiceOption.Click();
                                }
                                catch(Exception ex)
                                {

                                }
                                
                                Thread.Sleep(6000);
                                ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(a,ds.country));
                                //configurationPage.FindStrikeOfListPrice();
                                constant.jse.ExecuteScript("window.scrollBy(0,-200)");

                                constant.jse.ExecuteScript("arguments[0].scrollIntoView(true);", constant.GetTitleWithModuleId(ModuleId, ds.ServiceHeaderText));
                                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(configPage.ByComponentTitle(ds.ServiceHeaderText)));
                                MinimizeChevron(ds.ServiceHeaderText);
                               
                                serviceTitle = configPage.FindComponentTitle(ds.ServiceHeaderText).Text;

                                ds.AddService(ModuleId, servicedesc, serviceTitle, ServiceListPrice, 0, true, ds.PnrLog.ServiceType);
                                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Service "+ servicedesc + " is added ",true);
                          
                           //     Constant.jse.ExecuteScript("arguments[0].click();", constant.MinimizeServiceMenu(ServieDesc));
                                
                                isclicked = true;
                                isFound = true;

                                break;
                            }



                        }



                    }
                    else
                    {
                        isclicked = true;
                    }
                    if (!isclicked && service.Displayed)
                    {
                        constant.jse.ExecuteScript("arguments[0].click();", service);

                    }
                    if (isFound)
                    {
                        break;
                    }

                }

                Thread.Sleep(5000);
                constant.ClickOnConfigurationViewQuote();

            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }

      
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to Add Service  with Auto mode ,Please check AddService() in Scenario Main Class,Main Clas-D05", ex);
            }





        }

        public bool  MinimizeChevron(string serviceHeaderText)
        {
            bool isClikced = false;
            try
            {
                ConfigurationPage page = new ConfigurationPage(WebDriver);
                
                for (int i = 0; i < 10; i++)
                {


                    try
                    {

                        IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
                        jse.ExecuteScript("window.scrollBy(0,-250)", page.FindComponentTitle(serviceHeaderText));
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(page.FindComponentTitle(serviceHeaderText)));
                        page.FindComponentTitle(serviceHeaderText).Click();//to monimize the component


                        isClikced = true;

                    }
                    catch (Exception)
                    {

                        isClikced = false;

                    }
                    if (isClikced == true)
                    {
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage($"{ex.Message},{ex.StackTrace}");
            }
            return isClikced;
        }

        public void DeleteService(Constant element, TestData_Module test, DSAPageObject ds)
        {
            try
            {
                if (test.ModuleId == null && test.OfferingId == null)
                {
                    ClickOnConfigItem(element, ds);
                    element.jse.ExecuteScript("window.scrollBy(0, 900)");
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                    element.ServiceMenu.Click();

                    if (WebDriver.FindElement(By.XPath("(//*[text()='" + ds.ServiceName + "'])[2]")).Displayed)
                    {
                        WebDriver.FindElement(By.XPath("(//*[text()='" + ds.ServiceName + "'])[2]")).Click();
                        element.ServiceConfig(4).Click();//click on the 'None' radio button to remove the service.
                    }
                }
                if (test.ModuleId != null && test.OfferingId == null)
                {

                }
                if (test.ModuleId != null && test.OfferingId != null)
                {
                    try
                    {
                        ClickOnConfigItem(element, ds);
                        new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(element.ByServicesMenu));
                        element.jse.ExecuteScript("window.scrollBy(0, 800)");
                        //      Thread.Sleep(3000);

                        element.ServiceMenu.Click();
                        element.ClickOnServicesWithModuleId(test.ModuleId).Click();
                        element.jse.ExecuteScript("window.scrollBy(0,300)");
                        //       constant.CheckAccessories(test).Click();
                        var a = element.ServiceHeaderText.Text;
                        WebDriver.FindElement(By.XPath("(//*[@id='productConfig_DellServices:ExtendedBatteryServices_selection_0'])[2]")).Click();
                        //*[@id="productConfig_option_0_DellServices:ExtendedBatteryServices_toggle"]/div[2]/text()
                        WebDriver.FindElement(By.XPath("(//*[@id='productConfig_option_0_DellServices: ExtendedBatteryServices_toggle']/div[2]/text())[2]")).Click();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Module Id  and Service Id is not found");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static bool RemoveNotMatchedOrderCode(IWebElement ele)
        {
            return ele != null ? true : false;

        }
        /// <summary>
        /// Remove all other Products .
        /// </summary>
        /// <param name="element"></param>
        //public void RemoveAllProductsExceptOneWithBaseService(Constant element, string OrderCode)
        //{
          
           
        //    int i = 0;
        //    bool IsproductFound = false;
        //    List<IWebElement> viewmore = WebDriver.FindElements(By.XPath("//*[text()=' View More']")).ToList();
        // //   List<IWebElement> orderCodeAndSku = WebDriver.FindElements(By.XPath("//*[text()='Sku Number' or text()='Order Code']")).ToList();
         
        //    while (i < viewmore.Count)
        //    {
        //        try
        //        {

        //            Actions action = new Actions(WebDriver);
        //            action.MoveToElement(viewmore[i]);
        //            action.Click().Build().Perform();
        //            if (element.DSAOrderCode(i).Displayed == true && !element.DSAOrderCode(i).Text.Equals(OrderCode))
        //            {

        //                string ordercode = element.DSAOrderCode(i).Text;
        //                Constant.jse.ExecuteScript("window.scrollBy(0,-600)");
        //                element.RemoveProduct(ordercode).Click();

        //                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.InvisibilityOfElementLocated(element.ByDSAOrderCode(i)));
        //                //Console.WriteLine(ordercode + "is removed");
        //                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("{0} is removed",ordercode),true);
        //                viewmore = WebDriver.FindElements(By.XPath("//*[text()=' View More']")).ToList();
        //                Thread.Sleep(2000);
        //                IsproductFound = true;
        //                i = 0;

        //            }
        //            else
        //            {
        //                element.ViewLess.Click();
        //                i++;
        //            }
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
        //            //Console.WriteLine(ex.Message);
        //        }

        //    }
        //}





    }
    
}




