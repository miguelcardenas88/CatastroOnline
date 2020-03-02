using Comun;
using Comun.Modelo;
using Interfaz;
using Servicio.Conexion;
using System;
using System.Collections.Generic;
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

        public async Task<T> ValidarInicioSesionAsync<T>(object objeto)
        {
            string respuestaJson = await VelbyRest.PostRestAsync<T>("Usuario/InicioSesion", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = Utilitario.Deserializar<T>(respuestaJson);
            return respuesta;
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
