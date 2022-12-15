using SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPrice_E2E_WebAutomation.Objects.SmartPrice.Compare
{
  public class PNRAPILogCompare
    {
        public string OldSmartPriceId;
        public RequestParams OldInputParamObject;
        public SalesContext OldSalesContextObject;
        public Guidance OldGuidance;

        public string NewSmartPriceId;
        public RequestParams NewInputParamObject;
        public SalesContext NewSalesContextObject;
        public Guidance NewGuidance;

        // a false value indicates that there was a mis-match
        public bool Is_Context_MissMatch = false;
        public bool Is_Guidance_MissMatch = false;

        //Sales Context Parameter
        public bool Is_MissMatch_ItemClassCodeDesc = false;
        public bool Is_MissMatch_SKUNumberDescription = false;
        public bool Is_MissMatch_DellClassCode = false;
        public bool Is_MissMatch_SubClassCode = false;
        public bool Is_MissMatch_ManufactureCode = false;
        public bool Is_MissMatch_BULocalChannel = false;
        public bool Is_MissMatch_SLSBULVL4 = false;
        public bool Is_MissMatch_GeoBU = false;
        public bool Is_MissMatch_GeoCountry = false;
        public bool Is_MissMatch_IsRegistered = false;
        public bool Is_MissMatch_IsAccount = false;
        public bool Is_MissMatch_IsDirect = false;
        public bool Is_MissMatch_IsPartner = false;
        public bool Is_MissMatch_IsDistributor = false;
        public bool Is_MissMatch_DealSize = false;
        public bool Is_MissMatch_EndUserAccountExists = false;
        public bool Is_MissMatch_AccountGroup = false;
        public bool Is_MissMatch_AccountGroupHierarchy = false;
        public bool Is_MissMatch_PartnerRelationshipLevel = false;
        public bool Is_MissMatch_IsSmallDealPath = false;
        public bool Is_MissMatch_isGreenfieldOppty = false;
        public bool Is_MissMatch_BuyingPowerCategory = false;
        public bool Is_MissMatch_SalesCompensationRole = false;   // To Be Ignored
        public bool Is_MissMatch_KickerType = false;
        public bool Is_MissMatch_EndUserAccountRating = false;
        public bool Is_MissMatch_SegmentLocalChannelID = false;
        public bool Is_MissMatch_SLSBusinessUnit = false;
        public bool Is_MissMatch_SLSBULVL1 = false;
        public bool Is_MissMatch_SLSBULVL2 = false;
        public bool Is_MissMatch_SLSBULVL3 = false;
        public bool Is_MissMatch_ProductType = false;
        public bool Is_MissMatch_ProductGroup = false;
        public bool Is_MissMatch_ProductLOB = false;
        public bool Is_MissMatch_ProductBrandGroup = false;
        public bool Is_MissMatch_ProductBrandCategory = false;
        public bool Is_MissMatch_ProductFamilyParent = false;
        public bool Is_MissMatch_ProductFamilyDescription = false;
        public bool Is_MissMatch_ProductOffering = false;
        public bool Is_MissMatch_GeoRegion = false;
        public bool Is_MissMatch_GeoSubRegion = false;
        public bool Is_MissMatch_GeoArea = false;
        public bool Is_MissMatch_SegmentNo = false;
        public bool Is_MissMatch_ExchangeRate = false;
        public bool Is_MissMatch_TargetAmount = false;
        public bool Is_MissMatch_OpportunitySizeConverted = false;
        public bool Is_MissMatch_QuoteSizeConverted = false;
        public bool Is_MissMatch_ListPricePerUnitConverted = false;
        public bool Is_MissMatch_DiscountedPricePerUnitConverted = false;
        public bool Is_MissMatch_SIC1Code = false;
        public bool Is_MissMatch_SIC2Code = false;
        public bool Is_MissMatch_SIC3Code = false;
        public bool Is_MissMatch_LMPerCentage = false;
        public bool Is_MissMatch_CostPerUnitConverted = false;
        public bool Is_MissMatch_AdditionalInfo = false;  // To Be Ignored
        public bool Is_MissMatch_Errors = false;

        //Guidance Parameter

        public bool Is_MissMatch_RecommendedDOLPercentage = false;
        public bool Is_MissMatch_CompAnchorDOLPercentage = false;
        public bool Is_MissMatch_FloorDOLPercentage = false;
        public bool Is_MissMatch_StandardPartnerDiscountPercentage = false;
        public bool Is_MissMatch_StandardPartnerMarginPercentage = false;
        public bool Is_MissMatch_BaseMultiplier = false;
        public bool Is_MissMatch_CompAccelerator = false;
        public bool Is_MissMatch_CompDecelerator = false;
        public bool Is_MissMatch_MarginRecommended = false;
        public bool Is_MissMatch_MarginCompAnchor = false;
        public bool Is_MissMatch_MarginFloor = false;
        public bool Is_MissMatch_LeaseModifier = false;
        public bool Is_MissMatch_CompModMin = false;
        public bool Is_MissMatch_CompModMax = false;
        public bool Is_MissMatch_CompAccel2 = false;
        public bool Is_MissMatch_CompDecel2 = false;
        public bool Is_MissMatch_RebateThreshold = false;
        public bool Is_MissMatch_RebateType = false;
        public bool Is_MissMatch_IsRebateAvailable = false;
        public bool Is_MissMatch_CommissionMod1 = false;
        public bool Is_MissMatch_CommissionMod2 = false;
        public bool Is_MissMatch_CommissionMod3 = false;
        public bool Is_MissMatch_CommissionMod4 = false;


        //contructor
        public PNRAPILogCompare()
        {
            OldInputParamObject = new RequestParams();
            OldSalesContextObject = new SalesContext();
            OldGuidance = new Guidance();


            NewInputParamObject = new RequestParams();
            NewSalesContextObject = new SalesContext();
            NewGuidance = new Guidance();
        }

        

        public  void CompareContext()
        {
            bool isAtleastOneMisMatch = false; 

            if (NewSalesContextObject != null)           
            {
                /*
                if (OldSalesContextObject.ItemClassCodeDesc == NewSalesContextObject.ItemClassCodeDesc)
                {
                    Is_MissMatch_ItemClassCodeDesc = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SKUNumberDescription == NewSalesContextObject.SKUNumberDescription)
                {
                    Is_MissMatch_SKUNumberDescription = true; Is_Context_MissMatch = true;
                }
                


                if (OldSalesContextObject.DellClassCode == NewSalesContextObject.DellClassCode)
                {
                    Is_MissMatch_DellClassCode = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SubClassCode == NewSalesContextObject.SubClassCode)
                {
                    Is_MissMatch_SubClassCode = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ManufactureCode == NewSalesContextObject.ManufactureCode)
                {
                    Is_MissMatch_ManufactureCode = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.BULocalChannel == NewSalesContextObject.BULocalChannel)
                {
                    Is_MissMatch_BULocalChannel = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SLSBULVL4 == NewSalesContextObject.SLSBULVL4)
                {
                    Is_MissMatch_SLSBULVL4 = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.GeoBU == NewSalesContextObject.GeoBU)
                {
                    Is_MissMatch_GeoBU = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.GeoCountry == NewSalesContextObject.GeoCountry)
                {
                    Is_MissMatch_GeoCountry = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsRegistered == NewSalesContextObject.IsRegistered)
                {
                    Is_MissMatch_IsRegistered = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsAccount == NewSalesContextObject.IsAccount)
                {
                    Is_MissMatch_IsAccount = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsDirect == NewSalesContextObject.IsDirect)
                {
                    Is_MissMatch_IsDirect = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsPartner == NewSalesContextObject.IsPartner)
                {
                    Is_MissMatch_IsPartner = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsDistributor == NewSalesContextObject.IsDistributor)
                {
                    Is_MissMatch_IsDistributor = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.DealSize == NewSalesContextObject.DealSize)
                {
                    Is_MissMatch_DealSize = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.EndUserAccountExists == NewSalesContextObject.EndUserAccountExists)
                {
                    Is_MissMatch_EndUserAccountExists = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.AccountGroup == NewSalesContextObject.AccountGroup)
                {
                    Is_MissMatch_AccountGroup = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.AccountGroupHierarchy == NewSalesContextObject.AccountGroupHierarchy)
                {
                    Is_MissMatch_AccountGroupHierarchy = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.PartnerRelationshipLevel == NewSalesContextObject.PartnerRelationshipLevel)
                {
                    Is_MissMatch_PartnerRelationshipLevel = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.IsSmallDealPath == NewSalesContextObject.IsSmallDealPath)
                {
                    Is_MissMatch_IsSmallDealPath = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.isGreenfieldOppty == NewSalesContextObject.isGreenfieldOppty)
                {
                    Is_MissMatch_isGreenfieldOppty = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.BuyingPowerCategory == NewSalesContextObject.BuyingPowerCategory)
                {
                    Is_MissMatch_BuyingPowerCategory = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SalesCompensationRole == NewSalesContextObject.SalesCompensationRole)
                {
                    Is_MissMatch_SalesCompensationRole = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.KickerType == NewSalesContextObject.KickerType)
                {
                    Is_MissMatch_KickerType = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.EndUserAccountRating == NewSalesContextObject.EndUserAccountRating)
                {
                    Is_MissMatch_EndUserAccountRating = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SegmentLocalChannelID == NewSalesContextObject.SegmentLocalChannelID)
                {
                    Is_MissMatch_SegmentLocalChannelID = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SLSBusinessUnit == NewSalesContextObject.SLSBusinessUnit)
                {
                    Is_MissMatch_SLSBusinessUnit = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SLSBULVL1 == NewSalesContextObject.SLSBULVL1)
                {
                    Is_MissMatch_SLSBULVL1 = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SLSBULVL2 == NewSalesContextObject.SLSBULVL2)
                {
                    Is_MissMatch_SLSBULVL2 = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SLSBULVL3 == NewSalesContextObject.SLSBULVL3)
                {
                    Is_MissMatch_SLSBULVL3 = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductType == NewSalesContextObject.ProductType)
                {
                    Is_MissMatch_ProductType = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductGroup == NewSalesContextObject.ProductGroup)
                {
                    Is_MissMatch_ProductGroup = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductLOB == NewSalesContextObject.ProductLOB)
                {
                    Is_MissMatch_ProductLOB = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductBrandGroup == NewSalesContextObject.ProductBrandGroup)
                {
                    Is_MissMatch_ProductBrandGroup = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductBrandCategory == NewSalesContextObject.ProductBrandCategory)
                {
                    Is_MissMatch_ProductBrandCategory = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductFamilyParent == NewSalesContextObject.ProductFamilyParent)
                {
                    Is_MissMatch_ProductFamilyParent = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductFamilyDescription == NewSalesContextObject.ProductFamilyDescription)
                {
                    Is_MissMatch_ProductFamilyDescription = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ProductOffering == NewSalesContextObject.ProductOffering)
                {
                    Is_MissMatch_ProductOffering = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.GeoRegion == NewSalesContextObject.GeoRegion)
                {
                    Is_MissMatch_GeoRegion = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.GeoSubRegion == NewSalesContextObject.GeoSubRegion)
                {
                    Is_MissMatch_GeoSubRegion = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.GeoArea == NewSalesContextObject.GeoArea)
                {
                    Is_MissMatch_GeoArea = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SegmentNo == NewSalesContextObject.SegmentNo)
                {
                    Is_MissMatch_SegmentNo = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ExchangeRate == NewSalesContextObject.ExchangeRate)
                {
                    Is_MissMatch_ExchangeRate = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.TargetAmount == NewSalesContextObject.TargetAmount)
                {
                    Is_MissMatch_TargetAmount = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.OpportunitySizeConverted == NewSalesContextObject.OpportunitySizeConverted)
                {
                    Is_MissMatch_OpportunitySizeConverted = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.QuoteSizeConverted == NewSalesContextObject.QuoteSizeConverted)
                {
                    Is_MissMatch_QuoteSizeConverted = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.ListPricePerUnitConverted == NewSalesContextObject.ListPricePerUnitConverted)
                {
                    Is_MissMatch_ListPricePerUnitConverted = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.DiscountedPricePerUnitConverted == NewSalesContextObject.DiscountedPricePerUnitConverted)
                {
                    Is_MissMatch_DiscountedPricePerUnitConverted = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SIC1Code == NewSalesContextObject.SIC1Code)
                {
                    Is_MissMatch_SIC1Code = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SIC2Code == NewSalesContextObject.SIC2Code)
                {
                    Is_MissMatch_SIC2Code = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.SIC3Code == NewSalesContextObject.SIC3Code)
                {
                    Is_MissMatch_SIC3Code = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.LMPerCentage == NewSalesContextObject.LMPerCentage)
                {
                    Is_MissMatch_LMPerCentage = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.CostPerUnitConverted == NewSalesContextObject.CostPerUnitConverted)
                {
                    Is_MissMatch_CostPerUnitConverted = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.AdditionalInfo == NewSalesContextObject.AdditionalInfo)
                {
                    Is_MissMatch_AdditionalInfo = true; Is_Context_MissMatch = true;
                }

                if (OldSalesContextObject.Errors == NewSalesContextObject.Errors)
                {
                    Is_MissMatch_Errors = true; Is_Context_MissMatch = true;
                }
                */
                if (OldSalesContextObject.ItemClassCodeDesc == NewSalesContextObject.ItemClassCodeDesc)
                {
                    Is_MissMatch_ItemClassCodeDesc = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SKUNumberDescription == NewSalesContextObject.SKUNumberDescription)
                {
                    Is_MissMatch_SKUNumberDescription = true;
                }
                else isAtleastOneMisMatch = true;



                if (OldSalesContextObject.DellClassCode == NewSalesContextObject.DellClassCode)
                {
                    Is_MissMatch_DellClassCode = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SubClassCode == NewSalesContextObject.SubClassCode)
                {
                    Is_MissMatch_SubClassCode = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ManufactureCode == NewSalesContextObject.ManufactureCode)
                {
                    Is_MissMatch_ManufactureCode = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.BULocalChannel == NewSalesContextObject.BULocalChannel)
                {
                    Is_MissMatch_BULocalChannel = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SLSBULVL4 == NewSalesContextObject.SLSBULVL4)
                {
                    Is_MissMatch_SLSBULVL4 = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.GeoBU == NewSalesContextObject.GeoBU)
                {
                    Is_MissMatch_GeoBU = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.GeoCountry == NewSalesContextObject.GeoCountry)
                {
                    Is_MissMatch_GeoCountry = true;
                }
                else isAtleastOneMisMatch = true;

                /*if (OldSalesContextObject.IsRegistered == NewSalesContextObject.IsRegistered)
                {
                    Is_MissMatch_IsRegistered = true;
                }
                else isAtleastOneMisMatch = true;
                */
                Is_MissMatch_IsRegistered = true;

                if (OldSalesContextObject.IsAccount == NewSalesContextObject.IsAccount)
                {
                    Is_MissMatch_IsAccount = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.IsDirect == NewSalesContextObject.IsDirect)
                {
                    Is_MissMatch_IsDirect = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.IsPartner == NewSalesContextObject.IsPartner)
                {
                    Is_MissMatch_IsPartner = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.IsDistributor == NewSalesContextObject.IsDistributor)
                {
                    Is_MissMatch_IsDistributor = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.DealSize == NewSalesContextObject.DealSize)
                {
                    Is_MissMatch_DealSize = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.EndUserAccountExists == NewSalesContextObject.EndUserAccountExists)
                {
                    Is_MissMatch_EndUserAccountExists = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.AccountGroup == NewSalesContextObject.AccountGroup)
                {
                    Is_MissMatch_AccountGroup = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.AccountGroupHierarchy == NewSalesContextObject.AccountGroupHierarchy)
                {
                    Is_MissMatch_AccountGroupHierarchy = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.PartnerRelationshipLevel == NewSalesContextObject.PartnerRelationshipLevel)
                {
                    Is_MissMatch_PartnerRelationshipLevel = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.IsSmallDealPath == NewSalesContextObject.IsSmallDealPath)
                {
                    Is_MissMatch_IsSmallDealPath = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.isGreenfieldOppty == NewSalesContextObject.isGreenfieldOppty)
                {
                    Is_MissMatch_isGreenfieldOppty = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.BuyingPowerCategory == NewSalesContextObject.BuyingPowerCategory)
                {
                    Is_MissMatch_BuyingPowerCategory = true;
                }
                else isAtleastOneMisMatch = true;

                /* if (OldSalesContextObject.SalesCompensationRole == NewSalesContextObject.SalesCompensationRole)
                 {
                     Is_MissMatch_SalesCompensationRole = true;
                 }
                 else isAtleastOneMisMatch = true;
                 */
                Is_MissMatch_SalesCompensationRole = true;
                if (OldSalesContextObject.KickerType == NewSalesContextObject.KickerType)
                {
                    Is_MissMatch_KickerType = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.EndUserAccountRating == NewSalesContextObject.EndUserAccountRating)
                {
                    Is_MissMatch_EndUserAccountRating = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SegmentLocalChannelID == NewSalesContextObject.SegmentLocalChannelID)
                {
                    Is_MissMatch_SegmentLocalChannelID = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SLSBusinessUnit == NewSalesContextObject.SLSBusinessUnit)
                {
                    Is_MissMatch_SLSBusinessUnit = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SLSBULVL1 == NewSalesContextObject.SLSBULVL1)
                {
                    Is_MissMatch_SLSBULVL1 = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SLSBULVL2 == NewSalesContextObject.SLSBULVL2)
                {
                    Is_MissMatch_SLSBULVL2 = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SLSBULVL3 == NewSalesContextObject.SLSBULVL3)
                {
                    Is_MissMatch_SLSBULVL3 = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductType == NewSalesContextObject.ProductType)
                {
                    Is_MissMatch_ProductType = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductGroup == NewSalesContextObject.ProductGroup)
                {
                    Is_MissMatch_ProductGroup = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductLOB == NewSalesContextObject.ProductLOB)
                {
                    Is_MissMatch_ProductLOB = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductBrandGroup == NewSalesContextObject.ProductBrandGroup)
                {
                    Is_MissMatch_ProductBrandGroup = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductBrandCategory == NewSalesContextObject.ProductBrandCategory)
                {
                    Is_MissMatch_ProductBrandCategory = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductFamilyParent == NewSalesContextObject.ProductFamilyParent)
                {
                    Is_MissMatch_ProductFamilyParent = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductFamilyDescription == NewSalesContextObject.ProductFamilyDescription)
                {
                    Is_MissMatch_ProductFamilyDescription = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ProductOffering == NewSalesContextObject.ProductOffering)
                {
                    Is_MissMatch_ProductOffering = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.GeoRegion == NewSalesContextObject.GeoRegion)
                {
                    Is_MissMatch_GeoRegion = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.GeoSubRegion == NewSalesContextObject.GeoSubRegion)
                {
                    Is_MissMatch_GeoSubRegion = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.GeoArea == NewSalesContextObject.GeoArea)
                {
                    Is_MissMatch_GeoArea = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SegmentNo == NewSalesContextObject.SegmentNo)
                {
                    Is_MissMatch_SegmentNo = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ExchangeRate == NewSalesContextObject.ExchangeRate)
                {
                    Is_MissMatch_ExchangeRate = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.TargetAmount == NewSalesContextObject.TargetAmount)
                {
                    Is_MissMatch_TargetAmount = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.OpportunitySizeConverted == NewSalesContextObject.OpportunitySizeConverted)
                {
                    Is_MissMatch_OpportunitySizeConverted = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.QuoteSizeConverted == NewSalesContextObject.QuoteSizeConverted)
                {
                    Is_MissMatch_QuoteSizeConverted = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.ListPricePerUnitConverted == NewSalesContextObject.ListPricePerUnitConverted)
                {
                    Is_MissMatch_ListPricePerUnitConverted = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.DiscountedPricePerUnitConverted == NewSalesContextObject.DiscountedPricePerUnitConverted)
                {
                    Is_MissMatch_DiscountedPricePerUnitConverted = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SIC1Code == NewSalesContextObject.SIC1Code)
                {
                    Is_MissMatch_SIC1Code = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SIC2Code == NewSalesContextObject.SIC2Code)
                {
                    Is_MissMatch_SIC2Code = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.SIC3Code == NewSalesContextObject.SIC3Code)
                {
                    Is_MissMatch_SIC3Code = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.LMPerCentage == NewSalesContextObject.LMPerCentage)
                {
                    Is_MissMatch_LMPerCentage = true;
                }
                else isAtleastOneMisMatch = true;

                if (OldSalesContextObject.CostPerUnitConverted == NewSalesContextObject.CostPerUnitConverted)
                {
                    Is_MissMatch_CostPerUnitConverted = true;
                }
                else isAtleastOneMisMatch = true;

                /* if (OldSalesContextObject.AdditionalInfo == NewSalesContextObject.AdditionalInfo)
                 {
                    Is_MissMatch_AdditionalInfo = true;
                 }
                 else isAtleastOneMisMatch = true;

                 if (OldSalesContextObject.Errors == NewSalesContextObject.Errors)
                 {
                    Is_MissMatch_Errors = true;
                 }
                 else isAtleastOneMisMatch = true;
             */

                Is_MissMatch_AdditionalInfo = true;
                Is_MissMatch_Errors = true;

                if (isAtleastOneMisMatch == false) Is_Context_MissMatch = true;
            }

            }
        

        public void CompareGuidance()
        {
            bool isAtleastOneMissMatch = false;

            if (NewGuidance != null)
            {
                /*
                if (OldGuidance.RecommendedDOLPercentage == NewGuidance.RecommendedDOLPercentage)
                {
                    Is_MissMatch_RecommendedDOLPercentage = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompAnchorDOLPercentage == NewGuidance.CompAnchorDOLPercentage)
                {
                    Is_MissMatch_CompAnchorDOLPercentage = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.FloorDOLPercentage == NewGuidance.FloorDOLPercentage)
                {
                    Is_MissMatch_FloorDOLPercentage = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.StandardPartnerDiscountPercentage == NewGuidance.StandardPartnerDiscountPercentage)
                {
                    Is_MissMatch_StandardPartnerDiscountPercentage = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.StandardPartnerMarginPercentage == NewGuidance.StandardPartnerMarginPercentage)
                {
                    Is_MissMatch_StandardPartnerMarginPercentage = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.BaseMultiplier == NewGuidance.BaseMultiplier)
                {
                    Is_MissMatch_BaseMultiplier = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompAccelerator == NewGuidance.CompAccelerator)
                {
                    Is_MissMatch_CompAccelerator = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompDecelerator == NewGuidance.CompDecelerator)
                {
                    Is_MissMatch_CompDecelerator = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.MarginRecommended == NewGuidance.MarginRecommended)
                {
                    Is_MissMatch_MarginRecommended = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.MarginCompAnchor == NewGuidance.MarginCompAnchor)
                {
                    Is_MissMatch_MarginCompAnchor = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.MarginFloor == NewGuidance.MarginFloor)
                {
                    Is_MissMatch_MarginFloor = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.LeaseModifier == NewGuidance.LeaseModifier)
                {
                    Is_MissMatch_LeaseModifier = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompModMin == NewGuidance.CompModMin)
                {
                    Is_MissMatch_CompModMin = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompModMax == NewGuidance.CompModMax)
                {
                    Is_MissMatch_CompModMax = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompAccel2 == NewGuidance.CompAccel2)
                {
                    Is_MissMatch_CompAccel2 = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CompDecel2 == NewGuidance.CompDecel2)
                {
                    Is_MissMatch_CompDecel2 = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.RebateThreshold == NewGuidance.RebateThreshold)
                {
                    Is_MissMatch_RebateThreshold = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.RebateType == NewGuidance.RebateType)
                {
                    Is_MissMatch_RebateType = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.IsRebateAvailable == NewGuidance.IsRebateAvailable)
                {
                    Is_MissMatch_IsRebateAvailable = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CommissionMod1 == NewGuidance.CommissionMod1)
                {
                    Is_MissMatch_CommissionMod1 = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CommissionMod2 == NewGuidance.CommissionMod2)
                {
                    Is_MissMatch_CommissionMod2 = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CommissionMod3 == NewGuidance.CommissionMod3)
                {
                    Is_MissMatch_CommissionMod3 = true; Is_Guidance_MissMatch = true;
                }

                if (OldGuidance.CommissionMod4 == NewGuidance.CommissionMod4)
                {
                    Is_MissMatch_CommissionMod4 = true; Is_Guidance_MissMatch = true;
                }
                */

                if (OldGuidance.RecommendedDOLPercentage == NewGuidance.RecommendedDOLPercentage)
                {
                    Is_MissMatch_RecommendedDOLPercentage = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompAnchorDOLPercentage == NewGuidance.CompAnchorDOLPercentage)
                {
                    Is_MissMatch_CompAnchorDOLPercentage = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.FloorDOLPercentage == NewGuidance.FloorDOLPercentage)
                {
                    Is_MissMatch_FloorDOLPercentage = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.StandardPartnerDiscountPercentage == NewGuidance.StandardPartnerDiscountPercentage)
                {
                    Is_MissMatch_StandardPartnerDiscountPercentage = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.StandardPartnerMarginPercentage == NewGuidance.StandardPartnerMarginPercentage)
                {
                    Is_MissMatch_StandardPartnerMarginPercentage = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.BaseMultiplier == NewGuidance.BaseMultiplier)
                {
                    Is_MissMatch_BaseMultiplier = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompAccelerator == NewGuidance.CompAccelerator)
                {
                    Is_MissMatch_CompAccelerator = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompDecelerator == NewGuidance.CompDecelerator)
                {
                    Is_MissMatch_CompDecelerator = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.MarginRecommended == NewGuidance.MarginRecommended)
                {
                    Is_MissMatch_MarginRecommended = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.MarginCompAnchor == NewGuidance.MarginCompAnchor)
                {
                    Is_MissMatch_MarginCompAnchor = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.MarginFloor == NewGuidance.MarginFloor)
                {
                    Is_MissMatch_MarginFloor = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.LeaseModifier == NewGuidance.LeaseModifier)
                {
                    Is_MissMatch_LeaseModifier = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompModMin == NewGuidance.CompModMin)
                {
                    Is_MissMatch_CompModMin = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompModMax == NewGuidance.CompModMax)
                {
                    Is_MissMatch_CompModMax = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompAccel2 == NewGuidance.CompAccel2)
                {
                    Is_MissMatch_CompAccel2 = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CompDecel2 == NewGuidance.CompDecel2)
                {
                    Is_MissMatch_CompDecel2 = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.RebateThreshold == NewGuidance.RebateThreshold)
                {
                    Is_MissMatch_RebateThreshold = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.RebateType == NewGuidance.RebateType)
                {
                    Is_MissMatch_RebateType = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.IsRebateAvailable == NewGuidance.IsRebateAvailable)
                {
                    Is_MissMatch_IsRebateAvailable = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CommissionMod1 == NewGuidance.CommissionMod1)
                {
                    Is_MissMatch_CommissionMod1 = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CommissionMod2 == NewGuidance.CommissionMod2)
                {
                    Is_MissMatch_CommissionMod2 = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CommissionMod3 == NewGuidance.CommissionMod3)
                {
                    Is_MissMatch_CommissionMod3 = true;
                }
                else isAtleastOneMissMatch = true;

                if (OldGuidance.CommissionMod4 == NewGuidance.CommissionMod4)
                {
                    Is_MissMatch_CommissionMod4 = true;
                }
                else isAtleastOneMissMatch = true;

                if (isAtleastOneMissMatch == false) Is_Guidance_MissMatch = true; // this means there no mismatches

            }

        }


    }
}
