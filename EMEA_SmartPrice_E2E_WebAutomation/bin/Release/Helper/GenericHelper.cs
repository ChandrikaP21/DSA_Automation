using EMEA_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMEA_SmartPrice_E2E_WebAutomation.Helper
{
    public static class GenericHelper
    {
        private static string ReplaceCountryCode(ref string  currencyText, string cultureInfo = "en-US")
        {   
            return currencyText.ToLower().Replace("us", "").Replace("rm","").Replace("kr","").Replace(" ", ""); 
        }
        private static string RemoveNonNumericCharacter(ref string text)
        {
            char[] charArray = text.ToCharArray();
            foreach ( var arr in charArray)
            {
                if(!Char.IsNumber(arr))
                {
                    text = text.Remove(0).Trim();
                }
            }
            return text;
        }

            private static string RemoveFirstCharacter(ref string text)
        {
            char [] ch= text.ToCharArray();
            if(!Char.IsNumber(ch[0]))
            {
                text = text.Remove(0, 1).Replace(",","");
                
            }
       
            return text;
        }
        

        public static string ConvertCurrencyIntoString(string currency, string country="us")
        {
            
                string cultureInfo = "en-us";
                switch (country)
                {
                    case "UNITED KINGDOM":
                        cultureInfo = "en-gb";
                        break;
                    case "FINLAND":
                        cultureInfo = "fi-FI";
                        break;
                    case "MALAYSIA":
                        cultureInfo = "en-MY";
                        break;
                case "AUSTRALIA":
                    cultureInfo = "en-AU";
                    break;
                case "SWEDEN":
                    cultureInfo = "sv-SE";
                    break;
                case "FRANCE":
                    cultureInfo = "fr-FR";
                    break;
            }
                    try
                    {
                      currency= ReplaceCountryCode(ref currency, cultureInfo).ToUpper();

                    
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            
       //     currency = ReplaceCountryCode(ref currency, cultureInfo);
            
                try
                {
                    RemoveNonNumericCharacter(ref currency);


                    return Decimal.Parse(currency, NumberStyles.Currency, CultureInfo.GetCultureInfo(cultureInfo)).ToString();
                }
                catch (Exception e)
                {
                Bedrock.Utilities.SimpleLogger.LogMessage("Currency is not is proper format,currency :"+currency+",Error: "+e.Message);
                Bedrock.Utilities.SimpleLogger.LogMessage("Currency is not is proper format,currency :" + currency + ",Error: " + e.StackTrace);
                return currency;
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

