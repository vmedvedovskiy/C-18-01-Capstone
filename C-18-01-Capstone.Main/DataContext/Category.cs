using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_18_01_Capstone.Main.DataContext
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
