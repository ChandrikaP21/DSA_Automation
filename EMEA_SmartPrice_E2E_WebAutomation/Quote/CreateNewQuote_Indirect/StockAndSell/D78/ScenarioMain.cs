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

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.StockAndSell.D78
{
  public class ScenarioMain: Quote.CreateNewQuote_Direct.D33.ScenarioMain
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

        public void ExecuteTestcases(TestCase test,string scenarioId,int testCaseNumber, string country = null, List<string> application = null)
        {


                    WebDriver = new ChromeDriver();
                    DSAPageObject dsa = new DSAPageObject(WebDriver);
                    TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                    CustomerClass customerClass = new CustomerClass();
                   
                    List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
                    Constant cs = new Constant(WebDriver);
                    ResultLogging resultLog = new ResultLogging();
                    Product productobj = new Product(WebDriver);
                     dsa.country = country;
                        try
                        {
                        
                        
                        LoadDSA();
                        CreateQuoteRetriveAndCompareCustomerDetails(dsa,test,customerClass,productobj);

                     Thread.Sleep(4000);
                     RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData,totalFinancialCalculation, dsa, resultLog, scenarioId, testCaseNumber, "1", customerClass, cs, test, true);

                     

                        }
                        catch (ShowStopperException ex)
                        {
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            //WebDriverUtils.TakeSnapShot(WebDriver);
                            throw new ShowStopperException("Unexpected error occured in D78", ex);
                            //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
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
                Thread.Sleep(4000);
                //WebDriver.Navigate().Refresh();
                //Thread.Sleep(6000);

                OracleDBConnection odb = new OracleDBConnection();
                CustomerPage customerPage = new CustomerPage(WebDriver);
                if(IsChangeCustomer==false)
                customerObject1.OldAccountCustomerId = IdentifyCustomerAndCreateQuote(test.EndCustomerId,test.IsCustomerIdentificationQueryBased, customerClass.OldAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData, customerObject1, dsa.PnrLog, true,dsa.country);
                else
                    customerObject1.OldAccountCustomerId = IdentifyCustomerAndCreateQuote(test.EndCustomerId, test.IsNewCustomerIdentificationQueryBased, customerClass.OldAccountCustomerId, test.NewCustomerIdentificationQuery, test.NewCustomerIdentificationData, customerObject1, dsa.PnrLog, true,dsa.country);

                if (IsReSell == true)
                {
                    dsa.EnterEndCustomerId(customerObject1.OldEndUserAccountCustomerId);
                   sfdcDealIdCheck.ClickSFDCField();

                    Thread.Sleep(5000);
                }
                Constant cs = new Constant(WebDriver);
                CustomerPage customerPage1 = new CustomerPage(WebDriver);
                customerPage1.ProvideResSellerException();
                if (test.IsCustomerIdentificationQueryBased == "N")
                {
                    dsa.PnrLog.SFDCDEALID = test.SFDCID;
                }
                
                if (!string.IsNullOrEmpty(dsa.PnrLog.SFDCDEALID))
                {
                    sfdcDealIdCheck.EnterSFDCField(dsa.PnrLog.SFDCDEALID);
                    cs.jse.ExecuteScript("window.scrollBy(0,-100)", sfdcDealIdCheck.SfdcDealIdElement);
                    WebDriver.ScrollIntoView(dsa.QuoteName);
                    dsa.ClickQuoteName();
                    new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(sfdcDealIdCheck.BySfdcDealIdElement));
                }
                Thread.Sleep(3000);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,dsa.country,LoadDeserializeObject());

                if(IscompareCustomerAndSfdc)
                {  DraftQuotePageObject draftquoteObject = new DraftQuotePageObject(WebDriver);
                    odb.RetriveEndUserInformation(draftquoteObject.DraftQuoteNumber, customerObject2);
                    customerPage.CompareCustomerDetails(customerObject1, customerObject2, customerClass);
                   
                     PNRAPILog pNRAPILog = new PNRAPILog();
                    Constant constant = new Constant(WebDriver);

                    //constant.ViewConfiguration(constant.Count).Click();
                    ClickOnProductConfiguration(constant.Count);
                    string productDescription = constant.ProductDescriptin(constant.Count).GetAttribute("innerText");
                    string skunumber = draftquoteObject.RetriveSkunumber(constant, 1, dsa, productDescription,constant.Count);//add additional count ... need to test
                    odb.RetriveSFDCDetails(draftquoteObject.DraftQuoteNumber, pNRAPILog);
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
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in CreateQuoteRetriveAndCompareCustomerDetails() in ScenarioMain Class", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
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
        public string IdentifyCustomerAndCreateQuote(string endUserAccountCustomerId,string isCustomerIdentificationQueryBased, string oldAccountCustomerId, string customerIdentificationQuery, string customerIdentificationData,CustomerClass customerClass,PNRAPILog pnrApilog,bool IsDirect=false,string country="us")
        {
            try
            {
                OracleDBConnection odb = new OracleDBConnection();
                DSAPageObject dSAPageObject = new DSAPageObject(WebDriver);
                dSAPageObject.ChangeCountry(country);
                if (isCustomerIdentificationQueryBased == "Y")
                {
                    odb.RetriveIndirectCustomerDetails(customerIdentificationQuery, customerClass, pnrApilog);
                    oldAccountCustomerId = customerClass.OldAccountCustomerId;
                }
                else
                {
                    oldAccountCustomerId = customerIdentificationData;
                    customerClass.OldEndUserAccountCustomerId = endUserAccountCustomerId;


                }
                dSAPageObject.CreateNewDSAQuote(oldAccountCustomerId, country);
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new ShowStopperException(" Error Occured in IdentifyCustomerAndCreateQuote() in ScenarioMain Class", ex);
            }
            return oldAccountCustomerId;


        }
    }
            
        }
        
            

      
