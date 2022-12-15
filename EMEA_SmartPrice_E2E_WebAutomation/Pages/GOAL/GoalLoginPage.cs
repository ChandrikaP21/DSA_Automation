using OpenQA.Selenium;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using System.Configuration;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Pages.GOAL
{
    public class GoalLoginPage : DriverHelper
    {
        private IWebDriver WebDriver { get; }
        public GoalLoginPage(IWebDriver WebDriver) => this.WebDriver = WebDriver;
        #region -- Elements --

        public IWebElement UserName => WebDriver.FindElement(_UserName);
        protected By _UserName => By.Id("username");
        public IWebElement Password => WebDriver.FindElement(_Password);
        protected By _Password => By.Id("password");
        public IWebElement LogonBtn => WebDriver.FindElement(_LogonBtn);
        protected By _LogonBtn => By.XPath("//button[@name='Logon']");

        #endregion -- Elements --


        #region -- Actions --
        public GoalHomePage LoginToGOAL(string userName, string password)
        {
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            WebDriver.waitForWaitAnimationToLoad();
            WebDriverUtils.WaitForElement(WebDriver, _UserName);
            WebElementUtil.Set(UserName, userName);
            WebElementUtil.Set(Password, password);
            LogonBtn.Click();
            WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
            return new GoalHomePage(WebDriver);
        }
        #endregion -- Actions --
        
        public string env = "PROD";//CustomConfigurationProvider.GetKey("Environment");
        public string userName = "chandrika_panigrahi";//CustomConfigurationProvider.GetKey("UserName");
        public string password = "Passw0rd_";//CustomConfigurationProvider.GetKey("Password");
        public GoalHomePage LoginToGOAL()
        {
            try
            {
                Console.WriteLine("Logging into {0} - GOAL application", env);
                WebDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["Goal"]);
                Console.WriteLine("GOAL application login page loaded sucessfully");
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                WebDriver.waitForWaitAnimationToLoad();

                if (WebDriver.Title.Contains("Log"))
                    try
                    {
                        WebDriverUtils.WaitForElement(WebDriver, _UserName);
                        WebElementUtil.Set(UserName, userName);
                        WebElementUtil.Set(Password, password);
                        LogonBtn.Click();
                        WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                        Console.WriteLine("GOAL application signed in sucessfully");
                    }
                    catch (Exception)
                    {
                    }
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Can not navigate to Goal Url,Method-LoginToGOAL, Class-GoalLogInPage ",ex);
            }

            return new GoalHomePage(WebDriver);
        }

        public GoalHomePage ReloadGoalApp()
        {
            try
            {
                Console.WriteLine("Reloading - GOAL application", env);
                WebDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["Goal"]);
                Console.WriteLine("GOAL application loaded sucessfully");
            }
            catch (Exception ex)
            {

                throw new ShowStopperException("Could not reload GoalApp", ex);
            }
            return new GoalHomePage(WebDriver);
        }

        private string GetGoalUrl(string env)
        {
            string url;
            switch (env.ToUpper())
            {
                case "GE1":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE1");
                    break;
                case "GE2":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE2");
                    break;
                case "GE3":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE3");
                    break;
                case "GE4":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE4");
                    break;
                case "PROD":
                default:
                    url = CustomConfigurationProvider.GetKey("GOAL:PROD");
                    break;
            }
            return url;
        }
    }
}
