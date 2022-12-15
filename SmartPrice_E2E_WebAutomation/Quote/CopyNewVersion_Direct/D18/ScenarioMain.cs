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
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using OpenQA.Selenium.Support.UI;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D18
{
 public  class ScenarioMain:D17.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
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
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            Constant cs = new Constant(WebDriver);
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
                SearchQuote(ds,cs);

            }
            string draftquote = "";
            if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
            {
                    draftquote = CopyAndCreateNewVersion();
            }
                if (test.IsProductSelectionAuto == "Y")
                {
                    productObject.OrderCode = odb.RetriveOrderCode(test.ProductIdentificationQuery);
                }
                else
                {
                    productObject.OrderCode = test.ProductIdentificationData;
                }

                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

            RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, null,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
            CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
            resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,scenarioId,testCaseNumber,RunId);
            resultLog.ExportToSqlServerDB(ds,test,scenarioId,retriveSmartPriceData);
            resultLog.ExportToSummaryTable(test, scenarioId);
            Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test, null,retriveSmartPriceData);
            }
        }
    }
           
        }
        
            

      
