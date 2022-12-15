using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SmartPrice_E2E_WebAutomation.DataFactory.DBConnection;
using SmartPrice_E2E_WebAutomation.Objects;
using SmartPrice_E2E_WebAutomation.Reports;
using SmartPrice_E2E_WebAutomation.DataFactory.DataAccessLayer;
using SmartPrice_E2E_WebAutomation.Objects.Quote;
using System.Threading;

namespace SmartPrice_E2E_WebAutomation.Utilities
{
    public  class CustomGarbageCollector
    {
        

        internal void NullifyUnusedObject(IWebDriver webDriver=null, OracleDBConnection odb=null, DSAPageObject ds=null, TotalFinancialCalculation totalFinancialCalculation=null, Constant cs=null, ResultLogging resultLog=null, TestCase test=null,Product productObj=null,List<DSAPageObject> list=null)
        {
            try
            {

                webDriver.Close();
                odb = null;
                ds = null;
                totalFinancialCalculation = null;
                cs = null;
                resultLog = null;
                test = null;
                list = null;
                productObj = null;
                Thread.Sleep(7000);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
