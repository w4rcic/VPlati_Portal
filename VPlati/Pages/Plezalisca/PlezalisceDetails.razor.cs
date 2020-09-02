using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VPlati.Components;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Pages.Plezalisca
{
    public class PlezalisceDetailsBase : ComponentBase
    {
        public IEnumerable<Ocena> Ocene { get; set; }
        public Plezalisce Plezalisce { get; set; } = new Plezalisce();
        protected PotrdiComponent PotrdiDelete { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Plezalec PrijavljenPlezalec { get; set; }

        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Plezalisce = await DataRepository.GetPlezalisce(int.Parse(Id));
            Ocene = (await DataRepository.GetOcene()).ToList();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var currentUser = authState.User;
            PrijavljenPlezalec = await UserManager.GetUserAsync(currentUser);
        }
        protected void IzbrisiKlik()
        {
            PotrdiDelete.Show();
        }
        protected async Task IzbrisiPlezalisce_Klik(bool potrdiIzbrisi)
        {
            if (potrdiIzbrisi)
            {
                await DataRepository.DeletePlezalisce(int.Parse(Id));
                NavigationManager.NavigateTo("/Plezalisca");
            }
        }
    }
}
