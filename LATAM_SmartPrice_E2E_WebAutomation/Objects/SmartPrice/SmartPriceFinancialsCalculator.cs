using SmartPrice_E2E_WebAutomation.Objects.Quote;
using System;
using System.Collections.Generic;
using System.Text;

//using GPTAutomationSuite.bedrock.smartobjects;
//using DSATest.SmartPriceObject;
namespace SmartPrice_E2E_WebAutomation.Objects.SmartPrice
{
    public static class SmartPriceFinancialsCalculator
    {

        public static SmartPriceFinancials Calculate(Guidance guidance, decimal ListPrice, decimal sellingPrice)
        {
            decimal CompMod = 0;

            decimal recommendedPrice = 0;
            decimal compAnchorPrice = 0;
            decimal floorPrice = 0;


            decimal SmartPriceRevenue1 = 0;

            recommendedPrice = ListPrice - (ListPrice * Convert.ToDecimal(guidance.RecommendedDOLPercentage_Final) / 100);
            compAnchorPrice = ListPrice - (ListPrice * Convert.ToDecimal(guidance.CompAnchorDOLPercentage_Final) / 100);
            floorPrice = ListPrice - (ListPrice * Convert.ToDecimal(guidance.FloorDOLPercentage_Final) / 100);

            // Setting EMC Comm Modifier
            /*
                If Sell Price >= Reco, then Comm Mod = Comm Mod Max
                If Sell Price >= Comp Anchor, then Comm Mod = Comm Mod High
                If Sell Price >= Floor, then Comm Mod = Comm Mod Low
                Else Comm Mod = Comm Mod Min
            */

            if (sellingPrice >= recommendedPrice)
                CompMod = Convert.ToDecimal(guidance.CommissionMod1);
            else if (sellingPrice >= compAnchorPrice)
                CompMod = Convert.ToDecimal(guidance.CommissionMod2);
            else if (sellingPrice >= floorPrice)
                CompMod = Convert.ToDecimal(guidance.CommissionMod3);
            else
                CompMod = Convert.ToDecimal(guidance.CommissionMod4);

            // Calculate SmartPrice Revenue

            if (sellingPrice >= compAnchorPrice)
            {
                if (recommendedPrice >= compAnchorPrice)
                    if (sellingPrice >= recommendedPrice)
                    {
                        //SmartPrice Revn1 = Comp Anchor + (Reco – Comp Anchor) * Comp Accel 1 + (Sell price – Reco) * Comp Accel 2 + Sell Price * (Comm Mod – 1)
                        SmartPriceRevenue1 = compAnchorPrice + (recommendedPrice - compAnchorPrice) * Convert.ToDecimal(guidance.CompAccelerator)
                            + (sellingPrice - recommendedPrice) * Convert.ToDecimal(guidance.CompmaxAccel)
                            + sellingPrice * (CompMod - 1);
                    }
                    else
                    {
                        //SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Accel1 + Sell Price * (Comm Mod – 1)
                        SmartPriceRevenue1 = compAnchorPrice + (sellingPrice - compAnchorPrice) * Convert.ToDecimal(guidance.CompAccelerator) + sellingPrice * (CompMod - 1);
                    }
                else
                //Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Accel2 + Sell Price * (Comm Mod – 1)
                {
                    SmartPriceRevenue1 = compAnchorPrice + (sellingPrice - compAnchorPrice) * Convert.ToDecimal(guidance.CompmaxAccel) + sellingPrice * (CompMod - 1);
                }
            }
            //If Sell Price < Comp Anchor
            if (sellingPrice < compAnchorPrice)
            {
                //If Floor<Comp Anchor
                //If Sell Price < Floor
                if (floorPrice < compAnchorPrice)
                    if (sellingPrice < floorPrice)
                    {
                        //SmartPrice Revn1 = Comp Anchor + (Floor – Comp Anchor) *Comp Decel 1 + (Sell price – Floor) *Comp Decel 2 + Sell Price * (Comm Mod – 1)
                        SmartPriceRevenue1 = compAnchorPrice + (floorPrice - compAnchorPrice) * Convert.ToDecimal(guidance.CompDecelerator)
                            + (sellingPrice - floorPrice) * Convert.ToDecimal(guidance.Compmaxdecel)
                            + sellingPrice * (CompMod - 1);
                    }
                    else
                    {
                        // Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) *Comp Decel1 + Sell Price * (Comm Mod – 1)
                        SmartPriceRevenue1 = compAnchorPrice + (sellingPrice - compAnchorPrice) * Convert.ToDecimal(guidance.CompDecelerator) + sellingPrice * (CompMod - 1);

                    }
                else
                {
                    //Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) *Comp Decel2 + Sell Price * (Comm Mod – 1) 
                    SmartPriceRevenue1 = compAnchorPrice + (sellingPrice - compAnchorPrice) * Convert.ToDecimal(guidance.Compmaxdecel) + sellingPrice * (CompMod - 1);

                }
            }

            /*
                If SmartPrice Revn1 / Sell Price >= Comp Mod Max,
                        SmartPrice Revn = Sell Price * Comp Mod Max+ Sell Price * (Base Modifier – 1)
                If SmartPrice Revn1  /Sell Price < Comp Mod Min,
                        SmartPrice Revn = Sell Price * Comp Mod Min + Sell Price * (Base Modifier – 1)
                Else, SmartPrice Revn = SmartPrice Revn1 + Sell Price * (Base Modifier – 1)
             */

            if ((SmartPriceRevenue1 / sellingPrice) >= Convert.ToDecimal(guidance.CompModMax))
            {
                SmartPriceRevenue1 = sellingPrice * Convert.ToDecimal(guidance.CompModMax) + sellingPrice * (Convert.ToDecimal(guidance.BaseMultiplier) - 1);
            }
            else if ((SmartPriceRevenue1 / sellingPrice) < Convert.ToDecimal(guidance.CompModMin))
            {
                SmartPriceRevenue1 = sellingPrice * Convert.ToDecimal(guidance.CompModMin) + sellingPrice * (Convert.ToDecimal(guidance.BaseMultiplier) - 1);
            }
            else
            {
                SmartPriceRevenue1 = SmartPriceRevenue1 + sellingPrice * (Convert.ToDecimal(guidance.BaseMultiplier) - 1);
            }

            SmartPriceFinancials spFinancials = new SmartPriceFinancials();
            spFinancials.SmartPriceRevenue = SmartPriceRevenue1;
            spFinancials.PricingModifier = SmartPriceRevenue1 / sellingPrice;

            return spFinancials;

            // Calculation for modified revnue and upsell revenue can be deliverd as phase 2

        }
        //public static SmartPriceFinancials CalculateModifiedServices(SmartPriceFinancials financials, decimal totalProductSellingPrice, decimal totalServiceSellingPrice, decimal serviceModier, decimal totalSellingPriceOfUpsellService)
        //{


