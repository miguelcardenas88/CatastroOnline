using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IInicioSesion
    {
        T ValidarInicioSesion<T>(object objeto);

        Task<T> ValidarInicioSesionAsync<T>(object objeto);
    }
}
