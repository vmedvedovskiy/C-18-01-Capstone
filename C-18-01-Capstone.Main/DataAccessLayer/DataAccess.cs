using C_18_01_Capstone.Main.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_18_01_Capstone.Main.DataAccessLayer
{
   public class DataAccess<T> : IDataAccess<T> where T : class, new()
   {
      public void AddEntity(T entity)
      {
         using (SocialNetworkContext context = new SocialNetworkContext())
         {
            context.Set<T>().Add(entity);
            context.SaveChanges();
         }
      }

      public void DeleteEntity(T entity)
      {
         using (SocialNetworkContext context = new SocialNetworkContext())
         {
            if (entity.GetType() == typeof(User))
            {
               var user = context.Users.Find((entity as User).UserId);
               context.Users.Remove(user);
               context.SaveChanges();
            }


         }
      }
   }
}
