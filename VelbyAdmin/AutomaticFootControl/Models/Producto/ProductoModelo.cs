using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomaticFootControl.Models.Producto
{
    public class ProductoModelo
    {
        private int idServicio;
        private string descripcion;
        private string estado;
        private DateTime fechaRegistro;
        private byte[] imagen;
        private string strImagen;
        private string genero;
        public string TipoAccion { get; set; }

        public HttpPostedFileBase ImageData { get; set; }

        public int IdServicios
        {
            get { return idServicio; }
            set
            {
                idServicio = value;
            }
        }

        [Required(ErrorMessage = Constantes.CAMPO_REQUERIDO)]
        [Display(Name = "Descripcion:", Prompt = "Descripcion")]
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
            }
        }

        public string Estado
        {
            get { return estado; }
            set
            {
                estado = value;
            }
        }

        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set
            {
                fechaRegistro = value;
            }
        }

        public byte[] Imagen
        {
            get { return imagen; }
            set
            {
                imagen = value;
            }
        }

        public string StrImagen
        {
            get { return strImagen; }
            set
            {
                strImagen = value;
            }
        }

        public string Genero
        {
            get { return genero; }
            set
            {
                genero = value;
            }
        }


    }
}