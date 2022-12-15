using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Configuration;
using System.IO;
namespace SmartPrice_E2E_WebAutomation.Bedrock.Utilities
{
    class SimpleLogger
    {
        public static void LogMessage(string message)
        {
            string logFormattedText = string.Format("{0} : {1}", DateTime.Now.ToString(), message);
            string filepath = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(logFormattedText);
            try
            {

                if (ConfigurationManager.AppSettings["EnableApplicationLogs"].ToUpper() == "Y")
                {
                    filepath = ConfigurationManager.AppSettings["ApplicaitonLogLocation"];
                    File.AppendAllText(filepath, sb.ToString());
                }
            }
            catch (Exception)
            {
                /* not a show stopper, move on without logging */
            }
            
        }
    }
}
