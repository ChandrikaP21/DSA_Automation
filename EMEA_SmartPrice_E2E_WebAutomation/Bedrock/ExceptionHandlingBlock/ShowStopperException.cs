using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMEA_SmartPrice_E2E_WebAutomation.Reports;
using EMEA_SmartPrice_E2E_WebAutomation.Bedrock.Utilities;
namespace EMEA_SmartPrice_E2E_WebAutomation.Bedrock.ExceptionHandlingBlock
{
    class ShowStopperException : System.Exception
    {

        public ShowStopperException(string message)
        : base(message)
        {
        }

        public ShowStopperException(string message, Exception inner)
            : base(message, inner)
        {
        }

        

        public bool LogException()
        {
            /*
             will consume the logger log and exception in the applicaiton log

             */
            bool result = false;
           
            try
            {
                if (!string.IsNullOrEmpty(this.Message))
                    
                    Bedrock.Utilities.SimpleLogger.LogMessage(this.Message);
                if (!string.IsNullOrEmpty(this.InnerException.Message))
                    Bedrock.Utilities.SimpleLogger.LogMessage("Inner Exception Message" + this.InnerException.Message);
                if (!string.IsNullOrEmpty(this.InnerException.StackTrace))
                    Bedrock.Utilities.SimpleLogger.LogMessage("Inner Exception Stack Trace" + this.InnerException.StackTrace);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
               
                
            }
            return result;
        }
    }
}
