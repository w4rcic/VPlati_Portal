using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace VPlati.Models
{
    public class Smer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime smeri je obvezen podatek!")]
        [MinLength(2, ErrorMessage = "Ime mora biti dolgo vsaj 2 znaka!")]
        public string ImeSmeri { get; set; }
        [Required(ErrorMessage = "Dolžina smeri je obvezen podatek!")]
        [Range(5,50,ErrorMessage = "Vnesite dolžino smeri med 5-50 metrov!")]
        public int Dolzina { get; set; }
        public IEnumerable<OcenaSmeri> OceneSmeri { get; set; }
        [Required(ErrorMessage = "Datum prvega vzpona je obvezen podatek!")]
        public DateTime PrviVzpon { get; set; }
        [MinLength(2, ErrorMessage = "Ime mora biti dolgo vsaj 2 znaka!")]
        public string Opremjevalec { get; set; }
        public IEnumerable<Komentar> Komentarji { get; set; }
        public int SektorId { get; set; }
        public Sektor Sektor { get; set; }
        public int SteviloVzponov { get; set; } = 0;
        public int PovprecnaOcena => PovOcena();
        private int PovOcena()
        {
            double povOcenaSmeri = 0;
            double st = 0;
            foreach (var ocena in OceneSmeri)
            {
                povOcenaSmeri += ocena.OcenaId;
                st++;
            }
            if (st != 0)
                return (int)Math.Round(povOcenaSmeri / st)-1;
            return 0;
        }
    }
}
