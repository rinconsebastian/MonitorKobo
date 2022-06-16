using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Models
{
    public class Configuracion
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Display(Name = "Imagen logo")]
        public string Logo { get; set; }

        [Display(Name = "Favicon")]
        public string Favicon { get; set; }

        [Display(Name = "Imagen encabezado")]
        public string ImgHeader { get; set; }

        [Display(Name = "Color Texto Cabecera")]
        public string ColorTextoHeader { get; set; }

        [Display(Name = "Color  Principal")]
        public string ColorPrincipal { get; set; }

        [Display(Name = "Color Texto Principal")]
        public string ColorTextoPrincipal { get; set; }


        [Required]
        [Display(Name = "Email  contacto")]
        public string Contacto { get; set; }

        [Required]
        [Display(Name = "Nombre entidad")]
        public string Entidad { get; set; }

        [Required]
        [Display(Name = "Nombre plan estratégico")]
        public string NombrePlan { get; set; }


        [Required]
        [Display(Name = "Nombre aplicación")]
        public string NombreApp { get; set; }

        [Required]
        [Display(Name = "Descrición aplicación")]
        public string DescripcionApp { get; set; }

        [Required]
        [Range(0, 99999,ErrorMessage = "El valor del {0} debe estar entre {1} y {2}.")]
        [Display(Name = "Código Encuestador")]
        public int CodeEncuestador { get; set; }

        [Display(Name = "KPI Url")]
        public string KoboKpiUrl { get; set; }

        [Display(Name = "Api Token")]
        public string KoboApiToken { get; set; }

        [Display(Name = "Asset UID (Caracterización)")]
        public string KoboAssetUid { get; set; }

        [Display(Name = "Asset UID (Asociaciones)")]
        public string KoboAssetUidAssociation { get; set; }

        [Display(Name = "Attachment URL")]
        public string KoboAttachment { get; set; }

        [Display(Name = "Attachment User")]
        public string KoboUsername { get; set; }

        [Display(Name = "Params Map")]
        public string KoboParamsMap { get; set; }

        [Display(Name = "Params Map (Asociaciones)")]
        public string KoboParamsMapAssociation { get; set; }

    }
}
