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
using C_18_01_Capstone.Web.Infrastructure.Filters;

namespace C_18_01_Capstone.Web.Controllers
{
    [AuthorizeUser]
    public class UserController : Controller
    {
        private readonly IApiClient apiClient;
        private readonly IEncryptionService encryptionService;

        private const string LoginError = "That username is taken. Try another.";

        public UserController(IApiClient apiClient
            , IEncryptionService encryptionService)
        {
            this.apiClient = apiClient;
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            var x = this.apiClient.GetToken("","");

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
        [AllowAnonymous]
        public async Task<ActionResult> Register(
            UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            //if(ExistUserLogin(userViewModel.Login).Result)
            //{
            //    this.ModelState.AddModelError("", LoginError);
            //    return View(userViewModel);
            //}
            
            var hasUserCreated = await this.apiClient.CreateUser(this.Convert(userViewModel));

            if (!hasUserCreated)
            {
                this.ModelState.AddModelError(
                nameof(UserViewModel.Login),
                "Something went wrong. Please, try again");
            }            

            return RedirectToAction("Login");
        }
        
        private async Task<bool> ExistUserLogin(string login)
        {
            UserModel user = await this.apiClient.GetUser(login);

            if (user == null)
                return false;
            
            return true;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            UserModel user = await apiClient.GetUser(loginModel.Login);

            string hashedPassword = encryptionService.EncryptPassword
                (loginModel.Password, user.Salt);

            if (user.HashedPassword.Equals(hashedPassword,
                StringComparison.Ordinal))
            {
                HttpContext.Session["Login"] = user.Login;
                HttpContext.Session["HashedPassword"] = user.HashedPassword;
                return RedirectToAction("Index");
            }
            else
            {
                throw new ApplicationException();
            }
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