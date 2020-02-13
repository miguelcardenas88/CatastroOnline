using AutomaticFootControl.Helpers;
using AutomaticFootControl.Models;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [NoValidarSesionAplication]
        public ActionResult Index()
        {
            return View();
        }

        [NoValidarSesionAplication]
        public ActionResult Error(string error)
        {
            ErrorModelo errorModelo = new ErrorModelo();
            string strError = string.Format(Constantes.HTML_ERROR_GENERICO, error.Split('|')[0], error.Split('|')[1], "Lo sentimos, por favor comunicate con SmartCode Solutions", "1800 000 000");
            errorModelo.ErrorHtml = strError;
            return PartialView(errorModelo);
        }
    }
}