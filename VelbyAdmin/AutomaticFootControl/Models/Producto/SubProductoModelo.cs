using Comun;
using Comun.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Models.Producto
{
    public class SubProductoModelo
    {
        private int cantidadSeleccionada;

        public string TipoAccion { get; set; }
        public int IdSubservicio { get; set; }

        public List<SelectListItem> LstServicios { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        public string Descripcion { get; set; }

        public decimal Costo { get; set; }

        public string CostoTemporal { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        public DateTime TiempoDuracionServicio { get; set; }

        public string StrTiempoDuracionServicio { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        public string StrImagen { get; set; }
        public string Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Seleccionado { get; set; }
        public bool IsVisible { get; set; } = false;

        public int CantidadSeleccionadaTemp { get; set; }
        public int CantidadSeleccionada
        {
            get { return cantidadSeleccionada; }
            set
            {
                cantidadSeleccionada = value;
            }
        }

        public List<modFicha> LstRegistrosCatastros { get; set; }

    }
}