using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Objects.SmartPrice
{
  public class GuidanceMessage
    {
        public string MessageType = "";
        public string MessageTitle = "";
        public string MessageSequence = "";
        private string PromoMessageType = "";
        //made it private due to parse error , later it will be modified to public..
        private string PromoMessage = "";
    }
}
