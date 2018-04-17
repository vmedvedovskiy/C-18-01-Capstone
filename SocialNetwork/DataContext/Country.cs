using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataContext
{
    public class Country
    {
        [Key]
        [MaxLength(3)]
        public string Alpha3 { get; set; }

        [MaxLength(2)]
        public string Alpha2 { get; set; }

        public string Name { get; set; }

        public int NumericCode { get; set; }

        public ICollection<User> Users { get; set; }

        public Country(string name, string alpha2, string alpha3, int numericCode)
        {
            this.Alpha2 = alpha2;
            this.Alpha3 = alpha3;
            this.Name = name;
            this.NumericCode = numericCode;
        }
    }
}
