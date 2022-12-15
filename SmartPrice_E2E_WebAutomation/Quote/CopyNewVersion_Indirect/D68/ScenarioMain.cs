using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Objects;

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
using System.IO;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D68
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



        public void ExecuteTestcases(TestCase test, string ScenarioId, int testCaseNumber)
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

                    ds.PnrLog.ServiceType = "1";
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
                    CopyAndCreateNewVersion();
                    //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                    odb.RetriveEndUserInformation(ds.QuoteNumber, customerObj2);
                }
                customerPage.CompareCustomerDetails(customerObj1, customerObj2,customerObj);
                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06 = new CopyNewQuote_Direct.D06.ScenarioMain();
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d06.UpGradeServices(cs, test, ds);

                    }
                    else
                    {
                        Console.WriteLine("Product is not available , Services  can not be added.");

                    }
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d06.UpGradeServices(cs, test.ModuleId, test.OfferingId, ds);

                    }
                    else
                    {

                        Console.WriteLine("Product is not available , Services can not be added.");


                    }
                }



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, ScenarioId, testCaseNumber, RunId, null, customerObj, null, null, true);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);


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
       
    }
}


 
        
            

      
