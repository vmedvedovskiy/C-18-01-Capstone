using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_18_01_Capstone.Web.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Enter a login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Enter a password")]
    public string Password { get; set; }
  }
}