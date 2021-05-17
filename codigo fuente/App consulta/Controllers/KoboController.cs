using App_consulta.Data;
using App_consulta.Models;
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

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment _env;

        public KoboController(ApplicationDbContext context,UserManager<ApplicationUser> _userManager, IWebHostEnvironment env)
        {
            db = context;
            userManager = _userManager;
            _env = env;
        }

        //Listados de encuestas
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
         
                //Mapea los resultados
                var formalizacion = new Formalization()
                {
                    Estado = Formalization.ESTADO_BORRADOR,
                    CreateDate = DateTime.Now,
                    LastEditDate = DateTime.Now,
                    IdResponsable = user.IDDependencia,
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
                        oProperty.SetValue(formalizacion, value, null);
                    }
                }
              
                if(imageErrors){r.Message = "ERROR: No fue posible cargar los archivos adjuntos"; return r; }

                try
                {
                    db.Formalization.Add(formalizacion);
                    await db.SaveChangesAsync();
                    r.Url = "Formalizacion/Edit/" + formalizacion.Id;
                    r.Success = true;
                } catch(Exception e){r.Message = e.InnerException.Message; return r;}
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
            var fecha = DateTime.Now.ToString("r");
            var resp = await UpdateDataFile();
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
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            ViewBag.Error = error;
            ViewBag.Accion = accion;
            return View();
        }

        [HttpGet]
        public async Task<bool> Auto(string auth)
        {
            if (auth == "#51kS7.Jms22")
            {
                var fecha = DateTime.Now.ToString("r");
                var resp = await UpdateDataFile();
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
                }
                catch (Exception) { }
                return true;
            }
            return false;
        }

        public String GetDatetimeData()
        {
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                DateTime creation = System.IO.File.GetCreationTime(Path.Combine(_path, "data.json"));
                text = creation.ToString("f");
            }
            catch (Exception) { }
            return text;
        }

        //Extra
        private async Task<String> UpdateDataFile()
        {
            //Carga los datos de conexión desde la configuración 
            var config = await db.Configuracion.FirstOrDefaultAsync();
            var mapParams = JsonConvert.DeserializeObject<EncuestaMap>(config.KoboParamsMap);
            var fields= JsonConvert.SerializeObject(new string[] { mapParams.IdKobo, mapParams.User, mapParams.LocationCode, mapParams.Datetime, mapParams.Validation });
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
                    });
                }

                //Guarda la información mapeada en el archivo data.json
                var dataFile = JsonConvert.SerializeObject(encuestas);
                var statusSave = SaveDataFile(dataFile);
                if (statusSave == "")
                {
                    resp = "EXITO: Se cargo con el archivo con " + encuestas.Count + " encuestas.";
                }
                else
                {
                    resp = "ERROR: Se leyeron " + encuestas.Count + " encuestas, pero no fue posible guardar el archivo: "+ statusSave;
                }
                    
            }
            catch (HttpRequestException e){
                resp = "ERROR: " + e.Message;
            }
            return resp;
        }

        private String  SaveDataFile(String data)
        {
            var error = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");

                if (!Directory.Exists(_path))
                {
                   Directory.CreateDirectory(_path);
                }

                
                System.IO.File.Delete(Path.Combine(_path, "data.json"));
                System.IO.File.WriteAllText(Path.Combine(_path, "data.json"), data);
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
                text = System.IO.File.ReadAllText(Path.Combine(_path, "data.json"));
            }
            catch (Exception) { }

            if (text != "")
            {
                var data = JsonConvert.DeserializeObject<List<EncuestaMap>>(text);
                if (data.Count > 0)
                {
                    //Filtra los datos
                    var dataFiltered = code != null ? data.Where(n => n.User == code).ToList() : data;

                    if (dataFiltered.Count > 0)
                    {
                        var codesLocation = dataFiltered.Select(n => n.LocationCode).Distinct().ToList();
                        var locations = await db.Location.Where(n => codesLocation.Contains(n.Code.ToString()))
                            .Select(n => new
                            {
                                Code = n.Code.ToString(),
                                Mun = n.Name,
                                Dep = n.LocationParent != null ? n.LocationParent.Name : ""
                            }).ToDictionaryAsync(n => n.Code, n => n);

                        var idsKobo = dataFiltered.Select(n => n.IdKobo).ToList();
                        var formalizaciones = await db.Formalization.Where(n => idsKobo.Contains(n.IdKobo))
                            .Select(n => new
                            {
                                n.IdKobo,
                                n.Id
                            }).ToDictionaryAsync(n => n.IdKobo, n => n.Id);


                        //Permisos de columnas
                        var verValidacion = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver" && c.Value == "1"));

                        foreach (var item in dataFiltered)
                        {
                            var encuesta = new EncuestaDataModel
                            {
                                IdKobo = item.IdKobo,
                                User = item.User,
                                LocationCode = item.LocationCode,
                                Datetime = item.Datetime,
                                Mun = item.LocationCode,
                                Dep = "",
                                Validation = verValidacion ? item.Validation != null && item.Validation != "" : false
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
                                encuesta.FormalizacionId = formalizaciones[item.IdKobo];
                            }
                            resp.Add(encuesta);
                        }
                    }

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
