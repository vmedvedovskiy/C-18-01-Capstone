using System;
using System.Linq;
using System.Web.Mvc;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Services;
using C_18_01_Capstone.Web.ViewModels;

namespace C_18_01_Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IEncryptionService encryptionService;
        private readonly IUserService userService;
        
        public UserController(
            IEncryptionService encryptionService,
            IUserService userService)
        {
            this.encryptionService = encryptionService;
            this.userService = userService;
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
            return this.View();
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

            var dataAccess = new EfDataAccess<Country>();

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
                    HashedPassword = encryptionService
                    .EncryptPassword(userViewModel.Password, salt),
                };

                userService.Add(user);
            }
            else
            {
                throw new ApplicationException();
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            var user = this.userService
                .FindUser(loginModel.Login);

            if (user == null)
            {
                this.ModelState.AddModelError(
                    nameof(LoginViewModel.Login),
                    "User not found");

                return this.View();
            }

            if(this.encryptionService
                .EncryptPassword(loginModel.Password, user.Salt)
                 != user.HashedPassword)
            {
                this.ModelState.AddModelError(
                    nameof(LoginViewModel.Password),
                    "Password doesn't match");

                return this.View();
            }

            return this.Redirect("~/Home");
        }
    }
}