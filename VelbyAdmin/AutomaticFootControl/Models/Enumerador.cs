using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutomaticFootControl.Models
{
    public static class ClsDescripciones
    {
        public static string ObtenerDescripcion(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }

    public class Enumerador
    {
        public enum EnumTipoRespuesta
        {
            ErrorException = 1,
            ErrorSqlException = 2,
            ErrorControlado = 3,
            RespuestaOk = 4
        };

        public enum EnumTipoAccion
        {
            Insertar = 1,
            Actualizar = 2,
            Eliminar = 3,
            Consultar = 4
        };

        public enum EnumEstado
        {
            Inactivo = 0,
            Activo = 1
        };

        public enum NombreAccionEjecutar
        {
            [Description("Listar Empresas")]
            ListarEmpresa,
            [Description("Aceptar")]
            GestionEmpresa,

            [Description("Listar Horarios")]
            ListarHorario,
            [Description("Aceptar")]
            GestionHorario,
        };

        public enum NombreVista
        {
            GestionEmpresa,
            ListarEmpresa,
            GestionHorario,
            ListarHorario,
            GestionProducto,
            ListarProducto,
        }
    }
}