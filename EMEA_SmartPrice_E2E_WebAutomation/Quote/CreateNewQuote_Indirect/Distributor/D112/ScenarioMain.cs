using System;

using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;

using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;

using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;

namespace EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.Distributor.D112
{
    public class ScenarioMain : ReSell.D91.ScenarioMain
    {
        public string TestURL;
        public ScenarioMain(string URL) : base(URL)
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
      
                Thread.Sleep(5000);
            
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void ExecuteTestcases(TestCase test,string scenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
            
            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            CustomerClass customerClass = new CustomerClass();
            
            
            ResultLogging resultLog = new ResultLogging();
            Product productobject = new Product(WebDriver);
            ds.country = country;
            try
            {

                
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject,true,false,true);
               
                ds.QuoteNumber = ds.draftquotnumber.Text;
                string[] str = ds.QuoteNumber.Split('.');
                string withOutVersionQuoteNo = str[0];
                EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D10.ScenarioMain d10scenario= new CopyNewQuote_Direct.D10.ScenarioMain();
                Thread.Sleep(3000);
                d10scenario.UpdateCustomer(retriveSmartPriceData,withOutVersionQuoteNo, totalFinancialCalculation, ds, resultLog, scenarioId, test.IsCustomerInSameSegment, test.IsCustomerIdentificationQueryBased, testCaseNumber, test.CustomerIdentificationQuery, test.CustomerIdentificationData, test.CrossSegmenId,customerClass,test,true);


                resultLog.ExportToSqlServerDB(ds, test, scenarioId, retriveSmartPriceData);

            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D112", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, null, resultLog, test);
            }
        }

       

       
        
       
    }
}
        
            

      
