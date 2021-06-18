using App_consulta.Data;
using App_consulta.Models;
using App_consulta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace App_consulta.Controllers
{
    public class KoboController : Controller
    {

        public const String FILE_CARACTERIZACION = "data";
        public const String FILE_ASOCIACION = "association";


        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment _env;

        public KoboController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> _userManager,
            IWebHostEnvironment env
            )
        {
            db = context;
            userManager = _userManager;
            _env = env;
        }

        //Listados de encuestas (caracterizaciones)

        [Authorize(Policy = "Encuestas.Usuario")]
        public async Task<ActionResult> ListadoEncuestasUsuario(String code)
        {
            var resp = await GetListadoEncuestas(code);
            return Json(resp);
        }

        [Authorize(Policy = "Encuestas.Listado")]
        public async Task<ActionResult> ListadoEncuestas()
        {
            var resp = await GetListadoEncuestas();
            return Json(resp);
        }

        //Listados de encuestas (asociaciones)

        [Authorize(Policy = "Encuestas.Usuario")]
        public async Task<ActionResult> ListadoAsociacionesUsuario(String code)
        {
            var resp = await GetListadoAsociaciones(code);
            return Json(resp);
        }

        [Authorize(Policy = "Encuestas.Listado")]
        public async Task<ActionResult> ListadoAsociaciones()
        {
            var resp = await GetListadoAsociaciones();
            return Json(resp);
        }

        //Load formalizacion

        [Authorize(Policy = "Formalizacion.Validar")]
        public async Task<RespuestaAccion> LoadFormalizacion(string idKobo, string identity = null )
        {
            var r = new RespuestaAccion();

            //Carga los datos de conexión desde la configuración 
            var config = await db.Configuracion.FirstOrDefaultAsync();
            var configFormalizacion = await db.FormalizationConfig.Where(n => n.Field != "" && n.Value != "").ToListAsync();

            var fields = JsonConvert.SerializeObject(configFormalizacion.Select(n => n.Value).ToArray() );
            var query = JsonConvert.SerializeObject(new { _id = idKobo });

            var url = config.KoboKpiUrl + "/assets/" + config.KoboAssetUid + "/submissions/?format=json" +
                "&query="+ HttpUtility.UrlEncode(query) +
                "&fields=" + HttpUtility.UrlEncode(fields);

            dynamic result = null;
            
            try
            {

                //Consulta la información
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Token " + config.KoboApiToken);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(responseBody);
                //Valida que se cargaran resultados
                if (data == null) { r.Message = "ERROR: No fue posible recuperar la información"; return r ; }
                result = data[0];
            }
            catch (HttpRequestException e){r.Message = e.Message; return r; }

            if(result != null)
            {
                var username = identity != null ? identity : User.Identity.Name;
                var user = await userManager.FindByNameAsync(username);

                var responsable = user.IDDependencia;
                //Mapea los resultados
                var formalizacion = new Formalization()
                {
                    Estado = Formalization.ESTADO_BORRADOR,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now,
                    IdResponsable = responsable,
                    IdCreateByUser = user.Id,
                    IdLastEditByUser = user.Id
                };

                //folder para los adjunto

                var _path = Path.Combine(_env.ContentRootPath, "Storage", "Formalizacion", idKobo);
                var _relative = Path.Combine("Formalizacion", idKobo);
                var remoteUri = config.KoboAttachment + config.KoboUsername + "/attachments/";

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                var imageErrors = false;

                var map = configFormalizacion.ToDictionary(n => n.Field, n => n.Value);

                var oType = typeof(Formalization);
                foreach (var oProperty in oType.GetProperties())
                {
                    var name = oProperty.Name;

                    if (map.ContainsKey(name))
                    {
                        var fieldName = map[name];
                        var value = (String)result[fieldName];

                        //Si es una campo de imagen descarga el adjunto
                        if (name.StartsWith("Img") && value != null && value != "")
                        {
                            var filePath = DownloadFile(remoteUri, value, _path, _relative, config.KoboApiToken);
                            if(filePath == "")
                            {
                                imageErrors = true;
                                break;
                            }
                            value = filePath;
                        }
                        oProperty.SetValue(formalizacion, value == null ? "" : value, null);
                    }
                }
              
                //Valida los adjuntos
                if(imageErrors){r.Message = "ERROR: No fue posible cargar los archivos adjuntos"; return r; }

                //Consulta la dependencia del encuestador
                if(formalizacion.Encuestador != null && formalizacion.Encuestador != "")
                {
                    int numId = Int32.Parse(formalizacion.Encuestador);
                    var encuestador = await db.Pollster.Where(n => n.DNI == numId).FirstOrDefaultAsync();
                    formalizacion.IdResponsable = encuestador != null ? encuestador.IdResponsable : responsable;
                }

                var log = new Logger(db);
                try
                {
                    //Departamento 
                    var municipio = await db.Location.Where(n => n.Code2 == formalizacion.Municipio).FirstOrDefaultAsync();
                    if(municipio != null)
                    {
                        formalizacion.Municipio = municipio.Name;
                        formalizacion.Departamento = municipio.LocationParent != null ? municipio.LocationParent.Name : formalizacion.Departamento;
                    }

                    db.Formalization.Add(formalizacion);
                    await db.SaveChangesAsync();
                    r.Url = "Formalizacion/Edit/" + formalizacion.Id;
                    r.Success = true;

                    var registro = new RegistroLog { Usuario = identity, Accion = "Create", Modelo = "Formalization", ValNuevo = formalizacion };
                    await log.Registrar(registro, typeof(Formalization));

                }
                catch (Exception e){r.Message = e.InnerException.Message; return r;}
            }
            else
            {
                r.Message = "ERROR: No se encuentran resultados."; return r;
            }

            return r;
        }

        //Actualizar archivo de encuestas
        [Authorize(Policy = "Encuestas.Actualizar")]
        public async Task<ActionResult> ActualizarManual()
        {
            var error = "";
            var accion = "";

            var resp = await UpdateDataFileCaract();
            var r = execUpdateData(resp);
            if (r.Success){accion = r.Message;}
            else{error = r.Message;}

            resp = await UpdateDataFileAssoc();
            r = execUpdateData(resp);
            if (r.Success){accion += "<br>" + r.Message;}
            else{error += "<br>" + r.Message;}

            ViewBag.Error = error;
            ViewBag.Accion = accion;
            return View();
        }

        [HttpGet]
        public async Task<bool> Auto(string auth)
        {
            if (auth == "#51kS7.Jms22")
            {
                
                var resp = await UpdateDataFileCaract();
                execUpdateData(resp);

                resp = await UpdateDataFileAssoc();
                execUpdateData(resp);

                return true;
            }
            return false;
        }

        private RespuestaAccion execUpdateData(String resp)
        {
            var r = new RespuestaAccion();

            var fecha = DateTime.Now.ToString("r");
            var accion = "[" + fecha + "] " + resp;


            var _path = Path.Combine(_env.ContentRootPath, "Storage");
            var archivo = _path + "/Logs.txt";
            try
            {
                if (!System.IO.File.Exists(archivo))
                {
                    using (StreamWriter sw = System.IO.File.CreateText(archivo))
                    {
                        sw.WriteLine(accion);
                    }
                }
                else
                {
                    using (StreamWriter sw = System.IO.File.AppendText(archivo))
                    {
                        sw.WriteLine(accion);
                    }
                }
                r.Message = accion;
                r.Success = true;
            }
            catch (Exception e)
            {
                r.Message = e.Message;
            }

            return r;
        }
         
        public String GetDatetimeData(String file)
        {
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                DateTime creation = System.IO.File.GetCreationTime(Path.Combine(_path, file  + ".json"));
                text = creation.ToString("f");
            }
            catch (Exception) { }
            return text;
        }

        //Extra
        private async Task<String> UpdateDataFileCaract()
        {
            //Carga los datos de conexión desde la configuración 
            var config = await db.Configuracion.FirstOrDefaultAsync();
            var mapParams = JsonConvert.DeserializeObject<EncuestaMap>(config.KoboParamsMap);
            var fields= JsonConvert.SerializeObject(new string[] { mapParams.IdKobo, mapParams.User, mapParams.LocationCode, mapParams.Datetime, mapParams.Validation, mapParams.DNI });
            var url = config.KoboKpiUrl + "/assets/" + config.KoboAssetUid + "/submissions/?format=json&fields=" + HttpUtility.UrlEncode(fields);

       
            String resp;

            try
            {

                //Consulta la información
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Token " + config.KoboApiToken);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
             
                dynamic data = JsonConvert.DeserializeObject(responseBody);
                //Valida que se cargaran resultados
                if (data == null) { return "ERROR: No fue posible recuperar la información"; }

                //Mapea los resultados
                var encuestas = new List<EncuestaMap>();

                foreach (var result in data)
                {
                    encuestas.Add(new EncuestaMap()
                    {
                        IdKobo = (String)result[mapParams.IdKobo],
                        User = (String)result[mapParams.User],
                        LocationCode = (String)result[mapParams.LocationCode],
                        Datetime = (String)result[mapParams.Datetime],
                        Validation = (String)result[mapParams.Validation],
                        DNI = (String)result[mapParams.DNI],
                    });
                }

                //Guarda la información mapeada en el archivo .json
                var dataFile = JsonConvert.SerializeObject(encuestas);
                var statusSave = SaveDataFile(dataFile, FILE_CARACTERIZACION);
                if (statusSave == "")
                {
                    resp = "EXITO: Se cargo con el archivo con " + encuestas.Count + " encuestas de caracterización.";
                }
                else
                {
                    resp = "ERROR: Se leyeron " + encuestas.Count + " encuestas de caracterización, pero no fue posible guardar el archivo: "+ statusSave;
                }
                    
            }
            catch (HttpRequestException e){
                resp = "ERROR: " + e.Message;
            }
            return resp;
        }

        private async Task<String> UpdateDataFileAssoc()
        {
            //Carga los datos de conexión desde la configuración 
            var config = await db.Configuracion.FirstOrDefaultAsync();
            var mapParams = JsonConvert.DeserializeObject<AsociacionMap>(config.KoboParamsMapAssociation);
            var fields = JsonConvert.SerializeObject(new string[] { mapParams.IdKobo, mapParams.User, mapParams.LocationCode, mapParams.Datetime, mapParams.Name});
            var url = config.KoboKpiUrl + "/assets/" + config.KoboAssetUidAssociation + "/submissions/?format=json&fields=" + HttpUtility.UrlEncode(fields);


            String resp;

            try
            {

                //Consulta la información
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Token " + config.KoboApiToken);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(responseBody);
                //Valida que se cargaran resultados
                if (data == null) { return "ERROR: No fue posible recuperar la información"; }

                //Mapea los resultados
                var encuestas = new List<AsociacionMap>();

                foreach (var result in data)
                {
                    encuestas.Add(new AsociacionMap()
                    {
                        IdKobo = (String)result[mapParams.IdKobo],
                        User = (String)result[mapParams.User],
                        LocationCode = (String)result[mapParams.LocationCode],
                        Datetime = (String)result[mapParams.Datetime],
                        Name = (String)result[mapParams.Name]
                    });
                }

                //Guarda la información mapeada en el archivo .json
                var dataFile = JsonConvert.SerializeObject(encuestas);
                var statusSave = SaveDataFile(dataFile, FILE_ASOCIACION);
                if (statusSave == "")
                {
                    resp = "EXITO: Se cargo con el archivo con " + encuestas.Count + " encuestas  de asociaciones.";
                }
                else
                {
                    resp = "ERROR: Se leyeron " + encuestas.Count + " encuestas de asociaciones, pero no fue posible guardar el archivo: " + statusSave;
                }

            }
            catch (HttpRequestException e)
            {
                resp = "ERROR: " + e.Message;
            }
            return resp;
        }


        private String  SaveDataFile(String data, String file)
        {
            var error = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");

                if (!Directory.Exists(_path))
                {
                   Directory.CreateDirectory(_path);
                }

                
                System.IO.File.Delete(Path.Combine(_path, file + ".json"));
                System.IO.File.WriteAllText(Path.Combine(_path, file + ".json"), data);
            }
            catch (Exception e) {
                error = e.Message;
            }
            return error;
        }

        private async Task<List<EncuestaDataModel>> GetListadoEncuestas(String code = null)
        {
            var resp = new List<EncuestaDataModel>();

            //Consulta el archivo y valida que tenga datos.
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                text = System.IO.File.ReadAllText(Path.Combine(_path, FILE_CARACTERIZACION + ".json"));
            }
            catch (Exception) { }

            if (text != "")
            {
                var data = JsonConvert.DeserializeObject<List<EncuestaMap>>(text);
                if (data.Count > 0)
                {
                    //Filtra los datos
                    var user = await userManager.FindByNameAsync(User.Identity.Name);
                    var encuestadorControl = new EncuestadorController(db, userManager, _env);
                    var respRelacionado = await encuestadorControl.GetResponsablesbyIdParent(user.IDDependencia, 1, 3);
                    var cedulasValidas = await db.Pollster.Where(n => respRelacionado.Contains(n.IdResponsable)).Select(n => n.DNI.ToString()).ToListAsync();

                    var dataFiltered = code != null ? data.Where(n => n.User == code).ToList() : data.Where(n => cedulasValidas.Contains(n.User)).ToList();

                    if (dataFiltered.Count > 0)
                    {
                        var codesLocation = dataFiltered.Select(n => n.LocationCode).Distinct().ToList();
                        var locations = await db.Location.Where(n => codesLocation.Contains(n.Code2))
                            .Select(n => new
                            {
                                Code2 = n.Code2,
                                Mun = n.Name,
                                Dep = n.LocationParent != null ? n.LocationParent.Name : ""
                            }).ToDictionaryAsync(n => n.Code2, n => n);

                        var idsKobo = dataFiltered.Select(n => n.IdKobo).ToList();
                        var formalizaciones = await db.Formalization.Where(n => idsKobo.Contains(n.IdKobo))
                            .Select(n => new
                            {
                                n.IdKobo,
                                n.Id,
                                n.Estado
                            }).ToDictionaryAsync(n => n.IdKobo, n => n);


                        //Permisos de columnas
                        var verValidacion = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver" && c.Value == "1"));

                        foreach (var item in dataFiltered)
                        {
                            var encuesta = new EncuestaDataModel
                            {
                                IdKobo = item.IdKobo,
                                User = item.User,
                                DNI = item.DNI,
                                LocationCode = item.LocationCode,
                                Datetime = item.Datetime,
                                Mun = item.LocationCode,
                                Dep = "",
                                Validation = verValidacion ? item.Validation != null && item.Validation != "" : false,
                                Status = item.Validation != null && item.Validation != "" ? "Pend." : "No"
                            };
                            //Completa los municipios y departamentos
                            if (locations.ContainsKey(item.LocationCode))
                            {
                                var aux = locations[item.LocationCode];
                                encuesta.Mun = aux.Mun;
                                encuesta.Dep = aux.Dep;
                            }
                            //Completa las formalizaciones
                            if (formalizaciones.ContainsKey(item.IdKobo))
                            {
                                var aux = formalizaciones[item.IdKobo];
                                encuesta.FormalizacionId = aux.Id;
                                encuesta.FormalizacionEstado = aux.Estado;
                                encuesta.Status = "Si";
                            }                         
                            resp.Add(encuesta);
                        }
                    }

                }

            }

            return resp;
        }

        private async Task<List<AsociacionDataModel>> GetListadoAsociaciones(String code = null)
        {
            var resp = new List<AsociacionDataModel>();

            //Consulta el archivo y valida que tenga datos.
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                text = System.IO.File.ReadAllText(Path.Combine(_path, FILE_ASOCIACION + ".json"));
            }
            catch (Exception) { }

            if (text != "")
            {
                var data = JsonConvert.DeserializeObject<List<AsociacionMap>>(text);
                if (data.Count > 0)
                {
                    //Filtra los datos
                    var user = await userManager.FindByNameAsync(User.Identity.Name);
                    var encuestadorControl = new EncuestadorController(db, userManager, _env);
                    var respRelacionado = await encuestadorControl.GetResponsablesbyIdParent(user.IDDependencia, 1, 3);
                    var cedulasValidas = await db.Pollster.Where(n => respRelacionado.Contains(n.IdResponsable)).Select(n => n.DNI.ToString()).ToListAsync();

                    var dataFiltered = code != null ? data.Where(n => n.User == code).ToList() : data.Where(n => cedulasValidas.Contains(n.User)).ToList();

                    if (dataFiltered.Count > 0)
                    {
                        var codesLocation = dataFiltered.Select(n => n.LocationCode).Distinct().ToList();
                        var locations = await db.Location.Where(n => codesLocation.Contains(n.Code2))
                            .Select(n => new
                            {
                                n.Code2,
                                Mun = n.Name,
                                Dep = n.LocationParent != null ? n.LocationParent.Name : ""
                            }).ToDictionaryAsync(n => n.Code2, n => n);

                        var idsKobo = dataFiltered.Select(n => n.IdKobo).ToList();
                    
                        foreach (var item in dataFiltered)
                        {
                            var asociacion = new AsociacionDataModel
                            {
                                IdKobo = item.IdKobo,
                                User = item.User,
                                LocationCode = item.LocationCode,
                                Datetime = item.Datetime,
                                Mun = item.LocationCode,
                                Dep = "",
                                Name = item.Name
                            };
                            //Completa los municipios y departamentos
                            if (locations.ContainsKey(item.LocationCode))
                            {
                                var aux = locations[item.LocationCode];
                                asociacion.Mun = aux.Mun;
                                asociacion.Dep = aux.Dep;
                            }
                            
                            resp.Add(asociacion);
                        }
                    }

                }

            }

            return resp;
        }


        public Dictionary<String, int> GetTotalEncuestas()
        {
            var resp = new Dictionary<String, int>();

            //Consulta el archivo y valida que tenga datos.
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                text = System.IO.File.ReadAllText(Path.Combine(_path, FILE_CARACTERIZACION + ".json"));
            }
            catch (Exception) { }

            if (text != "")
            {
                var data = JsonConvert.DeserializeObject<List<EncuestaMap>>(text);
                
                if (data.Count > 0)
                {
                    var dataAux = data.Where(n => n.User != null).ToList();
                    resp = dataAux.GroupBy(n => n.User).ToDictionary(g => g.Key, g => g.Count());

                }

            }

            return resp;
        }
        public Dictionary<String, int> GetTotalAsociaciones()
        {
            var resp = new Dictionary<String, int>();

            //Consulta el archivo y valida que tenga datos.
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                text = System.IO.File.ReadAllText(Path.Combine(_path, FILE_ASOCIACION + ".json"));
            }
            catch (Exception) { }

            if (text != "")
            {
                var data = JsonConvert.DeserializeObject<List<AsociacionMap>>(text);
                if (data.Count > 0)
                {
                    var dataAux = data.Where(n => n.User != null).ToList();
                    resp = dataAux.GroupBy(n => n.User).ToDictionary(g => g.Key, g => g.Count());

                }

            }

            return resp;
        }

        private string DownloadFile(string remoteUri, string fileName, string path, string relative, String token)
        {
            var relativePath = "";

            try
            {
                string myStringWebResource = remoteUri + fileName;
                WebClient myWebClient = new WebClient();
                myWebClient.Headers.Add("User-Agent: Other");
                myWebClient.Headers.Add(HttpRequestHeader.Authorization, "token  " + token);
                var fullPath = Path.Combine(path, fileName);
                myWebClient.DownloadFile(myStringWebResource, fullPath);

                relativePath = Path.Combine(relative, fileName);
            }
            catch (Exception e) {
                var a = e.Message;
            }

            return relativePath;
        }


    }
}
