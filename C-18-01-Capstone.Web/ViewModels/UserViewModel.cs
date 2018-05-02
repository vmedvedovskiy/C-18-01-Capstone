using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C_18_01_Capstone.Web.ViewModels
{
  public class UserViewModel
    {
        [Required(ErrorMessage = "Enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter a last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter a birth date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Enter a Country")]
        public string CountryIso { get; set; }

        [Required(ErrorMessage = "Enter a login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }
        
        public IReadOnlyList<CountryViewModel> 
            Countries { get; set; }
  }
}