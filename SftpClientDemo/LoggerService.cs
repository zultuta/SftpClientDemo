using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SftpClientDemo
{
    public class LoggerService
    {
        private static object mutex;
        string directory = ConfigurationManager.AppSettings["logPath"];
        public LoggerService()
        {
            mutex = new object();
        }

        public void LogError(Exception exception)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string filepath = directory + @"\" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".txt";
                lock (mutex)
                {
                    File.AppendAllText(filepath, "Event Time: " + DateTime.Now.ToString() + " | Exception: " + exception + Environment.NewLine);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to write to log file: {ex.Message}\n");
            }
        }

        public void LogInfo(string message)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string filepath = directory + @"\" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".txt";
                lock (mutex)
                {
                    File.AppendAllText(filepath, "Event Time: " + DateTime.Now.ToString() + " | Message: " + message + Environment.NewLine);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to write to log file: {ex.Message}\n");
            }
        }
    }
}
