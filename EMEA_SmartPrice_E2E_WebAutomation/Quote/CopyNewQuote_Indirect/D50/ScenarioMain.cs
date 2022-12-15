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
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Indirect.D50.ScenarioMain
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



        public  void ExecuteTestCases(TestCase test,int testCaseNumber, string country = null, List<string> application = null)
        {

                    WebDriver = new ChromeDriver();
                    OracleDBConnection odb = new OracleDBConnection();
                    DSAPageObject ds = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerPage customerPage = new CustomerPage(WebDriver);
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    DSAQuoteSummaryPage summaryObject = new DSAQuoteSummaryPage(WebDriver);
                    DraftQuotePageObject draftQuotePageObject = new DraftQuotePageObject(WebDriver);
                    Product productObject = new Product(WebDriver);
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
                            summaryObject.PerformCopyAsNewQuote();
                            WebDriverUtils.WaitForElementDisplayed(WebDriver, draftQuotePageObject.BydraftquoteNumber, TimeSpan.FromSeconds(5), true);
                         draftquote = draftQuotePageObject.DraftQuoteNumber;
                        }
                        odb.RetriveEndUserInformation(draftquote, customerObj2);
                        customerPage.CompareCustomerDetails(customerObj1, customerObj2, customerObj);
                        odb.RetriveSFDCDetails(draftquote, pnrapilog2);
                        ds.PnrLog.CompareSFDCDetails(pnrapilog1, pnrapilog2);

                         productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,country,LoadDeserializeObject());

                        RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation,null,ds);
                         SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                        CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceData);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);


                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle,"D50", test.ApprovalType, quoteSummaryObject, test);
                }

                resultLog.GenerateReportInCSVIndirect(ds, retriveSmartPriceData, totalFinancialCalculation,"D50", testCaseNumber, null, customerObj, true, "", null, null, false, pnrapilog1, pnrapilog2,quoteSummaryObject,goalDetails);

                 resultLog.ExportToSqlServerDB(ds, test, "D50", retriveSmartPriceData);
            }
            catch (ShowStopperException e)
            {
                throw e;

            }
            catch (Exception e)
            {
                ShowStopperException exec = new ShowStopperException("Exception Occured in D50", e);
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
        
            

      
