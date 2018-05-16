﻿using System.Web.Http;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.API.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UserController : ApiController
    {
        private const string LoginIsRequired = "Login is required";
        private const string PasswordIsRequired = "Password is required";

        private readonly IUserService userService;

        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("{login}")]
        public IHttpActionResult Get(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return this.BadRequest(LoginIsRequired);
            }

            var user = userService.FindUser(login);

            if(user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [HttpGet]
        [Route("authorizedRoute")]
        [Authorize]
        public IHttpActionResult AuthorizedRoute()
        {
            return this.Ok("authorized!");
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Post(
            CreateUserApiModel userToCreate)
        {
            if(string.IsNullOrEmpty(userToCreate.Login))
            {
                return this.BadRequest(LoginIsRequired);
            }

            if (string.IsNullOrEmpty(userToCreate.Password))
            {
                return this.BadRequest(PasswordIsRequired);
            }

            if (string.IsNullOrEmpty(userToCreate.CountryId))
            {
                return this.BadRequest("CountryIso is required");
            }

            this.userService.Add(
                new CreateUserModel
            {
                BirthDate = userToCreate.BirthDate,
                CountryIsoCode3 = userToCreate.CountryId,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Login = userToCreate.Login,
                Password = userToCreate.Password
            });

            return this.Ok();
        }
    }
}
