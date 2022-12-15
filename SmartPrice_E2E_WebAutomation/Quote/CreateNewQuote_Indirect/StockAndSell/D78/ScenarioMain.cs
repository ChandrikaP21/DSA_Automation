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

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D78
{
  public class ScenarioMain:CreateNewQuote_Direct.D33.ScenarioMain
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

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber)
        {


                    WebDriver = new ChromeDriver();
                    DSAPageObject dsa = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerClass customerClass = new CustomerClass();
                   
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    Product productobj = new Product(WebDriver);
                    try
                     {
                        
                        
                        LoadDSA();
                        CreateQuoteRetriveAndCompareCustomerDetails(dsa,test,customerClass,productobj);

                     Thread.Sleep(4000);
                     RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, scenarioId, testCaseNumber, RunId, "1", customerClass, cs, test, true);
                  
                        resultLog.ExportToSqlServerDB(dsa, test, scenarioId,retriveSmartPriceData);
                        resultLog.ExportToSummaryTable(test, scenarioId);
                        Console.WriteLine(test.TestCaseID);

                     }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        
                    }
                    finally
                    {
                        CustomGarbageCollector customeGc = new CustomGarbageCollector();
                        customeGc.NullifyUnusedObject(WebDriver, null,dsa, totalFinancialCalculation, cs, resultLog, test);
                    }
        }

         
        public DSAPageObject CreateQuoteRetriveAndCompareCustomerDetails(DSAPageObject dsa,TestCase test, CustomerClass customerClass, Product productObject,bool IsReSell=false,bool IscompareCustomerAndSfdc=true,bool IsChangeCustomer=false)
        {
            try
            {
                CustomerClass customerObject1 = new CustomerClass();
                CustomerClass customerObject2 = new CustomerClass();
                SFDCChangeClass sfdcDealIdCheck = new SFDCChangeClass(WebDriver);
                //DSAPageObject dsa = new DSAPageObject();


                OracleDBConnection odb = new OracleDBConnection();
                CustomerPage customerPage = new CustomerPage(WebDriver);
                if(IsChangeCustomer==false)
                customerObject1.OldAccountCustomerId = IdentifyCustomerAndCreateQuote(test.EndCustomerId,test.IsCustomerIdentificationQueryBased, customerClass.OldAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData, customerObject1, dsa.PnrLog, true);
                else
                    customerObject1.OldAccountCustomerId = IdentifyCustomerAndCreateQuote(test.EndCustomerId, test.IsNewCustomerIdentificationQueryBased, customerClass.OldAccountCustomerId, test.NewCustomerIdentificationQuery, test.NewCustomerIdentificationData, customerObject1, dsa.PnrLog, true);

                if (IsReSell == true)
                {

                    dsa.EndCustomerDetails.SendKeys(customerObject1.OldEndUserAccountCustomerId);
                    sfdcDealIdCheck.SfdcDealIdElement.Click();
                    Thread.Sleep(5000);
                }
                if (test.IsCustomerIdentificationQueryBased == "N")
                {
                    dsa.PnrLog.SFDCDEALID = test.SFDCID;
                }
                Constant cs = new Constant(WebDriver);
                if (!string.IsNullOrEmpty(dsa.PnrLog.SFDCDEALID))
                {
                    sfdcDealIdCheck.SfdcDealIdElement.SendKeys(dsa.PnrLog.SFDCDEALID);
                    cs.jse.ExecuteScript("window.scrollBy(0,80)", sfdcDealIdCheck.SfdcDealIdElement);
                    dsa.EndCustomerDetails.Click();
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(sfdcDealIdCheck.BySfdcDealIdElement));
                }
                Thread.Sleep(3000);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);

                if(IscompareCustomerAndSfdc)
                {
                    DraftQuotePageObject draftquoteObject = new DraftQuotePageObject(WebDriver);
                    odb.RetriveEndUserInformation(draftquoteObject.DraftQuoteNumber, customerObject2);
                    customerPage.CompareCustomerDetails(customerObject1, customerObject2, customerClass);
                   
                    PNRAPILog pNRAPILog = new PNRAPILog();
                    Constant constant = new Constant(WebDriver);
                    constant.ViewMore(constant.Count).Click();
                    string productDescription = constant.ProductDescriptin(constant.Count).GetAttribute("innerText");
                    string skunumber = draftquoteObject.RetriveSkunumber(constant, 1, dsa, productDescription,constant.Count);//add additional count ... need to test
                    odb.RetriveSFDCDetails(draftquoteObject.DraftQuoteNumber, pNRAPILog, skunumber);
                    dsa.PnrLog.CompareSFDCDetails(dsa.PnrLog, pNRAPILog);
                }
                else
                {
                    customerClass.OldAccountCustomerId = customerObject1.OldAccountCustomerId;
                    customerClass.OldAccountId = customerObject1.OldAccountId;
                    customerClass.OldEndUserAccountCustomerId = customerObject1.OldEndUserAccountCustomerId;
                    customerClass.OldEndUserAccountId = customerClass.OldEndUserAccountId;
                    customerClass.OldQuoteAccountId = customerClass.OldQuoteAccountId;
                }

                

               
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dsa;
        }

        public string IdentifyOrderCodeUsingQuery(string isIdentifiedByQuery,string productIdentificationQuery,string productIdentificationData)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dsa = new DSAPageObject(WebDriver);
            string orderCode = "";
            string[] text = dsa.LocalChannelIdElement.Text.Split('-');
            dsa.LocalChannelId = text[0].Trim();
            if (isIdentifiedByQuery == "Y")
            {
                orderCode = odb.RetriveOrderCode(productIdentificationQuery, dsa.LocalChannelId);
            }
            else
            {
                orderCode = productIdentificationData;

            }
            return orderCode;
        }

        public string IdentifyCustomerAndCreateQuote(string isCustomerIdentificationQueryBased,string oldAccountCustomerId, string customerIdentificationQuery, string customerIdentificationData)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dSAPageObject = new DSAPageObject();
            if (isCustomerIdentificationQueryBased == "Y")
            {
                oldAccountCustomerId = odb.RetriveEnduserAccountCustomerIdWithoutQuoteNumber(customerIdentificationQuery);
            }
            else
            {
                oldAccountCustomerId = customerIdentificationData;
            }
            dSAPageObject.CreateNewDSAQuote(oldAccountCustomerId);
            return oldAccountCustomerId;


        }
        public string IdentifyCustomerAndCreateQuote(string endUserAccountCustomerId,string isCustomerIdentificationQueryBased, string oldAccountCustomerId, string customerIdentificationQuery, string customerIdentificationData,CustomerClass customerClass,PNRAPILog pnrApilog,bool IsDirect=false)
        {
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject dSAPageObject = new DSAPageObject(WebDriver);
            if (isCustomerIdentificationQueryBased == "Y")
            {
               odb.RetriveIndirectCustomerDetails(customerIdentificationQuery,customerClass,pnrApilog);
                oldAccountCustomerId = customerClass.OldAccountCustomerId;
            }
            else
            {
                oldAccountCustomerId = customerIdentificationData;
                customerClass.OldEndUserAccountCustomerId = endUserAccountCustomerId;

                
            }
            dSAPageObject.CreateNewDSAQuote(oldAccountCustomerId);
            return oldAccountCustomerId;


        }
    }
            
        }
        
            

      
