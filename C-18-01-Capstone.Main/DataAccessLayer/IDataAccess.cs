using System.Collections.Generic;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    public interface IDataAccess<T> where T : class
    {
        void AddEntity(T entity);
        void DeleteEntity(T entity);
        void AddRange(IEnumerable<T> entities);
    }
}
