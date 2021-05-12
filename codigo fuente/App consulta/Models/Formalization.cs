using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Models
{
    public class Formalization
    {
        public const int ESTADO_BORRADOR = 1;
        public const int ESTADO_COMPLETO = 2;
        public const int ESTADO_CANCELADO = 3;


        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Id Kobo")]
        public string IdKobo { get; set; }

        [Display(Name = "Número registro")]
        public string NumeroRegistro { get; set; }

        [Display(Name = "Nombre y apellidos")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Fecha solicitud")]
        [Required]
        public string FechaSolicitud { get; set; }

        [Display(Name = "Cédula")]
        [Required]
        public string Cedula { get; set; }


        [Display(Name = "Departamento")]
        public string Departamento { get; set; }


        [Display(Name = "Municipio")]
        public string Municipio { get; set; }


        [Display(Name = "Área de pesca")]
        public string AreaPesca { get; set; }


        [Display(Name = "Artes de pesca")]
        public string ArtesPesca { get; set; }


        [Display(Name = "Nombre asociación")]
        public string NombreAsociacion { get; set; }


        [Display(Name = "Fotografía pescador")]
        public string ImgPescador { get; set; }

        [Display(Name = "Solicitud de carnetización")]
        public string ImgSolicitudCarnet { get; set; }

        [Display(Name = "Certificación")]
        public string ImgCertificacion { get; set; }

        [Display(Name = "Foto Cédula (Anverso)")]
        public string ImgCedulaAnverso { get; set; }

        [Display(Name = "Foto Cédula (Reverso)")]
        public string ImgCedulaReverso { get; set; }

        [Display(Name = "Firma digital")]
        public string ImgFirmaDigital { get; set; }

        [Display(Name = "Estado")]
        public int Estado { get; set; }
    }
}
