using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Services.Services;
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

            return (user.HashedPassword.Equals(hashedPassword,
                StringComparison.Ordinal));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "User",
                        action = "Login"
                    }
                    )
               );
        }
    }
}