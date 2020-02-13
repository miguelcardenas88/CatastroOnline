using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Modelo
{
    public class vmoFichaCatastro
    {
        public ObservableCollection<modFicha> LstFichaCatastro { get; set; }
        public RespuestaComun Respuesta { get; set; }
    }
}
