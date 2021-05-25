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

        [Authorize(Policy = "Encuestador.Ver")]
        public async Task<ActionResult> ListadoAjax(String code = null)
        {
            var resp = new List<EncuestaDataModel>();

            //Valida la consulta y los permisos
            var verInforme = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Informes.Encuestadores" && c.Value == "1"));
            if (code == null && !verInforme) { return Json(resp); }

            //Consulta el archivo y valida que tenga datos.
            var text = "";
            try
            {
                var _path = Path.Combine(_env.ContentRootPath, "Storage");
                text = System.IO.File.ReadAllText(Path.Combine(_path, "data.json"));
            }
            catch (Exception){}

            if(text != "")
            {
                var data = JsonConvert.DeserializeObject<List<EncuestaMap>>(text);
                if(data.Count > 0)
                {
                    //Filtra los datos
                    var dataFiltered = code != null ? data.Where(n => n.Id == code).ToList(): data;                        

                    if(dataFiltered.Count > 0)
                    {
                        var codesLocation = dataFiltered.Select(n => n.LocationCode).Distinct().ToList();
                        var locations = await db.Location.Where(n => codesLocation.Contains(n.Code2))
                            .Select(n => new
                            {
                                Code2 = n.Code2,
                                Mun = n.Name,
                                Dep = n.LocationParent != null ? n.LocationParent.Name : ""
                            }).ToDictionaryAsync(n => n.Code2, n => n);

                        //Permisos de columnas
                        var verValidacion = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver" && c.Value == "1"));

                        foreach (var item in dataFiltered)
                        {
                            var encuesta = new EncuestaDataModel
                            {
                                Id = item.Id,
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
                            resp.Add(encuesta);
                        }
                    }
                    
                }
                
            }

            return Json(resp);
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

        [HttpGet]
        public ActionResult GetParams(String id = "id", String location = "location", String datetime = "datetime", String validation = "validation")
        {
            var data = new EncuestaMap
            {
                Id = id,
                LocationCode = location,
                Datetime = datetime,
                Validation = validation
            };
            return Json(data);
        }

        private async Task<String> UpdateDataFile()
        {
            //Carga los datos de conexión desde la configuración 
            var config = await db.Configuracion.FirstOrDefaultAsync();
            var mapParams = JsonConvert.DeserializeObject<EncuestaMap>(config.KoboParamsMap);
            var fields= JsonConvert.SerializeObject(new string[] { mapParams.Id, mapParams.LocationCode, mapParams.Datetime, mapParams.Validation });
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
                        Id = (String)result[mapParams.Id],
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

        [Authorize(Policy = "Kobo.Actualizar")]
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
            catch (Exception e) {
                error = e.Message;
            }
            ViewBag.Error = error;
            ViewBag.Accion = accion;
            return View();
        }

        [HttpGet]
        public async Task<bool> Auto(string auth)
        {
            if (auth == "#51kS7.Jms22" )
            {
                var fecha = DateTime.Now.ToString("r");
                var resp = await UpdateDataFile();
                var accion = "[" + fecha + "] "+ resp;

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
                catch (Exception){}
                return true;
            }
            return false;
        }


        public int TotalEncuestador(String code )
        {
            int resp = 0;

           
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
                    var dataFiltered = code != null ? data.Where(n => n.Id == code).ToList() : data;

                    if (dataFiltered.Count > 0)
                    {
                        resp = dataFiltered.Count;


                    }

                }

            }

            return resp;
        }
    }
}
