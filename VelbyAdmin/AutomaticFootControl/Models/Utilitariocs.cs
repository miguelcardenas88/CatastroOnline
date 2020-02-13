using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Script.Serialization;

namespace AutomaticFootControl.Models
{
    public class Utilitariocs
    {
        public static LogModel PreparaGuardaLogsBase(Exception ex)
        {
            LogModel log = new LogModel();
            log.CodigoError = Constantes.CODIGO_ERROR_GENERICO;
            log.MensajeError = ex.Message;
            log.HelpLink = ex.HelpLink;
            log.HResult = ex.HResult;
            log.Source = ex.Source;
            log.StackTrace = ex.StackTrace;
            log.TargetSite = ex.TargetSite.Name;
            log.Descripcion = ex.Message;
            log.Fecha = DateTime.Now;
            log.Usuario = HttpContext.Current.Request.Url.Host;
            log.Url = HttpContext.Current.Request.Url.AbsoluteUri;
            log.Trace = HttpContext.Current.Request.Url.AbsolutePath;
            log.TipoError = 4;
            log.MensajeTipoError = Constantes.CODIGO_ERROR_GENERICO;

            JavaScriptSerializer js = new JavaScriptSerializer();
            return log;
        }
    }
}