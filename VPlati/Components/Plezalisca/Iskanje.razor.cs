using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VPlati.Components.Plezalisca
{
    public class IskanjeBase : ComponentBase
    {
        [Parameter]
        public string LabelText { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        [Parameter]
        public EventCallback<string> OnSearch_Klik { get; set; }
        protected async Task Search()
        {
            await OnSearch_Klik.InvokeAsync(SearchTerm);
        }
    }
}
