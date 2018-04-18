using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_18_01_Capstone.Main.DataContext
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; private set; } = Guid.NewGuid();

        public Guid PostId { get; set; }

        public Guid AuthorId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        public string Content { get; set; }

        [ForeignKey("CommentId")]
        public ICollection<Like> Likes { get; set; }
    }
}
