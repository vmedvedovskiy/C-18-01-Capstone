namespace C_18_01_Capstone.Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyForCountry : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Country_CountryIsoCode3" });
            DropColumn("dbo.Users", "CountryId");
            RenameColumn(table: "dbo.Users", name: "Country_CountryIsoCode3", newName: "CountryId");
            AlterColumn("dbo.Users", "CountryId", c => c.String(maxLength: 3));
            CreateIndex("dbo.Users", "CountryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "CountryId" });
            AlterColumn("dbo.Users", "CountryId", c => c.String());
            RenameColumn(table: "dbo.Users", name: "CountryId", newName: "Country_CountryIsoCode3");
            AddColumn("dbo.Users", "CountryId", c => c.String());
            CreateIndex("dbo.Users", "Country_CountryIsoCode3");
        }
    }
}
