using Comun.Modelo;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IInicioSesion
    {
        T ValidarInicioSesion<T>(object objeto);

        Task<T> ValidarInicioSesionAsync<T>(object objeto);

        RespuestaComun CrearUsuario(UsuarioModelo oDatosUsuario, string accion);
    }
}
