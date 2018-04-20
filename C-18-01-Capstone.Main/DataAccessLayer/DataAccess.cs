using C_18_01_Capstone.Main.DataContext;
using System.Collections.Generic;
using System.Linq;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    public class DataAccess<T> : IDataAccess<T> where T : class
    {
        public void AddEntity(T entity)
        {
            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                AddEntity(item);
            }
        }

        public void DeleteEntity(T entity)
        {
            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public IList<T> GetEntities()
        {
            using (var context = new SocialNetworkContext())
            {
                var result = context.Set<T>().Select(_ => _).ToList();
                
                return result;
            }
        }
    }
}
