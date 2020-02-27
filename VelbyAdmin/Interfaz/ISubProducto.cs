using Comun.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface ISubProducto
    {
        T ObtenerListaSubProducto<T>(object objeto);

        Task<T> ObtenerListaSubProductoAsync<T>(object objeto);

        RespuestaComun GuardarFichaCatastro(modFicha oFicha, string accion);

        modFicha ObtenerFichaCatastro(modFicha oFicha, string accion);

        List<modFicha> ListaRegistroCatastro();
        void PostReportPartial();
    }
}
