using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SmartPrice_E2E_WebAutomation.Helper
{
    public class CustomConfigurationProvider
    {
        private static IConfiguration Configuration { get; set; }
        public static string osdelimiter = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/" : "\\";
        static CustomConfigurationProvider()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("TestConfiguration.json");
            Configuration = builder.Build();
        }

        public static T GetSection<T>(string key) where T : class
        {
            if (Configuration != null)

            {
                T objInstance = Activator.CreateInstance<T>();
                Configuration.GetSection(key).Bind(objInstance);
                T actualObject = objInstance; return actualObject;
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
            if (Configuration != null)
            {
                value = Configuration[key];
                if (value == null)
                {
                    throw new Exception($"Could not find the key {key} in TestConfiguration.json file");
                }
            }
            return value;
        }
    }
}
