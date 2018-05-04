using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Web.Services;

namespace C_18_01_Capstone.Web.Infrastructure.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        [Import]
        public IApiClient ApiClient { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string login = (string)httpContext.Session["Login"];
            string hashedPassword = (string)httpContext.Session["HashedPassword"];

            Task<UserModel> getUserTask = ApiClient.GetUser(login);
            getUserTask.Wait();

            UserModel user = getUserTask.Result;

            var result = (user.HashedPassword.Equals(hashedPassword,
                StringComparison.Ordinal));

            Logging(result, user.Login);

            return result;
        }

        private void Logging(bool result, string login)
        {
            var successful = result ? "successful" : "unsuccessful";
            DataLogger.LogOperation($"{login}'s authorize was {successful}");
        }
        

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "User",
                        action = "Login"
                    })
               );
        }
    }
}