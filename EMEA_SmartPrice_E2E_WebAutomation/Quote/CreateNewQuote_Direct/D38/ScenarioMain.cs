﻿using System;
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
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Pages;
using System.IO;

using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Utilities;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.ModuleFlow.GOAL;

namespace  EMEA_SmartPrice_E2E_WebAutomation.Quote.CreateNewQuote_Direct.D38
{
   public  class ScenarioMain:D34.ScenarioMain
    {
        public string TestURL;
       // public static List<DSAPageObject> baseProductQuotes = new List<DSAPageObject>();
        public ScenarioMain(string URL ):base(URL)
        {
            TestURL = URL;

        }
        public ScenarioMain(string URL, IWebDriver driver) : base(URL, driver)
        {
            TestURL = URL;
            WebDriver = driver;

        }
        public ScenarioMain():base()
        {
        }

       
        
        
        public void ExecuteTestcases(TestCase test,string ScenarioId, int testCaseNumber, string country = null, List<string> application = null)
        {
           
                      ;
                    DSAPageObject ds = new DSAPageObject();
                     Constant cs = new Constant(WebDriver);
                     
                     TotalFinancialCalculation totalFinancialCalculation = new TotalFinancialCalculation();
                     Product productObject = new Product(WebDriver);
                     CustomerClass customerClass = new CustomerClass();
                     WebDriver = new ChromeDriver();
                ResultLogging resultLog = new ResultLogging();
            List<DSAPageObject> retriveSmartPriceDetailsList = new List<DSAPageObject>();
            try
            {
                        
               
                       
                LoadDSA();

                IdentifyCustomerAndCreateQuote(test.IsCustomerIdentificationQueryBased, customerClass.OldEndUserAccountCustomerId, test.CustomerIdentificationQuery, test.CustomerIdentificationData,country);
                productObject.OrderCode = IdentifyOrderCodeUsingQuery(test.IsIdentifiedByQuery, test.ProductIdentificationQuery, test.ProductIdentificationData);
                productObject.AddProduct(productObject.OrderCode, test.SystemSearch, test.SAndPSearch,country, LoadDeserializeObject());
        // RemoveAllProductsExceptOneWithBaseService(cs, ds.OrderCode);
        EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain d06UpgradeService = new EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D06.ScenarioMain();

        if (!string.IsNullOrEmpty(test.ModuleId) && string.IsNullOrEmpty(test.OfferingId))//With Module ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        d06UpgradeService.UpGradeServices(cs, test, ds);

                    }
                    else
                    {
                        Console.WriteLine("Product is not available , Services  can not be added.");

                    }
                }
        //Module ID with Service id is not applicable for now ..
                /*  if (!string.IsNullOrEmpty(test.ModuleId)&& !string.IsNullOrEmpty(test.OfferingId))// With Module ID and Service ID
                {
                    if (test.ProductIdentificationData != null)
                    {
                        UpGradeServices(cs, test.ModuleId,test.OfferingId, ds);

                    }
                    else
                    {

                        Console.WriteLine("Product is not available , Services can not be added.");


                    }
                }*/



                RetriveDataFromDSAScreen(retriveSmartPriceDetailsList,totalFinancialCalculation, ScenarioId, ds,ds.ServiceHeaderText, ds.ServiceModules);
                SmartPriceCalculation(retriveSmartPriceDetailsList, totalFinancialCalculation);
                CompareSmartPrice(totalFinancialCalculation,retriveSmartPriceDetailsList);
                if (StaticBriefCase.ApplicationType.Contains("GoalLite"))
                {
                    DSAQuoteSummaryPage dsaQuoteSummaryPage = new DSAQuoteSummaryPage(WebDriver);
                    SubmitGoalLite(test, ds, ds.country, dsaQuoteSummaryPage);

                }
                if (StaticBriefCase.ApplicationType.Contains("Goal"))
                {
                    SubmitAndApproveGoalRequest(test.TestCaseTitle, ScenarioId, test.ApprovalType, quoteSummaryObject, test);
                }
                resultLog.GenerateReportInCSV(ds, retriveSmartPriceDetailsList, totalFinancialCalculation,ScenarioId,testCaseNumber,quoteSummaryObject,goalDetails);
                resultLog.ExportToSqlServerDB(ds, test, ScenarioId, retriveSmartPriceDetailsList);


                //WebDriver.Close();
            }
            catch (ShowStopperException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                //WebDriverUtils.TakeSnapShot(WebDriver);
                throw new ShowStopperException("Unexpected error occured in D38", ex);
                //Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
            }
            finally
            {
                CustomGarbageCollector customeGc = new CustomGarbageCollector();
                customeGc.NullifyUnusedObject(WebDriver, null, ds, totalFinancialCalculation, cs, resultLog, test);
            }
        }
       
       
    }
  }
        
            

      
