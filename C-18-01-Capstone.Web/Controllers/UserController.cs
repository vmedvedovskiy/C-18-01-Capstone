using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Web.ViewModels;
using Newtonsoft.Json;

namespace C_18_01_Capstone.Web.Controllers
{
    public class UserController : Controller
    {
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
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient
                        .PostAsync(
                            "http://localhost:3122/api/v1/users",
                            await CreateApiRequest(userViewModel));
                }
            }
            else
            {
                throw new ApplicationException();
            }

            return this.Login();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            return this.View();
        }

        private async Task<IReadOnlyList<CountryViewModel>> 
            GetCountries()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient
                    .GetAsync("http://localhost:3122/api/v1/countries");

                var content = await result
                    .Content.ReadAsStringAsync();

                return await JsonConvert
                    .DeserializeObjectAsync<List<CountryApiModel>>(content)
                    .ContinueWith(task =>
                        task.Result.Select(country => new CountryViewModel
                        {
                            CountryId = country.CountryId,
                            Name = country.Name
                        })
                        .ToList()
                        .AsReadOnly());
            }
        }

        private async Task<HttpContent> CreateApiRequest(
            UserViewModel userViewModel)
        {
            var user = new CreateUserApiModel
            {
                Login = userViewModel.Login,
                BirthDate = userViewModel.BirthDate,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                CountryId = userViewModel.CountryIso 
                    ?? "ALB",
                Password = userViewModel.Password
            };

            return new StringContent(
                await JsonConvert.SerializeObjectAsync(user),
                Encoding.UTF8,
                "application/json");
        }
    }
}