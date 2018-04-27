using System.Web.Http;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace C_18_01_Capstone.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            InitDependencyResolution(GlobalConfiguration.Configuration);
        }

        private static void InitDependencyResolution(
            HttpConfiguration config)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle
                = new AsyncScopedLifestyle();
            
            container.Register(
                typeof(IDataAccess<>), 
                typeof(EfDataAccess<>),
                Lifestyle.Scoped);

            container
                .Register<IUserService, UserService>(
                Lifestyle.Scoped);

            container
                .Register<ICountryService, CountryService>(
                Lifestyle.Scoped);

            container
               .Register<IEncryptionService, EncryptionService>(
               Lifestyle.Scoped);
            
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
