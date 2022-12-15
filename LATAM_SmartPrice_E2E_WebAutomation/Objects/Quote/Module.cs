using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice_E2E_WebAutomation.Objects.Quote
{
        
    public class Module
    {
        public string ID = string.Empty;
        public string Title = string.Empty;
    }

    public class ServicesModule : Module
    {
        public List<Service> Services = new List<Service>();
        
    }

    public class ComponentModule : Module
    {
        public List<Component> Components = new List<Component>();
    }

    /*
     Example : [5113] Monitors (Non-Tied)
     */
    public class AccessoriesModule : Module
    {
        /*
         * Example : Dell 22 Monitor - P2219H
         * 
        */
        public List<Accessory> Accessories = new List<Accessory>();
    }
}

