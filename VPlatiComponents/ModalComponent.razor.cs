using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPlatiComponents
{
    public class ModalComponentBase:ComponentBase
    {
        [Parameter]
        public string ModalSize { get; set; } = "";
        [Parameter]
        public string ButtonStyle { get; set; }
        [Parameter]
        public string ButtonText { get; set; }
        [Parameter]
        public string ModalHeader { get; set; }
        [Parameter]
        public RenderFragment ModalBody { get; set; }
        public bool Obrazec { get; set; }
        [Parameter]
        public EventCallback<bool> OnPotrdi { get; set; }
        [Parameter]
        public EventCallback OnOdpriObrazec { get; set; }

        public async Task Show()
        {
            await OnOdpriObrazec.InvokeAsync(Obrazec);
            Obrazec = true;
        }

        public async Task OnConfirmationChange(bool value)
        {
            Obrazec = false;
            if (value)
            {
                await OnPotrdi.InvokeAsync(value);
            }
        }
    }
}
