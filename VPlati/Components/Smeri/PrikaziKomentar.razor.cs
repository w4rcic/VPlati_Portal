using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Smeri
{
    public class PrikaziKomentarBase:ComponentBase
    {
        [Parameter]
        public Komentar Komentar { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public EventCallback OnIzbrisiKomentar_Klik { get; set; }
        protected PotrdiComponent PotrdiDelete { get; set; }

        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
        protected async Task IzbrisiKomentar_Klik(bool potrdiIzbrisi)
        {
            if (potrdiIzbrisi)
            {
                await DataRepository.DeleteKomentar(Komentar.Id);
                await OnIzbrisiKomentar_Klik.InvokeAsync(Komentar.Id);
            }
        }
    }
}
