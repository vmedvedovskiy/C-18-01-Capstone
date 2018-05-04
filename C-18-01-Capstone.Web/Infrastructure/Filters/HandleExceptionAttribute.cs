using C_18_01_Capstone.Web.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace C_18_01_Capstone.Web.Infrastructure.Filters
{
    public class HandleExceptionAttribute : FilterAttribute,
        IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                (
                    new Dictionary<string, object>
                    {
                        { "controller", "Error" },
                        { "action", "Error" },
                        { "statusCode", filterContext.HttpContext.Response.StatusCode },
                        { "message", filterContext.Exception.Message }
                    }
                    ));
            filterContext.ExceptionHandled = true;

            DataLogger.LogOperation($"Exception: {filterContext.HttpContext.Response.StatusCode}" +
                $" message: {filterContext.Exception.Message}");
        }
    }
}