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

using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D35
{
    public class ScenarioMain : D34.ScenarioMain
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



        public void ExecuteTestcases(TestCase test, string ScenarioId, int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
           
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
           
            ResultLogging resultLog = new ResultLogging();
            try
            {

               

                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03upgradeComponent = new CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain();
                List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//without module id and with out component ID
                {
                    d03upgradeComponent.UpGradeComponent(cs,test,ds,WebDriver);

                }
                if (test.ModuleId != "" && string.IsNullOrEmpty(test.ComponentId))//With Module ID
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



                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData, ds.ServiceModules, ScenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation, ScenarioId, testCaseNumber, RunId);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
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
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
           
        
        
    }
}
    

                                         
    
  
        
            

      
