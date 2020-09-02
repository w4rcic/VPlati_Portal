using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Plezalisca
{
    public class DodajNovoOpozoriloBase:ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public bool Obrazec { get; set; }
        public ModalComponent ModalComponentChange { get; set; }
        [Parameter]
        public Plezalisce Plezalisce { get; set; }
        [Parameter]
        public EventCallback OnVnesiNovoOpozorilo { get; set; }
        public Opozorilo NovoOpozorilo { get; set; }

        protected void OdpriObrazecNovoOpozorilo()
        {
            NovoOpozorilo = new Opozorilo();
        }
        protected async Task NovoOpozoriloAdd(bool value)
        {
            if (value)
            {
                NovoOpozorilo.PlezalisceId = Plezalisce.Id;
                NovoOpozorilo.OpozoriloDatum = DateTime.UtcNow;
                await DataRepository.AddOpozorilo(NovoOpozorilo);
                await OnVnesiNovoOpozorilo.InvokeAsync(NovoOpozorilo);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
            NovoOpozorilo = new Opozorilo();
        }
    }
}
