using AutomaticFootControl.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System;
using System.Web.Script.Serialization;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using AutomaticFootControl.Models.Empresa;
using System.Globalization;
using System.Linq;
using AutomaticFootControl.Validadores;

namespace AutomaticFootControl.Controllers.Empresa
{
    public class EmpresaController : Controller
    {
        JavaScriptSerializer js = new JavaScriptSerializer();

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GestionEmpresa(EmpresaModel mEmpresa)
        {
            RespuestaComun respuestaComun = new RespuestaComun();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        //int ddd = int.Parse("www");
                        mEmpresa.TipoAccion = mEmpresa.TipoAccion == 0 ? (int)Enumerador.EnumTipoAccion.Insertar : mEmpresa.TipoAccion;
                        mEmpresa.EstadoEmpresa = (int)Enumerador.EnumEstado.Activo;
                        var response = client.PostAsync("http://localhost/FootControlRest/api/empresa/GestionarEmpresa", new StringContent(new JavaScriptSerializer().Serialize(mEmpresa),
                            Encoding.UTF8, "application/json")).Result;

                        
                        if (response.IsSuccessStatusCode)
                        {
                            string strJson = response.Content.ReadAsStringAsync().Result;
                            respuestaComun = JsonConvert.DeserializeObject<RespuestaComun>(strJson);

                            if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
                            {
                                respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ObtenerDescripcion();
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                                respuestaComun.AccionEjecutarListar = "_ListarEmpresa";
                                respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ToString();
                                ViewBag.RespuestaListarAceptar = respuestaComun;

                            }else if(respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
                            {
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                                ViewBag.RespuestaErrorControlado = respuestaComun;

                            }else
                            {
                                throw new Exception();
                            }

                            mEmpresa.Respuesta = respuestaComun;
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
            return View(mEmpresa);
        }

        [EncryptedActionParameter]
        public async Task<ActionResult> Editar(string IdEmpresa)
        {
            EmpresaModelContenedor empresaContenedor = new EmpresaModelContenedor();
            EmpresaModel empresaModel = new EmpresaModel();
            int idEmpresaInput = int.Parse(IdEmpresa);
            try
            {

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;
                    EmpresaModel mEmpresa = new EmpresaModel();
                    mEmpresa.TipoAccion = (int)Enumerador.EnumTipoAccion.Consultar;
                    mEmpresa.IdEmpresa = idEmpresaInput;
                    mEmpresa.EstadoEmpresa = (int)Enumerador.EnumEstado.Activo;
                    response = await client.PostAsJsonAsync("http://localhost/FootControlRest/api/empresa/GestionarEmpresa", mEmpresa);
                    if (response.IsSuccessStatusCode)
                    {
                        empresaContenedor.LstEmpresaModel = await response.Content.ReadAsAsync<List<EmpresaModel>>();
                        empresaModel = empresaContenedor.LstEmpresaModel.FirstOrDefault(x => x.IdEmpresa == idEmpresaInput);
                        empresaModel.TipoAccion = (int)Enumerador.EnumTipoAccion.Actualizar;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorGenerico = string.Format(CultureInfo.CurrentCulture, Constantes.HTML_ERROR_GENERICO, Constantes.CODIGO_ERROR_GENERICO, Constantes.MENSAJE_ERROR_GENERICO, Constantes.MENSAJE_CONTACTO_ERROR_GENERICO);
                using (var client = new HttpClient())
                    await client.PostAsJsonAsync("http://localhost/FootControlRest/api/logs/GestionarLogException", Utilitariocs.PreparaGuardaLogsBase(ex));

            }
            return View("GestionEmpresa", empresaModel);
        }

        [EncryptedActionParameter]
        public async Task<ActionResult> Eliminar(string IdEmpresa)
        {
            EmpresaModel empresaModel = new EmpresaModel();
            RespuestaComun respuestaComun = new RespuestaComun();
            int idEmpresaInput = int.Parse(IdEmpresa);
            try
            {
                using (var client = new HttpClient())
                {
                    empresaModel.TipoAccion = (int)Enumerador.EnumTipoAccion.Eliminar;
                    empresaModel.IdEmpresa = idEmpresaInput;
                    var response = client.PostAsync("http://localhost/FootControlRest/api/empresa/GestionarEmpresa", new StringContent(new JavaScriptSerializer().Serialize(empresaModel),
                        Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string strJson = response.Content.ReadAsStringAsync().Result;
                        respuestaComun = JsonConvert.DeserializeObject<RespuestaComun>(strJson);

                        if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
                        {
                            respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ObtenerDescripcion();
                            respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                            respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ToString();
                            respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ToString();
                            ViewBag.RespuestaOkBotonAceptar = respuestaComun;

                        }
                        else if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
                        {
                            respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                            respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ToString();
                            respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ToString();
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

            //EmpresaModelContenedor empresaContenedor = await this.ObtenerEmpresasBase(respuestaComun);
            List<EmpresaModel> lstEmpresas = new List<EmpresaModel>();
            return View("ListarEmpresa", lstEmpresas);
        }

        public async Task<ActionResult> Empresa(RespuestaComun respuestaComun)
        {
            EmpresaModelContenedor empresaContenedor = await this.ObtenerEmpresasBase(respuestaComun);
            return View(empresaContenedor.LstEmpresaModel);
            //return await Task.Run(() => View());
        }

        public async Task<PartialViewResult> _ListarEmpresa(RespuestaComun respuestaComun)
        {
            //EmpresaModelContenedor empresaContenedor = await this.ObtenerEmpresasBase(respuestaComun);
            //return PartialView(empresaContenedor.LstEmpresaModel);
            return PartialView();
        }

        private async Task<EmpresaModelContenedor> ObtenerEmpresasBase(RespuestaComun respuestaComun)
        {
            EmpresaModelContenedor empresaContenedor = new EmpresaModelContenedor();
            try
            {

                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;
                    EmpresaModel mEmpresa = new EmpresaModel();
                    mEmpresa.TipoAccion = (int)Enumerador.EnumTipoAccion.Consultar;
                    mEmpresa.EstadoEmpresa = (int)Enumerador.EnumEstado.Activo;
                    response = await client.PostAsJsonAsync("http://localhost/FootControlRest/api/empresa/GestionarEmpresa", mEmpresa);
                    if (response.IsSuccessStatusCode)
                    {
                        empresaContenedor.LstEmpresaModel = await response.Content.ReadAsAsync<List<EmpresaModel>>();
                        empresaContenedor.Respuesta = respuestaComun;
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
            return empresaContenedor;
        }

        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> _GestionEmpresa(EmpresaModel mEmpresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        //int ddd = int.Parse("www");
                        mEmpresa.TipoAccion = mEmpresa.TipoAccion == 0 ? (int)Enumerador.EnumTipoAccion.Insertar : mEmpresa.TipoAccion;
                        mEmpresa.EstadoEmpresa = (int)Enumerador.EnumEstado.Activo;
                        var response = client.PostAsync("http://localhost/FootControlRest/api/empresa/GestionarEmpresa", new StringContent(new JavaScriptSerializer().Serialize(mEmpresa),
                            Encoding.UTF8, "application/json")).Result;

                        RespuestaComun respuestaComun = new RespuestaComun();
                        if (response.IsSuccessStatusCode)
                        {
                            string strJson = response.Content.ReadAsStringAsync().Result;
                            respuestaComun = JsonConvert.DeserializeObject<RespuestaComun>(strJson);

                            if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.RespuestaOk && respuestaComun.Codigo == Constantes.RESPUESTA_CODIGO_OK)
                            {
                                respuestaComun.NombreBotonListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ObtenerDescripcion();
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                                respuestaComun.AccionEjecutarListar = Enumerador.NombreAccionEjecutar.ListarEmpresa.ToString();
                                respuestaComun.AccionEjecutarAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ToString();
                                ViewBag.RespuestaListarAceptar = respuestaComun;

                            }
                            else if (respuestaComun.Tipo == (int)Enumerador.EnumTipoRespuesta.ErrorControlado)
                            {
                                respuestaComun.NombreBotonAceptar = Enumerador.NombreAccionEjecutar.GestionEmpresa.ObtenerDescripcion();
                                ViewBag.RespuestaErrorControlado = respuestaComun;

                            }
                            else
                            {
                                throw new Exception();
                            }

                            mEmpresa.Respuesta = respuestaComun;
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
            return PartialView(mEmpresa);
        }
    }
}