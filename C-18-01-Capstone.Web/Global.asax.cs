using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
using C_18_01_Capstone.Web.Infrastructure.Filters;
using C_18_01_Capstone.Web.Infrastructure.SimpleInjector;
using C_18_01_Capstone.Web.Services;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace C_18_01_Capstone.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new HandleExceptionAttribute());
            GlobalFilters.Filters.Add(new CustomActionAttribute());

            var container = new Container();

            container.Options.PropertySelectionBehavior
                = new ImportPropertySelectionBehavior();
            container.Options.DefaultScopedLifestyle
                 = new WebRequestLifestyle();

            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);
            container.Register<IApiClient, ApiClient>(Lifestyle.Scoped);
            container.Register<IEncryptionService, EncryptionService>(Lifestyle.Scoped);

            container.RegisterMvcControllers();
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
