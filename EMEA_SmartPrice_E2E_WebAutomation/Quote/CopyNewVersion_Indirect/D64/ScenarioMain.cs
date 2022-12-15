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
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Indirect.D64.ScenarioMain
{
  public class ScenarioMain: CopyNewVersion_Direct.D17.ScenarioMain
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
      


        public  void ExecuteTestCases(TestCase test,int testCaseNumber, string country = null, List<string> application = null)
        {

                    WebDriver = new ChromeDriver();
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerPage customerPage = new CustomerPage(WebDriver);
                    Product productObject = new Product(WebDriver); 
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    ds.country = country;
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
                        string draftquote = "";
                        if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                        {
                            draftquote = CopyAndCreateNewVersion();
                          
                           
                        }
                        odb.RetriveEndUserInformation(draftquote, customerObj2);
                        customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);

                        odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                        ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);




                        productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,country,LoadDeserializeObject());
                       
                        RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,null,ds);
                        SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                        CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                //Add Product Reporting
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle,"D64", test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSVIndirect(ds,retriveSmartPriceData, totalFinancialCalculation,"D64",testCaseNumber,null,customerObj,true,"",null,null,false,pnrapilog1,pnrapilog2,quoteSummaryObject,goalDetails);
                         resultLog.ExportToSqlServerDB(ds, test,"D64", retriveSmartPriceData);
                //WebDriver.Close();
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D64", e);
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
        
            

      
