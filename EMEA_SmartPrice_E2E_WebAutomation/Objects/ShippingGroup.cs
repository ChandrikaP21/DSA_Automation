using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
   public class ShippingGroup
    {
        public IWebDriver driver;
        public ShippingGroup(IWebDriver webDriver)
        {
            driver = webDriver;
        }
        #region By
        public By ByShippingGroup => By.XPath("//*[@id='quoteCreate_group_0']");
        #endregion
        #region WebElement
        public IWebElement ShippingGroupElement => driver.FindElement(ByShippingGroup);
        #endregion
    }
}
