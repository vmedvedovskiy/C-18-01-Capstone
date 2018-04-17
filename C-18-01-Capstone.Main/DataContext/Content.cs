using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_18_01_Capstone.Main.DataContext
{
    public class Content
    {
        [Key]
        public int TypeContent { get; set; }

        public string Name { get; set; }

        [ForeignKey("TypeContent")]
        public ICollection<Post> Posts { get; set; }
    }
}
