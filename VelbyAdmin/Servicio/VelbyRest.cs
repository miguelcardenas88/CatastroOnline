using Comun;
using Comun.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public static class VelbyRest
    {
        private static HttpClient oClienteHttp = new HttpClient();

        public static string PostRest<T>(string controladorAccion, object objeto, string token)
        {
            string strJson = string.Empty;
            string postCuerpo = string.Empty;
            using (HttpClient oClienteHttp = ConfigurarClienteHttp(token))
            {
                HttpResponseMessage oRespuesta = new HttpResponseMessage();
                postCuerpo = Utilitario.SerializarIdentado(objeto);

                try
                {
                    oRespuesta = oClienteHttp.PostAsync(controladorAccion, new StringContent(postCuerpo, Encoding.UTF8, "application/json")).Result;
                    oRespuesta = ManejarRespuesta(oRespuesta);
                    if (oRespuesta.IsSuccessStatusCode)
                    {
                        strJson = oRespuesta.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        throw new Exception(string.Format(CultureInfo.CurrentCulture, "{0} | {1} | {2}", oRespuesta.ReasonPhrase, oRespuesta.RequestMessage.ToString(), oRespuesta.StatusCode.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    oRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    oRespuesta.ReasonPhrase = ex.Message.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                    RespuestaComun respuesta = new RespuestaComun();
                    respuesta.Codigo = Constantes.CODIGO_ERROR_GENERICO;
                    respuesta.Mensaje = Constantes.MENSAJE_ERROR_GENERICO;
                    respuesta.MensajeInterno = Constantes.MENSAJE_ERROR_GENERICO;
                    strJson = Utilitario.SerializarIdentado(respuesta);

                    //LogModelo modLog = Utilitario.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, ObtieneDatosMetodo(controladorAccion, postCuerpo));
                    //oClienteHttp.PostAsync("Logs/GestionarLogException", new StringContent(Utilitario.SerializarIdentado(modLog), Encoding.UTF8, "application/json"));
                }
                return strJson;
            }
        }

        public static async Task<string> PostRestAsync<T>(string controladorAccion, object objeto, string token)
        {
            string strJson = string.Empty;
            using (HttpClient oClienteHttp = ConfigurarClienteHttp(token))
            {
                HttpResponseMessage oRespuesta = new HttpResponseMessage();
                string postCuerpo = Utilitario.SerializarIdentado(objeto);

                try
                {
                    oRespuesta = await oClienteHttp.PostAsync(controladorAccion, new StringContent(postCuerpo, Encoding.UTF8, "application/json"));
                    oRespuesta = ManejarRespuesta(oRespuesta);
                    if (oRespuesta.IsSuccessStatusCode)
                    {
                        strJson = oRespuesta.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        throw new Exception(string.Format(CultureInfo.CurrentCulture, "{0} | {1} | {2}", oRespuesta.ReasonPhrase, oRespuesta.RequestMessage.ToString(), oRespuesta.StatusCode.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    oRespuesta.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    oRespuesta.ReasonPhrase = ex.Message.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                    RespuestaComun respuesta = new RespuestaComun();
                    respuesta.Codigo = Constantes.CODIGO_ERROR_GENERICO;
                    respuesta.Mensaje = Constantes.MENSAJE_ERROR_GENERICO;
                    respuesta.MensajeInterno = Constantes.MENSAJE_ERROR_GENERICO;
                    strJson = Utilitario.SerializarIdentado(respuesta);

                    //LogModelo modLog = Utilitario.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, ObtieneDatosMetodo(controladorAccion, postCuerpo));
                    //await oClienteHttp.PostAsync("Logs/GestionarLogException", new StringContent(Utilitario.SerializarIdentado(modLog), Encoding.UTF8, "application/json"));
                }
                return strJson;
            }
        }

        private static HttpClient ConfigurarClienteHttp(string strToken)
        {
            string urlRest = Constantes.SERVIDOR_REST;
            HttpClient oClienteHttp = new HttpClient()
            {
                BaseAddress = new Uri(urlRest)
            };
            oClienteHttp.DefaultRequestHeaders.Accept.Clear();
            return oClienteHttp;
        }

        private static HttpResponseMessage ManejarRespuesta(HttpResponseMessage oRespuesta)
        {
            switch (oRespuesta.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    oRespuesta.ReasonPhrase = "No se pudo procesar la petición.";
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    oRespuesta.ReasonPhrase = "Intento de acceso no autorizado.";
                    break;
                case System.Net.HttpStatusCode.MethodNotAllowed:
                    oRespuesta.ReasonPhrase = "El método usado para la petición no es permitido.";
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    oRespuesta.ReasonPhrase = "El recurso solicitado no existe.";
                    break;
                case System.Net.HttpStatusCode.RequestEntityTooLarge:
                    oRespuesta.ReasonPhrase = "El tamaño de la petición es demasiado grande.";
                    break;
                case System.Net.HttpStatusCode.RequestTimeout:
                    oRespuesta.ReasonPhrase = "Tiempo de respuesta excedido.";
                    break;
                case System.Net.HttpStatusCode.RequestUriTooLong:
                    oRespuesta.ReasonPhrase = "Tamaño de dirección de petición demasiado largo.";
                    break;
                case System.Net.HttpStatusCode.ServiceUnavailable:
                    oRespuesta.ReasonPhrase = "Servicio no se encuentra disponible.";
                    break;
            }
            return oRespuesta;
        }

        //private static DatosMetodo ObtieneDatosMetodo(string metodoAccion, string postCuerpo)
        //{
        //    DatosMetodo datosMetodo = new DatosMetodo();
        //    datosMetodo.Ip = ObtieneIp();
        //    datosMetodo.NombreMetodo = metodoAccion;
        //    datosMetodo.Host = Dns.GetHostName();
        //    datosMetodo.Parametros = postCuerpo;
        //    return datosMetodo;
        //}

        private static string ObtieneIp()
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            string ipAddress = string.Empty;
            if (addresses != null && addresses[0] != null)
            {
                ipAddress = addresses[0].ToString();
            }
            else
            {
                ipAddress = null;
            }
            return ipAddress;
        }
    }
}
