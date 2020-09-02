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
    public class OceniSmerBase:ComponentBase
    {
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public string OcenaSmeri { get; set; } = "4a";
        public string NacinPreplezanja { get; set; } = "Rdeča Pika";
        public bool Obrazec { get; set; }
        public ModalComponent ModalComponentChange { get; set; }
        [Parameter]
        public Smer Smer { get; set; }
        [Parameter]
        public EventCallback OnVnesi { get; set; }
        [Parameter]
        public OcenaSmeri NovaOcenaSmeri { get; set; } = new OcenaSmeri();
        public IEnumerable<Ocena> VseOceneSmeri { get; set; }
        public IEnumerable<NacinPreplezanjaSmeri> VsiNacini { get; set; }
        
        protected async Task OdpriObrazec()
        {
            NovaOcenaSmeri = new OcenaSmeri();
            await PridobiVse();
        }
        protected async Task Add(bool value)
        {
            if (value)
            {
                Ocena ocena = VseOceneSmeri.FirstOrDefault(o => o.OcenaSmeri == OcenaSmeri);
                NacinPreplezanjaSmeri novNacin = VsiNacini.FirstOrDefault(n => n.Nacin == NacinPreplezanja);
                NovaOcenaSmeri.SmerId = Smer.Id;
                NovaOcenaSmeri.OcenaId = ocena.Id;
                NovaOcenaSmeri.NacinPreplezanjaSmeriId = novNacin.Id;
                NovaOcenaSmeri.PlezalecId = await VrniPrijavljenPlezalecId();
                NovaOcenaSmeri.OcenaSmeriDatum = DateTime.UtcNow;
                await DataRepository.AddOcena(NovaOcenaSmeri);
                await OnVnesi.InvokeAsync(NovaOcenaSmeri);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
            NovaOcenaSmeri = new OcenaSmeri();
        }
        protected async Task<int> VrniPrijavljenPlezalecId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var currentUser = authState.User;
            var plezalec = await UserManager.GetUserAsync(currentUser);
            return plezalec.Id;
        }
        protected async Task PridobiVse()
        {
            VseOceneSmeri = await DataRepository.GetOcene();
            VsiNacini = await DataRepository.GetVsiNacini();
        }
    }
}
