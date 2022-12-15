using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D01;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote
{
    public class Product
    {
        public IWebDriver WebDriver;
        public Product(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
        public Product()
        {

        }
        #region Financials 
        /*LP,SP,Cost,Margin,SP financials, SP Guidance should be captured here*/
        #endregion

        public string OrderCode = string.Empty;
        public string SKUNumber = string.Empty;
        public string ItemDescription = string.Empty;
        public List<ServicesModule> ServiceModules = new List<ServicesModule>();
        public List<AccessoriesModule> AccessoryModules = new List<AccessoriesModule>();
        public List<ComponentModule> ComponentModules = new List<ComponentModule>();

        #region By
        public By ByOrderCodeInputField => By.XPath("//input[@id='productSearch_orderCode']");
        public By ByProductviewQuote => By.XPath("//button[@id='products_viewquote']");
        public By ByProductSearchInputField => By.XPath("//input[@id='txtPDSearch']");
        public By ByProductSearch( string SearchText) => (SearchText== "Search"|| SearchText == "") ?By.XPath("//button[contains(text(),'Search')]"): By.XPath("//button[contains(text(),'"+SearchText+"')]");

   //     public By ByAddProductbutton => By.XPath("//table/tbody/tr/td/following::th//span[@id='addtool-0']");
        public By ByAddProductbutton => By.XPath("//table/tbody/tr/td/following::th//span[starts-with(@id,'addtool-0')]");
        #endregion
        #region WebElemenet


        private IWebElement ProductSearchInputField => WebDriver.FindElement(ByProductSearchInputField);
        public IWebElement ProductSearch(string text) => WebDriver.FindElement(ByProductSearch(text));
        public List<IWebElement> AddProductNonUScountry => WebDriver.FindElements(ByAddProductbutton).ToList();
        public IWebElement OrderCodeInputField => WebDriver.FindElement(ByOrderCodeInputField);
        public IWebElement ProductviewQuote => WebDriver.FindElement(ByProductviewQuote);
        #endregion

        #region Add Remove & Update Services

        public void AddService(string moduleId, string moduleTitle, string servieTitle, decimal serviceListPrice)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
            {
                serviceModule = new ServicesModule();
                serviceModule.ID = moduleId;
                serviceModule.Title = moduleTitle;
                ServiceModules.Add(serviceModule);
            }

            serviceModule.Services.Add(
                    new Service()
                    {
                        Title = servieTitle,
                        ListPrice = serviceListPrice
                    }

                );
        }

        public void AddService(string moduleId, string moduleTitle, string servieTitle, decimal serviceListPrice, decimal serviceSellingPrice)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
            {
                serviceModule = new ServicesModule();
                serviceModule.ID = moduleId;
                serviceModule.Title = moduleTitle;
                ServiceModules.Add(serviceModule);
            }

            serviceModule.Services.Add(
                    new Service()
                    {
                        Title = servieTitle,
                        ListPrice = serviceListPrice,
                        SellingPrice = serviceSellingPrice
                    }

                );
        }

        public void AddService(string moduleId, string moduleTitle, string servieTitle, decimal serviceListPrice, decimal serviceSellingPrice, bool isUpSell, string serviceTypeId)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
            {
                serviceModule = new ServicesModule();
                serviceModule.ID = moduleId;
                serviceModule.Title = moduleTitle;
                ServiceModules.Add(serviceModule);
            }

            serviceModule.Services.Add(
                    new Service()
                    {
                        Title = servieTitle,
                        ListPrice = serviceListPrice,
                        SellingPrice = serviceSellingPrice,
                        IsUpsell = isUpSell,
                        ServiceTypeId = serviceTypeId
                    }

                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="moduleTitle"></param>
        /// <param name="servieTitle"></param>
        /// <param name="serviceListPrice"></param>
        /// <param name="serviceSellingPrice"></param>
        /// <param name="isUpSell"></param>
        /// <param name="serviceTypeId"></param>
        public void UpgradeService(string moduleId, string moduleTitle, string servieTitle, decimal serviceListPrice, decimal serviceSellingPrice, bool isUpSell, string serviceTypeId)
        {
            try
            {
                ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
                if (serviceModule == null)
                {
                    serviceModule = new ServicesModule();
                    serviceModule.ID = moduleId;
                    serviceModule.Title = moduleTitle;
                    ServiceModules.Add(serviceModule);
                }
                if (serviceModule.Services.Count > 1 && serviceModule.Services.Count == 0)
                {
                    serviceModule.Services.Add(
                            new Service()
                            {
                                Title = servieTitle,
                                ListPrice = serviceListPrice,
                                SellingPrice = serviceSellingPrice,
                                IsUpsell = isUpSell,
                                ServiceTypeId = serviceTypeId
                            }

                        );
                }
                else
                {
                    serviceModule.Services.RemoveAt(0);
                    serviceModule.Services.Add(
                           new Service()
                           {
                               Title = servieTitle,
                               ListPrice = serviceListPrice,
                               SellingPrice = serviceSellingPrice,
                               IsUpsell = isUpSell,
                               ServiceTypeId = serviceTypeId
                           }

                       );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// DownGrade The services
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="moduleTitle"></param>
        /// <param name="servieTitle"></param>
        /// <param name="serviceListPrice"></param>
        /// <param name="serviceSellingPrice"></param>
        /// <param name="isUpSell"></param>
        /// <param name="serviceTypeId"></param>
        public void DowngradeService(string moduleId, string moduleTitle, string servieTitle, decimal serviceListPrice, decimal serviceSellingPrice, bool isUpSell, string serviceTypeId)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
            {
                serviceModule = new ServicesModule();
                serviceModule.ID = moduleId;
                serviceModule.Title = moduleTitle;
                ServiceModules.Add(serviceModule);
            }
            if (serviceModule.Services.Count > 1)
            {
                serviceModule.Services.Add(
                        new Service()
                        {
                            Title = servieTitle,
                            ListPrice = serviceListPrice,
                            SellingPrice = serviceSellingPrice,
                            IsUpsell = isUpSell,
                            ServiceTypeId = serviceTypeId
                        }

                    );
            }
            else
            {
                serviceModule.Services.RemoveAt(0);
                serviceModule.Services.Add(
                       new Service()
                       {
                           Title = servieTitle,
                           ListPrice = serviceListPrice,
                           SellingPrice = serviceSellingPrice,
                           IsUpsell = isUpSell,
                           ServiceTypeId = serviceTypeId
                       }

                   );

            }
        }


        public void RemoveService(string moduleId, string serviceTitle)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
                throw new Exception("Couldnt find module");
            else
            {
                Service service = serviceModule.Services.Find(y => y.Title == serviceTitle);
                if (service == null)
                    throw new Exception("Couldnt find service");
                else
                    serviceModule.Services.Remove(service);
            }
        }

        public void UpdateServiceSellingPriceAndServiceType(string moduleId, string serviceTitle, decimal sellingPrice, string serviceTypeId)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
                throw new Exception("Couldnt find module");
            else
            {
                Service service = serviceModule.Services.Find(y => y.Title == serviceTitle);
                if (service == null)
                    throw new Exception("Couldnt find service");
                else
                {
                    service.SellingPrice = sellingPrice;
                    service.ServiceTypeId = serviceTypeId;
                }
            }
        }


        public void UpdateServiceSellingPrice(string moduleId, string serviceTitle, decimal sellingPrice)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
                throw new Exception("Couldnt find module");
            else
            {
                Service service = serviceModule.Services.Find(y => y.Title == serviceTitle);
                if (service == null)
                    throw new Exception("Couldnt find service");
                else
                    service.SellingPrice = sellingPrice;
            }
        }

        public void UpdateServiceType(string moduleId, string serviceTitle, string serviceTypeId)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
                throw new Exception("Couldnt find module");
            else
            {
                Service service = serviceModule.Services.Find(y => y.Title == serviceTitle);
                if (service == null)
                    throw new Exception("Couldnt find service");
                else
                    service.ServiceTypeId = serviceTypeId;
            }
        }

        public void UpdateServiceType(string moduleId, string serviceTitle, bool isUpSell)
        {
            ServicesModule serviceModule = ServiceModules.Find(x => x.ID == moduleId);
            if (serviceModule == null)
                throw new Exception("Couldnt find module");
            else
            {
                Service service = serviceModule.Services.Find(y => y.Title == serviceTitle);
                if (service == null)
                    throw new Exception("Couldnt find service");
                else
                    service.IsUpsell = isUpSell;
            }
        }

        #endregion

        #region Add Accessories
        /*to be developed*/
        #endregion

        #region Add Components
        public void AddComponent(string moduleId, string moduleTitle, string componentTitle, decimal componentListPrice)
        {
            ComponentModule componentModule = ComponentModules.Find(x => x.ID == moduleId);
            if (componentModule == null)
            {
                componentModule = new ComponentModule();
                componentModule.ID = moduleId;
                componentModule.Title = moduleTitle;
                ComponentModules.Add(componentModule);
            }

            componentModule.Components.Add(
                    new Component()
                    {
                        Title = componentTitle,
                        ListPrice = componentListPrice
                    }

                );
        }

        public void AddComponent(string moduleId, string moduleTitle, string componentTitle, decimal componentListPrice, decimal componentSellingPrice)
        {
            ComponentModule componentModule = ComponentModules.Find(x => x.ID == moduleId);
            if (componentModule == null)
            {
                componentModule = new ComponentModule();
                componentModule.ID = moduleId;
                componentModule.Title = moduleTitle;
                ComponentModules.Add(componentModule);
            }

            componentModule.Components.Add(
                    new Component()
                    {
                        Title = componentTitle,
                        ListPrice = componentListPrice,
                        SellingPrice = componentSellingPrice
                    }

                );
        }

        public void AddComponent(string moduleId, string moduleTitle, string componentTitle, decimal componentListPrice, decimal componentSellingPrice, bool isUpSell, string
            serviceTypeId)
        {
            ComponentModule componentModule = ComponentModules.Find(x => x.ID == moduleId);
            if (componentModule == null)
            {
                componentModule = new ComponentModule();
                componentModule.ID = moduleId;
                componentModule.Title = moduleTitle;
                ComponentModules.Add(componentModule);
            }


            componentModule.Components.Add(
                     new Component()
                     {
                         Title = componentTitle,
                         ListPrice = componentListPrice,
                         SellingPrice = componentSellingPrice
                     }

                 );
        }
        public void AddAccessories(string moduleId, string moduleTitle, string accessoryTitle, decimal accessoryListPrice, decimal accessorySellingPrice, bool isUpSell, string
            serviceTypeId)
        {
            AccessoriesModule accessoryModule = AccessoryModules.Find(x => x.ID == moduleId);
            if (accessoryModule == null)
            {
                accessoryModule = new AccessoriesModule();
                accessoryModule.ID = moduleId;
                accessoryModule.Title = moduleTitle;
                AccessoryModules.Add(accessoryModule);
            }


            accessoryModule.Accessories.Add(
                     new Accessory()
                     {
                         Title = accessoryTitle,
                         ListPrice = accessoryListPrice,
                         SellingPrice = accessorySellingPrice
                     }

                 );
        }
        #endregion

        public Product SearchProductInputFieldForNonUsCountry(string productName)
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(3));
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementIsVisible(ByProductSearchInputField));
                ProductSearchInputField.SendKeys(productName);
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Error: Unable search in product Input box , refer SearchProductInputFieldForNonUsCountry()", ex);
            }
            
            return new Product(WebDriver);
        }
      
        public Product SearchProductForNonUsCountry(string text="")
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(3));
                string searchText = "";
                try
                {
                    searchText = WebDriver.FindElement(By.XPath("//input[@id='txtPDSearch']//following::button[contains(@class,'PD_search-search-products')]")).GetAttribute("innerText");
                    if (searchText == text)
                    {
                        ProductSearch(searchText).Click();
                    }
                    else
                    {
                        ProductSearch(searchText).Click();
                    }
                }
                catch (Exception ex)
                {
                    ProductSearch(searchText).Click();
                }

                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
            }
            catch(Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.SimpleLogger.LogMessage(" Error: Unable to search,Please refer SearchProductForNonUsCountry() " + ex);
            }
            return new Product(WebDriver);
        }
        public Product AddProductForNonUScountry()
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(10));
                foreach(var add in AddProductNonUScountry)
                {
                    //it will  select first product which has listprice.
                    if(add.FindElement(By.XPath("./preceding::td[3]")).Text!="-" && !string.IsNullOrEmpty(add.FindElement(By.XPath("./preceding::td[3]")).Text)&&add.Text!=""&& add.IsElementVisible())
                    {
                        Thread.Sleep(3000);
                        add.Click();
                        break;
                    }
                }
                //AddProductNonUScountry.Click();// commented due to unabl to find the add product from the selected list.
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(ByProductviewQuote));
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is added", true);
                //WebDriverUtils.TakeSnapShot(WebDriver);
            }
            
           catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to add Product", true);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to add Product"+ex.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to add Product" + ex.StackTrace);
            }
            return new Product(WebDriver);
        }
        public Product AddProductForUScountry()
        {
            try
            {
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(3));
                foreach (var add in AddProductNonUScountry)
                {
                    if (add.FindElement(By.XPath("./preceding::td[3]")).Text != "-" && !string.IsNullOrEmpty(add.FindElement(By.XPath("./preceding::td[3]")).Text) && add.Text != "")
                    {
                        add.Click();
                        break;
                    }
                }
                //AddProductNonUScountry.Click();// commented due to unabl to find the add product from the selected list.
                WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(2));
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is added", true);
                
            }

            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Unable to add Product", true);
                WebDriverUtils.TakeSnapShot(WebDriver);
            }
            return new Product(WebDriver);
        }
        public Product ClickOnViewQuoteButton()
        {
            try
            {
                Constant constant = new Constant(WebDriver);
                DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
                //WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(5));
                if(constant.IsElementPresent(constant.ByProductViewQuote))
                {
                    ProductviewQuote.Click();
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementIsVisible(drobj.BydraftquoteNumber));
                    WebDriver.WaitForPageLoad(TimeSpan.FromSeconds(35));
                }
                    
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Product(WebDriver);
        }

        private void AddProductBasedOnSystemSearch(string productText,string country= "UNITED STATES")
        {
            
            try
            {
                     WaitHelperClass waitHelperClass = new WaitHelperClass(WebDriver);
                
                
               
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySystemSearch));
                    SystemSearch.SendKeys(productText);
                    ScrollInto(ProductSearchButton);

                 new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementExists(ByListOfProduct));
                    for (int i = 1; i <= ListOfProduct.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(ProductListPriceInSearchpanel(i).Text))
                        {
                        Thread.Sleep(2000);
                        //AddProductForUScountry();
                        AddProductInProductSearchPage(i).Click();

                            Thread.Sleep(4000);
                            string[] text = ProductDescription(i).Text.Split('|');
                            ItemDescription = text[0];
                          ClickOnViewQuoteButton();
                            Thread.Sleep(3000);
                            try
                            {
                                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                                wait.Until(ExpectedConditions.ElementIsVisible(ByProductlabel(ItemDescription)));
                            }
                            catch (Exception ex)
                            {
                                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("", true);
                            }
                            break;

                        }
                    }
                
                
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(ItemDescription + "is added", true);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                 WebDriverUtils.TakeSnapShot(WebDriver);
            throw new ShowStopperException("Unable to add Product",ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }


        }
        public void ScrollInto(IWebElement ProductSearchButton, int y = 10)
        {
            Constant cs = new Constant(WebDriver);
            if (cs.IsElementPresent(ByProductSearchButton))
            {
                try
                {
                    Constant constant = new Constant(WebDriver);
                    constant.jse.ExecuteScript("window.scrollBy(0, " + y + ")", ProductSearchButton);
                    ProductSearchButton.Click();
                }
                catch (Exception ex)
                {
                    ScrollInto(ProductSearchButton);
                }
            }
            else
            {
                Console.WriteLine("Element is not visible");
            }

        }
        private void AddProductBasedOnSAndPSearch(string productDescription)
        {
            try
            {

                Constant element = new Constant(WebDriver);
                
                    element.AddItem.Click();
                    WaitHelperClass waitHelperClass = new WaitHelperClass(WebDriver);

                //     WebDriverUtils.WaitingForSpinner(WebDriver, waitHelperClass.BywaitUntilspinnerLoad);
                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementIsVisible(BySAndPSearch));
                SAndPSearch.SendKeys(productDescription);

                ScrollInto(ProductSearchButton);


                //Constant.jse.ExecuteScript("window.scrollBy(0, 100)", ProductSearchButton);

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementExists(ByListOfProduct));
                Thread.Sleep(4000);
                for (int i = 1; i <= ListOfProduct.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ProductListPriceInSearchpanel(i).Text))
                    {
                        AddProductInProductSearchPage(i).Click();

                        Thread.Sleep(4000);
                        string[] text = ProductDescription(i).Text.Split('|', '-');

                        ItemDescription = text[0];
                        ViewQuote.Click();
                        Thread.Sleep(12000);
                        WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond));
                        wait.Until(ExpectedConditions.ElementIsVisible(ByProductlabel(ItemDescription)));
                        break;

                    }
                }
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(ItemDescription + "is added", true);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                new ShowStopperException("Unable to add Product", ex);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }

        }
        public void AddProduct(string orderCode, string SystemSearchText, string SAndPSearchText,string country="",List<Translate> translates=null)
        {
            
            try
            {
              
                if (!string.IsNullOrEmpty(orderCode) && orderCode!=" ")
                {
                    string[] ordercodeString = orderCode.Split(',');
                    //add order code
                    for (int i = 0; i < ordercodeString.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ordercodeString[i]))
                        {
                            AddProductTextBoxObject.SendKeys(ordercodeString[i]);
                            try
                            {
                                WebDriverUtils.ScrollIntoView(WebDriver, ByAddProduct);
                                AddProductButtonbject.Click();
                                Thread.Sleep(20000);
                                
                                Constant constant = new Constant(WebDriver);
                                if(constant.IsElementPresent(By.XPath("//*[@class='invalid-input']")))
                                {
                                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("orderCode is not found",true);
                                    //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("orderCode is not found", true);
                                    AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText, country, translates);
                                }
                                if(constant.IsElementPresent(By.XPath("//*[@id='quoteCreate_groupAccordion_0_body']/div/div[1]/div[2]/div[2]")))
                                {
                                    IWebElement text = WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_groupAccordion_0_body']/div/div[1]/div[2]/div[2]"));
                                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(text.Text, true);
                                    AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText);
                                }
                                if (constant.IsElementPresent(By.XPath("//div[@id='_confirm']")))
                                {
                                    WebDriver.FindElement(By.XPath("//button[@name='close' and @type='button']")).Click();
                                    AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText,country,translates);
                                }
                                //else {
                                //    if (constant.IsElementPresent(constant.ByOrderCode(ordercodeString[i])))///// D34 could not able to find product bcz it is hidden
                                //     {
                                //        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is added",true);
                                //    }
                                //    else {
                                //        AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText);
                                //    }
                                //}

                            }
                            
                            catch (Exception ex)
                            {
                                
                                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(" SKU or Order Code doesn't exist in this company number. Please use an alternate or contact Presales for assistance. ", true);
                                AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText,country, translates);
                            }

                        }
                        else
                        {
                            AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText,country, translates);
                        }
                    }
                }

                else
                {
                    AddProductWithMultipleSerachInput(SystemSearchText, SAndPSearchText, country, translates);
                }





            }
            catch(Bedrock.ExceptionHandlingBlock.ShowStopperException exception)
            {
                throw exception;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product in not added. Error:" + ex.Message);
            }
            
             
        }
        public string TranslateToExpectedLanguage(List<Translate>translates=null,string countryName="")
        {
            string translatedText = "";
            bool isFound = false;
            if (translates != null)
            {
                foreach (var a in translates)
                {
                    if (a.To.ToUpper() == countryName)
                    {
                        foreach (var b in a.Words)
                        {
                            switch (b.txttotranslate) { 
                                case "Search":
                                    translatedText = b.translatedtext;
                                    isFound = true;
                                    break;
                                case "Käyttöjärjestelmä":
                                    translatedText = b.translatedtext;
                                    isFound = true;
                                    break;
                            }
                            if (isFound == true) break;

                        }
                    }
                    if (isFound == true) break;

                }

            }
            return translatedText;
        }
        public string TranslateToExpectedLanguage(List<Translate> translates = null,string translateLanguage= "", string countryName = "")
        {
            string translatedText = "";
            bool isFound = false;
            if (translates!=null)
            {
             foreach(var translatesobj in translates)
                {
                    foreach(var wordObj in translatesobj.Words)
                    {
                        if(wordObj.txttotranslate.Contains(translateLanguage))
                        {
                            translatedText = wordObj.translatedtext;
                            isFound = true;
                            break;
                        }
                    }
                    if (isFound) break;
                }
                
           
            }
            return translatedText;
        }
        private void AddProductWithMultipleSerachInput(string SystemSearch, string SAndPSearch,string countryName="",List<Translate> translates=null)
        {
            try
            {
                Thread.Sleep(4000);
                AddItem(1).Click();
                string translatedText = TranslateToExpectedLanguage(translates, countryName);
                if (countryName != "UNITED STATES")
                {
                    if (!string.IsNullOrEmpty(SystemSearch) && string.IsNullOrEmpty(SAndPSearch))
                    {
                        SearchProductInputFieldForNonUsCountry(SystemSearch).SearchProductForNonUsCountry(translatedText).AddProductForNonUScountry();
                    }
                    if (!string.IsNullOrEmpty(SystemSearch) && !string.IsNullOrEmpty(SAndPSearch))
                    {
                        SearchProductInputFieldForNonUsCountry(SystemSearch).SearchProductForNonUsCountry(translatedText).AddProductForNonUScountry();
                    }
                    if (string.IsNullOrEmpty(SystemSearch) && !string.IsNullOrEmpty(SAndPSearch))
                    {
                        SearchProductInputFieldForNonUsCountry(SAndPSearch).SearchProductForNonUsCountry(translatedText).AddProductForNonUScountry();
                    }
                    Thread.Sleep(4000);
                    ClickOnViewQuoteButton();
                }
                else
                {
                    if (!string.IsNullOrEmpty(SystemSearch) && string.IsNullOrEmpty(SAndPSearch))
                    {
                        AddProductBasedOnSystemSearch(SystemSearch);
                    }
                    if (!string.IsNullOrEmpty(SystemSearch) && !string.IsNullOrEmpty(SAndPSearch))
                    {
                        AddProductBasedOnSystemSearch(SystemSearch);
                    }
                    if (string.IsNullOrEmpty(SystemSearch) && !string.IsNullOrEmpty(SAndPSearch))
                    {
                        AddProductBasedOnSAndPSearch(SAndPSearch);
                    }
                }
            }
            catch(ShowStopperException ex)
            {
               
                throw ex;
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperException = new Bedrock.ExceptionHandlingBlock.ShowStopperException(ex.Message, ex);
                throw showStopperException;
            }





        }
        public IWebElement sellingprice(int count) => WebDriver.FindElement(By.XPath("//*[@id='quoteCreate_LI_pricingTotals_sellingPrice_0_" + count + "']"));
        /// <summary>
        /// Increase Quantity for single Product
        /// </summary>
        /// <param name="testCase"></param>
        /// <param name="constant"></param>
        public void IncreaseQuantity(TestCase testCase, Constant constant)
        {
            WebDriver = constant.WebDriver;
            int i = 0;
            DraftQuotePageObject drobj = new DraftQuotePageObject(WebDriver);
            int text = Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value"));
            constant.IncreaseQuantity(i).Clear();

            int quantityTobeIncreased = text + testCase.Quantity;
            constant.IncreaseQuantity(i).SendKeys(quantityTobeIncreased.ToString());
            
            try
            {

                constant.IncreaseQuantity(i).SendKeys(Keys.Enter);
                sellingprice(0).Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

            }

            Thread.Sleep(10000);

            new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(constant.ByDellIcon));
            Thread.Sleep(10000);
            // Console.WriteLine("Quantity is updated for "+ testCase.QuantityChangeType+"........");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Quantity is updated: {0} ", constant.IncreaseQuantity(i).GetAttribute("value")), true);
        }
        /// <summary>
        /// decrease Quantity
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="decreaseQuantity"></param>
        /// <param name="quantityLabel"></param>
        /// <param name="firstproduct"></param>
        public void DecreaseQuantityForSingleProduct(int quantity, QuantityChangeType firstproduct)
        {
            Constant constant = new Constant(WebDriver);
            try
            {
                int num = Convert.ToInt32(constant.IncreaseQuantity(0).GetAttribute("value"));
                int updatedquantity = num - quantity;
                constant.IncreaseQuantity(0).Clear();

                constant.IncreaseQuantity(0).SendKeys(quantity.ToString());
                try
                {
                    constant.IncreaseQuantity(0).SendKeys(Keys.Enter);
                    sellingprice(0).Click();
                    Thread.Sleep(2000);
                    Console.WriteLine(" Quantity is decreased");
                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Console.WriteLine(ex.Message);
            }
        }
        public void IncreaseQuantityforMultipleScenario(TestCase testCase, Constant constant)
        {
            try
            {
                int i = 0;
                if (testCase.QuantityChangeType == QuantityChangeType.AnyProduct)
                {
                    if (constant.ApplyChangesItem.Text == "Manual")
                    {
                        // Actions action = new Actions(WebDriver);
                        constant.jse.ExecuteScript("arguments[0].click()", constant.ClickOntheManualandStandard);
                        Thread.Sleep(1000);
                        constant.jse.ExecuteScript("arguments[0].click()", constant.SelectStandard);


                    }
                    WebDriverUtils.ScrollIntoView(WebDriver, constant.ByAddQuantity(i));
                    int text = Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value"));
                    constant.IncreaseQuantity(i).Clear();
                    int quantityTobeIncreased = text + testCase.Quantity;
                    constant.IncreaseQuantity(i).SendKeys(quantityTobeIncreased.ToString());
                    //Actions action = new Actions(WebDriver);
                    //action.Release();


                    try
                    {
                        sellingprice(i).Click();
                        WebDriverUtils.WaitForElement(WebDriver, constant.ByAddQuantity(i), 10, true);
                        //constant.QuantityLabel.Click();
                    }
                    catch (Exception ex)
                    {

                    }

                    Thread.Sleep(10000);

                }
                else if (testCase.QuantityChangeType == QuantityChangeType.ForSpecificProduct)
                {
                    try
                    {
                        if (constant.OrderCode(testCase).Enabled)
                        {
                            int text = Convert.ToInt32(constant.QuantitySpecificOrder(testCase).Text);
                            constant.QuantitySpecificOrder(testCase).Clear();
                            int quantityTobeIncreased = text + testCase.Quantity;
                            constant.QuantitySpecificOrder(testCase).SendKeys(quantityTobeIncreased.ToString());

                            try
                            {
                                sellingprice(0).Click();
                            }
                            catch (Exception ex)
                            {

                            }
                            Thread.Sleep(10000);

                        }
                    }
                    catch (Exception ex)
                    {
                        WebDriverUtils.TakeSnapShot(WebDriver);
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Order Code is not present :Error-" + ex.Message, true);
                        Console.WriteLine("Order Code is not present :Error-", ex.Message);
                    }



                }
                else if (testCase.QuantityChangeType == QuantityChangeType.AllProducts)
                {
                    if (constant.ApplyChangesItem.Text == "Standard")
                    {
                        // Actions action = new Actions(WebDriver);
                        constant.jse.ExecuteScript("arguments[0].click()", constant.SelectManual);
                        constant.ApplyChanges.Click();//Change manual to standard or standard to  manual.

                    }

                    ChangeQuantityForAllProduct(constant, testCase);


                }

                new WebDriverWait(WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond)).Until(ExpectedConditions.ElementToBeClickable(constant.ByDellIcon));
                Thread.Sleep(10000);
                // Console.WriteLine("Quantity is updated for "+ testCase.QuantityChangeType+"........");
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Quantity is updated for {0} ", testCase.QuantityChangeType), true);
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to change quantity,Please refer IncreaseQuantityforMultipleScenario() ", ex);
            }
        }

        private void ChangeQuantityForAllProduct(Constant constant, TestCase testCase)
        {
            try
            {


                while (constant.Count < constant.NoOfProducts.Count)
                {
                    int text = Convert.ToInt32(WebDriver.FindElement(By.XPath("//input[@id='quoteCreate_LI_quantity_0_" + constant.Count + "']")).GetAttribute("value"));

                    WebDriver.FindElement(By.XPath("//input[@id='quoteCreate_LI_quantity_0_" + constant.Count + "']")).Clear();
                    int quantityTobeIncreased = text + testCase.Quantity;
                    WebDriver.FindElement(By.XPath("//input[@id='quoteCreate_LI_quantity_0_" + constant.Count + "']")).SendKeys(quantityTobeIncreased.ToString());
                    // constant.QuantityLabel.Click();
                    new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(5000));

                    constant.Count++;
                }
                constant.jse.ExecuteScript("window.scrollBy(0,-750)");
                constant.ApplyChanges.Click();



                IClock iClock = new SystemClock();

                WebDriverWait wait = new WebDriverWait(iClock, WebDriver, TimeSpan.FromSeconds(StaticBriefCase.TimeOutSecond), TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                throw new ShowStopperException("Unable to change quantity for all product, Please check ChangeQuantityForAllProduct in Product Class", ex);
                //Console.WriteLine(ex.Message);
            }
        }
        public void DecreaseQuantityforMultipleScenario(TestCase testCase, Constant constant)
        {
            int i = 0;

            //for anyproducts....
            if (testCase.QuantityChangeType == QuantityChangeType.AnyProduct)
            {
                //if (Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value")) - testCase.Quantity > 0)
                //{
                try
                {
                    int num = Convert.ToInt32(constant.IncreaseQuantity(0).GetAttribute("value"));
                    int updatedquantity = testCase.Quantity - num;
                    constant.IncreaseQuantity(0).Clear();
                    constant.IncreaseQuantity(0).SendKeys(updatedquantity.ToString());

                    try
                    {
                        sellingprice(0).Click();
                        Console.WriteLine(" Quantity is changed:" + QuantityChangeType.AnyProduct);
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    WebDriverUtils.TakeSnapShot(WebDriver);
                    throw new ShowStopperException("Unable to Increase quantity",ex);
                }
                //}


            }
            //For specific product..1st Product...
            if (testCase.QuantityChangeType == QuantityChangeType.ForSpecificProduct)
            {
                try
                {

                    if (constant.OrderCode(testCase).Enabled)
                    {
                        //if (Convert.ToInt32(constant.QuantitySpecificOrder(testCase).GetAttribute("value")) - testCase.Quantity > 0)
                        //{
                        {
                            int num = Convert.ToInt32(constant.QuantitySpecificOrder(testCase).GetAttribute("value"));
                            int updatedquantity = num - testCase.Quantity;
                            constant.QuantitySpecificOrder(testCase).Clear();
                            constant.QuantitySpecificOrder(testCase).SendKeys(updatedquantity.ToString());

                            try
                            {
                                sellingprice(0).Click();

                            }
                            catch (Exception ex)
                            {

                            }
                            Thread.Sleep(10000);
                            Console.WriteLine(" Quantity is changed:" + QuantityChangeType.ForSpecificProduct);
                        }
                        // }
                    }

                }
                catch (Exception ex)
                {
                    WebDriverUtils.TakeSnapShot(WebDriver);
                    throw new ShowStopperException("Unable to Increase quantity,Please ", ex);
                }


            }


            ////For All products...
            if (testCase.QuantityChangeType == QuantityChangeType.AllProducts)
            {

                if (constant.ApplyChangesItem.Text == "Standard")
                {
                    // Actions action = new Actions(WebDriver);

                    //     constant.ApplyChangesItem.Click();//Change manual to standard or standard to  manual.
                    constant.jse.ExecuteScript("arguments[0].click()", constant.SelectManual);
                }

                DecreaseQuantityForAllProduct(constant, testCase);

                //constant.IncreaseQuantity.Clear();
                //constant.IncreaseQuantity.SendKeys(testCase.QuantityChangeType.ToString());
                //     new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(constant.ByDellIcon));
                Thread.Sleep(10000);
                Console.WriteLine("Quantity is decreased for " + testCase.QuantityChangeType + "........");


            }


        }

        private void DecreaseQuantityForAllProduct(Constant constant, TestCase testCase)
        {
            try
            {


                while (constant.Count < constant.NoOfProducts.Count)
                {
                    //if (Convert.ToInt32(constant.IncreaseQuantity(constant.Count).GetAttribute("value")) - testCase.Quantity > 0)
                    //{
                    int num = Convert.ToInt32(constant.IncreaseQuantity(constant.Count).GetAttribute("value"));
                    int updatedquantity = num - testCase.Quantity;
                    constant.IncreaseQuantity(constant.Count).Clear();

                    constant.IncreaseQuantity(constant.Count).SendKeys(updatedquantity.ToString());

                    Thread.Sleep(5000);

                    //}
                    //else
                    //{
                    //    Console.WriteLine("Quantity can not be decreased for " + constant.Count + " product ");
                    //}

                    constant.Count++;
                }
                Actions actions = new Actions(WebDriver);
                actions.MoveToElement(constant.ApplyChanges);
                actions.Click().Build().Perform();
                Thread.Sleep(5000);

            }
            catch (Exception ex)
            {
                WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unable to decrease quantity,Please check DecreaseQuantityForAllProduct() ", ex);
            }

        }


        #region By
        public By ByUnitSellingPrice(int index) => By.XPath("//*[@id='quoteCreate_LI_unitSellingPrice_0_" + index + "']");
        public By ByDiscountField(int index) => By.XPath("//*[@id='quoteCreate_LI_dolPercentage_0_" + index + "']");
        public By ByProductSkuNumber(int index) => By.XPath("//*[@id='lineitem_config_block_0_" + index + "_1_0_body']/div[2]/table/tbody/tr[2]/td[3]");
        public By ByProductUnitListprice(int index) => By.XPath("//*[@id='quoteCreate_LI_PI_unitPrice_0_" + index + "']");
        public By ByGetOrderCodeAttribute(string orderCode) => By.XPath("//*[contains(text(),'" + orderCode + "')]");
        public By ByAddItem(int count) => By.XPath("//button[@id='quoteCreate_additem_top_header_']");
        public By ByAddProductTextBox => By.XPath("//input[@id='quoteCreate_addText_top']");
        public By ByAddProduct => By.XPath("//button[@id='quoteCreate_addButton_top']");
        public By ByListOfProducts => By.XPath("//div[starts-with(@id,'quoteCreate_LI_productDescription')]");
        #region Product Search
        public By BySystemSearch => By.XPath("//input[@id='productSearch_chassisDescription']");
        public By BySAndPSearch => By.XPath("//input[@id='productSearch_chassisDescription']");
        public By ByProductSearchButton => By.XPath("//button[@id='productSearch_SearchButton']");
        public By ByListOfProduct => By.XPath("//table[@id='DataTables_Table_productResult_paneList']/tbody/tr/td[7]");
        public By ByViewQuote => By.XPath("//*[@id='products_viewquote']");
        public By ByProductListPriceInSearchpanel(int i) => By.XPath("(//table[@id='DataTables_Table_productResult_paneList']/tbody/tr/td[6])[" + i + "]");
        public By ByAddProductInProductSearchPage(int i) => By.XPath("(//table[@id='DataTables_Table_productResult_paneList']/tbody/tr/td[7]//a[@id='productResult_grid_add'])[" + i + "]");
        public By ByProductDescription(int i) => By.XPath("(//table[@id='DataTables_Table_productResult_paneList']/tbody/tr/td[2])[" + i + "]");

        public By ByProductlabel(string description) => By.XPath("//div[contains(@aria-label,'" + description + "')]");
        #endregion
        #endregion

        #region WebElement
        public By ByOrderCode(string OrderCode) => By.XPath("//label[contains(text(),'Order Code')]/following-sibling::div[contains(text(),'" + OrderCode + "')]");
        public IWebElement UnitSellingPrice(int index) => WebDriver.FindElement(ByUnitSellingPrice(index));
        public IWebElement DiscountField(int index) => WebDriver.FindElement(ByDiscountField(index));
        public IWebElement ProductSkuNumber(int index) => WebDriver.FindElement(ByProductSkuNumber(index));
        public IWebElement ProductUnitListprice(int index) => WebDriver.FindElement(ByProductUnitListprice(index));
        public string getOrderCodeAttributeName(string orderCode) => WebDriver.FindElement(ByGetOrderCodeAttribute(orderCode)).GetAttribute("id");
        public IWebElement ProductOrderCode(TestCase test) => WebDriver.FindElement(ByOrderCode(test.OrderCode));

        public IWebElement AddItem(int count) => WebDriver.FindElement(ByAddItem(count));
        public IWebElement AddProductTextBoxObject => WebDriver.FindElements(ByAddProductTextBox).FirstOrDefault();
        public IWebElement AddProductButtonbject => WebDriver.FindElements(ByAddProduct).FirstOrDefault();
        public List<IWebElement> NoOfProducts => WebDriver.FindElements(ByListOfProducts).ToList();
        #region ProductSearch
        public IWebElement SystemSearch => WebDriver.FindElement(BySystemSearch);
        public IWebElement SAndPSearch => WebDriver.FindElement(BySAndPSearch);
        public IWebElement ProductSearchButton => WebDriver.FindElement(ByProductSearchButton);
        public List<IWebElement> ListOfProduct => WebDriver.FindElements(ByListOfProduct).ToList();
        public IWebElement ProductListPriceInSearchpanel(int i) => WebDriver.FindElement(ByProductListPriceInSearchpanel(i));
        public IWebElement AddProductInProductSearchPage(int i) => WebDriver.FindElement(ByAddProductInProductSearchPage(i));
        public IWebElement ViewQuote => WebDriver.FindElement(ByViewQuote);
        public IWebElement ProductDescription(int i) => WebDriver.FindElement(ByProductDescription(i));

        public IWebElement Productlabel(string description) => WebDriver.FindElement(ByProductlabel(description));

        #endregion
        #endregion

    }
}
