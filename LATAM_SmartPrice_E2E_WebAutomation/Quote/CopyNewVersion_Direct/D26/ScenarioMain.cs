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
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare;
using Guidance = SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare.Guidance;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D26
{
    public class ScenarioMain : D17.ScenarioMain
    {
        public string TestURL;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        {
            WebDriver = driver;
            TestURL = URL;

        }

        
        public void ChangeCustomer(Constant locator,CustomerPage customerPageObj,CustomerClass customerClass)
        {
            
            try
            {
           //     customerClass.CustomerName=customerPageObj.GetCustomerName.Text;
                locator.MenuButton.Click();
                Thread.Sleep(3000);
                customerPageObj.ClickOnCustomerLink.Click();
                Thread.Sleep(3000);
                customerPageObj.SearchCustomerID_DCN.SendKeys(customerClass.NewEndUserAccountCustomerId);
                customerPageObj.FindCustomerButton.Click();
                Thread.Sleep(2000);
                customerPageObj.UseInQuote.Click();
                customerPageObj.ChangeCustomerPopUpButton.Click();
         
                Thread.Sleep(5000);
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();

            List<DSAPageObject> retriveSmartPriceDataList = new List<DSAPageObject>();
            try
            {

                ResultLogging resultLog = new ResultLogging();
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                   // ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                ds.QuoteNumber= CopyAndCreateNewVersion();
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10.ScenarioMain d10scenario = new CopyNewQuote_Direct.D10.ScenarioMain();
                d10scenario.UpdateCustomer(retriveSmartPriceDataList,withOutVersionQuoteNo, totalFinancialCalculation, ds, resultLog, scenarioId, test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber, test.CustomerIdentificationQuery, test.CustomerIdentificationData, test.CrossSegmenId,null);

                
                resultLog.ExportToSqlServerDB(ds,test, scenarioId, retriveSmartPriceDataList);
                

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
           
        }

     

       
       
    }
}
        
            

      
