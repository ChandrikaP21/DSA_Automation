
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;

using System.IO;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using EMEA_SmartPrice_E2E_WebAutomation.Pages.GOAL;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using EMEA_SmartPrice_E2E_WebAutomation.Helper;
using System.Configuration;
using LATAM_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using LATAM_SmartPrice_E2E_WebAutomation.Objects;

namespace EMEA_SmartPrice_E2E_WebAutomation.Reports
{
    public class ResultLogging : Constant
    {

        public string filename_Data_Source = string.Empty;
        public string FilePath = string.Empty;
       public string rootfolder = ConfigurationManager.AppSettings["CsvReportPath"].ToString();
       //public string rootfolder = @"\\glonwspot360w.aus.amer.dell.com\gpttools\SP_DSA_UI_TEST_Report";

        public void GenerateReportInCSV(DSAPageObject dsaObject, List<DSAPageObject> retriveSmartPriceData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber, DSAQuoteSummaryObject quoteSummaryObject = null, GoalObject goalDetails = null)
        {

            if (quoteSummaryObject != null && goalDetails != null)
                GenerateReportInCSV(dsaObject, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", null, null, null, false, false, quoteSummaryObject, goalDetails);
            else GenerateReportInCSV(dsaObject, retriveSmartPriceData, totalFinancialCalculation, scenarioId, testCaseNumber, "", null, null, null);

            return;

        }


        public void GenerateReportInCSV(DSAPageObject dsaObject, List<DSAPageObject> retriveSmartPriceData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber, string refreshID, CustomerClass customerObj, List<PNRAPILog> sp1Log, List<PNRAPILog> sp2Log, bool isindirect = false, bool IsStandardPartnerDiscount = false, DSAQuoteSummaryObject quoteSummaryObject = null, GoalObject goalDetails = null)
        {
            
            randomNumber = RandomNumber(1000, 10000).ToString();
            if (!Directory.Exists(rootfolder))
            {
                Directory.CreateDirectory(rootfolder);
            }
            //string s = retriveSmartPriceData.Select(x => x.LocalChannelId).First()+"_"+dsaObject.country;
            string subFolderPath = StaticBriefCase.CsvReportName;//string.Format("{0}{1}{2}{3}{4}{5}", rootfolder, "\\", "R_" + s + "_" + scenarioId + "_", runId, "\\", scenarioId + "_" + runId);
            string subFolder = Path.Combine(rootfolder, subFolderPath);

            if (!Directory.Exists(subFolder))
            {
                Directory.CreateDirectory(subFolder);
            }

            if (!string.IsNullOrEmpty(refreshID))
            {
                refreshID = string.Format("_{0}", refreshID);
            }
            filename_Data_Source = scenarioId + "_" + "TC" + testCaseNumber + refreshID + "_" + currentdate + ".csv";
            //  FilePath = string.Format("{0}{1}{2}", rootfolder, "\\", filename_Data_Source);
            //currentdate
            FilePath = string.Format("{0}{1}{2}", subFolderPath, "\\", filename_Data_Source);
            bool is_fileExists = File.Exists(FilePath);

            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            int count = 1;
            if (is_fileExists == false)

                try
                {

                    // *******Exporting Header in CSV - Coma Seperated
                    foreach (DSAPageObject report in retriveSmartPriceData)
                    {

                        if (count == 1)
                        {
                            sb1.AppendLine(string.Format("{0}{1}", "ActualQuoteNumber,", dsaObject.ActualQuote));
                            sb1.AppendLine(string.Format("{0}{1}", "NewQuoteNumber,", report.QuoteNumber));
                            sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Suite Identified,", "validation"));
                            if (isindirect == true)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", "Partner Account,", customerObj.OldAccountCustomerId));
                                sb1.AppendLine(string.Format("{0}{1}", "EndUserAccount,", customerObj.OldEndUserAccountCustomerId));
                            }
                            //SFDC details....................
                            //sb1.AppendLine(string.Format("{0}{1}{2}", ",,Calculated,", "Calculated,", "Validation"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealID,,", dsaObject.PnrLog.SFDCDEALID, ",", report.PnrLog.SFDCDEALID, ",", dsaObject.PnrLog.IsSFDCDEALID));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCUnassEndUserC,,", dsaObject.PnrLog.SFDCUNASSIGNEDENDUSERC, ",", report.PnrLog.SFDCUNASSIGNEDENDUSERC, ",", dsaObject.PnrLog.IsSFDCUNASSIGNEDENDUSERC));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyProbability,,", dsaObject.PnrLog.SFDCOPPTYPROBABILITY, ",", report.PnrLog.SFDCOPPTYPROBABILITY, ",", dsaObject.PnrLog.IsSFDCOPPTYPROBABILITY));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCType,,", dsaObject.PnrLog.SFDCTYPE, ",", dsaObject.PnrLog.SFDCTYPE, ",", report.PnrLog.IsSFDCTYPE));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCRegistrationStatus,,", dsaObject.PnrLog.SFDCREGISTRATIONSTATUS, ",", report.PnrLog.SFDCREGISTRATIONSTATUS, ",", dsaObject.PnrLog.IsSFDCREGISTRATIONSTATUS));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCFulfilmentPath,,", dsaObject.PnrLog.SFDCFULFILMENTPATH, ",", report.PnrLog.SFDCFULFILMENTPATH, ",", dsaObject.PnrLog.IsSFDCFULFILMENTPATH));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealTypeC,,", dsaObject.PnrLog.SFDCDEALTYPEC, ",", report.PnrLog.SFDCDEALTYPEC, ",", dsaObject.PnrLog.IsSFDCDEALTYPEC));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyStage,,", dsaObject.PnrLog.SFDCOPPTYSTAGE.Replace(",", " or"), ",", report.PnrLog.SFDCOPPTYSTAGE.Replace(",", " or"), ",", dsaObject.PnrLog.IsSFDCOPPTYSTAGE));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyBookedDate,,", dsaObject.PnrLog.SFDCOPPTYBOOKEDDATE, ",", report.PnrLog.SFDCOPPTYBOOKEDDATE, ",", dsaObject.PnrLog.IsSFDCOPPTYBOOKEDDATE));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealRegOppty,,", dsaObject.PnrLog.SFDCDEALREGOPPTY, ",", report.PnrLog.SFDCDEALREGOPPTY, ",", dsaObject.PnrLog.IsSFDCDEALREGOPPTY));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyRecordType,,", dsaObject.PnrLog.SFDCOPPTYRECORDTYPE, ",", report.PnrLog.SFDCOPPTYRECORDTYPE, ",", dsaObject.PnrLog.IsSFDCOPPTYRECORDTYPE));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDistrAccountID,,", dsaObject.PnrLog.SFDCDISTIACCOUNTID, ",", report.PnrLog.SFDCDISTIACCOUNTID, ",", dsaObject.PnrLog.IsSFDCDISTIACCOUNTID));

                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "ValidOpportunity", ",", dsaObject.PnrLog.ValidOppportunitySPDecision, ",", dsaObject.ValidOppportunitySuiteDecision, ",", dsaObject.OpportunityStatus));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "OpportunityMessageDisplayed", ",", dsaObject.IsSfdcMessageDisplayed, ",", null, ",", dsaObject.SfdcMessageDisplayed));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "OpportunitySize,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "BookType,", ",Future release", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Stage,", ",Future release", report.PnrLog.SFDCOPPTYSTAGE, ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "OpportunityType,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "RecordType,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}", ",,OldValue,", "NewValue,", "Validation"));
                            if (scenarioId == "D10" || isindirect == true || customerObj != null)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId ,", "," + customerObj.OldAccountCustomerId + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "AccountId ,", "," + customerObj.OldAccountId + "," + customerObj.NewAccountId + "," + customerObj.IsAccountIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "QuoteAccountId ,", "," + customerObj.OldQuoteAccountId + "," + customerObj.NewQuoteAccountId + "," + customerObj.IsQuoteAccountIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "EndUserAccountCustomerId ,", "," + customerObj.OldEndUserAccountCustomerId + "," + customerObj.NewEndUserAccountCustomerId + "," + customerObj.IsEndUserAccountCustomerIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId,", "," + customerObj.OldAccountCustomerId + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                            }
                            sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Calculated,", "Validation"));


                            sb1.AppendLine(string.Format("{0}{1}", "TotalListPriceSummaryLevel,", "," + report.SummaryTotalListPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsListPricMatchSummaryLevel));
                            sb1.AppendLine(string.Format("{0}{1}", "TotalSellingPriceSummaryLevel,", "," + report.SummarySellingPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                            //sb1.AppendLine(string.Format("{0}{1}", "TotalSmartPriceRevenue,", "," + report.smart.Replace(",", "") + "," + Math.Round(report.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "TotalModifiedRevenue,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}", "TotalPricingModifierSummeryLevel,", "," + report.SummaryPricingModifier.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2) + "," + report.comparisionObjects.IsPricingModifierMatchSummaryLevel));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithServicesIncentive,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithDFSIncentive,", ",Future release", ",Future release", ",Future release"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                        }

                        sb1.AppendLine(string.Format("{0}{1}", "BaseProduct,", +count));
                        // added dsaObject.comparisionObjects.CalDiscountedPricePerUnit during D13 to dispaly calculated discount.
                        CreateCSVForLineItemLevel(sb1, report, sp1Log, sp2Log, scenarioId, dsaObject);
                        if (IsStandardPartnerDiscount)
                        {
                            sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Base Product"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                        }
                        CreateCSVForCommonGuidance(sb1, report, sp1Log, sp2Log, scenarioId, dsaObject, IsStandardPartnerDiscount);
                        foreach (DSAPageObject accessory in report.Accessories)
                        {
                            sb1.AppendLine(string.Format("{0}{1}", ",Accessories,", +count));
                            CreateCSVForAccessoriesLineItemLevel(sb1, accessory);//accessories line item
                            sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Accessories"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                            CreateCSVForAccessoriesGuidance(sb1, accessory);
                        }

                        File.AppendAllText(FilePath, sb1.ToString());

                        sb1.Clear();
                        count++;
                    }
                    if (quoteSummaryObject != null && goalDetails != null)
                    {
                        PrintGoalLiteResults(quoteSummaryObject, goalDetails,sb1,count);
                        

                    }


                    // Console.WriteLine(string.Format("CSV file {0} has created", currentdate));
                    Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("CSV file {0} has created", filename_Data_Source), true);
                    FileOperationClass.CopyDirectory(ConfigurationManager.AppSettings["SnapShot"].ToString(), FilePath);
                }
                catch (IOException ex)
                {
                    Bedrock.Utilities.SimpleLogger.LogMessage(" IO Exception found in Calculate () in ResultLogging class" + ex.Message);
                    Bedrock.Utilities.SimpleLogger.LogMessage("IO Exception found in Calculate () in ResultLogging class" + ex.StackTrace);
                }
                catch (ArithmeticException ex)
                {
                    Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in ResultLogging class" + ex.Message);
                    Bedrock.Utilities.SimpleLogger.LogMessage("Arithmatic error found in Calculate () in ResultLogging class" + ex.StackTrace);

                }
                catch (Exception ex)
                {
                    Bedrock.Utilities.SimpleLogger.LogMessage("Unknown error found in Calculate () in ResultLogging class" + ex.Message);
                    Bedrock.Utilities.SimpleLogger.LogMessage("Unknow error found in Calculate () in ResultLogging class" + ex.StackTrace);

                }
                
                

        }

        private void PrintGoalLiteResults(DSAQuoteSummaryObject quoteSummaryObject, GoalObject goalDetails,StringBuilder sb1,int count)
        {
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
            sb1.AppendLine(string.Format("{0}", ",GOALLITE Details"));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
            sb1.AppendLine(string.Format("{0}{1}", "Goal approval type:,", goalDetails.ApprovalType));
            sb1.AppendLine(string.Format("{0}{1}", "Goal approved by:,", goalDetails.Approver));
            sb1.AppendLine(string.Format("{0}{1}", "Goal has product type:,", goalDetails.ProductType));
            sb1.AppendLine(string.Format("{0}{1}", "Goal has AffinityAccountGroupID:,", goalDetails.AffinityAccountGroupID));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
            sb1.AppendLine(string.Format("{0}", "Financials used during creating Approval/Denial policy Details"));
            sb1.AppendLine(string.Format("{0}{1}{2}", "Financial Component,", "Upper Limit,", "Lower Limit"));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Revenue,", GenericHelper.SetLimit(quoteSummaryObject.QuoteListPrice), "," + GenericHelper.SetLimit(quoteSummaryObject.QuoteListPrice, false), ","));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Margin,", GenericHelper.SetLimit(quoteSummaryObject.QuoteTotalMargin), "," + GenericHelper.SetLimit(quoteSummaryObject.QuoteTotalMargin, false), ","));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "DOL,", GenericHelper.SetLimit(quoteSummaryObject.QuoteDiscountInPercentage), "," + GenericHelper.SetLimit(quoteSummaryObject.QuoteDiscountInPercentage, false), ","));

            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
            sb1.AppendLine(string.Format("{0}{1}{2}", "DSA Quote Details,", "Goal Lite Details,", "Validation"));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "QuoteNumber,", quoteSummaryObject.QuoteNumber, "," + goalDetails.QuoteNumber, "," + CompareStrings(quoteSummaryObject.QuoteNumber, goalDetails.QuoteNumber)));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "List Price,", quoteSummaryObject.QuoteListPrice, "," + goalDetails.ListPrice, "," +
                CompareStrings(quoteSummaryObject.QuoteListPrice, goalDetails.ListPrice)));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Selling Price,", quoteSummaryObject.QuoteSellingPrice, "," + goalDetails.TotalNSP, "," +
                CompareStrings(quoteSummaryObject.QuoteSellingPrice, goalDetails.TotalNSP)));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Discount in Percentage,", quoteSummaryObject.QuoteDiscountInPercentage, "," + goalDetails.TotalDOLinPercentage, "," +
                CompareStrings(quoteSummaryObject.QuoteDiscountInPercentage, goalDetails.TotalDOLinPercentage)));
            //sb1.AppendLine(string.Format("{0}{1}{2}{3}", "QuoteNumber,", "," + quoteSummaryObject.QuoteNumber, "," + goalDetails.QuoteNumber, "," + "true"));
            //sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSA Quote Details,", "Goal Lite Details,", "Validation"));
            //sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSA Quote Details,", "Goal Lite Details,", "Validation"));
            // *******Exporting Header in CSV - Coma Seperated
            for (int i = 0; i < quoteSummaryObject.ProductDetails.Count; i++)
            {
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSA Quote Product Details,", "Goal Lite Quote Product Details,", "Validation"));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Product Description,", "," + quoteSummaryObject.ProductDetails[i].ProductDescription, "," + goalDetails.ProductDetails[i].ProductDescription, "," +
                    CompareStrings(quoteSummaryObject.ProductDetails[i].ProductDescription, goalDetails.ProductDetails[i].ProductDescription)));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Unit List Price,", "," + quoteSummaryObject.ProductDetails[i].UnitListPrice, "," + goalDetails.ProductDetails[i].UnitListPrice, "," +
                    CompareStrings(quoteSummaryObject.ProductDetails[i].UnitListPrice, goalDetails.ProductDetails[i].UnitListPrice)));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Selling Price,", "," + quoteSummaryObject.ProductDetails[i].SellingPrice, "," + goalDetails.ProductDetails[i].SellingPrice, "," +
                    CompareStrings(quoteSummaryObject.ProductDetails[i].SellingPrice, goalDetails.ProductDetails[i].SellingPrice)));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Unit Price,", "," + quoteSummaryObject.ProductDetails[i].UnitPrice, "," + goalDetails.ProductDetails[i].UnitPrice, "," +
                    CompareStrings(quoteSummaryObject.ProductDetails[i].UnitPrice, goalDetails.ProductDetails[i].UnitPrice)));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "QuoteNumber,", "," + quoteSummaryObject.ProductDetails[i].Quantity, "," + goalDetails.ProductDetails[i].Quantity, "," +
                    CompareStrings(quoteSummaryObject.ProductDetails[i].Quantity, goalDetails.ProductDetails[i].Quantity)));
                File.AppendAllText(FilePath, sb1.ToString());

                sb1.Clear();
                count++;
            }
        }

        public string CompareStrings(string actual, string expected)
        {
            string t = (String.Equals(actual, expected)) ? "true" : "false";
            return t;
        }
        /// <summary>
        /// Scenario-13 reporting
        /// </summary>
        /// <param name="dsaObject"></param>
        /// <param name="retriveSmartPriceData"></param>
        /// <param name="totalFinancialCalculation"></param>
        /// <param name="scenarioId"></param>
        /// <param name="testCaseNumber"></param>
        /// <param name="runId"></param>
        /// <param name="refreshID"></param>
        /// <param name="customerObj"></param>
        /// <param name="IsScenarioD13"></param>
        public void GenerateReportInCSV(DSAPageObject dsaObject, List<DSAPageObject> retriveSmartPriceData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber, string refreshID, CustomerClass customerObj, bool IsScenarioD13, string pricePointChange, bool isindirect = false, bool IsStandardPartnerDiscount = false, DSAQuoteSummaryObject quoteSummaryObject = null, GoalObject goalDetails = null)
        {
            if (IsScenarioD13)
            {
                randomNumber = RandomNumber(1000, 10000).ToString();


                if (!Directory.Exists(rootfolder))
                {
                    Directory.CreateDirectory(rootfolder);

                }
                string s = retriveSmartPriceData.Select(x => x.LocalChannelId).First();
                string subFolderPath1 = StaticBriefCase.CsvReportName;//string.Format("{0}{1}{2}{3}", "R_" + s + "_" + scenarioId + "", runId, "\\", scenarioId + "_" + runId);
                string subFolder1 = Path.Combine(rootfolder, subFolderPath1);
                if (!Directory.Exists(subFolder1))
                {
                    Directory.CreateDirectory(subFolder1);
                }

                string subFolderPath2 = string.Format("{0}", pricePointChange);
                string subFolder2 = Path.Combine(subFolder1, subFolderPath2);

                if (!Directory.Exists(subFolder2))
                {
                    Directory.CreateDirectory(subFolder2);
                }

                if (!string.IsNullOrEmpty(refreshID))
                {
                    refreshID = string.Format("_{0}", refreshID);
                }
                if (!string.IsNullOrEmpty(pricePointChange))
                {
                    pricePointChange = string.Format("_{0}", pricePointChange);
                }
                filename_Data_Source = scenarioId + "_" + "TC" + testCaseNumber + refreshID + pricePointChange + "_" + currentdate + ".csv";
                //  FilePath = string.Format("{0}{1}{2}", rootfolder, "\\", filename_Data_Source);
                //currentdate
                FilePath = string.Format("{0}{1}{2}", subFolder2, "\\", filename_Data_Source);
                bool is_fileExists = File.Exists(FilePath);

                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                int count = 1;
                if (is_fileExists == false)
                    try
                    {

                        // *******Exporting Header in CSV - Coma Seperated
                        foreach (DSAPageObject report in retriveSmartPriceData)
                        {

                            if (count == 1)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", "ActualQuoteNumber,", dsaObject.ActualQuote));
                                sb1.AppendLine(string.Format("{0}{1}", "NewQuoteNumber,", report.QuoteNumber));
                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Suite Identified,", "validation"));
                                if (isindirect == true)
                                {
                                    sb1.AppendLine(string.Format("{0}{1}", "Partner Account,", customerObj.OldAccountCustomerId));
                                    sb1.AppendLine(string.Format("{0}{1}", "EndUserAccount,", customerObj.OldEndUserAccountCustomerId));
                                }
                                //SFDC details....................
                                //sb1.AppendLine(string.Format("{0}{1}{2}", ",,Calculated,", "Calculated,", "Validation"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealID,,", dsaObject.PnrLog.SFDCDEALID, ",", report.PnrLog.SFDCDEALID, ",", dsaObject.PnrLog.IsSFDCDEALID));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCUnassEndUserC,,", dsaObject.PnrLog.SFDCUNASSIGNEDENDUSERC, ",", report.PnrLog.SFDCUNASSIGNEDENDUSERC, ",", dsaObject.PnrLog.IsSFDCUNASSIGNEDENDUSERC));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyProbability,,", dsaObject.PnrLog.SFDCOPPTYPROBABILITY, ",", report.PnrLog.SFDCOPPTYPROBABILITY, ",", dsaObject.PnrLog.IsSFDCOPPTYPROBABILITY));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCType,,", dsaObject.PnrLog.SFDCTYPE, ",", dsaObject.PnrLog.SFDCTYPE, ",", report.PnrLog.IsSFDCTYPE));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCRegistrationStatus,,", dsaObject.PnrLog.SFDCREGISTRATIONSTATUS, ",", report.PnrLog.SFDCREGISTRATIONSTATUS, ",", dsaObject.PnrLog.IsSFDCREGISTRATIONSTATUS));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCFulfilmentPath,,", dsaObject.PnrLog.SFDCFULFILMENTPATH, ",", report.PnrLog.SFDCFULFILMENTPATH, ",", dsaObject.PnrLog.IsSFDCFULFILMENTPATH));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealTypeC,,", dsaObject.PnrLog.SFDCDEALTYPEC, ",", report.PnrLog.SFDCDEALTYPEC, ",", dsaObject.PnrLog.IsSFDCDEALTYPEC));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyStage,,", dsaObject.PnrLog.SFDCOPPTYSTAGE.Replace(",", " or"), ",", report.PnrLog.SFDCOPPTYSTAGE.Replace(",", " or"), ",", dsaObject.PnrLog.IsSFDCOPPTYSTAGE));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyBookedDate,,", dsaObject.PnrLog.SFDCOPPTYBOOKEDDATE, ",", report.PnrLog.SFDCOPPTYBOOKEDDATE, ",", dsaObject.PnrLog.IsSFDCOPPTYBOOKEDDATE));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealRegOppty,,", dsaObject.PnrLog.SFDCDEALREGOPPTY, ",", report.PnrLog.SFDCDEALREGOPPTY, ",", dsaObject.PnrLog.IsSFDCDEALREGOPPTY));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyRecordType,,", dsaObject.PnrLog.SFDCOPPTYRECORDTYPE, ",", report.PnrLog.SFDCOPPTYRECORDTYPE, ",", dsaObject.PnrLog.IsSFDCOPPTYRECORDTYPE));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDistrAccountID,,", dsaObject.PnrLog.SFDCDISTIACCOUNTID, ",", report.PnrLog.SFDCDISTIACCOUNTID, ",", dsaObject.PnrLog.IsSFDCDISTIACCOUNTID));

                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "ValidOpportunity", ",", dsaObject.PnrLog.ValidOppportunitySPDecision, ",", dsaObject.ValidOppportunitySuiteDecision, ",", dsaObject.OpportunityStatus));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "OpportunityMessageDisplayed", ",", dsaObject.IsSfdcMessageDisplayed, ",", null, ",", dsaObject.SfdcMessageDisplayed));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "OpportunitySize,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "BookType,", ",Future release", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Stage,", ",Future release", report.PnrLog.SFDCOPPTYSTAGE, ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "OpportunityType,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "RecordType,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,OldValue,", "NewValue,", "Validation"));
                                if (scenarioId == "D10" || isindirect == true || customerObj != null)
                                {
                                    sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId ,", "," + customerObj.OldAccountCustomerId + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                                    sb1.AppendLine(string.Format("{0}{1}", "AccountId ,", "," + customerObj.OldAccountId + "," + customerObj.NewAccountId + "," + customerObj.IsAccountIdMatched));
                                    sb1.AppendLine(string.Format("{0}{1}", "QuoteAccountId ,", "," + customerObj.OldQuoteAccountId + "," + customerObj.NewQuoteAccountId + "," + customerObj.IsQuoteAccountIdMatched));
                                    sb1.AppendLine(string.Format("{0}{1}", "EndUserAccountCustomerId ,", "," + customerObj.OldEndUserAccountCustomerId + "," + customerObj.NewEndUserAccountCustomerId + "," + customerObj.IsEndUserAccountCustomerIdMatched));
                                    sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId,", "," + customerObj.OldAccountCustomerId + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                                }

                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Calculated,", "Validation"));


                                sb1.AppendLine(string.Format("{0}{1}", "TotalListPriceSummaryLevel,", "," + report.SummaryTotalListPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsListPricMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}", "TotalSellingPriceSummaryLevel,", "," + report.SummarySellingPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                                //sb1.AppendLine(string.Format("{0}{1}", "TotalSmartPriceRevenue,", "," + report.smart.Replace(",", "") + "," + Math.Round(report.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "TotalModifiedRevenue,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}", "TotalPricingModifierSummeryLevel,", "," + report.SummaryPricingModifier.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2) + "," + report.comparisionObjects.IsPricingModifierMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithServicesIncentive,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithDFSIncentive,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                            }

                            sb1.AppendLine(string.Format("{0}{1}", "BaseProduct,", +count));

                            CreateCSVForLineItemLevel(sb1, report, null, null, scenarioId, dsaObject);
                            sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Base Product"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                            CreateCSVForCommonGuidance(sb1, report, null, null, scenarioId, dsaObject, IsStandardPartnerDiscount);
                            foreach (DSAPageObject accessory in report.Accessories)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", ",Accessories,", +count));
                                CreateCSVForAccessoriesLineItemLevel(sb1, accessory);//accessories line item
                                sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Accessories"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                                CreateCSVForAccessoriesGuidance(sb1, accessory);
                            }

                            File.AppendAllText(FilePath, sb1.ToString());

                            sb1.Clear();
                            count++;
                        }
                        if (quoteSummaryObject != null && goalDetails != null)
                        {
                            PrintGoalLiteResults(quoteSummaryObject, goalDetails, sb1, count);


                        }
                        // Console.WriteLine(string.Format("CSV file {0} has created", currentdate));
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("CSV file {0} has created", filename_Data_Source), true);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        //return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }

        }
        public void GenerateReportInCSVIndirect(DSAPageObject dsaObject, List<DSAPageObject> retriveSmartPriceData, TotalFinancialCalculation totalFinancialCalculation, string scenarioId, int testCaseNumber, string refreshID, CustomerClass customerObj, bool isIndirect, string pricePointChange, List<PNRAPILog> sp1, List<PNRAPILog> sp2, bool IsStandardPartnerDiscount = false,PNRAPILog pnrlog1=null,PNRAPILog pnrlog2=null, DSAQuoteSummaryObject quoteSummaryObject = null, GoalObject goalDetails = null)
        {
            if (isIndirect)
            {
                randomNumber = RandomNumber(1000, 10000).ToString();
                string s = retriveSmartPriceData.Select(x => x.LocalChannelId).First();

                if (!Directory.Exists(rootfolder))
                {
                    Directory.CreateDirectory(rootfolder);

                }
                string subFolderPath1 = StaticBriefCase.CsvReportName;//string.Format("{0}{1}{2}{3}", "R_" + s + "_" + scenarioId + "", runId, "\\", scenarioId + "_" + runId);
                string subFolder1 = Path.Combine(rootfolder, subFolderPath1);
                if (!Directory.Exists(subFolder1))
                {
                    Directory.CreateDirectory(subFolder1);
                }

                string subFolderPath2 = string.Format("{0}", pricePointChange);
                string subFolder2 = Path.Combine(subFolder1, subFolderPath2);

                if (!Directory.Exists(subFolder2))
                {
                    Directory.CreateDirectory(subFolder2);
                }

                if (!string.IsNullOrEmpty(refreshID))
                {
                    refreshID = string.Format("_{0}", refreshID);
                }
                if (!string.IsNullOrEmpty(pricePointChange))
                {
                    pricePointChange = string.Format("_{0}", pricePointChange);
                }
                filename_Data_Source = scenarioId + "_" + "TC" + testCaseNumber + refreshID + pricePointChange + "_" + currentdate + ".csv";
                //  FilePath = string.Format("{0}{1}{2}", rootfolder, "\\", filename_Data_Source);
                //currentdate
                FilePath = string.Format("{0}{1}{2}", subFolder2, "\\", filename_Data_Source);
                bool is_fileExists = File.Exists(FilePath);

                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                int count = 1;
                if (is_fileExists == false)
                    try
                    {

                        // *******Exporting Header in CSV - Coma Seperated
                        foreach (DSAPageObject report in retriveSmartPriceData)
                        {

                            if (count == 1)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", "ActualQuoteNumber,", dsaObject.ActualQuote));
                                sb1.AppendLine(string.Format("{0}{1}", "NewQuoteNumber,", report.QuoteNumber));
                                
                                if (isIndirect == true)
                                {
                                    sb1.AppendLine(string.Format("{0}{1}", "Partner Account,", customerObj.OldAccountCustomerId));
                                    sb1.AppendLine(string.Format("{0}{1}", "EndUserAccount,", customerObj.OldEndUserAccountCustomerId));
                                }
                                //SFDC details....................
                                //sb1.AppendLine(string.Format("{0}{1}{2}", ",,Calculated,", "Calculated,", "Validation"));
                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,OldValue,", "NewValue,", "Validation"));
                                if (pnrlog1 != null && pnrlog2 != null)
                                {
                                    ShowSFDCResults(sb1, pnrlog1, pnrlog2);
                                }
                                
                                    
                                else {
                                    if (pnrlog1 != null && dsaObject.PnrLog != null)
                                    {

                                        ShowSFDCResults(sb1, pnrlog1, dsaObject.PnrLog);
                                    }
                                }
                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Suite Identified,", "validation"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", "ValidOpportunity",",", ",", dsaObject.PnrLog.ValidOppportunitySPDecision, ",", dsaObject.ValidOppportunitySuiteDecision, ",", dsaObject.OpportunityStatus));


                                    //sb1.AppendLine(string.Format("{0}{1}{2}{3}", "Stage,", ",", pnrlog2.SFDCOPPTYSTAGE, ","));
                                

                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,OldValue,", "NewValue,", "Validation"));
                                sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId ,", "," + customerObj.OldAccountCustomerId.Replace(",", "") + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "AccountId ,", "," + customerObj.OldAccountId + "," + customerObj.NewAccountId + "," + customerObj.IsAccountIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "QuoteAccountId ,", "," + customerObj.OldQuoteAccountId + "," + customerObj.NewQuoteAccountId + "," + customerObj.IsQuoteAccountIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "EndUserAccountCustomerId ,", "," + customerObj.OldEndUserAccountCustomerId + "," + customerObj.NewEndUserAccountCustomerId + "," + customerObj.IsEndUserAccountCustomerIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}", "AccountCustomerId,", "," + customerObj.OldAccountCustomerId + "," + customerObj.NewAccountCustomerId + "," + customerObj.IsAccountCustomerIdMatched));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "HasCustomerInformationChanged,,", ",", ",", IsCustomerIdChanged(customerObj)));

                                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Calculated,", "Validation"));


                                sb1.AppendLine(string.Format("{0}{1}", "TotalListPriceSummaryLevel,", "," + report.SummaryTotalListPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalListpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsListPricMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}", "TotalSellingPriceSummaryLevel,", "," + report.SummarySellingPrice.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                                //sb1.AppendLine(string.Format("{0}{1}", "TotalSmartPriceRevenue,", "," + report.smart.Replace(",", "") + "," + Math.Round(report.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel, 2) + "," + report.comparisionObjects.IsSellingPricMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "TotalModifiedRevenue,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}", "TotalPricingModifierSummeryLevel,", "," + report.SummaryPricingModifier.Replace(",", "") + "," + Math.Round(totalFinancialCalculation.CalculatedTotalPricingModifierSummaryLevel, 2) + "," + report.comparisionObjects.IsPricingModifierMatchSummaryLevel));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithServicesIncentive,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "WithDFSIncentive,", ",Future release", ",Future release", ",Future release"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                            }

                            sb1.AppendLine(string.Format("{0}{1}", "BaseProduct,", +count));

                            CreateCSVForLineItemLevel(sb1, report, null, null, scenarioId, dsaObject);
                            sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Base Product"));
                            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                            CreateCSVForCommonGuidance(sb1, report, null, null, scenarioId, dsaObject, IsStandardPartnerDiscount);
                            foreach (DSAPageObject accessory in report.Accessories)
                            {
                                sb1.AppendLine(string.Format("{0}{1}", ",Accessories,", +count));
                                CreateCSVForAccessoriesLineItemLevel(sb1, accessory);//accessories line item
                                sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Accessories"));
                                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                                CreateCSVForAccessoriesGuidance(sb1, accessory);
                            }

                            File.AppendAllText(FilePath, sb1.ToString());

                            sb1.Clear();
                            count++;
                        }
                        if (quoteSummaryObject != null && goalDetails != null)
                        {
                            PrintGoalLiteResults(quoteSummaryObject, goalDetails, sb1, count);


                        }
                        // Console.WriteLine(string.Format("CSV file {0} has created", currentdate));
                        Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("CSV file {0} has created", filename_Data_Source), true);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.Message);
                        //return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }

        }

        private void ShowSFDCResults(StringBuilder sb1, PNRAPILog pnrlog1, PNRAPILog pnrlog2)
        {
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealID,,", pnrlog1.SFDCDEALID, ",", pnrlog2.SFDCDEALID, ",", pnrlog1.IsSFDCDEALID));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCUnassEndUserC,,", pnrlog1.SFDCUNASSIGNEDENDUSERC, ",", pnrlog2.SFDCUNASSIGNEDENDUSERC, ",", pnrlog1.IsSFDCUNASSIGNEDENDUSERC));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyProbability,,", pnrlog1.SFDCOPPTYPROBABILITY, ",", pnrlog2.SFDCOPPTYPROBABILITY, ",", pnrlog1.IsSFDCOPPTYPROBABILITY));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCType,,", pnrlog1.SFDCTYPE, ",", pnrlog2.SFDCTYPE, ",", pnrlog1.IsSFDCTYPE));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCRegistrationStatus,,", pnrlog1.SFDCREGISTRATIONSTATUS, ",", pnrlog2.SFDCREGISTRATIONSTATUS, ",", pnrlog1.IsSFDCREGISTRATIONSTATUS));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCFulfilmentPath,,", pnrlog1.SFDCFULFILMENTPATH, ",", pnrlog2.SFDCFULFILMENTPATH, ",", pnrlog1.IsSFDCFULFILMENTPATH));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealTypeC,,", pnrlog1.SFDCDEALTYPEC, ",", pnrlog2.SFDCDEALTYPEC, ",", pnrlog1.IsSFDCDEALTYPEC));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyStage,,", pnrlog1.SFDCOPPTYSTAGE.Replace(",", " or"), ",", pnrlog2.SFDCOPPTYSTAGE.Replace(",", " or"), ",", pnrlog1.IsSFDCOPPTYSTAGE));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyBookedDate,,", pnrlog1.SFDCOPPTYBOOKEDDATE, ",", pnrlog2.SFDCOPPTYBOOKEDDATE, ",", pnrlog1.IsSFDCOPPTYBOOKEDDATE));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDealRegOppty,,", pnrlog1.SFDCDEALREGOPPTY, ",", pnrlog2.SFDCDEALREGOPPTY, ",", pnrlog1.IsSFDCDEALREGOPPTY));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCOpptyRecordType,,", pnrlog1.SFDCOPPTYRECORDTYPE, ",", pnrlog2.SFDCOPPTYRECORDTYPE, ",", pnrlog1.IsSFDCOPPTYRECORDTYPE));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", "SFDCDistrAccountID,,", pnrlog1.SFDCDISTIACCOUNTID, ",", pnrlog2.SFDCDISTIACCOUNTID, ",", pnrlog1.IsSFDCDISTIACCOUNTID));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", "HasSFDCInformationChanged,,", ",",",", HasSFDCInformationChanged(pnrlog1)));

        }

        private void FivePricePointDataCapture(StringBuilder sb1, DSAPageObject report, string pricingHeader, int count)
        {
            try
            {
                sb1.AppendLine(string.Format("{0}{1}{2}", pricingHeader, "Discount Applied", report.DiscountField(0).GetAttribute("innerText")));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Unit Selling Price", ",", report.UnitSellingPrice(0)));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Total Selling Price", ",", report.TotalSellingPrice));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Modified Revenue", ",", report.CalculatedActualSPCompFinancials.ModifiedRevenue));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Upsell", ",", report.CalculatedCompAnchorSPCompFinancials.UpsellModifiedRevenue));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "SmartPrice Revenue", ",", report.CalculatedActualSPCompFinancials.SmartPriceRevenue));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Price Modifier", ",", report.CalculatedActualSPCompFinancials.PricingModifier));

                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));
                sb1.AppendLine(string.Format("{0}", ",,", "QuoteSummary", ",", report.PnrLog.DiscountedPricePerUnit));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Total Selling Price", ",", report.TotalSellingPrice));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "Modified Revenue", ",", report.CalculatedActualSPCompFinancials.ModifiedRevenue));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "With Services Incentive:", ""));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "SmartPrice Revenue", ",", report.CalculatedActualSPCompFinancials.SmartPriceRevenue));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,", "With DFS Incentive", ",", ""));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CreateCSVForCommonGuidance(StringBuilder sb1, DSAPageObject report, List<PNRAPILog> sp1, List<PNRAPILog> sp2, string scenarioId, DSAPageObject dsaObject, bool IsStandardPartnerDiscount)
        {
            if (IsStandardPartnerDiscount == false)
            {
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",RecommendedsellingPrice,", report.RecommenededSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitRecommendedgPrice, "," + report.comparisionObjects.IsSpRecommendedsellingPriceMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",CompAnchorSellingPrice,", report.CompAnchorSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitCompAnchorPrice, "," + report.comparisionObjects.IsCompAnchorsellingPriceMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",FloorSellingPrice,", report.FloorSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitFloorPrice, "," + report.comparisionObjects.IsFloorSellingPriceMatch));

                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",RecommendedSmartPriceRevenue,", report.RecommendedSmartPricRevenue.Replace(",", ""), "," + report.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsRecommenededSmartPriceRevenueMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",CompAnchorSmartPriceRevenue,", report.CompAnchorSmartPricRevenue.Replace(",", ""), "," + report.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",FloorSmartPriceRevenue,", report.FloorSmartPricRevenue.Replace(",", ""), "," + report.CalculatedFloorSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsFloorSmartPriceRevenueMatch));

                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",RecommendedPricingModifier,", report.RecommendedPricingModifier.Replace(",", ""), "," + report.CalculatedRecommendedSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsRecommendedPricingModifierMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",CompAnchorPricingModifier,", report.CompAnchorPricingModifier.Replace(",", ""), "," + report.CalculatedCompAnchorSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsCompAnchorPricingModifierMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",FloorPricingModifier,", report.FloorPricingModifier.Replace(",", ""), "," + report.CalculatedFloorSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsFloorPricingModifierMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
            }


            //  /*------------------------------- show actual guidance from pnr log----------------*/
            sb1.AppendLine(string.Format("{0}", ",Guidance Delivered by SmartPrice"));
            sb1.AppendLine(string.Format("{0}{1}", ",RecommendedDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}", ",CompAnchorDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}", ",FloorDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.FloorDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}{2}", ",StandardPartnerDiscountPercentage,", report.PnrLog.SmartPriceGuidance.StandardPartnerDiscountPercentage, report.PnrLog.StandardPartnerDiscount));
            sb1.AppendLine(string.Format("{0}{1}", ",StandardPartnerMarginPercentage,", report.PnrLog.SmartPriceGuidance.StandardPartnerMarginPercentage));
            sb1.AppendLine(string.Format("{0}{1}", ",BaseMultiplier,", report.PnrLog.SmartPriceGuidance.BaseMultiplier));
            sb1.AppendLine(string.Format("{0}{1}", ",CompAccelerator,", report.PnrLog.SmartPriceGuidance.CompAccelerator));
            sb1.AppendLine(string.Format("{0}{1}", ",CompDecelerator,", report.PnrLog.SmartPriceGuidance.CompDecelerator));
            sb1.AppendLine(string.Format("{0}{1}", ",MarginRecommended,", report.PnrLog.SmartPriceGuidance.MarginRecommended));
            sb1.AppendLine(string.Format("{0}{1}", ",MarginCompAnchor,", report.PnrLog.SmartPriceGuidance.MarginCompAnchor));
            sb1.AppendLine(string.Format("{0}{1}", ",MarginFloor,", report.PnrLog.SmartPriceGuidance.MarginFloor));
            sb1.AppendLine(string.Format("{0}{1}", ",LeaseModifier,", report.PnrLog.SmartPriceGuidance.LeaseModifier));
            sb1.AppendLine(string.Format("{0}{1}", ",CompModMin,", report.PnrLog.SmartPriceGuidance.CompModMin));
            sb1.AppendLine(string.Format("{0}{1}", ",CompModMax,", report.PnrLog.SmartPriceGuidance.CompModMax));
            sb1.AppendLine(string.Format("{0}{1}", ",RebateThreshold,", report.PnrLog.SmartPriceGuidance.RebateThreshold));
            sb1.AppendLine(string.Format("{0}{1}", ",RebateType,", report.PnrLog.SmartPriceGuidance.RebateType));
            sb1.AppendLine(string.Format("{0}{1}", ",IsRebateAvailable,", report.PnrLog.SmartPriceGuidance.IsRebateAvailable));
            sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod1,", report.PnrLog.SmartPriceGuidance.CommissionMod1));
            sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod2,", report.PnrLog.SmartPriceGuidance.CommissionMod2));
            sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod3,", report.PnrLog.SmartPriceGuidance.CommissionMod3));
            sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod4,", report.PnrLog.SmartPriceGuidance.CommissionMod4));
            sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}", ",RecommendedDOLPercentage_Final,,", report.RecommenededDiscount, ",", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final), 2), ",", Math.Round(Convert.ToDecimal(dsaObject.comparisionObjects.IsRecommendedDOLPercentage_Final), 2)));
            sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}{6}", ",CompAnchorDOLPercentage_Final,", ",", report.CompAnchorDiscount, ", ", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2), ", ", Math.Round(Convert.ToDecimal(report.comparisionObjects.IsCompAnchorDOLPercentage_Final), 2)));
            sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}{6}", ",FloorDOLPercentage_Final,", ",", report.FloorDiscount, ", ", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final), 2), ", ", Math.Round(Convert.ToDecimal(report.comparisionObjects.IsFloorDOLPercentage_Final), 2)));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));
            if (sp2 != null)
            {
                sb1.AppendLine(string.Format("{0}", ",SmartPrice Guidance Popup for Standard Partner Discount"));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));
                sb1.AppendLine(string.Format("{0}", ",Standard Partner Discount"));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));
                sb1.AppendLine(string.Format("{0}", ",Standard Partner Guidance Delivered by SmartPrice"));
                sb1.AppendLine(string.Format("{0}{1}", ",RecommendedDOLPercentage,", Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.RecommendedDOLPercentage), 2)));
                sb1.AppendLine(string.Format("{0}{1}", ",CompAnchorDOLPercentage,", Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.CompAnchorDOLPercentage), 2)));
                sb1.AppendLine(string.Format("{0}{1}", ",FloorDOLPercentage,", Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.FloorDOLPercentage), 2)));
                sb1.AppendLine(string.Format("{0}{1}", ",StandardPartnerDiscountPercentage,", sp2[0].SmartPriceGuidance.StandardPartnerDiscountPercentage));
                sb1.AppendLine(string.Format("{0}{1}", ",StandardPartnerMarginPercentage,", sp2[0].SmartPriceGuidance.StandardPartnerMarginPercentage));
                sb1.AppendLine(string.Format("{0}{1}", ",BaseMultiplier,", sp2[0].SmartPriceGuidance.BaseMultiplier));
                sb1.AppendLine(string.Format("{0}{1}", ",CompAccelerator,", sp2[0].SmartPriceGuidance.CompAccelerator));
                sb1.AppendLine(string.Format("{0}{1}", ",CompDecelerator,", sp2[0].SmartPriceGuidance.CompDecelerator));
                sb1.AppendLine(string.Format("{0}{1}", ",MarginRecommended,", sp2[0].SmartPriceGuidance.MarginRecommended));
                sb1.AppendLine(string.Format("{0}{1}", ",MarginCompAnchor,", sp2[0].SmartPriceGuidance.MarginCompAnchor));
                sb1.AppendLine(string.Format("{0}{1}", ",MarginFloor,", sp2[0].SmartPriceGuidance.MarginFloor));
                sb1.AppendLine(string.Format("{0}{1}", ",LeaseModifier,", sp2[0].SmartPriceGuidance.LeaseModifier));
                sb1.AppendLine(string.Format("{0}{1}", ",CompModMin,", sp2[0].SmartPriceGuidance.CompModMin));
                sb1.AppendLine(string.Format("{0}{1}", ",CompModMax,", sp2[0].SmartPriceGuidance.CompModMax));
                sb1.AppendLine(string.Format("{0}{1}", ",RebateThreshold,", sp2[0].SmartPriceGuidance.RebateThreshold));
                sb1.AppendLine(string.Format("{0}{1}", ",RebateType,", sp2[0].SmartPriceGuidance.RebateType));
                sb1.AppendLine(string.Format("{0}{1}", ",IsRebateAvailable,", sp2[0].SmartPriceGuidance.IsRebateAvailable));
                sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod1,", sp2[0].SmartPriceGuidance.CommissionMod1));
                sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod2,", sp2[0].SmartPriceGuidance.CommissionMod2));
                sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod3,", sp2[0].SmartPriceGuidance.CommissionMod3));
                sb1.AppendLine(string.Format("{0}{1}", ",CommissionMod4,", sp2[0].SmartPriceGuidance.CommissionMod4));


                sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}", ",RecommendedDOLPercentage_Final,", dsaObject.StandardpartnerDiscountText, ",", "," + Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.RecommendedDOLPercentage_Final), 2), ",", Math.Round(Convert.ToDecimal(dsaObject.comparisionObjects.IsStandardPartnerDiscount), 2)));
                //sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}", ",CompAnchorDOLPercentage_Final,", report.CompAnchorDiscount, ", ", Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2), ", ", Math.Round(Convert.ToDecimal(report.comparisionObjects.IsCompAnchorDOLPercentage_Final), 2)));
                //sb1.AppendLine(string.Format("{0}{1}{3}{4}{5}", ",FloorDOLPercentage_Final,", report.FloorDiscount, ", ", Math.Round(Convert.ToDecimal(sp2[0].SmartPriceGuidance.FloorDOLPercentage_Final), 2), ", ", Math.Round(Convert.ToDecimal(report.comparisionObjects.IsFloorDOLPercentage_Final), 2)));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));

            }

        }
        public void CreateCSVForAccessoriesGuidance(StringBuilder sb1, DSAPageObject report)
        {

            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,RecommendedsellingPrice,", report.RecommenededSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitRecommendedgPrice, "," + report.comparisionObjects.IsSpRecommendedsellingPriceMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,CompAnchorSellingPrice,", report.CompAnchorSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitCompAnchorPrice, "," + report.comparisionObjects.IsCompAnchorsellingPriceMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,FloorSellingPrice,", report.FloorSellingPrice.Replace(",", ""), "," + report.PnrLog.ProductUnitFloorPrice, "," + report.comparisionObjects.IsFloorSellingPriceMatch));

            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,RecommendedSmartPriceRevenue,", report.RecommendedSmartPricRevenue.Replace(",", ""), "," + report.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsRecommenededSmartPriceRevenueMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,CompAnchorSmartPriceRevenue,", report.CompAnchorSmartPricRevenue.Replace(",", ""), "," + report.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,FloorSmartPriceRevenue,", report.FloorSmartPricRevenue.Replace(",", ""), "," + report.CalculatedFloorSPCompFinancials.SmartPriceRevenue, "," + report.comparisionObjects.IsFloorSmartPriceRevenueMatch));

            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,RecommendedPricingModifier,", report.RecommendedPricingModifier.Replace(",", ""), "," + report.CalculatedRecommendedSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsRecommendedPricingModifierMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,CompAnchorPricingModifier,", report.CompAnchorPricingModifier.Replace(",", ""), "," + report.CalculatedCompAnchorSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsCompAnchorPricingModifierMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,FloorPricingModifier,", report.FloorPricingModifier.Replace(",", ""), "," + report.CalculatedFloorSPCompFinancials.PricingModifier, "," + report.comparisionObjects.IsFloorPricingModifierMatch));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));



            //  /*------------------------------- show actual guidance from pnr log----------------*/
            sb1.AppendLine(string.Format("{0}", ",,,Show Actual guidance from PnrApiLog"));
            sb1.AppendLine(string.Format("{0}{1}", ",,RecommendedDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}", ",,CompAnchorDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}", ",,FloorDOLPercentage,", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.FloorDOLPercentage), 2)));
            sb1.AppendLine(string.Format("{0}{1}", ",,StandardPartnerDiscountPercentage,", report.PnrLog.SmartPriceGuidance.StandardPartnerDiscountPercentage));
            sb1.AppendLine(string.Format("{0}{1}", ",,StandardPartnerMarginPercentage,", report.PnrLog.SmartPriceGuidance.StandardPartnerMarginPercentage));
            sb1.AppendLine(string.Format("{0}{1}", ",,BaseMultiplier,", report.PnrLog.SmartPriceGuidance.BaseMultiplier));
            sb1.AppendLine(string.Format("{0}{1}", ",,CompAccelerator,", report.PnrLog.SmartPriceGuidance.CompAccelerator));
            sb1.AppendLine(string.Format("{0}{1}", ",,CompDecelerator,", report.PnrLog.SmartPriceGuidance.CompDecelerator));
            sb1.AppendLine(string.Format("{0}{1}", ",,MarginRecommended,", report.PnrLog.SmartPriceGuidance.MarginRecommended));
            sb1.AppendLine(string.Format("{0}{1}", ",,MarginCompAnchor,", report.PnrLog.SmartPriceGuidance.MarginCompAnchor));
            sb1.AppendLine(string.Format("{0}{1}", ",,MarginFloor,", report.PnrLog.SmartPriceGuidance.MarginFloor));
            sb1.AppendLine(string.Format("{0}{1}", ",,LeaseModifier,", report.PnrLog.SmartPriceGuidance.LeaseModifier));
            sb1.AppendLine(string.Format("{0}{1}", ",,CompModMin,", report.PnrLog.SmartPriceGuidance.CompModMin));
            sb1.AppendLine(string.Format("{0}{1}", ",,CompModMax,", report.PnrLog.SmartPriceGuidance.CompModMax));
            sb1.AppendLine(string.Format("{0}{1}", ",,RebateThreshold,", report.PnrLog.SmartPriceGuidance.RebateThreshold));
            sb1.AppendLine(string.Format("{0}{1}", ",,RebateType,", report.PnrLog.SmartPriceGuidance.RebateType));
            sb1.AppendLine(string.Format("{0}{1}", ",,IsRebateAvailable,", report.PnrLog.SmartPriceGuidance.IsRebateAvailable));
            sb1.AppendLine(string.Format("{0}{1}", ",,CommissionMod1,", report.PnrLog.SmartPriceGuidance.CommissionMod1));
            sb1.AppendLine(string.Format("{0}{1}", ",,CommissionMod2,", report.PnrLog.SmartPriceGuidance.CommissionMod2));
            sb1.AppendLine(string.Format("{0}{1}", ",,CommissionMod3,", report.PnrLog.SmartPriceGuidance.CommissionMod3));
            sb1.AppendLine(string.Format("{0}{1}", ",,CommissionMod4,", report.PnrLog.SmartPriceGuidance.CommissionMod4));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,RecommendedDOLPercentage_Final,", "", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final), 2), ""));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,CompAnchorDOLPercentage_Final,", "", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2), ""));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",,FloorDOLPercentage_Final,", "", Math.Round(Convert.ToDecimal(report.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final), 2), ""));
            sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------", ",------------------------------------------"));


        }
        public void CreateCSVForLineItemLevel(StringBuilder sb1, DSAPageObject report, List<PNRAPILog> sp1, List<PNRAPILog> sp2, string scenarioId, DSAPageObject dsaObject)
        {
            try
            {
                sb1.AppendLine(string.Format("{0}{1}", "OrderCode,", report.OrderCode));
                sb1.AppendLine(string.Format("{0}{1}", "SKU,", report.SKUNumber));
                sb1.AppendLine(string.Format("{0}{1}", "Quantity,", report.PnrLog.SmartPriceGuidance.Quantity));
                // sb1.AppendLine(string.Format("{0}{1}", "Title,", report.comparisionObjects.dsaObjects.Title));
                sb1.AppendLine(string.Format("{0}{1}{2}", ",,DSAScreen,", "Calculated,", "Validation"));
                sb1.AppendLine(string.Format("{0}{1}", "UnitListPrice,", "," + report.UnitListPrice.Replace(",", "")/*,report.PnrLog.ListPricePerUnit*/)); // calculated unit list price needs to be added.
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "UnitSellingPrice,", "," + report.LineItemUnitSellingPrice.Replace(",", ""), "," + report.comparisionObjects.CalUnitSellingPrice, "," + report.comparisionObjects.IsUnitSellingPriceMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}{6}", "Discount,", ",", report.DiscountInputField, ",", report.comparisionObjects.CalDiscountedPricePerUnit, ",", report.comparisionObjects.IsDiscountedPricePerUnitMatch));

                sb1.AppendLine(string.Format("{0}{1}", "TotalListPrice,", "," + report.TotalListPrice.Replace(",", "") + "," + report.comparisionObjects.CalListPrice + "," + " "/*report.comparisionObjects.IsTotalListPriceMatch*/));
                sb1.AppendLine(string.Format("{0}{1}", "TotalSellingPrice,", "," + report.LineItemSummeryTotalSellingPrice.Replace(",", "") + "," + report.comparisionObjects.CalTotalSellingPrice + "," + report.comparisionObjects.IsTotalSellingPriceMatch));
                sb1.AppendLine(string.Format("{0}{1}", "ModifiedRevenue,", "," + report.LineItemSummaryModifiedRevenue.Replace(",", "") + "," + Math.Round(report.CalculatedModifiedServices.ModifiedRevenue, 2) + "," + report.comparisionObjects.IsModifiedRevenueMatch));
                sb1.AppendLine(string.Format("{0}{1}", "SmartPriceRevenue,", "," + report.LineItemSummerySmartPriceRevenue.Replace(",", "") + "," + Math.Round(report.comparisionObjects.TotalActualSmartPriceRevenue, 2) + "," + report.comparisionObjects.IsActualSmartPriceRevenueMatch));
                sb1.AppendLine(string.Format("{0}{1}", "Upsell Services Modified Revenue,", "," + report.LineItemSummaryUpsellServicesModifiedRevenue.Replace(",", "") + "," + Math.Round(report.CalculatedModifiedServices.UpsellModifiedRevenue, 2) + "," + report.comparisionObjects.IsUpSellServicesModifiedRevenueMatch));
                sb1.AppendLine(string.Format("{0}{1}", "PricingModifier,", "," + report.LineItemSummeryPricingModifier.Replace(",", "") + "," + Math.Round(report.comparisionObjects.CalculatedTotalPricingModifier, 2) + "," + report.comparisionObjects.IsPricingModifierMatch));
                sb1.AppendLine(string.Format("{0}{1}", "With Services Incentive,", "," + report.LineItemSummaryWithServicesIncentive.Replace(",", "") + "," + Math.Round(report.CalculatedModifiedServices.WithServicesIncentive, 2) + "," + report.comparisionObjects.IsWithServicesIncentiveMatch));
                sb1.AppendLine(string.Format("{0}{1}", "WithDFSIncentive,", "," + report.WithDFSIncentive.Replace(",", "") + "," + Math.Round(report.CalculatedActualSPCompFinancials.DFSModifier, 2) + "," + report.comparisionObjects.IsDFSModifierMatch));
                sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));

                if (scenarioId == "D62")
                {
                    sb1.AppendLine(string.Format("{0}", ",SmartPrice Request for Standard Partner Discount Guidance"));
                    sb1.AppendLine(string.Format("{0}{1}{2}{3}{4}{5}", ",REQUEST STANDARD PARTNER DISCOUNT,", sp1[0].StandardPartnerDiscount, ",", sp2[0].StandardPartnerDiscount, ",", report.comparisionObjects.IsStandardPartnerDiscount));
                    sb1.AppendLine(string.Format("{0}", ",SmartPrice LOOKUP fields"));
                    sb1.AppendLine(string.Format("{0}{1}{2}{3}", "-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
                    sb1.AppendLine(string.Format("{0}{1}", ",IsRegistered", "," + sp1[0].IsRegistered.Replace(",", "") + "," + sp2[0].IsRegistered.Replace(",", "") + "," + dsaObject.comparisionObjects.IsResgistered));
                    sb1.AppendLine(string.Format("{0}{1}", ",IsDirect", "," + sp1[0].IsDirect.Replace(",", "") + "," + sp2[0].IsDirect.Replace(",", "") + "," + dsaObject.comparisionObjects.IsDirect));
                    sb1.AppendLine(string.Format("{0}{1}", ",IsPartner", "," + sp1[0].IsPartner.Replace(",", "") + "," + sp2[0].IsPartner.Replace(",", "") + "," + dsaObject.comparisionObjects.IsPartner));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CreateCSVForAccessoriesLineItemLevel(StringBuilder sb1, DSAPageObject report)
        {
            sb1.AppendLine(string.Format("{0}{1}", ",OrderCode,", report.OrderCode));
            sb1.AppendLine(string.Format("{0}{1}", ",SKU,", report.SKUNumber));
            sb1.AppendLine(string.Format("{0}{1}", ",Quantity,", report.PnrLog.SmartPriceGuidance.Quantity));
            // sb1.AppendLine(string.Format("{0}{1}", "Title,", report.comparisionObjects.dsaObjects.Title));
            sb1.AppendLine(string.Format("{0}{1}{2}", ",,,DSAScreen,", "Calculated,", "Validation"));
            sb1.AppendLine(string.Format("{0}{1}", ",UnitListPrice,", "," + report.UnitListPrice.Replace(",", "")/*,report.PnrLog.ListPricePerUnit*/)); // calculated unit list price needs to be added.
            sb1.AppendLine(string.Format("{0}{1}", ",UnitSellingPrice,", "," + report.LineItemUnitSellingPrice.Replace(",", "")));
            sb1.AppendLine(string.Format("{0}{1}", ",SmartPriceRevenue,", "," + report.LineItemSmartRevenue.Replace(",", "")));

            sb1.AppendLine(string.Format("{0}{1}{2}{3}", ",-------------------------------------------", ",-------------------------------------------", ",-------------------------------------------", ", -------------------------------------------"));
        }

        public void ExportToSqlServerDB(DSAPageObject calculateObj, TestCase testCase, string scenarioId, List<DSAPageObject> retriveSmartPriceData,CustomerClass customerObj=null)
        {
            try
            {
                InsertTestResult testResults = new InsertTestResult();
                testResults.InsertDataIntoSqlServer(retriveSmartPriceData, FilePath, currentdate, calculateObj.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel,
                calculateObj.comparisionObjects.CalculatedTotalListpriceSummaryLevel, calculateObj.comparisionObjects.CalculatedTotalPricingModifier, testCase, scenarioId, customerObj);
                //testResults.InsertResults(retriveSmartPriceData, FilePath, currentdate, calculateObj.comparisionObjects.CalculatedTotalSellingpriceSummaryLevel,
                //calculateObj.comparisionObjects.CalculatedTotalListpriceSummaryLevel, calculateObj.comparisionObjects.CalculatedTotalPricingModifier, testCase, scenarioId);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Data is inserted to Main Table and RequestId {0} is created", currentdate), true);
            }
            catch (ShowStopperException ex)
            {

                Console.WriteLine("Data not able to insert into SQl Server.Error: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Data not able to insert into SQl Server.Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Export data into SumarryTable in Sql Server.
        /// </summary>
        /// <param name="calculateObj"></param>
        public void ExportToSummaryTable(TestCase testCase, string scenarioId)
        {
            try
            {
                InsertTestResult testResults = new InsertTestResult();
                testResults.InsertResultsIntoSummaryTable(testCase.TestCaseTitle, currentdate, scenarioId);
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Details(string.Format("Data is inserted to SummaryTable and RequestId {0} is created", currentdate), true);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Data not able to insert into SQl Server.Error: " + ex.Message);
            }
        }
        public string HasSFDCInformationChanged(PNRAPILog pnrApilog)
        {
           List<bool> listofSfdc = new List<bool>(new bool[] {
               pnrApilog.IsSFDCDEALID,
               pnrApilog.IsSFDCDEALREGOPPTY,
               pnrApilog.IsSFDCDEALTYPEC,
               pnrApilog.IsSFDCDISTIACCOUNTID,
               pnrApilog.IsSFDCFULFILMENTPATH,
               pnrApilog.IsSFDCOPPTYBOOKEDDATE,
               pnrApilog.IsSFDCOPPTYPROBABILITY,
               pnrApilog.IsSFDCOPPTYRECORDTYPE,
               pnrApilog.IsSFDCOPPTYSTAGE,
               pnrApilog.IsSFDCREGISTRATIONSTATUS,
               pnrApilog.IsSFDCTYPE,
               pnrApilog.IsSFDCUNASSIGNEDENDUSERC,
               
           });
            if (listofSfdc.Contains(false))
                {
                return pnrApilog.HasSFDCInformationChanged + "Yes";
                }
            return pnrApilog.HasSFDCInformationChanged + " No";
        }
        public string IsCustomerIdChanged(CustomerClass customerClass)
        {
            List<string> listofcustomerdetails = new List<string>(new string[] {
               customerClass.IsAccountCustomerIdMatched,
               customerClass.IsAccountIdMatched,
               customerClass.IsEndUserAccountCustomerIdMatched,
               customerClass.IsEndUserAccountIdMatched,
               

           });
            if (listofcustomerdetails.Contains("false"))
            {
                return customerClass.IsCustomerChanged + "Yes";
            }
            return customerClass.IsCustomerChanged + " No";
        }

    }
            
    
    //
    
}
