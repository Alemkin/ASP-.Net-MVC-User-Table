using System;
using System.Web;
using System.IO;

namespace FirstDemoProjectForRPbyAL.Services.Logging
{
    /**
     * Utility class for Logging
     */
    public sealed class LogFactory
    {
        /* Method to Log Exceptions */
        public void LogException(Exception e, string source)
        {
            // Get the absolute path to the log file
            var logFile = "~/App_Data/ErrorLog.txt";

            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for appending and write the log
            var sw = new StreamWriter(logFile, true);
            sw.WriteLine("============== {0} ==============", DateTime.Now);
            if (e.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(e.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(e.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(e.InnerException.Source);
                if (e.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(e.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(e.GetType().ToString());
            sw.WriteLine("Exception: " + e.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (e.StackTrace != null)
            {
                sw.WriteLine(e.StackTrace);
                sw.WriteLine();
            }
            sw.WriteLine("=======================================");
            sw.Flush();
            sw.Close();
        }
    }
}
