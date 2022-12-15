using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Objects
{
 public class CustomerClass
    {
        public string OldAccountCustomerId { get; set; }
        public string OldAccountId { get; set; }
        public string OldQuoteAccountId { get; set; }
        public string OldEndUserAccountCustomerId { get; set; }
        public string OldEndUserAccountId { get; set; }
        public string NewAccountCustomerId { get; set; }
        public string NewAccountId { get; set; }
        public string NewQuoteAccountId { get; set; }
        public string NewEndUserAccountCustomerId { get; set; }
        public string NewEndUserAccountId { get; set; }

        
        public string CustomerName { get; set; }
        public string CrossSegmenId { get; set; }
        public string IsAccountCustomerIdMatched { get; set; }
        public string IsAccountIdMatched { get; set; }
        public string IsQuoteAccountIdMatched { get; set; }
        public string IsEndUserAccountCustomerIdMatched { get; set; }
        public string IsEndUserAccountIdMatched { get; set; }
    }
}
