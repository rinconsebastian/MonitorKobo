using App_consulta.Data;
using App_consulta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        public async Task Registrar(RegistroLog registro,Type oType = null)
        {
            await Task.Run(() => {

                var anterior = registro.ValAnterior != null ? JsonSerializer.Serialize(registro.ValAnterior) : "";
                var nuevo = registro.ValNuevo != null ? JsonSerializer.Serialize(registro.ValNuevo) : "";

                if(oType != null && anterior != "" && nuevo != "")
                {
                    var arrAnterior = new Dictionary<String, String>();
                    var arrNuevo = new Dictionary<String, String>();

                    foreach (var oProperty in oType.GetProperties())
                    {
                        var oOldValue = oProperty.GetValue(registro.ValAnterior, null);
                        var oNewValue = oProperty.GetValue(registro.ValNuevo, null);
                        
                        if (!object.Equals(oOldValue, oNewValue))
                        {
                            var sOldValue = oOldValue == null ? "null" : oOldValue.ToString();
                            var sNewValue = oNewValue == null ? "null" : oNewValue.ToString();

                            arrAnterior.Add(oProperty.Name, sOldValue);
                            arrNuevo.Add(oProperty.Name, sNewValue);
                        }
                    }
                    anterior = arrAnterior.Count > 0 ? JsonSerializer.Serialize(arrAnterior) : "";
                    nuevo = arrNuevo.Count > 0 ? JsonSerializer.Serialize(arrNuevo) : "";
                }
                if(anterior != "" || nuevo != "")
                {
                    try
                    {
                        LogModel logn = new LogModel
                        {

                            Usuario = registro.Usuario,
                            Fecha = DateTime.Now,
                            Accion = registro.Accion,
                            Modelo = registro.Modelo,
                            ValAnterior = anterior,
                            ValNuevo = nuevo
                        };

                        db.Add(logn);
                        db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        var n = e;
                    }
                }

            });
        }



    }
}
