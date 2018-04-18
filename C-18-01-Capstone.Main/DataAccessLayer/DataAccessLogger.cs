using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
   public class DataAccessLogger<T> : IDataAccess<T> where T : class, new()
   {
      private IDataAccess<T> entity;
      private string path = "DataServiceLog.txt";


      public DataAccessLogger(IDataAccess<T> entity)
      {
         this.entity = entity;
      }

      public void AddEntity(T entity)
      {
         string logString = string.Format("{0}({1})", nameof(this.AddEntity), typeof(T).Name);
         LogDataStart(logString);
         this.entity.AddEntity(entity);
         LogDataEnd(logString);
      }

      public void DeleteEntity(T entity)
      {
         string logString = string.Format("{0}({1})", nameof(this.DeleteEntity), typeof(T).Name);
         LogDataStart(logString);
         this.entity.DeleteEntity(entity);
         LogDataEnd(logString);
      }

      private void LogDataStart(string proccessName)
      {
         string log = LogString("Starting", proccessName);

         File.AppendAllText(path,
                           log,
                           Encoding.UTF8);

      }

      private void LogDataEnd(string proccessName)
      {
         string log = LogString("Finishing", proccessName);

         File.AppendAllText(path,
                           log,
                           Encoding.UTF8);
      }

      private string LogString(string state, string proccessName)
      {
         return string.Format("{0} proccess {1} {2} {3}",
                        state,
                        proccessName,
                        DateTime.Now.ToString(),
                        Environment.NewLine);
      }
   }
}

