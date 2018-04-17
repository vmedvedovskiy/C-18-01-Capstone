namespace SocialNetwork.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Reflection.Emit;
    

    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
            : base("name=SocialNetworkContext")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Friend>().HasKey(_ => new {_.FriendId, _.UserId });

            modelBuilder.Entity<Like>().HasKey(_ => new { _.PostId, _.UserId, _.CommentId});
        }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Repost> Reposts { get; set; }
    }
}