﻿using AutomaticFootControl.Helpers;
using AutomaticFootControl.Models.Pantalla;
using Comun;
using Comun.Modelo;
using Interfaz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PantallaModelo = Comun.Modelo.PantallaModelo;

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
        public ActionResult RegistroUsuarios()
        {
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
        public async Task<ActionResult> RegistrarUsuario(UsuarioModelo mUsuario)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta;


            respuesta = _inicioSesion.CrearUsuario(mUsuario, "I");
            if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            {
                ViewBag.Mensaje = "Ficha guardada correctamente";
                NotificacionCreacionUsuario(mUsuario);
            }
            else
            {
                ViewBag.Mensaje = "Error al guardar la ficha";
                //ErrorModelo errorModelo = new ErrorModelo();
                //string strError = respuesta.Mensaje + "|" + respuesta.Codigo;
                //return RedirectToAction("Error", "Error", new { @error = strError });
            }
            return PartialView(Enumerador.NombreVista.RegistroUsuarios.ToString(), new UsuarioModelo());
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
                //usuario = await _inicioSesion.ValidarInicioSesionAsync<UsuarioModelo>(usuario);
                usuario = _inicioSesion.ValidarInicioSesionAsync<UsuarioModelo>(usuario);
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
                if(usuario.Usuario.Equals("admin@admin.com"))
                {

                    return RedirectToAction("Inicio", "Menu");

                }
                else
                {
                    return RedirectToAction("InicioUsuarios", "Menu");
                }
            }
            catch (Exception ex)
            {
                //clsConfiguracion.SesionCaducada(); //Aqui caducar las sesiones si da error y registrar logs
                Session.RemoveAll();
                return RedirectToAction("InicioSesion", "InicioSesion");
            }
        }

        public void NotificacionCreacionUsuario(UsuarioModelo mUsuario)
        {
            List<EmailNotificacionModelo> lstEmail = new List<EmailNotificacionModelo>();
            EmailNotificacionModelo email;
            //foreach (UsuarioModelo colaborador in lstColaboradorSinAsig)
            //{
            email = new EmailNotificacionModelo
            {
                Asunto = "Creación de usuario",
                CorreoDestino = mUsuario.Correo,
                CueporMensaje = "/CreacionUsuario.html"
            };
                lstEmail.Add(email);
            //}

            //Pepara los datos para la plantilla
            Dictionary<string, string> datosPlantilla = new Dictionary<string, string>();
            datosPlantilla.Add("{Fecha}",DateTime.Now.ToString());
            datosPlantilla.Add("{Password}", mUsuario.Contrasenia);
            _inicioSesion.EnviarEmailAsync(lstEmail, datosPlantilla);
        }


        // GET: InicioSesion
        public ActionResult Index()
        {
            return View();
        }
    }
}