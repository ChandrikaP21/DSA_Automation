using EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA;
using System;
using System.Collections.Generic;

namespace EMEA_SmartPrice_E2E_WebAutomation.Pages.GOAL
{
    public class GoalObject
    {
        public string QuoteNumber { get; internal set; }
        public string CustomerID { get; internal set; }
        public string ListPrice { get; internal set; }
        public string TotalNSP { get; internal set; }
        public string TotalNSPinUSD { get; internal set; }
        public string TotalDOLinPercentage { get; internal set; }
        public string CombinedDAMQuoteLevel { get; internal set; }
        public string ExceedingAmountQuoteLevel { get; internal set; }
        public string QuoteMarginInCurrency { get; internal set; }
        public string QuoteMarginInPercentage { get; internal set; }
        public List<ProductDetails> ProductDetails { get; set; }

        public string ApprovalType { get; internal set; }
        public string ApprovalStatus { get; internal set; }
        public string Approver { get; internal set; }
        public string AffinityAccountGroupID { get; internal set; }
        public string ProductType { get; internal set; }
    }
}