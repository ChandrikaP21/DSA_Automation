using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
  public  class SmartPricePopUpClass
    {
        public IWebDriver WebDriver;
        public SmartPricePopUpClass(IWebDriver driver)
        {
            WebDriver = driver;
        }
        #region By
        public  By ByshowDiscountvalue => By.XPath("//*[contains(text(),'Show Discount')]");
        public By BystandardPartnerDiscountText => By.XPath("//*[contains(text(),'Standard Partner Discount: ')]/following::span[1]");
     //   public By BystandardPartnerDiscountValue => By.XPath("//*[contains(text(),'Standard Partner Discount: ')]/following::span[1]");
        public By BystandardDiscountTextWithShowDiscount => By.XPath("//smart-price-guidance/div/div/div[2]/div[2]");
        #endregion
        #region IWebElement
        public IWebElement standardPartnerShowDiscount => WebDriver.FindElement(BystandardPartnerDiscountText);
        public IWebElement showDiscountvalue => WebDriver.FindElement(ByshowDiscountvalue);
        public IWebElement standardDiscountTextWithShowDiscount => WebDriver.FindElement(BystandardDiscountTextWithShowDiscount);
        
        #endregion
    }
}