        //    return null;
        //}
        /// <summary>
        /// Calculate modified revenue,with Services Incentive,Upsell Services Modified Revenue
        /// </summary>
        /// <param name="guidance"></param>
        /// <param name="ListPrice"></param>
        /// <param name="SellingPriceServiceLevel"></param>
        /// <returns></returns>
        public static SmartPriceFinancials CalculateModifiedServices(SmartPriceFinancials financials,decimal totalProductSellingPrice,decimal UpsellServiceSellingPrice,decimal serviceSellingPrice,string ServiceModifier)
        {
            //unit selling price
            //dsaObjects.LineItemUnitSellingPrice;
            //unit service Selling Price
            // dsaObjects.HardwareSupportServices;
            //Percentage of Service contribution to Selling price

            #region Commented to avoid compilation errors.. 

           try
            {
                //foreach (AppliedPolicy appliedpolicy in policy)
                //{

                  if (!ServiceModifier.Equals(""))
                    {


                        //      decimal PercentageofService = (Convert.ToDecimal(dsaObjects.LineItemUnitSellingPrice) / totalCalcObj.CalculatedTotalServicesSellingPrice) * 100;
                        decimal PercentageofService = serviceSellingPrice / totalProductSellingPrice;
                        //  SmartPriceFinancials financials=new SmartPriceFinancials();

                        decimal servicesSmartPriceRevenue =   financials.SmartPriceRevenue* PercentageofService;

                    
                    financials.UpsellModifiedRevenue = servicesSmartPriceRevenue + UpsellServiceSellingPrice * (Convert.ToDecimal(ServiceModifier) - 1);// up sell selling price
                    
                    financials.ModifiedRevenue = financials.SmartPriceRevenue + serviceSellingPrice * (Convert.ToDecimal(ServiceModifier) - 1);//needs to be fixed.
                        financials.WithServicesIncentive = financials.ModifiedRevenue / totalProductSellingPrice;

                        
                    }
                   
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return financials;
        }
            

            #endregion

           // return null;

            /* Complete Formula for Reference

            Set Comm Mod
            If Sell Price >= Reco, then Comm Mod = Comm Mod Max
            If Sell Price >= Comp Anchor, then Comm Mod = Comm Mod High
            If Sell Price >= Floor, then Comm Mod = Comm Mod Low
            Else Comm Mod = Comm Mod Min

            Calculate SmartPrice Revn (SmartPrice Revn1 is draft state to calc final SmartPrice Revn)
            If Sell Price >= Comp Anchor
            If Reco >= Comp Anchor
            If Sell Price >= Reco
            SmartPrice Revn1 = Comp Anchor + (Reco – Comp Anchor) * Comp Accel 1 + (Sell price – Reco) * Comp Accel 2 + Sell Price * (Comm Mod – 1)
            Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Accel1 + Sell Price * (Comm Mod – 1)
            Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Accel2 + Sell Price * (Comm Mod – 1) 
            If Sell Price < Comp Anchor
            If Floor < Comp Anchor
            If Sell Price < Floor
            SmartPrice Revn1 = Comp Anchor + (Floor – Comp Anchor) * Comp Decel 1 + (Sell price – Floor) * Comp Decel 2 + Sell Price * (Comm Mod – 1)
            Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Decel1 + Sell Price * (Comm Mod – 1)
            Else, SmartPrice Revn1 = Comp Anchor + (Sell Price – Comp Anchor) * Comp Decel2 + Sell Price * (Comm Mod – 1) 

            If SmartPrice Revn1 / Sell Price >= Comp Mod Max,
            SmartPrice Revn = Sell Price * Comp Mod Max+ Sell Price * (Base Modifier – 1)
            If SmartPrice Revn1  /Sell Price < Comp Mod Min,
            SmartPrice Revn = Sell Price * Comp Mod Min + Sell Price * (Base Modifier – 1)
            Else, SmartPrice Revn = SmartPrice Revn1 + Sell Price * (Base Modifier – 1)

            */
        }

    }

