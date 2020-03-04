using Comun;
using Comun.Modelo;
using Interfaz;
using Servicio.Conexion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Servicio
{
    public class InicioSesion : IInicioSesion
    {
        private GestionConexiones gestionConexiones;

        public InicioSesion()
        {
            gestionConexiones = new GestionConexiones();
        }

        public T ValidarInicioSesion<T>(object objeto)
        {
            string respuestaJson = VelbyRest.PostRest<T>("Usuario/InicioSesion", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = Utilitario.Deserializar<T>(respuestaJson);
            return respuesta;
        }

        public UsuarioModelo ValidarInicioSesionAsync<T>(object objeto)
        {
            List<Dictionary<string, string>> respuesta = new List<Dictionary<string, string>>();
            UsuarioModelo usuario = new UsuarioModelo();
            usuario = objeto as UsuarioModelo;
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "us_correo_electronico", usuario.Correo },
                    { "us_password", usuario.Contrasenia}
                };

                string storeProcedure = "sps_consulta_usuario_catastro";
                respuesta = gestionConexiones.EjecutaStoreProcedureConsulta(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);
                if (respuesta.Count >= 1)
                {
                    string respuestaStr = Utilitario.SerializarIdentado(respuesta[0]);
                    return Utilitario.Deserializar<UsuarioModelo>(respuestaStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return null;
        }


        public void EnviarEmailAsync(List<EmailNotificacionModelo> lstEmailNotificacionModelo, Dictionary<string, string> datosPlantilla)
        {
            try
            {
                //if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings[Constantes.HABILITAR_ENVIO_MAIL]))
                //{
                    if (lstEmailNotificacionModelo != null && lstEmailNotificacionModelo.Count > 0)
                    {
                        String BODY;
                        SmtpClient SmtpServer;
                        MailMessage mail;
                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                        path = path.Substring(6);
                        foreach (EmailNotificacionModelo enviarMail in lstEmailNotificacionModelo)
                        {
                            string cuerpoMensaje = path + enviarMail.CueporMensaje;
                            BODY = cuerpoPlantilla(datosPlantilla, cuerpoMensaje);
                            SmtpServer = new SmtpClient(Constantes.SMTP_SERVER);
                            mail = new MailMessage();
                            mail.From = new MailAddress(Constantes.MAIL_ENVIO, Constantes.NOMBRE_PANTALLA);
                            mail.To.Add(enviarMail.CorreoDestino);
                            mail.Subject = enviarMail.Asunto;
                            mail.IsBodyHtml = true;
                            mail.Body = BODY;
                            SmtpServer.Port = Constantes.PUERTO_EMAIL;
                            SmtpServer.UseDefaultCredentials = false;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(Constantes.MAIL_ENVIO, Constantes.CONTRASENIA_MAIL);
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                //conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, new VelbyRestModelo.Log.DatosMetodo());
            }
        }

        private string cuerpoPlantilla(Dictionary<string, string> datosPlantilla, string direccionPlantilla)
        {
            string plantilla = string.Empty;
            if (datosPlantilla == null)
            {
                return File.ReadAllText(direccionPlantilla);
            }
            else
            {
                //using streamreader for reading my htmltemplate   
                using (StreamReader reader = new StreamReader(direccionPlantilla))
                {
                    plantilla = reader.ReadToEnd();
                }

                foreach (KeyValuePair<string, string> dato in datosPlantilla)
                {
                    plantilla = plantilla.Replace(dato.Key, dato.Value); //replacing the required things  
                }
                return plantilla;
            }
        }


        public RespuestaComun CrearUsuario(UsuarioModelo oDatosUsuario, string accion)
        {

            RespuestaComun respuesta = new RespuestaComun();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
               

          { "us_nombre",oDatosUsuario.Nombres },
          { "us_apellido",oDatosUsuario.Apellidos },
          { "us_numero_identificacion","1722993324" },
          { "us_usuario","cardenasma" },
          { "us_correo_electronico",oDatosUsuario.Correo },
          { "us_password",oDatosUsuario.Contrasenia }, };

                //string parametroDatoMetodo = Utilitario.SerializarIdentado(datosMetodo);
                string storeProcedure = string.Empty;
                if (accion == "I")
                {
                    storeProcedure = "spi_crea_usuario_catastro";
                }
                else if (accion == "M")
                {
                    storeProcedure = Enumerador.SpSubServicios.spu_sub_servicio.ObtenerDescripcion();
                }
                else if (accion == "E")
                {
                    storeProcedure = Enumerador.SpSubServicios.spd_sub_servicio.ObtenerDescripcion();
                }
                respuesta = gestionConexiones.EjecutaStoreProcedure(Utilitario.SerializarIdentado(parametros), string.Empty, storeProcedure);

            }
            catch (Exception ex)
            {
                //respuesta = conexionGestion.AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, datosMetodo);
            }
            return respuesta;
        }

    }
}
