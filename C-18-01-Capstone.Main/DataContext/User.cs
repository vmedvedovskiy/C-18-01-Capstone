using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_18_01_Capstone.Main.DataContext
{
    public class User
    {
        [Key]
        public Guid UserId { get; private  set; } = Guid.NewGuid();
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }
        
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        [ForeignKey("UserId")]
        public ICollection<UserFriend> Friends { get; set; }

        [ForeignKey("AuthorId")]
        public ICollection<Post> Posts { get; set; }
    }
}
