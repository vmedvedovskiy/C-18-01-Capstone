using System;
using System.Collections.Generic;

namespace C_18_01_Capstone.Web.ViewModels
{
  public class UserViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string CountryIso { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }

        public IReadOnlyList<CountryViewModel> 
            Countries { get; set; }
  }
}