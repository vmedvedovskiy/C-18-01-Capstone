using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace C_18_01_Capstone.Web
{
    public class RouteConfig
    {
        private const string IdParameterName = "id";

        public static void RegisterRoutes(RouteCollection routes)
        {
      
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "Profile",
                    url: $"user/{IdParameterName}",
                    defaults: new { controller = "User", action = "Index" },
                    constraints: new UserProfileConstraint()
            );

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
        }

        public class UserProfileConstraint 
            : IRouteConstraint
        {
            public bool Match
                (
                    HttpContextBase httpContext,
                    Route route,
                    string parameterName,
                    RouteValueDictionary values,
                    RouteDirection routeDirection
                )
            {
                Guid parsedId;

                return Guid.TryParse(
                    route
                        .GetRouteData(httpContext)
                        .Values[IdParameterName]
                        .ToString(),
                    out parsedId);
            }
        }
    }
}
