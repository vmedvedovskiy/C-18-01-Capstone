namespace C_18_01_Capstone.Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        Title = c.String(),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PublicationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CategoryId = c.Guid(nullable: false),
                        ContentType = c.Int(nullable: false),
                        FileLink = c.String(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Contents", t => t.ContentType)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId)
                .Index(t => t.ContentType);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        PostId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.UserId, t.CommentId })
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ContentType = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ContentType);
            
            CreateTable(
                "dbo.Reposts",
                c => new
                    {
                        RepostId = c.Guid(nullable: false),
                        PostId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        PublicationData = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.RepostId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CountryId = c.String(),
                        Login = c.String(),
                        Salt = c.String(),
                        HashedPassword = c.String(),
                        Country_CountryIsoCode3 = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryIsoCode3)
                .Index(t => t.Country_CountryIsoCode3);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryIsoCode3 = c.String(nullable: false, maxLength: 3),
                        CountryIsoCode2 = c.String(maxLength: 2),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryIsoCode3);
            
            CreateTable(
                "dbo.UserFriends",
                c => new
                    {
                        FriendId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FriendId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserFriends", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Country_CountryIsoCode3", "dbo.Countries");
            DropForeignKey("dbo.Reposts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ContentType", "dbo.Contents");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.UserFriends", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Country_CountryIsoCode3" });
            DropIndex("dbo.Reposts", new[] { "PostId" });
            DropIndex("dbo.Likes", new[] { "CommentId" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "ContentType" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropTable("dbo.UserFriends");
            DropTable("dbo.Countries");
            DropTable("dbo.Users");
            DropTable("dbo.Reposts");
            DropTable("dbo.Contents");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
