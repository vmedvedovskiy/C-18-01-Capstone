using System.Web.Http;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
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
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle
                = new AsyncScopedLifestyle();

            // Register your types, for instance:
            container
                .Register<IDataAccess<User>, EfDataAccess<User>>(
                Lifestyle.Scoped);

            container
                .Register<IDataAccess<Country>, EfDataAccess<Country>>(
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

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
