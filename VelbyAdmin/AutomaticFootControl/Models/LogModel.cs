using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models
{
    public class LogModel
    {
        public int Id { get; set; }

        public string CodigoError { get; set; }

        public string MensajeError { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        public string Url { get; set; }

        public string Trace { get; set; }

        public int TipoError { get; set; }

        public string HelpLink { get; set; }

        public int HResult { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public string TargetSite { get; set; }

        public string MensajeTipoError { get; set; }

        public string Parametro { get; set; }
    }
}