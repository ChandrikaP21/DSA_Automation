using System;
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

using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10
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
        
       


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
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
            DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
            DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
            string draftQuote = "";
            ds.country = country;
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
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    summaryObject.PerformCopyAsNewQuote();
                    WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(10), true);
                    draftQuote = draftQuotePageObject.DraftQuoteNumber;
                }


                string[] str = draftQuote.Split('.');
                string withOutVersionQuoteNo = str[0];
                UpdateCustomer(retriveSmartPriceData,withOutVersionQuoteNo, totalFinancialCalculation,ds,resultLog,scenarioId,test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber,test.CustomerIdentificationQuery,test.CustomerIdentificationData,test.CrossSegmenId,customerClass,test,false,null,null,application);

                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);


                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D13", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

        public void UpdateCustomer(List<DSAPageObject> retriveSmartPriceData, string withOutVersionQuoteNo,TotalFinancialCalculation totalFinancialCalculation,DSAPageObject ds,ResultLogging resultLog,string scenarioId,string IsCustomerInSameSegment,string IsCustomerIdentificationQueryBased,int testCaseNumber,string CustomerIdentificationQuery,string CustomerIdentificationData,string crossSegmenId,CustomerClass customerObject,TestCase test,bool isIndirect=false,PNRAPILog log1=null,PNRAPILog log2=null,List<string> application=null)
        {
            try
            {
                WebDriver = ds.WebDriver;
                OracleDBConnection odb = new OracleDBConnection();
                CustomerClass customerClass = new CustomerClass();

                odb.RetriveEndCustomerDetailsFromCurrentDraftQuote(withOutVersionQuoteNo, customerClass);
                if (isIndirect == false)
                {

                    CompareCustomerDetails(customerClass);//1st refresh
                }

               
                if (customerObject != null)
                {
                    RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "0", customerObject, test, isIndirect, log1, log2, application);
                }

                else
                {
                    RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", customerClass, test);
                }


                //Retrive accountCustomerId from draftQuote page afer coping the quote.

                if (IsCustomerInSameSegment == "Y")
                {
                    //string[] id = ds.LocalChannelIdElement.Text.Split('-');
                    //customerClass.CrossSegmenId =id[0] ;
                    Console.WriteLine(string.Format("Customer Segment :{0} ", customerClass.CrossSegmenId));
                    if (IsCustomerIdentificationQueryBased == "Y")
                    {
                        odb.RetriveEndCustomerDetailsNotInCurrentQuote(withOutVersionQuoteNo, customerClass, CustomerIdentificationQuery);

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
                        odb.RetriveEndCustomerDetailsNotInCurrentQuote(withOutVersionQuoteNo, customerClass, CustomerIdentificationQuery);

                    }
                    else
                    {
                        customerClass.NewEndUserAccountCustomerId = CustomerIdentificationData;

                    }
                }
                CustomerPage customerPageObj = new CustomerPage(WebDriver);
                if (isIndirect) customerPageObj.ChangeCustomer(customerClass.NewAccountCustomerId, ds.country, isIndirect);
                else
                    customerPageObj.ChangeCustomer(customerClass.NewEndUserAccountCustomerId, ds.country, false);
                CompareCustomerDetails(customerClass);//2nd refresh
                if (retriveSmartPriceData.Count > 0)
                {
                    retriveSmartPriceData.RemoveAt(0);
                }

                RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "2", customerClass, test);
                customerPageObj.RefreshSmartPriceButton.Click();
                Thread.Sleep(5000);
                CompareCustomerDetails(customerClass);//3rd refresh
                if (retriveSmartPriceData.Count > 0)
                {
                    retriveSmartPriceData.RemoveAt(0);
                }
                RefreshGuidanceAndGenerateReport(totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "3", customerClass, test);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(" Error occured in UpdateCustomer()" + ex.Message,true);
            }
            
        }

        public void CompareCustomerDetails(CustomerClass customerClass)
        {
            try
            {
                if (customerClass.OldAccountCustomerId == customerClass.NewAccountCustomerId)
                {
                    customerClass.IsAccountCustomerIdMatched = "true";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("AccountCustomerId is not matched", true);
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
            catch(Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Unable to compare customer details,Please check CompareCustomerDetails()"+ex.Message);
            }

        }

        public void RefreshGuidanceAndGenerateReport(TotalFinancialCalculation totalFinancialCalculation,DSAPageObject ds,ResultLogging resultLog,string scenarioId,int testCaseNumber,string refreshId,CustomerClass customerObj,TestCase test,bool isindirect=false,PNRAPILog pNRAPILog1=null,PNRAPILog pnrapilog2=null,List<string> application=null)
        {
            try
            {
                List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, null, ds, ds.ServiceHeaderText);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
                if (application.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

                }
                if (application.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, scenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                if (isindirect == true)
                    resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, refreshId, customerObj, isindirect, "", null, null, false, pNRAPILog1, pnrapilog2, quoteSummaryObject, goalDetails);
                else
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, refreshId, customerObj,true, null, isindirect,false,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
            }
            catch(ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Error occuered in RefreshGuidanceAndGenerateReport()"+ex.Message+ex.StackTrace);
            }
        }
        
       
    }
}
        
            

      
