using App_consulta.Data;
using App_consulta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Controllers
{
    public class LogsController : Controller
    {

        private readonly ApplicationDbContext db;
      


        public LogsController(ApplicationDbContext context)
        {
            db = context;
          
        }

        


        [Authorize(Policy = "Configuracion.Logs")]
        public async Task<ActionResult> Index(string resultado)
        {
            string error = (string)HttpContext.Session.GetComplex<string>("error");
            if (error != "")
            {
                ViewBag.error = error;
                HttpContext.Session.Remove("error");
            }
            var logs = await db.Log.OrderByDescending(n => n.Fecha).Take(100).ToListAsync();
            return View(logs);
        }

        [Authorize(Policy = "Configuracion.Logs")]
        public async Task<ActionResult> Details(int Id)
        {
            LogModel log = await db.Log.FindAsync(Id);
            if (log == null) { return NotFound(); }
            ViewBag.Old = log.ValAnterior==null?"": JsonConvert.SerializeObject(JsonConvert.DeserializeObject(log.ValAnterior), Formatting.Indented);
            ViewBag.New = log.ValNuevo == null ? "" : JsonConvert.SerializeObject(JsonConvert.DeserializeObject(log.ValNuevo), Formatting.Indented);
            return View(log);
        }
    }
}
