using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VPlati.Models
{
    public class Plezalisce
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime plezališča je obvezen podatek.")]
        [MinLength(3)]
        public string ImePlezalisca { get; set; }

        [Required(ErrorMessage = "Vnesi nadmorsko višino plezališča v metrih.")]
        [Range(260,3000, ErrorMessage = "Vnesite nadmorsko višino plezališča, med 260 m - 3000 m.")]
        public int Visina { get; set; }

        [Required(ErrorMessage = "Vnesi čas dostopa v minutah.")]
        [Range(1, 120, ErrorMessage = "Vnesite čas dostopa v minutah med 1 in 120 minut.")]
        public int CasDostopa { get; set; }
        public string OpisDostopa { get; set; }
        public string SlikaPlezalisca { get; set; }
        public IEnumerable<Sektor> Sektorji { get; set; }
        public IEnumerable<Opozorilo> Opozorila { get; set; }
        public UsmerjenostStene UsmerjenostStene { get; set; }
        public int[] PovOcene => IzracunajTezavnost();

        private int[] IzracunajTezavnost()
        {
            int stirke = 0;
            int petke = 0;
            int sestke = 0;
            int sedemke = 0;
            int osemke = 0;
            int devetke = 0;
            foreach (var sektor in Sektorji)
            {
                foreach (var smer in sektor.Smeri)
                {
                    if (smer.PovprecnaOcena < 4)
                        stirke += 1;
                    else if (smer.PovprecnaOcena < 7)
                        petke += 1;
                    else if (smer.PovprecnaOcena < 12)
                        sestke += 1;
                    else if (smer.PovprecnaOcena < 18)
                        sedemke += 1;
                    else if (smer.PovprecnaOcena < 24)
                        osemke += 1;
                    else
                        devetke += 1;
                }
            }
            int[] x = { stirke, petke, sestke, sedemke, osemke, devetke };
            return x;
        }
        public int IzracunajPriljubljenost()
        {
            int count = 0;
            foreach(var sektor in Sektorji)
            {
                foreach(var smer in sektor.Smeri)
                {
                    foreach(var ocenaSmeri in smer.OceneSmeri)
                    {
                        count++;
                    }
                }
            }
            if (count > 30)
                return 5;
            if (count > 15)
                return 4;
            if (count > 10)
                return 3;
            if (count > 5)
                return 2;
            return 1;
        }
        public int VrniStSmeri()
        {
            int st = 0;
            foreach(var sektor in Sektorji)
            {
                foreach(var smer in sektor.Smeri)
                {
                    st++;
                }
            }
            return st;
        }
    }
}