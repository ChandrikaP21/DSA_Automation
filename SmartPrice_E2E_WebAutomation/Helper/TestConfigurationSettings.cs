namespace SmartPrice_E2E_WebAutomation.Helper
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class DSA
    {
        public string GE1 { get; set; }
        public string GE2 { get; set; }
        public string GE3 { get; set; }
        public string GE4 { get; set; }
        public string PROD { get; set; }
    }

    public class GOAL
    {
        public string GE1 { get; set; }
        public string GE2 { get; set; }
        public string GE3 { get; set; }
        public string GE4 { get; set; }
        public string PROD { get; set; }
    }

    public class TestConfigurationSettings
    {
        public DSA DSA { get; set; }
        public GOAL GOAL { get; set; }
        public string Environment { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string WebDriver { get; set; }
        public string WebDriverType { get; set; }
        public string SeleniumGridServer { get; set; }
    }

}