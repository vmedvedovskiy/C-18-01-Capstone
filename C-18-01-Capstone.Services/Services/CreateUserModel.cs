using System;

namespace C_18_01_Capstone.Services
{
    public class CreateUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }

        public string CountryIsoCode3 { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
