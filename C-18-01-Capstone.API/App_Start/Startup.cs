using System;
using System.Web.Http;
using C_18_01_Capstone.API.Infrastructure;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Services.Implementation.Services;
using C_18_01_Capstone.Services.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

[assembly: OwinStartup(typeof(C_18_01_Capstone.API.App_Start.Startup))]
namespace C_18_01_Capstone.API.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = GlobalConfiguration.Configuration;

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

            container
                .Register<IOAuthAuthorizationServerProvider,
                AuthorizationServerProvider>(
                Lifestyle.Scoped);

            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var options = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/api/v1/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                    Provider =
                    container.GetInstance<IOAuthAuthorizationServerProvider>()
                };
                appBuilder.UseOAuthAuthorizationServer(options);
                appBuilder.UseOAuthBearerAuthentication(
                    new OAuthBearerAuthenticationOptions());
            }

            WebApiConfig.Register(config);

            config.EnsureInitialized();
        }
    }
}