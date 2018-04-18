using C_18_01_Capstone.Main.DataContext;


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

      public void DeleteEntity(T entity)
      {
         using (var context = new SocialNetworkContext())
         {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
         }
      }
   }
}
