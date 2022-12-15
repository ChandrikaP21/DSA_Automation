using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;

namespace SmartPrice_E2E_WebAutomation.Objects
{
    class AppliedPoliciesJSONDeserializer:DriverHelper
    {
        public static List<AppliedPolicy> GetObject(string jsonPayload)
        {
          
            
            FormatJSONPayload(ref jsonPayload);
           
            List<AppliedPolicy> appliedPolicies =  (List<AppliedPolicy>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonPayload, typeof(List<AppliedPolicy>));
           // List<AppliedPolicy> appliedPolicies = (List<AppliedPolicy>)WebDriver(jsonPayload, typeof(List<AppliedPolicy>));
            return appliedPolicies;
        }

        private static void FormatJSONPayload(ref string jsonPayload)
        {
            jsonPayload = jsonPayload.Replace(@"\", "");
            jsonPayload = jsonPayload.Replace("\"1\":", string.Format("\"{0}\":", "PolicyName"));
          jsonPayload = jsonPayload.Replace("\"2\":", string.Format("\"{0}\":", "PolicyDescription"));
     
            jsonPayload = jsonPayload.Replace("\"3\":", string.Format("\"{0}\":", "WorkBookID"));
            jsonPayload = jsonPayload.Replace("\"4\":", string.Format("\"{0}\":", "PolicyID"));
            jsonPayload = jsonPayload.Replace("\"5\":", string.Format("\"{0}\":", "Value1"));
            jsonPayload = jsonPayload.Replace("\"6\":", string.Format("\"{0}\":", "Value2"));
            jsonPayload = jsonPayload.Replace("\"7\":", string.Format("\"{0}\":", "Value3"));
            jsonPayload = jsonPayload.Replace("\"8\":", string.Format("\"{0}\":", "Value4"));
            
        }

    }
}
