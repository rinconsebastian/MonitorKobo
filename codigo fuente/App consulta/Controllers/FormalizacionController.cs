using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_consulta.Data;
using App_consulta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace App_consulta.Controllers
{
    public class FormalizacionController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment _env;

        public FormalizacionController(ApplicationDbContext context, UserManager<ApplicationUser> _userManager, IWebHostEnvironment env)
        {
            db = context;
            userManager = _userManager;
            _env = env;
        }

        [Authorize(Policy = "Formalizacion.Validar")]
        [HttpPost]
        public async Task<IActionResult> Cargar(string idKobo)
        {
            var r = new RespuestaAccion();

            var formalizacion = await db.Formalization.Where(n => n.IdKobo == idKobo).FirstOrDefaultAsync();

            if(formalizacion == null)
            {
                var kobo = new KoboController(db, userManager, _env);
                r = await kobo.LoadFormalizacion(idKobo, User.Identity.Name);
                return Json(r);
            }
            
            r.Url = "Formalizacion/Edit/" + formalizacion.Id;
            r.Success = true;
            return Json(r);
        }


        [Authorize(Policy = "Formalizacion.Listado")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Policy = "Formalizacion.Listado")]
        public async Task<IActionResult> Ajax()
        {
            var formalizaciones = await db.Formalization.ToListAsync();
            return Json(formalizaciones);
        }


        [Authorize(Policy = "Formalizacion.Ver")]
        public async Task<ActionResult> Details(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var encuestadorControl = new EncuestadorController(db, userManager, _env);
            var respRelacionado = await encuestadorControl.GetResponsablesbyIdParent(user.IDDependencia, 1, 3);

            var formalizacion = await db.Formalization.Where(n => n.Id == id && respRelacionado.Contains(n.IdResponsable)).FirstOrDefaultAsync();
            if (formalizacion == null) { return NotFound(); }

            return View(formalizacion);
        }

        [Authorize(Policy = "Formalizacion.Imprimir")]
        public async Task<ActionResult> Print(int[] ids)
        {
            var formalizaciones = await db.Formalization.Where(n => ids.Contains(n.Id)).ToListAsync();
            return View(formalizaciones);
        }



        // GET: EncuestadorController/Edit/5
        [Authorize(Policy = "Formalizacion.Validar")]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var encuestadorControl = new EncuestadorController(db, userManager, _env);
            var respRelacionado = await encuestadorControl.GetResponsablesbyIdParent(user.IDDependencia, 1, 3);

            var formalizacion = await db.Formalization.Where(n => n.Id == id && respRelacionado.Contains(n.IdResponsable)).FirstOrDefaultAsync();
            if (formalizacion == null) { return NotFound(); }

            ViewBag.Coordinaciones = new SelectList(await db.Responsable.Where(n => n.Nombre.StartsWith("[CDR]")).OrderBy(n => n.Nombre).ToListAsync(), "Id", "Nombre", formalizacion.IdResponsable);

            var estados = new Dictionary<int, string>{
                {  Formalization.ESTADO_BORRADOR, "Borrador" },
                {  Formalization.ESTADO_COMPLETO, "Completo" },
                {  Formalization.ESTADO_CANCELADO, "Cancelado" }
            };

            ViewBag.Estados = new SelectList(estados, "Key", "Value", formalizacion.Estado);

            return View(formalizacion);

        }

       
        [Authorize(Policy = "Formalizacion.Validar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( FormalizacionPostModel formalizacion)
        {
            var original = await db.Formalization.FindAsync(formalizacion.Id);
            if (original == null)
            {
                ModelState.AddModelError(string.Empty, "Formalización no registrada.");
            }
            if (ModelState.IsValid)
            {
                var permisoEditar = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Editar" && c.Value == "1"));
                if (permisoEditar)
                {
                    original.Name = formalizacion.Name;
                    original.Cedula= formalizacion.Cedula;
                    original.NombreAsociacion = formalizacion.NombreAsociacion;
                    original.AreaPesca= formalizacion.AreaPesca;
                    original.ArtesPesca = formalizacion.ArtesPesca;
                }

                original.Estado = formalizacion.Estado;

                if (formalizacion.IdResponsable > 0)
                {
                    original.IdResponsable = formalizacion.IdResponsable;
                }

                var user = await userManager.FindByNameAsync(User.Identity.Name);
                original.LastEditDate = DateTime.Now;
                original.LastEditByUser = user;

                db.Entry(original).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = formalizacion.Id });
            }
            
            ViewBag.Coordinaciones = new SelectList(await db.Responsable.Where(n => n.Nombre.StartsWith("[CDR]")).OrderBy(n => n.Nombre).ToListAsync(), "Id", "Nombre", formalizacion.IdResponsable);

            var estados = new Dictionary<int, string>{
                {  Formalization.ESTADO_BORRADOR, "Borrador" },
                {  Formalization.ESTADO_COMPLETO, "Completo" },
                {  Formalization.ESTADO_CANCELADO, "Cancelado" }
            };

            ViewBag.Estados = new SelectList(estados, "Key", "Value", formalizacion.Estado);

            return View(formalizacion);
        }
       

        [HttpGet]
        public FileStreamResult ViewImage(string filename)
        {
            var permiso = User.HasClaim(c => (c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver" && c.Value == "1"));
            if (!permiso) { filename = "lock.jpg"; }

            if (filename == null) { filename = "noFound.jpg"; }

            string filepath = Path.Combine(_env.ContentRootPath, "Storage", filename);

            byte[] data = System.IO.File.ReadAllBytes(filepath);

            Stream stream = new MemoryStream(data);
            return new FileStreamResult(stream, "image/jpeg");
        }


        [HttpPost]
        [Authorize(Policy = "Formalizacion.Validar")]
        public async Task<IActionResult> UpdateImage(IFormFile file, string filename)
        {
            var r = new RespuestaAccion();

            if (file != null && file.Length > 0)
            {
                string filepath = Path.Combine(_env.ContentRootPath, "Storage", filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                    try
                    {
                        using (var fileStream = new FileStream(filepath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        r.Success = true;
                    }
                    catch (Exception e) { r.Message = "Error: " + e.Message; }
                }
                else { r.Message = "Error: El archivo original no existe."; }
            }
            else { r.Message = "Error: El archivo no es válido."; }
            return Json(r);
        }
    }
}
