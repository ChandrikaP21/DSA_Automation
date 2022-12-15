using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace SmartPrice_E2E_WebAutomation.Utilities
{
    public static class WebDriverUtils
    {
        private static double pageLoadWaitTime = 60;
        private static readonly TimeSpan defaultWait = TimeSpan.FromSeconds(10);

        public static void WaitForElement(this IWebDriver driver, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(drv => drv.FindElement(by));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw e;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw toe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }

        public static void WaitForWebElement(this IWebDriver driver, By by, TimeSpan timeout)
        {
            //below lines can be used with addtional nuget SeleniumExtras
            //var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("content-section")));


            /*var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            var element = wait.Until(() =>
            {
                var e = Driver.FindElement(By.Id("content-section"));
                if (e.Displayed)
                    return e;
            });*/

            new WebDriverWait(driver, timeout).Until(c => c.FindElement(by));
        }

        public static void WaitForPageLoad(this IWebDriver driver, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(driver, timeout);
                wait.Until(driver1 => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete")
                    && (bool)(driver as IJavaScriptExecutor).ExecuteScript("return (window.jQuery != null) && (jQuery.active == 0);"));
            }
            catch (Exception exception)
            {
                if (exception is WebDriverTimeoutException)
                    Console.WriteLine(@"The specified wait time has expired.");

                Console.WriteLine(@"{0}:{1}", "The Angular/Ajax components failed to load properly. See Exception details.", exception.Message);
                if (!suppressException)
                    // ReSharper disable once PossibleIntendedRethrow
                    throw;
            }
        }
        /// <summary>
        ///     Element to locate the title "Solution Configurator" on the page
        /// </summary>
        private static By waitAnimation => By.XPath(".//*[@class='pleaseWaitImage']");

        /// <summary>
        ///     Elemenet to location wait animation - that blocks page for a while
        /// </summary>
        private static By waitAnimation2 => By.XPath(".//*[@class='pleaseWaitOverlay']");

        /// <summary>
        ///     Loading after search product in CP/Gii EMEA/APJ
        /// </summary>
        private static By LoadingContainer => By.CssSelector(".uif_gridLoadingContainer");

        /// <summary>
        ///     please waiting animation in groups tab operation
        /// </summary>
        private static By GroupWaitingOverlay => By.Id("composerTabGroupsOverlay");

        /// <summary>
        ///     Locate the Spinner web element
        /// </summary>
        private static By Spinner => By.XPath(".//div[contains(@class,'spinner')]");

        /// <summary>
        ///     Locate the catalog expanding spinner
        /// </summary>
        private static By CategoryExpandingWaitingSpinnerBy => By.XPath(".//*[@class='smaller spinner']");

        /// <summary>
        ///     Waits for the loading wait animation to load and returns true when it completes loading
        ///     Merge all the spinner, loading wait animation, for the animation isn't displayed, make sure it won't wait
        /// </summary>
        /// <returns>true when the animation is no longer visible</returns>
        public static bool waitForWaitAnimationToLoad(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //new PageHelper(driver).waitForWaitAnimationToLoad();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(pageLoadWaitTime));
            wait.PollingInterval = TimeSpan.FromSeconds(2);
            //WaitingForSpinner(driver, By.XPath("//*[contains(@class,'busy-indicator')]"));
            //WaitingForSpinner(driver, By.XPath("//p[normalize-space()='Processing... Please Wait.']"));
            WaitingForSpinner(driver, By.XPath("//*[@id='busy-overlay' or @id='busy-indicator' or contains(@class,'busy-indicator')]"));
            //WaitingForSpinner(driver, By.XPath("//*[@id='busy-indicator']"));
            //WaitingForSpinner(driver, By.XPath("//p[normalize-space()='Processing... Please Wait.']"));
            //WaitingForSpinner(driver, By.Id("busy - overlay"));


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return true;
        }
        /// <summary>
        ///     Waiting for sppiner disappear
        /// </summary>
        public static void WaitingForSpinner(this IWebDriver driver, By spinnerBy)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.PollingInterval = TimeSpan.FromSeconds(1);
            wait.Until(
                delegate
                {
                    try
                    {
                        var spinners = driver.FindElements(spinnerBy);
                        return !spinners.Any(spinner => spinner.Displayed);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
        }
        ///html/body/app-root

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By findBy, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(drv => (drv.FindElements(@findBy).Count > 0) ? drv.FindElements(@findBy) : null);
        }

        public static bool ElementDisplayed(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed; 
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(drv => drv.FindElement(@by).Displayed ? drv.FindElement(@by) : null);
        }

        public static void WaitForElementVisible(this IWebDriver driver, By by, int seconds, bool suppressException = true)
        {
            TimeSpan timeout = TimeSpan.FromSeconds(seconds);
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(drv => drv.FindElement(by).Displayed && drv.FindElement(by).Enabled);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw e;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw toe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }

        public static void WaitForElementDisplayed(this IWebDriver driver, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(drv => drv.FindElement(by).Displayed);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw e;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw toe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }
        public static void WaitForElementEnabled(this IWebDriver driver, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(drv => drv.FindElement(by).Enabled);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw e;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw toe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }

        internal static void WaitForPageToLoad(IWebDriver driver, double pageLoadWaitTime, int checkInterval = 60)
        {
            // This method is temp until we do have solution for Webdriver issue.

            int retryFreq = (int)Math.Ceiling(((double)pageLoadWaitTime) / checkInterval);
            int retryCount = 1;
        label:
            try
            {
                do
                {
                    retryCount++;
                    driver.WaitForPageLoad(TimeSpan.FromSeconds(checkInterval), false);
                } while (retryCount > retryFreq);

            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("timed out after 60 seconds"))
                {

                    goto label;
                }
                throw;
            }
        }

        public static bool IsScrolledTo(this IWebDriver driver, int expectedScrollY)
        {
            //var actualScrollY = Convert.ToInt32(new SS.OSC.Pages.YourSolutionsPage(driver).ExecuteScript("return window.scrollY;"));
            var actualScrollY = Convert.ToInt32(12);
            return Math.Abs(actualScrollY - expectedScrollY) < 15;//Check that the element is not farther from the top than 15px.
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this ISearchContext context, By findBy, TimeSpan timeout, bool throwException)
        {
            try
            {
                if (context.IsElement())
                    return ((IWebElement)context).FindElements(findBy, timeout);
                else
                    return ((IWebDriver)context).FindElements(findBy, timeout);
            }
            catch
            {
                if (throwException)
                    throw;
            }
            return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        }

        private static bool IsElement(this ISearchContext context)
        {
            return context.GetType().ToString().Contains("WebElement");
        }

        public static void LaunchUrl(IWebDriver browser, string url)
        {
            browser.Navigate().GoToUrl(url);
        }
       

        public static bool WaitForElementNotExists(this IWebDriver driver, IWebElement parent, By spinnerBy, int timeout = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            return wait.Until<bool>(
                delegate
                {
                    try
                    {
                        var spinners = parent.FindElements(spinnerBy);
                        return !spinners.Any(spinner => spinner.Displayed);
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                });
        }

        public static bool WaitForElementNotExists(this IWebDriver driver, By spinnerBy, int timeout = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            return wait.Until<bool>(
                delegate
                {
                    try
                    {
                        var spinners = driver.FindElements(spinnerBy);
                        return !spinners.Any(spinner => spinner.Displayed);
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                });
        }

        public static void SkipAlertIfAny(this IWebDriver driver)
        {
            if (IsAlertPresent(driver))
            {
                IAlert alert = driver.SwitchTo().Alert();
                if (alert != null)
                {
                    alert.Accept();
                }
            }
            driver.WaitForPageLoad(TimeSpan.FromSeconds(120));
        }

        private static bool IsAlertPresent(IWebDriver driver)
        {

            try
            {
                //Please dont add any code to wait , As this method is only resonsible to check if Alert is present or not.
                driver.SwitchTo().Alert();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool IsBrowserMaximized(this IWebDriver driver)
        {
            var script = "return window.screenTop == 0 ? true : false;";
            var val = ((IJavaScriptExecutor)driver).ExecuteScript(script);
            Console.WriteLine($"Browser is maximized : {Convert.ToBoolean(val)}");
            return Convert.ToBoolean(val);
        }

        public static void WaitForElement(this IWebDriver driver, By by, int timeToWaitInSeconds = 20, bool checkDisplayed = true)
        {
            try
            {
                bool isFound = false;
                IWebElement element = FindElement(driver, by);
                while (timeToWaitInSeconds != 0)
                {
                    if (element == null)
                    {
                        System.Threading.Thread.Sleep(1000);
                        timeToWaitInSeconds--;
                        element = FindElement(driver, by);
                    }
                    else
                    {
                        if (checkDisplayed)
                        {
                            if (!element.Displayed)
                            {
                                System.Threading.Thread.Sleep(1000);
                                timeToWaitInSeconds--;
                                element = FindElement(driver, by);
                            }
                            else
                            {
                                isFound = true;
                                break;
                            }
                        }
                        else
                        {
                            isFound = true;
                            break;
                        }
                    }
                }

                if (!isFound)
                {
                    throw new NoSuchElementException("Even after waiting for 50 sec the element not found");
                }
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine($"Caught stale exception when searching for path {by.ToString()}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static IWebElement FindElement(IWebDriver driver, By by)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(by);
            }
            catch { }
            return element;
        }

        public static void WebDriverElementToBeClickable(this IWebDriver driver, IWebElement webElement = null)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5000));

            Thread.Sleep(2000);
            var counter = 0;
            try
            {
                while (driver.FindElement(By.XPath("//app-manage-collaborators/ngx-loading")).GetAttribute("ng-reflect-show") == "true")
                {
                    if (counter < 100)
                    {
                        Thread.Sleep(2000);
                    }
                    counter++;
                }
            }
            catch (Exception)
            {


            }
        }
        public static IWebElement WaitUntilFindElement(this IWebDriver driver, By locator)
        {
            WebDriverWait Wait = new WebDriverWait(driver, defaultWait);
            Wait.Until(drv => drv.FindElement(locator));
            return driver.FindElement(locator);
        }
        public static void ScrollIntoView(this IWebDriver driver, By selector)
        {
            driver.ScrollIntoView(driver.WaitUntilFindElement(selector));
        }
        public static void ScrollIntoView(this IWebDriver driver, IWebElement element)
        {
            // Assumes IWebDriver can be cast as IJavaScriptExecutor.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            driver.ScrollIntoViewByValue();
        }
        public static void ScrollIntoViewByValue(this IWebDriver driver, int xAxis = 0, int yAxis = -300)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"window.scrollBy({xAxis},{yAxis})", "");
        }
        public static void PerformDoubleClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.DoubleClick(element).Build().Perform();
        }
        public static void PerformRightClick(this IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.ContextClick(element).Build().Perform();
        }


    }
}
