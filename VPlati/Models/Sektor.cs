using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VPlati.Models
{
    public class Sektor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime sektorja je obvezen podatek!")]
        [MinLength(1, ErrorMessage = "Ime sektorja mora vsebovati vsaj 1 znak!")]
        public string ImeSektorja { get; set; }
        public List<Smer> Smeri { get; set; }
        public int PlezalisceId { get; set; }
        public Plezalisce Plezalisce { get; set; }
    }
}
