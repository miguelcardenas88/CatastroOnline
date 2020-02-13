using Comun;
using Comun.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Conexion
{
    public class GestionConexiones
    {
        protected SqlCommand cmd;
        protected SqlConnection con;
        protected SqlDataReader dr;

        protected void abrirConexion()
        {
            try
            {
                this.con = new SqlConnection(Constantes.CADENA_CONEXION);

                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void cerrarConexion()
        {
            try
            {
                if (this.con.State == ConnectionState.Open)
                    this.con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Dictionary<string, string>> EjecutaStoreProcedureConsulta(string parametrosSP, string parametroDatoMetodo, string spNombre)
        {
            //DatosMetodo Metodo = Utilitario.Deserializar<DatosMetodo>(parametroDatoMetodo);
            List<Dictionary<string, string>> lstOpenWith = new List<Dictionary<string, string>>();
            try
            {
                abrirConexion();
                SqlCommand cm = new SqlCommand(spNombre, this.con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlCommandBuilder.DeriveParameters(cm);

                var parametrosJsonSp = Utilitario.Deserializar<Dictionary<string, string>>(parametrosSP);
                foreach (SqlParameter param in cm.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input)
                    {
                        if (parametrosJsonSp != null)
                        {
                            foreach (var parametroJson in parametrosJsonSp)
                            {
                                if (param.ParameterName.Replace("@_", "").ToUpper() == parametroJson.Key.ToUpper())
                                {
                                    switch (param.SqlDbType.ToString().ToUpper())
                                    {
                                        case "INT":
                                        case "INT32":
                                        case "INT64":
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = 0;
                                            else
                                                param.Value = int.Parse(parametroJson.Value);
                                            break;
                                        case "DECIMAL":
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = 0.0;
                                            else
                                                param.Value = int.Parse(parametroJson.Value);
                                            break;

                                        case "VARCHAR":
                                        case "NVARCHAR":
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = string.Empty;
                                            else
                                                param.Value = parametroJson.Value;
                                            break;

                                        case "DATETIME":
                                        case "TIME":

                                            //string fecha = parametroJson.Value;
                                            //fecha = "[\"" + fecha.Replace("/", "\\/") + "\"]";
                                            //DateTime dt = ser.Deserialize<DateTime[]>(fecha).Single();
                                            //param.Value = dt;
                                            //DateTime.ParseExact(parametroJson.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = DateTime.Now;
                                            else
                                                param.Value = DateTime.Parse(parametroJson.Value);
                                            //param.Value = DateTime.ParseExact(parametroJson.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                            break;

                                        case "BIT":
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = 0;
                                            else
                                                param.Value = int.Parse(parametroJson.Value);

                                            break;
                                        case "IMAGE":
                                            if (string.IsNullOrEmpty(parametroJson.Value))
                                                param.Value = string.Empty;
                                            else
                                                param.Value = parametroJson.Value;
                                            break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                SqlDataReader reader = cm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Dictionary<string, string> openWith = new Dictionary<string, string>();
                    int numeroColumnas = reader.FieldCount;
                    for (int x = 0; x < numeroColumnas; x++)
                    {
                        string valor = reader.GetValue(x).ToString();
                        //Modificar este metodo para que no este aqui y hacer algo generico 
                        if (reader.GetName(x) == "Latitud" || reader.GetName(x) == "Longitud")
                        {
                            valor = reader.GetValue(x).ToString().Replace(Constantes.CHAR_COMA, Constantes.CHAR_PUNTO);
                        }
                        else
                        {
                            switch (reader.GetDataTypeName(x).ToUpper())
                            {
                                case "INT":
                                case "INT32":
                                case "INT64":
                                    if (string.IsNullOrEmpty(reader.GetValue(x).ToString()))
                                        valor = "0";
                                    break;

                                case "DECIMAL":
                                    if (string.IsNullOrEmpty(reader.GetValue(x).ToString()))
                                        valor = "0";
                                    else
                                        valor = reader.GetValue(x).ToString().Replace(Constantes.CHAR_COMA, Constantes.CHAR_PUNTO);
                                    break;

                                case "DOUBLE":
                                    if (string.IsNullOrEmpty(reader.GetValue(x).ToString()))
                                        valor = "0";
                                    else
                                        valor = reader.GetValue(x).ToString().Replace(Constantes.CHAR_COMA, Constantes.CHAR_PUNTO);
                                    break;
                            }
                        }

                        openWith.Add(reader.GetName(x), valor);
                    }
                    lstOpenWith.Add(openWith);
                }
                //UtilitarioLogs.PreparaGuardaLogsBase(null, Enumerador.EnumTipoRespuesta.RespuestaOk, Metodo);
            }
            catch (SqlException sqlex)
            {
                AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorSqlException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(sqlex, Enumerador.EnumTipoRespuesta.RespuestaOk, Metodo);
            }
            catch (Exception ex)
            {
                AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.RespuestaOk, Metodo);
                throw ex;
            }
            finally
            {
                if (this.con.State == ConnectionState.Open)
                    this.con.Close();
            }
            return lstOpenWith;
        }

        public RespuestaComun EjecutaStoreProcedure(string parametrosSP, string parametroDatoMetodo, string spNombre)
        {
            this.abrirConexion();
            RespuestaComun respuesta = new RespuestaComun();
            try
            {
                SqlCommand cm = new SqlCommand(spNombre, this.con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlCommandBuilder.DeriveParameters(cm);

                var parametrosJsonSp = Utilitario.Deserializar<Dictionary<string, object>>(parametrosSP);
                foreach (SqlParameter param in cm.Parameters)
                {
                    if (param.Direction == ParameterDirection.Input)
                    {
                        foreach (var parametroJson in parametrosJsonSp)
                        {
                            if (parametroJson.Value != null && parametroJson.Value.GetType().Name == Constantes.TIPO_DATO_JOBJECT)
                            {
                                var dictionary = ToDictionary<string>(parametroJson.Value);
                                foreach (var parametrosInternos in dictionary)
                                    param.Value = SeteaParametrosSql(param, parametrosInternos.Key,
                                        parametrosInternos.Value == null ? string.Empty : parametrosInternos.Value.ToString()).Value;
                            }
                            else
                            {
                                param.Value = SeteaParametrosSql(param, parametroJson.Key,
                                       parametroJson.Value == null ? string.Empty : parametroJson.Value.ToString()).Value;
                            }
                        }
                    }
                    if (param.Direction == ParameterDirection.InputOutput)
                    {
                        param.Value = "0";
                        param.SqlDbType = SqlDbType.SmallInt;
                        param.Direction = ParameterDirection.Output;
                        param.Size = 4;
                    }
                }
                string valorRetorno = string.Empty;
                int filasAfectadas = cm.ExecuteNonQuery();
                cm.Dispose();
                this.cerrarConexion();

                respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.RespuestaOk, Constantes.RESPUESTA_MENSAJE_OK);
                respuesta.FilasAfectadas = filasAfectadas;
                //UtilitarioLogs.PreparaGuardaLogsBase(null, Enumerador.EnumTipoRespuesta.RespuestaOk, Metodo);
                respuesta.ValorRetorno = valorRetorno;
            }
            catch (SqlException sqlex)
            {
                //UtilitarioLogs.PreparaGuardaLogsBase(sqlex, Enumerador.EnumTipoRespuesta.ErrorSqlException, Metodo);
                respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorSqlException, Constantes.RESPUESTA_MENSAJE_ERROR);
            }
            catch (Exception ex)
            {
                //UtilitarioLogs.PreparaGuardaLogsBase(ex, Enumerador.EnumTipoRespuesta.ErrorException, Metodo);
                respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
            }
            finally
            {
                if (this.con.State == ConnectionState.Open)
                {
                    this.con.Close();
                }
            }
            return respuesta;
        }

        private SqlParameter SeteaParametrosSql(SqlParameter param, string key, string value)
        {
            if (param.ParameterName.Replace("@_", "").ToUpper() == key.ToUpper())
            {
                switch (param.SqlDbType.ToString().ToUpper())
                {
                    case "INT":
                        if (value.ToString() == "True" || value.ToString() == "False")
                        {
                            param.Value = (value.ToString() == "True") ? 1 : 0;
                        }
                        else
                        {
                            param.Value = int.Parse(value.ToString());
                        }
                        break;

                    case "INT32":
                        param.Value = int.Parse(value.ToString());
                        break;

                    case "VARCHAR":
                    case "NVARCHAR":
                        param.Value = value;
                        break;

                    case "DATETIME":
                    case "TIME":

                        //string fecha = parametroJson.Value;
                        //fecha = "[\"" + fecha.Replace("/", "\\/") + "\"]";
                        //DateTime dt = ser.Deserialize<DateTime[]>(fecha).Single();
                        //param.Value = dt;

                        //param.Value = DateTime.Parse(value);
                        //break;
                        try
                        {
                            param.Value = DateTime.Parse(value.ToString());
                            //    string fecha = value.ToString();
                            //    fecha = "[\"" + fecha.Replace("/", "\\/") + "\"]";
                            //    DateTime dt = ser.Deserialize<DateTime[]>(fecha).Single();

                            //    if (dt < DateTime.Now)
                            //    {
                            //        param.Value = DateTime.Now;
                            //        break;
                            //    }

                            //    param.Value = dt;
                        }
                        catch
                        {
                            param.Value = DateTime.Parse(value.ToString());
                        }
                        break;

                    case "BIT":
                        param.Value = int.Parse(value.ToString());
                        break;
                    case "BOOL":
                        param.Value = bool.Parse(value);
                        break;
                    case "DECIMAL":

                        param.Value = decimal.Parse(value); //value;
                        break;
                }
            }
            return param;
        }

        public RespuestaComun AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta TipoRespuesta, string codigoError)
        {
            RespuestaComun respuesta = new RespuestaComun();
            switch (TipoRespuesta)
            {
                case Enumerador.EnumTipoRespuesta.ErrorException:
                    respuesta.Codigo = Constantes.RESPUESTA_CODIGO_ERROR;
                    respuesta.Mensaje = Constantes.RESPUESTA_MENSAJE_ERROR;
                    respuesta.Estado = Constantes.ESTADO_RESPUESTA_FALSE;
                    respuesta.Tipo = (int)Enumerador.EnumTipoRespuesta.ErrorException;
                    respuesta.MensajeInterno = Constantes.RESPUESTA_MENSAJE_ERROR;
                    break;

                case Enumerador.EnumTipoRespuesta.ErrorSqlException:
                    respuesta.Codigo = Constantes.RESPUESTA_CODIGO_ERROR;
                    respuesta.Mensaje = Constantes.RESPUESTA_MENSAJE_ERROR;
                    respuesta.Estado = Constantes.ESTADO_RESPUESTA_FALSE;
                    respuesta.Tipo = (int)Enumerador.EnumTipoRespuesta.ErrorSqlException;
                    respuesta.MensajeInterno = Constantes.RESPUESTA_MENSAJE_ERROR;
                    break;

                case Enumerador.EnumTipoRespuesta.ErrorControlado:
                    respuesta.Codigo = codigoError;
                    respuesta.Mensaje = Enumerador.EnumErrorControlado.ErrorEliminarEmpresa.ObtenerDescripcion();
                    respuesta.Estado = Constantes.ESTADO_RESPUESTA_FALSE;
                    respuesta.Tipo = (int)Enumerador.EnumTipoRespuesta.ErrorControlado;
                    respuesta.MensajeInterno = Enumerador.EnumErrorControlado.ErrorEliminarEmpresa.ObtenerDescripcion();
                    break;

                case Enumerador.EnumTipoRespuesta.RespuestaOk:
                    respuesta.Codigo = Constantes.RESPUESTA_CODIGO_OK;
                    respuesta.Mensaje = Constantes.RESPUESTA_MENSAJE_OK;
                    respuesta.Estado = Constantes.ESTADO_RESPUESTA_TRUE;
                    respuesta.MensajeInterno = Constantes.RESPUESTA_MENSAJE_OK;
                    respuesta.Tipo = (int)Enumerador.EnumTipoRespuesta.RespuestaOk;
                    break;
            }
            return respuesta;
        }

        //public RespuestaComun GuardaLogsBase(string parametrosSP, string spNombre, DatosMetodo metodo)
        //{
        //    this.abrirConexion();
        //    RespuestaComun respuesta = new RespuestaComun();
        //    try
        //    {
        //        SqlCommand cm = new SqlCommand(spNombre, this.con)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        SqlCommandBuilder.DeriveParameters(cm);

        //        var parametrosJsonSp = UtilitarioRest.Deserializar<Dictionary<string, string>>(parametrosSP);
        //        foreach (SqlParameter param in cm.Parameters)
        //        {
        //            if (param.Direction == ParameterDirection.Input)
        //            {
        //                foreach (var parametroJson in parametrosJsonSp)
        //                {
        //                    if (parametroJson.Value != null && parametroJson.Value.GetType().Name == Constantes.TIPO_DATO_JOBJECT)
        //                    {
        //                        var dictionary = ToDictionary<string>(parametroJson.Value);
        //                        foreach (var parametrosInternos in dictionary)
        //                            param.Value = SeteaParametrosSql(param, parametrosInternos.Key,
        //                                parametrosInternos.Value == null ? string.Empty : parametrosInternos.Value.ToString()).Value;
        //                    }
        //                    else
        //                        param.Value = SeteaParametrosSql(param, parametroJson.Key,
        //                            parametroJson.Value == null ? string.Empty : parametroJson.Value.ToString()).Value;
        //                }
        //            }
        //        }

        //        int filasAfectadas = cm.ExecuteNonQuery();
        //        cm.Dispose();
        //        this.cerrarConexion();
        //        respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.RespuestaOk, Constantes.RESPUESTA_MENSAJE_OK);
        //    }
        //    catch (SqlException sqlex)
        //    {
        //        UtilitarioLogs.PreparaLogsBaseArchivo(sqlex, Enumerador.EnumTipoRespuesta.ErrorException, metodo);
        //        respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorSqlException, Constantes.RESPUESTA_MENSAJE_ERROR);
        //    }
        //    catch (Exception ex)
        //    {
        //        UtilitarioLogs.PreparaLogsBaseArchivo(ex, Enumerador.EnumTipoRespuesta.ErrorException, metodo);
        //        respuesta = AsignarDatosRespuesta(Enumerador.EnumTipoRespuesta.ErrorException, Constantes.RESPUESTA_MENSAJE_ERROR);
        //    }
        //    finally
        //    {
        //        if (this.con.State == ConnectionState.Open)
        //        {
        //            this.con.Close();
        //        }
        //    }
        //    return respuesta;
        //}

        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = Utilitario.SerializarIdentado(obj);
            var dictionary = Utilitario.Deserializar<Dictionary<string, TValue>>(json);
            return dictionary;
        }

    }
}
