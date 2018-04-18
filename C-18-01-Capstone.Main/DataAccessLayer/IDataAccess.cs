using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
   public interface IDataAccess<T> where T : class, new()
   {
      void AddEntity(T entity);
      void DeleteEntity(T entity);
   }
}
