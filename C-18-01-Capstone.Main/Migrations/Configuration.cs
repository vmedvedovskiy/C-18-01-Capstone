namespace C_18_01_Capstone.Main.Migrations
{
    using C_18_01_Capstone.Main.DataContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<C_18_01_Capstone.Main.DataContext.SocialNetworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "C_18_01_Capstone.Main.DataContext.SocialNetworkContext";
        }

        protected override void Seed(C_18_01_Capstone.Main.DataContext.SocialNetworkContext context)
        {
            DbInitializer dbInitializer = new DbInitializer();
            dbInitializer.InitializeCountriesTable();
        }
    }
}
