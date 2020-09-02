using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VPlati.Models
{
    public class OcenaSmeri
    {
        //razred narejen zaradi relacije many to many
        public int Id { get; set; }
        public DateTime OcenaSmeriDatum { get; set; }
        public int NacinPreplezanjaSmeriId { get; set; }
        public NacinPreplezanjaSmeri NacinPreplezanjaSmeri { get; set; }
        public int SmerId { get; set; }
        public Smer Smer { get; set; }
        public int OcenaId { get; set; }
        public Ocena Ocena { get; set; }
        public int PlezalecId { get; set; }
        public Plezalec Plezalec { get; set; }       
    }
}
