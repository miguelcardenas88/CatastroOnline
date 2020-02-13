using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Empresa
{
    public class EmpresaModel
    {
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Nombre:", Prompt ="Nombre cliente")]
        public string NombreEmpresa { get; set; }

        [Display(Name = "Dirección:")]
        public string DireccionEmpresa { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Teléfono no válido")]
        [StringLength(10, ErrorMessage = "El tamaño máximo es 10 y el mínimo 7 digitos", MinimumLength = 7)]
        public string TelefonoEmpresa { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Email no válido")]
        public string EmailEmpresa { get; set; }

        public int EstadoEmpresa { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int TipoAccion { get; set; }

        public RespuestaComun Respuesta { get; set; }
    }
}