using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;

using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using System.Threading;

using System.Globalization;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D11_D12
{
    public class ScenarioMain : D01.ScenarioMain
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
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
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
                    ds.OrderCode = test.ProductIdentificationData;
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
                ChangeSFDCId(retriveSmartPriceData,withOutVersionQuoteNo,customerClass,test.IsSFDCIdentificationDataAutomatic,ds, test.SFDCIdentificationQuery,test.SFDCIdentificationData, totalFinancialCalculation,scenarioId,testCaseNumber,test,true);


                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D11_D12", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs,resultLog, test);
            }
        }

        public void ChangeSFDCId(List<DSAPageObject> retriveSmartPriceData, string withOutVersionQuoteNo, CustomerClass customerObj, string IsSFDCIdentificationDataAutomatic, DSAPageObject ds, string SFDCIdentificationQuery, string SFDCIdentificationData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber,TestCase testCase, bool IsDirect = false, PNRAPILog pnrlog1=null,PNRAPILog pnrlog2=null)
        {
            try
            {


                WebDriver = ds.WebDriver;
                OracleDBConnection odb = new OracleDBConnection();
                SFDCChangeClass sfdcDealIdCheck = new SFDCChangeClass(WebDriver);
                ResultLogging resultLog = new ResultLogging();

                Constant cs = new Constant(WebDriver);
                CustomerClass customerClass = new CustomerClass();
                odb.RetriveEndCustomerDetailsFromCurrentDraftQuote(withOutVersionQuoteNo, customerClass);
                
                
                
                if (IsSFDCIdentificationDataAutomatic == "Y")
                {
                    //Retrive QuoteAccountid
                    odb.RetriveSFDCDealId(withOutVersionQuoteNo, ds, customerClass, SFDCIdentificationQuery);//Retrive SFDCDealId
                    if (string.IsNullOrEmpty(ds.PnrLog.SFDCDEALID))
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("SFDCDealId is not found", true);
                    }
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("SFDCDealId is  found", true);
                    }

                }
                else
                {

                    ds.PnrLog.SFDCDEALID = SFDCIdentificationData;
                    odb.RetriveSFDCDealDetails(withOutVersionQuoteNo, ds, customerClass);//retrive sfdc details.
                }
                PNRAPILog pnrapilog1 = new PNRAPILog();
                if (IsDirect == false)
                {
                    odb.RetriveSFDCDetails(withOutVersionQuoteNo, pnrapilog1);
                  
                }

                if (!string.IsNullOrEmpty(sfdcDealIdCheck.SfdcDealIdElement.GetAttribute("innerText")))
                    sfdcDealIdCheck.ClearSFDCField();
                
                sfdcDealIdCheck.SfdcDealIdElement.SendKeys(ds.PnrLog.SFDCDEALID);//change sfdc dealid
                try
                {
                    sfdcDealIdCheck.SoldToCustomer.Click();
                    Thread.Sleep(5000);
                }
                catch (Exception ex)
                {

                }
                CultureInfo culture = new CultureInfo("en-US");
                DateTime tempDate = Convert.ToDateTime(ds.CreatedDate, culture);

                DateTime sfdcOptyBookDate = Convert.ToDateTime(ds.PnrLog.SFDCOPPTYBOOKEDDATE, culture);
                string TotalNoOfDays =Math.Abs(tempDate.Subtract(sfdcOptyBookDate).Days).ToString();
                // retrive SFDC guidance Message from pnrapilog

                odb.RetriveSFDCGuidanceMessage(withOutVersionQuoteNo, ds);
                //Check if opp id is valid or invalid(Suite Decision)
                if (Math.Abs(Convert.ToInt32(TotalNoOfDays)) > 30 || ds.PnrLog.SFDCOPPTYSTAGE.Contains("Win") || ds.PnrLog.SFDCOPPTYSTAGE.Contains("Lost") || ds.PnrLog.SFDCOPPTYSTAGE.Contains("Cancelled"))
                {
                    ds.ValidOppportunitySuiteDecision = "No";
                    SuitDecisionInvalid(ds, cs, sfdcDealIdCheck);

                }
                else
                {
                    ds.ValidOppportunitySuiteDecision = "Yes";
                    SuitDecisionValid(ds, cs, sfdcDealIdCheck);


                }

                ComparSuiteDecisionVsSpDecision(ds);
                PNRAPILog pnrapilog2 = new PNRAPILog();
                if (IsDirect==false)
                {
                    odb.RetriveSFDCDetails(withOutVersionQuoteNo, pnrapilog2);
                    ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);
                }

                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, null, ds, ds.ServiceHeaderText);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(testCase, ds, ds.country, dsaQuoteSummaryPage);

                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(testCase.TestCaseTitle, scenarioId, testCase.ApprovalType, quoteSummaryObject, testCase);
                }
                if (IsDirect)
                    resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, null, customerClass, true, null, IsDirect,false,quoteSummaryObject,goalDetails);
                else
                    resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", customerObj, true, "", null, null, false, pnrapilog1, pnrapilog2,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, testCase, scenarioId, retriveSmartPriceData);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            }

        private void ComparSuiteDecisionVsSpDecision(DSAPageObject ds)
        {
           if(ds.ValidOppportunitySuiteDecision==ds.PnrLog.ValidOppportunitySPDecision)
            {
                ds.OpportunityStatus = "Passed";
            }
            else
            {
                ds.OpportunityStatus = "Failed";
            }
            if (ds.IsSfdcMessageDisplayed== "Yes" && !string.IsNullOrEmpty(ds.PnrLog.GUIDANCEMESSAGESJSON))
            {
                ds.SfdcMessageDisplayed = "Passed";
            }
            else
            {
                ds.SfdcMessageDisplayed = "Failed";
            }

        }

        private void SuitDecisionValid(DSAPageObject ds,Constant locator,SFDCChangeClass sfdcpageObj)
        {
            ds.ValidOppportunity = "true";
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Valid Opportunity", true);
            
            if (Math.Round(Convert.ToDecimal(ds.DealSize), 2)== Math.Round(Convert.ToDecimal(ds.PnrLog.OPPORTUNITYSIZE), 2))
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("DealSize matched with OpportunitySize", true);
                if (locator.IsElementPresent(sfdcpageObj.BySfdcMessage))
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("sfdcpageObj.BySfdcMessage", true);
                    //ds.ValidOppportunity = "true";
                    ds.PnrLog.ValidOppportunitySPDecision = "Yes";
                    ds.IsSfdcMessageDisplayed = "Yes";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Opp is not msg is not displayed Failed", true);
                    ds.PnrLog.ValidOppportunitySPDecision = "No";
                }
                ds.PnrLog.ValidOppportunitySPDecision = "Yes";

            }
            else
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("DealSize is not matched with OpportunitySize", true);
                // Console.WriteLine("Failed");
                ds.PnrLog.ValidOppportunitySPDecision = "No";
            }
        }

        private void SuitDecisionInvalid(DSAPageObject ds,Constant locator,SFDCChangeClass sfdcpageObj)
        {
          //  Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("InValid Opportunity", true);
            if (Math.Round(Convert.ToDecimal(ds.DealSize), 2) != Math.Round(Convert.ToDecimal(ds.PnrLog.OPPORTUNITYSIZE), 2))
            {
           
                
                if (locator.IsElementPresent(sfdcpageObj.BySfdcMessage))
                {
                    string sfdcText = WebDriver.FindElement(By.XPath("//*[@id='sfdcNotificationMessages_0']/p")).GetAttribute("innerText");
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(sfdcText, true);
                    if(sfdcText.Equals(sfdcpageObj.sfdcMessageText))
                    {
                        Console.WriteLine("Passed");
                        ds.ValidOppportunity = "true";
                        ds.IsSfdcMessageDisplayed = "Yes";
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                        ds.ValidOppportunity = "false";
                    }

                    ds.PnrLog.ValidOppportunitySPDecision = "Yes";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Opp is not msg is not displayed", true);
                    //ds.ValidOppportunity = "false";
                    ds.PnrLog.ValidOppportunitySPDecision = "No";
                }
            }
            else
            {
                string[] info = ds.PnrLog.AdditionalInfo.Split(';');
                string quoteSizeDiscountPolicy = "";
                string[] value;
                for (int i=0;i<info.Length;i++)
                {
                    if (info[i].Contains("Value"))
                    {
                        value = info[i].Split('[');
                        quoteSizeDiscountPolicy = value[3].Replace("]","");
                    }
                }
                //     string[] additionalinformation = info[1].Split('[');
                decimal assumedRevenue = 0;
                //ds.quotesize-(ds.quotesize*quoteSizeDiscountPolicy/100)
                if (!string.IsNullOrEmpty(quoteSizeDiscountPolicy))
                 assumedRevenue = Convert.ToDecimal(ds.QuoteSize) - (Convert.ToDecimal(ds.QuoteSize) * Convert.ToDecimal(quoteSizeDiscountPolicy) / 100);
                else
                    assumedRevenue = Convert.ToDecimal(ds.QuoteSize) - (Convert.ToDecimal(ds.QuoteSize) * Convert.ToDecimal(0) / 100);
                //     string assumedRevenue = (Convert.ToDecimal(additionalinformation)* Convert.ToDecimal(ds.QuoteSize)).ToString();
                if (assumedRevenue== Convert.ToDecimal(ds.PnrLog.OPPORTUNITYSIZE))
                {
                    if(locator.IsElementPresent(sfdcpageObj.BySfdcMessage))
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("sfdcpageObj.BySfdcMessage", true);
                        ds.ValidOppportunity = "true";
                        ds.IsSfdcMessageDisplayed = "Yes";
                    }
                    else
                    {
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Opp is not msg is not displayed Failed", true);
                    }
                    ds.PnrLog.ValidOppportunitySPDecision = "Yes";
                }
                else
                {
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Failed", true);
                    //ds.ValidOppportunity = "false";
                    ds.PnrLog.ValidOppportunitySPDecision = "No";
                }

            }
            
        }
    }
}
        
            

      
