using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Smeri
{
    public class DodajNovKomentarSmeriBase:ComponentBase
    {
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public bool Obrazec { get; set; }
        public ModalComponent ModalComponentChange { get; set; }
        [Parameter]
        public Smer Smer { get; set; }
        [Parameter]
        public EventCallback OnVnesi { get; set; }
        public Komentar NovKomentar { get; set; }
        protected void OdpriObrazec()
        {
            NovKomentar = new Komentar();
        }
        protected async Task Add(bool value)
        {
            if (value)
            {
                NovKomentar.SmerId = Smer.Id;
                NovKomentar.PlezalecId = await VrniPrijavljenPlezalecId();
                NovKomentar.KomentarDatum = DateTime.UtcNow;
                await DataRepository.AddKomentar(NovKomentar);
                await OnVnesi.InvokeAsync(NovKomentar);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
            NovKomentar = new Komentar();
        }
        protected async Task<int> VrniPrijavljenPlezalecId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var currentUser = authState.User;
            var plezalec = await UserManager.GetUserAsync(currentUser);
            return plezalec.Id;
        }
    }
}