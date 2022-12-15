using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Utilities
{
    public static class WebElementUtil
    {
        public static void WaitForElement(this IWebElement element, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);
                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => element.FindElement(by));
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("Element Timeout Exception. " + e.Message);
                if (!suppressException)
                {
                    throw e;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw ex;
            }
        }

        public static void WaitForElementVisible(this IWebElement element, By by, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);
                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => element.FindElement(by).Displayed && element.FindElement(by).Enabled);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw;
            }
        }

        public static void WaitForElementNotVisible(this IWebElement element, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);
                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => !element.Displayed || !element.Enabled);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw;
            }
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebElement element, By by, TimeSpan timeout)
        {
            var wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = timeout;
            return wait.Until(ctx => element.FindElements(@by).Count > 0 ? element.FindElements(@by) : null);
        }

        public static bool ElementExists(this IWebElement element, By by)
        {
            try
            {
                element.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        public static IWebElement FindElement(this IWebElement element, By by, TimeSpan timeout)
        {
            var wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = timeout;
            return wait.Until(ctx => element.FindElement(@by).Displayed ? element.FindElement(@by) : null);
        }

        public static void WaitForElementDisplayed(this IWebElement element, TimeSpan timeout,
            bool suppressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);
                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => element.Displayed);
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("Element Timeout Exception. " + e.Message);
                if (!suppressException)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw;
            }

        }

        public static bool IsElementVisible(this IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public static void WaitForElementVisible(this IWebElement element, TimeSpan timeout, bool suppressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);
                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => element.Displayed && element.Enabled);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element Not Found. " + e.Message);
                if (!suppressException)
                    throw;
            }
            catch (WebDriverTimeoutException toe)
            {
                Console.WriteLine("Element Timeout Exception. " + toe.Message);
                if (!suppressException)
                    throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!suppressException)
                    throw;
            }
        }

        public static void WaitForElementDisplayed(this IWebElement element, By by, TimeSpan timeout, bool supressException = true)
        {
            try
            {
                var wait = new DefaultWait<IWebElement>(element);

                wait.Timeout = timeout;
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until(ctx => element.FindElement(by).Displayed);
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("Element Timeout Exception. " + e.Message);
                if (!supressException)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown. Please see Exception details " + ex.Message);
                if (!supressException)
                    throw;
            }
        }

        public static void Set(this IWebElement element, string valuetoSet)
        {
            element.Click();
            element.Clear();
            element.SendKeys(valuetoSet);
        }

        public static void Set(this IWebElement element, bool valuetoSet)
        {
            if (valuetoSet)
            {
                if (!element.Selected) element.Click();
            }
            else
            {
                if (element.Selected) element.Click();
            }
        }

        public static SelectElement Select(this IWebElement element)
        {
            SelectElement ele = new SelectElement(element);

            return ele;
        }

        public static void SelectByPartialText(this SelectElement selectElement, string partialText)
        {
            foreach (var element in selectElement.Options)
            {
                if (element.Text.Contains(partialText) && !element.Selected)
                {
                    element.Click();
                    break;
                }
            }
        }

        public static IList<IWebElement> Children(this IWebElement element)
        {
            return element.FindElements(By.XPath("./*"));
        }

        public static IWebElement Parent(this IWebElement childElement)
        {
            return childElement.ParentWithTag(null);
        }

        public static IWebElement ParentWithTag(this IWebElement childElement, string tagName)
        {
            var getParent = new Func<IWebElement, IWebElement>(child => child.FindElement(By.XPath("..")));
            var parent = getParent(childElement);
            if (!string.IsNullOrEmpty(tagName))
                while (parent.TagName.ToLower() != tagName)
                {
                    parent = getParent(parent);
                    if (parent == null)
                        throw new Exception("Parent with <" + tagName + "> tag wan't found");
                }
            return parent;
        }

        public static void JsClick(this IWebElement element)
        {
            (element.GetDriver() as IJavaScriptExecutor).ExecuteScript("arguments[0].click()", element);
        }

        public static IWebDriver GetDriver(this IWebElement element)
        {
            IWrapsDriver wrappedElement = element as IWrapsDriver;
            if (wrappedElement == null)
            {
                FieldInfo fieldInfo = element.GetType().GetField("underlyingElement", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    wrappedElement = fieldInfo.GetValue(element) as IWrapsDriver;
                    if (wrappedElement == null)
                        throw new ArgumentException("Element must wrap a web driver", "element");
                }
            }

            return wrappedElement.WrappedDriver;
        }

        public static IWebElement PreviousSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./preceding-sibling::*[1]"));
        }

        public static IWebElement NextSibling(this IWebElement element)
        {
            return element.FindElement(By.XPath("./following-sibling::*[1]"));
        }

        public static void MoveAndClick(this IWebElement element)
        {
            new OpenQA.Selenium.Interactions.Actions(element.GetDriver()).MoveToElement(element).Click().Build().Perform();
        }

        public static bool WaitForElementsCount(this IWebElement parent, IWebDriver webDriver, By child, int timeout = 30)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));


            return wait.Until<bool>(
                delegate
                {
                    try
                    {
                        return parent.FindElements(child).Count > 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
        }
        public static string ReplaceNonNumericCharacter(string text)
        {
            text.Replace("$", "").Replace(",", "").Replace("US", "");
            return text;
            
        }
    }
}
