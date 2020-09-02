using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Smeri
{
    public class PrikaziSmerBase:ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Parameter]
        public Plezalec PrijavljenPlezalec { get; set; }
        [CascadingParameter]
        public Smer Smer { get; set; }
        [Parameter]
        public int Counter { get; set; }
        [Parameter]
        public IEnumerable<Ocena> Ocene { get; set; }
        [Parameter]
        public EventCallback OnSprememba_Smer_Klik { get; set; }

        public PotrdiComponent PotrdiDelete { get; set; }

        protected async Task IzbrisiSmer_Klik()
        {
            await DataRepository.DeleteSmer(Smer.Id);
            await OnSprememba_Smer_Klik.InvokeAsync(Smer.ImeSmeri);
        }
        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
        protected void NavigateToSmerIndex()
        {
            NavigationManager.NavigateTo($"/smer/{Smer.Id}");
        }
        protected async Task PopraviSmer()
        {
            await OnSprememba_Smer_Klik.InvokeAsync(Smer.ImeSmeri);
        }
    }
}
