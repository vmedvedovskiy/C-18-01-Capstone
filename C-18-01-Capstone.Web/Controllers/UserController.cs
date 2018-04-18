using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
using C_18_01_Capstone.Web.ViewModels;

namespace C_18_01_Capstone.Web.Controllers
{  
    public class UserController : Controller
    {
    private readonly IEncryptionService encryptionService;
    private readonly IUserService userService;

    public UserController()
    {
      encryptionService = new EncryptionService();
      userService = new UserService();
    }

    // GET: User
    [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {     
            return View();
        }

    [HttpGet]
    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
      if (ModelState.IsValid)
      {
        var user = new User
        {
          Login = userViewModel.Login,
          HashedPassword = encryptionService.EncryptPassword(
            userViewModel.Password, encryptionService.CreateSalt())
        };
        userService.Add(user);
        return View();
      }
      throw new ApplicationException();
    }

        [HttpPost]
        public string Login(LoginViewModel loginViewModel)
        {
      if (ModelState.IsValid)
      {
        return "Logged in!";
      }
      throw new ApplicationException();
        }
    }
}