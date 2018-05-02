using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C_18_01_Capstone.Web.ViewModels;

namespace C_18_01_Capstone.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error(string message, string statusCode)
        {
            return View(new ErrorViewModel
            {
                StatusCode = statusCode,
                Message = message
            });
        }
    }
}