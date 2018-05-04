using System.Web.Http;
using C_18_01_Capstone.API.Infrastructure;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace C_18_01_Capstone.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        }
    }
}
