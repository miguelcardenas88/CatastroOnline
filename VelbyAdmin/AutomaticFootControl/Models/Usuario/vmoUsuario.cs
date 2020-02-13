using Comun.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Usuario
{
    public class vmoUsuario
    {
        public UsuarioModelo Cliente { get; set; }
        public List<UsuarioModelo> LstColaborador { get; set; }
        public RespuestaComun Respuesta { get; set; }
    }
}