using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Plezalisca
{
    public class DodajNovSektorDetailsBase:ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        //public string NovoOpozorilo { get; set; }
        public bool Obrazec { get; set; }
        public ModalComponent ModalComponentChange { get; set; }
        [Parameter]
        public Plezalisce Plezalisce { get; set; }
        [Parameter]
        public EventCallback OnVnesiNovSektor { get; set; }
        public Sektor NovSektor { get; set; }
        public int MyProperty { get; set; }

        protected void OdpriObrazecNovSektor()
        {
            NovSektor = new Sektor();
        }
        protected async Task NovSektorAdd(bool value)
        {
            if (value)
            {
                NovSektor.PlezalisceId = Plezalisce.Id;
                await DataRepository.AddSektor(NovSektor);
                await DataRepository.GetPlezalisce(Plezalisce.Id);
                await OnVnesiNovSektor.InvokeAsync(NovSektor);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
            NovSektor = new Sektor();
        }

    }
}
