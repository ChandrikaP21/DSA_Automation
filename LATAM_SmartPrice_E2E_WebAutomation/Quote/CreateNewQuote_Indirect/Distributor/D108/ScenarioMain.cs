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

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D108
{
   public  class ScenarioMain: ReSell.D91.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
        {
            TestURL = URL;

        }

        public ScenarioMain():base()
        {
        }

        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }


        public void ExecuteTestcases(TestCase test,string ScenarioId, int testCaseNumber)
        {


                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                     Constant cs = new Constant(WebDriver);
                     
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    
                     CustomerClass customerClass = new CustomerClass();
                     ResultLogging resultLog = new ResultLogging();

            try
                    {
                        
                        

                Product productobject = new Product(WebDriver);
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject,true);
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



                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioId, ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                 resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId,"",customerClass,true,"",true);
                 resultLog.ExportToSqlServerDB(ds,test,ScenarioId,retriveSmartPriceData);
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
                customeGc.NullifyUnusedObject(WebDriver,null, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
       
        
        
    }
  }
        
            

      
