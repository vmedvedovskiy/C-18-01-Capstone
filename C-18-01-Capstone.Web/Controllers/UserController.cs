using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Web.ViewModels;
using C_18_01_Capstone.Web.Services;
using C_18_01_Capstone.Services.Services;
using C_18_01_Capstone.Services;

namespace C_18_01_Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiClient apiClient;
        private readonly IEncryptionService encryptionService;

        private readonly string LoginError = "The user name or password provided is incorrect.";

        public UserController(IApiClient apiClient, IEncryptionService encryptionService)
        {
            this.apiClient = apiClient;
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> Index()
        {
            string login = this.Request.QueryString["login"];
            var x = System.Web.HttpContext.Current.User.Identity.Name;

            UserModel user = await apiClient.GetUser(login);

            return View(user);
            
        }

        [HttpGet]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var userViewModel = new UserViewModel
            {
                BirthDate = DateTime.Now,
                FirstName = "Mihail",
                LastName = "Ivanov",
                Login = "loginName",
                Password = "46672754",
                Countries = await this.GetCountries()
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Register(
            UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            var hasUserCreated = await this.apiClient
                .CreateUser(this.Convert(userViewModel));

            if (!hasUserCreated)
            {
                this.ModelState.AddModelError(
                nameof(UserViewModel.Login),
                "Something went wrong. Please, try again");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            UserModel user = await apiClient.GetUser(loginModel.Login);

            if(user is null)
            {
                ModelState.AddModelError("", LoginError);
                return this.View();
            }

            string hashedPassword = encryptionService.EncryptPassword
                (loginModel.Password, user.Salt);

            if (user.HashedPassword.Equals(hashedPassword,
                StringComparison.Ordinal))
            {
                return RedirectToAction("Index", "User", new {  user.Login});
            }

            ModelState.AddModelError("", LoginError);
            return this.View();
        }
        
        private async Task<IReadOnlyList<CountryViewModel>> 
            GetCountries()
        {
            return (await this.apiClient.GetCountries())
                .Select(_ => new CountryViewModel
                {
                    CountryId = _.CountryId,
                    Name = _.Name
                })
                .ToList()
                .AsReadOnly();
        }

        private CreateUserApiModel Convert(
            UserViewModel userViewModel)
        {
            return new CreateUserApiModel
            {
                Login = userViewModel.Login,
                BirthDate = userViewModel.BirthDate,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                CountryId = userViewModel.CountryIso,
                Password = userViewModel.Password
            };
        }
    }
}