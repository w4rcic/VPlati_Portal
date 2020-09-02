using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPlatiComponents
{
    public class PotrdiComponentBase : ComponentBase
    {
        protected bool PokaziPotrdiOkno { get; set; }

        [Parameter]
        public string NaslovPotrdiOkno { get; set; } = "Potrdi brisanje";

        [Parameter]
        public string PotrdiOknoText { get; set; } = "Ali res želite izbrisati";

        public void Show()
        {
            PokaziPotrdiOkno = true;
            StateHasChanged();
        }
        [Parameter]
        public EventCallback<bool> OnPotrdiSprememba { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            PokaziPotrdiOkno = false;
            if (value == true)
            {
                await OnPotrdiSprememba.InvokeAsync(value);
            }          
        }
    }
}