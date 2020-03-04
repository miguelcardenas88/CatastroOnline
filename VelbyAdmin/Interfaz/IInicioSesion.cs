using Comun.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IInicioSesion
    {
        T ValidarInicioSesion<T>(object objeto);

       UsuarioModelo ValidarInicioSesionAsync<T>(object objeto);

        RespuestaComun CrearUsuario(UsuarioModelo oDatosUsuario, string accion);

        void EnviarEmailAsync(List<EmailNotificacionModelo> lstEmailNotificacionModelo, Dictionary<string, string> datosPlantilla);

    }
}
