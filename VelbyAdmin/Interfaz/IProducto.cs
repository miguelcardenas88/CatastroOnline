using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IProducto
    {
        T ObtenerListaProducto<T>(object objeto);

        Task<T> ObtenerListaProductoAsync<T>(object objeto);

        Task<T> ProcesarGestionProducto<T>(object objeto);
    }
}
