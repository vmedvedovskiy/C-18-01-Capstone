using System;

namespace C_18_01_Capstone.Services
{
    public class UserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }

        public string CountryId { get; set; }

        public CountryModel Country { get; set; }

        public string Login { get; set; }

        public string Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}
