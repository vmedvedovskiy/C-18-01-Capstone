﻿using System.Collections.Generic;
using System.Linq;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    public interface IDataAccess<T> where T : class
    {
        void AddEntity(T entity);
        void DeleteEntity(T entity);
        void AddRange(IList<T> entities);

        IQueryable<T> GetEntities();
        
    }
}
