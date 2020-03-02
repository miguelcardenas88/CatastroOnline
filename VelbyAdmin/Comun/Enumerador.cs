using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public static class ClsDescripciones
    {
        public static string[] TipoDatos = new string[14] { "INT", "INT32", "INT64", "VARCHAR", "STRING", "NCHAR", "DATETIME", "BIT", "BOOLEAN", "DECIMAL",
            "IMAGE","BYTE","BYTE[]","DOUBLE" };

        public static string ObtenerDescripcion(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }

    public class Enumerador
    {
        #region Navegacion Paginas

        public enum EnumListaMenu
        {
            Productos,
            Informacion
        }

        #endregion

        #region Stored Procedure

        public enum SpHorario
        {
            [Description("spi_horario")]
            Insertar,
            [Description("spu_horario")]
            Actualizar,
            [Description("")]
            Eliminar,
            [Description("sps_horario")]
            Consultar
        };

        public enum SpMenu
        {
            [Description("spi_Menu")]
            Insertar,
            [Description("spu_Menu")]
            Actualizar,
            [Description("spd_Menu")]
            Eliminar,
            [Description("sps_Menu")]
            Consultar
        };

        public enum SpGestionUsuario
        {
            [Description("sps_inicio_sesion")]
            sps_inicio_sesion,
            [Description("sps_verificar_usuario")]
            sps_verificar_usuario,
            [Description("sps_registrar_usuario")]
            sps_registrar_usuario,
            [Description("sps_registrar_servicio_colaborador")]
            sps_registrar_servicio_colaborador,
        };

        public enum SpInicioSesion
        {
            [Description("sps_inicio_sesion")]
            sps_inicio_sesion,
        };

        public enum SpServicios
        {
            [Description("sps_Servicios")]
            sps_servicios,
            [Description("sps_sub_Servicios")]
            sps_sub_servicios,
            [Description("sps_SubServicio_x_Reserva")]
            sps_SubServicio_x_Reserva,
            [Description("spi_servicio")]
            spi_servicio,
            [Description("spu_servicio")]
            spu_servicio,
            [Description("spd_servicio")]
            spd_servicio,
        };

        public enum SpSubServicios
        {
            [Description("spi_sub_servicio")]
            spi_sub_servicio,
            [Description("spu_sub_servicio")]
            spu_sub_servicio,
            [Description("spd_sub_servicio")]
            spd_sub_servicio,
        };

        public enum SpConfiguracion
        {
            [Description("sps_configuracion")]
            sps_configuracion,
        };

        public enum SpReserva
        {
            [Description("spi_reserva")]
            Insertar,
            [Description("spu_reserva")]
            Actualizar,
            [Description("")]
            Eliminar,
            [Description("sps_reserva")]
            Consultar,
            [Description("spi_reserva_servicio")]
            spi_reserva_servicio,
            [Description("sps_reserva_colaborador")]
            sps_reserva_colaborador,
        };

        public enum SpUbicacion
        {
            [Description("spi_ubicacion")]
            Insertar,
        };

        public enum SpColaborador
        {
            [Description("sps_colaborador_disponible")]
            sps_colaborador_disponible,
            [Description("sps_colaborador_Sin_Asignacion")]
            sps_colaborador_Sin_Asignacion,
            [Description("spu_aceptar_reserva")]
            spu_aceptarReserva,
            [Description("spu_iniciar_reserva")]
            spu_iniciar_reserva,
            [Description("spu_finalizar_reserva")]
            spu_finalizar_reserva,
            [Description("spu_colaborador")]
            spu_colaborador,
        };

        public enum SPCliente
        {
            [Description("sps_reserva_cliente")]
            sps_reserva_cliente,
            [Description("spu_reserva_cancela_modifica")]
            spu_reserva_cancela_modifica,
        }

        public enum SpUsuario
        {
            [Description("sps_usuario")]
            sps_usuario,
        }

        #endregion

        public enum EnumMetodoPago
        {
            Efectivo = 1,
            Tarjeta = 2,
        }

        public enum EnumTipoRespuesta
        {
            ErrorException = 1,
            ErrorSqlException = 2,
            ErrorControlado = 3,
            RespuestaOk = 4
        };

        public enum TipoDatoRespuesta
        {
            Lista = 1,
            Mensaje = 2
        }

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
            GestionSubProducto,
            ListaRegistroCatastro,
            RegistroUsuarios,
            RegistroCatastro,
            ListadoRegistroCatastro,
        }

        #region Codigos de error y mensajes de error

        public enum EnumErrorControlado
        {
            [Description("No se pudo eliminar la empresa")]
            ErrorEliminarEmpresa = 10,
        };

        #endregion
    }
}
