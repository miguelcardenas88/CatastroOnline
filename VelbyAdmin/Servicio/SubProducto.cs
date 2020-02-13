using Comun;
using Comun.Modelo;
using Interfaz;
using Newtonsoft.Json;
using Servicio.Conexion;
using System;
using System.Collections.Generic;
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

        public RespuestaComun GuardarFichaCatastro(modFicha oFicha, string accion)
        {
            RespuestaComun respuesta = new RespuestaComun();
            string subProductosEmail = string.Empty;
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

                //string parametroDatoMetodo = Utilitario.SerializarIdentado(datosMetodo);
                string storeProcedure = string.Empty;
                if (accion == "I")
                    storeProcedure = "spi_ficha_catastro";
                else if (accion == "M")
                    storeProcedure = Enumerador.SpSubServicios.spu_sub_servicio.ObtenerDescripcion();
                else if (accion == "E")
                    storeProcedure = Enumerador.SpSubServicios.spd_sub_servicio.ObtenerDescripcion();

                respuesta = gestionConexiones.EjecutaStoreProcedure(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);

            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
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

    }
}
