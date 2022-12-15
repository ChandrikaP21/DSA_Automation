using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.Quote;
using EMEA_SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D01;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using System.Configuration;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation
{
    class SmartPriceMain : DriverHelper
    {
        

        //static SmartPriceMain()
        //{
        //    WebDriver = new ChromeDriver();

        //}
        //public SmartPriceMain()
        //{
        //    Driver = new ChromeDriver();

        //}
        public static TimeSpan TimeElapse(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan totalTimeElapse = startTime - endTime;
            return totalTimeElapse;
        }
        public static TimeSpan StartTime => DateTime.Now.TimeOfDay;
        public static TimeSpan EndTime => DateTime.Now.TimeOfDay;
        static void Main(string[] args)
        {
            Bedrock.Utilities.Console_PresentationLayer.Report_RunStatus("InProgress", true);
            DSAPageObject.runDate = DateTime.Now.TimeOfDay;

            TestDataReader reader = new TestDataReader();
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress("Loading Test Scenarios", true);
            try
            {
                reader.LoadXmlElements();



                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Test Scenarios Load Completed.Total {0} Scenarios loaded", TestDataReader._scenarios.Count), true);



                SmartPriceMain smartPriceMain = new SmartPriceMain();
                string runId = "";

                //string url = URLHelper.GetDSAUrl();
                foreach (var scenario in TestDataReader._scenarios)

                {
                    try
                    {
                        //ExecuteTestCases(scenario.ScenarioID,scenario.TestCases);
                        runId = DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace("/", "").Replace(" ", "").Replace("AM", "").Replace("PM", "");
                      StaticBriefCase.CsvReportName= string.Format("{0}{1}{2}{3}{4}{5}", ConfigurationManager.AppSettings["CsvReportPath"].ToString(), "\\", "R_" + TestDataReader.CountryToTest[0] + "_" + scenario.ScenarioID + "_", runId, "\\", scenario.ScenarioID + "_" + runId);
                        StaticBriefCase.ApplicationType = TestDataReader.ApplicationsToTest;
                        switch (scenario.ScenarioID)
                        {

                            case "D01":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver,scenario.ScenarioID);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D01" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D01" + ex.StackTrace);
                                }
                                break;

                            case "D02":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain d02scenarioMain = new Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver,scenario.ScenarioID);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d02scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D02" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D02" + ex.StackTrace);
                                }
                                break;
                            case "D03":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03scenarioMain = new Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d03scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D03" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D03" + ex.StackTrace);
                                }

                                break;
                            case "D04":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D04.ScenarioMain d04scenarioMain = new Quote.CopyNewQuote_Direct.D04.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d04scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D04" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D04" + ex.StackTrace);
                                }
                                

                                break;
                            case "D05":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D05.ScenarioMain d05scenarioMain = new Quote.CopyNewQuote_Direct.D05.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d05scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D05" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D05" + ex.StackTrace);
                                }
                                
                                break;
                            case "D06":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D06.ScenarioMain d06scenarioMain = new Quote.CopyNewQuote_Direct.D06.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d06scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D06" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D06" + ex.StackTrace);
                                }
                               
                                break;
                            case "D07":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D07.ScenarioMain d07scenarioMain = new Quote.CopyNewQuote_Direct.D07.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d07scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D07" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D07" + ex.StackTrace);
                                }
                                
                                break;
                            case "D08":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D08.ScenarioMain d08scenarioMain = new Quote.CopyNewQuote_Direct.D08.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d08scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D08" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D08" + ex.StackTrace);
                                }
                               
                                break;

                            case "D09":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D09.ScenarioMain d09scenarioMain = new Quote.CopyNewQuote_Direct.D09.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d09scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D09" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D09" + ex.StackTrace);
                                }

                               
                                break;
                            case "D10":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D10.ScenarioMain d10scenarioMain = new Quote.CopyNewQuote_Direct.D10.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d10scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D10" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D10" + ex.StackTrace);
                                }

                                
                                break;
                            case "D11":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11scenarioMain = new Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d11scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D11_D12" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D11_D12" + ex.StackTrace);
                                }

                               
                                break;
                            case "D13":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D13.ScenarioMain d13scenarioMain = new Quote.CopyNewQuote_Direct.D13.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d13scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D13" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D13" + ex.StackTrace);
                                }


                                
                                break;
                            case "D16":
                                try
                                {
                                    Quote.CopyNewQuote_Direct.D16.ScenarioMain d16scenarioMain = new Quote.CopyNewQuote_Direct.D16.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d16scenarioMain);


                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D16" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D16" + ex.StackTrace);
                                }
                                break;

                            case "D17":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D17.ScenarioMain d17scenarioMain = new Quote.CopyNewVersion_Direct.D17.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d17scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D17" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D17" + ex.StackTrace);
                                }
                                
                                break;

                            case "D18":

                                  try
                                {
                                    Quote.CopyNewVersion_Direct.D18.ScenarioMain d18scenarioMain = new Quote.CopyNewVersion_Direct.D18.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d18scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D18" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D18" + ex.StackTrace);
                                }
                                break;
                            case "D19":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D19.ScenarioMain d19scenarioMain = new Quote.CopyNewVersion_Direct.D19.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d19scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D19" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D19" + ex.StackTrace);
                                }
                                
                                break;
                            case "D20":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D20.ScenarioMain d20scenarioMain = new Quote.CopyNewVersion_Direct.D20.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d20scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D20" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D20" + ex.StackTrace);
                                }

                                
                                break;
                            case "D21":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D21.ScenarioMain d21scenarioMain = new Quote.CopyNewVersion_Direct.D21.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d21scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D21" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D21" + ex.StackTrace);
                                }


                               
                                break;
                            case "D22":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D22.ScenarioMain d22scenarioMain = new Quote.CopyNewVersion_Direct.D22.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d22scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D22" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D22" + ex.StackTrace);
                                }

                                
                                break;
                            case "D23":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D23.ScenarioMain d23scenarioMain = new Quote.CopyNewVersion_Direct.D23.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d23scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D23" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D23" + ex.StackTrace);
                                }
                                
                                break;
                            case "D24":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D24.ScenarioMain d24scenarioMain = new Quote.CopyNewVersion_Direct.D24.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d24scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D24" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D24" + ex.StackTrace);
                                }

                                
                                break;

                            case "D25":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D25.ScenarioMain d25scenarioMain = new Quote.CopyNewVersion_Direct.D25.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d25scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D24" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D24" + ex.StackTrace);
                                }

                                
                                break;
                            case "D26":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D26.ScenarioMain d26scenarioMain = new Quote.CopyNewVersion_Direct.D26.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d26scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D26" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D26" + ex.StackTrace);
                                }
                                
                                break;
                            case "D27_D28":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain d26scenarioMain = new Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d26scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D27_D28" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D27_D28" + ex.StackTrace);
                                }
                               
                                break;
                            case "D29":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D29.ScenarioMain d29scenarioMain = new Quote.CopyNewVersion_Direct.D29.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d29scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D27_D28" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D27_D28" + ex.StackTrace);
                                }

                                
                                break;
                            case "D32":
                                try
                                {
                                    Quote.CopyNewVersion_Direct.D32.ScenarioMain d32scenarioMain = new Quote.CopyNewVersion_Direct.D32.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d32scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D32" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D32" + ex.StackTrace);
                                }


                                break;
                               


                            case "D34":

                                try
                                {
                                    Quote.CreateNewQuote_Direct.D34.ScenarioMain d34scenarioMain = new Quote.CreateNewQuote_Direct.D34.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d34scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D34" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D34" + ex.StackTrace);
                                }
                                break;
                            case "D35":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D35.ScenarioMain d35scenarioMain = new Quote.CreateNewQuote_Direct.D35.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d35scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D35" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D35" + ex.StackTrace);
                                }
                               
                                break;
                            case "D36":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D36.ScenarioMain d36scenarioMain = new Quote.CreateNewQuote_Direct.D36.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d36scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D36" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D36" + ex.StackTrace);
                                }

                                break;
                                

                                
                            case "D37":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D37.ScenarioMain d37scenarioMain = new Quote.CreateNewQuote_Direct.D37.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d37scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D37" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D37" + ex.StackTrace);
                                }

                                break;
                                
                               
                            case "D38":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D38.ScenarioMain d38scenarioMain = new Quote.CreateNewQuote_Direct.D38.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d38scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D38" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D38" + ex.StackTrace);
                                }

                                break;
                               

                                
                            case "D39":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D39.ScenarioMain d39scenarioMain = new Quote.CreateNewQuote_Direct.D39.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d39scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D39" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D39" + ex.StackTrace);
                                }

                                break;
                               
                                
                            case "D40":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D40.ScenarioMain d40scenarioMain = new Quote.CreateNewQuote_Direct.D40.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d40scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D40" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D40" + ex.StackTrace);
                                }
                               

                                break;

                            case "D41":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D41.ScenarioMain d41scenarioMain = new Quote.CreateNewQuote_Direct.D41.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d41scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D41" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D41" + ex.StackTrace);
                                }
                                
                                break;
                            case "D42":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D42.ScenarioMain d42scenarioMain = new Quote.CreateNewQuote_Direct.D42.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d42scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D42" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D42" + ex.StackTrace);
                                }

                               
                                break;
                            case "D43-D44":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain D43_D44scenarioMain = new Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, D43_D44scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D43_D44" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D43_D44" + ex.StackTrace);
                                }
                               
                                break;
                            case "D45":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D45.ScenarioMain d45scenarioMain = new Quote.CreateNewQuote_Direct.D45.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d45scenarioMain);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D45" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D45" + ex.StackTrace);
                                }
                                

                                break;

                            case "D48":
                                try
                                {
                                    Quote.CreateNewQuote_Direct.D48.ScenarioMain d48scenarioMain = new Quote.CreateNewQuote_Direct.D48.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d48scenarioMain);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D48" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D48" + ex.StackTrace);
                                }

                               
                                break;

                            case "D49":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D49.ScenarioMain d49 = new Quote.CopyNewQuote_Indirect.D49.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d49);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D49" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D49" + ex.StackTrace);
                                }

                               
                                break;
                            case "D50":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain d50 = new Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d50);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D50" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D50" + ex.StackTrace);
                                }

                                break;
                            case "D51":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain d51 = new Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d51);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D51" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D51" + ex.StackTrace);
                                }

                               
                                break;
                            case "D52":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D52.ScenarioMain d52 = new Quote.CopyNewQuote_Indirect.D52.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d52);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D52" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D52" + ex.StackTrace);
                                }

                                break;
                            case "D53":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D53.ScenarioMain d53 = new Quote.CopyNewQuote_Indirect.D53.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d53);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D53" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D53" + ex.StackTrace);
                                }
                                

                                break;
                            case "D54":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D54.ScenarioMain d54 = new Quote.CopyNewQuote_Indirect.D54.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d54);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D54" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D54" + ex.StackTrace);
                                }

                               
                                break;
                            case "D55":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D55.ScenarioMain d55 = new Quote.CopyNewQuote_Indirect.D55.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d55);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D55" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D55" + ex.StackTrace);
                                }

                               
                                break;

                            case "D56":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D56.ScenarioMain d56 = new Quote.CopyNewQuote_Indirect.D56.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d56);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D56" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D56" + ex.StackTrace);
                                }

                                break;
                            case "D57":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D57.ScenarioMain d57 = new Quote.CopyNewQuote_Indirect.D57.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d57);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D57" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D57" + ex.StackTrace);
                                }

                               
                                break;
                            case "D58":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D58.ScenarioMain d58 = new Quote.CopyNewQuote_Indirect.D58.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d58);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D58" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D58" + ex.StackTrace);
                                }


                              
                                break;
                            case "D59_D60":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain d59_60 = new Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d59_60);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D59_D60" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D59_D60" + ex.StackTrace);
                                }

                                break;
                            case "D61":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D61.ScenarioMain d61 = new Quote.CopyNewQuote_Indirect.D61.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d61);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D61" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D61" + ex.StackTrace);
                                }

                                break;
                            case "D62":
                                try
                                {
                                    Quote.CopyNewQuote_Indirect.D62.ScenarioMain d62 = new Quote.CopyNewQuote_Indirect.D62.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d62);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D62" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D62" + ex.StackTrace);
                                }
                                
                                break;

                            case "D63":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D63.ScenarioMain d63 = new Quote.CopyNewVersion_Indirect.D63.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d63);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D63" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D63" + ex.StackTrace);
                                }

                               
                                break;
                            case "D64":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain d64 = new Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d64);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D64" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D64" + ex.StackTrace);
                                }

                               
                                break;

                            case "D65":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain d65 = new Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d65);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D65" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D65" + ex.StackTrace);
                                }

                               
                                break;

                            case "D66":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D66.ScenarioMain d66 = new Quote.CopyNewVersion_Indirect.D66.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d66);


                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D66" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D66" + ex.StackTrace);
                                }

                               
                                break;
                            case "D67":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D67.ScenarioMain d67 = new Quote.CopyNewVersion_Indirect.D67.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d67);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D67" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D67" + ex.StackTrace);
                                }
                               

                                break;
                            case "D68":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D68.ScenarioMain d68 = new Quote.CopyNewVersion_Indirect.D68.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d68);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D68" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D68" + ex.StackTrace);
                                }


                                break;
                               
                                
                            case "D69":
                                
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D69.ScenarioMain d69 = new Quote.CopyNewVersion_Indirect.D69.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d69);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D69" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D69" + ex.StackTrace);
                                }

                                break;

                            case "D70":
                                
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D70.ScenarioMain d70 = new Quote.CopyNewVersion_Indirect.D70.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d70);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D70" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D70" + ex.StackTrace);
                                }

                                break;
                            case "D71":
                                
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D71.ScenarioMain d71 = new Quote.CopyNewVersion_Indirect.D71.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d71);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D71" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D71" + ex.StackTrace);
                                }

                                break;
                            case "D72":
                               
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D72.ScenarioMain d72 = new Quote.CopyNewVersion_Indirect.D72.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d72);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D72" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D72" + ex.StackTrace);
                                }
                                break;
                            case "D73_D74":
                               
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain d73_74 = new Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d73_74);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D73_D74" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D73_D74" + ex.StackTrace);
                                }
                                break;
                            case "D75":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D75.ScenarioMain d75 = new Quote.CopyNewVersion_Indirect.D75.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d75);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D75" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D75" + ex.StackTrace);
                                }

                                break;

                            case "D76":
                                try
                                {
                                    Quote.CopyNewVersion_Indirect.D76.ScenarioMain d76 = new Quote.CopyNewVersion_Indirect.D76.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d76);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                    showStopperEx = null;
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D76" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D76" + ex.StackTrace);
                                }

                                break;
                            case "D77":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain d77 = new Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d77);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D77" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D77" + ex.StackTrace);
                                }
                                break;
                            case "D78":
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain d78 = new Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d78);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D78" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D78" + ex.StackTrace);
                                }
                                

                                break;

                            case "D79":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain d79 = new Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d79);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D79" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D79" + ex.StackTrace);
                                }
                                break;

                            case "D80":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain d80 = new Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d80);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D80" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D80" + ex.StackTrace);
                                }
                                break;
                            case "D81":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain d81 = new Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d81);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D81" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D81" + ex.StackTrace);
                                }
                                break;
                            case "D82":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain d82 = new Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d82);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D82" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D82" + ex.StackTrace);
                                }
                                break;
                            case "D83":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain d83 = new Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d83);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D83" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D83" + ex.StackTrace);
                                }
                                break;

                            case "D84":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain d84 = new Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d84);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D84" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D84" + ex.StackTrace);
                                }
                                break;
                            case "D85":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain d85 = new Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d85);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D85" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D85" + ex.StackTrace);
                                }
                                break;
                            case "D86":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain d86 = new Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d86);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D86" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D86" + ex.StackTrace);
                                }
                                break;
                            case "D87_D88":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain d87_88 = new Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d87_88);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D87_D88" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D87_D88" + ex.StackTrace);
                                }
                                break;
                            case "D89":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain d89 = new Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d89);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D89" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D89" + ex.StackTrace);
                                }
                                break;
                            case "D90":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain d90 = new Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d90);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D90" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D90" + ex.StackTrace);
                                }
                                break;
                            case "D91":
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain d91 = new Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d91);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D91" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D91" + ex.StackTrace);
                                }
                                break;

                            case "D92":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain d92 = new Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d92);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D92" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D92" + ex.StackTrace);
                                }
                                break;

                            case "D93":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain d93 = new Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d93);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D93" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D93" + ex.StackTrace);
                                }
                                break;
                                
                            case "D94":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain d94 = new Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d94);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D94" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D94" + ex.StackTrace);
                                }
                                break;
                            case "D95":
                              
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain d95 = new Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d95);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D95" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D95" + ex.StackTrace);
                                }
                                break;
                            case "D96":
                              
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain d96 = new Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d96);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D96" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D96" + ex.StackTrace);
                                }
                                break;

                            case "D97":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain d97 = new Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d97);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D97" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D97" + ex.StackTrace);
                                }
                                break;
                            case "D98":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain d98 = new Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d98);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D98" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D98" + ex.StackTrace);
                                }
                                break;
                            case "D99":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain d99 = new Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d99);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D99" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D99" + ex.StackTrace);
                                }
                                break;
                            case "D100_D101":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain d100 = new Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d100);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D100" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D100" + ex.StackTrace);
                                }
                                break;

                            case "D102":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain d102 = new Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d102);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D102" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D102" + ex.StackTrace);
                                }
                                break;
                            case "D103":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain d103 = new Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d103);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D103" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D103" + ex.StackTrace);
                                }
                                break;
                            case "D104":
                                
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain d104 = new Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d104);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D104" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D104" + ex.StackTrace);
                                }
                                break;
                            case "D105":
                              
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain d105 = new Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d105);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D105" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D105" + ex.StackTrace);
                                }
                                break;

                            case "D106":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain d106 = new Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d106);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D106" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D106" + ex.StackTrace);
                                }
                                break;

                            case "D107":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain d107 = new Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d107);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D107" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D107" + ex.StackTrace);
                                }
                                break;
                            case "D108":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain d108 = new Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d108);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D108" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D108" + ex.StackTrace);
                                }
                                break;
                            case "D109":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain d109 = new Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d109);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D109" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D109" + ex.StackTrace);
                                }
                                break;
                            case "D110":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain d110 = new Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d110);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D110" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D110" + ex.StackTrace);
                                }
                                break;

                            case "D111":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain d111 = new Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d111);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D111" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D111" + ex.StackTrace);
                                }
                                break;
                            case "D112":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain d112 = new Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d112);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D112" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D112" + ex.StackTrace);
                                }
                                break;
                            case "D113_D114":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain d113_d114 = new Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d113_d114);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D113_D114" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D113_D114" + ex.StackTrace);
                                }
                                break;

                            case "D115":
                                                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain d115 = new Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d115);
                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D115" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D115" + ex.StackTrace);
                                }
                                break;
                            case "D116":
                               
                                try
                                {
                                    Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain d116 = new Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                                    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d116);

                                }
                                catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperEx)
                                {
                                    showStopperEx.LogException();
                                }
                                catch (Exception ex)
                                {
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D116" + ex.Message);
                                    Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception block , exiting D116" + ex.StackTrace);
                                }
                                break;



                        }
                    }
                    catch (Exception ex)
                    {
                        Bedrock.ExceptionHandlingBlock.ShowStopperException exception = new Bedrock.ExceptionHandlingBlock.ShowStopperException(ex.Message, ex);

                        exception.LogException();

                    }



                }
            }
            catch (Bedrock.ExceptionHandlingBlock.ShowStopperException showStopperException)
            {
                showStopperException.LogException();
                showStopperException = null;
            }

            catch (Exception err)
            {
                Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception Block,Error found in LoadXMLEleemnts()" + err.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Hit Exception Block,Error found in LoadXmlElements()" + err.StackTrace);

            }
            finally
            {

            }

        }



        public void ExecuteTestCases(string scenarioId, List<TestCase> testCases, object scenarioMain)
        {


            TimeSpan d01startTime = DateTime.Now.TimeOfDay;
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Scenario {0} Test Execution Initiated.", scenarioId), true);
            int testCaseNumber = 01;

            foreach (var countryToTest in TestDataReader.CountryToTest)
            {
                foreach (TestCase test in testCases)
                {

                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Test Case {0} initiated.", test.TestCaseTitle), true);  //Test Case "Test case title" initiated.
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Test Case {0} initiated.", countryToTest), true);  

                    if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D01.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D01.ScenarioMain d01 = (Quote.CopyNewQuote_Direct.D01.ScenarioMain)scenarioMain;
                        d01.ExecuteTestCases(test,scenarioId, testCaseNumber,countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain d02 = (Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain)scenarioMain;
                        d02.ExecuteTestCases(test,scenarioId ,testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain))
                    {

                        Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03 = (Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain)scenarioMain;
                        d03.ExecuteTestcases(test, "D03", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                        
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D04.ScenarioMain))
                    {


                        Quote.CopyNewQuote_Direct.D04.ScenarioMain d04 = (Quote.CopyNewQuote_Direct.D04.ScenarioMain)scenarioMain;
                        d04.ExecuteTestcases(test, "D04", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);

                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D05.ScenarioMain))
                    {

                        Quote.CopyNewQuote_Direct.D05.ScenarioMain d05 = (Quote.CopyNewQuote_Direct.D05.ScenarioMain)scenarioMain;
                        d05.ExecuteTestcases(test, "D05", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);

                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D06.ScenarioMain))
                    {

                        Quote.CopyNewQuote_Direct.D06.ScenarioMain d06 = (Quote.CopyNewQuote_Direct.D06.ScenarioMain)scenarioMain;
                        d06.ExecuteTestcases(test, "D06", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);

                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D07.ScenarioMain))
                    {
                       
                            Quote.CopyNewQuote_Direct.D07.ScenarioMain d07 = (Quote.CopyNewQuote_Direct.D07.ScenarioMain)scenarioMain;
                            d07.ExecuteTestcases(test, "D07", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                        
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D08.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D08.ScenarioMain d08 = (Quote.CopyNewQuote_Direct.D08.ScenarioMain)scenarioMain;
                        d08.ExecuteTestcases(test, "D08", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D09.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D09.ScenarioMain d08 = (Quote.CopyNewQuote_Direct.D09.ScenarioMain)scenarioMain;
                        d08.ExecuteTestcases(test, "D09", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D10.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D10.ScenarioMain d10 = (Quote.CopyNewQuote_Direct.D10.ScenarioMain)scenarioMain;
                        d10.ExecuteTestcases(test, "D10", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11_d12 = (Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain)scenarioMain;
                        d11_d12.ExecuteTestcases(test, "d11_d12", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D13.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D13.ScenarioMain d13 = (Quote.CopyNewQuote_Direct.D13.ScenarioMain)scenarioMain;
                        d13.ExecuteTestcases(test, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }


                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D16.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Direct.D16.ScenarioMain d16 = (Quote.CopyNewQuote_Direct.D16.ScenarioMain)scenarioMain;
                        d16.ExecuteTestcases(test, "D16", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D17.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D17.ScenarioMain d17 = (Quote.CopyNewVersion_Direct.D17.ScenarioMain)scenarioMain;
                        d17.ExecuteTestcases(test, "D17", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D18.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D18.ScenarioMain d18 = (Quote.CopyNewVersion_Direct.D18.ScenarioMain)scenarioMain;
                        d18.ExecuteTestcases(test, "D18", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D19.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D19.ScenarioMain d19 = (Quote.CopyNewVersion_Direct.D19.ScenarioMain)scenarioMain;
                        d19.ExecuteTestcases(test, "D19", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D20.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D20.ScenarioMain d20 = (Quote.CopyNewVersion_Direct.D20.ScenarioMain)scenarioMain;
                        d20.ExecuteTestcases(test, "D20", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D21.ScenarioMain))
                    {
                        if (test.ModuleId != "" && string.IsNullOrEmpty(test.OfferingId))
                        {
                            Quote.CopyNewVersion_Direct.D21.ScenarioMain d21 = (Quote.CopyNewVersion_Direct.D21.ScenarioMain)scenarioMain;
                            d21.ExecuteTestcases(test, "D21", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                        }
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D22.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D22.ScenarioMain d22 = (Quote.CopyNewVersion_Direct.D22.ScenarioMain)scenarioMain;
                        d22.ExecuteTestcases(test, "D06", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D23.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D23.ScenarioMain d23 = (Quote.CopyNewVersion_Direct.D23.ScenarioMain)scenarioMain;
                        d23.ExecuteTestcases(test, "D23", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D24.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D24.ScenarioMain d24 = (Quote.CopyNewVersion_Direct.D24.ScenarioMain)scenarioMain;
                        d24.ExecuteTestcases(test, "D24", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D25.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D25.ScenarioMain d25 = (Quote.CopyNewVersion_Direct.D25.ScenarioMain)scenarioMain;
                        d25.ExecuteTestcases(test, "D25", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D26.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D26.ScenarioMain d26 = (Quote.CopyNewVersion_Direct.D26.ScenarioMain)scenarioMain;
                        d26.ExecuteTestcases(test, "D26", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain d27_28 = (Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain)scenarioMain;
                        d27_28.ExecuteTestcases(test, "D27_D28", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D29.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D29.ScenarioMain d29 = (Quote.CopyNewVersion_Direct.D29.ScenarioMain)scenarioMain;
                        d29.ExecuteTestcases(test, "D29", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }


                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D32.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Direct.D32.ScenarioMain d32 = (Quote.CopyNewVersion_Direct.D32.ScenarioMain)scenarioMain;
                        d32.ExecuteTestcases(test, "D32", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D33.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D33.ScenarioMain d33 = (Quote.CreateNewQuote_Direct.D33.ScenarioMain)scenarioMain;
                        d33.ExecuteTestCases(test, "D33", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D34.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D34.ScenarioMain d19 = (Quote.CreateNewQuote_Direct.D34.ScenarioMain)scenarioMain;
                        d19.ExecuteTestcases(test, "D34", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D35.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D35.ScenarioMain d35 = (Quote.CreateNewQuote_Direct.D35.ScenarioMain)scenarioMain;
                        d35.ExecuteTestcases(test, "D35", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D36.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D36.ScenarioMain d21 = (Quote.CreateNewQuote_Direct.D36.ScenarioMain)scenarioMain;
                        d21.ExecuteTestcases(test, "D36", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D37.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D37.ScenarioMain d37 = (Quote.CreateNewQuote_Direct.D37.ScenarioMain)scenarioMain;
                        d37.ExecuteTestcases(test, "D37", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D38.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D38.ScenarioMain d23 = (Quote.CreateNewQuote_Direct.D38.ScenarioMain)scenarioMain;
                        d23.ExecuteTestcases(test, "D38", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D39.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D39.ScenarioMain d39 = (Quote.CreateNewQuote_Direct.D39.ScenarioMain)scenarioMain;
                        d39.ExecuteTestcases(test, "D39", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D40.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D40.ScenarioMain d40 = (Quote.CreateNewQuote_Direct.D40.ScenarioMain)scenarioMain;
                        d40.ExecuteTestcases(test, "D40", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D41.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D41.ScenarioMain d26 = (Quote.CreateNewQuote_Direct.D41.ScenarioMain)scenarioMain;
                        d26.ExecuteTestcases(test, "D41", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D42.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D42.ScenarioMain d42 = (Quote.CreateNewQuote_Direct.D42.ScenarioMain)scenarioMain;
                        d42.ExecuteTestcases(test, "D42", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain d43_D44 = (Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain)scenarioMain;
                        d43_D44.ExecuteTestcases(test, "D43_D44", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }


                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D45.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D45.ScenarioMain d32 = (Quote.CreateNewQuote_Direct.D45.ScenarioMain)scenarioMain;
                        d32.ExecuteTestcases(test, "D45", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D48.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Direct.D48.ScenarioMain d48 = (Quote.CreateNewQuote_Direct.D48.ScenarioMain)scenarioMain;
                        d48.ExecuteTestcases(test, "D48", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D49.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D49.ScenarioMain d49 = (Quote.CopyNewQuote_Indirect.D49.ScenarioMain)scenarioMain;
                        d49.ExecuteTestcases(test, "D49", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain d34 = (Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain)scenarioMain;
                        d34.ExecuteTestCases(test, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain d51 = (Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain)scenarioMain;
                        d51.ExecuteTestcases(test, "D51", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D52.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D52.ScenarioMain d52 = (Quote.CopyNewQuote_Indirect.D52.ScenarioMain)scenarioMain;
                        d52.ExecuteTestcases(test, "D52", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D53.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D53.ScenarioMain d21 = (Quote.CopyNewQuote_Indirect.D53.ScenarioMain)scenarioMain;
                        d21.ExecuteTestcases(test, "D53", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D54.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D54.ScenarioMain d54 = (Quote.CopyNewQuote_Indirect.D54.ScenarioMain)scenarioMain;
                        d54.ExecuteTestcases(test, "D54", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D55.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D55.ScenarioMain d23 = (Quote.CopyNewQuote_Indirect.D55.ScenarioMain)scenarioMain;
                        d23.ExecuteTestcases(test, "D55", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D56.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D56.ScenarioMain d39 = (Quote.CopyNewQuote_Indirect.D56.ScenarioMain)scenarioMain;
                        d39.ExecuteTestcases(test, "D56", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D57.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D57.ScenarioMain d40 = (Quote.CopyNewQuote_Indirect.D57.ScenarioMain)scenarioMain;
                        d40.ExecuteTestcases(test, "D57", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D58.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D58.ScenarioMain d26 = (Quote.CopyNewQuote_Indirect.D58.ScenarioMain)scenarioMain;
                        d26.ExecuteTestcases(test, "D58", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain d59_60 = (Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain)scenarioMain;
                        d59_60.ExecuteTestcases(test, "D59_D60", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D61.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D61.ScenarioMain d61 = (Quote.CopyNewQuote_Indirect.D61.ScenarioMain)scenarioMain;
                        d61.ExecuteTestcases(test, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }


                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D62.ScenarioMain))
                    {
                        Quote.CopyNewQuote_Indirect.D62.ScenarioMain d62 = (Quote.CopyNewQuote_Indirect.D62.ScenarioMain)scenarioMain;
                        d62.ExecuteTestcases(test, "D62", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D63.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D63.ScenarioMain d63 = (Quote.CopyNewVersion_Indirect.D63.ScenarioMain)scenarioMain;
                        d63.ExecuteTestcases(test, "D63", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain d34 = (Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain)scenarioMain;
                        d34.ExecuteTestCases(test, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain d51 = (Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain)scenarioMain;
                        d51.ExecuteTestcases(test, "D65", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D66.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D66.ScenarioMain d66 = (Quote.CopyNewVersion_Indirect.D66.ScenarioMain)scenarioMain;
                        d66.ExecuteTestcases(test, "D66", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D67.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D67.ScenarioMain d67 = (Quote.CopyNewVersion_Indirect.D67.ScenarioMain)scenarioMain;
                        d67.ExecuteTestcases(test, "D67", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D68.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D68.ScenarioMain d68 = (Quote.CopyNewVersion_Indirect.D68.ScenarioMain)scenarioMain;
                        d68.ExecuteTestcases(test, "D68", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D69.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D69.ScenarioMain d69 = (Quote.CopyNewVersion_Indirect.D69.ScenarioMain)scenarioMain;
                        d69.ExecuteTestcases(test, "D69", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D70.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D70.ScenarioMain d70 = (Quote.CopyNewVersion_Indirect.D70.ScenarioMain)scenarioMain;
                        d70.ExecuteTestcases(test, "D70", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D71.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D71.ScenarioMain d71 = (Quote.CopyNewVersion_Indirect.D71.ScenarioMain)scenarioMain;
                        d71.ExecuteTestcases(test, "D71", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D72.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D72.ScenarioMain d72 = (Quote.CopyNewVersion_Indirect.D72.ScenarioMain)scenarioMain;
                        d72.ExecuteTestcases(test, "D72", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain d73_74 = (Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain)scenarioMain;
                        d73_74.ExecuteTestcases(test, "D73_D74", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D75.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D75.ScenarioMain d75 = (Quote.CopyNewVersion_Indirect.D75.ScenarioMain)scenarioMain;
                        d75.ExecuteTestcases(test, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }


                    else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D76.ScenarioMain))
                    {
                        Quote.CopyNewVersion_Indirect.D76.ScenarioMain d76 = (Quote.CopyNewVersion_Indirect.D76.ScenarioMain)scenarioMain;
                        d76.ExecuteTestcases(test, "D76", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain d77 = (Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain)scenarioMain;
                        d77.ExecuteTestCases(test, "D77", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain d78 = (Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain)scenarioMain;
                        d78.ExecuteTestcases(test, "D78", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain d79 = (Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain)scenarioMain;
                        d79.ExecuteTestcases(test, "D79", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain d80 = (Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain)scenarioMain;
                        d80.ExecuteTestcases(test, "D80", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain d81 = (Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain)scenarioMain;
                        d81.ExecuteTestcases(test, "D81", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain d82 = (Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain)scenarioMain;
                        d82.ExecuteTestcases(test, "D82", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain d83 = (Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain)scenarioMain;
                        d83.ExecuteTestcases(test, "D69", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain d84 = (Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain)scenarioMain;
                        d84.ExecuteTestcases(test, "D84", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain d71 = (Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain)scenarioMain;
                        d71.ExecuteTestcases(test, "D85", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain d86 = (Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain)scenarioMain;
                        d86.ExecuteTestcases(test, "D86", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain d87_88 = (Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain)scenarioMain;
                        d87_88.ExecuteTestcases(test, "D87_D88", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain d89 = (Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain)scenarioMain;
                        d89.ExecuteTestcases(test, "D89", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain d90 = (Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain)scenarioMain;
                        d90.ExecuteTestcases(test, "D90", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain d91 = (Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain)scenarioMain;
                        d91.ExecuteTestcases(test, scenarioId, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain d92 = (Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain)scenarioMain;
                        d92.ExecuteTestcases(test, "D92", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain d93 = (Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain)scenarioMain;
                        d93.ExecuteTestcases(test, "D93", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain d94 = (Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain)scenarioMain;
                        d94.ExecuteTestcases(test, "D94", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain d95 = (Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain)scenarioMain;
                        d95.ExecuteTestcases(test, "D95", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain d96 = (Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain)scenarioMain;
                        d96.ExecuteTestcases(test, "D96", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain d97 = (Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain)scenarioMain;
                        d97.ExecuteTestcases(test, "D97", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain d98 = (Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain)scenarioMain;
                        d98.ExecuteTestcases(test, "D98", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain d99 = (Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain)scenarioMain;
                        d99.ExecuteTestcases(test, "D99", testCaseNumber, countryToTest.ToUpper(),TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain d100_101 = (Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain)scenarioMain;
                        d100_101.ExecuteTestcases(test, "D100_D101", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain d102 = (Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain)scenarioMain;
                        d102.ExecuteTestcases(test, "D102", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain d103 = (Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain)scenarioMain;
                        d103.ExecuteTestcases(test, "D103", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain d104 = (Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain)scenarioMain;
                        d104.ExecuteTestCases(test, "D104", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain d105 = (Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain)scenarioMain;
                        d105.ExecuteTestcases(test, "D105", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain d106 = (Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain)scenarioMain;
                        d106.ExecuteTestcases(test, "D106", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain d107 = (Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain)scenarioMain;
                        d107.ExecuteTestcases(test, "D107", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain d108 = (Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain)scenarioMain;
                        d108.ExecuteTestcases(test, "D108", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain d109 = (Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain)scenarioMain;
                        d109.ExecuteTestcases(test, "D109", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain d110 = (Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain)scenarioMain;
                        d110.ExecuteTestcases(test, "D110", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain d111 = (Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain)scenarioMain;
                        d111.ExecuteTestcases(test, "D111", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain d112 = (Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain)scenarioMain;
                        d112.ExecuteTestcases(test, "D112", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain d113 = (Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain)scenarioMain;
                        d113.ExecuteTestcases(test, "D113_D114", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain d115 = (Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain)scenarioMain;
                        d115.ExecuteTestcases(test, "D115", testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }
                    if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain))
                    {
                        Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain d116 = (Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain)scenarioMain;
                        d116.ExecuteTestcases(test, scenarioId, testCaseNumber, countryToTest.ToUpper(), TestDataReader.ApplicationsToTest);
                    }

                    testCaseNumber++;
                    if (Driver != null)
                    {
                        Driver.Close();
                    }



                }
            }
            TimeSpan d01endTime = DateTime.Now.TimeOfDay;
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("TimeElapse: {0} ", TimeElapse(d01startTime, d01endTime)), true);
            Console.WriteLine(scenarioId + "is Completed");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Scenario {0} Test Execution Completed.", scenarioId), true);
           
        }
    }
}

       
    
