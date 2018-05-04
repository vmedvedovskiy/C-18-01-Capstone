using C_18_01_Capstone.Main.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    public class EfDataAccess<T> : IDataAccess<T> where T : class
    {
        public void AddEntity(T entity)
        {
            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void AddRange(IList<T> entities)
        {
            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
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

        public IQueryable<User> GetUserByLogin(string login)
            => new SocialNetworkContext()
                .Set<User>()
                .Include(_ => (_ as User).Country)
                .Where(user => user.Login == login);



        public IQueryable<T> GetEntities()
            => new SocialNetworkContext()
                .Set<T>();
        
    }
}
