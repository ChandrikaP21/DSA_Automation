using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Configuration;

namespace SmartPrice_E2E_WebAutomation.Bedrock.Utilities
{
    /*
     Run Status 	In-Progress
Run Progress	
	Data connection established
	Goal Goal Lite data fetched
	Deal Score Generation In-Progress
	Deal Score Fetch Completed
	Report Generation In-Progress
	Report Generation Completed
Errors :	

     
     */
    class Console_PresentationLayer
    {
        private static string DealScoreRunHeaderTop = "***********************************************";
        private static string DealScoreRunHeaderContent = " GPT Automation Suite";
        private static string DealScoreRunHeaderBottom = "***********************************************";

        private static string RunStatus = string.Empty;
        private static List<string> RunProgress = new List<string>();
        private static string RunProgress_Separater = "-----------------------------------------------";
        private static List<string> Details = new List<string>();
        private static string firsitemin_Details = string.Empty;
        private static string Details_Separater = "-----------------------------------------------";
        private static List<string> RunErrors = new List<string>();
        private static string Errors_Separater = "-----------------------------------------------";




        public static void ReportCurrentRunStatus()
        {
            Console.Clear();
            
            Console.WriteLine(DealScoreRunHeaderTop);
            Console.WriteLine(DealScoreRunHeaderContent); 
            Console.WriteLine(DealScoreRunHeaderBottom);

            Console.WriteLine(String.Format("Run Status : {0}", RunStatus));
            
            Console.WriteLine(String.Format("Run Progress : "));
            foreach (string item in RunProgress)
            {
                Console.WriteLine(String.Format("---> {0}",item));
            }
            Console.WriteLine(RunProgress_Separater);

            Console.WriteLine(String.Format("Details : "));
            foreach (string item in Details)
            {
                Console.WriteLine(String.Format("---> {0}", item));
            }
            Console.WriteLine(Details_Separater);

            Console.WriteLine(String.Format("Errors : "));
            foreach (string item in RunErrors)
            {
                Console.WriteLine(String.Format("---> {0}", item));
            }
            Console.WriteLine(Details_Separater);

        }

        public static void Report_RunStatus(string status, bool reportCurrentRunStatus)
        {
            RunStatus = status;
            if (reportCurrentRunStatus) ReportCurrentRunStatus();
        }
        public static void Report_Run_Progress(string status, bool reportCurrentRunStatus)
        {
            if (RunProgress.Count > 9) RunProgress.RemoveAt(0);
            RunProgress.Add(status);
            if (reportCurrentRunStatus) ReportCurrentRunStatus();
            SimpleLogger.LogMessage("PROGRESS : " + status);

        }

        public static void Report_Run_Details(string status, bool reportCurrentRunStatus)
        {
            if (Details.Count >20) Details.RemoveAt(0);
            Details.Add(status);
            if (reportCurrentRunStatus) ReportCurrentRunStatus();
            SimpleLogger.LogMessage("DETAILS : " + status);

        }

        public static void Report_Run_Details_Clear()
        {
            Details.Clear();
        }

        public static void Report_Run_Errors(string status, bool reportCurrentRunStatus)
        {
            if (RunErrors.Count > 9) RunErrors.RemoveAt(0);
            RunErrors.Add(status);
            if (reportCurrentRunStatus) ReportCurrentRunStatus();
            SimpleLogger.LogMessage("ERROR : " + status);
        }

        
    }
}