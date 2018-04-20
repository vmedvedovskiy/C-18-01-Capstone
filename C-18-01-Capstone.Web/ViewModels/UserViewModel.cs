using C_18_01_Capstone.Main.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
  }
}