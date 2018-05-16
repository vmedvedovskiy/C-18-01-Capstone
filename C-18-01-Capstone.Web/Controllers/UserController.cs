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
using C_18_01_Capstone.Web.Infrastructure;

namespace C_18_01_Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiClient apiClient;
        private readonly IEncryptionService encryptionService;

        private const string LoginError = "That username is taken. Try another.";

        public UserController(
            IApiClient apiClient, 
            IEncryptionService encryptionService)
        {
            this.apiClient = apiClient;
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        //[AuthorizeUser]
        public async Task<ActionResult> Index(string login)
        {
            //string result = await this.apiClient.CheckAuthorizedRoute();
            //UserModel user = await apiClient.GetUser(login);
            return View((object)login);//user);
        }

        [HttpGet]
        public PartialViewResult GetProfileData(string login)
        {
            UserModel user = null;
            var task = Task.Run(async () => { user = await apiClient.GetUser(login); });
            task.Wait();

            return PartialView("_ProfileData", user);
        }

        [HttpGet]
        [AllowAnonymous]
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

        private bool TryParse(string value, out object result)
        {
            result = 10;

            return true;
        }

        [HttpPost]
        public async Task<ActionResult> Register(
            UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException();
            }

            if(await ExistUserLogin(userViewModel.Login))
            {
                this.ModelState.AddModelError("", LoginError);

                return View(userViewModel);
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
        

        private async Task<bool> ExistUserLogin(string login)
            => (await this.apiClient.GetUser(login)) != null;

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

            await apiClient.GetAndStoreToken(loginModel.Login, hashedPassword);

            return RedirectToAction("Index", new { Login = loginModel.Login });
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