using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Plezalci
{
    public class PlezalecCardBase:ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Parameter]
        public Plezalec Plezalec { get; set; }
        public IEnumerable<OcenaSmeri> OceneSmeri { get; set; }
        protected PotrdiComponent PotrdiDelete { get; set; }
        public string CSSShadow { get; set; } = "sencaOn";
        protected void IzbrisiKlik()
        {
            PotrdiDelete.Show();
        }
        protected void HoverEffect()
        {
            CSSShadow = "sencaOff";
        }
        protected void NormalEffect()
        {
            CSSShadow = "sencaOn";
        }
        protected void PojdiNaPlezalecDetails()
        {
            NavigationManager.NavigateTo($"/plezalec/{Plezalec.Id}");
        }
    }
}
