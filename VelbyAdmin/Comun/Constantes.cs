using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Constantes
    {
        //public const string CADENA_CONEXION = "Data Source=DESKTOP-PNIVKFS\\SQLEXPRESS;Initial Catalog=COMEDOR;Persist Security Info=True;User ID=sa;Password=sa";
        //public const string CADENA_CONEXION =  @"Data Source=DESKTOP-PNIVKFS\SQLEXPRESS;Initial Catalog=COMEDOR;Integrated Security=True";
        //public const string CADENA_CONEXION = "Data Source=SQL5041.site4now.net;Initial Catalog=DB_A46A97_desarrollo;User Id=DB_A46A97_desarrollo_admin;Password=Desarrollo42";

        public const string CAMPO_REQUERIDO = " *";
        public const string RESPUESTA_MENSAJE_OK = "Proceso Ejecutado Correctamente";
        public const string RESPUESTA_CODIGO_OK = "10000";
        public const string HTML_ERROR_GENERICO = "<div class='row'> " +
                        "<div class='col-md-12 my-5 text-center'> " +
                        "	<div class='text-danger'> " +
                        "		<i class='batch-icon batch-icon-link-alt batch-icon-xxl'></i> " +
                        "		<i class='batch-icon batch-icon-search batch-icon-xxl'></i>  " +
                        "		<i class='batch-icon batch-icon-link-alt batch-icon-xxl'></i>  " +
                        "	</div> " +
                        "	<h4 class='display-5'>{0}</h4> " + //404
                        "	<div class='lead mb-3'>{1}</div> " +  //Page Not Found
                        "	<div class='lead'>{2}</div> " + //We can't find the page you are looking for.
                        "	<div class='lead'>{3}</div> " + //We can't find the page you are looking for.
                        "   </div> " +
                        "</div>";
        public const string CODIGO_ERROR_GENERICO = "99999";
        public const string MENSAJE_ERROR_GENERICO = "Se ha producido un error";
        public const string MENSAJE_CONTACTO_ERROR_GENERICO = "Por favor comuníquese con nosotros al 1800-00-00-00.";
        public string MENSAJE_MOSTRAR_ERROR_GENERICO = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO,
            Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
        public const string FORMATO_FECHA_TABLA_DDMMYYY = "{0:dd/MM/yyyy}";
        public const string FORMATO_HORA_TABLA_HHMM = "{0:HH:mm}";
        public const string FORMATO_FECHA_FORM_DDMMYYY = "MM/dd/yyyy";
        public const string FORMATO_HORA_FORM_HHMM = "HH:MM";
        public const string FORMATO_HORA_HHmm = "HH:mm";
        public const bool ESTADO_RESPUESTA_TRUE = true;
        public const bool ESTADO_RESPUESTA_FALSE = false;
        public const string RESPUESTA_CODIGO_ERROR = "99999";
        public const string RESPUESTA_CODIGO_ERROR_CONTROLADO = "44444";
        public const string RESPUESTA_MENSAJE_ERROR = "Error al ejecutar el proceso";
        public const string PAGO_EFECTIVO = "Efectivo";
        public const string PAGO_TARJETA = "Tarjeta";
        public const string ESTADO_RESERVA_PENDIENTE = "P";//Reserva pendiente que el colaborador acepte la reserva
        public const string ESTADO_RESERVA_ACEPTADA = "A"; //Reserva aceptada, en espera para que el colaborador de el servicio
        public const string ESTADO_RESERVA_INICIADA = "I"; //Reserva en desarrollo
        public const string ESTADO_RESERVA_FINALIZADA = "F"; //Reserva Finalizada
        public const string ESTADO_RESERVA_CANCELADA = "C"; //Reserva Cancelada
        public const string ESTADO_RESERVA_MODIFICADA = "M"; //Reserva Modificada

        public const string ESTADO_TEXTO_RESERVA_PENDIENTE = "Pendiente";//Reserva pendiente quESTADO_RESERVA_FINALIZADAe el colaborador acepte la reserva
        public const string ESTADO_TEXTO_RESERVA_ACEPTADA = "Aceptada"; //Reserva aceptada, en espera para que el colaborador de el servicio
        public const string ESTADO_TEXTO_RESERVA_EN_PROCESO = "En proceso"; //Reserva en desarrollo
        public const string ESTADO_TEXTO_RESERVA_FINALIZADA = "Finalizada"; //Reserva Finalizada
        public const string ESTADO_TEXTO_RESERVA_CANCELADA = "Cancelada"; //Reserva Finalizada

        public const string ESTADO_ACTIVO = "1"; //Activo
        public const string ESTADO_INACTIVO = "0"; //Inactivo

        #region Pasar a un archivo XML
        public const string IDIOMA_APLICACION = "es-ES";
        // const string SERVIDOR_REST = "http://jochcrip1-001-site6.itempurl.com/";
        public const string SERVIDOR_REST = "https://bellezapprest.smcodes.com";
        public const string CADENA_CONEXION = "Data Source=SQL5045.site4now.net;Initial Catalog=DB_A4907E_Catastro;Persist Security Info=True;User ID=DB_A4907E_Catastro_admin;Password=CatastroA4A3A2";
        //public const string CADENA_CONEXION = "Data Source=SQL5047.site4now.net;Initial Catalog=DB_A4907E_Desarrollo42;User Id=DB_A4907E_Desarrollo42_admin;Password=Desarrollo42";
        //public const string CADENA_CONEXION = "Data Source=SQL5047.site4now.net;Initial Catalog=DB_A4907E_SmartCart;User Id=DB_A4907E_SmartCart_admin;Password=SmartCart2019";
        public const string TOKEN = "Velby2019@";
        public const char CHAR_COMA = ',';
        public const char CHAR_PUNTO = '.';
        public const string HABILITAR_ENVIO_MAIL = "HabilitarEnviarMail";

        public const string ACCION_INSERTAR = "I";
        public const string ACCION_MODIFICAR = "M";
        public const string ACCION_ELIMINAR = "E";
        #endregion


        public const string NOMBRE_ARCHIVO_LOG_XML = "logArchivo";
        public const string FORMATO_YYYYMMDD_HHMMSS = "yyyymmdd HHmmss";
        public const string DIRECTORIO_ARCHIVO_LOG = "C:\\Users\\hp\\Documents\\Visual Studio 2012\\Projects\\FootControlRest\\Scripts\\Logs\\";
        public const string TIPO_DATO_JOBJECT = "JObject";
        public const string XML = ".xml";

        public const string usuarioLogueo = "CambiarAarchivo";
        public const string contraseniaLogueo = "CambiarAarchivo";

        #region Paginas de redireccion

        public const string CONTENEDOR_PRINCIPAL_TABBED_PAGE = "ContenedorPrincipalTabbedPage";
        public const string CONTENEDOR_COLABORADOR_TABBED_PAGE = "ContenidoTabbedColaborador";

        #endregion

        #region Metodos de pago

        public const string METODO_PAGO_EFECTIVO = "Efectivo";
        public const string METODO_PAGO_TARJETA_CREDITO = "Tarjeta de crédito";
        public const string METODO_PAGO_TARJETA_DEBITO = "Tarjeta de débito";
        public const string METODO_PAGO_TRANSFERENCIA_BANCARIA = "Transferencia bancaria";
        public const string METODO_PAGO_OTROS = "Otros";

        #endregion

        #region Dialogos textos

        public const string DLG_PROCESANDO = "Procesando";

        #endregion

        #region Textos pantallas

        public const string LBL_CODIGO_RESERVA = "Código: ";
        public const string LBL_CLIENTE_RESERVA = "Cliente: ";
        public const string LBL_FECHA_RESERVA = "Fecha: ";
        public const string LBL_COLABORADOR_RESERVA = "Colaborador: ";

        #endregion

        #region Errores Sacar a un archivo de cofiguracion

        public const string VERIFICAR_INFORMACION_INGRESADA = "Verifique la información ingresada";
        public const string ERROR_CONSULTA_AREA = "Para realizar la consulta ingrese los datos requeridos";
        public const string ERROR_INSERTAR_PROVEEDOR_EMPRESA = "Los datos incompletos para el registro del Proveedor Empresa";
        public const string ERROR_OBTENER_SERVICIO = "No se puede obtener los servicios.";
        public const string ERROR_OBTENER_DATOS = "No se puede los datos.";
        public const string ERROR_OBTENER_CONFIGURACION = "No se puede obtener la configuración.";
        public const string ERROR_MODIFICAR_RESERVA = "No se puede modificar la reserva, comuniquese con servicio al cliente";

        #endregion

        #region Errores

        public const string TITULO_RESERVA = "Reserva";
        public const string ERROR_UBICACION_NO_SELECCIONADA = "Selecciona la ubicación";
        public const string ERROR_FECHA_HORA_NO_SELECCIONADA = "Verifica la fecha y hora seleccionada";
        public const string ERROR_PAGO_NO_SELECCIONADO = "Selecciona el método de pago";
        public const string ERROR_SELECCIONE_SUBPRODUCTO = "Seleccione un servicio";

        public const string ERROR_HORARIO_FUERA_DE_ATENCION = "Lo sentimos por el momento nos encontramos fuera de servicio";
        public const string ERROR_SERVICIO_NO_DISPONIBLE = "Servicio no disponible";

        #endregion

        #region Errores Colaboradores

        public const string ERROR_COLABORADOR_NO_DISPONIBLE = "Colaborador no disponible";

        #endregion

        #region Mensaje Reserva

        public const string RESERVA_EXITOSA = "Tu reserva se registro correctamente";
        public const string RESERVA_ACEPTADA = "Registro exitoso";
        public const string RESERVA_INICIADA = "Reserva iniciada correctamente";
        public const string RESERVA_FINALIZADA = "Reserva finalizada correctamente";
        public const string RESERVA_CANCELADA = "Su reserva fue cancelada";
        public const string RESERVA_MODIFICADA = "Su reserva fue modificada";
        public const string SOLICITUD_EJECUTADA_CORRECTAMENTE = "Solicitud ejecutada correctamente";
        #endregion

        #region Genero

        public const string HOMBRE = "H";
        public const string MUJER = "M";

        #endregion

        #region Configuraciones Base de datos
        public const string CONF_HH_mm_ss = "HH:mm:ss";
        public const string CONF_FECHA_01_01_1900 = "01/01/1900";
        public const string CONF_HORA_APERTURA = "HoraApertura";
        public const string CONF_HORA_CIERRE = "HoraCierre";
        #endregion

        #region Configuraciones envio de notificaciones

        //public string CUERPO_MENSAJE = Directory.GetCurrentDirectory() + "C:/Users/migue/OneDrive/Desktop/CorreoBienvenida.html";
        public const string CUERPO_MENSAJE = "/ReservaPendiente.html";
        public const string SMTP_SERVER = "smtp.live.com";
        public const string MAIL_ENVIO = "miguel.cardenas.ch@hotmail.com";
        public const string ASUNTO = "CambiarAarchivo";
        public const string CONTRASENIA_MAIL = "Something20*";
        public const string NOMBRE_PANTALLA = "SINAT";
        public const int PUERTO_EMAIL = 587;

        public const string CUERPO_MENSAJE_RESERVA_ESPERA = "/ReservaPendiente.html";
        public const string CUERPO_MENSAJE_RESERVA_ACEPTADA = "/ReservaAceptada.html";
        public const string CUERPO_MENSAJE_RESERVA_INICIADA = "/ReservaIniciada.html";
        public const string CUERPO_MENSAJE_RESERVA_FINALIZADA = "/ReservaFinalizada.html";
        public const string CUERPO_MENSAJE_RESERVA_CANCELADA = "/ReservaCancelada.html";
        public const string CUERPO_MENSAJE_RESERVA_MODIFICADA = "/ReservaModificada.html";
        public const string CUERPO_MENSAJE_REGISTRO_CLIENTE = "/RegistroCliente.html";
        public const string CUERPO_MENSAJE_REGISTRO_COLABORADOR = "/RegistroColaborador.html";

        public const string ASUNTO_RESERVA_ESPERA = "RESERVA EN ESPERA";
        public const string ASUNTO_RESERVA_ACEPTADA = "RESERVA ACEPTADA";
        public const string ASUNTO_RESERVA_INICIADA = "RESERVA INICIADA";
        public const string ASUNTO_RESERVA_FINALIZADA = "RESERVA FINALIZADA";
        public const string ASUNTO_RESERVA_MODIFICADA = "RESERVA MODIFICADA";
        public const string ASUNTO_RESERVA_CANCELADA = "RESERVA CANCELADA";
        public const string ASUNTO_REGISTRO_CLIENTE = "REGISTRO CLIENTE";

        #endregion

        #region Formatos 

        public const string MM_dd_yyyy = "MM/dd/yyyy";
        public const string HH_MM_TT = "hh:mm tt";

        #endregion

        #region Estados Usuario 
        public const string INACTIVO = "0";
        public const string ACTIVO = "1";
        public const string BLOQUEADO = "2";

        #endregion
    }
}
