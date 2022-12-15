using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer
{
   public class Scenario
    {
        
        public string ScenarioID { get; set; }
        public string ScenarioName { get; set; }
        public List<TestCase> TestCases { get; set; }
     //   public string Release { get; set; }



    }
}
