using System.Web.Http;
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

            return this.Ok<UserModel>(userService.FindUser(login));
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
                new Services.CreateUserModel
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
