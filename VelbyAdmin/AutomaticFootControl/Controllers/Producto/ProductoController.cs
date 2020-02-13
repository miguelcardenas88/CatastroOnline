using AutomaticFootControl.Models;
using AutomaticFootControl.Models.Producto;
using AutomaticFootControl.Utilitario;
using AutomaticFootControl.Validadores;
using Comun;
using Comun.Modelo;
using Interfaz;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers.Producto
{
    public class ProductoController : Controller
    {
        private IProducto _producto;

        public ProductoController()
        {
            _producto = new Servicio.Producto();
        }

        /// <summary>
        /// Obtiene la lista de Productos registrados
        /// </summary>
        /// <param name="respuestaComun"></param>
        /// <returns></returns>
        public ActionResult ListarProducto()
        {
            //string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(ipAddress))
            //{
            //    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            //}
            //ViewBag.IPAddress = ipAddress;
            //ViewBag.IPAddress2 = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            //ViewBag.IPAddress3 = Request.Params["HTTP_CLIENT_IP"];
            //ViewBag.IPAddress4 = Request.UserHostAddress;
            return PartialView();
        }

        /// <summary>
        /// Obtiene la lista de Productos registrados
        /// </summary>
        /// <param name="respuestaComun"></param>
        /// <returns></returns>
        public async Task<vmoServicio> ObtenerListaProductoBase(RespuestaComun respuestaComun)
        {
            vmoServicio servicioVistaModelo = new vmoServicio();
            ProductoModelo mProducto = new ProductoModelo { Estado = "T" };
            servicioVistaModelo = await _producto.ObtenerListaProductoAsync<vmoServicio>(mProducto);

            if (servicioVistaModelo != null && servicioVistaModelo.Respuesta != null)
            {
                ViewBag.RespuestaErrorControlado = servicioVistaModelo.Respuesta;
            }
            Session["ListaProductos"] = servicioVistaModelo.LstProducto;
            return servicioVistaModelo;
        }


        public async Task<ActionResult> loaddata()
        {
            vmoServicio servicioVistaModelo = new vmoServicio();
            RespuestaComun respuestaComun = new RespuestaComun();
            servicioVistaModelo = await this.ObtenerListaProductoBase(respuestaComun);
            if (servicioVistaModelo.LstProducto == null)
                servicioVistaModelo.LstProducto = new ObservableCollection<ProductoModelo>();
            var data = servicioVistaModelo.LstProducto.ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="mProducto"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Nuevo(modFicha mFicha)
        {
            //RespuestaComun respuesta;
            //if (ModelState.IsValid)
            //{
            //    mProducto.StrImagen = UtilitarioFront.RedimencionarImagen(mProducto.StrImagen, 768);
            //    respuesta = await _producto.ProcesarGestionProducto<RespuestaComun>(mProducto);
            //    if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            //    {
            //        vmoServicio servicioVistaModelo = new vmoServicio();
            //        servicioVistaModelo = await this.ObtenerListaProductoBase(null);
            //        return PartialView("ListarProducto", servicioVistaModelo.LstProducto);
            //    }
            //    else
            //    {
            //        //string strError = string.Format(Constantes.HTML_ERROR_GENERICO, respuesta.Codigo, respuesta.Mensaje, "Lo sentimos, por favor comunicate con SmartCode Solutions", "1800 000 000");
            //        ErrorModelo errorModelo = new ErrorModelo();
            //        string strError = respuesta.Mensaje + "|" + respuesta.Codigo;
            //        return RedirectToAction("Error", "Error", new { @error = strError });
            //    }
            //}
            //mProducto.TipoAccion = Constantes.ACCION_INSERTAR;
            return PartialView(Enumerador.NombreVista.GestionProducto.ToString(), mFicha);
        }

        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="IdServicio"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(string idServicio)
        {
            vmoServicio vServicio = new vmoServicio();
            if (Session["ListaProductos"] == null)
                vServicio = await ObtenerListaProductoBase(new RespuestaComun());
            else
                vServicio.LstProducto = (ObservableCollection<ProductoModelo>)Session["ListaProductos"];

            ProductoModelo productoModelo = new ProductoModelo();
            for (int x = 0; x < vServicio.LstProducto.Count; x++)
            {
                if (int.Parse(idServicio) == vServicio.LstProducto[x].IdServicios)
                {
                    productoModelo = vServicio.LstProducto[x];
                    break;
                }
            }
            productoModelo.TipoAccion = Constantes.ACCION_MODIFICAR;
            return PartialView(Enumerador.NombreVista.GestionProducto.ToString(), productoModelo);
        }


        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="IdServicio"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(string idServicio)
        {
            ProductoModelo mProducto = new ProductoModelo();
            mProducto.Estado = Constantes.ESTADO_ACTIVO;
            mProducto.TipoAccion = Constantes.ACCION_ELIMINAR;
            mProducto.IdServicios = int.Parse(idServicio);
            RespuestaComun respuesta = await _producto.ProcesarGestionProducto<RespuestaComun>(mProducto);
            vmoServicio servicioVistaModelo = new vmoServicio();
            servicioVistaModelo = await this.ObtenerListaProductoBase(null);
            return PartialView("ListarProducto", servicioVistaModelo.LstProducto);
        }

        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="IdServicio"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        public ActionResult ObtenerProductoXId(string IdServicio)
        {
            ObservableCollection<ProductoModelo> lstProductos = ((ObservableCollection<ProductoModelo>)HttpContext.Session["Productos"]);
            ProductoModelo productoModelo = lstProductos.FirstOrDefault(x => x.IdServicios == int.Parse(IdServicio));
            //HttpPostedFileBase file = Request.Files["ImageData"];
            //productoModelo.Imagen = ConvertToBytes(file);
            return View(Enumerador.NombreVista.GestionProducto.ToString(), productoModelo);
        }

        /// <summary>
        /// Accion y vista generica para Crear un nuevo producto y editar un nuevo producto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GestionProducto()
        {
            return View();

        }

        /// <summary>
        /// Envia la informacio al rest para procesarla y guardar en base de datos
        /// </summary>
        /// <param name="mProducto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RespuestaComun> ProcesarGestionProducto(ProductoModelo mProducto)
        {
            RespuestaComun respuestaComun = new RespuestaComun();

            //mProducto.TipoAccion = mHorario.TipoAccion == 0 ? (int)Enumerador.EnumTipoAccion.Insertar : mHorario.TipoAccion;
            mProducto.Estado = "A";
            object obj = mProducto;
            obj = _producto.ProcesarGestionProducto<object>(obj);


            //respuestaComun = EjecutarHttpRest.Deserializar<RespuestaComun>(EjecutarHttpRest.SerializarIdentado(obj));
            if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            {
                //respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarHorario.ObtenerDescripcion();
                //respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                //respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                //respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ToString();
                ViewBag.RespuestaListarAceptar = respuestaComun;

            }
            else if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
            {
                //respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                ViewBag.RespuestaErrorControlado = respuestaComun;

            }
            else
            {
                throw new Exception();
            }

            return respuestaComun;
        }

    }

}