using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;

namespace SmartPrice_E2E_WebAutomation.DataFactory.DBConnection
{
    //Insert test results in sql sever db
    class InsertTestResult
    {
        public SqlConnection _sqlCon;

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



        public void InsertResults(List<DSAPageObject> results,string filepath,string currentdate, decimal TotalsellingPrice,decimal totallistPrice,decimal TotalPricingModifier, TestCase testcase, string scenarioId)
        {
            _sqlCon = new SqlConnection(@"Data Source=AUSDWSPOTSQLDB1.aus.amer.dell.com;Initial Catalog=GPTAUTOMATIONS;Integrated Security=true;");
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
            _sqlCon = new SqlConnection(@"Data Source=AUSDVSQLGRDAG03.aus.amer.dell.com;Initial Catalog=GPTAUTOMATIONS;Integrated Security=true;");
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

        
    

