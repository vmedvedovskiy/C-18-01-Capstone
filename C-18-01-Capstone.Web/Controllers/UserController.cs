using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Web.ViewModels;
using C_18_01_Capstone.Web.Services;

namespace C_18_01_Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiClient apiClient;

        public UserController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

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
        public async Task<ActionResult> Register()
        {
            var countries = this.GetCountries();

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

            if (hasUserCreated)
            {
                return this.Login();
            }

            this.ModelState.AddModelError(
                nameof(UserViewModel.Login),
                "Something went wrong. Please, try again");

            return await this.Register();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
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