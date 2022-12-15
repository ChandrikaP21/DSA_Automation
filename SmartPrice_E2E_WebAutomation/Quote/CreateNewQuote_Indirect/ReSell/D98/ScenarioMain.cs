using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Quote;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Reports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Utilities;
using OpenQA.Selenium.Chrome;

namespace SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Indirect.ReSell.D98
{
    public class ScenarioMain : StockAndSell.D78.ScenarioMain
    {
        public string TestURL;
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }

        
        /// <summary>
        /// Decrease the quantity by given input.If  quantity >1 then decrease else increase the quantity by 10 then decrease by given input.
        /// </summary>
        /// <param name="test"></param>
        /// <param name="ScenarioId"></param>
        /// <param name="testCaseNumber"></param>
        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
           
            CustomerClass customerClass = new CustomerClass();
            Constant cs = new Constant(WebDriver);
            ResultLogging resultLog = new ResultLogging();
            bool isDirect = true;
            try
            {
                
                
                LoadDSA();
                Product productobject = new Product(WebDriver);
                LoadDSA();
                CreateQuoteRetriveAndCompareCustomerDetails(ds,test, customerClass, productobject,true);


                DecreaseQuantity(retriveSmartPriceData,test, cs,totalFinancialCalculation,ScenarioId,ds,resultLog,testCaseNumber,customerClass,true);

               
                resultLog.ExportToSqlServerDB(ds,test,ScenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
                Console.WriteLine(/*tes.ScenarioID + " " +*/ test.TestCaseID);
               

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }

        private void DecreaseQuantity(List<DSAPageObject> retriveSmartPriceData, TestCase testCase, Constant constant,TotalFinancialCalculation totalFinancialCalculation,string ScenarioId,DSAPageObject dsa,ResultLogging resultLog,int testCaseNumber,CustomerClass customerClass,bool isDirect=false)
        {
           
            Product productObj = new Product(WebDriver);
            Constant cs = new Constant(WebDriver);
            string runId = RunId;
           
                //if (Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value")) - testCase.Quantity > 0)
                //  {
                 
                //    DecreaseQuantityForSingleProduct(testCase.Quantity, constant.IncreaseQuantity(i), constant.QuantityLabel, QuantityChangeType.AnyProduct);
                //    RefreshGuidanceAndGenerateReport(totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId, "1", null);
                // }
                //else
                
                productObj.IncreaseQuantity(testCase,cs);

                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData, totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId,"1", customerClass,constant,testCase,isDirect);
                 constant.jse.ExecuteScript("window.scrollBy(0,-200)");
                 productObj.DecreaseQuantityForSingleProduct(testCase.Quantity,QuantityChangeType.AnyProduct);
                RefreshGuidanceCompareAndGenerateReport(retriveSmartPriceData, totalFinancialCalculation, dsa, resultLog, ScenarioId, testCaseNumber, runId,"2", customerClass, constant,testCase,isDirect);
            
                
        }
        
        private void ChangeQuantityForAllProduct(Constant constant, TestCase testCase)
        {
            try
            {


                while (constant.Count < constant.NoOfProducts.Count)
                {
                    if (Convert.ToInt32(constant.IncreaseQuantity(constant.Count).GetAttribute("value")) - testCase.Quantity > 0)
                    {
                        constant.IncreaseQuantity(constant.Count).Clear();
                        //constant.IncreaseQuantity(constant.Count).Clear();
                        constant.IncreaseQuantity(constant.Count).SendKeys(testCase.Quantity.ToString());
                        // constant.QuantityLabel.Click();
                        Thread.Sleep(5000);

                    }
                    else
                    {
                        Console.WriteLine("Quantity can not be decreased for "+constant.Count+" product ");
                    }
                                        
                    constant.Count++;
                }
                Actions actions = new Actions(WebDriver);
                actions.MoveToElement(constant.ApplyChanges);
                actions.Click().Build().Perform();
                Thread.Sleep(5000);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

                           
    




