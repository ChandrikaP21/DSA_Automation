using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LATAM_SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer.TestData
{
    class TranslateContentLanguage
    {
        public static List<Translate> SealizeObejectToXMLString()
        {
            Content contentObj = new Content();

            var xmlSerializer = new XmlSerializer(typeof(List<Translate>), new XmlRootAttribute("Content"));
            var reader = new StreamReader(ConfigurationManager.AppSettings["LanguageTranslateXml"].ToString());
            contentObj.translate = (List<Translate>)xmlSerializer.Deserialize(reader);
            return contentObj.translate;
        }
        //public Dictionary<string, Dictionary<string, string>> SealizeObejectToXMLString()
        //{
        //    Content contentObj = new Content();

        //    var xmlSerializer = new XmlSerializer(typeof(Dictionary<string, Dictionary<string, string>>), new XmlRootAttribute("Content"));
        //    var reader = new StreamReader(ConfigurationManager.AppSettings["LanguageTranslateXml"].ToString());
        //    contentObj.translate = (Dictionary<string, Dictionary<string, string>>)xmlSerializer.Deserialize(reader);
        //    return contentObj.translate;
        //}

    }
    [XmlRoot(ElementName = "Content")]
    public class Content
    {
        [XmlElement(ElementName = "Translate")]
     //  public Dictionary<string,Dictionary<string,string>> translate { get; set; }

        public List<Translate> translate { get; set; }

    }
    
    public class Translate
    {
        [XmlElement(ElementName = "Word")]
        public List<Word> Words { get; set; }
         
        [XmlAttribute("To")]
        public string To { get; set; }

    }
    public class Word
    {
        [XmlElement(ElementName = "Texttotranslate")]
        public string txttotranslate { get; set; }
        [XmlElement(ElementName = "Translatedtext")]
        public string translatedtext { get; set; }
    }
    


}


