﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Chrome;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D97
{
    public class ScenarioMain : StockAndSell.D78.ScenarioMain
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
        public void ExecuteTestcases(TestCase test,string scenarioid,int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
            {
                
               // ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
               
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productObject, true);


                productObject.IncreaseQuantity(test, cs);
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,scenarioid,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioid, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioid,testCaseNumber,"1",customerClass,true,"",true,false,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, scenarioid, retriveSmartPriceData);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();

            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D97", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,null, ds, totalFinancialCalculation, cs, resultLog, test);
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
                        new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(StaticBriefCase.TimeOutSecond));

                   
                        

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



