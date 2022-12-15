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
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D37
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
        public ScenarioMain() : base()
        {
            

        }

        



        public void ExecuteTestcases(TestCase test, string ScenarioID,int testCaseNumber)
        {

           
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
          
            Product productObject = new Product(WebDriver);
            CustomerClass customerClass = new CustomerClass();
            WebDriver = new ChromeDriver();
            ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceDataList = new List<DSAPageObject>();
            try
            {

                
                //   ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                LoadDSA();
                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D05.ScenarioMain d05 = new CopyNewQuote_Direct.D05.ScenarioMain();
                // RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                bool IsOfferingId = true;

                

                    
                        if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))
                        {
                            d05.AddService(cs, ds);

                        }
                        if (test.ModuleId != "" && string.IsNullOrEmpty(test.OfferingId))
                        {
                            d05.AddService(cs, test, ds);

                        }
                        if (test.ModuleId != "" && !string.IsNullOrEmpty(test.OfferingId))
                        {
                            d05.AddService(cs, test, ds, IsOfferingId);
                        }
                       
                        

                RetriveDataFromDSAScreen(retriveSmartPriceDataList,totalFinancialCalculation, ScenarioID, ds, ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceDataList, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation, retriveSmartPriceDataList, ds.ServiceModules);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceDataList, totalFinancialCalculation,ScenarioID,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioID, retriveSmartPriceDataList);
                resultLog.ExportToSummaryTable(test, ScenarioID);

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //WebDriver.Close();
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,null ,ds, totalFinancialCalculation, cs, resultLog, test);
            }

        }
       



    }
    
}




