
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EMEA_SmartPrice_E2E_WebAutomation.Helper
{
    public class CustomConfigurationProvider
    {
        private static IConfiguration Configuration { get; set; } 
        public static string osdelimiter = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/" : "\\";
        static CustomConfigurationProvider()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory().Replace("\\",@"\")).AddJsonFile("TestConfiguration.json");
                Configuration = builder.Build();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static T GetSection<T>(string key) where T : class
        {
            try
            {
                if (Configuration != null)
                {
                    T objInstance = Activator.CreateInstance<T>();
                    Configuration.GetSection(key).Bind(objInstance);
                    T actualObject = objInstance; return actualObject;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static string GetSection(string key)
        {
            var obj = JObject.Parse(File.ReadAllText($@"{Directory.GetCurrentDirectory()}{osdelimiter}TestConfiguration.json"));
            var val = obj.SelectToken(key).ToString(Formatting.None);
            return val;
        }
        //parentKey:childKey
        public static string GetKey(string key)
        {
            string value = null;
            try
            {
                
                if (Configuration != null)
                {
                    value = Configuration[key];
                    if (value == null)
                    {
                        throw new Exception($"Could not find the key {key} in TestConfiguration.json file");
                    }
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return value;
        }
    }
}
