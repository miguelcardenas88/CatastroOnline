using AutomaticFootControl.Models;
using AutomaticFootControl.Models.Horario;
using AutomaticFootControl.Validadores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AutomaticFootControl.Controllers.Horario
{
    public class HorarioController : Controller
    {
        JavaScriptSerializer js = new JavaScriptSerializer();

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GestionHorario(HorarioModel mHorario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        mHorario.TipoAccion = mHorario.TipoAccion == 0 ? (int)Enumerador.EnumTipoAccion.Insertar : mHorario.TipoAccion;
                        mHorario.EstadoHorario = (int)Enumerador.EnumEstado.Activo;
                        var response = client.PostAsync("http://localhost/FootControlRest/api/horario/GestionarHorario", new StringContent(new JavaScriptSerializer().Serialize(mHorario),
                            Encoding.UTF8, "application/json")).Result;

                        RespuestaComun respuestaComun = new RespuestaComun();
                        if (response.IsSuccessStatusCode)
                        {
                            string strJson = response.Content.ReadAsStringAsync().Result;
                            respuestaComun = JsonConvert.DeserializeObject<RespuestaComun>(strJson);

                            if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
                            {
                                respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarHorario.ObtenerDescripcion();
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                                respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                                respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ToString();
                                ViewBag.RespuestaListarAceptar = respuestaComun;

                            }
                            else if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
                            {
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                                ViewBag.RespuestaErrorControlado = respuestaComun;

                            }
                            else
                            {
                                throw new Exception();
                            }

                            mHorario.Respuesta = respuestaComun;
                        }
                        else
                            throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorGenerico = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO, Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
                using (var client = new HttpClient())
                    await client.PostAsJsonAsync("http://localhost/FootControlRest/api/logs/GestionarLogException", Utilitariocs.PreparaGuardaLogsBase(ex));
            }
            return View(mHorario);
        }

        [EncryptedActionParameter]
        public async Task<ActionResult> Editar(string IdHorario)
        {
            HorarioModelContenedor horarioContenedor = new HorarioModelContenedor();
            HorarioModel horarioModel = new HorarioModel();
            int idHorarioInput = int.Parse(IdHorario);
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;
                    HorarioModel mHorario = new HorarioModel();
                    mHorario.TipoAccion = (int)Enumerador.EnumTipoAccion.Consultar;
                    mHorario.IdHorario = idHorarioInput;
                    mHorario.EstadoHorario = (int)Enumerador.EnumEstado.Activo;
                    response = await client.PostAsJsonAsync("http://localhost/FootControlRest/api/horario/GestionarHorario", mHorario);
                    if (response.IsSuccessStatusCode)
                    {
                        horarioContenedor.LstHorarioModel = await response.Content.ReadAsAsync<List<HorarioModel>>();
                        horarioModel = horarioContenedor.LstHorarioModel.FirstOrDefault(x => x.IdHorario == idHorarioInput);
                        horarioModel.TipoAccion = (int)Enumerador.EnumTipoAccion.Actualizar;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorGenerico = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO, Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
                using (var client = new HttpClient())
                    await client.PostAsJsonAsync("http://localhost/FootControlRest/api/logs/GestionarLogException", Utilitariocs.PreparaGuardaLogsBase(ex));

            }
            return View(Enumerador.NombreVista.GestionHorario.ToString(), horarioModel);
        }

        [EncryptedActionParameter]
        public async Task<ActionResult> Eliminar(string IdHorario)
        {
            HorarioModel horarioModel = new HorarioModel();
            RespuestaComun respuestaComun = new RespuestaComun();
            int idHorarioInput = int.Parse(IdHorario);
            try
            {
                using (var client = new HttpClient())
                {
                    horarioModel.TipoAccion = (int)Enumerador.EnumTipoAccion.Eliminar;
                    horarioModel.IdHorario = idHorarioInput;
                    var response = client.PostAsync("http://localhost/FootControlRest/api/horario/GestionarHorario", new StringContent(new JavaScriptSerializer().Serialize(horarioModel),
                        Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string strJson = response.Content.ReadAsStringAsync().Result;
                        respuestaComun = JsonConvert.DeserializeObject<RespuestaComun>(strJson);

                        if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
                        {
                            respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarHorario.ObtenerDescripcion();
                            respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                            respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                            respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                            ViewBag.RespuestaOkBotonAceptar = respuestaComun;

                        }
                        else if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
                        {
                            respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionHorario.ObtenerDescripcion();
                            respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                            respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.ListarHorario.ToString();
                            ViewBag.RespuestaErrorControlado = respuestaComun;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorGenerico = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO, Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
                using (var client = new HttpClient())
                    await client.PostAsJsonAsync("http://localhost/FootControlRest/api/logs/GestionarLogException", Utilitariocs.PreparaGuardaLogsBase(ex));
            }
            List<HorarioModel> lstHorarios = new List<HorarioModel>();
            return View(Enumerador.NombreVista.ListarHorario.ToString(), lstHorarios);
        }

        public async Task<ActionResult> ListarHorario(RespuestaComun respuestaComun)
        {
            HorarioModelContenedor horarioContenedor = await this.ObtenerHorariosBase(respuestaComun);
            return View(horarioContenedor.LstHorarioModel);
        }

        private async Task<HorarioModelContenedor> ObtenerHorariosBase(RespuestaComun respuestaComun)
        {
            HorarioModelContenedor horarioContenedor = new HorarioModelContenedor();
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;
                    HorarioModel mHorario = new HorarioModel();
                    mHorario.TipoAccion = (int)Enumerador.EnumTipoAccion.Consultar;
                    mHorario.EstadoHorario = (int)Enumerador.EnumEstado.Activo;
                    response = await client.PostAsJsonAsync("http://localhost/FootControlRest/api/horario/GestionarHorario", mHorario);
                    if (response.IsSuccessStatusCode)
                    {
                        horarioContenedor.LstHorarioModel = await response.Content.ReadAsAsync<List<HorarioModel>>();
                        horarioContenedor.Respuesta = respuestaComun;
                        ViewBag.RespuestaErrorControlado = respuestaComun;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorGenerico = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO, Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
                using (var client = new HttpClient())
                    await client.PostAsJsonAsync("http://localhost/FootControlRest/api/logs/GestionarLogException", Utilitariocs.PreparaGuardaLogsBase(ex));

            }
            return horarioContenedor;
        }
    }
}