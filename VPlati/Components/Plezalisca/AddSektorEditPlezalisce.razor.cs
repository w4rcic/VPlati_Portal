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
    public class AddSektorEditPlezalisceBase : ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }

        [Parameter]
        public EventCallback OnIzbrisiSektor_Klik { get; set; }
        [Parameter]
        public Sektor Sektor { get; set; }
        public PotrdiComponent PotrdiDelete { get; set; }

        protected async Task IzbrisiSektor_Klik()
        {
            await DataRepository.DeleteSektor(Sektor.Id);
            await OnIzbrisiSektor_Klik.InvokeAsync(Sektor.Id);
        }
        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
    }
}
