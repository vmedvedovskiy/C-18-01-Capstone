using System;
using System.Linq;
using System.Web.Mvc;
using C_18_01_Capstone.Main.DataAccessLayer;
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
            var userViewModel = new UserViewModel
            {
                BirthDate = DateTime.Now,
                CountryIso = "Ukrain",
                FirstName = "Mihail",
                LastName = "Ivanov",
                Login = "loginName",
                Password = "46672754"
            };

            var dataAccess = new DataAccess<Country>();

            HtmlLists.Countries = dataAccess.GetEntities().OrderBy(_ => _.Name).ToList();
            
            return View(userViewModel);
        }
        
        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                string salt = encryptionService.CreateSalt();
                var user = new User
                {
                    Login = userViewModel.Login,
                    BirthDate = userViewModel.BirthDate,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    CountryId = userViewModel.CountryIso,
                    Salt = salt,
                    HashedPassword = encryptionService.EncryptPassword(userViewModel.Password,
                                                salt),
                };
                userService.Add(user);
                return Login();
            }
            else
            {
                throw new ApplicationException();
            }
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