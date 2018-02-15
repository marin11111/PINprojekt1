using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Priprema1.Models
{
    public class zanr
    {
        public int id { get; set; }
        [Required, MaxLength(15, ErrorMessage = "15 znakova!")]
        public string Naziv { get; set; }
    }
}
