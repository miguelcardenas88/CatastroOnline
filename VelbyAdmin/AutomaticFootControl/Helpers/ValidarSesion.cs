using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ValidarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionDescriptor.IsDefined(typeof(NoValidarSesionAplication), true)
                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NoValidarSesionAplication), true))
                    return;

                if (filterContext.RequestContext.HttpContext.Session.Count == 0
                    || filterContext.RequestContext.HttpContext.Session["UsuarioLogueado"] == null
                    || !(bool)filterContext.RequestContext.HttpContext.Session["UsuarioLogueado"]
                    )
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                    {
                        Controller = "InicioSesion",
                        Action = "InicioSesion"
                    }));
                    base.OnActionExecuting(filterContext);
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    Controller = "InicioSesion",
                    Action = "InicioSesion"
                }));
                base.OnActionExecuting(filterContext);
            }
        }
    }
}