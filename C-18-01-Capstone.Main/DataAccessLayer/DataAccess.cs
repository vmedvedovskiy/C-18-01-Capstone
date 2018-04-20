using C_18_01_Capstone.Main.DataContext;
using System.Collections.Generic;
using System.Linq;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
    public class DataAccess<T> : IDataAccess<T> where T : class
    {
        private DataAccessLogger logger = new DataAccessLogger();

        delegate void SomeDelegate(T entity);
        
        public void AddEntity(T entity)
        {
            string logString = string.Format($"{nameof(this.AddEntity)}({typeof(T).Name})");
            logger.LogStartOperation(logString);

            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }

            logger.LogEndOperation(logString);
        }
        
        public void AddRange(IList<T> entities)
        {
            string logString = string.Format($"{nameof(this.AddRange)}({typeof(T).Name})");
            logger.LogStartOperation(logString);

            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
            }

            logger.LogEndOperation(logString);
        }

        public void DeleteEntity(T entity)
        {
            string logString = string.Format($"{nameof(this.DeleteEntity)}({typeof(T).Name})");
            logger.LogStartOperation(logString);

            using (var context = new SocialNetworkContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }

            logger.LogEndOperation(logString);
        }

        public IList<T> GetEntities()
        {
            string logString = string.Format($"{nameof(this.GetEntities)}({typeof(T).Name})");
            logger.LogStartOperation(logString);

            using (var context = new SocialNetworkContext())
            {
                var result = context.Set<T>().Select(_ => _).ToList();

                logger.LogEndOperation(logString);
                return result;
            }
        }
    }
}
