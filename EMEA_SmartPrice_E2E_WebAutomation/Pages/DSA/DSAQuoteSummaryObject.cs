using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Pages.DSA
{
    // DSAQuoteSummaryObject myDeserializedClass = JsonConvert.DeserializeObject<DSAQuoteSummaryObject>(myJsonResponse); 
    public class ProductDetails
    {
        public string ProductDescription { get; set; }
        public string Quantity { get; set; }
        public string UnitListPrice { get; set; }
        public string UnitPrice { get; set; }
        //public string ListPrice { get; set; }
        //public string UnitCost { get; set; }
        //public string CostPrice { get; set; }
        //public string UnitSellingPrice { get; set; }
        public string SellingPrice { get; set; }
        public string DiscountInPercantage { get; set; }
        public string DiscountInCurrency { get; set; }
        public string PriceModifier { get; set; }
    }

    public class DSAQuoteSummaryObject
    {
        public string QuoteNumber { get; set; }
        //public string CustomerID { get; set; }
        public string QuoteListPrice { get; set; }
        public string QuoteSellingPrice { get; set; }
        public string QuoteCostPrice { get; set; }
        public string QuoteDiscountInPercentage { get; set; }
        public string QuoteDiscountInCurrency { get; set; }
        public string QuoteTotalMargin { get; set; }
        public string QuoteTotalMarginPercentage { get; set; }
        public string DsaQuantity { get; set; }
        //public string ExceedingAmountQuoteLevel { get; set; }
        public List<ProductDetails> ProductDetails { get; set; }
    }


}
