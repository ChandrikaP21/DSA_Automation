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
using System.Globalization;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D11_D12
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

       
        

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
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
                ds.QuoteNumber= CopyAndCreateNewQuote();
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                ChangeSFDCId(retriveSmartPriceData,withOutVersionQuoteNo,customerClass,test.IsSFDCIdentificationDataAutomatic,ds, test.SFDCIdentificationQuery,test.SFDCIdentificationData, totalFinancialCalculation,scenarioId,testCaseNumber);
                

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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs,resultLog, test);
            }
        }

        public void ChangeSFDCId(List<DSAPageObject> retriveSmartPriceData, string withOutVersionQuoteNo, CustomerClass customerClass, string IsSFDCIdentificationDataAutomatic, DSAPageObject ds, string SFDCIdentificationQuery, string SFDCIdentificationData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber, bool IsDirect = false)
        {
            try
            {


                WebDriver = ds.WebDriver;
                OracleDBConnection odb = new OracleDBConnection();
                SFDCChangeClass sfdcDealIdCheck = new SFDCChangeClass(WebDriver);
                ResultLogging resultLog = new ResultLogging();

                Constant cs = new Constant(WebDriver);
                //List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
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
                    odb.RetriveSFDCDealDetails(withOutVersionQuoteNo, ds, customerClass);
                }
                if (!string.IsNullOrEmpty(sfdcDealIdCheck.SfdcDealIdElement.GetAttribute("innerText")))
                    sfdcDealIdCheck.SfdcDealIdElement.Clear();
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
                string TotalNoOfDays = tempDate.Subtract(sfdcOptyBookDate).Days.ToString();
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



                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, null, ds, ds.ServiceHeaderText);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData, totalFinancialCalculation);
                string runId = RunId;
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, runId, null, customerClass, true, null, IsDirect);

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
        
            

      
