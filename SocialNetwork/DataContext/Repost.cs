using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataContext
{
    public class Repost
    {
        [Key]
        public Guid RepostId { get; private set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PublicationData { get; set; }

        public Repost()
        {
            this.RepostId = Guid.NewGuid();
        }
    }
}
