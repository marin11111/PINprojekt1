using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Priprema1.Models
{
    public class Films
    {
        public int id { get; set; }
        public int Id { get; internal set; }
        [Required]
        [StringLength (15, ErrorMessage ="maks 15 znakova")]
        public string Naziv { get; set; }
        public int Godina { get; set; }
        public int Zanrid { get; set; }
        [Display (Name = "Žanr")]
        public zanr Zanr { get; set; }
    }
}
