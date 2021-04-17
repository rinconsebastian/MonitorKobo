using App_consulta.Data;
using App_consulta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace App_consulta.Services
{
    public class Logger
    {
        private readonly ApplicationDbContext db;

        public Logger(ApplicationDbContext context)
        {
            db = context;

        }

        public async Task Registrar(RegistroLog registro)
        {
            await Task.Run(() => {

                var nuevo = System.Text.Json.JsonSerializer.Serialize(registro.ValNuevo);

                try
                {
                    LogModel logn = new LogModel
                    {

                        Usuario = registro.Usuario,
                        Fecha = DateTime.Now,
                        Accion = registro.Accion,
                        Modelo = registro.Modelo,
                        ValAnterior = registro.ValAnterior,
                        ValNuevo = nuevo
                    };

                    db.Add(logn);
                    db.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    var n = e;
                }

            });
        }



    }
}
