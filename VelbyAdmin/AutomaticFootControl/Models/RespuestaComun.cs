using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models
{
    public class RespuestaComun : Modal
    {
        public string Codigo { get; set; }

        public string Mensaje { get; set; }

        public string MensajeInterno { get; set; }

        public int Tipo { get; set; }

        public bool Estado { get; set; }
    }
}