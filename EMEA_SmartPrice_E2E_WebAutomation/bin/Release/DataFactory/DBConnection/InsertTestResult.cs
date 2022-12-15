using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using EMEA_SmartPrice_E2E_WebAutomation.Objects;
using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;


namespace EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DBConnection
{
    //Insert test results in sql sever db
    class InsertTestResult
    {
        public SqlConnection _sqlCon;
        public string _sqlConnection;
        public InsertTestResult()
        {
            _sqlConnection = ConfigurationManager.AppSettings["SqlServerConnection"];
        }
        public static List<string> summaryTestCaseStatusList = new List<string>();
        public string TestCaseSummaryStatus()
        {
            if(summaryTestCaseStatusList.Contains("Failed"))
            {
                return "Failed";
            }
            return "Passed";
            
        }
        public string TestcaseStatus(PNRAPICalulationCompare ds,string result)
        {
            
            
            List<bool> bs = new List<bool>();
             bs.Add(ds.IsPricingModifierMatch);
            bs.Add(ds.IsSpRecommendedsellingPriceMatch);
            bs.Add(ds.IsRecommenededSmartPriceRevenueMatch);
            bs.Add(ds.IsRecommendedPricingModifierMatch);
            bs.Add(ds.IsCompAnchorsellingPriceMatch);
            bs.Add(ds.IsCompAnchorSmartPriceRevenueMatch);
            bs.Add(ds.IsCompAnchorPricingModifierMatch);
            bs.Add(ds.IsFloorSellingPriceMatch);
            bs.Add(ds.IsFloorSmartPriceRevenueMatch);
            bs.Add(ds.IsFloorPricingModifierMatch);
            bs.Add(ds.IsTotalSellingPriceMatch);
            bs.Add(ds.IsActualSmartPriceRevenueMatch);
            bs.Add(ds.IsPricingModifierMatch);
            bs.Add(ds.IsListPricMatchSummaryLevel);
            bs.Add(ds.IsSellingPricMatchSummaryLevel);
            bs.Add(ds.IsPricingModifierMatchSummaryLevel);
            //bs.Add(ds.IsModifiedRevenueMatch);
            //bs.Add(ds.IsUpSellServicesModifiedRevenueMatch);
            //bs.Add(ds.IsWithServicesIncentiveMatch);


            if (bs.TrueForAll(b=>b))
            {
                result = "Passed";
            }
            
            else
            {
                result = "Failed";
                
            }
            //string result = "passed";
            //switch(result)

            return result;
        }

