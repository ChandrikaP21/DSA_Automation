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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D73_D74
{
    public class ScenarioMain : CopyNewVersion_Direct.D17.ScenarioMain
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
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            CustomerPage customerPage = new CustomerPage(WebDriver);
            ResultLogging resultLog = new ResultLogging();

            try
            {

              
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                if (customerObj1 != null)
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails are retrived", true);
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndCustomerDetails not able to retrived", true);
                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                   ds.QuoteNumber=CopyAndCreateNewVersion();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                    odb.RetriveEndUserInformation(ds.QuoteNumber, customerObj2);
                }
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11_12 = new CopyNewQuote_Direct.D11_D12.ScenarioMain();
                d11_12.ChangeSFDCId(retriveSmartPriceData,withOutVersionQuoteNo,customerObj,test.IsSFDCIdentificationDataAutomatic,ds, test.SFDCIdentificationQuery,test.SFDCIdentificationData, totalFinancialCalculation,scenarioId,testCaseNumber);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                //WebDriver.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

        
    }
}
        
            

      
