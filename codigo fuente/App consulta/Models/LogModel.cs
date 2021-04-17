using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Models
{
    public class LogModel
    {

        [Required]
        [Key]
        public int Id { get; set; }

        
       
        public string Usuario { get; set; }


        public DateTime Fecha { get; set; }


        public string Accion { get; set; }

        public string Modelo { get; set; }

        public string ValAnterior { get; set; }

        public string ValNuevo { get; set; }

    }
}
