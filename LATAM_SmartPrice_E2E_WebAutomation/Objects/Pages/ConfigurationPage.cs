using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Helper;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Objects.Pages
{
  public class ConfigurationPage
   
    {
        public IWebDriver WebDriver;
        public  ConfigurationPage(IWebDriver driver)
        {
            WebDriver = driver;
        }
        #region Service
        #region By
        public By ByServiceOption(string Text) => By.XPath("//*[starts-with(@id,'productConfig_DellServices:ExtendedService_selection_')]");
        public By BySelectedServiceListPrice => By.XPath("/../following::td[4]");
        public By ByAllServiceListPrice(string Text) => By.XPath("//*[starts-with(@id,'productConfig_0_" + Text + "_listPrice_')]/div/div/span[1]");
        public By ByModuleText(string ModuleId) => By.XPath("(//label[@id='productConfig_option_"+ModuleId+"']/span[2])[2]");
        public By ByServiceTitle(string Text) => By.XPath("(//*[contains(text(),'" + Text + "')]/../../../../td[1]/input[1])[2]/following::td[1]");
        public By ByServiceOptionClickBasedOnListPrice(string listPrice) => By.XPath("(//*[text()='" + listPrice + "']/../../../../td[1]/input)[2]");
        public By ByServiceOptionClickBasedOnListPriceComponent => By.XPath("./../../../../td[1]/input");
        public By ByConfigItem(int count) => By.XPath("//*[@id='quoteCreate_LI_configItem_0_" + count + "']");
        public By ByServiceTitleBasedOnListPrice(string list) => By.XPath("(//*[contains(text(),'" + list + "')]/../../../../td[1]/input[1])[2]/following::td[1]");
        #endregion
        #region WebElement
        public IWebElement Moduletext(string ModuleId) => WebDriver.FindElement(ByModuleText(ModuleId));
        public List<IWebElement> ServiceOption(string Text) => WebDriver.FindElements(ByServiceOption(Text)).ToList();
        public IWebElement SelectedServiceListPrice(IWebElement Text) => Text.FindElement(BySelectedServiceListPrice);
        public List<IWebElement> AllServiceListPrice(string text) => WebDriver.FindElements(ByAllServiceListPrice(text)).ToList();
        public IWebElement ServiceTitleBasedOnListPrice(string Text)=>WebDriver.FindElement(ByServiceTitle(Text));
         public IWebElement ConfigItem(int i) => WebDriver.FindElement(ByConfigItem(i));
        //public IWebElement ConfigItem(int i) => WebDriver.FindElement(ByConfigItem(i));
        //Click the element based on the listprice
        public IWebElement ServiceOptionClickBasedOnListPrice(string title) => WebDriver.FindElement(ByServiceOptionClickBasedOnListPrice(title));
        public IWebElement ServiceOptionClickBasedOnListPrice(IWebElement listpriceelement) => listpriceelement.FindElement(ByServiceOptionClickBasedOnListPriceComponent);
        //Find service title of a service based on ListPrice.
        public IWebElement ServiceTitleListPrice(string list) => WebDriver.FindElement(ByServiceTitleBasedOnListPrice(list));
        #endregion
        #endregion
        #region Accessories
        #region By
        public By ByModuleIdTitle(string moduleId) => By.XPath("(//*[text()='"+ moduleId + "']/following::span[1])[2]");
        public By ByaccesorySelection(string moduleHeader) => By.XPath("(//*[@id='productConfig_option_0_"+moduleHeader+"_toggle'])[2]");
        public By ByModuleId(string moduleId) => By.XPath("(//*[@id='productConfig_option_"+moduleId+"'])[2]");
        public By ByAccessoryInputSelection(string moduleHeader,int count) => By.XPath("(//input[@id='productConfig_"+ moduleHeader + "_selection_"+count+"'])[2]");
        public By ByGreenTextElement(string headerText) => By.XPath("(//*[contains(@id,'productConfig_0_" + headerText + "_listPrice_')])[2]//span[@class='greenText']");

        #endregion
        #region WebElement
        public IWebElement ModuleIdTitle(string moduleId) => WebDriver.FindElement(ByModuleIdTitle(moduleId));
        public IWebElement accesorySelection(string moduleHeader) => WebDriver.FindElement(ByaccesorySelection(moduleHeader));
        public IWebElement AccessoryInputSelection(string moduleHeader,int count) => WebDriver.FindElement(ByAccessoryInputSelection(moduleHeader, count));
        public IWebElement SearchByModuleId(string moduleId) => WebDriver.FindElement(ByModuleId(moduleId));
        public IWebElement GreenTextElement( string headerText) => WebDriver.FindElement(ByGreenTextElement(headerText));

        #endregion

        #endregion
        #region Component
        #region By
        public By BySystemMenu => By.XPath("(//*[text()='System'])[1]");
        public By ByComponentTitle(string moduleHeader) => By.XPath("(//a[@id='productConfig_option_0_"+ moduleHeader + "_toggle']/div[@class='module-desc'])[2]");
        public By ByComponentModuleTitle => By.XPath("//label[starts-with(@id,'productConfig_option_')]");
        #endregion
        #region WebElement
        public IWebElement SystemMenu => WebDriver.FindElement(BySystemMenu);
        public IWebElement FindComponentTitle(string moduleHeader) => WebDriver.FindElement(ByComponentTitle(moduleHeader));//component title
        public List<IWebElement> ComponentModuleTitle=> WebDriver.FindElements(ByComponentModuleTitle).ToList();
        #endregion
        #endregion
        #region Method
        public void ClickOnConfigBasedOnOrderCode(string OrderCode)
        {

            try
            {
                Constant element = new Constant(WebDriver);
                ConfigurationPage configpage = new ConfigurationPage(WebDriver);
                int i = 0;
                
                List<IWebElement> viewmore = WebDriver.FindElements(By.XPath("//*[text()=' View More']")).ToList();
                
                string ordercodeid = "";
                string configItem = "";
                
                while (i < viewmore.Count)
                {
                    try
                    {
                        ordercodeid = "quoteCreate_LI_orderCode_0_" + i + "";
                        configItem = "quoteCreate_LI_removeItem_0_" + i + "";
                        Actions action = new Actions(WebDriver);
                        action.MoveToElement(viewmore[i]);
                        action.Click().Build().Perform();
                        
                        //if (element.IsElementPresent(By.XPath("//*[contains(text(),'" + OrderCode + "')]")))
                        //{
                        if (element.OrderCode(OrderCode).Displayed)
                        {



                            string ordercode = element.DSAOrderCode(i).Text;
                            
                            WebDriverUtils.ScrollIntoView(WebDriver, configpage.ByConfigItem(i));
                            configpage.ConfigItem(i).Click();

                            break;
                        }
                          
                        //}
                        else
                        {
                            if (element.IsElementPresent(element.ByViewLess))
                            {
                                element.ViewLess.Click();

                            }
                            i++;

                        }

                    }
                    catch (Exception ex)
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                        //Console.WriteLine(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ClickOnConfigItem(string orderCode)
        {

            try
            {   
                Constant locator = new Constant(WebDriver);
                
                
                Thread.Sleep(2000);
                if (!string.IsNullOrEmpty(orderCode))
                {
                    
                    ClickOnConfigBasedOnOrderCode(orderCode);
                }
                else
                {
                    if (locator.IsElementPresent(ByConfigItem(0)))
                    {
                        WebDriverUtils.ScrollIntoView(WebDriver, ByConfigItem(0));
                        ConfigItem(0).Click();
                    }
                     else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Config lable is not present", true);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SelectListPrice(ref decimal componentListPrice)
        {
            List<IWebElement> inputSelection = WebDriver.FindElements(By.XPath("//table//input[@type='radio']")).ToList();

            //      IWebElement inputSelection1 = WebDriver.FindElement(By.XPath("(//input[@type='radio'])[11]"));
            try
            {

                foreach (var item in inputSelection)
                {
                    if (item.Selected)
                    {
                        string text = item.FindElement(By.XPath("./../../td[6]//span")).GetAttribute("innerText");//US$250.00
                        componentListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(text));
                    }
                }


            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
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

                        string text = WebDriver.FindElement(By.XPath("(//*[text()='" + serviceTitle + "']//preceding::label[1])[2]")).Text;
                        string[] moduleText = text.Split(']');
                        ModuleId = moduleText[0].Replace("[", "");
                        ds.ServiceHeaderText = moduleText[1].Replace("(Non-Tied)", "").Trim();
                        string headerText = ds.ServiceHeaderText.Replace(" ", "").Replace("/", "");

                        //ds.ServiceHeaderText = constant.ServiceHeaderText.Text;
                        constant.jse.ExecuteScript("window.scrollBy(0,300)");


                        if (constant.IsElementPresent(By.XPath("(//input[@type='radio']/../following::td[text()='" + OfferingId + "'])[2]")))
                        {

                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(constant.ConfigServiceListPrice(OfferingId).Text));
                        }
                        else
                        {
                            ServiceListPrice = Convert.ToDecimal(GenericHelper.ConvertCurrencyIntoString(constant.ConfigServiceListPriceCheckBox(OfferingId).Text));
                        }
                        ds.AddService(ModuleId, ds.ServiceHeaderText, serviceTitle, ServiceListPrice, 0, false, ServiceType);
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Base Service details are retrived", true);
                        constant.jse.ExecuteScript("arguments[0].click();", availableService);

                        constant.jse.ExecuteScript("window.scrollBy(0,-300)");

                    }

                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }



        }
        #endregion
    }
}
