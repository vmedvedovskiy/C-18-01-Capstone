using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_18_01_Capstone.Web.ViewModels
{
  public class UserViewModel
  {
    [Required(ErrorMessage = "This field is required")]
    public string Login { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public string Password { get; set; }
  }
}