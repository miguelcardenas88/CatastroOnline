using AutomaticFootControl.Models.Pantalla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models.Usuario
{
    public class UsuarioModelo
    {
        public UsuarioModelo()
        {
        }
        public PantallaModelo Pantalla { get; set; }

        public int IdCliente { get; set; }

        public int IdColaborador { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string DocumentoIdentidad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Correo { get; set; }

        public string Celular { get; set; }

        public string DireccionDomicilio { get; set; }

        public string Estado { get; set; }

        public int IdGenero { get; set; }

        public int IdTipo { get; set; }

        public int IdTipoUsuario { get; set; }

        public string TipoUsuario { get; set; }

        public string Usuario { get; set; }

        public string Contrasenia { get; set; }

        public string ConfirmarContrasenia { get; set; }

        public string NombreCompleto { get; set; }

        public string Hojavida { get; set; }
        public string RecordPolicial { get; set; }
        public bool Entrevista { get; set; }
        public string DescripcionEntrevista { get; set; }
        public bool PruebaTecnica { get; set; }
        public string DescripcionPrueba { get; set; }
        public string Foto { get; set; }

    }
}