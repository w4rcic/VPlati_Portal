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
    public class PrikaziOpozoriloBase : ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Parameter]
        public Opozorilo Opozorilo { get; set; }
        public PotrdiComponent PotrdiDelete { get; set; }
        [Parameter]
        public EventCallback OnIzbrisi_Klik { get; set; }

        protected async Task IzbrisiOpozorilo_Klik()
        {
            await DataRepository.DeleteOpozorilo(Opozorilo.Id);
            await OnIzbrisi_Klik.InvokeAsync(Opozorilo.Id);
        }

        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
    }
}
