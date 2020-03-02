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

        [NoValidarSesionAplication]
        public async Task<ActionResult> ListaRegistroCatastro()
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
            modFicha mRegistroCatastro = new modFicha();
            mRegistroCatastro.LstRegistrosCatastros = oFicha;
            mRegistroCatastro.consultaDesdeListado = "Y";
            return PartialView(Enumerador.NombreVista.GestionSubProducto.ToString(), mRegistroCatastro);
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


        /// <summary>
        /// Accion para validacion de modelo y redireccionamiento al procesamiento de datos o de vuelta al formulario
        /// </summary>
        /// <param name="mSubProducto"></param>
        /// <returns></returns>
        //[EncryptedActionParameter]
        //[ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public ActionResult RegistroCatastro(FichaCatastroModel mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            return PartialView(Enumerador.NombreVista.RegistroCatastro.ToString(), mFicha);
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> ConsultarRegistroCatastro(FichaCatastroModel mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();

            FichaCatastroModel oFicha = _subProducto.ConsultarFichaCatastro(mFicha, "I");
            if (oFicha != null)
            {
                ViewBag.Mensaje = "Registro Encontrado";
            }
            else
            {
                ViewBag.Mensaje = "Registro no encontrado";
            }
            return PartialView(Enumerador.NombreVista.RegistroCatastro.ToString(), oFicha);
        }

        [EncryptedActionParameter]
        [ValidateAntiForgeryToken]
        [NoValidarSesionAplication]
        public async Task<ActionResult> RegistarFicha(FichaCatastroModel mFicha)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta;
            vmoServicio servicioVistaModelo = new vmoServicio();


            respuesta = _subProducto.RegistrarFichaCatastro(mFicha, "I");
            if (respuesta.Codigo == Constantes.RESPUESTA_CODIGO_OK)
            {
                ViewBag.Mensaje = "Ficha guardada correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al guardar la ficha";
            }
            return PartialView(Enumerador.NombreVista.RegistroCatastro.ToString(), new FichaCatastroModel());
        }

        [NoValidarSesionAplication]
        public async Task<ActionResult> ListadoRegistroCatastro()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();

            List<FichaCatastroModel> oFicha = _subProducto.ListadoRegistroCatastro();
            if (oFicha == null)
            {
                ViewBag.Mensaje = "No existen registro";
            }
            else
            {
                SLDocument oSLDocument = new SLDocument();

                System.Data.DataTable dt = new System.Data.DataTable();

                //columnas
                dt.Columns.Add("CodigoCatastral", typeof(string));
                dt.Columns.Add("ClaveAnterior", typeof(string));
                dt.Columns.Add("TipoIdentificacion", typeof(string));
                dt.Columns.Add("TextoTipoIdentificacion", typeof(string));
                dt.Columns.Add("NumeroIdentificacion", typeof(string));
                dt.Columns.Add("NombrePropietario", typeof(string));

                int i = 0;
                foreach (FichaCatastroModel item in oFicha)
                {
                    i++;
                    dt.Rows.Add(
                     item.CodigoCatastral
                    , item.ClaveAnterior
                    , item.NumeroIdentificacion
                    , item.NombrePropietario
                    , item.PropietarioAnterior
                    , item.Direccion
);                }



                oSLDocument.ImportDataTable(1, 1, dt, true);
                string handle = Guid.NewGuid().ToString();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    oSLDocument.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData["ArchivoExcel"] = memoryStream.ToArray();
                }
            }
            FichaCatastroModel mRegistroCatastro = new FichaCatastroModel();
            mRegistroCatastro.LstRegistrosCatastros = oFicha;
            return PartialView(Enumerador.NombreVista.ListadoRegistroCatastro.ToString(), mRegistroCatastro);
        }

        [NoValidarSesionAplication]
        public ActionResult PostReportPartialCatastro()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-EC");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-EC");
            RespuestaComun respuesta = new RespuestaComun();
            string handle = string.Empty;
            List<FichaCatastroModel> oFicha = _subProducto.ListadoRegistroCatastro();
            if (oFicha == null)
            {
                ViewBag.Mensaje = "No existen registro";
            }
            else
            {
                SLDocument oSLDocument = new SLDocument();
                System.Data.DataTable dt = new System.Data.DataTable();

                //columnas
                dt.Columns.Add("AÑO EMISION", typeof(string));
                dt.Columns.Add("PARROQUIA", typeof(string));
                dt.Columns.Add("FECHA REGISTRO", typeof(string));
                dt.Columns.Add("FECHA EMISION", typeof(string));
                dt.Columns.Add("FECHA VENCIMIENTO", typeof(string));
                dt.Columns.Add("REFERENCIA", typeof(string));
                dt.Columns.Add("PROPIETARIO POSEEDOR", typeof(string));
                dt.Columns.Add("CI RUC", typeof(string));
                dt.Columns.Add("TITULO", typeof(string));
                dt.Columns.Add("ORIGEN", typeof(string));
                dt.Columns.Add("ESTADO ACTUAL DE LA CUENTA", typeof(string));
                dt.Columns.Add("FECHA BAJ MOD PAG", typeof(string));
                dt.Columns.Add("TOTAL RUBROS INICIAL", typeof(string));
                dt.Columns.Add("TOTAL RUBROS ACTUAL", typeof(string));
                dt.Columns.Add("DIFERENCIAS", typeof(string));
                dt.Columns.Add("PRIMERA EMISION", typeof(string));
                dt.Columns.Add("CLAVE ANTERIOR", typeof(string));
                dt.Columns.Add("PROPIETARIO ANTERIOR", typeof(string));
                dt.Columns.Add("DIRECCIÓN", typeof(string));
                dt.Columns.Add("SECTOR", typeof(string));
                dt.Columns.Add("CATEGORIA", typeof(string));
                dt.Columns.Add("MES CONSUMO", typeof(string));
                dt.Columns.Add("NÚMERO MEDIDOR", typeof(string));
                dt.Columns.Add("METROS CUBICOS", typeof(string));
                dt.Columns.Add("RANGO", typeof(string));
                dt.Columns.Add("OBSERVACIÓN", typeof(string));
                dt.Columns.Add("PORCENTAJE DEDUCCIÓN", typeof(string));
                dt.Columns.Add("CONSUMO DE AGUA POTABLE", typeof(string));
                dt.Columns.Add("RECOLECCION DE BASURA", typeof(string));
                dt.Columns.Add("TASA DE SERVICIO ADMINISTRATIVO", typeof(string));
                dt.Columns.Add("TOTAL RUBROS ACTUAL1", typeof(string));

                foreach (FichaCatastroModel item in oFicha)
                {

                    //registros , rows
                    dt.Rows.Add(
                    "2020" // Año Emision
                    , item.Parroquia
                    , DateTime.Now //fecha registro
                    , item.FechaEmision
                    , item.FechaVencimieto
                    , "Referencia" // Referencia
                    , item.NombrePropietario
                    , item.NumeroIdentificacion
                    , "TITULO" // titulo
                    , "ARCHIVO" // origen 
                    , "PEN" // Estado Actual de la cuenta 
                    , "" // fecha baj mod pag.
                    , "3.4" // total rubros inicial
                    , "3.4" // total rubros actual
                    , "" // diferencias
                    , "f" // primera emision
                    , item.ClaveAnterior
                    , item.PropietarioAnterior
                    , item.Direccion
                    , item.Sector
                    , item.Categoria
                    , "MARZO" // mes consumo 
                    , item.NumeroMedidor
                    , item.MetrosCubicoConsumo
                    , item.Rango
                    , item.Observaciones
                    , item.Deducciones
                    , "2"// consumo de agua potable
                    , "1"// recoleccion de basura
                    , "0.4" // tasa de servicio administrativo
                    ,"3.4" //tptal rubors actual
                    );
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
                Data = new { FileGuid = handle, FileName = "ReporteSinat.xlsx" }
            };
        }

        [NoValidarSesionAplication]
        public virtual ActionResult DownloadReporteSinat(string fileGuid, string fileName)
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