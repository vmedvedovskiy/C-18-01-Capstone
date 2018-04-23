using System;

namespace C_18_01_Capstone.API.Contract
{
    public class CreateUserApiModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string CountryIso { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}