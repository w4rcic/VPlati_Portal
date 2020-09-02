using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VPlati.Models
{
    public class Komentar
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Vnesi komentar!")]
        [StringLength(300, ErrorMessage = "Komentar mora biti dolg vsaj {2} in ne več kot {1} znakov.", MinimumLength = 5)]
        public string KomentarText { get; set; }
        public string KomentarUser { get; set; }
        public DateTime KomentarDatum { get; set; }
        public int SmerId { get; set; }
        public Smer Smer { get; set; }
        public int PlezalecId { get; set; }
        public Plezalec Plezalec { get; set; }
    }
}
