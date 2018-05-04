using System;
using System.IO;
using System.Text;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    internal class DataAccessLogger 
    {
        private string path = "DataServiceLog.txt";
        
        internal void LogStartOperation(string proccessName)
        {
            string log = LogString("Starting", proccessName);

            File.AppendAllText(path,
                              log,
                              Encoding.UTF8);
        }

        internal void LogEndOperation(string proccessName)
        {
            string log = LogString("Finishing", proccessName);

            File.AppendAllText(path,
                              log,
                              Encoding.UTF8);
        }

        internal string LogString(string state, string proccessName)
        {
            return string.Format(
                $"{state} proccess {proccessName} {DateTime.Now.ToString()}{Environment.NewLine}");
        }
    }
}

