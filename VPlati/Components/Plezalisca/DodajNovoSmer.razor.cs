using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Plezalisca
{
    public class DodajNovoSmerBase : ComponentBase
    {
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public IEnumerable<Ocena> VseOceneSmeri { get; set; }
        public IEnumerable<NacinPreplezanjaSmeri> VsiNacini { get; set; }
        public string KomentarText { get; set; } = "";
        public bool Obrazec { get; set; }
        public string OcenaNoveSmeri { get; set; } = "4a";
        public ModalComponent ModalComponentChange { get; set; }
        [Parameter]
        public Sektor Sektor { get; set; }
        [Parameter]
        public EventCallback OnVnesiNovoSmer { get; set; }
        public int IdPlezalca { get; set; }
        public string NacinPreplezanja { get; set; } = "Rdeča Pika";
        public Smer NovaSmer { get; set; } = new Smer() 
        {
            PrviVzpon=DateTime.UtcNow
        };

        public async Task DodajNovoOceno()
        {
            await PridobiVse();
            OcenaSmeri novaOcena = new OcenaSmeri();
            Ocena ocena = VseOceneSmeri.FirstOrDefault(o => o.OcenaSmeri == OcenaNoveSmeri);
            NacinPreplezanjaSmeri novNacin = VsiNacini.FirstOrDefault(n => n.Nacin == NacinPreplezanja);
            novaOcena.OcenaId = ocena.Id;
            novaOcena.SmerId = NovaSmer.Id;
            novaOcena.NacinPreplezanjaSmeriId = novNacin.Id;
            novaOcena.PlezalecId = await VrniPrijavljenPlezalecId();
            novaOcena.OcenaSmeriDatum = DateTime.UtcNow;
            await DataRepository.AddOcena(novaOcena);
        }
        public async Task AddKomentar()
        {
            if (KomentarText != "")
            {
                Komentar komentar = new Komentar()
                {
                    SmerId = NovaSmer.Id,
                    PlezalecId = await VrniPrijavljenPlezalecId(),
                    KomentarText = KomentarText
                };
                await DataRepository.AddKomentar(komentar);
            }
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
        protected async Task Add(bool value)
        {
            if (value)
            {
                NovaSmer.SektorId = Sektor.Id;
                await PridobiVse();
                await DataRepository.AddSmer(NovaSmer);
                await AddKomentar();
                await DodajNovoOceno();
                await OnVnesiNovoSmer.InvokeAsync(NovaSmer.Id);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
            NovaSmer = new Smer()
            {
                PrviVzpon = DateTime.UtcNow
            };
        }
    }
}
