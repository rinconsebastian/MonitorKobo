using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Models
{
    public class Pollster
    {

        [Required]
        [Key]
        public int Id { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Correo electrónico")]
        public string email { get; set; }



        [Display(Name = "Cedula")]
        [Required]
        public int DNI { get; set; }

        [Display(Name = "Código")]
        public string code { get; set; }


        [Display(Name = "Municipio")]
        public int IdLocation { get; set; }
        [ForeignKey("IdLocation")]
        public virtual Location LocationParent { get; set; }


        [Display(Name = "Coordinación")]
        public int IdResponsable { get; set; }
        [ForeignKey("IdResponsable")]
        public virtual Responsable Responsable { get; set; }


        [Display(Name = "Usuario registro")]
        public string IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Fecha registro")]
        public DateTime creationDate { get; set; }
    }
}
