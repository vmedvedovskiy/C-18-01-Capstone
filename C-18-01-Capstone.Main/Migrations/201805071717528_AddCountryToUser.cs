namespace C_18_01_Capstone.Main.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryToUser : DbMigration
    {
        public override void Up()
        {
            this.Sql(
                @"Update Users set CountryID = 'UKR'
                  where CountryId is null");
        }
        
        public override void Down()
        {
        }
    }
}
