using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;

namespace VPlati.Pages.Plezalisca
{
    public class PlezaliscaIndexBase : ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public string SearchTerm { get; set; } = "";
        public IEnumerable<Plezalisce> Plezalisca { get; set; }
        public string PoImenu { get; set; } = "active";
        public string PoStSmeri { get; set; } = "";
        public string PoPopularnosti { get; set; } = "";
        public string PoCasuDostopa { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            PoImenu = "active";
            PoPopularnosti = "";
            PoStSmeri = "";
            PoCasuDostopa = "";
            Plezalisca = (await DataRepository.GetPlezalisca()).ToList();
            Plezalisca = Plezalisca.OrderBy(p => p.ImePlezalisca);
        }        
        protected async Task Search_Klik(string searchTerm)
        {
            SearchTerm = searchTerm;
            Plezalisca = (await DataRepository.Search(searchTerm)).ToList();
        }
        protected void RazvrstiPoStSmeri()
        {
            PoImenu = "";
            PoPopularnosti = "";
            PoStSmeri = "active";
            PoCasuDostopa = "";
            Plezalisca = Plezalisca.OrderByDescending(p => p.VrniStSmeri());
        }
        protected void RazvrstiPoPopularnosti()
        {
            PoImenu = "";
            PoPopularnosti = "active";
            PoStSmeri = "";
            PoCasuDostopa = "";
            Plezalisca = Plezalisca.OrderByDescending(p => p.IzracunajPriljubljenost());
        }
        protected void RazvrstiPoCasuDostopa()
        {
            PoImenu = "";
            PoPopularnosti = "";
            PoStSmeri = "";
            PoCasuDostopa = "active";
            Plezalisca = Plezalisca.OrderBy(p => p.CasDostopa);
        }
    }
}
