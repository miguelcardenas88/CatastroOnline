using AutomaticFootControl.Helpers;
using AutomaticFootControl.Models.Pantalla;
using AutomaticFootControl.Models.Usuario;
using Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers
{
    [NoCacheControl]
    public class InicioSesionController : Controller
    {
        private IInicioSesion _inicioSesion;

        public InicioSesionController()
        {
            _inicioSesion = new Servicio.InicioSesion();
        }

        [NoValidarSesionAplication]
        public ActionResult InicioSesion()
        {
            Session.RemoveAll();
            //Aqui poner codigo catalpgos, tamaños maximos para las cajas de texto, etc
            UsuarioModelo usuario = new UsuarioModelo();
            return View();
        }

        [NoValidarSesionAplication]
        public ActionResult CerrarSesion()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            Response.AppendHeader("Cache-Control", "no-store");

            //Aqui poner codigo catalpgos, tamaños maximos para las cajas de texto, etc
            UsuarioModelo usuario = new UsuarioModelo();
            return View("InicioSesion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> ValidarUsuarioContrasenia(UsuarioModelo usuario)
        {
            try
            {
                //Rest rest = new Rest();
                //usuario = rest.ValidarUsuarioContrasenia(usuario);
                usuario.IdTipoUsuario = 3;
                usuario = await _inicioSesion.ValidarInicioSesionAsync<UsuarioModelo>(usuario);
                if (usuario == null)
                {
                    PantallaModelo Pantalla = new PantallaModelo
                    {
                        MensajeInformativo = "Usuario / Contraseña incorrectos"
                    };
                    usuario = new UsuarioModelo
                    {
                        Pantalla = new PantallaModelo()
                    };
                    usuario.Pantalla = Pantalla;
                    return View("InicioSesion", usuario);
                }
                usuario.Contrasenia = null;
                Session["UsuarioLogueado"] = true;
                Session["DatosUsuario"] = usuario;

                return RedirectToAction("Inicio", "Menu");
            }
            catch (Exception ex)
            {
                //clsConfiguracion.SesionCaducada(); //Aqui caducar las sesiones si da error y registrar logs
                Session.RemoveAll();
                return RedirectToAction("InicioSesion", "InicioSesion");
            }
        }

        // GET: InicioSesion
        public ActionResult Index()
        {
            return View();
        }
    }
}