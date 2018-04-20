using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
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

            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle 
                = new WebRequestLifestyle();

            // Register your types, for instance:
            container
                .Register<IDataAccess<User>, EfDataAccess<User>>(
                Lifestyle.Scoped);

            container
                .Register<IUserService, UserService>(
                Lifestyle.Scoped);

            container
               .Register<IEncryptionService, EncryptionService>(
               Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }


        protected void Application_EndRequest()
        {   //here breakpoint
            // under debug mode you can find the exceptions at code: 
            var result = this.Context.AllErrors;
        }
    }
}
