using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using OpenQA.Selenium.Chrome;

namespace EMEA_SmartPrice_E2E_WebAutomation.Helper
{
    public class InitializeBaseClass
    {
        private readonly Dictionary<string, IWebDriver> driverStack = new Dictionary<string, IWebDriver>();
        public IWebDriver webDriver;
        TestConfigurationSettings configurationSettings = new TestConfigurationSettings();
        //public string tunnelIdentifier = CustomConfigurationProvider.GetTunnelIdentifier();
        public string path;
        public InitializeBaseClass()
        {
            configurationSettings = JsonConvert.DeserializeObject<TestConfigurationSettings>(JObject.Parse(File.ReadAllText("TestConfig.json")).ToString());
            path = PathForDriverInit();
        }
        private string PathForDriverInit()
        {
            string path;
            if (configurationSettings.WebDriverType.ToLower().Contains("saucelabs"))
            {
                path = $@"C:\Users\Administrator\Documents\";
            }
            else
            {
                path = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\";
            }
            return path;
        }
        public IWebDriver InitializeDriver()
        {
            string testName = "";
            switch (configurationSettings.WebDriverType)
            {
                case "RemoteWebDriver":
                    var remoteBrowserOptions = new ChromeOptions();
                    remoteBrowserOptions.AddArgument("disable-extensions");
                    remoteBrowserOptions.AddArgument("--no-sandbox");
                    remoteBrowserOptions.AddUserProfilePreference("download.default_directory", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    remoteBrowserOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    remoteBrowserOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    remoteBrowserOptions.AddUserProfilePreference("safebrowsing.enabled", "false");
                    var test = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    //Selenium Grid Remote Server
                    webDriver = new RemoteWebDriver(new Uri(uriString: configurationSettings.SeleniumGridServer), remoteBrowserOptions);
                    //localhost for selenium executions
                    //webDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"),chromeOptions);
                    //Local Chrome browser without Grid
                    //webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    webDriver.Manage().Window.Maximize();
                    break;
                case "IWebDriver":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("disable-extensions");
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddUserProfilePreference("download.default_directory", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "false");
                    //var test = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions, TimeSpan.FromMinutes(3));
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    webDriver.Manage().Window.Maximize();
                    //webDriver.Navigate().GoToUrl("https://www.google.co.in/");
                    //webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    //var title = webDriver.Title;
                    //Console.WriteLine(title);
                    break;
                case "SauceLabs":
                    ChromeOptions options = new ChromeOptions { BrowserVersion = "latest" };
                    options.AddUserProfilePreference("download.default_directory", path);
                    options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                    options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("intl.accept_languages", "nl");
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("safebrowsing.enabled", "false");
                    options.AddAdditionalCapability("username", "solutions_selling", true);
                    options.AddAdditionalCapability("accessKey", "bf947fe0-bc52-4fdc-8544-2b344cad3a58", true);
                    options.AddAdditionalCapability("platform", "Windows 10", true);
                    options.AddAdditionalCapability("name", testName, true);
                    options.AddAdditionalCapability("tunnelIdentifier", "SITG4", true);
                    options.AddAdditionalCapability("parentTunnel", "dell_sauce", true);
                    HttpCommandExecutor executor = new HttpCommandExecutor(new Uri("https://ondemand.us-west-1.saucelabs.com:443/wd/hub"), TimeSpan.FromSeconds(120), false)
                    {
                        Proxy = new WebProxy(new Uri("http://proxy-aus.dell.com:80"))
                    };
                    webDriver = new RemoteWebDriver(executor, options.ToCapabilities());
                    break;
                case "MoonExecution":
                    ChromeOptions option = new ChromeOptions { };
                    option.AddArgument("ignore-certificate-errors");
                    option.AddAdditionalCapability("chrome", "91.0", true);
                    option.AddAdditionalCapability("name", testName, true);
                    option.AddAdditionalCapability("enableVNC", true, true);
                    option.AddAdditionalCapability("enableVideo", true, true);
                    var t = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
                    option.AddExtension(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "/moon-chrome-auth.crx");
#if (GE1 || GE1B)
                    Dictionary<string, object> moonOptions = new Dictionary<string, object>();
                    string[] hostsEntries = { "www.dell.com:10.179.215.187", "fcs.dell.com:10.179.215.187", "afcs.dell.com:10.179.215.187" };
                    moonOptions.Add("hostsEntries", hostsEntries);
                    option.AddAdditionalCapability("moon:options", moonOptions, true);
#elif (GE2 || GE2B)
                    Dictionary<string, object> moonOptions = new Dictionary<string, object>();
                    string[] hostsEntries = { "www.dell.com:10.179.215.187", "fcs.dell.com:10.179.215.187", "afcs.dell.com:10.179.215.187" };
                    moonOptions.Add("hostsEntries", hostsEntries);
                    option.AddAdditionalCapability("moon:options", moonOptions, true);
#elif (GE3 || GE3B)
                    Dictionary<string, object> moonOptions = new Dictionary<string, object>();
                    string[] hostsEntries = { "www.dell.com:10.179.215.187", "fcs.dell.com:10.179.215.187", "afcs.dell.com:10.179.215.187" };
                    moonOptions.Add("hostsEntries", hostsEntries);
                    option.AddAdditionalCapability("moon:options", moonOptions, true);
#elif (GE4 || GE4B)
                    Dictionary<string, object> moonOptions = new Dictionary<string, object>();
                    string[] hostsEntries = { "www.dell.com:10.179.215.209", "fcs.dell.com:10.179.215.209", "afcs.dell.com:10.179.215.209" };
                    moonOptions.Add("hostsEntries", hostsEntries);
                    option.AddAdditionalCapability("moon:options", moonOptions, true);
#else
                    //path = $"{Directory.GetCurrentDirectory()}{osdelimiter}Data{osdelimiter}GE2{osdelimiter}";
#endif
                    webDriver = new RemoteWebDriver(new Uri("https://moon.dell.com/wd/hub"), option.ToCapabilities(), TimeSpan.FromSeconds(500));
                    Console.WriteLine(webDriver);
                    SessionId t1 = ((RemoteWebDriver)webDriver).SessionId;
                    webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    Console.WriteLine("Session log can be found here: https://moon-logs.ausmpc.pcf.dell.com/" + ((RemoteWebDriver)webDriver).SessionId + "/session.log");
                    Console.WriteLine("Session video can be seen here: https://moon-logs.ausmpc.pcf.dell.com/" + ((RemoteWebDriver)webDriver).SessionId + "/video.mp4");
                    break;
                default:
                    break;
            }
            driverStack.Add(testName, webDriver);
            return webDriver;
        }
    }
}
