using System;
namespace App_consulta.Models
{
    public class EncuestadorViewModel
    {
        

        public int ID { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Municipio { get; set; }
        public string Departamento { get; set; }
        public string Coordinacion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int NumeroEncuestas { get; set; }
    
    }
}
