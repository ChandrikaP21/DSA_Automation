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
using OpenQA.Selenium.Chrome;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D25
{
    public class ScenarioMain : D17.ScenarioMain
    {
        public string TestURL;
        public ScenarioMain(string URL) : base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL,IWebDriver driver) : base(URL,driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }

        
        public void ExecuteTestcases(TestCase test,string ScenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
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
                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                
                productObject.DecreaseQuantityforMultipleScenario(test, cs);
                RetriveDataFromDSAScreen(retriveSmartPriceData,totalFinancialCalculation, null,ds);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation);
                
                resultLog.GenerateReportInCSV(ds,retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
                resultLog.ExportToSqlServerDB(ds,test,ScenarioId,retriveSmartPriceData);
                resultLog.ExportToSummaryTable(test, ScenarioId);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,productObject, retriveSmartPriceData);
            }
        }

        private void DecreaseQuantity(TestCase testCase, Constant constant)
        {
            int i = 0;
            
                //for anyproducts....
                if (testCase.QuantityChangeType == QuantityChangeType.AnyProduct)
                {
                if (Convert.ToInt32(constant.IncreaseQuantity(i).GetAttribute("value")) - testCase.Quantity > 0)
                {
                    try
                    {
                        constant.IncreaseQuantity(i).Clear();
                        constant.IncreaseQuantity(i).SendKeys(testCase.Quantity.ToString());
                        try
                        {
                            constant.QuantityLabel.Click();
                            Console.WriteLine(" Quantity is changed:"+ QuantityChangeType.AnyProduct);
                        }
                        catch (Exception ex)
                        {

                        }
                        Thread.Sleep(10000);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
               

                }
                //For specific product..1st Product...
                if (testCase.QuantityChangeType == QuantityChangeType.ForSpecificProduct)
                {
                try
                {

                    if (constant.OrderCode(testCase).Enabled)
                    {
                        if (Convert.ToInt32(constant.QuantitySpecificOrder(testCase).GetAttribute("value")) - testCase.Quantity > 0)
                        {
                            {
                                constant.QuantitySpecificOrder(testCase).Clear();
                                constant.QuantitySpecificOrder(testCase).SendKeys(testCase.Quantity.ToString());

                                try
                                {
                                    constant.QuantityLabel.Click();
                                    
                                }
                                catch (Exception ex)
                                {

                                }
                                Thread.Sleep(10000);
                                Console.WriteLine(" Quantity is changed:" + QuantityChangeType.ForSpecificProduct);
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Order Code is not present :Error-", ex.Message);
                }



                }


            ////For All products...
            if (testCase.QuantityChangeType == QuantityChangeType.AllProducts)
            {

                if (constant.ApplyChangesItem.Text == "Standard")
                {
                    // Actions action = new Actions(WebDriver);

                    //     constant.ApplyChangesItem.Click();//Change manual to standard or standard to  manual.
                    constant.jse.ExecuteScript("arguments[0].click()", constant.SelectManual);
                }

                ChangeQuantityForAllProduct(constant, testCase);

                //constant.IncreaseQuantity.Clear();
                //constant.IncreaseQuantity.SendKeys(testCase.QuantityChangeType.ToString());
           //     new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(constant.ByDellIcon));
                Thread.Sleep(10000);
                Console.WriteLine("Quantity is decreased for " + testCase.QuantityChangeType + "........");


            }
            
            
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

                           
    




