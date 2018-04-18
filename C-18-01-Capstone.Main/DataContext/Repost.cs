using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_18_01_Capstone.Main.DataContext
{
    public class Repost
    {
        [Key]
        public Guid RepostId { get; private set; } = Guid.NewGuid();
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PublicationData { get; set; }
    }
}
