using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace SmartPrice_E2E_WebAutomation.Reports
{
    class ErrorLoging
    {
        public bool WriteErrorLog(string LogMessage)
        {
            bool status = false;
            string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"].ToString();
            DateTime CurrentDateTime = DateTime.Now;
            string CurrentDateTimeString = CurrentDateTime.ToString();
            CheckCreateLogDirectory(LogDirectory);
            string LogLine = BuildLogLine(CurrentDateTimeString, LogMessage);
            LogDirectory = (LogDirectory + "Log_" + LogFile(DateTime.Now) + ".txt");
            lock (typeof(ErrorLoging))
            {
                StreamWriter streamWriter = null;
                try
                {
                    streamWriter = new StreamWriter(LogDirectory, true);
                    Console.WriteLine(LogLine);
                    status = true;
                }
                catch
                {

                }
                finally
                {
                    if (streamWriter != null)
                        streamWriter.Close();
                }
            }
            return status;
        }

        private string BuildLogLine(string currentDateTimeString, string logMessage)
        {
            throw new NotImplementedException();
        }

        private string LogFile(DateTime now)
        {
            throw new NotImplementedException();
        }

        private void CheckCreateLogDirectory(string LogPath)
        {
            bool loggingDirectoryExists = false;
            DirectoryInfo directoryInfo = new DirectoryInfo(LogPath);
        }
    }
}
