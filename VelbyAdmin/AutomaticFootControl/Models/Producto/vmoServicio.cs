using Comun.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Producto
{
    public class vmoServicio
    {
        public ObservableCollection<ProductoModelo> LstProducto { get; set; }
        public ObservableCollection<SubProductoModelo> LstSubProducto { get; set; }
        public RespuestaComun Respuesta { get; set; }
    }
}