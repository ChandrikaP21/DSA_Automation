using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
namespace SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData
{
    class TestDataReader
    {
        public static List<Scenario> _scenarios = new List<Scenario>();
        public static bool _IsEnvironmentConfigurationsLoaded = false;
        public static string _FilePath = string.Empty;
        public readonly string _stockAndSell = "StockAndSell";
        public readonly string _reSell = "ReSell";
        public readonly string _distributor = "Distributor";
        public static List<string> ApplicationsToTest;
        public TestDataReader()
        {
            if (_scenarios.Count > 0)
            {
                _scenarios.Clear();
                _IsEnvironmentConfigurationsLoaded = false;
                _FilePath = string.Empty;
            }

        }

        public List<Scenario> LoadXmlElements()
        {
            if (_IsEnvironmentConfigurationsLoaded == false)
            {
                _FilePath = ConfigurationManager.AppSettings["TestScenarios"].ToString();
              //  _FilePath = @"\\glonwspot360w.aus.amer.dell.com\gpttools\SP_DSA_UI_TEST_Report\DataXmlFiles\30032022160008737_D02_abc.xml";
                LoadXmlConfigurations();
            }
            return _scenarios;

        }

        private void LoadXmlConfigurations()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_FilePath);
            XmlNodeList xmlNodesEnvCollection = xmlDoc.SelectNodes("DataFeed/Scenarios/Scenario");
            XmlNode applicationsListNode = xmlDoc.SelectSingleNode("DataFeed/Applications");
            ApplicationsToTest = applicationsListNode.InnerText.Split(',').ToList<string>();
            if (xmlNodesEnvCollection != null)
            {

                // datafeed.Release = xmlNodesEnvCollection["Release"].InnerText;


                foreach (XmlNode item in xmlNodesEnvCollection)
                {
                    Scenario cs = new Scenario();
                    cs.ScenarioID = item.Attributes["ID"].Value;
                    cs.TestCases = new List<TestCase>();

                    foreach (XmlNode childnode in item.ChildNodes)
                    {
                        TestCase testCase = new TestCase();
                        //testCase.modules = new List<TestData_Module>();

                        switch (cs.ScenarioID)
                        {
                            case "D01":
                                ScenarioD01_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D02":
                                ScenarioD02_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D03":
                                ScenarioD03_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D04":
                                ScenarioD04_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D05":
                                ScenarioD05_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D06":
                                ScenarioD06_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D07":
                                ScenarioD07_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D08":
                                ScenarioD08_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D09":
                                ScenarioD09_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D10":
                                ScenarioD10_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D11":
                                ScenarioD11_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D13":
                                ScenarioD13_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D16":
                                ScenarioD16_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D17":
                                ScenarioD01_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D18":
                                ScenarioD02_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D19":
                                ScenarioD03_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D20":
                                ScenarioD04_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D21":
                                ScenarioD05_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D22":
                                ScenarioD06_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D23":
                                ScenarioD07_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D24":
                                ScenarioD08_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D25":
                                ScenarioD09_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D26":
                                ScenarioD10_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D27_D28":
                                ScenarioD11_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D29":
                                ScenarioD13_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D32":
                                ScenarioD16_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D33":
                                ScenarioD33_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D34":
                                ScenarioD34_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D35":
                                ScenarioD35_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D36":
                                ScenarioD36_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D37":
                                ScenarioD37_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D38":
                                ScenarioD38_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D39":
                                ScenarioD39_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D40":
                                ScenarioD40_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D41":
                                ScenarioD41_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D42":
                                ScenarioD42_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D43-D44":
                                ScenarioD43_D44LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D45":
                                ScenarioD45_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D48":
                                ScenarioD48_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D49":
                                ScenarioD01_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D50":
                                ScenarioD02_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D51":
                                ScenarioD03_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D52":
                                ScenarioD04_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D53":
                                ScenarioD05_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D54":
                                ScenarioD06_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D55":
                                ScenarioD07_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D56":
                                ScenarioD08_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D57":
                                ScenarioD09_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D58":
                                ScenarioD10_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D59_D60":
                                ScenarioD11_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D61":
                                ScenarioD13_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D62":
                                ScenarioD16_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D63":
                                ScenarioD01_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D64":
                                ScenarioD02_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D65":
                                ScenarioD03_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D66":
                                ScenarioD04_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D67":
                                ScenarioD05_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D68":
                                ScenarioD06_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D69":
                                ScenarioD07_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D70":
                                ScenarioD08_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D71":
                                ScenarioD09_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D72":
                                ScenarioD10_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D73_D74":
                                ScenarioD11_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D75":
                                ScenarioD13_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D76":
                                ScenarioD16_LoadTestCase(ref testCase, childnode, cs.ScenarioID);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D77":
                                ScenarioD33_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null,_stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D78":
                                ScenarioD34_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D79":
                                ScenarioD35_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D80":
                                ScenarioD36_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D81":
                                ScenarioD37_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D82":
                                ScenarioD38_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null,_stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D83":
                                ScenarioD39_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D84":
                                ScenarioD40_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D85":
                                ScenarioD41_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D86":
                                ScenarioD42_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D87_D88":
                                ScenarioD43_D44LoadTestCase(ref testCase, childnode, cs.ScenarioID, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D89":
                                ScenarioD45_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null, _stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D90":
                                ScenarioD48_LoadTestCase(ref testCase, childnode, cs.ScenarioID,null,_stockAndSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D91":
                                ScenarioD34_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D92":
                                ScenarioD35_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D93":
                                ScenarioD36_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D94":
                                ScenarioD37_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D95":
                                ScenarioD38_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D96":
                                ScenarioD39_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D97":
                                ScenarioD40_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D98":
                                ScenarioD41_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D99":
                                ScenarioD42_LoadTestCase(ref testCase, childnode, cs.ScenarioID,_reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D100_D101":
                                ScenarioD43_D44LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D102":
                                ScenarioD45_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D103":
                                ScenarioD48_LoadTestCase(ref testCase, childnode, cs.ScenarioID, _reSell);
                                cs.TestCases.Add(testCase);
                                break;


                            case "D104":
                                ScenarioD34_LoadTestCase(ref testCase, childnode, cs.ScenarioID,"","",_distributor);
                                cs.TestCases.Add(testCase);
                                break;
                                
                            case "D105":
                                ScenarioD35_LoadTestCase(ref testCase, childnode, cs.ScenarioID,"","",_distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D106":
                                ScenarioD36_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D107":
                                ScenarioD37_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D108":
                                ScenarioD38_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D109":
                                ScenarioD39_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D110":
                                ScenarioD40_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D111":
                                ScenarioD41_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D112":
                                ScenarioD42_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D113_D114":
                                ScenarioD43_D44LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D115":
                                ScenarioD45_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                            case "D116":
                                ScenarioD48_LoadTestCase(ref testCase, childnode, cs.ScenarioID, "", "", _distributor);
                                cs.TestCases.Add(testCase);
                                break;
                           

                            default:
                                break;
                        }



                        


                    }
                    _scenarios.Add(cs);

                }
                _IsEnvironmentConfigurationsLoaded = true;
            }

        }

        private void ScenarioD48_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode standardPartnerDiscount = childnode.SelectSingleNode("StandardPartnerDiscount"); 
                testCase.StandardPartnerDiscountType = standardPartnerDiscount["StandardPartnerDiscountType"].InnerText;
                testCase.ProductIdentificationData = standardPartnerDiscount["ProductIdentificationData"].InnerText;
            }
            catch (Exception ex)
            {
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD45_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode priceChangeNode = childnode.SelectSingleNode("PriceChange");
                testCase.PriceChangeType = priceChangeNode["PriceChangeType"].InnerText;
                testCase.ProductIdentificationData = priceChangeNode["ProductIdentificationData"].InnerText;

                //testCase.IsSellingPriceBased = priceChangeNode["IsSellingPriceBased"].InnerText;
                //testCase.SellingPrice = priceChangeNode["SellingPrice"].InnerText;
                //testCase.Discount = priceChangeNode["Discount"].InnerText;


            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD43_D44LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode sfdc = childnode.SelectSingleNode("SFDCIdToPatch");
                testCase.IsSFDCIdentificationDataAutomatic = sfdc["IsSFDCIdentificationDataAutomatic"].InnerText;
                testCase.SFDCIdentificationQuery = sfdc["SFDCIdentificationQuery"].InnerText;
                testCase.SFDCIdentificationData = sfdc["SFDCIdentificationData"].InnerText;


            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD42_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null,string distributor=null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsNewCustomerIdentificationQueryBased = childnode["IsNewCustomerIdentificationQueryBased"].InnerText;
                testCase.NewCustomerIdentificationQuery = childnode["NewCustomerIdentificationQuery"].InnerText;
                testCase.NewCustomerIdentificationData = childnode["NewCustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell"|| distributor=="Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode Customer = childnode.SelectSingleNode("EndUserCustomerIdToPatch");
                testCase.CustomerIdentificationQuery = Customer["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = Customer["CustomerIdentificationData"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = Customer["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.IsCustomerInSameSegment = Customer["IsCustomerInSameSegment"].InnerText;
                testCase.CrossSegmenId = Customer["CrossSegmenId"].InnerText;



            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD41_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;

                // XmlNodeList quantityList = childnode.ChildNodes[4].ChildNodes;
                XmlNode quantityChangeNode = childnode.SelectSingleNode("QuantityChange");

                //switch (quantityChangeNode.SelectSingleNode("Quantity").Attributes["ChangeType"].Value)
                //{
                //    case "AnyProduct":
                //testCase.QuantityChangeType = QuantityChangeType.AnyProduct;
                testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                //testCase.Quantity_AnyProduct = qntType["quantity"].InnerText;








            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD40_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;

                // XmlNodeList quantityList = childnode.ChildNodes[4].ChildNodes;
                XmlNode quantityChangeNode = childnode.SelectSingleNode("QuantityChange");

                //switch (quantityChangeNode.SelectSingleNode("Quantity").Attributes["ChangeType"].Value)
                //{
                //    case "AnyProduct":
                //testCase.QuantityChangeType = QuantityChangeType.AnyProduct;
                testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                //testCase.Quantity_AnyProduct = qntType["quantity"].InnerText;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD39_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;

                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");


                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode service = baseProduct.SelectSingleNode("Services").SelectSingleNode("Module");
                testCase.ModuleId = service["Id"].InnerText; //--5113 represents the module ID for monitors-- >
                testCase.OfferingId = service["ServiceId"].InnerText;//if left blank the first accessory in the list under the module shall be picked up



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD38_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {

                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;

                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");


                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode service = baseProduct.SelectSingleNode("Services").SelectSingleNode("Module");
                testCase.ModuleId = service["Id"].InnerText;
                testCase.OfferingId = service["ServiceId"].InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD37_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID,string reSell = null,string stockAndSell = null,string distributor= null)
        {
            try
            {


                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");


                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode service = baseProduct.SelectSingleNode("Services").SelectSingleNode("Module");
                testCase.ModuleId = service["Id"].InnerText;
                testCase.OfferingId = service["ServiceId"].InnerText;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD36_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null,string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");


                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode Accessory = baseProduct.SelectSingleNode("Accessories").SelectSingleNode("Module");
                //read and add base product level proerties into the tescase
                testCase.ModuleId = Accessory["Id"].InnerText;
                testCase.AccessoriesId = Accessory["AccessoryID"].InnerText;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD35_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null,string distributor=null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");


                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                XmlNode Component = baseProduct.SelectSingleNode("Component").SelectSingleNode("Module");
                testCase.ModuleId = Component.SelectSingleNode("Id").InnerText;
                //     testCase.ModuleId = Component["Id"].InnerText;
                testCase.ComponentId = Component["ComponentId"].InnerText;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ScenarioD34_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID,string reSell=null,string stockAndSell=null,string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell" || distributor == "Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");

               

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                //reaad and add base product level proerties into the tescase
                //testCase.addProduct.Add(product);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }

        private void ScenarioD33_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID, string reSell = null, string stockAndSell = null, string distributor = null)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = childnode["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.CustomerIdentificationQuery = childnode["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = childnode["CustomerIdentificationData"].InnerText;
                if (reSell == "ReSell" || stockAndSell == "StockAndSell"||distributor=="Distributor")
                {
                    testCase.AccountCustomerId = childnode["AccountCustomerId"].InnerText;
                    testCase.EndCustomerId = childnode["EndCustomerId"].InnerText;
                    testCase.SFDCID = childnode["SFDCID"].InnerText;
                }
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");



                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;
                //reaad and add base product level proerties into the tescase
                //testCase.addProduct.Add(product);
                //}
            }
            catch (Exception ex)
            {
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD01_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

            }
            catch (Exception ex)
            {
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD02_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                XmlNode productIdentificaton = childnode.SelectSingleNode("ProductIdentificaton");
                XmlNode baseProduct = productIdentificaton.SelectSingleNode("BaseProduct");
                if (scenarioID.ToLower().Contains("d02"))
                {
                    testCase.ApprovalType = childnode["ApprovalType"].InnerText;
                    testCase.Approver = childnode["Approver"].InnerText;
                    testCase.AffinityAccountGroupID = childnode["AffinityAccountGroupID"].InnerText;
                    testCase.ProductType = childnode["ProductType"].InnerText;
                }

                testCase.IsIdentifiedByQuery = baseProduct["IsIdentifiedByQuery"].InnerText;
                testCase.ProductIdentificationQuery = baseProduct["ProductIdentificationQuery"].InnerText;
                testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                testCase.SystemSearch = baseProduct["SystemSearch"].InnerText;
                testCase.SAndPSearch = baseProduct["SAndPSearch"].InnerText;



                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }

        private void ScenarioD03_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                XmlNodeList baseProductlist = childnode.ChildNodes[4].ChildNodes;

                foreach (XmlNode baseProduct in baseProductlist)
                {
                    testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                    testCase.UpdateServices = baseProduct["Component"].Attributes["Type"].Value;
                    XmlNode Component = baseProduct.ChildNodes[1].FirstChild;
                    testCase.ModuleId = Component["Id"].InnerText;
                    testCase.ComponentId = Component["ComponentId"].InnerText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void ScenarioD04_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                //      testCase.TestCaseTitle = childnode.SelectSingleNode("TestCaseTitle").InnerText;
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

                XmlNodeList baseProductlist = childnode.ChildNodes[4].ChildNodes;

                foreach (XmlNode baseProduct in baseProductlist)
                {

                    testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                    testCase.UpdateAccessories = baseProduct["Accessories"].Attributes["Type"].Value;
                    XmlNode Accessory = baseProduct.ChildNodes[1].FirstChild;
                    //reaad and add base product level proerties into the tescase
                    testCase.ModuleId = Accessory["Id"].InnerText;
                    testCase.AccessoriesId = Accessory["AccessoryID"].InnerText;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD05_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {


                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;

                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

                XmlNodeList baseProductlist = childnode.ChildNodes[4].ChildNodes;


                foreach (XmlNode baseProduct in baseProductlist)
                {

                    testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                    testCase.UpdateServices = baseProduct["Services"].Attributes["Type"].Value;
                    XmlNode component = baseProduct.ChildNodes[1].FirstChild;
                    testCase.ModuleId = component["Id"].InnerText;
                    testCase.OfferingId = component["ServiceId"].InnerText;


                }
                //    XmlNodeList serviceList = baseProduct.ChildNodes[1].ChildNodes;

                //    //read and add base product level proerties into the tescase
                //    foreach (XmlNode module in serviceList)
                //    {
                //        TestData_Module testModule = new TestData_Module();

                //        testModule.ModuleId = module["Id"].InnerText; //--5113 represents the module ID for monitors-- >
                //        testModule.OfferingId = module["ServiceId"].InnerText;//if left blank the first accessory in the list under the module shall be picked up
                //        testCase.modules.Add(testModule);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD06_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {

                
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
              
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

                XmlNodeList baseProductlist = childnode.ChildNodes[4].ChildNodes;


                foreach (XmlNode baseProduct in baseProductlist)
                {

                    testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                    testCase.UpdateServices = baseProduct["Services"].Attributes["Type"].Value;
                    XmlNode service = baseProduct.ChildNodes[1].FirstChild;
                    testCase.ModuleId = service["Id"].InnerText;
                    testCase.OfferingId = service["ServiceId"].InnerText;

                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD07_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {

                //      testCase.TestCaseTitle = childnode.SelectSingleNode("TestCaseTitle").InnerText;
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

                XmlNodeList baseProductlist = childnode.ChildNodes[4].ChildNodes;


                foreach (XmlNode baseProduct in baseProductlist)
                {

                    testCase.ProductIdentificationData = baseProduct["ProductIdentificationData"].InnerText;
                    testCase.UpdateServices = baseProduct["Services"].Attributes["Type"].Value;
                    XmlNode service = baseProduct.ChildNodes[1].FirstChild;
                    //read and add base product level proerties into the tescase
                    //foreach (XmlNode module in serviceList)
                    //{
                    //    TestData_Module testModule = new TestData_Module();

                    testCase.ModuleId = service["Id"].InnerText; //--5113 represents the module ID for monitors-- >
                    testCase.OfferingId = service["ServiceId"].InnerText;//if left blank the first accessory in the list under the module shall be picked up

                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD08_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                // testCase.QuantityChange = childnode["QuantityChange"].Attributes["Type"].Value;

                // XmlNodeList quantityList = childnode.ChildNodes[4].ChildNodes;
                XmlNode quantityChangeNode = childnode.SelectSingleNode("QuantityChange");

                switch (quantityChangeNode.SelectSingleNode("Quantity").Attributes["ChangeType"].Value)
                {
                    case "AnyProduct":
                        testCase.QuantityChangeType = QuantityChangeType.AnyProduct;
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                        //testCase.Quantity_AnyProduct = qntType["quantity"].InnerText;

                        break;
                    case "AllProducts":

                        testCase.QuantityChangeType = QuantityChangeType.AllProducts;
                        //     testCase.Quantity = Convert.ToInt32(quantityChangeNode.InnerText);
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);

                        break;
                    case "ForSpecificProduct":
                        testCase.QuantityChangeType = QuantityChangeType.ForSpecificProduct;
                        //       testCase.Quantity = Convert.ToInt32(quantityChangeNode.InnerText);
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                        testCase.OrderCode = quantityChangeNode.SelectSingleNode("Ordercode").InnerText.ToString();

                        break;
                    default:
                        break;

                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD09_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                // testCase.QuantityChange = childnode["QuantityChange"].Attributes["Type"].Value;

                // XmlNodeList quantityList = childnode.ChildNodes[4].ChildNodes;
                XmlNode quantityChangeNode = childnode.SelectSingleNode("QuantityChange");

                switch (quantityChangeNode.SelectSingleNode("Quantity").Attributes["ChangeType"].Value)
                {
                    case "AnyProduct":
                        testCase.QuantityChangeType = QuantityChangeType.AnyProduct;
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                        //testCase.Quantity_AnyProduct = qntType["quantity"].InnerText;

                        break;
                    case "AllProducts":

                        testCase.QuantityChangeType = QuantityChangeType.AllProducts;
                        //     testCase.Quantity = Convert.ToInt32(quantityChangeNode.InnerText);
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);

                        break;
                    case "ForSpecificProduct":
                        testCase.QuantityChangeType = QuantityChangeType.ForSpecificProduct;
                        //       testCase.Quantity = Convert.ToInt32(quantityChangeNode.InnerText);
                        testCase.Quantity = Convert.ToInt32(quantityChangeNode.SelectSingleNode("Quantity").InnerText);
                        testCase.OrderCode = quantityChangeNode.SelectSingleNode("Ordercode").InnerText.ToString();

                        break;
                    default:
                        break;

                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD10_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                XmlNode Customer = childnode.ChildNodes[4];
                testCase.CustomerIdentificationQuery = Customer["CustomerIdentificationQuery"].InnerText;
                testCase.CustomerIdentificationData = Customer["CustomerIdentificationData"].InnerText;
                testCase.IsCustomerIdentificationQueryBased = Customer["IsCustomerIdentificationQueryBased"].InnerText;
                testCase.IsCustomerInSameSegment = Customer["IsCustomerInSameSegment"].InnerText;
                testCase.CrossSegmenId = Customer["CrossSegmenId"].InnerText;



            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD11_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                XmlNode sfdc = childnode.ChildNodes[4];
                testCase.IsSFDCIdentificationDataAutomatic = sfdc["IsSFDCIdentificationDataAutomatic"].InnerText;
                testCase.SFDCIdentificationQuery = sfdc["SFDCIdentificationQuery"].InnerText;
                testCase.SFDCIdentificationData = sfdc["SFDCIdentificationData"].InnerText;


            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
        private void ScenarioD13_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;
                XmlNode priceChangeNode = childnode.ChildNodes[4];
                testCase.PriceChangeType = priceChangeNode["PriceChangeType"].InnerText;
                testCase.ProductIdentificationData = priceChangeNode["ProductIdentificationData"].InnerText;
                
                //testCase.IsSellingPriceBased = priceChangeNode["IsSellingPriceBased"].InnerText;
                //testCase.SellingPrice = priceChangeNode["SellingPrice"].InnerText;
                //testCase.Discount = priceChangeNode["Discount"].InnerText;


            }
            catch (Exception ex)
            {

                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }
        }
        private void ScenarioD16_LoadTestCase(ref TestCase testCase, XmlNode childnode, string scenarioID)
        {
            try
            {
                testCase.TestCaseTitle = childnode["TestCaseTitle"].InnerText;
                // testCase.BaseQuoteQuery = childnode["BaseQuoteQuery"].InnerText;
                testCase.IsBaseQuoteIdentificationQueryBased = childnode["IsBaseQuoteIdentificationQueryBased"].InnerText;
                testCase.BaseQuoteIdentificationQUERY = childnode["BaseQuoteIdentificationQUERY"].InnerText;
                testCase.BaseQuoteIdentificationDATA = childnode["BaseQuoteIdentificationDATA"].InnerText;

                XmlNode standardPartnerDiscount = childnode.ChildNodes[4];
                testCase.StandardPartnerDiscountType = standardPartnerDiscount["StandardPartnerDiscountType"].InnerText;
                testCase.ProductIdentificationData = standardPartnerDiscount["ProductIdentificationData"].InnerText;
            }
            catch (Exception ex)
            {
                //report in log file that there was an errror loading a test case under scenario scenarioID and error = ex.message
            }

        }
    }
}
