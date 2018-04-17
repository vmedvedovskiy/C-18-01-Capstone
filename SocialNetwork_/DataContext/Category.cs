using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataContext
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        public int Number { get; set; }

        [ForeignKey("CategoryId")]
        public ICollection<Post> Posts { get; set; }
    }
}
