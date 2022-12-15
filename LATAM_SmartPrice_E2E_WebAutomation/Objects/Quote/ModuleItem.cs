using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Objects
{
    public class ModuleItem
    {
        public string Title { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SellingPrice { get; set; }
        

    }

    public class Service : ModuleItem
    {
        public bool IsUpsell { get; set; }
        public string ServiceTypeId { get; set; }
        public decimal UpsellSellingPrice { get; set; }

    }

    /*
     Example : Dell 22 Monitor - P2219H
    */
    public class Accessory : ModuleItem
    {
        /*
        Dell 22 Monitor - P2219H:Dell 22 Monitor - P2219H
        Hardware Support Services:3 Years Advanced Exchange Service
        */
        public List<ModuleItem> AccessoryItems = new List<ModuleItem>();
        

        
    }

    /*Component = Configure\System in DSA*/
    public class Component : ModuleItem
    {
        public bool IsUpsell { get; set; }
        public decimal UpsellSellingPrice { get; set; }
    }
}
