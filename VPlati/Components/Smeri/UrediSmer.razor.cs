using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Smeri
{
    public class UrediSmerBase : ComponentBase
    {
        [CascadingParameter]
        public Smer Smer { get; set; }
        [Parameter]
        public EventCallback OnVnesiNovoSmer { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public ModalComponent ModalComponentChange { get; set; }

        protected async Task Update(bool value)
        {
            if (value)
            {
                await DataRepository.UpdateSmer(Smer);
                await OnVnesiNovoSmer.InvokeAsync(Smer.Id);
            }
        }
        protected async Task ConfirmationChange(bool value)
        {
            await ModalComponentChange.OnConfirmationChange(value);
        }
    }
}
