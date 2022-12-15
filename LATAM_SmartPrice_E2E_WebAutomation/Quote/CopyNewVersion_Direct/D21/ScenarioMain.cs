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

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D21

{
    public class ScenarioMain : D17.ScenarioMain
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




        public void ExecuteTestcases(TestCase test, string ScenarioID,int testCaseNumber)
        {
            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject();
            Constant cs = new Constant(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
         
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            ResultLogging resultLog = new ResultLogging();
            try
            {

               
                //   ScenarioMain mainScenario = new ScenarioMain(Constant.URL_Prod);
                LoadDSA();
                if (test.IsBaseQuoteIdentificationQueryBased == "Y")
                {
                    odb.RetriveServiceTypeOneQuote(ds, test.BaseQuoteIdentificationQUERY);
                    SearchQuote(ds,cs);
                }
                else
                {
                    ds.ActualQuote = test.BaseQuoteIdentificationDATA;
                    ds.OrderCode = test.ProductIdentificationData;
                    SearchQuote(ds,cs);

                }
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D05.ScenarioMain d05 = new CopyNewQuote_Direct.D05.ScenarioMain();
                RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
                bool IsOfferingId = true;

                switch (test.UpdateServices)
                {

                    case "ADD":
                        if (test.ModuleId == "")
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
                       
                        break;
                        //case "DELETE":
                        //    foreach (TestData_Module module in test.modules)
                        //    {
                        //        if (test.ProductIdentificationData != null)
                        //        {

                        //            DeleteService(cs, module, ds);
                        //            Thread.Sleep(15000);

                        //            cs.CurrentQuote.Click();
                        //            Thread.Sleep(20000);
                        //        }
                        //    }
                        //    break;
                }


                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, ScenarioID, ds, ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioID,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioID,retriveSmartPriceData);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null,retriveSmartPriceData);

            }
        }
        




    }
    
}




