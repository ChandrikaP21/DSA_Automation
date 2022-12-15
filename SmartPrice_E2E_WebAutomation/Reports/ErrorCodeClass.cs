using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Reports
{
  public class ErrorCodeClass:ArgumentException
    {
        public int ErrorCode { get; }
        public ErrorCodeClass(string paramName, int errorCode)
            : base(paramName)
        {
            ErrorCode = errorCode;
        }
    }
}
