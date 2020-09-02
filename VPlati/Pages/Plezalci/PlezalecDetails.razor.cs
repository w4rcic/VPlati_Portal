using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;

namespace VPlati.Pages.Plezalci
{
    public class PlezalecDetailsBase:ComponentBase
    {
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Plezalec Plezalec { get; set; }
        [Parameter]
        public string Id { get; set; }
        public IEnumerable<OcenaSmeri> OceneSmeri { get; set; }
        public int Flash { get; set; } = 0;
        public int RdecaPika { get; set; } = 0;
        public int NaPogled { get; set; } = 0;
        public int DolzinaVsehSmeri { get; set; } = 0;
        public int StSmeri { get; set; } = 0;
        public IEnumerable<Ocena> VseOcene { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Plezalec = await UserManager.Users.FirstOrDefaultAsync(p => p.Id == int.Parse(Id));
            VseOcene = await DataRepository.GetOcene();
            await GetPreplezaneSmeri();
        }
        protected async Task GetPreplezaneSmeri()
        {
            OceneSmeri = await DataRepository.GetOceneSmeri();
            OceneSmeri = OceneSmeri.Where(p => p.PlezalecId == Plezalec.Id).OrderByDescending(d => d.OcenaSmeriDatum);
            foreach(var ocenaSmeri in OceneSmeri)
            {
                DolzinaVsehSmeri += ocenaSmeri.Smer.Dolzina;
                if (ocenaSmeri.OcenaId > RdecaPika && ocenaSmeri.NacinPreplezanjaSmeri.Nacin == "Rdeča Pika")
                {
                    RdecaPika = ocenaSmeri.OcenaId;
                }
                if(ocenaSmeri.OcenaId > Flash && ocenaSmeri.NacinPreplezanjaSmeri.Nacin == "Flash")
                {
                    Flash = ocenaSmeri.OcenaId;
                }
                if (ocenaSmeri.OcenaId > NaPogled && ocenaSmeri.NacinPreplezanjaSmeri.Nacin == "Na Pogled")
                {
                    NaPogled = ocenaSmeri.OcenaId;
                }
                StSmeri++;
            }
        }
        protected string GetOcena(int ocenaId)
        {
            if (ocenaId == 0)
                return "";
            else
            {
                var ocena = VseOcene.FirstOrDefault(o => o.Id == ocenaId);
                return ocena.OcenaSmeri;
            }       
        }
    }
}
