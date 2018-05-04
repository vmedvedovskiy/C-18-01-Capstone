using System;
using System.IO;
using System.Text;

namespace C_18_01_Capstone.Web.Services
{
    public static class DataLogger
    {
        private static string path = String.Format(Path.GetTempPath() + "DataLog.txt" );

        internal static void LogOperation(string proccessName)
        {
            string log = LogString(proccessName);

            File.AppendAllText(path, log, Encoding.UTF8);
        }

        private static string LogString(string proccessName)
        {
            return string.Format(
                $"{proccessName} {DateTime.Now.ToString()}{Environment.NewLine}");
        }
    }
}