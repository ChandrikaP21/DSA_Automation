using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SmartPrice_E2E_WebAutomation.Objects.Pages
{
  public class WaitHelperClass
    {
        public IWebDriver WebDriver;
        public WaitHelperClass(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        #region By
        public By BywaitUntilspinnerLoad => By.XPath("//*[@id='busy - indicator']");
        #endregion
        #region Element
        public IWebElement WaitUntilspinnerLoadElement => WebDriver.FindElement(BywaitUntilspinnerLoad);
        #endregion
    }
}
