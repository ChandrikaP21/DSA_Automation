using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;

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
using System.IO;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D65.ScenarioMain
{
    public class ScenarioMain :Quote.CopyNewVersion_Direct.D17.ScenarioMain
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



        public void ExecuteTestcases(TestCase test, string ScenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            ConfigurationPage configurationPage = new ConfigurationPage(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerPage customerPage = new CustomerPage(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            ds.country = country;
            try
            {

                

                LoadDSA();

                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                    ds.PnrLog.ServiceType = "1";
                }
                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();
                odb.RetriveEndUserInformation(ds.ActualQuote, customerObj1);
                odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
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
                    ds.QuoteNumber= CopyAndCreateNewVersion();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                   
                }
                string draftquote = ds.QuoteNumber.Split('.')[0];
                odb.RetriveEndUserInformation(draftquote, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);


                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03upgradeComponent = new Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain();
                if (string.IsNullOrEmpty(test.ModuleId)&& string.IsNullOrEmpty(test.ComponentId))//without module id and with out component ID
                {
                    d03upgradeComponent.UpGradeComponent(cs,ds,WebDriver);

                }
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d03upgradeComponent.UpGradeComponent(cs, test, ds,WebDriver);

                    }
                    else
                    {
                        //Console.WriteLine("Product is not available , Services  can not be added.");
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);
                    }
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.ComponentId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d03upgradeComponent.UpGradeComponent(cs, test.ModuleId, test.ComponentId, ds,WebDriver);

                    }
                    else
                    {

                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);


                    }
                }



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceData,ds.ServiceModules,ScenarioId);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,null,customerObj,true,"",null,null,false,pnrapilog1,pnrapilog2,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceData);
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D65", e);
                exec.LogException();

            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }

        }
       
        
    }
}
    

                                         
    
  
        
            

      
