﻿

using System.ComponentModel.DataAnnotations;

namespace App_consulta.Models
{
    public class EncuestaMap
    {
        public string IdKobo { get; set; }
        public string User { get; set; }
        public string LocationCode { get; set; }
        public string Datetime { get; set; }
        public string Validation { get; set; }
    }

    public class EncuestaDataModel
    {
        public string IdKobo { get; set; }
        public string User { get; set; }
        public string LocationCode { get; set; }
        public string Dep { get; set; }
        public string Mun { get; set; }
        public string Datetime { get; set; }
        public bool Validation { get; set; }
        public int FormalizacionId { get; set; }
    }

    public class EncuestadorDataModel
    {
        public int ID { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Coordinacion { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int NumeroEncuestas { get; set; }
    }


    public class FormalizacionPostModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string AreaPesca { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string ArtesPesca { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string NombreAsociacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public int Estado { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public int IdResponsable { get; set; }

    }
}
