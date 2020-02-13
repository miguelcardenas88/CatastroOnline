using Comun;
using Interfaz;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class Colaborador : IColaborador
    {
        public T ObtenerListaColaborador<T>(object objeto)
        {
            string strJson = VelbyRest.PostRest<T>("Usuario/ObtenerUsuarios", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        public async Task<T> ObtenerListaColaboradorAsync<T>(object objeto)
        {
            string strJson = await VelbyRest.PostRestAsync<T>("Usuario/ObtenerUsuarios", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        public async Task<T> ProcesarGestionColaborador<T>(object objeto)
        {
            string strJson = await VelbyRest.PostRestAsync<T>("Usuario/ProcesarGestionColaborador", objeto, Constantes.TOKEN);
            T respuesta = default(T); ;
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }
    }
}
