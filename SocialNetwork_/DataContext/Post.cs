using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataContext
{
    public class Post
    {
        [Key()]
        public Guid PostId { get; private set; } = Guid.NewGuid();
        public User User { get; set; }
        
        public Guid AuthorId { get; set; }

        public string Titl { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PublicationDate { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public int ContentType { get; set; }
        public Content Content { get; set; }

        public string FileLink { get; set; }

        [ForeignKey("PostId")]
        public ICollection<Comment> Comments { get; set; }

        [ForeignKey("PostId")]
        public ICollection<Repost> Reposts { get; set; }

        [ForeignKey("PostId")]
        public ICollection<Like> Likes { get; set; }
        
    }
}
