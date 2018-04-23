using System.Web.Http;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.API.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Post(
            CreateUserApiModel userToCreate)
        {
            if(string.IsNullOrEmpty(userToCreate.Login))
            {
                return this.BadRequest("Login is required");
            }

            if (string.IsNullOrEmpty(userToCreate.Password))
            {
                return this.BadRequest("Password is required");
            }

            if (string.IsNullOrEmpty(userToCreate.CountryIso))
            {
                return this.BadRequest("CountryIso is required");
            }

            this.userService.Add(
                new Services.CreateUserModel
            {
                BirthDate = userToCreate.BirthDate,
                CountryIsoCode3 = userToCreate.CountryIso,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Login = userToCreate.Login,
                Password = userToCreate.Password
            });

            return this.Ok();
        }
    }
}
