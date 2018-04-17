using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C_18_01_Capstone.Main.DataContext
{
    public class Country
    {
        [Key]
        [MaxLength(3)]
        public string CountryIsoCode3 { get; set; }

        [MaxLength(2)]
        public string CountryIsoCode2 { get; set; }

        public string Name { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}
