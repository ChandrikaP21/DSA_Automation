using LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer
{
    public enum QuantityChangeType
    {
        ForSpecificProduct = 0,
        AnyProduct =1,
        AllProducts = 2
    }
   public class TestCase
    {
        public string TestCaseID { get; set; }
       
        public string TestCaseTitle { get; set; }
        public string BaseQuoteQuery { get; set; }
        public string BaseQuoteNumber { get; set; }
        public string BaseProductionIdentificationQuery { get; set; }
        public string BaseProductOrderCodes { get; set; }
        public string Components { get; set; }
        public string Accessories { get; set; }
        public string IsBaseQuoteIdentificationQueryBased { get; set; }
        public string BaseQuoteIdentificationQUERY { get; set; }
        public string BaseQuoteIdentificationDATA { get; set; }
        public string IsIdentifiedByQuery { get; set; }
        public string ProductIdentificationQuery { get; set; }
        public string ProductIdentificationData { get; set; }
        public string ComponentUpgrade { get; set; }
        public int Quantity { get; set; }
        public  string OrderCode { get; set; }
        public string UpdateAccessories { get; set; }
        public string UpdateServices { get; set; }
        public  string ModuleId { get; set; }
        public string AccessoriesId { get; set; }
        public string ServiceId { get; set; }
        public string OfferingId { get; set; }
        public string ComponentId { get; set; }
       
        public string CustomerIdentificationQuery { get; set; }
        public string CustomerIdentificationData { get; set; }

        public string IsCustomerIdentificationQueryBased { get; set; }
        public string NewCustomerIdentificationQuery { get; set; }
        public string NewCustomerIdentificationData { get; set; }
        public string ApprovalType { get; set; }
        public string IsNewCustomerIdentificationQueryBased { get; set; }
        public string IsCustomerInSameSegment { get; set; }
        public string CrossSegmenId { get; set; }
        // additional fields to identify the end customere and create quote.
        public string AccountCustomerId { get; set; }
        public string EndCustomerId { get; set; }
        public string SFDCID { get; set; }
        

        public string IsSFDCIdentificationDataAutomatic { get; set; }
        public string SFDCIdentificationQuery { get; set; }
        public string SFDCIdentificationData { get; set; }

        public QuantityChangeType QuantityChangeType { get; set; }
        public List<TestData_Module> modules = new List<TestData_Module>();
        public List<AddProduct> addProduct = new List<AddProduct>();
        public Content content { get; set; }
        public string IsPriceChangeAutomatic { get; set; }
        public string IsProductSelectionAuto { get; set; }
        public string PriceChangeType { get; set; }
        public string SellingPrice { get; set; }
        public string Discount { get; set; }
        public string StandardPartnerDiscountType { get; set; }

        //public string IsIdentifiedByQuery { get; set; }
        //public string ProductIdentificationQuery { get; set; }
        //public string ProductIdentificationData { get; set; }
        public string SystemSearch { get; set; }
        public string SAndPSearch { get; set; }
        public string Approver { get; internal set; }
        public string AffinityAccountGroupID { get; internal set; }
        public string ProductType { get; internal set; }



    }
    public class TestData_Module
    {
        public string ModuleId { get; set; }
        //below property will hold accessory Id or component Id or Service Id based on the test.
        public string OfferingId { get; set; }
    }
    public class AddProduct  //rename the class name
    {
        public string IsIdentifiedByQuery { get; set; }
        public string ProductIdentificationQuery { get; set; }
        public string ProductIdentificationData { get; set; }
        public string SystemSearch { get; set; }
        public string SAndPSearch { get; set; }


    }
}
