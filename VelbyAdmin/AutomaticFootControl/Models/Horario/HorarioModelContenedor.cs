using AutomaticFootControl.Models.Horario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Horario
{
    public class HorarioModelContenedor
    {
        public List<HorarioModel> LstHorarioModel { get; set; }

        public RespuestaComun Respuesta { get; set; }
    }
}