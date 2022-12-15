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

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D38
{
   public  class ScenarioMain:D34.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain():base()
        {
        }

       
        
        
        public void ExecuteTestcases(TestCase test,string ScenarioId, int testCaseNumber)
        {
           
                      ;
                    DSAPageObject ds = new DSAPageObject();
                     Constant cs = new Constant(WebDriver);
                     
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                     Product productObject = new Product(WebDriver);
                     CustomerClass customerClass = new CustomerClass();
                     WebDriver = new ChromeDriver();
                ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            try
            {
                        
               
                       
                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);
        // RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
        SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeService = new SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain();

        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d06UpgradeService.UpGradeServices(cs, test, ds);

                    }
                    else
                    {
                        Console.WriteLine("Product is not available , Services  can not be added.");

                    }
                }
        //Module ID with Service id is not applicable for now ..
                /*  if (!string.IsNullOrEmpty(test.ModuleId)&& !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeServices(cs, test.ModuleId,test.OfferingId, ds);

                    }
                    else
                    {

                        Console.WriteLine("Product is not available , Services can not be added.");


                    }
                }*/



                RetriveDataFromDSAScreen(retriveSmartPriceDetailsList,totalFinancialCalculation, ScenarioId, ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceDetailsList, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceDetailsList);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceDetailsList, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioId, retriveSmartPriceDetailsList);
                resultLog.ExportToSummaryTable(test,ScenarioId);

               
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
        
            

      
