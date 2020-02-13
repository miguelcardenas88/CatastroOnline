using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Horario
{
    public class HorarioModel
    {
        public int IdHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Nombre:", Prompt = "Nombre horario")]
        public string NombreHorario { get; set; }

        public string DescripcionHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Color:", Prompt = "Color horario")]
        public string ColorHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Fecha inicio:", Prompt = "Fecha inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicioHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Fecha fin:", Prompt = "Fecha fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFinHorario { get; set; }

        public int EstadoHorario { get; set; }

        public DateTime FechaRegistroHorario { get; set; }

        public DateTime FechaModificadoHorario { get; set; }

        public byte ImagenHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Hora inicio:", Prompt = "Hora inicio")]
        [DataType(DataType.Time)]
        public DateTime HoraInicioHorario { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Hora fin:", Prompt = "Hora fin")]
        [DataType(DataType.Time)]
        public DateTime HoraFinHorario { get; set; }

        public int TipoAccion { get; set; }

        public RespuestaComun Respuesta { get; set; }
    }
}