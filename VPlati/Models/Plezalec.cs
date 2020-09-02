using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPlati.Models
{
    public class Plezalec:IdentityUser<int>
    {
        [Required(ErrorMessage = "Ime je obvezen podatek.")]
        [Display(Name = "Ime")]
        [StringLength(15, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 3)]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Priimek je obvezen podatek.")]
        [Display(Name = "Priimek")]
        [StringLength(15, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 3)]
        public string Priimek { get; set; }
        [Display(Name = "O meni")]
        [StringLength(300, ErrorMessage = "{0} mora biti dolgo vsaj {2} in ne več kot {1} znakov.", MinimumLength = 15)]
        public string PlezalecInfo { get; set; }
        public string SlikaPlezalca { get; set; }
        public DateTime DatumRojstva { get; set; }
        public DateTime DatumRegistracije { get; set; }
    }
}
