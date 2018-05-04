using C_18_01_Capstone.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace C_18_01_Capstone.Web.Infrastructure.Filters
{
    public class CustomActionAttribute : ActionFilterAttribute,
                                IActionFilter, IResultFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DataLogger.LogOperation("Action Executing: " + filterContext.ActionDescriptor.ActionName);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogActionExecuted(filterContext);
           
        }

        private void LogActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                DataLogger.LogOperation("Exception thrown.");

            DataLogger.LogOperation("Action Executed: " + filterContext.ActionDescriptor.ActionName);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            DataLogger.LogOperation("Result Executing");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            DataLogger.LogOperation("Result Executed");
        }
    }
}