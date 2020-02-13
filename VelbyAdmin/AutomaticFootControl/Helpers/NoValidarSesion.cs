using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NoValidarSesion : Attribute { }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NoValidarSesionAplication : Attribute { }
}