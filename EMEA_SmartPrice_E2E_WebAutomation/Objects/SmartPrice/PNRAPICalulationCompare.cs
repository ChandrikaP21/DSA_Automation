using System;
using System.Collections.Generic;
using System.Text;

using EMEA_SmartPrice_E2E_WebAutomation.Objects;


namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice
{
    public class PNRAPICalulationCompare
    {
        public string DSAUnitSellingPrice { get; set; }
        public bool IsRecommendedDOLPercentage_Final { get; set; }
        public bool IsCompAnchorDOLPercentage_Final { get; set; }
        public bool IsFloorDOLPercentage_Final { get; set; }
        public string CalUnitSellingPrice { get; set; }
        public string DSATotalSellingPrice { get; set; }
        public decimal CalTotalSellingPrice { get; set; }
        public decimal CalListPrice { get; set; }
        public bool IsTotalSellingPriceMatch { get; set; }
        public bool IsTotalListPriceMatch { get; set; }
    
        public bool IsActualSmartPriceRevenueMatch { get; set; }
        public bool IsModifiedRevenueMatch { get; set; }
        public bool IsUpSellServicesModifiedRevenueMatch { get; set; }
        public bool IsWithServicesIncentiveMatch { get; set; }
        public bool IsPricingModifierMatch { get; set; }
        public bool IsDFSModifierMatch { get; set; }
        public  bool IsTotalPricingModifierMatch { get; set; }// At summery level
        public string IsUnitSellingPriceMatch { get; set; }
        public string DSAListPricePerUnit { get; set; }
        public string CalListPricePerUnit { get; set; }
        public bool IsListPriceMatch { get; set; }
        public string DSADiscountedPricePerUnit { get; set; }
        public string CalDiscountedPricePerUnit { get; set; }
        public string IsDiscountedPricePerUnitMatch { get; set; }
        public string DsaRecommendedsellingPrice { get; set; }
        public string CalcRecommendedsellingPrice { get; set; }
        public bool IsSpRecommendedsellingPriceMatch { get; set; }
        public string DSARecommenededSmartPriceRevenue { get; set; }
        public string CalRecommenededSmartPriceRevenue { get; set; }
        public bool IsRecommenededSmartPriceRevenueMatch { get; set; }
        public string DSARecommendedPricingModifier { get; set; }
        public string CalcRecommendedPricingModifier { get; set; }
        public bool IsRecommendedPricingModifierMatch { get; set; }
        public string DSACompAnchorsellingPrice { get; set; }
        public string CalcCompAnchorsellingPrice { get; set; }
        public bool IsCompAnchorsellingPriceMatch { get; set; }
        public string DSACompAnchorSmartPriceRevenue { get; set; }
        public string CalCompAnchorSmartPriceRevenue { get; set; }
        public bool IsCompAnchorSmartPriceRevenueMatch { get; set; }
        public string DSACompAnchorPricingModifier { get; set; }
        public string CalcCompAnchorPricingModifier { get; set; }
        public bool IsCompAnchorPricingModifierMatch { get; set; }

        public string DSAFloorSellingPrice { get; set; }
        public string CalcFloorSellingPrice { get; set; }
        public bool IsFloorSellingPriceMatch { get; set; }

        public string DSAFloorSmartPriceRevenue { get; set; }
        public string CAlcFloorSmartPriceRevenue { get; set; }
        public bool IsFloorSmartPriceRevenueMatch { get; set; }

        public string DSAFloorPricingModifier { get; set; }
        public string CAlcFloorPricingModifier { get; set; }
        public bool IsFloorPricingModifierMatch { get; set; }
        public string DSACompAccel2 { get; set; }
        public string CalCompAccel2 { get; set; }
        public bool IsCompAccel2Match { get; set; }
        public string DSACompDecel2 { get; set; }
        public string CalCompDecel2 { get; set; }
        public bool IsCompDecel2Match { get; set; }
        public string DSACostPerUnit { get; set; }
        public string CalCostPerUnit { get; set; }
        public bool IsCostPerUnitMatch { get; set; }
        public string DSAQuantity { get; set; }
        public string CalQuantity { get; set; }
        public bool IsQuantityMatch { get; set; }
        public bool IsSkuMatch { get; set; }
        public string QuoteNumber { get; set; }
        public string SKUNum { get; set; }
        public string DSAMargin { get; set; }
        public string CalMargin { get; set; }
        public bool IsMarginMatched { get; set; }
        public string DSAUnitMargin { get; set; }
        public string CalcUnitMargin { get; set; }
        public string IsUnitMarginMatched { get; set; }
        public DSAPageObject dsaObjects { get; set; }
        public  decimal CalculatedTotalPricingModifier { get; set; }
        public decimal CalculatedTotalPricingModifierSummaryLevel { get; set; }
        public  decimal CalculatedTotalSellingpriceSummaryLevel { get; set; }
        public  decimal CalculatedTotalListpriceSummaryLevel { get; set; }
        public  bool IsSellingPricMatchSummaryLevel { get; set; }
        public  bool IsListPricMatchSummaryLevel { get; set; }
        public bool IsPricingModifierMatchSummaryLevel { get; set; }
        public bool IsModifierMatchSummaryLevel { get; set; }
        public  decimal TotalActualSmartPriceRevenue { get; set; }
        
        public decimal CalculatedTotalActualSmartPriceRevenueSummaryLevel { get; set; }

        public bool IsDirect = false;
        public bool IsResgistered = false;
        public bool IsPartner = false;
        public bool IsStandardPartnerDiscount = false;

    }

}
