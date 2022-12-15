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
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare;
using Guidance = SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare.Guidance;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D42
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

        //Constant cs = new Constant();
        //PNRAPILogCompare apiCompareObject = new PNRAPILogCompare();
        //RequestParams oldRequestParams = new RequestParams();
        //SalesContext oldSalesContext = new SalesContext();
        //Guidance oldGuidance = new Guidance();
        //List<PNRAPILogCompare> apiCompareObjectCollection = new List<PNRAPILogCompare>();
        public void ChangeCustomer(Constant locator,CustomerPage customerPageObj,CustomerClass customerClass)
        {
            
            try
            {
           //     customerClass.CustomerName=customerPageObj.GetCustomerName.Text;
                locator.MenuButton.Click();
                Thread.Sleep(3000);
                customerPageObj.ClickOnCustomerLink.Click();
                Thread.Sleep(3000);
                customerPageObj.SearchCustomerID_DCN.SendKeys(customerClass.NewEndUserAccountCustomerId);
                customerPageObj.FindCustomerButton.Click();
                Thread.Sleep(2000);
                customerPageObj.UseInQuote.Click();
                customerPageObj.ChangeCustomerPopUpButton.Click();
         //       string changedCustomerName= customerPageObj.GetCustomerName.Text;
                Thread.Sleep(5000);
                //if (changedCustomerName!= customerClass.CustomerName)
                //{
                //    Console.WriteLine("Customer Name has changed:"+changedCustomerName);
                //}
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            
            Product productObject = new Product(WebDriver);
           
            List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            try
            {

                
                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsNewCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.NewCustomerIdentificationQuery, test.NewCustomerIdentificationData);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch);
                ds.QuoteNumber = ds.draftquotnumber.Text;
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10.ScenarioMain d10scenario= new CopyNewQuote_Direct.D10.ScenarioMain();
                d10scenario.UpdateCustomer(retriveSmartPriceDetailsList,withOutVersionQuoteNo, totalFinancialCalculation, ds, resultLog, scenarioId, test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber, test.CustomerIdentificationQuery, test.CustomerIdentificationData, test.CrossSegmenId,customerClass);

                
                resultLog.ExportToSqlServerDB(ds,test, scenarioId, retriveSmartPriceDetailsList);
                

                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver,null, ds, totalFinancialCalculation, null, null, test);
            }
        }

       

       
        
       
    }
}
        
            

      
