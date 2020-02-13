using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IColaborador
    {
        T ObtenerListaColaborador<T>(object objeto);

        Task<T> ObtenerListaColaboradorAsync<T>(object objeto);

        Task<T> ProcesarGestionColaborador<T>(object objeto);
    }
}
