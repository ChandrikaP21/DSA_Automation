using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice_E2E_WebAutomation.Quote;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using SmartPrice_E2E_WebAutomation.Quote.CopyNewQuote_Direct.D01;
using SmartPrice_E2E_WebAutomation.Helper;

namespace SmartPrice_E2E_WebAutomation
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
            reader.LoadXmlElements();
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Test Scenarios Load Completed.Total {0} Scenarios loaded", TestDataReader._scenarios.Count), true);
            SmartPriceMain smartPriceMain = new SmartPriceMain();
            foreach (var scenario in TestDataReader._scenarios)

            {
                try
                {
                    //ExecuteTestCases(scenario.ScenarioID,scenario.TestCases);


                    switch (scenario.ScenarioID)
                    {

                        //case "D01":

                        //    Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(URLHelper.GetDSAUrl(), smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, scenarioMain);

                        //    break;

                        //case "D02":

                        //    Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain d02scenarioMain = new Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain(URLHelper.GetDSAUrl(), smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d02scenarioMain);

                        //   break;
                        //case "D03":
                        //    Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03scenarioMain = new Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d03scenarioMain);

                        //    break;
                        //case "D04":

                        //    Quote.CopyNewQuote_Direct.D04.ScenarioMain d04scenarioMain = new Quote.CopyNewQuote_Direct.D04.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d04scenarioMain);


                        //    break;
                        //case "D05":
                        //    Quote.CopyNewQuote_Direct.D05.ScenarioMain d05scenarioMain = new Quote.CopyNewQuote_Direct.D05.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d05scenarioMain);

                        //    break;
                        //case "D06":
                        //    Quote.CopyNewQuote_Direct.D06.ScenarioMain d06scenarioMain = new Quote.CopyNewQuote_Direct.D06.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d06scenarioMain);

                        //    break;
                        //case "D07":
                        //    Quote.CopyNewQuote_Direct.D07.ScenarioMain d07scenarioMain = new Quote.CopyNewQuote_Direct.D07.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d07scenarioMain);

                        //    break;
                        //case "D08":
                        //    Quote.CopyNewQuote_Direct.D08.ScenarioMain d08scenarioMain = new Quote.CopyNewQuote_Direct.D08.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d08scenarioMain);

                        //    break;

                        //case "D09":
                        //    Quote.CopyNewQuote_Direct.D09.ScenarioMain d09scenarioMain = new Quote.CopyNewQuote_Direct.D09.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d09scenarioMain);

                        //    break;
                        //case "D10":
                        //    Quote.CopyNewQuote_Direct.D10.ScenarioMain d10scenarioMain = new Quote.CopyNewQuote_Direct.D10.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d10scenarioMain);

                        //    break;
                        //case "D11":
                        //    Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11scenarioMain = new Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d11scenarioMain);

                        //    break;
                        //case "D13":
                        //    Quote.CopyNewQuote_Direct.D13.ScenarioMain d13scenarioMain = new Quote.CopyNewQuote_Direct.D13.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                        //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d13scenarioMain);

                        //    break;
                        case "D16":

                            Quote.CopyNewQuote_Direct.D16.ScenarioMain d16scenarioMain = new Quote.CopyNewQuote_Direct.D16.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d16scenarioMain);

                            break;
                            //case "D17":

                            //    Quote.CopyNewVersion_Direct.D17.ScenarioMain d17scenarioMain = new Quote.CopyNewVersion_Direct.D17.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d17scenarioMain);

                            //    break;

                            //case "D18":

                            //    Quote.CopyNewVersion_Direct.D18.ScenarioMain d18scenarioMain = new Quote.CopyNewVersion_Direct.D18.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d18scenarioMain);

                            //    break;
                            //case "D19":
                            //    Quote.CopyNewVersion_Direct.D19.ScenarioMain d19scenarioMain = new Quote.CopyNewVersion_Direct.D19.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d19scenarioMain);

                            //    break;
                            //case "D20":
                            //    Quote.CopyNewVersion_Direct.D20.ScenarioMain d20scenarioMain = new Quote.CopyNewVersion_Direct.D20.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d20scenarioMain);

                            //    break;
                            //case "D21":
                            //    Quote.CopyNewVersion_Direct.D21.ScenarioMain d21scenarioMain = new Quote.CopyNewVersion_Direct.D21.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d21scenarioMain);

                            //    break;
                            //case "D22":
                            //    Quote.CopyNewVersion_Direct.D22.ScenarioMain d22scenarioMain = new Quote.CopyNewVersion_Direct.D22.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d22scenarioMain);

                            //    break;
                            //case "D23":
                            //    Quote.CopyNewVersion_Direct.D23.ScenarioMain d23scenarioMain = new Quote.CopyNewVersion_Direct.D23.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d23scenarioMain);

                            //    break;
                            //case "D24":
                            //    Quote.CopyNewVersion_Direct.D24.ScenarioMain d24scenarioMain = new Quote.CopyNewVersion_Direct.D24.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d24scenarioMain);

                            //    break;

                            //case "D25":
                            //    Quote.CopyNewVersion_Direct.D25.ScenarioMain d25scenarioMain = new Quote.CopyNewVersion_Direct.D25.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d25scenarioMain);

                            //    break;
                            //case "D26":
                            //    Quote.CopyNewVersion_Direct.D26.ScenarioMain d26scenarioMain = new Quote.CopyNewVersion_Direct.D26.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d26scenarioMain);

                            //    break;
                            //case "D27_D28":
                            //    Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain d27_d28scenarioMain = new Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d27_d28scenarioMain);

                            //    break;
                            //case "D29":
                            //    Quote.CopyNewVersion_Direct.D29.ScenarioMain d29scenarioMain = new Quote.CopyNewVersion_Direct.D29.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d29scenarioMain);

                            //    break;
                            //case "D32":
                            //    Quote.CopyNewVersion_Direct.D32.ScenarioMain d32scenarioMain = new Quote.CopyNewVersion_Direct.D32.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d32scenarioMain);

                            //    break;
                            //case "D33":

                            //    Quote.CreateNewQuote_Direct.D33.ScenarioMain d33scenarioMain = new Quote.CreateNewQuote_Direct.D33.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d33scenarioMain);

                            //    break;

                            //case "D34":

                            //    Quote.CreateNewQuote_Direct.D34.ScenarioMain d34scenarioMain = new Quote.CreateNewQuote_Direct.D34.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d34scenarioMain);

                            //    break;
                            //case "D35":
                            //    Quote.CreateNewQuote_Direct.D35.ScenarioMain d35scenarioMain = new Quote.CreateNewQuote_Direct.D35.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d35scenarioMain);

                            //    break;
                            //case "D36":
                            //    Quote.CreateNewQuote_Direct.D36.ScenarioMain d36scenarioMain = new Quote.CreateNewQuote_Direct.D36.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d36scenarioMain);

                            //    break;
                            //case "D37":
                            //    Quote.CreateNewQuote_Direct.D37.ScenarioMain d37scenarioMain = new Quote.CreateNewQuote_Direct.D37.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d37scenarioMain);

                            //    break;
                            //case "D38":
                            //    Quote.CreateNewQuote_Direct.D38.ScenarioMain d38scenarioMain = new Quote.CreateNewQuote_Direct.D38.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d38scenarioMain);

                            //    break;
                            //case "D39":
                            //    Quote.CreateNewQuote_Direct.D39.ScenarioMain d39scenarioMain = new Quote.CreateNewQuote_Direct.D39.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d39scenarioMain);

                            //    break;
                            //case "D40":
                            //    Quote.CreateNewQuote_Direct.D40.ScenarioMain d40scenarioMain = new Quote.CreateNewQuote_Direct.D40.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d40scenarioMain);

                            //    break;

                            //case "D41":
                            //    Quote.CreateNewQuote_Direct.D41.ScenarioMain d41scenarioMain = new Quote.CreateNewQuote_Direct.D41.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d41scenarioMain);

                            //    break;
                            //case "D42":
                            //    Quote.CreateNewQuote_Direct.D42.ScenarioMain d42scenarioMain = new Quote.CreateNewQuote_Direct.D42.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d42scenarioMain);

                            //    break;
                            //case "D43-D44":
                            //    Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain D43_D44scenarioMain = new Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, D43_D44scenarioMain);

                            //    break;
                            //case "D45":
                            //    Quote.CreateNewQuote_Direct.D45.ScenarioMain d45scenarioMain = new Quote.CreateNewQuote_Direct.D45.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d45scenarioMain);

                            //    break;

                            //case "D48":
                            //    Quote.CreateNewQuote_Direct.D48.ScenarioMain d48scenarioMain = new Quote.CreateNewQuote_Direct.D48.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d48scenarioMain);

                            //    break;

                            //case "D49":

                            //    Quote.CopyNewQuote_Indirect.D49.ScenarioMain d49 = new Quote.CopyNewQuote_Indirect.D49.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d49);

                            //    break;
                            //case "D50":
                            //    Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain d50 = new Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d50);

                            //    break;
                            //case "D51":
                            //    Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain d51 = new Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d51);
                            //    break;
                            //case "D52":
                            //    Quote.CopyNewQuote_Indirect.D52.ScenarioMain d52 = new Quote.CopyNewQuote_Indirect.D52.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d52);

                            //    break;
                            //case "D53":
                            //    Quote.CopyNewQuote_Indirect.D53.ScenarioMain d53 = new Quote.CopyNewQuote_Indirect.D53.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d53);

                            //    break;
                            //case "D54":
                            //    Quote.CopyNewQuote_Indirect.D54.ScenarioMain d54 = new Quote.CopyNewQuote_Indirect.D54.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d54);

                            //    break;
                            //case "D55":
                            //    Quote.CopyNewQuote_Indirect.D55.ScenarioMain d55 = new Quote.CopyNewQuote_Indirect.D55.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d55);

                            //    break;

                            //case "D56":
                            //    Quote.CopyNewQuote_Indirect.D56.ScenarioMain d56 = new Quote.CopyNewQuote_Indirect.D56.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d56);

                            //    break;
                            //case "D57":
                            //    Quote.CopyNewQuote_Indirect.D57.ScenarioMain d57 = new Quote.CopyNewQuote_Indirect.D57.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d57);

                            //    break;
                            //case "D58":
                            //    Quote.CopyNewQuote_Indirect.D58.ScenarioMain d58 = new Quote.CopyNewQuote_Indirect.D58.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d58);

                            //    break;
                            //case "D59_D60":
                            //    Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain d59_60 = new Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d59_60);

                            //    break;
                            //case "D61":
                            //    Quote.CopyNewQuote_Indirect.D61.ScenarioMain d61 = new Quote.CopyNewQuote_Indirect.D61.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d61);
                            //    break;
                            //case "D62":
                            //    Quote.CopyNewQuote_Indirect.D62.ScenarioMain d62 = new Quote.CopyNewQuote_Indirect.D62.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d62);
                            //    break;

                            //case "D63":

                            //    Quote.CopyNewVersion_Indirect.D63.ScenarioMain d63 = new Quote.CopyNewVersion_Indirect.D63.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d63);

                            //    break;
                            //case "D64":
                            //    Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain d64 = new Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d64);

                            //    break;

                            //case "D65":
                            //    Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain d65 = new Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d65);
                            //    break;

                            //case "D66":
                            //    Quote.CopyNewVersion_Indirect.D66.ScenarioMain d66 = new Quote.CopyNewVersion_Indirect.D66.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d66);

                            //    break;
                            //case "D67":
                            //    Quote.CopyNewVersion_Indirect.D67.ScenarioMain d67 = new Quote.CopyNewVersion_Indirect.D67.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d67);

                            //    break;
                            //case "D68":
                            //    Quote.CopyNewVersion_Indirect.D68.ScenarioMain d68 = new Quote.CopyNewVersion_Indirect.D68.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d68);

                            //    break;
                            //case "D69":
                            //    Quote.CopyNewVersion_Indirect.D69.ScenarioMain d69 = new Quote.CopyNewVersion_Indirect.D69.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d69);

                            //    break;

                            //case "D70":
                            //    Quote.CopyNewVersion_Indirect.D70.ScenarioMain d70 = new Quote.CopyNewVersion_Indirect.D70.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d70);

                            //    break;
                            //case "D71":
                            //    Quote.CopyNewVersion_Indirect.D71.ScenarioMain d71 = new Quote.CopyNewVersion_Indirect.D71.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d71);

                            //    break;
                            //case "D72":
                            //    Quote.CopyNewVersion_Indirect.D72.ScenarioMain d72 = new Quote.CopyNewVersion_Indirect.D72.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d72);

                            //    break;
                            //case "D73_D74":
                            //    Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain d73_74 = new Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d73_74);

                            //    break;
                            //case "D75":
                            //    Quote.CopyNewVersion_Indirect.D75.ScenarioMain d75 = new Quote.CopyNewVersion_Indirect.D75.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d75);
                            //    break;
                            //case "D76":
                            //    Quote.CopyNewVersion_Indirect.D76.ScenarioMain d76 = new Quote.CopyNewVersion_Indirect.D76.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d76);
                            //    break;
                            //case "D77":

                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain d77 = new Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d77);

                            //    break;
                            //case "D78":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain d78 = new Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d78);

                            //    break;

                            //case "D79":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain d79 = new Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d79);
                            //    break;

                            //case "D80":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain d80 = new Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d80);

                            //    break;
                            //case "D81":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain d81 = new Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d81);

                            //    break;
                            //case "D82":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain d82 = new Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d82);

                            //    break;
                            //case "D83":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain d83 = new Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d83);

                            //    break;

                            //case "D84":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain d84 = new Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d84);

                            //    break;
                            //case "D85":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain d85 = new Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d85);

                            //    break;
                            //case "D86":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain d86 = new Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d86);

                            //    break;
                            //case "D87_D88":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain d87_88 = new Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d87_88);

                            //    break;
                            //case "D89":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain d89 = new Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d89);
                            //    break;
                            //case "D90":
                            //    Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain d90 = new Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d90);
                            //    break;
                            //case "D91":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain d91 = new Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d91);

                            //    break;

                            //case "D92":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain d92 = new Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d92);
                            //    break;

                            //case "D93":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain d93 = new Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d93);

                            //    break;
                            //case "D94":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain d94 = new Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d94);

                            //    break;
                            //case "D95":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain d95 = new Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d95);

                            //    break;
                            //case "D96":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain d96 = new Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d96);

                            //    break;

                            //case "D97":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain d97 = new Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d97);

                            //    break;
                            //case "D98":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain d98 = new Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d98);

                            //    break;
                            //case "D99":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain d99 = new Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d99);

                            //    break;
                            //case "D100_D101":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain d100 = new Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d100);

                            //    break;

                            //case "D102":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain d102 = new Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d102);
                            //    break;
                            //case "D103":
                            //    Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain d103 = new Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d103);
                            //    break;
                            //case "D104":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain d104 = new Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d104);

                            //    break;
                            //case "D105":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain d105 = new Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d105);

                            //    break;

                            //case "D106":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain d106 = new Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d106);
                            //    break;

                            //case "D107":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain d107 = new Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d107);

                            //    break;
                            //case "D108":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain d108 = new Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d108);

                            //    break;
                            //case "D109":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain d109 = new Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d109);

                            //    break;
                            //case "D110":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain d110 = new Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d110);

                            //    break;

                            //case "D111":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain d111 = new Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d111);

                            //    break;
                            //case "D112":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain d112 = new Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d112);

                            //    break;
                            //case "D113_D114":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain d113_d114 = new Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d113_d114);

                            //    break;

                            //case "D115":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain d115 = new Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d115);

                            //    break;
                            //case "D116":
                            //    Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain d116 = new Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain(Constant.URL_Prod, smartPriceMain.Driver);
                            //    smartPriceMain.ExecuteTestCases(scenario.ScenarioID, scenario.TestCases, d116);

                            //    break;



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }
            
        }



        public void ExecuteTestCases(string scenarioId, List<TestCase> testCases, object scenarioMain)
        {


            TimeSpan d01startTime = DateTime.Now.TimeOfDay;
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Scenario {0} Test Execution Initiated.", scenarioId), true);
            int testCaseNumber = 01;


            foreach (TestCase test in testCases)
            {

                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Test Case {0} initiated.", test.TestCaseTitle), true);  //Test Case "Test case title" initiated.
                
                if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D01.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D01.ScenarioMain d01 = (Quote.CopyNewQuote_Direct.D01.ScenarioMain)scenarioMain;
                    d01.ExecuteTestCases(test, testCaseNumber, TestDataReader.ApplicationsToTest);
                }
                
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain d02 = (Quote.CopyNewQuote_Direct.D02.ScenarioMain.ScenarioMain)scenarioMain;
                    d02.ExecuteTestCases(test, testCaseNumber, TestDataReader.ApplicationsToTest);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain d03 = (Quote.CopyNewQuote_Direct.D03.ScenarioMain.ScenarioMain)scenarioMain;
                    d03.ExecuteTestcases(test, "D03", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D04.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D04.ScenarioMain d04 = (Quote.CopyNewQuote_Direct.D04.ScenarioMain)scenarioMain;
                    d04.ExecuteTestcases(test, "D04", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D05.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D05.ScenarioMain d05 = (Quote.CopyNewQuote_Direct.D05.ScenarioMain)scenarioMain;
                    d05.ExecuteTestcases(test, "D05", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D06.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D06.ScenarioMain d06 = (Quote.CopyNewQuote_Direct.D06.ScenarioMain)scenarioMain;
                    d06.ExecuteTestcases(test, "D06", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D07.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D07.ScenarioMain d07 = (Quote.CopyNewQuote_Direct.D07.ScenarioMain)scenarioMain;
                    d07.ExecuteTestcases(test, "D07", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D08.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D08.ScenarioMain d08 = (Quote.CopyNewQuote_Direct.D08.ScenarioMain)scenarioMain;
                    d08.ExecuteTestcases(test, "D08", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D09.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D09.ScenarioMain d08 = (Quote.CopyNewQuote_Direct.D09.ScenarioMain)scenarioMain;
                    d08.ExecuteTestcases(test, "D09", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D10.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D10.ScenarioMain d10 = (Quote.CopyNewQuote_Direct.D10.ScenarioMain)scenarioMain;
                    d10.ExecuteTestcases(test, "D10", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain d11_d12 = (Quote.CopyNewQuote_Direct.D11_D12.ScenarioMain)scenarioMain;
                    d11_d12.ExecuteTestcases(test, "d11_d12", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D13.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D13.ScenarioMain d13 = (Quote.CopyNewQuote_Direct.D13.ScenarioMain)scenarioMain;
                    d13.ExecuteTestcases(test, testCaseNumber);
                }


                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Direct.D16.ScenarioMain))
                {
                    Quote.CopyNewQuote_Direct.D16.ScenarioMain d16 = (Quote.CopyNewQuote_Direct.D16.ScenarioMain)scenarioMain;
                    d16.ExecuteTestcases(test, "D16", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D17.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D17.ScenarioMain d17 = (Quote.CopyNewVersion_Direct.D17.ScenarioMain)scenarioMain;
                    d17.ExecuteTestcases(test, "D17", testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D18.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D18.ScenarioMain d18 = (Quote.CopyNewVersion_Direct.D18.ScenarioMain)scenarioMain;
                    d18.ExecuteTestcases(test,"D18", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D19.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D19.ScenarioMain d19 = (Quote.CopyNewVersion_Direct.D19.ScenarioMain)scenarioMain;
                    d19.ExecuteTestcases(test, "D19", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D20.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D20.ScenarioMain d20 = (Quote.CopyNewVersion_Direct.D20.ScenarioMain)scenarioMain;
                    d20.ExecuteTestcases(test, "D20", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D21.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D21.ScenarioMain d21 = (Quote.CopyNewVersion_Direct.D21.ScenarioMain)scenarioMain;
                    d21.ExecuteTestcases(test, "D21", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D22.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D22.ScenarioMain d22 = (Quote.CopyNewVersion_Direct.D22.ScenarioMain)scenarioMain;
                    d22.ExecuteTestcases(test, "D06", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D23.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D23.ScenarioMain d23 = (Quote.CopyNewVersion_Direct.D23.ScenarioMain)scenarioMain;
                    d23.ExecuteTestcases(test, "D23", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D24.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D24.ScenarioMain d24 = (Quote.CopyNewVersion_Direct.D24.ScenarioMain)scenarioMain;
                    d24.ExecuteTestcases(test, "D24", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D25.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D25.ScenarioMain d25 = (Quote.CopyNewVersion_Direct.D25.ScenarioMain)scenarioMain;
                    d25.ExecuteTestcases(test, "D25", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D26.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D26.ScenarioMain d26 = (Quote.CopyNewVersion_Direct.D26.ScenarioMain)scenarioMain;
                    d26.ExecuteTestcases(test, "D26", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain d27_28 = (Quote.CopyNewVersion_Direct.D27_D28.ScenarioMain)scenarioMain;
                    d27_28.ExecuteTestcases(test, "D27_D28", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D29.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D29.ScenarioMain d29 = (Quote.CopyNewVersion_Direct.D29.ScenarioMain)scenarioMain;
                    d29.ExecuteTestcases(test, "D29", testCaseNumber);
                }


                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Direct.D32.ScenarioMain))
                {
                    Quote.CopyNewVersion_Direct.D32.ScenarioMain d32 = (Quote.CopyNewVersion_Direct.D32.ScenarioMain)scenarioMain;
                    d32.ExecuteTestcases(test, "D32", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D33.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D33.ScenarioMain d33 = (Quote.CreateNewQuote_Direct.D33.ScenarioMain)scenarioMain;
                    d33.ExecuteTestCases(test, "D33", testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D34.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D34.ScenarioMain d19 = (Quote.CreateNewQuote_Direct.D34.ScenarioMain)scenarioMain;
                    d19.ExecuteTestcases(test, "D34", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D35.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D35.ScenarioMain d20 = (Quote.CreateNewQuote_Direct.D35.ScenarioMain)scenarioMain;
                    d20.ExecuteTestcases(test, "D35", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D36.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D36.ScenarioMain d21 = (Quote.CreateNewQuote_Direct.D36.ScenarioMain)scenarioMain;
                    d21.ExecuteTestcases(test, "D36", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D37.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D37.ScenarioMain d37 = (Quote.CreateNewQuote_Direct.D37.ScenarioMain)scenarioMain;
                    d37.ExecuteTestcases(test, "D37", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D38.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D38.ScenarioMain d23 = (Quote.CreateNewQuote_Direct.D38.ScenarioMain)scenarioMain;
                    d23.ExecuteTestcases(test, "D38", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D39.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D39.ScenarioMain d39 = (Quote.CreateNewQuote_Direct.D39.ScenarioMain)scenarioMain;
                    d39.ExecuteTestcases(test, "D39", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D40.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D40.ScenarioMain d40 = (Quote.CreateNewQuote_Direct.D40.ScenarioMain)scenarioMain;
                    d40.ExecuteTestcases(test, "D40", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D41.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D41.ScenarioMain d26 = (Quote.CreateNewQuote_Direct.D41.ScenarioMain)scenarioMain;
                    d26.ExecuteTestcases(test, "D41", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D42.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D42.ScenarioMain d42 = (Quote.CreateNewQuote_Direct.D42.ScenarioMain)scenarioMain;
                    d42.ExecuteTestcases(test, "D42", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain d43_D44 = (Quote.CreateNewQuote_Direct.D43_D44.ScenarioMain)scenarioMain;
                    d43_D44.ExecuteTestcases(test, "D43_D44", testCaseNumber);
                }


                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D45.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D45.ScenarioMain d32 = (Quote.CreateNewQuote_Direct.D45.ScenarioMain)scenarioMain;
                    d32.ExecuteTestcases(test, "D45", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Direct.D48.ScenarioMain))
                {
                    Quote.CreateNewQuote_Direct.D48.ScenarioMain d48 = (Quote.CreateNewQuote_Direct.D48.ScenarioMain)scenarioMain;
                    d48.ExecuteTestcases(test, "D48", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D49.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D49.ScenarioMain d49 = (Quote.CopyNewQuote_Indirect.D49.ScenarioMain)scenarioMain;
                    d49.ExecuteTestcases(test, "D49", testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain d34 = (Quote.CopyNewQuote_Indirect.D50.ScenarioMain.ScenarioMain)scenarioMain;
                    d34.ExecuteTestCases(test, testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain d51 = (Quote.CopyNewQuote_Indirect.D51.ScenarioMain.ScenarioMain)scenarioMain;
                    d51.ExecuteTestcases(test, "D51", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D52.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D52.ScenarioMain d52 = (Quote.CopyNewQuote_Indirect.D52.ScenarioMain)scenarioMain;
                    d52.ExecuteTestcases(test, "D52", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D53.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D53.ScenarioMain d21 = (Quote.CopyNewQuote_Indirect.D53.ScenarioMain)scenarioMain;
                    d21.ExecuteTestcases(test, "D53", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D54.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D54.ScenarioMain d54 = (Quote.CopyNewQuote_Indirect.D54.ScenarioMain)scenarioMain;
                    d54.ExecuteTestcases(test, "D54", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D55.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D55.ScenarioMain d23 = (Quote.CopyNewQuote_Indirect.D55.ScenarioMain)scenarioMain;
                    d23.ExecuteTestcases(test, "D55", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D56.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D56.ScenarioMain d39 = (Quote.CopyNewQuote_Indirect.D56.ScenarioMain)scenarioMain;
                    d39.ExecuteTestcases(test, "D56", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D57.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D57.ScenarioMain d40 = (Quote.CopyNewQuote_Indirect.D57.ScenarioMain)scenarioMain;
                    d40.ExecuteTestcases(test, "D57", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D58.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D58.ScenarioMain d26 = (Quote.CopyNewQuote_Indirect.D58.ScenarioMain)scenarioMain;
                    d26.ExecuteTestcases(test, "D58", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain d59_60 = (Quote.CopyNewQuote_Indirect.D59_D60.ScenarioMain)scenarioMain;
                    d59_60.ExecuteTestcases(test, "D59_D60", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D61.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D61.ScenarioMain d61 = (Quote.CopyNewQuote_Indirect.D61.ScenarioMain)scenarioMain;
                    d61.ExecuteTestcases(test, testCaseNumber);
                }


                else if (scenarioMain.GetType() == typeof(Quote.CopyNewQuote_Indirect.D62.ScenarioMain))
                {
                    Quote.CopyNewQuote_Indirect.D62.ScenarioMain d62 = (Quote.CopyNewQuote_Indirect.D62.ScenarioMain)scenarioMain;
                    d62.ExecuteTestcases(test, "D62", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D63.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D63.ScenarioMain d63 = (Quote.CopyNewVersion_Indirect.D63.ScenarioMain)scenarioMain;
                    d63.ExecuteTestcases(test, "D63", testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain d34 = (Quote.CopyNewVersion_Indirect.D64.ScenarioMain.ScenarioMain)scenarioMain;
                    d34.ExecuteTestCases(test, testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain d51 = (Quote.CopyNewVersion_Indirect.D65.ScenarioMain.ScenarioMain)scenarioMain;
                    d51.ExecuteTestcases(test, "D65", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D66.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D66.ScenarioMain d66 = (Quote.CopyNewVersion_Indirect.D66.ScenarioMain)scenarioMain;
                    d66.ExecuteTestcases(test, "D66", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D67.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D67.ScenarioMain d67 = (Quote.CopyNewVersion_Indirect.D67.ScenarioMain)scenarioMain;
                    d67.ExecuteTestcases(test, "D67", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D68.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D68.ScenarioMain d68 = (Quote.CopyNewVersion_Indirect.D68.ScenarioMain)scenarioMain;
                    d68.ExecuteTestcases(test, "D68", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D69.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D69.ScenarioMain d69 = (Quote.CopyNewVersion_Indirect.D69.ScenarioMain)scenarioMain;
                    d69.ExecuteTestcases(test, "D69", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D70.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D70.ScenarioMain d70 = (Quote.CopyNewVersion_Indirect.D70.ScenarioMain)scenarioMain;
                    d70.ExecuteTestcases(test, "D70", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D71.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D71.ScenarioMain d71 = (Quote.CopyNewVersion_Indirect.D71.ScenarioMain)scenarioMain;
                    d71.ExecuteTestcases(test, "D71", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D72.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D72.ScenarioMain d72 = (Quote.CopyNewVersion_Indirect.D72.ScenarioMain)scenarioMain;
                    d72.ExecuteTestcases(test, "D72", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain d73_74 = (Quote.CopyNewVersion_Indirect.D73_D74.ScenarioMain)scenarioMain;
                    d73_74.ExecuteTestcases(test, "D73_D74", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D75.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D75.ScenarioMain d75 = (Quote.CopyNewVersion_Indirect.D75.ScenarioMain)scenarioMain;
                    d75.ExecuteTestcases(test, testCaseNumber);
                }


                else if (scenarioMain.GetType() == typeof(Quote.CopyNewVersion_Indirect.D76.ScenarioMain))
                {
                    Quote.CopyNewVersion_Indirect.D76.ScenarioMain d76 = (Quote.CopyNewVersion_Indirect.D76.ScenarioMain)scenarioMain;
                    d76.ExecuteTestcases(test, "D76", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain d77 = (Quote.CreateNewQuote_Indirect.StockAndSell.D77.ScenarioMain)scenarioMain;
                    d77.ExecuteTestCases(test, "D77", testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain d78 = (Quote.CreateNewQuote_Indirect.StockAndSell.D78.ScenarioMain)scenarioMain;
                    d78.ExecuteTestcases(test, "D78", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain d79 = (Quote.CreateNewQuote_Indirect.StockAndSell.D79.ScenarioMain)scenarioMain;
                    d79.ExecuteTestcases(test, "D79", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain d80 = (Quote.CreateNewQuote_Indirect.StockAndSell.D80.ScenarioMain)scenarioMain;
                    d80.ExecuteTestcases(test, "D80", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain d81 = (Quote.CreateNewQuote_Indirect.StockAndSell.D81.ScenarioMain)scenarioMain;
                    d81.ExecuteTestcases(test, "D81", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain d82 = (Quote.CreateNewQuote_Indirect.StockAndSell.D82.ScenarioMain)scenarioMain;
                    d82.ExecuteTestcases(test, "D82", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain d83 = (Quote.CreateNewQuote_Indirect.StockAndSell.D83.ScenarioMain)scenarioMain;
                    d83.ExecuteTestcases(test, "D69", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain d84 = (Quote.CreateNewQuote_Indirect.StockAndSell.D84.ScenarioMain)scenarioMain;
                    d84.ExecuteTestcases(test, "D84", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain d71 = (Quote.CreateNewQuote_Indirect.StockAndSell.D85.ScenarioMain)scenarioMain;
                    d71.ExecuteTestcases(test, "D85", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain d86 = (Quote.CreateNewQuote_Indirect.StockAndSell.D86.ScenarioMain)scenarioMain;
                    d86.ExecuteTestcases(test, "D86", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain d87_88 = (Quote.CreateNewQuote_Indirect.StockAndSell.D87_D88.ScenarioMain)scenarioMain;
                    d87_88.ExecuteTestcases(test, "D87_D88", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain d89 = (Quote.CreateNewQuote_Indirect.StockAndSell.D89.ScenarioMain)scenarioMain;
                    d89.ExecuteTestcases(test, "D89", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain d90 = (Quote.CreateNewQuote_Indirect.StockAndSell.D90.ScenarioMain)scenarioMain;
                    d90.ExecuteTestcases(test, "D90", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain d91 = (Quote.CreateNewQuote_Indirect.ReSell.D91.ScenarioMain)scenarioMain;
                    d91.ExecuteTestcases(test, scenarioId, testCaseNumber);
                }
                //Quote.CopyNewQuote_Direct.D01.ScenarioMain scenarioMain = new Quote.CopyNewQuote_Direct.D01.ScenarioMain(Constant.URL_Prod);
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain d92 = (Quote.CreateNewQuote_Indirect.ReSell.D92.ScenarioMain)scenarioMain;
                    d92.ExecuteTestcases(test, "D92", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain d93 = (Quote.CreateNewQuote_Indirect.ReSell.D93.ScenarioMain)scenarioMain;
                    d93.ExecuteTestcases(test, "D93", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain d94 = (Quote.CreateNewQuote_Indirect.ReSell.D94.ScenarioMain)scenarioMain;
                    d94.ExecuteTestcases(test, "D94", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain d95 = (Quote.CreateNewQuote_Indirect.ReSell.D95.ScenarioMain)scenarioMain;
                    d95.ExecuteTestcases(test, "D95", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain d96 = (Quote.CreateNewQuote_Indirect.ReSell.D96.ScenarioMain)scenarioMain;
                    d96.ExecuteTestcases(test, "D96", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain d97 = (Quote.CreateNewQuote_Indirect.ReSell.D97.ScenarioMain)scenarioMain;
                    d97.ExecuteTestcases(test, "D97", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain d98 = (Quote.CreateNewQuote_Indirect.ReSell.D98.ScenarioMain)scenarioMain;
                    d98.ExecuteTestcases(test, "D98", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain d99 = (Quote.CreateNewQuote_Indirect.ReSell.D99.ScenarioMain)scenarioMain;
                    d99.ExecuteTestcases(test, "D99", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain d100_101 = (Quote.CreateNewQuote_Indirect.ReSell.D100_D101.ScenarioMain)scenarioMain;
                    d100_101.ExecuteTestcases(test, "D100_D101", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain d102 = (Quote.CreateNewQuote_Indirect.ReSell.D102.ScenarioMain)scenarioMain;
                    d102.ExecuteTestcases(test, "D102", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain d103 = (Quote.CreateNewQuote_Indirect.ReSell.D103.ScenarioMain)scenarioMain;
                    d103.ExecuteTestcases(test, "D103", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain d104 = (Quote.CreateNewQuote_Indirect.Distributor.D104.ScenarioMain)scenarioMain;
                    d104.ExecuteTestCases(test, "D104", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain d105 = (Quote.CreateNewQuote_Indirect.Distributor.D105.ScenarioMain)scenarioMain;
                    d105.ExecuteTestcases(test, "D105", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain d106 = (Quote.CreateNewQuote_Indirect.Distributor.D106.ScenarioMain)scenarioMain;
                    d106.ExecuteTestcases(test, "D106", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain d107 = (Quote.CreateNewQuote_Indirect.Distributor.D107.ScenarioMain)scenarioMain;
                    d107.ExecuteTestcases(test, "D107", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain d108 = (Quote.CreateNewQuote_Indirect.Distributor.D108.ScenarioMain)scenarioMain;
                    d108.ExecuteTestcases(test, "D108", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain d109 = (Quote.CreateNewQuote_Indirect.Distributor.D109.ScenarioMain)scenarioMain;
                    d109.ExecuteTestcases(test, "D109", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain d110 = (Quote.CreateNewQuote_Indirect.Distributor.D110.ScenarioMain)scenarioMain;
                    d110.ExecuteTestcases(test, "D110", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain d111 = (Quote.CreateNewQuote_Indirect.Distributor.D111.ScenarioMain)scenarioMain;
                    d111.ExecuteTestcases(test, "D111", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain d112 = (Quote.CreateNewQuote_Indirect.Distributor.D112.ScenarioMain)scenarioMain;
                    d112.ExecuteTestcases(test, "D112", testCaseNumber);
                }
                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain d113 = (Quote.CreateNewQuote_Indirect.Distributor.D113_D114.ScenarioMain)scenarioMain;
                    d113.ExecuteTestcases(test, "D113_D114", testCaseNumber);
                }

                else if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain d115 = (Quote.CreateNewQuote_Indirect.Distributor.D115.ScenarioMain)scenarioMain;
                    d115.ExecuteTestcases(test, "D115", testCaseNumber);
                }
                if (scenarioMain.GetType() == typeof(Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain))
                {
                    Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain d116 = (Quote.CreateNewQuote_Indirect.Distributor.D116.ScenarioMain)scenarioMain;
                    d116.ExecuteTestcases(test, scenarioId, testCaseNumber);
                }

                testCaseNumber++;
                if(Driver!=null)
                {
                   Driver.Close();
                }


            }
            TimeSpan d01endTime = DateTime.Now.TimeOfDay;
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("TimeElapse: {0} ", TimeElapse(d01startTime, d01endTime)), true);
            Console.WriteLine(scenarioId + "is Completed");
            Bedrock.Utilities.Console_PresentationLayer.Report_Run_Progress(string.Format("Scenario {0} Test Execution Completed.", scenarioId), true);
           
        }
    }
}

       
    
