﻿using System;
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
using System.Globalization;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D45
{
    public class ScenarioMain : D34.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain() : base()
        {
            

        }

      
        

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            DSAPageObject ds = new DSAPageObject(WebDriver);
      
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            Constant cs = new Constant(WebDriver);
            Product productObject = new Product(WebDriver);
            
            List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();

            try
            {

               
                LoadDSA();
                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData,country);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,country,LoadDeserializeObject());
                WebDriverUtils.ScrollIntoView(WebDriver, ds.ByDraftQuoteNumber);
                ds.QuoteNumber = ds.draftquotnumber.Text;
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];

                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain d13Scenario = new EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain();
               
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    d13Scenario.RetriveDataFromDSAScreen(retriveSmartPriceDetailsList, totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, scenarioId, null, null, null);
                    
                }

                
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D45", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, cs, null, test);
            }
        }
   
    }
    
}
        
            

      
