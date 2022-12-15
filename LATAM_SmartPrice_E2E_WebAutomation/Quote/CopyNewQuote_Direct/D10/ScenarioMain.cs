using System;
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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10
{
    public class ScenarioMain : D01.ScenarioMain
    {
        public string TestURL;
        public IWebDriver chromedriver;
        // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;
            

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            chromedriver = driver;

        }
        public ScenarioMain() : base()
        {
            

        }
        
       


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            //CustomerPage customerPageObj = new CustomerPage(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            List <DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
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
                   // ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                ds.QuoteNumber= CopyAndCreateNewQuote();


                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                UpdateCustomer(retriveSmartPriceData,withOutVersionQuoteNo, totalFinancialCalculation,ds,resultLog,scenarioId,test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber,test.CustomerIdentificationQuery,test.CustomerIdentificationData,test.CrossSegmenId,customerClass);

                resultLog.ExportToSqlServerDB(ds, test, "D10", retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, "D10");


                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               
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

        public void UpdateCustomer(List<DSAPageObject> retriveSmartPriceData, string withOutVersionQuoteNo,TotalFinancialCalculation totalFinancialCalculation,DSAPageObject ds,ResultLogging resultLog,string scenarioId,string IsCustomerInSameSegment,string IsCustomerIdentificationQueryBased,int testCaseNumber,string CustomerIdentificationQuery,string CustomerIdentificationData,string crossSegmenId,CustomerClass customerObject,bool isIndirect=false)
        {
            WebDriver = ds.WebDriver;
            OracleDBConnection odb = new OracleDBConnection();
            CustomerClass customerClass = new CustomerClass();
            
            odb.RetriveEndCustomerDetailsFromCurrentDraftQuote(withOutVersionQuoteNo, customerClass);
            if (isIndirect == false)
            {
                CompareCustomerDetails(customerClass);//1st refresh
            }
                        
            string runId = RunId;
            if(customerObject!=null)
            {
                RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "0", customerObject, isIndirect);
            }
           
            else
            {
                RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "1", customerClass);
            }
           

            //Retrive accountCustomerId from draftQuote page afer coping the quote.

            if (IsCustomerInSameSegment == "Y")
            {
                Console.WriteLine(string.Format("Customer Segment :{0} ", customerClass.CrossSegmenId));
                if (IsCustomerIdentificationQueryBased == "Y")
                {
                    odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.QuoteNumber, customerClass, CustomerIdentificationQuery);

                }
                else
                {
                    customerClass.NewEndUserAccountCustomerId = CustomerIdentificationData;

                }
            }
            else
            {
                customerClass.CrossSegmenId = crossSegmenId;
                if (IsCustomerIdentificationQueryBased == "Y")
                {
                    odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.QuoteNumber, customerClass, CustomerIdentificationQuery);

                }
                else
                {
                    customerClass.NewEndUserAccountCustomerId = CustomerIdentificationData;

                }
            }
            CustomerPage customerPageObj = new CustomerPage(WebDriver);
            customerPageObj.ChangeCustomer(customerClass.NewEndUserAccountCustomerId);
            CompareCustomerDetails(customerClass);//2nd refresh
            if(retriveSmartPriceData.Count>0)
            {
                retriveSmartPriceData.RemoveAt(0);
            }
            
            RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "2", customerClass);
            customerPageObj.RefreshSmartPriceButton.Click();
            Thread.Sleep(5000);
            CompareCustomerDetails(customerClass);//3rd refresh
            if (retriveSmartPriceData.Count > 0)
            {
                retriveSmartPriceData.RemoveAt(0);
            }
            RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, runId, "3", customerClass);
            
        }

        public void CompareCustomerDetails(CustomerClass customerClass)
        {
            if(customerClass.OldAccountCustomerId==customerClass.NewAccountCustomerId)
            {
                customerClass.IsAccountCustomerIdMatched = "true";
            }
            else
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("AccountCustomerId is not matched",true);
            }
            if (customerClass.OldAccountId == customerClass.NewAccountId)
            {
                customerClass.IsAccountIdMatched = "true";
            }
            else
            {
                customerClass.IsAccountIdMatched = "false";
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("AccountId is not matched", true);
            }
            if (customerClass.OldEndUserAccountCustomerId == customerClass.NewEndUserAccountCustomerId)
            {
                customerClass.IsEndUserAccountCustomerIdMatched = "true";
            }
            else
            {
                customerClass.IsEndUserAccountCustomerIdMatched = "false";
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndUserAccountCustomerId is not matched", true);
            }
            if (customerClass.OldEndUserAccountId == customerClass.NewEndUserAccountId)
            {
                customerClass.IsEndUserAccountIdMatched = "true";
            }
            else
            {
                customerClass.IsEndUserAccountIdMatched = "false";
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("EndUserAccountId is not matched", true);
            }
            if (customerClass.OldQuoteAccountId == customerClass.NewQuoteAccountId)
            {
                customerClass.IsQuoteAccountIdMatched = "true";
            }
            else
            {
                customerClass.IsQuoteAccountIdMatched = "false";
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("QuoteAccountId is not matched", true);
            }


        }

        public void RefreshGuidanceAndGenerateReport(TotalFinancialCalculation totalFinancialCalculation,DSAPageObject ds,ResultLogging resultLog,string scenarioId,int testCaseNumber,string RunId,string refreshId,CustomerClass customerObj,bool isindirect=false)
        {
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, null, ds, ds.ServiceHeaderText);
            SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
            CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
            resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, RunId, refreshId,customerObj,true,null,isindirect);
        }
        
       
    }
}
        
            

      
