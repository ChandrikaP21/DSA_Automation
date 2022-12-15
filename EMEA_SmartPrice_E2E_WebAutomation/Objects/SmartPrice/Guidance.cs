using System;
using System.Collections.Generic;
using System.Text;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice
   
{
   public class Guidance
    {
        public string RecommendedDOLPercentage = "".Replace("%","");
        public string CompAnchorDOLPercentage = "".Replace("%", "");
        public string FloorDOLPercentage = "".Replace("%", "");
        public string StandardPartnerDiscountPercentage = "".Replace("%", "");
        public string StandardPartnerMarginPercentage = "".Replace("%", "");
        public string BaseMultiplier = "";
        public string CompAccelerator = "";
        public string CompDecelerator = "";
        public string MarginRecommended = "";
        public string MarginCompAnchor = "";
        public string MarginFloor = "";
        public string LeaseModifier = "";
        public string CompModMin = "";
        public string CompModMax = "";
        public string CompmaxAccel = "";
        public string Compmaxdecel = "";
        public string RebateThreshold = "";
        public string RebateType = "";
        public string IsRebateAvailable = "";
        public string CommissionMod1 = "";
        public string CommissionMod2 = "";
        public string CommissionMod3 = "";
        public string CommissionMod4 = "";
        public string Quantity = "";
        public string StandardPartnerDiscount = "";

        public string RecommendedDOLPercentage_Final = "";
        public string CompAnchorDOLPercentage_Final = "";
        public string FloorDOLPercentage_Final = "";

       

            public void IdentifyFinalRCF(decimal unitProductListPrice,decimal unitCost)
        {
            /*
             * Step 1 : Calculate R, C and F Selling Price based on R,C and F DOLs
             * Formula to calculate DOL based selling price = List Price - (List Price * DOL %)  
                          */

            decimal unitSellingPrice_RecommendedDOL = unitProductListPrice - (unitProductListPrice * Convert.ToDecimal(RecommendedDOLPercentage) / 100);
            decimal unitSellingPrice_CompAnchorDOL = unitProductListPrice - (unitProductListPrice * Convert.ToDecimal(CompAnchorDOLPercentage) / 100);
            decimal unitSellingPrice_FloorDOL = unitProductListPrice - (unitProductListPrice * Convert.ToDecimal(FloorDOLPercentage) / 100);

            #region Hack to force margin based
            /*
            unitSellingPrice_RecommendedDOL = 0;
            unitSellingPrice_CompAnchorDOL = 0;
            unitSellingPrice_FloorDOL = 0;
            */
            #endregion

            /*
             * Step 2 : Calculate R, C and F Selling Price based on R,C and F Margins
             * Formula to calculate Margin based selling price = Cost / (1 - (Margin %))
             */
            try
            {
                decimal d = Math.Round(Convert.ToDecimal(MarginCompAnchor),2);
            }
            catch(Exception ex)
            {

            }
            

            decimal unitSellingPrice_RecommendedMargin = unitCost / (1 - Convert.ToDecimal(MarginRecommended) / 100);
            decimal unitSellingPrice_CompAnchorMargin = unitCost / (1 - Convert.ToDecimal(MarginCompAnchor) / 100);
            decimal unitSellingPrice_FloorMargin = unitCost / (1 - Convert.ToDecimal(MarginFloor) / 100);

            // Final Recommended DOL Identification
            if (unitSellingPrice_RecommendedDOL >= unitSellingPrice_RecommendedMargin)
                RecommendedDOLPercentage_Final = RecommendedDOLPercentage;
            else
                RecommendedDOLPercentage_Final = (100 - (unitSellingPrice_RecommendedMargin / unitProductListPrice * 100)).ToString();

            //Final Comp Anchor DOL Identification
            if (unitSellingPrice_CompAnchorDOL >= unitSellingPrice_CompAnchorMargin)
                CompAnchorDOLPercentage_Final = CompAnchorDOLPercentage;
            else
                CompAnchorDOLPercentage_Final = (100 - (unitSellingPrice_CompAnchorMargin / unitProductListPrice * 100)).ToString();

            //Final Floot DOL Identification
            if (unitSellingPrice_FloorDOL >= unitSellingPrice_FloorMargin)
                FloorDOLPercentage_Final = FloorDOLPercentage;
            else
                FloorDOLPercentage_Final = (100 - (unitSellingPrice_FloorMargin / unitProductListPrice * 100)).ToString();





        }
    }
}
