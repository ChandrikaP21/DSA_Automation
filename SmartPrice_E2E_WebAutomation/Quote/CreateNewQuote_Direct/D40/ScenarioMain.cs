﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Quote;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Reports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Chrome;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D40
{
    public class ScenarioMain : D34.ScenarioMain
    {
        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        /// <summary>
        /// Increase quantity based on the user input on addition to the existing quantity.
        ///e.g quantiy=1,given Quantity to increase=5, increaseQuantity=1+5=6;
        /// </summary>
        /// <param name="test"></param>
        /// <param name="scenarioid"></param>
        /// <param name="testCaseNumber"></param>
        public void ExecuteTestcases(TestCase test,string scenarioid,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();

            //foreach (Scenario testCase in TestDataReader._scenarios[2].TestCases)
            //{
            //    foreach (TestCase test in testCase.TestCases)
            //    {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
           
            List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            try
            {
                
               // ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

                productObject.IncreaseQuantity(test, cs);
                RetriveDataFromDSAScreen(retriveSmartPriceDetailsList,totalFinancialCalculation, scenarioid,ds);
                SmartPriceCalculation(retriveSmartPriceDetailsList, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceDetailsList);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceDetailsList, totalFinancialCalculation,scenarioid,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,scenarioid,retriveSmartPriceDetailsList);
                resultLog.ExportToSummaryTable(test,scenarioid);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

 
        public By ByAddQuantity(int Count) => By.XPath("//input[@id='quoteCreate_LI_quantity_0_"+ Count + "']");
        
        private void ChangeQuantityForAllProduct(Constant constant,TestCase testCase)
        {
            try
            {
                

                while (constant.Count < constant.NoOfProducts.Count)
                {
                    WebDriver.FindElement(By.XPath("//input[@id='quoteCreate_LI_quantity_0_" + constant.Count + "']")).Clear();
                    //constant.IncreaseQuantity(constant.Count).Clear();
                    WebDriver.FindElement(By.XPath("//input[@id='quoteCreate_LI_quantity_0_" + constant.Count + "']")).SendKeys(testCase.Quantity.ToString());
                   // constant.QuantityLabel.Click();
                        new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(5000));

                   
                        

                    // new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(20000)).Until(ExpectedConditions.ElementToBeSelected(constant.Product));

                    //Thread.Sleep(20000);
                    constant.Count++;
                }
                   constant.jse.ExecuteScript("window.scrollBy(0,-750)");
                constant.ApplyChanges.Click();

                //WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10 };
                // wait.Until(ExpectedConditions.ElementToBeSelected(constant.Product));
                //wait.PollingInterval = TimeSpan.FromSeconds(10);

                IClock iClock = new SystemClock();

                WebDriverWait wait = new WebDriverWait(iClock, WebDriver, TimeSpan.FromSeconds(50), TimeSpan.FromSeconds(10));
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                //Console.WriteLine(ex.Message);
            }
        }

      }                              
    
}



