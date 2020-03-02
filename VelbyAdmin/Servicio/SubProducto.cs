using Comun;
using Comun.Modelo;
using Interfaz;
using Newtonsoft.Json;
using Servicio.Conexion;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicio
{
    public class SubProducto : ISubProducto
    {
        private GestionConexiones gestionConexiones;

        public SubProducto()
        {
            gestionConexiones = new GestionConexiones();
        }

        public T ObtenerListaSubProducto<T>(object objeto)
        {
            string strJson = VelbyRest.PostRest<T>("Servicio/ObtenerSubServicio", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        public async Task<T> ObtenerListaSubProductoAsync<T>(object objeto)
        {
            string strJson = await VelbyRest.PostRestAsync<T>("Servicio/ObtenerSubServicio", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        // Servicio.SubProducto
        public RespuestaComun GuardarFichaCatastro(modFicha oFicha, string accion)
        {
            RespuestaComun respuesta = new RespuestaComun();
            string arg_0B_0 = string.Empty;
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("CodigoUnico", oFicha.CodigoUnico);
                parametros.Add("CodigoCatastral", oFicha.CodigoCatastral);
                parametros.Add("ClaveAnterior", oFicha.ClaveAnterior);
                parametros.Add("TipoIdentificacion", oFicha.TipoIdentificacion);
                parametros.Add("TextoTipoIdentificacion", oFicha.TextoTipoIdentificacion);
                parametros.Add("NumeroIdentificacion", oFicha.NumeroIdentificacion);
                parametros.Add("NombrePropietario", oFicha.NombrePropietario);
                parametros.Add("PropietarioAnterior", oFicha.PropietarioAnterior);
                parametros.Add("Direccion", oFicha.Direccion);
                parametros.Add("Barrio", oFicha.Barrio);
                parametros.Add("UsoPredio", oFicha.UsoPredio);
                parametros.Add("TextoUsoPredio", oFicha.TextoUsoPredio);
                parametros.Add("Escritura", oFicha.Escritura);
                parametros.Add("TextoEscritura", oFicha.TextoEscritura);
                parametros.Add("Ocupacion", oFicha.Ocupacion);
                parametros.Add("TextoOcupacion", oFicha.TextoOcupacion);
                parametros.Add("Localizacion", oFicha.Localizacion);
                parametros.Add("TextoLocalizacion", oFicha.TextoLocalizacion);
                parametros.Add("NumeroPiso", oFicha.NumeroPiso);
                parametros.Add("Abastecimiento", oFicha.Abastecimiento);
                parametros.Add("TextoAbastecimiento", oFicha.TextoAbastecimiento);
                parametros.Add("AguaRecib", oFicha.AguaRecib);
                parametros.Add("TextoAguaRecib", oFicha.TextoAguaRecib);
                parametros.Add("Alcantarillado", oFicha.Alcantarillado);
                parametros.Add("TextoAlcantarillado", oFicha.TextoAlcantarillado);
                parametros.Add("CodigoLocalizacion", oFicha.CodigoLocalizacion);
                parametros.Add("TieneMedidor", oFicha.TieneMedidor);
                parametros.Add("UsuarioRegistro", oFicha.UsuarioRegistro);
                parametros.Add("Observacion", oFicha.Observacion);
                string storeProcedure = string.Empty;
                if (accion == "I")
                {
                    storeProcedure = "spi_ficha_catastro";
                }
                else if (accion == "M")
                {
                    storeProcedure = Enumerador.SpSubServicios.spu_sub_servicio.ObtenerDescripcion();
                }
                else if (accion == "E")
                {
                    storeProcedure = Enumerador.SpSubServicios.spd_sub_servicio.ObtenerDescripcion();
                }
                respuesta = this.gestionConexiones.EjecutaStoreProcedure(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);
            }
            catch (Exception)
            {
            }
            return respuesta;
        }

        public modFicha ObtenerFichaCatastro(modFicha oFicha, string accion)
        {
            string subProductosEmail = string.Empty;
            List<Dictionary<string, string>> respuesta = new List<Dictionary<string, string>>();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "CodigoUnico", oFicha.CodigoUnico },
                    { "CodigoCatastral", oFicha.CodigoCatastral }
                };

                //string parametroDatoMetodo = Utilitario.SerializarIdentado(datosMetodo);
                string storeProcedure = "sps_ficha_catastro_x_codigo";
                respuesta = gestionConexiones.EjecutaStoreProcedureConsulta(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);
                if (respuesta.Count >= 1)
                {
                    string respuestaStr = Utilitario.SerializarIdentado(respuesta[0]);
                    oFicha = Utilitario.Deserializar<modFicha>(respuestaStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return oFicha;
        }


        public List<modFicha> ListaRegistroCatastro()
        {
            string subProductosEmail = string.Empty;
            List<Dictionary<string, string>> respuesta = new List<Dictionary<string, string>>();
            List<modFicha> oFicha = null;
            //string pathFile = AppDomain.CurrentDomain.BaseDirectory + "miExcel.xlsx";
            try
            {

                string storeProcedure = "sps_fichas_catastro";
                respuesta = gestionConexiones.EjecutaStoreProcedureConsulta(null, string.Empty, storeProcedure);

                if (respuesta.Count >= 1)
                {
                    string respuestaStr = Utilitario.SerializarIdentado(respuesta);
                    oFicha = Utilitario.Deserializar<List<modFicha>>(respuestaStr);

                    //string pathFile = AppDomain.CurrentDomain.BaseDirectory + "miExcel.xlsx";
                    //SLDocument oSLDocument = new SLDocument();

                    //System.Data.DataTable dt = new System.Data.DataTable();

                    ////columnas
                    //dt.Columns.Add("CodigoUnico", typeof(string));
                    //dt.Columns.Add("CodigoCatastral", typeof(string));
                    //dt.Columns.Add("ClaveAnterior", typeof(string));
                    //dt.Columns.Add("TipoIdentificacion", typeof(string));
                    //dt.Columns.Add("TextoTipoIdentificacion", typeof(string));
                    //dt.Columns.Add("NumeroIdentificacion", typeof(string));
                    //dt.Columns.Add("NombrePropietario", typeof(string));
                    //dt.Columns.Add("PropietarioAnterior", typeof(string));
                    //dt.Columns.Add("Direccion", typeof(string));
                    //dt.Columns.Add("Barrio", typeof(string));
                    //dt.Columns.Add("UsoPredio", typeof(string));
                    //dt.Columns.Add("TextoUsoPredio", typeof(string));
                    //dt.Columns.Add("Escritura", typeof(string));
                    //dt.Columns.Add("TextoEscritura", typeof(string));
                    //dt.Columns.Add("Ocupacion", typeof(string));
                    //dt.Columns.Add("TextoOcupacion", typeof(string));
                    //dt.Columns.Add("Localizacion", typeof(string));
                    //dt.Columns.Add("TextoLocalizacion", typeof(string));
                    //dt.Columns.Add("NumeroPiso", typeof(string));
                    //dt.Columns.Add("Abastecimiento", typeof(string));
                    //dt.Columns.Add("TextoAbastecimiento", typeof(string));
                    //dt.Columns.Add("AguaRecib", typeof(string));
                    //dt.Columns.Add("TextoAguaRecib", typeof(string));
                    //dt.Columns.Add("Alcantarillado", typeof(string));
                    //dt.Columns.Add("TextoAlcantarillado", typeof(string));
                    //dt.Columns.Add("CodigoLocalizacion", typeof(string));
                    //dt.Columns.Add("TieneMedidor", typeof(string));
                    //dt.Columns.Add("UsuarioRegistro", typeof(string));
                    //dt.Columns.Add("Observacion", typeof(string));
                    //int i = 0;
                    //foreach (modFicha item in oFicha)
                    //{
                    //    i++;
                    //    //registros , rows
                    //    dt.Rows.Add(
                    //    item.CodigoUnico
                    //    , item.CodigoCatastral
                    //    , item.ClaveAnterior
                    //    , item.TipoIdentificacion
                    //    , item.TextoTipoIdentificacion
                    //    , item.NumeroIdentificacion
                    //    , item.NombrePropietario
                    //    , item.PropietarioAnterior
                    //    , item.Direccion
                    //    , item.Barrio
                    //    , item.UsoPredio
                    //    , item.TextoUsoPredio
                    //    , item.Escritura
                    //    , item.TextoEscritura
                    //    , item.Ocupacion
                    //    , item.TextoOcupacion
                    //    , item.Localizacion
                    //    , item.TextoLocalizacion
                    //    , item.NumeroPiso
                    //    , item.Abastecimiento
                    //    , item.TextoAbastecimiento
                    //    , item.AguaRecib
                    //    , item.TextoAguaRecib
                    //    , item.Alcantarillado
                    //    , item.TextoAlcantarillado
                    //    , item.CodigoLocalizacion
                    //    , item.TieneMedidor
                    //    , item.UsuarioRegistro
                    //    , item.Observacion);

                    //}



                    //oSLDocument.ImportDataTable(1, 1, dt, true);

                    //oSLDocument.SaveAs(pathFile);

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return oFicha;
        }
        public void PostReportPartial()
        {

            // Validate the Model is correct and contains valid data
            // Generate your report output based on the model parameters
            // This can be an Excel, PDF, Word file - whatever you need.

            // As an example lets assume we've generated an EPPlus ExcelPackage

            SLDocument workbook = new SLDocument();
            // Do something to populate your workbook

            // Generate a new unique identifier against which the file can be stored
            string handle = Guid.NewGuid().ToString();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.Position = 0;
               var temp = memoryStream.ToArray();
            }
            //return new JsonResult()
            //{
            //    Data = new { FileGuid = handle, FileName = "TestReportOutput.xlsx" }
            //};

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oFicha"></param>
        /// <param name="accion"></param>
        /// <returns></returns>
        public FichaCatastroModel ConsultarFichaCatastro(FichaCatastroModel oFicha, string accion)
        {
            string subProductosEmail = string.Empty;
            List<Dictionary<string, string>> respuesta = new List<Dictionary<string, string>>();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "CodigoCatastral", oFicha.CodigoCatastral }
                };

                string storeProcedure = "sps_ficha_catastro_x_codigo_catastro";
                respuesta = gestionConexiones.EjecutaStoreProcedureConsulta(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);
                if (respuesta.Count >= 1)
                {
                    string respuestaStr = Utilitario.SerializarIdentado(respuesta[0]);
                    oFicha = Utilitario.Deserializar<FichaCatastroModel>(respuestaStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return oFicha;
        }

        public RespuestaComun RegistrarFichaCatastro(FichaCatastroModel oFicha, string accion)
        {
            RespuestaComun respuesta = new RespuestaComun();
            string arg_0B_0 = string.Empty;
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("parroquia", oFicha.Parroquia);
                parametros.Add("referencia", oFicha.CodigoCatastral);
                parametros.Add("clave_anterior", oFicha.ClaveAnterior);
                parametros.Add("documento_identidad", oFicha.NumeroIdentificacion);
                parametros.Add("propietario", oFicha.NombrePropietario);
                parametros.Add("propietario_anterior", oFicha.PropietarioAnterior);
                parametros.Add("direccion", oFicha.Direccion);
                parametros.Add("sector", oFicha.Sector);
                parametros.Add("categoria", oFicha.Categoria);
                parametros.Add("mes_consumo", oFicha.MesConsumo);
                parametros.Add("numero_medidor", oFicha.NumeroMedidor);
                parametros.Add("metros_cubicos_consumo", oFicha.MetrosCubicoConsumo);
                parametros.Add("rango", oFicha.Rango);
                parametros.Add("observaciones", oFicha.Observaciones);
                parametros.Add("deducciones", oFicha.Deducciones);
                parametros.Add("tarifa", oFicha.TarifaFija);
                //parametros.Add("fecha_emision", oFicha.FechaEmision);
                //parametros.Add("fecha_vencimiento", oFicha.FechaVencimieto);
                string storeProcedure = string.Empty;
                if (accion == "I")
                {
                    storeProcedure = "spi_registra_ficha_catastro";
                }
                else if (accion == "M")
                {
                    storeProcedure = Enumerador.SpSubServicios.spu_sub_servicio.ObtenerDescripcion();
                }
                else if (accion == "E")
                {
                    storeProcedure = Enumerador.SpSubServicios.spd_sub_servicio.ObtenerDescripcion();
                }
                respuesta = this.gestionConexiones.EjecutaStoreProcedure(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);
            }
            catch (Exception)
            {
            }
            return respuesta;
        }

        public List<FichaCatastroModel> ListadoRegistroCatastro()
        {
            string subProductosEmail = string.Empty;
            List<Dictionary<string, string>> respuesta = new List<Dictionary<string, string>>();
            List<FichaCatastroModel> oFicha = null;
            try
            {

                string storeProcedure = "sps_listado_fichas_catastro";
                respuesta = gestionConexiones.EjecutaStoreProcedureConsulta(null, string.Empty, storeProcedure);

                if (respuesta.Count >= 1)
                {
                    string respuestaStr = Utilitario.SerializarIdentado(respuesta);
                    oFicha = Utilitario.Deserializar<List<FichaCatastroModel>>(respuestaStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return oFicha;
        }
    }
}
