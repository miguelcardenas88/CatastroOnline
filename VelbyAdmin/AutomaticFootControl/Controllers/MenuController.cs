using AutomaticFootControl.Helpers;
using AutomaticFootControl.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers
{
    [NoCacheControl]
    public class MenuController : Controller
    {
        [NoValidarSesionAplication]
        public ActionResult Inicio()
        {
            //UsuarioModelo usuario = (UsuarioModelo)Session["DatosUsuario"];
            //ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.TipoUsuario = "admin";
            return View();
        }

        [NoValidarSesionAplication]
        public ActionResult InicioUsuarios()
        {
            //UsuarioModelo usuario = (UsuarioModelo)Session["DatosUsuario"];
            //ViewBag.NombreUsuario = usuario.Nombres;
            return View();
        }
    }
}
