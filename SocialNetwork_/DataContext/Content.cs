using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataContext
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
