using Comun;
using Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class InicioSesion : IInicioSesion
    {
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
    }
}
