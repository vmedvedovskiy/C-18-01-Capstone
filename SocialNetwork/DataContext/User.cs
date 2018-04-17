using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataContext
{
    public class User
    {
        [Key]
        public Guid UserId { get; private  set; }
        public string FName { get; set; }

        public string LName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }
        
        public string CountryId { get; set; }
        public Country Country { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        [ForeignKey("UserId")]
        public ICollection<Friend> Friends { get; set; }

        [ForeignKey("AuthorId")]
        public ICollection<Post> Posts { get; set; }

        public User()
        {
            this.UserId = Guid.NewGuid();
        }
    }
}
