using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VPlati.Models
{
    public class Opozorilo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vnesite vsebino novega opozorila!")]
        [MinLength(12, ErrorMessage = "Ime mora biti dolgo vsaj 12 znakov!")]
        public string OpozoriloText { get; set; }
        public DateTime OpozoriloDatum { get; set; }
        public int PlezalisceId { get; set; }
        public Plezalisce Plezalisce { get; set; }
    }
}
