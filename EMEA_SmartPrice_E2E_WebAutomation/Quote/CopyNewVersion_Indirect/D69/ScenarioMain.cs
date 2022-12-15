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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D69
{
    public class ScenarioMain :Quote.CopyNewVersion_Direct.D17.ScenarioMain
    {
        // public string TestURL;
        //// public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        // public ScenarioMain(string URL ):base(URL)
        // {
        //     TestURL = URL;

        // }

        // Constant cs = new Constant();

        public ScenarioMain(string URL) : base(URL)
        { }
        public ScenarioMain() : base()
        { }


        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public void ExecuteTestcases(TestCase test, string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
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
                    SearchQuote(ds, cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds, cs);

                }

                //Retrive customerdetails from Actual Quote...
                CustomerClass customerObj1 = new CustomerClass();
                CustomerClass customerObj2 = new CustomerClass();
                CustomerClass customerObj = new CustomerClass();
                PNRAPILog pnrapilog1 = new PNRAPILog();
                PNRAPILog pnrapilog2 = new PNRAPILog();
                
                odb.RetriveSFDCDetails(ds.ActualQuote, pnrapilog1);
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
                    CopyAndCreateNewVersion();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                   
                }
                string  draftquote = ds.QuoteNumber.Split('.')[0];
                odb.RetriveEndUserInformation(draftquote, customerObj2);
                customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D07.ScenarioMain d07downgradeServices= new Quote.CopyNewQuote_Direct.D07.ScenarioMain();
                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                
                bool isindirect = true;
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                        {

                            d07downgradeServices.UpGradeServices(cs, test, ds);
                            string upgradedServiceListPrice = ds.ServiceModules.Select(x => x.Services.Select(y => y.ListPrice)).ToString();
                            if (isindirect)
                            {
                                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", customerObj, cs, test,isindirect);
                            }
                            else
                            {
                                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", null, cs, test, false);
                            }
                            d07downgradeServices.DownGradeServices(cs, test, ds);
                            if (isindirect)
                            {
                                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "2", customerObj, cs, test, isindirect);
                            }
                            else
                            {
                                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "2", null, cs, test,false);
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Product is not available , Accessories  can not be added.");

                    }
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d07downgradeServices. DownGradeServices(cs, test.ModuleId, test.OfferingId, ds);
                        if (isindirect)
                        {
                            RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", customerObj, cs, test);
                        }
                        else
                        {
                            RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, ds, resultLog, scenarioId, testCaseNumber, "1", null, cs, test);
                        }
                    }
                    else
                    {

                        Console.WriteLine("Product is not available , Accessories  can not be added.");


                    }
                }



                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D69", e);
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
        
            

      
