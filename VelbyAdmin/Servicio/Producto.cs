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
    public class Producto : IProducto
    {

        public T ObtenerListaProducto<T>(object objeto)
        {
            string strJson = VelbyRest.PostRest<T>("Servicio/ObtenerServicio", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        public async Task<T> ObtenerListaProductoAsync<T>(object objeto)
        {
            string strJson = await VelbyRest.PostRestAsync<T>("Servicio/ObtenerServicio", objeto, Constantes.TOKEN);
            T respuesta = default(T);
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }

        public async Task<T> ProcesarGestionProducto<T>(object objeto)
        {
            string strJson = await VelbyRest.PostRestAsync<T>("Servicio/ProcesarGestionServicio", objeto, Constantes.TOKEN);
            T respuesta = default(T); ;
            respuesta = JsonConvert.DeserializeObject<T>(strJson);
            return respuesta;
        }
    }
}
