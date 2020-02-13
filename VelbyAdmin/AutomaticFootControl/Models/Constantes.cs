using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models
{
    public class Constantes
    {
        public const string CAMPO_REQUERIDO = "Campo Requerido";
        public const string RESPUESTA_CODIGO_OK = "10000";
        public const string HTML_ERROR_GENERICO = "<div class='row'> " +
                        "<div class='col-md-12 my-5 text-center'> " +
                        "	<div class='text-danger'> " +
                        "		<i class='batch-icon batch-icon-link-alt batch-icon-xxl'></i> " +
                        "		<i class='batch-icon batch-icon-search batch-icon-xxl'></i>  " +
                        "		<i class='batch-icon batch-icon-link-alt batch-icon-xxl'></i>  " +
                        "	</div> " +
                        "	<h4 class='display-5'>{0}</h4> " + //404
                        "	<div class='lead mb-3'>{1}</div> " +  //Page Not Found
                        "	<div class='lead'>{2}</div> " + //We can't find the page you are looking for.
                        "   </div> " +
                        "</div>";
        public const string CODIGO_ERROR_GENERICO = "99999";
        public const string MENSAJE_ERROR_GENERICO = "Se ha producido un error";
        public const string MENSAJE_CONTACTO_ERROR_GENERICO = "Por favor comuníquese con nosotros al 1800-00-00-00.";
        public string MENSAJE_MOSTRAR_ERROR_GENERICO = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO,
            Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
        public const string FORMATO_FECHA_TABLA_DDMMYYY = "{0:dd/MM/yyyy}";
        public const string FORMATO_HORA_TABLA_HHMM = "{0:HH:mm}";
        public const string FORMATO_FECHA_FORM_DDMMYYY = "dd/MM/yyyy";
        public const string FORMATO_HORA_FORM_HHMM = "HH:MM";
    }
}