        public void InsertDataIntoSqlServer(List<DSAPageObject> listOfDsaObj, string filepath, string currentdate, decimal TotalsellingPrice, decimal totallistPrice, decimal TotalPricingModifier, TestCase testcase, string scenarioId,CustomerClass customerobj=null)
        {
            _sqlCon = new SqlConnection(_sqlConnection);
            int count = 1;
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                    


                foreach (DSAPageObject rs in listOfDsaObj)
                {

                    InsertIntoDSATable(rs, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Baseproduct",customerobj);
                    InsertIntoSuiteTable(rs, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Baseproduct", customerobj);
                    InsertIntoDSASuiteValidationTable(rs, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Baseproduct", customerobj);
                    InsertIntoOriginalDataTable(rs, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Baseproduct", customerobj);
                    if (rs.Accessories.Count > 0)
                    {
                        foreach (DSAPageObject accessory in rs.Accessories)
                        {
                            InsertIntoDSATable(accessory, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Accessory",customerobj);
                            InsertIntoSuiteTable(accessory, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Accessory", customerobj);
                            InsertIntoDSASuiteValidationTable(accessory, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Accessory", customerobj);
                            InsertIntoOriginalDataTable(accessory, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count, "Accessory", customerobj);
                        }


                    }


                    count++;
                }

                _sqlCon.Close();

            }
            catch(ShowStopperException ex)
            {
                ex.LogException();
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
  
        private void InsertIntoOriginalDataTable(DSAPageObject dsaObj, string filepath, string currentdate, decimal totalsellingPrice, decimal totallistPrice, decimal totalPricingModifier, TestCase testcase, string scenarioId, int count, string TypeOfProduct = "", CustomerClass customerObj=null)
        {
            try
            {
                string sqlQuery = "INSERT INTO GPT_Automation_T0_OriginalQuote (Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged) values (@Run_ID,@RUN_ID2,@Scenario_ID,@QuoteNumber,@PartnerAccount,@EndUserAccount,@SFDCDealID,@SFDCUnassEndUserC,@SFDCOpptyProbability,@SFDCType,@SFDCRegistrationStatus,@SFDCFulfilmentPath,@SFDCDealTypeC,@SFDCOpptyStage,@SFDCOpptyBookedDate,@SFDCDealRegOppty,@SFDCOpptyRecordType,@SFDCDistrAccountID,@HasSFDCInformationChanged,@AccountCustomerId,@AccountId,@QuoteAccountId,@EndUserAccountCustomerId,@TotalListPriceSummaryLevel,@TotalSellingPriceSummaryLevel,@TotalModifiedRevenue,@TotalPricingModifierSummeryLevel,@WithServicesIncentive,@WithDFSIncentive,@BaseProduct,@OrderCode,@SKU,@Quantity,@UnitListPrice,@UnitSellingPrice,@Discount,@TotalListPrice,@TotalSellingPrice,@ModifiedRevenue,@SmartPriceRevenue,@UpsellServicesModifiedRevenue,@PricingModifier,@RecommendedsellingPrice,@CompAnchorSellingPrice,@FloorSellingPrice,@RecommendedSmartPriceRevenue,@CompAnchorSmartPriceRevenue,@FloorSmartPriceRevenue,@RecommendedPricingModifier,@CompAnchorPricingModifier,@FloorPricingModifier,@RecommendedDOLPercentage,@CompAnchorDOLPercentage,@FloorDOLPercentage,@StandardPartnerDiscountPercentage,@StandardPartnerMarginPercentage,@BaseMultiplier,@CompAccelerator,@CompDecelerator,@MarginRecommended,@MarginCompAnchor,@MarginFloor,@LeaseModifier,@CompModMin,@CompModMax,@RebateThreshold,@RebateType,@IsRebateAvailable,@CommissionMod1,@CommissionMod2,@CommissionMod3,@CommissionMod4,@RecommendedDOLPercentage_Final,@CompAnchorDOLPercentage_Final,@FloorDOLPercentage_Final,@IsCustomerChanged)";
                _sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, _sqlCon);
                cmd.Parameters.AddWithValue("@Run_ID", "");
                cmd.Parameters.AddWithValue("@RUN_ID2", "");
                cmd.Parameters.AddWithValue("@Scenario_ID", scenarioId);
                cmd.Parameters.AddWithValue("@QuoteNumber", dsaObj.QuoteNumber);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@EndUserAccount", customerObj.OldEndUserAccountCustomerId);


                }
                else
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", "");
                    cmd.Parameters.AddWithValue("@EndUserAccount", "");

                }
                cmd.Parameters.AddWithValue("@SFDCDealID", dsaObj.PnrLog.IsSFDCDEALID);
                cmd.Parameters.AddWithValue("@SFDCUnassEndUserC", dsaObj.PnrLog.IsSFDCUNASSIGNEDENDUSERC);
                cmd.Parameters.AddWithValue("@SFDCOpptyProbability", dsaObj.PnrLog.IsSFDCOPPTYPROBABILITY);
                cmd.Parameters.AddWithValue("@SFDCType", dsaObj.PnrLog.SFDCTYPE);
                cmd.Parameters.AddWithValue("@SFDCRegistrationStatus", dsaObj.PnrLog.IsSFDCREGISTRATIONSTATUS);
                cmd.Parameters.AddWithValue("@SFDCFulfilmentPath", dsaObj.PnrLog.IsSFDCFULFILMENTPATH);
                cmd.Parameters.AddWithValue("@SFDCDealTypeC", dsaObj.PnrLog.IsSFDCDEALTYPEC);
                cmd.Parameters.AddWithValue("@SFDCOpptyStage", dsaObj.PnrLog.IsSFDCOPPTYSTAGE);
                cmd.Parameters.AddWithValue("@SFDCOpptyBookedDate", dsaObj.PnrLog.IsSFDCOPPTYBOOKEDDATE);
                cmd.Parameters.AddWithValue("@SFDCDealRegOppty", dsaObj.PnrLog.IsSFDCDEALREGOPPTY);
                cmd.Parameters.AddWithValue("@SFDCOpptyRecordType", dsaObj.PnrLog.IsSFDCOPPTYRECORDTYPE);
                cmd.Parameters.AddWithValue("@SFDCDistrAccountID", dsaObj.PnrLog.IsSFDCDISTIACCOUNTID);
                cmd.Parameters.AddWithValue("@HasSFDCInformationChanged", dsaObj.PnrLog.HasSFDCInformationChanged);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@AccountId", customerObj.OldAccountId);
                    cmd.Parameters.AddWithValue("@QuoteAccountId", customerObj.OldQuoteAccountId);
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", customerObj.OldEndUserAccountCustomerId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", "");
                    cmd.Parameters.AddWithValue("@AccountId", "");
                    cmd.Parameters.AddWithValue("@QuoteAccountId", "");
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", "");
                }
                cmd.Parameters.AddWithValue("@TotalListPriceSummaryLevel", dsaObj.comparisionObjects.IsListPricMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalSellingPriceSummaryLevel", dsaObj.comparisionObjects.IsSellingPricMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalModifiedRevenue", dsaObj.comparisionObjects.IsModifierMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalPricingModifierSummeryLevel", dsaObj.comparisionObjects.IsPricingModifierMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@WithServicesIncentive", "");
                cmd.Parameters.AddWithValue("@WithDFSIncentive", "");
                cmd.Parameters.AddWithValue("@BaseProduct", TypeOfProduct);
                cmd.Parameters.AddWithValue("@OrderCode", dsaObj.OrderCode);
                cmd.Parameters.AddWithValue("@SKU", dsaObj.SKUNumber);
                cmd.Parameters.AddWithValue("@Quantity", dsaObj.Quantity);
                cmd.Parameters.AddWithValue("@UnitListPrice", dsaObj.UnitListPrice);
                cmd.Parameters.AddWithValue("@UnitSellingPrice", dsaObj.LineItemUnitSellingPrice);
                cmd.Parameters.AddWithValue("@Discount", dsaObj.LineItemUnitDiscount);
                cmd.Parameters.AddWithValue("@TotalListPrice", dsaObj.TotalListPrice);
                cmd.Parameters.AddWithValue("@TotalSellingPrice", dsaObj.TotalSellingPrice);
                cmd.Parameters.AddWithValue("@ModifiedRevenue", dsaObj.LineItemSummaryModifiedRevenue);
                cmd.Parameters.AddWithValue("@SmartPriceRevenue", dsaObj.LineItemSummerySmartPriceRevenue);
                cmd.Parameters.AddWithValue("@UpsellServicesModifiedRevenue", dsaObj.LineItemSummaryUpsellServicesModifiedRevenue);
                cmd.Parameters.AddWithValue("@PricingModifier", dsaObj.LineItemSummeryPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedsellingPrice", dsaObj.RecommenededSellingPrice);
                cmd.Parameters.AddWithValue("@CompAnchorSellingPrice", dsaObj.CompAnchorSellingPrice);
                cmd.Parameters.AddWithValue("@FloorSellingPrice", dsaObj.FloorSellingPrice);
                cmd.Parameters.AddWithValue("@RecommendedSmartPriceRevenue", dsaObj.RecommendedSmartPricRevenue);
                cmd.Parameters.AddWithValue("@CompAnchorSmartPriceRevenue", dsaObj.CompAnchorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@FloorSmartPriceRevenue", dsaObj.FloorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@RecommendedPricingModifier", dsaObj.RecommendedPricingModifier);
                cmd.Parameters.AddWithValue("@CompAnchorPricingModifier", dsaObj.CompAnchorPricingModifier);
                cmd.Parameters.AddWithValue("@FloorPricingModifier", dsaObj.FloorPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage", dsaObj.RecommenededDiscount);
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage", dsaObj.CompAnchorDiscount);
                cmd.Parameters.AddWithValue("@FloorDOLPercentage", dsaObj.FloorDiscount);

                

                cmd.Parameters.AddWithValue("@StandardPartnerDiscountPercentage", dsaObj.StandardpartnerDiscountText);
                cmd.Parameters.AddWithValue("@StandardPartnerMarginPercentage", "");
                cmd.Parameters.AddWithValue("@BaseMultiplier", "");
                cmd.Parameters.AddWithValue("@CompAccelerator", "");
                cmd.Parameters.AddWithValue("@CompDecelerator", "");
                cmd.Parameters.AddWithValue("@MarginRecommended", "");
                cmd.Parameters.AddWithValue("@MarginCompAnchor", "");
                cmd.Parameters.AddWithValue("@MarginFloor", "");
                cmd.Parameters.AddWithValue("@LeaseModifier", "");
                cmd.Parameters.AddWithValue("@CompModMin", "");
                cmd.Parameters.AddWithValue("@CompModMax", "");
                cmd.Parameters.AddWithValue("@RebateThreshold", "");
                cmd.Parameters.AddWithValue("@RebateType", "");
                cmd.Parameters.AddWithValue("@IsRebateAvailable", "");
                cmd.Parameters.AddWithValue("@CommissionMod1", "");
                cmd.Parameters.AddWithValue("@CommissionMod2", "");
                cmd.Parameters.AddWithValue("@CommissionMod3", "");
                cmd.Parameters.AddWithValue("@CommissionMod4", "");
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@FloorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@IsCustomerChanged", "");


                cmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Exception is thrown.Please  Check InsertIntoOriginalDataTable(), InsertResultClass ", true);
                throw new ShowStopperException("unable to insertInto Database table Name:GPT_Automation_T0_OriginalQuote,", ex);
                
            }
            finally
            {
                if (_sqlCon.State == System.Data.ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
            }
        }
        public string NullCheck<T>( T obj)
        {

            if (obj == null)
                return "";
            else
                return obj.ToString();
            
            

        }
        private void InsertIntoDSASuiteValidationTable(DSAPageObject dsaObj, string filepath, string currentdate, decimal totalsellingPrice, decimal totallistPrice, decimal totalPricingModifier, TestCase testcase, string scenarioId, int count, string TypeOfProduct = "", CustomerClass customerObj=null)
        {
            try
            {
                string sqlQuery = "INSERT INTO GPT_Automation_T3_DsaSuiteValidation (Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged) values (@Run_ID,@RUN_ID2,@Scenario_ID,@QuoteNumber,@PartnerAccount,@EndUserAccount,@SFDCDealID,@SFDCUnassEndUserC,@SFDCOpptyProbability,@SFDCType,@SFDCRegistrationStatus,@SFDCFulfilmentPath,@SFDCDealTypeC,@SFDCOpptyStage,@SFDCOpptyBookedDate,@SFDCDealRegOppty,@SFDCOpptyRecordType,@SFDCDistrAccountID,@HasSFDCInformationChanged,@AccountCustomerId,@AccountId,@QuoteAccountId,@EndUserAccountCustomerId,@TotalListPriceSummaryLevel,@TotalSellingPriceSummaryLevel,@TotalModifiedRevenue,@TotalPricingModifierSummeryLevel,@WithServicesIncentive,@WithDFSIncentive,@BaseProduct,@OrderCode,@SKU,@Quantity,@UnitListPrice,@UnitSellingPrice,@Discount,@TotalListPrice,@TotalSellingPrice,@ModifiedRevenue,@SmartPriceRevenue,@UpsellServicesModifiedRevenue,@PricingModifier,@RecommendedsellingPrice,@CompAnchorSellingPrice,@FloorSellingPrice,@RecommendedSmartPriceRevenue,@CompAnchorSmartPriceRevenue,@FloorSmartPriceRevenue,@RecommendedPricingModifier,@CompAnchorPricingModifier,@FloorPricingModifier,@RecommendedDOLPercentage,@CompAnchorDOLPercentage,@FloorDOLPercentage,@StandardPartnerDiscountPercentage,@StandardPartnerMarginPercentage,@BaseMultiplier,@CompAccelerator,@CompDecelerator,@MarginRecommended,@MarginCompAnchor,@MarginFloor,@LeaseModifier,@CompModMin,@CompModMax,@RebateThreshold,@RebateType,@IsRebateAvailable,@CommissionMod1,@CommissionMod2,@CommissionMod3,@CommissionMod4,@RecommendedDOLPercentage_Final,@CompAnchorDOLPercentage_Final,@FloorDOLPercentage_Final,@IsCustomerChanged)";
                _sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, _sqlCon);
                cmd.Parameters.AddWithValue("@Run_ID", "");
                cmd.Parameters.AddWithValue("@RUN_ID2", "");
                cmd.Parameters.AddWithValue("@Scenario_ID", scenarioId);
                cmd.Parameters.AddWithValue("@QuoteNumber", (dsaObj.QuoteNumber=="")?" ":dsaObj.QuoteNumber);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@EndUserAccount", customerObj.OldEndUserAccountCustomerId);


                }
                else
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", "");
                    cmd.Parameters.AddWithValue("@EndUserAccount", "");

                }
                cmd.Parameters.AddWithValue("@SFDCDealID", dsaObj.PnrLog.IsSFDCDEALID);
                cmd.Parameters.AddWithValue("@SFDCUnassEndUserC", dsaObj.PnrLog.IsSFDCUNASSIGNEDENDUSERC);
                cmd.Parameters.AddWithValue("@SFDCOpptyProbability", dsaObj.PnrLog.IsSFDCOPPTYPROBABILITY);
                cmd.Parameters.AddWithValue("@SFDCType", dsaObj.PnrLog.SFDCTYPE);
                cmd.Parameters.AddWithValue("@SFDCRegistrationStatus", dsaObj.PnrLog.IsSFDCREGISTRATIONSTATUS);
                cmd.Parameters.AddWithValue("@SFDCFulfilmentPath", dsaObj.PnrLog.IsSFDCFULFILMENTPATH);
                cmd.Parameters.AddWithValue("@SFDCDealTypeC", dsaObj.PnrLog.IsSFDCDEALTYPEC);
                cmd.Parameters.AddWithValue("@SFDCOpptyStage", dsaObj.PnrLog.IsSFDCOPPTYSTAGE);
                cmd.Parameters.AddWithValue("@SFDCOpptyBookedDate", dsaObj.PnrLog.IsSFDCOPPTYBOOKEDDATE);
                cmd.Parameters.AddWithValue("@SFDCDealRegOppty", dsaObj.PnrLog.IsSFDCDEALREGOPPTY);
                cmd.Parameters.AddWithValue("@SFDCOpptyRecordType", dsaObj.PnrLog.IsSFDCOPPTYRECORDTYPE);
                cmd.Parameters.AddWithValue("@SFDCDistrAccountID", dsaObj.PnrLog.IsSFDCDISTIACCOUNTID);
                cmd.Parameters.AddWithValue("@HasSFDCInformationChanged", dsaObj.PnrLog.HasSFDCInformationChanged);
                
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", customerObj.IsAccountCustomerIdMatched);
                    cmd.Parameters.AddWithValue("@AccountId", customerObj.IsAccountIdMatched);
                    cmd.Parameters.AddWithValue("@QuoteAccountId", customerObj.IsQuoteAccountIdMatched);
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", customerObj.IsEndUserAccountCustomerIdMatched);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", false);
                    cmd.Parameters.AddWithValue("@AccountId", false);
                    cmd.Parameters.AddWithValue("@QuoteAccountId", false);
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", false);
                }
                cmd.Parameters.AddWithValue("@TotalListPriceSummaryLevel", dsaObj.comparisionObjects.IsListPricMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalSellingPriceSummaryLevel", dsaObj.comparisionObjects.IsSellingPricMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalModifiedRevenue", dsaObj.comparisionObjects.IsModifierMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@TotalPricingModifierSummeryLevel", dsaObj.comparisionObjects.IsPricingModifierMatchSummaryLevel);
                cmd.Parameters.AddWithValue("@WithServicesIncentive", "");
                cmd.Parameters.AddWithValue("@WithDFSIncentive", "");
                cmd.Parameters.AddWithValue("@BaseProduct", TypeOfProduct);
                cmd.Parameters.AddWithValue("@OrderCode", "");
                cmd.Parameters.AddWithValue("@SKU", dsaObj.comparisionObjects.IsSkuMatch);
                cmd.Parameters.AddWithValue("@Quantity", dsaObj.comparisionObjects.IsQuantityMatch);
                cmd.Parameters.AddWithValue("@UnitListPrice", dsaObj.comparisionObjects.IsListPriceMatch);
                cmd.Parameters.AddWithValue("@UnitSellingPrice", NullCheck(dsaObj.comparisionObjects.IsUnitSellingPriceMatch));
                cmd.Parameters.AddWithValue("@Discount", NullCheck(dsaObj.comparisionObjects.IsDiscountedPricePerUnitMatch)) ;
                cmd.Parameters.AddWithValue("@TotalListPrice", NullCheck(dsaObj.comparisionObjects.IsTotalListPriceMatch));
                cmd.Parameters.AddWithValue("@TotalSellingPrice", NullCheck(dsaObj.comparisionObjects.IsTotalSellingPriceMatch));
                cmd.Parameters.AddWithValue("@ModifiedRevenue", NullCheck(dsaObj.comparisionObjects.IsModifiedRevenueMatch));
                cmd.Parameters.AddWithValue("@SmartPriceRevenue", NullCheck(dsaObj.comparisionObjects.IsActualSmartPriceRevenueMatch));
                cmd.Parameters.AddWithValue("@UpsellServicesModifiedRevenue", NullCheck(dsaObj.comparisionObjects.IsUpSellServicesModifiedRevenueMatch));
                cmd.Parameters.AddWithValue("@PricingModifier", NullCheck(dsaObj.comparisionObjects.IsPricingModifierMatch));

                cmd.Parameters.AddWithValue("@RecommendedsellingPrice", dsaObj.comparisionObjects.IsSpRecommendedsellingPriceMatch);
                cmd.Parameters.AddWithValue("@CompAnchorSellingPrice", dsaObj.comparisionObjects.IsCompAnchorsellingPriceMatch);
                cmd.Parameters.AddWithValue("@FloorSellingPrice", dsaObj.comparisionObjects.IsFloorSellingPriceMatch);

                cmd.Parameters.AddWithValue("@RecommendedSmartPriceRevenue", dsaObj.comparisionObjects.IsRecommenededSmartPriceRevenueMatch);
                cmd.Parameters.AddWithValue("@CompAnchorSmartPriceRevenue", dsaObj.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch);
                cmd.Parameters.AddWithValue("@FloorSmartPriceRevenue", dsaObj.comparisionObjects.IsFloorSmartPriceRevenueMatch);

                cmd.Parameters.AddWithValue("@RecommendedPricingModifier", dsaObj.comparisionObjects.IsRecommendedPricingModifierMatch);
                cmd.Parameters.AddWithValue("@CompAnchorPricingModifier", dsaObj.comparisionObjects.IsCompAnchorPricingModifierMatch);
                cmd.Parameters.AddWithValue("@FloorPricingModifier", dsaObj.comparisionObjects.IsFloorPricingModifierMatch);
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage", dsaObj.comparisionObjects.IsRecommendedDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage", dsaObj.comparisionObjects.IsCompAnchorDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@FloorDOLPercentage", dsaObj.comparisionObjects.IsFloorDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@StandardPartnerDiscountPercentage", dsaObj.StandardpartnerDiscountText==null? dsaObj.StandardpartnerDiscountText="": dsaObj.StandardpartnerDiscountText);
                cmd.Parameters.AddWithValue("@StandardPartnerMarginPercentage", "");
                cmd.Parameters.AddWithValue("@BaseMultiplier", "");
                cmd.Parameters.AddWithValue("@CompAccelerator", "");
                cmd.Parameters.AddWithValue("@CompDecelerator", "");
                cmd.Parameters.AddWithValue("@MarginRecommended", "");
                cmd.Parameters.AddWithValue("@MarginCompAnchor", "");
                cmd.Parameters.AddWithValue("@MarginFloor", "");
                cmd.Parameters.AddWithValue("@LeaseModifier", "");
                cmd.Parameters.AddWithValue("@CompModMin", "");
                cmd.Parameters.AddWithValue("@CompModMax", "");
                cmd.Parameters.AddWithValue("@RebateThreshold", "");
                cmd.Parameters.AddWithValue("@RebateType", "");
                cmd.Parameters.AddWithValue("@IsRebateAvailable", "");
                cmd.Parameters.AddWithValue("@CommissionMod1", "");
                cmd.Parameters.AddWithValue("@CommissionMod2", "");
                cmd.Parameters.AddWithValue("@CommissionMod3", "");
                cmd.Parameters.AddWithValue("@CommissionMod4", "");
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@FloorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@IsCustomerChanged", "");


                cmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
            catch(ShowStopperException ex)
            {
                ex.LogException();
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Exception is thrown.Please  Check InsertIntoDSASuiteValidationTable(), InsertResultClass ", true);
            }
            catch(NullReferenceException nullexception)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(nullexception.Message, true);
            }
            catch(Exception ex)
            {
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors(ex.Message, true);
                throw ex;
            }
            finally
            {
                _sqlCon.Close();
            }
            
        }
        
        private void InsertIntoDSATable(DSAPageObject dsaObj, string filepath, string currentdate, decimal totalsellingPrice, decimal totallistPrice, decimal totalPricingModifier, TestCase testcase, string scenarioId, int count,string TypeOfProduct="",CustomerClass customerObj=null)
        {
            try
            {
                //Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged,
                string sqlQuery = "INSERT INTO GPT_Automation_T1_DsaData (Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged) values (@Run_ID,@RUN_ID2,@Scenario_ID,@QuoteNumber,@PartnerAccount,@EndUserAccount,@SFDCDealID,@SFDCUnassEndUserC,@SFDCOpptyProbability,@SFDCType,@SFDCRegistrationStatus,@SFDCFulfilmentPath,@SFDCDealTypeC,@SFDCOpptyStage,@SFDCOpptyBookedDate,@SFDCDealRegOppty,@SFDCOpptyRecordType,@SFDCDistrAccountID,@HasSFDCInformationChanged,@AccountCustomerId,@AccountId,@QuoteAccountId,@EndUserAccountCustomerId,@TotalListPriceSummaryLevel,@TotalSellingPriceSummaryLevel,@TotalModifiedRevenue,@TotalPricingModifierSummeryLevel,@WithServicesIncentive,@WithDFSIncentive,@BaseProduct,@OrderCode,@SKU,@Quantity,@UnitListPrice,@UnitSellingPrice,@Discount,@TotalListPrice,@TotalSellingPrice,@ModifiedRevenue,@SmartPriceRevenue,@UpsellServicesModifiedRevenue,@PricingModifier,@RecommendedsellingPrice,@CompAnchorSellingPrice,@FloorSellingPrice,@RecommendedSmartPriceRevenue,@CompAnchorSmartPriceRevenue,@FloorSmartPriceRevenue,@RecommendedPricingModifier,@CompAnchorPricingModifier,@FloorPricingModifier,@RecommendedDOLPercentage,@CompAnchorDOLPercentage,@FloorDOLPercentage,@StandardPartnerDiscountPercentage,@StandardPartnerMarginPercentage,@BaseMultiplier,@CompAccelerator,@CompDecelerator,@MarginRecommended,@MarginCompAnchor,@MarginFloor,@LeaseModifier,@CompModMin,@CompModMax,@RebateThreshold,@RebateType,@IsRebateAvailable,@CommissionMod1,@CommissionMod2,@CommissionMod3,@CommissionMod4,@RecommendedDOLPercentage_Final,@CompAnchorDOLPercentage_Final,@FloorDOLPercentage_Final,@IsCustomerChanged)";
                _sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, _sqlCon);
                cmd.Parameters.AddWithValue("@Run_ID", "123");
                cmd.Parameters.AddWithValue("@RUN_ID2", "1234");
                cmd.Parameters.AddWithValue("@Scenario_ID", scenarioId);
                cmd.Parameters.AddWithValue("@QuoteNumber", dsaObj.QuoteNumber);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@EndUserAccount", customerObj.OldEndUserAccountCustomerId);
                   

                }
                else
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount","");
                    cmd.Parameters.AddWithValue("@EndUserAccount", "");
                    
                }
                cmd.Parameters.AddWithValue("@SFDCDealID", dsaObj.PnrLog.SFDCDEALID);
                cmd.Parameters.AddWithValue("@SFDCUnassEndUserC", dsaObj.PnrLog.IsSFDCUNASSIGNEDENDUSERC);
                cmd.Parameters.AddWithValue("@SFDCOpptyProbability", dsaObj.PnrLog.SFDCOPPTYPROBABILITY);
                cmd.Parameters.AddWithValue("@SFDCType", dsaObj.PnrLog.SFDCTYPE);
                cmd.Parameters.AddWithValue("@SFDCRegistrationStatus", dsaObj.PnrLog.SFDCREGISTRATIONSTATUS);
                cmd.Parameters.AddWithValue("@SFDCFulfilmentPath", dsaObj.PnrLog.SFDCFULFILMENTPATH);
                cmd.Parameters.AddWithValue("@SFDCDealTypeC", dsaObj.PnrLog.SFDCDEALTYPEC);
                cmd.Parameters.AddWithValue("@SFDCOpptyStage", dsaObj.PnrLog.IsSFDCOPPTYSTAGE);
                cmd.Parameters.AddWithValue("@SFDCOpptyBookedDate", dsaObj.PnrLog.SFDCOPPTYBOOKEDDATE);
                cmd.Parameters.AddWithValue("@SFDCDealRegOppty", dsaObj.PnrLog.SFDCDEALREGOPPTY);
                cmd.Parameters.AddWithValue("@SFDCOpptyRecordType", dsaObj.PnrLog.SFDCOPPTYRECORDTYPE);
                cmd.Parameters.AddWithValue("@SFDCDistrAccountID", dsaObj.PnrLog.SFDCDISTIACCOUNTID);
                cmd.Parameters.AddWithValue("@HasSFDCInformationChanged", dsaObj.PnrLog.HasSFDCInformationChanged);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@AccountId", customerObj.OldAccountId);
                    cmd.Parameters.AddWithValue("@QuoteAccountId", customerObj.OldQuoteAccountId);
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", customerObj.OldEndUserAccountCustomerId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", "");
                    cmd.Parameters.AddWithValue("@AccountId","");
                    cmd.Parameters.AddWithValue("@QuoteAccountId","");
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", "");
                }
                cmd.Parameters.AddWithValue("@TotalListPriceSummaryLevel", dsaObj.SummaryTotalListPrice);
                cmd.Parameters.AddWithValue("@TotalSellingPriceSummaryLevel", dsaObj.SummarySellingPrice);
                cmd.Parameters.AddWithValue("@TotalModifiedRevenue", dsaObj.SummaryModifiedRevenue);
                cmd.Parameters.AddWithValue("@TotalPricingModifierSummeryLevel", dsaObj.SummaryPricingModifier);
                cmd.Parameters.AddWithValue("@WithServicesIncentive", "");
                cmd.Parameters.AddWithValue("@WithDFSIncentive", "");
                cmd.Parameters.AddWithValue("@BaseProduct", TypeOfProduct);
                cmd.Parameters.AddWithValue("@OrderCode", dsaObj.OrderCode);
                cmd.Parameters.AddWithValue("@SKU", dsaObj.SKUNumber);
                cmd.Parameters.AddWithValue("@Quantity", dsaObj.Quantity);
                cmd.Parameters.AddWithValue("@UnitListPrice", dsaObj.UnitListPrice);
                cmd.Parameters.AddWithValue("@UnitSellingPrice", dsaObj.LineItemUnitSellingPrice);
                cmd.Parameters.AddWithValue("@Discount", dsaObj.LineItemUnitDiscount);
                cmd.Parameters.AddWithValue("@TotalListPrice", dsaObj.TotalListPrice);
                cmd.Parameters.AddWithValue("@TotalSellingPrice", dsaObj.TotalSellingPrice);
                cmd.Parameters.AddWithValue("@ModifiedRevenue", dsaObj.LineItemSummaryModifiedRevenue);
                cmd.Parameters.AddWithValue("@SmartPriceRevenue", dsaObj.LineItemSummerySmartPriceRevenue);
                cmd.Parameters.AddWithValue("@UpsellServicesModifiedRevenue", dsaObj.LineItemSummaryUpsellServicesModifiedRevenue);
                cmd.Parameters.AddWithValue("@PricingModifier", dsaObj.LineItemSummeryPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedsellingPrice", dsaObj.RecommenededSellingPrice);
                cmd.Parameters.AddWithValue("@CompAnchorSellingPrice", dsaObj.CompAnchorSellingPrice);
                cmd.Parameters.AddWithValue("@FloorSellingPrice", dsaObj.FloorSellingPrice);
                cmd.Parameters.AddWithValue("@RecommendedSmartPriceRevenue", dsaObj.RecommendedSmartPricRevenue);
                cmd.Parameters.AddWithValue("@CompAnchorSmartPriceRevenue", dsaObj.CompAnchorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@FloorSmartPriceRevenue", dsaObj.FloorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@RecommendedPricingModifier", dsaObj.RecommendedPricingModifier);
                cmd.Parameters.AddWithValue("@CompAnchorPricingModifier", dsaObj.CompAnchorPricingModifier);
                cmd.Parameters.AddWithValue("@FloorPricingModifier", dsaObj.FloorPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage", "");
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage", "");
                cmd.Parameters.AddWithValue("@FloorDOLPercentage", "");
                cmd.Parameters.AddWithValue("@StandardPartnerDiscountPercentage", "");
                cmd.Parameters.AddWithValue("@StandardPartnerMarginPercentage", "");
                cmd.Parameters.AddWithValue("@BaseMultiplier", "");
                cmd.Parameters.AddWithValue("@CompAccelerator", "");
                cmd.Parameters.AddWithValue("@CompDecelerator", "");
                cmd.Parameters.AddWithValue("@MarginRecommended", "");
                cmd.Parameters.AddWithValue("@MarginCompAnchor", "");
                cmd.Parameters.AddWithValue("@MarginFloor", "");
                cmd.Parameters.AddWithValue("@LeaseModifier", "");
                cmd.Parameters.AddWithValue("@CompModMin", "");
                cmd.Parameters.AddWithValue("@CompModMax", "");
                cmd.Parameters.AddWithValue("@RebateThreshold", "");
                cmd.Parameters.AddWithValue("@RebateType", "");
                cmd.Parameters.AddWithValue("@IsRebateAvailable", "");
                cmd.Parameters.AddWithValue("@CommissionMod1", "");
                cmd.Parameters.AddWithValue("@CommissionMod2", "");
                cmd.Parameters.AddWithValue("@CommissionMod3", "");
                cmd.Parameters.AddWithValue("@CommissionMod4", "");
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@FloorDOLPercentage_Final", "");
                cmd.Parameters.AddWithValue("@IsCustomerChanged", "");

               
                cmd.ExecuteNonQuery();
                _sqlCon.Close();
            }
            catch(Exception ex)
            {
                ShowStopperException exceptiondetailsObj = new ShowStopperException("",ex);
                exceptiondetailsObj.LogException();
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Exception is thrown.Please  Check InsertIntoDSATable(), InsertResultClass Error:"+ex.Message, true);
            }
            finally
            {
                _sqlCon.Close();
            }
        }
        void InsertIntoSuiteTable(DSAPageObject dsaObj, string filepath, string currentdate, decimal totalsellingPrice, decimal totallistPrice, decimal totalPricingModifier, TestCase testcase, string scenarioId, int count, string TypeOfProduct = "", CustomerClass customerObj=null)
        {
            try
            {
                //Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged,
                string sqlQuery = "INSERT INTO GPT_Automation_T2_SuiteData (Run_ID,RUN_ID2,Scenario_ID,QuoteNumber,PartnerAccount,EndUserAccount,SFDCDealID,SFDCUnassEndUserC,SFDCOpptyProbability,SFDCType,SFDCRegistrationStatus,SFDCFulfilmentPath,SFDCDealTypeC,SFDCOpptyStage,SFDCOpptyBookedDate,SFDCDealRegOppty,SFDCOpptyRecordType,SFDCDistrAccountID,HasSFDCInformationChanged,AccountCustomerId,AccountId,QuoteAccountId,EndUserAccountCustomerId,TotalListPriceSummaryLevel,TotalSellingPriceSummaryLevel,TotalModifiedRevenue,TotalPricingModifierSummeryLevel,WithServicesIncentive,WithDFSIncentive,BaseProduct,OrderCode,SKU,Quantity,UnitListPrice,UnitSellingPrice,Discount,TotalListPrice,TotalSellingPrice,ModifiedRevenue,SmartPriceRevenue,UpsellServicesModifiedRevenue,PricingModifier,RecommendedsellingPrice,CompAnchorSellingPrice,FloorSellingPrice,RecommendedSmartPriceRevenue,CompAnchorSmartPriceRevenue,FloorSmartPriceRevenue,RecommendedPricingModifier,CompAnchorPricingModifier,FloorPricingModifier,RecommendedDOLPercentage,CompAnchorDOLPercentage,FloorDOLPercentage,StandardPartnerDiscountPercentage,StandardPartnerMarginPercentage,BaseMultiplier,CompAccelerator,CompDecelerator,MarginRecommended,MarginCompAnchor,MarginFloor,LeaseModifier,CompModMin,CompModMax,RebateThreshold,RebateType,IsRebateAvailable,CommissionMod1,CommissionMod2,CommissionMod3,CommissionMod4,RecommendedDOLPercentage_Final,CompAnchorDOLPercentage_Final,FloorDOLPercentage_Final,IsCustomerChanged) values (@Run_ID,@RUN_ID2,@Scenario_ID,@QuoteNumber,@PartnerAccount,@EndUserAccount,@SFDCDealID,@SFDCUnassEndUserC,@SFDCOpptyProbability,@SFDCType,@SFDCRegistrationStatus,@SFDCFulfilmentPath,@SFDCDealTypeC,@SFDCOpptyStage,@SFDCOpptyBookedDate,@SFDCDealRegOppty,@SFDCOpptyRecordType,@SFDCDistrAccountID,@HasSFDCInformationChanged,@AccountCustomerId,@AccountId,@QuoteAccountId,@EndUserAccountCustomerId,@TotalListPriceSummaryLevel,@TotalSellingPriceSummaryLevel,@TotalModifiedRevenue,@TotalPricingModifierSummeryLevel,@WithServicesIncentive,@WithDFSIncentive,@BaseProduct,@OrderCode,@SKU,@Quantity,@UnitListPrice,@UnitSellingPrice,@Discount,@TotalListPrice,@TotalSellingPrice,@ModifiedRevenue,@SmartPriceRevenue,@UpsellServicesModifiedRevenue,@PricingModifier,@RecommendedsellingPrice,@CompAnchorSellingPrice,@FloorSellingPrice,@RecommendedSmartPriceRevenue,@CompAnchorSmartPriceRevenue,@FloorSmartPriceRevenue,@RecommendedPricingModifier,@CompAnchorPricingModifier,@FloorPricingModifier,@RecommendedDOLPercentage,@CompAnchorDOLPercentage,@FloorDOLPercentage,@StandardPartnerDiscountPercentage,@StandardPartnerMarginPercentage,@BaseMultiplier,@CompAccelerator,@CompDecelerator,@MarginRecommended,@MarginCompAnchor,@MarginFloor,@LeaseModifier,@CompModMin,@CompModMax,@RebateThreshold,@RebateType,@IsRebateAvailable,@CommissionMod1,@CommissionMod2,@CommissionMod3,@CommissionMod4,@RecommendedDOLPercentage_Final,@CompAnchorDOLPercentage_Final,@FloorDOLPercentage_Final,@IsCustomerChanged)";
                _sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, _sqlCon);
                cmd.Parameters.AddWithValue("@Run_ID", "");
                cmd.Parameters.AddWithValue("@RUN_ID2", "");
                cmd.Parameters.AddWithValue("@Scenario_ID", scenarioId);
                cmd.Parameters.AddWithValue("@QuoteNumber", dsaObj.QuoteNumber);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@EndUserAccount", customerObj.OldEndUserAccountCustomerId);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@PartnerAccount", "");
                    cmd.Parameters.AddWithValue("@EndUserAccount", "");

                }
                cmd.Parameters.AddWithValue("@SFDCDealID", dsaObj.PnrLog.SFDCDEALID);
                cmd.Parameters.AddWithValue("@SFDCUnassEndUserC", dsaObj.PnrLog.IsSFDCUNASSIGNEDENDUSERC);
                cmd.Parameters.AddWithValue("@SFDCOpptyProbability", dsaObj.PnrLog.SFDCOPPTYPROBABILITY);
                cmd.Parameters.AddWithValue("@SFDCType", dsaObj.PnrLog.SFDCTYPE);
                cmd.Parameters.AddWithValue("@SFDCRegistrationStatus", dsaObj.PnrLog.SFDCREGISTRATIONSTATUS);
                cmd.Parameters.AddWithValue("@SFDCFulfilmentPath", dsaObj.PnrLog.SFDCFULFILMENTPATH);
                cmd.Parameters.AddWithValue("@SFDCDealTypeC", dsaObj.PnrLog.SFDCDEALTYPEC);
                cmd.Parameters.AddWithValue("@SFDCOpptyStage", dsaObj.PnrLog.IsSFDCOPPTYSTAGE);
                cmd.Parameters.AddWithValue("@SFDCOpptyBookedDate", dsaObj.PnrLog.SFDCOPPTYBOOKEDDATE);
                cmd.Parameters.AddWithValue("@SFDCDealRegOppty", dsaObj.PnrLog.SFDCDEALREGOPPTY);
                cmd.Parameters.AddWithValue("@SFDCOpptyRecordType", dsaObj.PnrLog.SFDCOPPTYRECORDTYPE);
                cmd.Parameters.AddWithValue("@SFDCDistrAccountID", dsaObj.PnrLog.SFDCDISTIACCOUNTID);
                cmd.Parameters.AddWithValue("@HasSFDCInformationChanged", dsaObj.PnrLog.HasSFDCInformationChanged);
                if (customerObj != null)
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", customerObj.OldAccountCustomerId);
                    cmd.Parameters.AddWithValue("@AccountId", customerObj.OldAccountId);
                    cmd.Parameters.AddWithValue("@QuoteAccountId", customerObj.OldQuoteAccountId);
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", customerObj.OldEndUserAccountCustomerId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountCustomerId", "");
                    cmd.Parameters.AddWithValue("@AccountId", "");
                    cmd.Parameters.AddWithValue("@QuoteAccountId", "");
                    cmd.Parameters.AddWithValue("@EndUserAccountCustomerId", "");
                }
                cmd.Parameters.AddWithValue("@TotalListPriceSummaryLevel", dsaObj.SummaryTotalListPrice);
                cmd.Parameters.AddWithValue("@TotalSellingPriceSummaryLevel", dsaObj.SummarySellingPrice);
                cmd.Parameters.AddWithValue("@TotalModifiedRevenue", dsaObj.SummaryModifiedRevenue);
                cmd.Parameters.AddWithValue("@TotalPricingModifierSummeryLevel", dsaObj.SummaryPricingModifier);
                cmd.Parameters.AddWithValue("@WithServicesIncentive", "");
                cmd.Parameters.AddWithValue("@WithDFSIncentive", "");
                cmd.Parameters.AddWithValue("@BaseProduct", TypeOfProduct);
                cmd.Parameters.AddWithValue("@OrderCode", dsaObj.OrderCode);
                cmd.Parameters.AddWithValue("@SKU", dsaObj.SKUNumber);
                cmd.Parameters.AddWithValue("@Quantity", dsaObj.Quantity);
                cmd.Parameters.AddWithValue("@UnitListPrice", dsaObj.UnitListPrice);
                cmd.Parameters.AddWithValue("@UnitSellingPrice", dsaObj.LineItemUnitSellingPrice);
                cmd.Parameters.AddWithValue("@Discount", dsaObj.LineItemUnitDiscount);
                cmd.Parameters.AddWithValue("@TotalListPrice", dsaObj.TotalListPrice);
                cmd.Parameters.AddWithValue("@TotalSellingPrice", dsaObj.TotalSellingPrice);
                cmd.Parameters.AddWithValue("@ModifiedRevenue", dsaObj.LineItemSummaryModifiedRevenue);
                cmd.Parameters.AddWithValue("@SmartPriceRevenue", dsaObj.LineItemSummerySmartPriceRevenue);
                cmd.Parameters.AddWithValue("@UpsellServicesModifiedRevenue", dsaObj.LineItemSummaryUpsellServicesModifiedRevenue);
                cmd.Parameters.AddWithValue("@PricingModifier", dsaObj.LineItemSummeryPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedsellingPrice", dsaObj.RecommenededSellingPrice);
                cmd.Parameters.AddWithValue("@CompAnchorSellingPrice", dsaObj.CompAnchorSellingPrice);
                cmd.Parameters.AddWithValue("@FloorSellingPrice", dsaObj.FloorSellingPrice);
                cmd.Parameters.AddWithValue("@RecommendedSmartPriceRevenue", dsaObj.RecommendedSmartPricRevenue);
                cmd.Parameters.AddWithValue("@CompAnchorSmartPriceRevenue", dsaObj.CompAnchorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@FloorSmartPriceRevenue", dsaObj.FloorSmartPricRevenue);
                cmd.Parameters.AddWithValue("@RecommendedPricingModifier", dsaObj.RecommendedPricingModifier);
                cmd.Parameters.AddWithValue("@CompAnchorPricingModifier", dsaObj.CompAnchorPricingModifier);
                cmd.Parameters.AddWithValue("@FloorPricingModifier", dsaObj.FloorPricingModifier);
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage", dsaObj.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage);
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage", dsaObj.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage);
                cmd.Parameters.AddWithValue("@FloorDOLPercentage", dsaObj.PnrLog.SmartPriceGuidance.FloorDOLPercentage);
                cmd.Parameters.AddWithValue("@StandardPartnerDiscountPercentage", dsaObj.PnrLog.SmartPriceGuidance.StandardPartnerDiscountPercentage);
                cmd.Parameters.AddWithValue("@StandardPartnerMarginPercentage", dsaObj.PnrLog.SmartPriceGuidance.StandardPartnerMarginPercentage);
                cmd.Parameters.AddWithValue("@BaseMultiplier", dsaObj.PnrLog.SmartPriceGuidance.BaseMultiplier);
                cmd.Parameters.AddWithValue("@CompAccelerator", dsaObj.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage);
                cmd.Parameters.AddWithValue("@CompDecelerator", dsaObj.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage);
                cmd.Parameters.AddWithValue("@MarginRecommended", dsaObj.PnrLog.SmartPriceGuidance.MarginRecommended);
                cmd.Parameters.AddWithValue("@MarginCompAnchor", dsaObj.PnrLog.SmartPriceGuidance.MarginCompAnchor);
                cmd.Parameters.AddWithValue("@MarginFloor", dsaObj.PnrLog.SmartPriceGuidance.MarginFloor);
                cmd.Parameters.AddWithValue("@LeaseModifier", dsaObj.PnrLog.SmartPriceGuidance.LeaseModifier);
                cmd.Parameters.AddWithValue("@CompModMin", dsaObj.PnrLog.SmartPriceGuidance.CompModMin);
                cmd.Parameters.AddWithValue("@CompModMax", dsaObj.PnrLog.SmartPriceGuidance.CompModMax);
                cmd.Parameters.AddWithValue("@RebateThreshold", dsaObj.PnrLog.SmartPriceGuidance.RebateThreshold);
                cmd.Parameters.AddWithValue("@RebateType", dsaObj.PnrLog.SmartPriceGuidance.RebateType);
                cmd.Parameters.AddWithValue("@IsRebateAvailable", dsaObj.PnrLog.SmartPriceGuidance.IsRebateAvailable);
                cmd.Parameters.AddWithValue("@CommissionMod1", dsaObj.PnrLog.SmartPriceGuidance.CommissionMod1);
                cmd.Parameters.AddWithValue("@CommissionMod2", dsaObj.PnrLog.SmartPriceGuidance.CommissionMod2);
                cmd.Parameters.AddWithValue("@CommissionMod3", dsaObj.PnrLog.SmartPriceGuidance.CommissionMod3);
                cmd.Parameters.AddWithValue("@CommissionMod4", dsaObj.PnrLog.SmartPriceGuidance.CommissionMod4);
                cmd.Parameters.AddWithValue("@RecommendedDOLPercentage_Final", dsaObj.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@CompAnchorDOLPercentage_Final", dsaObj.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@FloorDOLPercentage_Final", dsaObj.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final);
                cmd.Parameters.AddWithValue("@IsCustomerChanged", "");


                cmd.ExecuteNonQuery();
                    }
            catch (Exception ex)
            {
                ShowStopperException exceptiondetailsObj = new ShowStopperException("", ex);
                exceptiondetailsObj.LogException();
                Bedrock.Utilities.Console_PresentationLayer.Report_Run_Errors("Exception is thrown.Please  Check InsertIntoSuiteTable(), InsertResultClass Error:" + ex.Message, true);
            }
            finally
            {
                _sqlCon.Close();
            }
        }

        public void InsertResults(List<DSAPageObject> results,string filepath,string currentdate, decimal TotalsellingPrice,decimal totallistPrice,decimal TotalPricingModifier, TestCase testcase, string scenarioId)
        {
            _sqlCon = new SqlConnection(@"Data Source=AUSDVSQLGRDAG03.aus.amer.dell.com;Initial Catalog=CCM;Integrated Security=true;");
            int count = 1;
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                    _sqlCon.Open();
                

                foreach (DSAPageObject rs in results)
                {
                    
                    InsertDataForProduct(rs,filepath,currentdate,TotalsellingPrice,totallistPrice,TotalPricingModifier,testcase,scenarioId,count,"Baseproduct");
                    if (rs.Accessories.Count > 0)
                    {
                        foreach(DSAPageObject accessory in rs.Accessories )
                        {
                            InsertDataForProduct(accessory, filepath, currentdate, TotalsellingPrice, totallistPrice, TotalPricingModifier, testcase, scenarioId, count,"Accessory");
                        }

                        
                    }
                    
                    
                    count++;
                }
                
                _sqlCon.Close();
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                //throw;
            }
            finally
            {
                if (_sqlCon.State == System.Data.ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
            }

        }


        
        private void InsertDataForProduct(DSAPageObject rs, string filepath, string currentdate, decimal TotalsellingPrice, decimal totallistPrice, decimal TotalPricingModifier, TestCase testcase, string scenarioId,int count,string TypeOfProduct)
        {
            SqlCommand sqlCmd = new SqlCommand("GPT_DSA_SEL_INSERT_TESTRESULT", _sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@RequestId", currentdate);
            sqlCmd.Parameters.AddWithValue("@QuoteNumber", rs.QuoteNumber.Trim());
            sqlCmd.Parameters.AddWithValue("@SKUNum", rs.SKUNumber.Trim());
            sqlCmd.Parameters.AddWithValue("@PartnerAccount", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@EndUserAccount", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@SFDCOpportunityID", "FutureReference".Trim());

            sqlCmd.Parameters.AddWithValue("@OpportunitySize", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@BookType", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@Stage", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@OpportunityType", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@ModifiedRevenue", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@WithServicesIncentive", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@WithDFSIncentive", "FutureReference".Trim());
            sqlCmd.Parameters.AddWithValue("@Products", count);

            sqlCmd.Parameters.AddWithValue("@UnitListPrice", rs.UnitListPrice.Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@UnitSellingPrice", rs.LineItemUnitSellingPrice.Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@TotalListPrice", rs.TotalListPrice.Trim());
            sqlCmd.Parameters.AddWithValue("@TotalSellingPrice", rs.TotalSellingPrice.Trim());
            sqlCmd.Parameters.AddWithValue("@ActualSmartPriceRevenue", rs.LineItemSummerySmartPriceRevenue.Replace("$", "").Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@ActualPricingModifier", rs.LineItemSummeryPricingModifier.Trim());

            sqlCmd.Parameters.AddWithValue("@DSA_RecommendedsellingPrice", rs.RecommenededSellingPrice.Replace("$", "").Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_RecommendedsellingPrice", rs.PnrLog.ProductUnitRecommendedgPrice.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsSpRecommendedsellingPriceMatch", rs.comparisionObjects.IsSpRecommendedsellingPriceMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_CompAnchorSellingPrice", rs.CompAnchorSellingPrice.Replace("$", "").Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_CompAnchorSellingPrice", rs.PnrLog.ProductUnitCompAnchorPrice.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsCompAnchorsellingPriceMatch", rs.comparisionObjects.IsCompAnchorsellingPriceMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_FloorSellingPrice", rs.FloorSellingPrice.Replace("$", "").Replace(",", "").Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_FloorSellingPrice", rs.PnrLog.ProductUnitFloorPrice.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsFloorSellingPriceMatch", rs.comparisionObjects.IsFloorSellingPriceMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_RecommendedSmartPriceRevenue", rs.RecommendedSmartPricRevenue.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_RecommendedSmartPriceRevenue", rs.CalculatedRecommendedSPCompFinancials.SmartPriceRevenue.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsRecommenededSmartPriceRevenueMatch", rs.comparisionObjects.IsRecommenededSmartPriceRevenueMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_CompAnchorSmartPriceRevenue", rs.CompAnchorSmartPricRevenue.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_CompAnchorSmartPriceRevenue", rs.CalculatedCompAnchorSPCompFinancials.SmartPriceRevenue.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsCompAnchorSmartPriceRevenueMatch", rs.comparisionObjects.IsCompAnchorSmartPriceRevenueMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_FloorSmartPriceRevenue", rs.FloorSmartPricRevenue.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_FloorSmartPriceRevenue", rs.CalculatedFloorSPCompFinancials.SmartPriceRevenue.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsFloorSmartPriceRevenueMatch", rs.comparisionObjects.IsFloorSmartPriceRevenueMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_RecommendedPricingModifier", rs.RecommendedPricingModifier.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_RecommendedPricingModifier", rs.CalculatedRecommendedSPCompFinancials.PricingModifier.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsRecommendedPricingModifierMatch", rs.comparisionObjects.IsRecommendedPricingModifierMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_CompAnchorPricingModifier", rs.CompAnchorPricingModifier.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_CompAnchorPricingModifier", rs.CalculatedCompAnchorSPCompFinancials.PricingModifier.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsCompAnchorPricingModifierMatch", rs.comparisionObjects.IsCompAnchorPricingModifierMatch);

            sqlCmd.Parameters.AddWithValue("@DSA_FloorPricingModifier", rs.FloorPricingModifier.Trim());
            sqlCmd.Parameters.AddWithValue("@Calculated_FloorPricingModifier", rs.CalculatedFloorSPCompFinancials.PricingModifier.ToString().Trim());
            sqlCmd.Parameters.AddWithValue("@IsFloorPricingModifierMatch", rs.comparisionObjects.IsFloorPricingModifierMatch);

            sqlCmd.Parameters.AddWithValue("@SP_RecommendedDOLPercentage", rs.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompAnchorDOLPercentage", rs.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_FloorDOLPercentage", rs.PnrLog.SmartPriceGuidance.FloorDOLPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_StandardPartnerDiscountPercentage", rs.PnrLog.SmartPriceGuidance.StandardPartnerDiscountPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_StandardPartnerMarginPercentage", rs.PnrLog.SmartPriceGuidance.StandardPartnerMarginPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_BaseMultiplier", rs.PnrLog.SmartPriceGuidance.BaseMultiplier.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompAccelerator", rs.PnrLog.SmartPriceGuidance.CompAccelerator.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompDecelerator", rs.PnrLog.SmartPriceGuidance.CompDecelerator.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_MarginRecommended", rs.PnrLog.SmartPriceGuidance.MarginRecommended.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_MarginCompAnchor", rs.PnrLog.SmartPriceGuidance.MarginCompAnchor.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_MarginFloor", rs.PnrLog.SmartPriceGuidance.MarginFloor.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_LeaseModifier", rs.PnrLog.SmartPriceGuidance.LeaseModifier.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompModMin", rs.PnrLog.SmartPriceGuidance.CompModMin.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompModMax", rs.PnrLog.SmartPriceGuidance.CompModMax.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_RebateThreshold", rs.PnrLog.SmartPriceGuidance.RebateThreshold.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_RebateType", rs.PnrLog.SmartPriceGuidance.RebateType.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_IsRebateAvailable", rs.PnrLog.SmartPriceGuidance.IsRebateAvailable.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CommissionMod1", rs.PnrLog.SmartPriceGuidance.CommissionMod1.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CommissionMod2", rs.PnrLog.SmartPriceGuidance.CommissionMod2.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CommissionMod3", rs.PnrLog.SmartPriceGuidance.CommissionMod3.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CommissionMod4", rs.PnrLog.SmartPriceGuidance.CommissionMod4.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_RecommendedDOLPercentage_Final", rs.PnrLog.SmartPriceGuidance.RecommendedDOLPercentage.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_CompAnchorDOLPercentage_Final", rs.PnrLog.SmartPriceGuidance.CompAnchorDOLPercentage_Final.Trim());
            sqlCmd.Parameters.AddWithValue("@SP_FloorDOLPercentage_Final", rs.PnrLog.SmartPriceGuidance.FloorDOLPercentage_Final.Trim());
            rs.TestCasesStatus = TestcaseStatus(rs.comparisionObjects, rs.TestCasesStatus);
            summaryTestCaseStatusList.Add(rs.TestCasesStatus);
            sqlCmd.Parameters.AddWithValue("@TestStatus", rs.TestCasesStatus);
            sqlCmd.Parameters.AddWithValue("@Comments", rs.StatusComments);
            sqlCmd.Parameters.AddWithValue("@FilePath", "Http://glonwspot360w.aus.amer.dell.com/gpttools/SP_DSA_UI_TEST_Report/" + currentdate + ".csv");
            sqlCmd.Parameters.AddWithValue("@TestTitle", testcase.TestCaseTitle);
            sqlCmd.Parameters.AddWithValue("@TestDescription", "Testing direct quotes built for customer FLORIDA MARLINS");
            sqlCmd.Parameters.AddWithValue("@Release", "0503");
            sqlCmd.Parameters.AddWithValue("@TestScenerio", scenarioId);

            sqlCmd.Parameters.AddWithValue("@DSA_TotalSellingPriceSummaryLevel", rs.SummarySellingPrice);
            sqlCmd.Parameters.AddWithValue("@Calculated_TotalSellingPriceSummaryLevel", TotalsellingPrice);
            sqlCmd.Parameters.AddWithValue("@IsTotalSellingPriceSummaryLevelMatched", rs.comparisionObjects.IsSellingPricMatchSummaryLevel);

            sqlCmd.Parameters.AddWithValue("@DSA_TotalListPriceSummaryLevel", rs.SummaryTotalListPrice);
            sqlCmd.Parameters.AddWithValue("@Calculated_TotalListPriceSummaryLevel", totallistPrice);
            sqlCmd.Parameters.AddWithValue("@IsTotalListPriceSummaryLevelMatched", rs.comparisionObjects.IsListPricMatchSummaryLevel);

            sqlCmd.Parameters.AddWithValue("@DSA_PricingModifierSummaryLevel", rs.SummaryPricingModifier);
            sqlCmd.Parameters.AddWithValue("@Calculated_PricingModifierSummaryLevel", TotalPricingModifier);
            sqlCmd.Parameters.AddWithValue("@IsPricingModifierSummaryLevelMatched", rs.comparisionObjects.IsTotalPricingModifierMatch);
            sqlCmd.Parameters.AddWithValue("@SFDCDEALID", rs.PnrLog.SFDCDEALID);
            sqlCmd.Parameters.AddWithValue("@SFDCUNASSIGNEDENDUSERC", rs.PnrLog.SFDCUNASSIGNEDENDUSERC);
            sqlCmd.Parameters.AddWithValue("@SFDCOPPTYPROBABILITY", rs.PnrLog.SFDCOPPTYPROBABILITY);
            sqlCmd.Parameters.AddWithValue("@SFDCTYPE", rs.PnrLog.SFDCTYPE);
            sqlCmd.Parameters.AddWithValue("@SFDCREGISTRATIONSTATUS", rs.PnrLog.SFDCREGISTRATIONSTATUS);
            sqlCmd.Parameters.AddWithValue("@SFDCFULFILMENTPATH", rs.PnrLog.SFDCFULFILMENTPATH);
            sqlCmd.Parameters.AddWithValue("@SFDCDEALTYPEC", rs.PnrLog.SFDCDEALTYPEC);
            sqlCmd.Parameters.AddWithValue("@SFDCOPPTYSTAGE", rs.PnrLog.SFDCOPPTYSTAGE);
            sqlCmd.Parameters.AddWithValue("@SFDCOPPTYBOOKEDDATE", rs.PnrLog.SFDCOPPTYBOOKEDDATE);
            sqlCmd.Parameters.AddWithValue("@SFDCDEALREGOPPTY", rs.PnrLog.SFDCDEALREGOPPTY);
            sqlCmd.Parameters.AddWithValue("@SFDCOPPTYRECORDTYPE", rs.PnrLog.SFDCOPPTYRECORDTYPE);
            sqlCmd.Parameters.AddWithValue("@SFDCDISTIACCOUNTID", rs.PnrLog.SFDCDISTIACCOUNTID);
            sqlCmd.Parameters.AddWithValue("@TIEID", rs.TiedId);
            sqlCmd.Parameters.AddWithValue("@ORDERCODE", rs.OrderCode);
            sqlCmd.Parameters.AddWithValue("@TIEPRODUCTTYPE",TypeOfProduct);
           // sqlCmd.Parameters.AddWithValue("@TIEPRODUCTTYPE",);
            sqlCmd.ExecuteNonQuery();
        }

        public void InsertResultsIntoSummaryTable(string TestCaseTitle,string currentdate,string scenarioId)
        {
            _sqlCon = new SqlConnection(@"Data Source=AUSDVSQLGRDAG03.aus.amer.dell.com;Initial Catalog=CCM;Integrated Security=true;");
            int count = 1;
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                    _sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("GPT_SP_DSA_INSERT_TESTRESULT_Summary", _sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@RequestId", currentdate);
                    sqlCmd.Parameters.AddWithValue("@ScenarioId", scenarioId);
                    sqlCmd.Parameters.AddWithValue("@TestCaseTitle", TestCaseTitle);
                    sqlCmd.Parameters.AddWithValue("@Release","0805");
                    sqlCmd.Parameters.AddWithValue("@TestStatus", TestCaseSummaryStatus());
                    
                    sqlCmd.ExecuteNonQuery();

                    count++;
                

                _sqlCon.Close();
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                //throw;
            }
            finally
            {
                if (_sqlCon.State == System.Data.ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
            }

        }
    }
}

        
    

