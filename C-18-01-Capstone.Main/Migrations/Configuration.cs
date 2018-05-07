namespace C_18_01_Capstone.Main.Migrations
{
    using C_18_01_Capstone.Main.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialNetworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "C_18_01_Capstone.Main.DataContext.SocialNetworkContext";
        }

        protected override void Seed(SocialNetworkContext context)
        {
            

            if(!context.Set<Country>().Any())
            {
                var dbInitializer = new DbInitializer();

                dbInitializer.InitializeCountriesTable();
            }            
        }
    }
}
