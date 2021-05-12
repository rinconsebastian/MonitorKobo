using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App_consulta.Models
{
    public class FormalizationConfig
    {

        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Campo DB")]
        [Required]
        public string Field { get; set; }

        [Display(Name = "Campo Kobo")]
        [Required]
        public string Value { get; set; }
    }
}
