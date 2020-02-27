using AutomaticFootControl.Helpers;
using AutomaticFootControl.Models.Producto;
using AutomaticFootControl.Validadores;
using Comun;
using Comun.Modelo;
using Interfaz;
using Newtonsoft.Json;
using Servicio;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutomaticFootControl.Controllers.Producto
{
    public class SubProductoController : Controller
    {
        private ISubProducto _subProducto;

        public SubProductoController()
        {
            _subProducto = new SubProducto();
        }
        /// <summary>
        /// Obtiene la lista de SubProductos registrados
        /// </summary>
        /// <param name="respuestaComun"></param>
        /// <returns></returns>
        [NoValidarSesionAplication]
        public ActionResult ListarSubProducto()
        {
            return PartialView();
        }

        /// <summary>
        /// Obtiene la lista de SubProductos registrados
        /// </summary>
        /// <param name="respuestaComun"></param>
        /// <returns></returns>
        [NoValidarSesionAplication]
        private async Task<vmoServicio> ObtenerListaSubProductoBase(RespuestaComun respuestaComun)
        {
            vmoServicio servicioVistaModelo = new vmoServicio();
            SubProductoModelo mSubProducto = new SubProductoModelo { Estado = "T" };
            servicioVistaModelo = await _subProducto.ObtenerListaSubProductoAsync<vmoServicio>(mSubProducto);

            if (servicioVistaModelo != null && servicioVistaModelo.Respuesta != null)
            {
                ViewBag.RespuestaErrorControlado = servicioVistaModelo.Respuesta;
            }
            Session["ListaSubProductos"] = servicioVistaModelo.LstSubProducto;
            return servicioVistaModelo;
        }


        public async Task<string> loaddata()
        {
            vmoServicio servicioVistaModelo = new vmoServicio();
            RespuestaComun respuestaComun = new RespuestaComun();
            servicioVistaModelo = await this.ObtenerListaSubProductoBase(respuestaComun);

            if (servicioVistaModelo.LstSubProducto == null)
                servicioVistaModelo.LstSubProducto = new ObservableCollection<SubProductoModelo>();


            foreach (SubProductoModelo subProducto in servicioVistaModelo.LstSubProducto)
            {
                subProducto.StrTiempoDuracionServicio = subProducto.TiempoDuracionServicio.ToString("hh:mm:ss");
            }

            var data = servicioVistaModelo.LstSubProducto.ToList();
            return JsonConvert.SerializeObject(new { data = data });
        }

        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="mSubProducto"></param>
        /// <returns></returns>
        //[EncryptedActionParameter]
        //[ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public ActionResult Nuevo(modFicha mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            //RespuestaComun respuesta;
            //vmoServicio servicioVistaModelo = new vmoServicio();
            //if (!string.IsNullOrEmpty(mSubProducto.CostoTemporal))
            //    mSubProducto.Costo = decimal.Parse(mSubProducto.CostoTemporal.Replace('.', ','));

            //if (!string.IsNullOrEmpty(mSubProducto.CostoTemporal) && !string.IsNullOrEmpty(mSubProducto.Descripcion)
            //    && !string.IsNullOrEmpty(mSubProducto.Nombre) && mSubProducto.TiempoDuracionServicio != null)
            ////&& !string.IsNullOrEmpty(mSubProducto.StrImagen))
            //{
            //    if (!string.IsNullOrEmpty(mSubProducto.StrImagen))
            //    {
            //        mSubProducto.StrImagen = UtilitarioFront.RedimencionarImagen(mSubProducto.StrImagen, 768);
            //    }


            //    respuesta = await _subProducto.ProcesarGestionSubProducto<RespuestaComun>(mSubProducto);
            //    if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            //    {
            //        servicioVistaModelo = await this.ObtenerListaSubProductoBase(null);
            //        return PartialView("ListarSubProducto", servicioVistaModelo.LstSubProducto);
            //    }
            //    else
            //    {
            //        //string strError = string.Format(Constantes.HTML_ERROR_GENERICO, respuesta.Codigo, respuesta.Mensaje, "Lo sentimos, por favor comunicate con SmartCode Solutions", "1800 000 000");
            //        ErrorModelo errorModelo = new ErrorModelo();
            //        string strError = respuesta.Mensaje + "|" + respuesta.Codigo;
            //        return RedirectToAction("Error", "Error", new { @error = strError });
            //    }
            //}


            //if (Session["ListaProductos"] == null)
            //{
            //    IProducto _producto = new Servicio.Producto();
            //    ProductoModelo mProducto = new ProductoModelo { Estado = "A" };
            //    servicioVistaModelo = await _producto.ObtenerListaProductoAsync<vmoServicio>(mProducto);
            //    Session["ListaProductos"] = servicioVistaModelo.LstProducto;
            //}
            //else
            //{
            //    servicioVistaModelo.LstProducto = (ObservableCollection<ProductoModelo>)Session["ListaProductos"];
            //}

            //List<SelectListItem> lstServicios = servicioVistaModelo.LstProducto.Select(elemento => new SelectListItem
            //{
            //    Text = string.Format("{0} - {1} ", elemento.Descripcion, elemento.Genero == "M" ? "Mujer" : "Hombre"),
            //    Value = elemento.IdServicios.ToString()
            //}).ToList();

            //mSubProducto.TipoAccion = Constantes.ACCION_INSERTAR;
            //mSubProducto.LstServicios = lstServicios;
            return PartialView(Enumerador.NombreVista.GestionSubProducto.ToString(), mFicha);
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> Guardar(modFicha mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta;
            vmoServicio servicioVistaModelo = new vmoServicio();


            respuesta = _subProducto.GuardarFichaCatastro(mFicha, "I");
            if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            {
                ViewBag.Mensaje = "Ficha guardada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al guardar la ficha";
                //ErrorModelo errorModelo = new ErrorModelo();
                //string strError = respuesta.Mensaje + "|" + respuesta.Codigo;
                //return RedirectToAction("Error", "Error", new { @error = strError });
            }
            return PartialView(Enumerador.NombreVista.GestionSubProducto.ToString(), new modFicha());
            //return PartialView("Nuevo", new modFicha());
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> ConsultarCatastro(modFicha mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();

            modFicha oFicha = _subProducto.ObtenerFichaCatastro(mFicha, "I");
            if (oFicha == null)
            {
                ViewBag.Mensaje = "Código no encontrado";
            }
            else
            {
                ViewBag.Mensaje = "Código encontrado";
            }
            return PartialView(Enumerador.NombreVista.GestionSubProducto.ToString(), oFicha);
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> ConsultarCatastroListado(modFicha mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();

            modFicha oFicha = _subProducto.ObtenerFichaCatastro(mFicha, "I");
            if (oFicha == null)
            {
                ViewBag.Mensaje = "Código no encontrado";
            }
            else
            {
                ViewBag.Mensaje = "Código encontrado";
            }
            return View(Enumerador.NombreVista.GestionSubProducto.ToString(), oFicha);
        }

        [NoValidarSesionAplication]
        public ActionResult ListaRegistroCatastro()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();

            List<modFicha> oFicha = _subProducto.ListaRegistroCatastro();
            if (oFicha == null)
            {
                ViewBag.Mensaje = "No existen registro";
            }
            else
            {
                //string pathFile = AppDomain.CurrentDomain.BaseDirectory + "miExcel.xlsx";
                SLDocument oSLDocument = new SLDocument();

                System.Data.DataTable dt = new System.Data.DataTable();

                //columnas
                dt.Columns.Add("CodigoUnico", typeof(string));
                dt.Columns.Add("CodigoCatastral", typeof(string));
                dt.Columns.Add("ClaveAnterior", typeof(string));
                dt.Columns.Add("TipoIdentificacion", typeof(string));
                dt.Columns.Add("TextoTipoIdentificacion", typeof(string));
                dt.Columns.Add("NumeroIdentificacion", typeof(string));
                dt.Columns.Add("NombrePropietario", typeof(string));
                dt.Columns.Add("PropietarioAnterior", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Barrio", typeof(string));
                dt.Columns.Add("UsoPredio", typeof(string));
                dt.Columns.Add("TextoUsoPredio", typeof(string));
                dt.Columns.Add("Escritura", typeof(string));
                dt.Columns.Add("TextoEscritura", typeof(string));
                dt.Columns.Add("Ocupacion", typeof(string));
                dt.Columns.Add("TextoOcupacion", typeof(string));
                dt.Columns.Add("Localizacion", typeof(string));
                dt.Columns.Add("TextoLocalizacion", typeof(string));
                dt.Columns.Add("NumeroPiso", typeof(string));
                dt.Columns.Add("Abastecimiento", typeof(string));
                dt.Columns.Add("TextoAbastecimiento", typeof(string));
                dt.Columns.Add("AguaRecib", typeof(string));
                dt.Columns.Add("TextoAguaRecib", typeof(string));
                dt.Columns.Add("Alcantarillado", typeof(string));
                dt.Columns.Add("TextoAlcantarillado", typeof(string));
                dt.Columns.Add("CodigoLocalizacion", typeof(string));
                dt.Columns.Add("TieneMedidor", typeof(string));
                dt.Columns.Add("UsuarioRegistro", typeof(string));
                dt.Columns.Add("Observacion", typeof(string));
                int i = 0;
                foreach (modFicha item in oFicha)
                {
                    i++;
                    //registros , rows
                    dt.Rows.Add(
                    item.CodigoUnico
                    , item.CodigoCatastral
                    , item.ClaveAnterior
                    , item.TipoIdentificacion
                    , item.TextoTipoIdentificacion
                    , item.NumeroIdentificacion
                    , item.NombrePropietario
                    , item.PropietarioAnterior
                    , item.Direccion
                    , item.Barrio
                    , item.UsoPredio
                    , item.TextoUsoPredio
                    , item.Escritura
                    , item.TextoEscritura
                    , item.Ocupacion
                    , item.TextoOcupacion
                    , item.Localizacion
                    , item.TextoLocalizacion
                    , item.NumeroPiso
                    , item.Abastecimiento
                    , item.TextoAbastecimiento
                    , item.AguaRecib
                    , item.TextoAguaRecib
                    , item.Alcantarillado
                    , item.TextoAlcantarillado
                    , item.CodigoLocalizacion
                    , item.TieneMedidor
                    , item.UsuarioRegistro
                    , item.Observacion);

                }



                oSLDocument.ImportDataTable(1, 1, dt, true);
                string handle = Guid.NewGuid().ToString();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    oSLDocument.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData["ArchivoExcel"] = memoryStream.ToArray();
                    //byte[] data = TempData[handle] as byte[];
                    //return File(data, "application/vnd.ms-Excel", "TestReportOutput.xlsx");
                }
                // oSLDocument.SaveAs(pathFile);
            }
            SubProductoModelo mRegistroCatastro = new SubProductoModelo();
            mRegistroCatastro.LstRegistrosCatastros = oFicha;
            return PartialView(Enumerador.NombreVista.ListaRegistroCatastro.ToString(), mRegistroCatastro);
        }
        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="IdServicio"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> Editar(string idSubServicio)
        {
            vmoServicio vServicio = new vmoServicio();
            if (Session["ListaSubProductos"] == null)
            {
                vServicio = await ObtenerListaSubProductoBase(new RespuestaComun());
            }
            else
            {
                vServicio.LstSubProducto = (ObservableCollection<SubProductoModelo>)Session["ListaSubProductos"];
            }
            SubProductoModelo subProductoModelo = new SubProductoModelo();
            for (int x = 0; x < vServicio.LstSubProducto.Count; x++)
            {
                if (int.Parse(idSubServicio) == vServicio.LstSubProducto[x].IdSubservicio)
                {
                    subProductoModelo = vServicio.LstSubProducto[x];
                    break;
                }
            }
            subProductoModelo.TipoAccion = Constantes.ACCION_MODIFICAR;

            if (Session["ListaProductos"] == null)
            {
                IProducto _producto = new Servicio.Producto();
                ProductoModelo mProducto = new ProductoModelo { Estado = "A" };
                vServicio = await _producto.ObtenerListaProductoAsync<vmoServicio>(mProducto);
                Session["ListaProductos"] = vServicio.LstProducto;
            }
            else
                vServicio.LstProducto = (ObservableCollection<ProductoModelo>)Session["ListaProductos"];

            subProductoModelo.LstServicios = vServicio.LstProducto.Select(elemento => new SelectListItem
            {
                Text = string.Format("{0} - {1} ", elemento.Descripcion, elemento.Genero == "M" ? "Mujer" : "Hombre"),
                Value = elemento.IdServicios.ToString()
            }).ToList(); ;

            return PartialView(Enumerador.NombreVista.GestionSubProducto.ToString(), subProductoModelo);
        }


        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="IdServicio"></param>
        /// <returns></returns>
        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> Eliminar(string codigo)
        {
            modFicha mFicha = new modFicha();
            mFicha.CodigoUnico = codigo;
            RespuestaComun respuesta = new RespuestaComun();//_subProducto.ProcesarGestionSubProducto(mFicha, "E");
            vmoServicio servicioVistaModelo = new vmoServicio();
            servicioVistaModelo = await this.ObtenerListaSubProductoBase(null);
            return PartialView("ListarSubProducto", servicioVistaModelo.LstSubProducto);
        }

        [NoValidarSesionAplication]
        public ActionResult PostReportPartial()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();
            string handle = string.Empty;
            List <modFicha> oFicha = _subProducto.ListaRegistroCatastro();
            if (oFicha == null)
            {
                ViewBag.Mensaje = "No existen registro";
            }
            else
            {
                //string pathFile = AppDomain.CurrentDomain.BaseDirectory + "miExcel.xlsx";
                SLDocument oSLDocument = new SLDocument();

                System.Data.DataTable dt = new System.Data.DataTable();

                //columnas
                dt.Columns.Add("CodigoUnico", typeof(string));
                dt.Columns.Add("CodigoCatastral", typeof(string));
                dt.Columns.Add("ClaveAnterior", typeof(string));
                dt.Columns.Add("TipoIdentificacion", typeof(string));
                dt.Columns.Add("TextoTipoIdentificacion", typeof(string));
                dt.Columns.Add("NumeroIdentificacion", typeof(string));
                dt.Columns.Add("NombrePropietario", typeof(string));
                dt.Columns.Add("PropietarioAnterior", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Barrio", typeof(string));
                dt.Columns.Add("UsoPredio", typeof(string));
                dt.Columns.Add("TextoUsoPredio", typeof(string));
                dt.Columns.Add("Escritura", typeof(string));
                dt.Columns.Add("TextoEscritura", typeof(string));
                dt.Columns.Add("Ocupacion", typeof(string));
                dt.Columns.Add("TextoOcupacion", typeof(string));
                dt.Columns.Add("Localizacion", typeof(string));
                dt.Columns.Add("TextoLocalizacion", typeof(string));
                dt.Columns.Add("NumeroPiso", typeof(string));
                dt.Columns.Add("Abastecimiento", typeof(string));
                dt.Columns.Add("TextoAbastecimiento", typeof(string));
                dt.Columns.Add("AguaRecib", typeof(string));
                dt.Columns.Add("TextoAguaRecib", typeof(string));
                dt.Columns.Add("Alcantarillado", typeof(string));
                dt.Columns.Add("TextoAlcantarillado", typeof(string));
                dt.Columns.Add("CodigoLocalizacion", typeof(string));
                dt.Columns.Add("TieneMedidor", typeof(string));
                dt.Columns.Add("UsuarioRegistro", typeof(string));
                dt.Columns.Add("Observacion", typeof(string));
           
                foreach (modFicha item in oFicha)
                {
                   
                    //registros , rows
                    dt.Rows.Add(
                    item.CodigoUnico
                    , item.CodigoCatastral
                    , item.ClaveAnterior
                    , item.TipoIdentificacion
                    , item.TextoTipoIdentificacion
                    , item.NumeroIdentificacion
                    , item.NombrePropietario
                    , item.PropietarioAnterior
                    , item.Direccion
                    , item.Barrio
                    , item.UsoPredio
                    , item.TextoUsoPredio
                    , item.Escritura
                    , item.TextoEscritura
                    , item.Ocupacion
                    , item.TextoOcupacion
                    , item.Localizacion
                    , item.TextoLocalizacion
                    , item.NumeroPiso
                    , item.Abastecimiento
                    , item.TextoAbastecimiento
                    , item.AguaRecib
                    , item.TextoAguaRecib
                    , item.Alcantarillado
                    , item.TextoAlcantarillado
                    , item.CodigoLocalizacion
                    , item.TieneMedidor
                    , item.UsuarioRegistro
                    , item.Observacion);
                  
                }



                oSLDocument.ImportDataTable(1, 1, dt, true);
                handle = Guid.NewGuid().ToString();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    oSLDocument.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData["DescargarArchivo"] = memoryStream.ToArray();
                    //byte[] data = TempData[handle] as byte[];
                    //return File(data, "application/vnd.ms-Excel", "TestReportOutput.xlsx");
                }
                // oSLDocument.SaveAs(pathFile);
              
            }
            return new JsonResult()
            {
                Data = new { FileGuid = handle, FileName = "TestReportOutput.xlsx" }
            };
        }

        [NoValidarSesionAplication]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-Excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
    }
}