using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Comun.Modelo
{
    public class FichaCatastroModel
    {
        public string Parroquia { get; set; }
        public string CodigoCatastral { get; set; }
        public string ClaveAnterior { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombrePropietario { get; set; }
        public string PropietarioAnterior { get; set; }
        public string Direccion { get; set; }
        public string Sector { get; set; }
        public string Categoria { get; set; }
        //public List<SelectListItem> MesConsumo { get; set; }
        public string MesConsumo { get; set; }
        public string NumeroMedidor { get; set; }
        public string MetrosCubicoConsumo { get; set; }
        public string Rango { get; set; }
        public string Observaciones { get; set; }
        public string Deducciones { get; set; }
        public string TarifaFija { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimieto { get; set; }
        public List<FichaCatastroModel> LstRegistrosCatastros { get; set; }


    }
}
