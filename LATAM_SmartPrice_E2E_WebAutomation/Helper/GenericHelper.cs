using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Helper
{
    public static class GenericHelper
    {
        private static string ReplaceCountryCode(string currencyText, string cultureInfo = "en-US")
        {
            return currencyText.ToLower().Replace("us", ""); 
        }
        public static string ConvertCurrencyIntoString(string currency, string cultureInfo = "en-US")
        {
            string countryCurrency = ReplaceCountryCode(currency);
            decimal value = Decimal.Parse(countryCurrency, NumberStyles.Currency, CultureInfo.GetCultureInfo(cultureInfo));
            try
            {
                return Decimal.Parse(countryCurrency, NumberStyles.Currency, CultureInfo.GetCultureInfo(cultureInfo)).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
           
        }
        public static int SetLimit(string value, bool upper = true, int limit = 10)
        {
            int modifiedValue = 0;
            if (Int32.TryParse(value.Split('.')[0], out int j))
            {
                Console.WriteLine(j);
                modifiedValue = upper ? j + limit : j - limit;
            }
            else
            {
                Console.WriteLine("String could not be parsed in SetLowerLimit method");
            }
            return modifiedValue;
        }
    }
}

