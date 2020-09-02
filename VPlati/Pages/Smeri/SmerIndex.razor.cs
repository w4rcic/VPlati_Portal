using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Components;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Pages.Smeri
{
    public class SmerIndexBase:ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Plezalec Plezalec { get; set; }
        [Parameter]
        public string Id { get; set; }
        public Smer Smer { get; set; } = new Smer();
        public IEnumerable<Ocena> Ocene { get; set; }
        public int PovOcena { get; set; }
        protected PotrdiComponent PotrdiDelete { get; set; }

        public IEnumerable<Plezalec> Plezalci { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Smer = await DataRepository.GetSmer(int.Parse(Id));
            Ocene = (await DataRepository.GetOcene()).ToList();        
        }
        protected async Task IzbrisiSmer_Klik(bool potrdiIzbrisi)
        {
            if (potrdiIzbrisi)
            {
                await DataRepository.DeleteSmer(Smer.Id);
                NavigationManager.NavigateTo($"/plezalisce/{Smer.Sektor.PlezalisceId}");
            }
        }
        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
        protected int PrestejVzpone()
        {
            int stVzponov = 0;
            foreach(var ocenaSmeri in Smer.OceneSmeri)
            {
                stVzponov++;               
            }
            return stVzponov;
        }
        protected bool PlezalecSeNiOcenilSmer(string prijavljenUser)
        {
            if (Smer.OceneSmeri.FirstOrDefault(p => p.Plezalec.UserName == prijavljenUser) == null)
            {
                return true;
            }
            return false;
        }
    }
}
