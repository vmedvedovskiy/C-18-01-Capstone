using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C_18_01_Capstone.Web.Infrastructure.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.HttpContext.Session[""]
            base.OnAuthorization(filterContext);
        }
    }
}