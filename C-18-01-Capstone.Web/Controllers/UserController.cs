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

        public UserController(IApiClient apiClient
            , IEncryptionService encryptionService)
        {
            this.apiClient = apiClient;
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            var user = this.GetUser();

            var userViewModel = new LoginViewModel
            {
                Login = user.Result.Login
            };

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

            var hasUserCreated = await this.apiClient
                .CreateUser(this.Convert(userViewModel));

            if (!hasUserCreated)
            {
<<<<<<< HEAD
                return RedirectPermanent("/User/Login");
                //return this.Login();
            }

            this.ModelState.AddModelError(
=======
                this.ModelState.AddModelError(
>>>>>>> d99fed55eeb362394f98f401000baee9260fab78
                nameof(UserViewModel.Login),
                "Something went wrong. Please, try again");
            }            

            return this.Login();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
<<<<<<< HEAD
            var user = this.GetUser();
            
            return this.View();
=======
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
                RedirectToAction("Index");
            }

            throw new ApplicationException();
>>>>>>> d99fed55eeb362394f98f401000baee9260fab78
        }

        private async Task<UserViewModel> GetUser()
        {
            return (await this.apiClient.FindUser("5"))
               .Select(_ => new UserViewModel
               {
                   BirthDate = _.BirthDate,
                   FirstName = _.FirstName,
                   Login = _.Login
               })
               .Where(_=>_.Login=="5")
               .First();
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