using System.Web;
using System.Web.Mvc;
using AutomaticFootControl.Helpers;

namespace AutomaticFootControl.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidarSesion());
            filters.Add(new NoCacheControl());
        }
    }
}