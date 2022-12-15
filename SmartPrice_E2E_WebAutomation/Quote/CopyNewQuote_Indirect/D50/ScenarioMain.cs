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
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D50.ScenarioMain
{
  public class ScenarioMain:D49.ScenarioMain
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



        public  void ExecuteTestCases(TestCase test,int testCaseNumber)
        {

                    WebDriver = new ChromeDriver();
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerPage customerPage = new CustomerPage(WebDriver);
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();

                    Product productObject = new Product(WebDriver);
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
                        string draftquote = "";
                        if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                        {
                            draftquote = CopyAndCreateNewQuote();
                            //odb.RetriveEndCustomerDetailsNotInCurrentQuote(ds.ActualQuote, customerObj);
                            odb.RetriveEndUserInformation(draftquote, customerObj2);
                        }
                        customerPage.CompareCustomerDetails(customerObj1, customerObj2,customerObj);
                
                        productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

                        RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,null,ds);
                         SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                        CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                        //Add Product Reporting
                        resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,"d02",testCaseNumber,RunId,null,customerObj,null,null,true);
                        resultLog.ExportToSqlServerDB(ds,test,"d02",retriveSmartPriceData);
                        resultLog.ExportToSummaryTable(test, "d02");
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
                    customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
                }
        }
            }
            
        }
        
            

      
