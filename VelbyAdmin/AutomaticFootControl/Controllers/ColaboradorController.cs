using AutomaticFootControl.Models;
using AutomaticFootControl.Models.Usuario;
using AutomaticFootControl.Validadores;
using Comun;
using Comun.Modelo;
using Interfaz;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UsuarioModelo = AutomaticFootControl.Models.Usuario.UsuarioModelo;

namespace AutomaticFootControl.Controllers
{
    public class ColaboradorController : Controller
    {
        private IColaborador _IColaborador;

        public ColaboradorController()
        {
            _IColaborador = new Colaborador();
        }

        /// <summary>
        /// Obtiene la lista de Colaboradores registrados
        /// </summary>
        /// <param name="respuestaComun"></param>
        /// <returns></returns>
        public ActionResult ListarColaborador()
        {
            return PartialView();
        }

        private async Task<vmoUsuario> ObtenerColaboradoresBase(RespuestaComun respuestaComun)
        {
            vmoUsuario vmUsuario = new vmoUsuario();
            UsuarioModelo mcolaborador = new UsuarioModelo { Estado = "1", TipoUsuario = "2" };
            vmUsuario = await _IColaborador.ObtenerListaColaboradorAsync<vmoUsuario>(mcolaborador);
            //vmUsuario = DatosTemporales();

            if (vmUsuario != null && vmUsuario.Respuesta != null)
                ViewBag.RespuestaErrorControlado = vmUsuario.Respuesta;

            Session["ListaColaboradores"] = vmUsuario.LstColaborador;
            return vmUsuario;
        }


        public async Task<JsonResult> ObtenerColaboradores()
        {
            vmoUsuario servicioVistaModelo = new vmoUsuario();
            RespuestaComun respuestaComun = new RespuestaComun();
            servicioVistaModelo = await this.ObtenerColaboradoresBase(respuestaComun);
            if (servicioVistaModelo.LstColaborador == null)
                servicioVistaModelo.LstColaborador = new List<UsuarioModelo>();
            var data = servicioVistaModelo.LstColaborador.ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ObtenerColaborador(string idColaborador)
        {
            UsuarioModelo usuario = new UsuarioModelo();
            List<UsuarioModelo> LstColaborador = (List<UsuarioModelo>)Session["ListaColaboradores"];
            usuario = LstColaborador.FirstOrDefault(x => x.IdColaborador == int.Parse(idColaborador));
            return PartialView("GestionColaborador", usuario);
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(UsuarioModelo usuario)
        {
            RespuestaComun respuesta = await _IColaborador.ProcesarGestionColaborador<RespuestaComun>(usuario);
            if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            {
                vmoUsuario vmUsuario = await this.ObtenerColaboradoresBase(null);
                return PartialView("ListarColaborador", vmUsuario.LstColaborador);
            }
            else
            {
                ErrorModelo errorModelo = new ErrorModelo();
                string strError = respuesta.Mensaje + "|" + respuesta.Codigo;
                return RedirectToAction("Error", "Error", new { @error = strError });
            }
        }

        private vmoUsuario DatosTemporales()
        {
            UsuarioModelo mcolaborador = new UsuarioModelo { Estado = "A" };
            mcolaborador.Apellidos = "Apellido 1";
            mcolaborador.Celular = "0999999999";
            mcolaborador.ConfirmarContrasenia = "1234";
            mcolaborador.Contrasenia = "1234";
            mcolaborador.Correo = "correo1";
            mcolaborador.DireccionDomicilio = "Aqui 1";
            mcolaborador.DocumentoIdentidad = "1777777777";
            mcolaborador.Estado = "A";
            mcolaborador.FechaNacimiento = DateTime.Now;
            mcolaborador.IdCliente = 0;
            mcolaborador.IdColaborador = 100;
            mcolaborador.IdGenero = 1;
            mcolaborador.IdTipo = 2;
            mcolaborador.IdTipoUsuario = 2;
            mcolaborador.TipoUsuario = "Colaborador";
            mcolaborador.NombreCompleto = "Nombre apellido 1";
            mcolaborador.Nombres = "Nombre 1";
            mcolaborador.Hojavida = null;
            mcolaborador.RecordPolicial = null;
            mcolaborador.Entrevista = false;
            mcolaborador.DescripcionEntrevista = "No entrevista";
            mcolaborador.PruebaTecnica = false;
            mcolaborador.DescripcionPrueba = "No prueba";
            mcolaborador.Foto = string.Empty;


            UsuarioModelo mcolaborador2 = new UsuarioModelo { Estado = "A" };
            mcolaborador2.Apellidos = "Apellido 2";
            mcolaborador2.Celular = "0888888888";
            mcolaborador2.ConfirmarContrasenia = "1234";
            mcolaborador2.Contrasenia = "1234";
            mcolaborador2.Correo = "correo 2";
            mcolaborador2.DireccionDomicilio = "Aqui 2";
            mcolaborador2.DocumentoIdentidad = "1666666666";
            mcolaborador2.Estado = "A";
            mcolaborador2.FechaNacimiento = DateTime.Now;
            mcolaborador2.IdCliente = 0;
            mcolaborador2.IdColaborador = 200;
            mcolaborador2.IdGenero = 1;
            mcolaborador2.IdTipo = 2;
            mcolaborador2.IdTipoUsuario = 2;
            mcolaborador2.TipoUsuario = "Colaborador";
            mcolaborador2.NombreCompleto = "Nombre 2 apellido 2";
            mcolaborador2.Nombres = "Nombre 2";
            mcolaborador2.Hojavida = null;
            mcolaborador2.RecordPolicial = null;
            mcolaborador2.Entrevista = true;
            mcolaborador2.DescripcionEntrevista = "OK";
            mcolaborador2.PruebaTecnica = true;
            mcolaborador2.DescripcionPrueba = "OK";
            mcolaborador2.Foto = string.Empty;


            vmoUsuario usuario = new vmoUsuario();
            List<UsuarioModelo> lstColaboradores = new List<UsuarioModelo>();
            lstColaboradores.Add(mcolaborador);
            lstColaboradores.Add(mcolaborador2);

            usuario.LstColaborador = lstColaboradores;

            return usuario;
        }

    }
}