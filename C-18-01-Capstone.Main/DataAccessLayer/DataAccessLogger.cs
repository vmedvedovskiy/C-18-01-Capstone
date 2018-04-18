using System;
using System.IO;
using System.Text;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
   public class DataAccessLogger<T> : IDataAccess<T> where T : class
   {
      private IDataAccess<T> entity;
      private string path = "DataServiceLog.txt";

      public DataAccessLogger(IDataAccess<T> entity)
      {
         this.entity = entity;
      }

      public void AddEntity(T entity)
      {
         string logString = string.Format($"{nameof(this.AddEntity)}({typeof(T).Name})");
         LogStartOperation(logString);
         this.entity.AddEntity(entity);
         LogEndOperation(logString);
      }

      public void DeleteEntity(T entity)
      {
         string logString = string.Format($"{nameof(this.DeleteEntity)}({typeof(T).Name})");
         LogStartOperation(logString);
         this.entity.DeleteEntity(entity);
         LogEndOperation(logString);
      }

      private void LogStartOperation(string proccessName)
      {
         string log = LogString("Starting", proccessName);

         File.AppendAllText(path,
                           log,
                           Encoding.UTF8);

      }

      private void LogEndOperation(string proccessName)
      {
         string log = LogString("Finishing", proccessName);

         File.AppendAllText(path,
                           log,
                           Encoding.UTF8);
      }

      private string LogString(string state, string proccessName)
      {
         return string.Format("{0} proccess {1} {2}{3}",
                        state,
                        proccessName,
                        DateTime.Now.ToString(),
                        Environment.NewLine);
      }
   }
}

