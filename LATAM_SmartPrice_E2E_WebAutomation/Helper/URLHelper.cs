using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Helper
{
    public static class URLHelper
    {
        private static string env = CustomConfigurationProvider.GetKey("Environment");

        public static string GetDSAUrl()
        {

            string url;
            switch (env.ToUpper())
            {
                case "GE1":
                    url = CustomConfigurationProvider.GetKey("DSA:GE1");
                    break;
                case "GE2":
                    url = CustomConfigurationProvider.GetKey("DSA:GE2");
                    break;
                case "GE3":
                    url = CustomConfigurationProvider.GetKey("DSA:GE3");
                    break;
                case "GE4":
                    url = CustomConfigurationProvider.GetKey("DSA:GE4");
                    break;
                case "PROD":
                default:
                    url = CustomConfigurationProvider.GetKey("DSA:PROD");
                    break;
            }
            return url;
        }
        public static string GetGoalUrl()
        {

            string url;
            switch (env.ToUpper())
            {
                case "GE1":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE1");
                    break;
                case "GE2":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE2");
                    break;
                case "GE3":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE3");
                    break;
                case "GE4":
                    url = CustomConfigurationProvider.GetKey("GOAL:GE4");
                    break;
                case "PROD":
                default:
                    url = CustomConfigurationProvider.GetKey("GOAL:PROD");
                    break;
            }
            return url;
        }
    }
}
