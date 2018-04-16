using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_18_01_Capstone.Web.ViewModels;

namespace C_18_01_Capstone.Web.Controllers
{  
    public class UserController : Controller
    {
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

        [HttpPost]
        public string Login(LoginViewModel loginViewModel)
        {
          return "Logged in!";
        }
    }
}