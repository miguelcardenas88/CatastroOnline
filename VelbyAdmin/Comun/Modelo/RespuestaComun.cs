using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Modelo
{
    [Serializable]
    public class RespuestaComun
    {
        [JsonConstructor]
        public RespuestaComun()
        {

        }
        public string Codigo { get; set; }

        public string Mensaje { get; set; }

        public string MensajeInterno { get; set; }

        public int Tipo { get; set; }

        public bool Estado { get; set; }

        public int FilasAfectadas { get; set; }

        public int TipoDatoRespuesta { get; set; }

        public string ValorRetorno { get; set; }
    }
}
