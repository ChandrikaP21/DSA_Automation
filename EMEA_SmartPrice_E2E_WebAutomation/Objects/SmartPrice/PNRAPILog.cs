using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock;
using System;
using System.Collections.Generic;
using System.Text;


namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice
{
    //retrive data from pnrapilogs 
   public class PNRAPILog
    {
        public decimal ListPricePerUnit = 0;
        public decimal DiscountedPricePerUnit = 0;
        public Guidance SmartPriceGuidance = new Guidance();
        //public string RecommendedDOLPercentage = string.Empty;
        //public string CompAnchorDOLPercentage = string.Empty;
        //public string FloorDOLPercentage = string.Empty;
        //public string CompAccel2 = string.Empty;  
        //public string CompDecel2 = string.Empty;
        public decimal CostPerUnit =0;
        //public int Quantity=0 ;
        public decimal ProductUnitRecommendedgPrice =0;
       public  decimal ProductUnitCompAnchorPrice = 0;
       public decimal ProductUnitFloorPrice = 0;
        public string SmartPriceID = string.Empty;
        public string SFDCDEALID = string.Empty;
        public string SFDCUNASSIGNEDENDUSERC = string.Empty;
        public string SFDCOPPTYPROBABILITY = string.Empty;
        public string SFDCREGISTRATIONSTATUS = string.Empty;
        public string SFDCFULFILMENTPATH = string.Empty;
        public string SFDCDEALTYPEC = string.Empty;
        public string SFDCOPPTYSTAGE = string.Empty;
        public string SFDCOPPTYBOOKEDDATE = string.Empty;
        public string SFDCDEALREGOPPTY = string.Empty;
        public string SFDCOPPTYRECORDTYPE = string.Empty;
        public string SFDCDISTIACCOUNTID = string.Empty;
        public string SFDCTYPE = string.Empty;
        public string HasSFDCInformationChanged = string.Empty;

        // public List<GuidanceMessage> GUIDANCEMESSAGESJSON = null;
        public string GUIDANCEMESSAGESJSON = null;
        public string OPPORTUNITYSIZE { get; set; }
        public string AdditionalInfo { get; set; }
        public string ServiceType = string.Empty;
        public string QuoteNumber { get; set; }
        public string ServiceModifier { get; set; }
        public string SKUNum { get; set; }
        public List<AppliedPolicy> AppliedPolicies = null;
        public string ValidOppportunitySPDecision { get; set; }


        public string IsDirect { get; set; }
        public string IsRegistered { get; set; }
        public string IsPartner { get; set; }
        public string StandardPartnerDiscount { get; set; }

        public bool IsSFDCDEALID = false;
        public bool IsSFDCUNASSIGNEDENDUSERC = false;
        public bool IsSFDCOPPTYPROBABILITY = false;
        public bool IsSFDCREGISTRATIONSTATUS = false;
        public bool IsSFDCFULFILMENTPATH = false;
        public bool IsSFDCDEALTYPEC = false;
        public bool IsSFDCOPPTYSTAGE = false;
        public bool IsSFDCOPPTYBOOKEDDATE = false;
        public bool IsSFDCDEALREGOPPTY = false;
        public bool IsSFDCOPPTYRECORDTYPE = false;
        public bool IsSFDCDISTIACCOUNTID = false;
        public bool IsSFDCTYPE = false;


        #region Method
        public void CompareSFDCDetails(PNRAPILog pnrApilog1,PNRAPILog pnrApilog2)
        {
            try
            {
                if (pnrApilog1.SFDCDEALID == pnrApilog2.SFDCDEALID)
                {
                    pnrApilog1.IsSFDCDEALID = true;

                }
                if (pnrApilog1.SFDCUNASSIGNEDENDUSERC == pnrApilog2.SFDCUNASSIGNEDENDUSERC)
                {
                    pnrApilog1.IsSFDCUNASSIGNEDENDUSERC = true;

                }
                if (pnrApilog1.SFDCOPPTYPROBABILITY == pnrApilog2.SFDCOPPTYPROBABILITY)
                {
                    pnrApilog1.IsSFDCOPPTYPROBABILITY = true;

                }
                if (pnrApilog1.SFDCREGISTRATIONSTATUS == pnrApilog2.SFDCREGISTRATIONSTATUS)
                {
                    pnrApilog1.IsSFDCREGISTRATIONSTATUS = true;

                }
                if (pnrApilog1.SFDCFULFILMENTPATH == pnrApilog2.SFDCFULFILMENTPATH)
                {
                    pnrApilog1.IsSFDCFULFILMENTPATH = true;

                }
                if (pnrApilog1.SFDCDEALTYPEC == pnrApilog2.SFDCDEALTYPEC)
                {
                    pnrApilog1.IsSFDCDEALTYPEC = true;

                }
                if (pnrApilog1.SFDCOPPTYSTAGE == pnrApilog2.SFDCOPPTYSTAGE)
                {
                    pnrApilog1.IsSFDCOPPTYSTAGE = true;

                }
                if (pnrApilog1.SFDCOPPTYBOOKEDDATE == pnrApilog2.SFDCOPPTYBOOKEDDATE)
                {
                    pnrApilog1.IsSFDCOPPTYBOOKEDDATE = true;

                }
                if (pnrApilog1.SFDCDEALREGOPPTY == pnrApilog2.SFDCDEALREGOPPTY)
                {
                    pnrApilog1.IsSFDCDEALREGOPPTY = true;

                }
                if (pnrApilog1.SFDCOPPTYRECORDTYPE == pnrApilog2.SFDCOPPTYRECORDTYPE)
                {
                    pnrApilog1.IsSFDCOPPTYRECORDTYPE = true;

                }
                if (pnrApilog1.SFDCDISTIACCOUNTID == pnrApilog2.SFDCDISTIACCOUNTID)
                {
                    pnrApilog1.IsSFDCDISTIACCOUNTID = true;

                }
                if (pnrApilog1.SFDCTYPE == pnrApilog2.SFDCTYPE)
                {
                    pnrApilog1.IsSFDCTYPE = true;
                }
            }
            catch(Exception ex)
            {
              ShowStopperException exception=  new ShowStopperException("Unable to compare sfdc details, Please Check CompareSFDCDetails() in PnrApiLog ", ex);
                exception.LogException();
            }

        }
        #endregion

    }
}
