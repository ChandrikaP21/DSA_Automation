﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.Quote;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.Globalization;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D115
{
    public class ScenarioMain : ReSell.D91.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain() : base()
        {
            

        }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }


        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            DSAPageObject ds = new DSAPageObject(WebDriver);
      
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();

            ResultLogging resultLog = new ResultLogging();
            Product productObject = new Product(WebDriver);
            Constant cs = new Constant(WebDriver);

            try
            {

                
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productObject,true);
                
                ds.QuoteNumber = ds.draftquotnumber.Text;
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];

                List<DSAPageObject> changedPricePointList = new List<DSAPageObject>();
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain d13Scenario = new SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D13.ScenarioMain();
                Thread.Sleep(3000);
                //D13 we have impleneted only for 1st product.
                if (test.PriceChangeType=="System")
                {
                    d13Scenario.RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, true, ds, cs, resultLog, testCaseNumber, test, scenarioId, null, null, null,true,customerClass);
                    
                }

                resultLog.ExportToSqlServerDB(ds, test, scenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, scenarioId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,null, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
   
    }
    
}
        
            

      
