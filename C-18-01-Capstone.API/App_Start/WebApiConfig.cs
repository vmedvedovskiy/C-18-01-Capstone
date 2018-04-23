using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace C_18_01_Capstone.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Services.Replace(
                typeof(IExceptionHandler),
                new OopsExceptionHandler());
        }
    }
}
