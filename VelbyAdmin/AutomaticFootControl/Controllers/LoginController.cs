using AutomaticFootControl.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers
{
    [NoCacheControl]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Entrar()
        {
            string ddd = string.Empty;
            return View();
        }
    }
}