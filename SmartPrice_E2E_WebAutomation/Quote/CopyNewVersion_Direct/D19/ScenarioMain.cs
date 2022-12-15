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
using SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.IO;
using SmartPrice_E2E_WebAutomation.Utilities;

namespace SmartPrice_E2E_WebAutomation.Quote.CopyNewVersion_Direct.D19
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



        public void ExecuteTestcases(TestCase test, string ScenarioId,int testCaseNumber)
        {

            WebDriver = new ChromeDriver();
            OracleDBConnection odb = new OracleDBConnection();
            DSAPageObject ds = new DSAPageObject(WebDriver);
            Constant cs = new Constant(WebDriver);
            List<DSAPageObject> retriveSmartPriceData = new List<DSAPageObject>();
            TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
            SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03upgradeComponent = new CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain();
            ResultLogging resultLog = new ResultLogging();
            try
            {
                

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

                    ds.PnrLog.ServiceType = "1";
                }

                if (!cs.IsElementPresent(cs.ByDraftQuoteNumber))
                {
                    CopyAndCreateNewVersion();
                }
                

                if (string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//without module id and with out component ID
                {
                    d03upgradeComponent.UpGradeComponent(cs,ds,WebDriver);

                }
                if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.ComponentId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d03upgradeComponent.UpGradeComponent(cs, test, ds, WebDriver);

                    }
                    else
                    {
                        //Console.WriteLine("Product is not available , Services  can not be added.");
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);
                    }
                }

                if (!string.IsNullOrEmpty(test.ModuleId) && !string.IsNullOrEmpty(test.ComponentId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d03upgradeComponent.UpGradeComponent(cs, test.ModuleId, test.ComponentId, ds,WebDriver);

                    }
                    else
                    {

                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details("Product is not available , Services  can not be added.", true);


                    }
                }



                RetriveDataFromDSAScreen(retriveSmartPriceData, totalFinancialCalculation, ScenarioId, ds, ds.ServiceHeaderText, ds.ServiceModules, null);
                SmartPriceCalculation(retriveSmartPriceData, totalFinancialCalculation);
                CompareSmartPrice(retriveSmartPriceData,totalFinancialCalculation, ds.ServiceModules,ScenarioId);
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceData, totalFinancialCalculation,ScenarioId,testCaseNumber,RunId);
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
                customeGc.NullifyUnusedObject(WebDriver, odb, ds, totalFinancialCalculation, cs, resultLog, test,null ,retriveSmartPriceData);
               
            }

        }
      
    }
}
    

                                         
    
  
        
            

      
