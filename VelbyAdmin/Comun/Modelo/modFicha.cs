using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Comun.Modelo
{
    public class modFicha
    {
        public string CodigoUnico { get; set; }
        public string CodigoCatastral { get; set; }
        public string ClaveAnterior { get; set; }
        public List<SelectListItem> LstTipoIdentificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string TextoTipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombrePropietario { get; set; }
        public string PropietarioAnterior { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public List<SelectListItem> LstUsoPredio { get; set; }
        public string UsoPredio { get; set; }
        public string TextoUsoPredio { get; set; }
        public List<SelectListItem> LstEscritura { get; set; }
        public string Escritura { get; set; }
        public string TextoEscritura { get; set; }
        public List<SelectListItem> LstOcupacion { get; set; }
        public string Ocupacion { get; set; }
        public string TextoOcupacion { get; set; }
        public List<SelectListItem> LstLocalizacion { get; set; }
        public string Localizacion { get; set; }
        public string TextoLocalizacion { get; set; }
        public int NumeroPiso { get; set; }
        public List<SelectListItem> LstAbastecimiento { get; set; }
        public string Abastecimiento { get; set; }
        public string TextoAbastecimiento { get; set; }
        public List<SelectListItem> LstAguaRecib { get; set; }
        public string AguaRecib { get; set; }
        public string TextoAguaRecib { get; set; }
        public List<SelectListItem> LstAlcantarillado { get; set; }
        public string Alcantarillado { get; set; }
        public string TextoAlcantarillado { get; set; }
        public string CodigoLocalizacion { get; set; }
        public bool TieneMedidor { get; set; }
        public string UsuarioRegistro { get; set; }
        public string Observacion { get; set; }
    }
}
