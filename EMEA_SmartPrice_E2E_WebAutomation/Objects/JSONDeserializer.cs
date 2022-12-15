using EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects
{
    class JSONDeserializer
    {
        public static List<GuidanceMessage> GetObject(string jsonPayload)
        {
            List<GuidanceMessage> guidanceMessages = null;
            try
            {
            //    string str = "\"MessageType\":\"TYPED Message\"";

                FormatJSONPayload(ref jsonPayload);
          //      Regex.Replace(jsonPayload, @"[^0-9a-zA-Z]+%", "");

                guidanceMessages = (List<GuidanceMessage>)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonPayload, typeof(List<GuidanceMessage>));
                // List<AppliedPolicy> appliedPolicies = (List<AppliedPolicy>)WebDriver(jsonPayload, typeof(List<AppliedPolicy>));
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return guidanceMessages;
        }

        private static void FormatJSONPayload(ref string jsonPayload)
        {
            
            jsonPayload = jsonPayload.Replace(@"\", " ");
            jsonPayload = jsonPayload.Replace("\"1\":", string.Format("\"{0}\":", "MessageType"));
            jsonPayload = jsonPayload.Replace("\"2\":", string.Format("\"{0}\":", "MessageTitle"));

            jsonPayload = jsonPayload.Replace("\"3\":", string.Format("\"{0}\":", "MessageSequence"));
            
            jsonPayload = jsonPayload.Replace("\"4\":", string.Format("\"{0}\":", "PromoMessageType"));
            jsonPayload = jsonPayload.Replace("\"5\":", string.Format("\"{0}\":", "PromoMessage"))/*.Replace("+","Increase").Replace("%","percent")*/;
            

        }
    }
}
