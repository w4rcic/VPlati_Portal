using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Components.Plezalisca
{
    public class PlezalisceCardBase:ComponentBase
    {
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public EventCallback OnIzbrisiPlezalisce_Klik { get; set; }
        [Parameter]
        public Plezalisce Plezalisce { get; set; }
        protected PotrdiComponent PotrdiDelete { get; set; }
        public string CSSShadow { get; set; } = "sencaOn";

        protected void IzbrisiKlik()
        {
            PotrdiDelete.Show();
        }
        protected async Task IzbrisiPlezalisce_Klik(bool potrdiIzbrisi)
        {
            if (potrdiIzbrisi)
            {
                await DataRepository.DeletePlezalisce(Plezalisce.Id);
                await OnIzbrisiPlezalisce_Klik.InvokeAsync(Plezalisce.Id);
                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "", Plezalisce.SlikaPlezalisca);
                if (Plezalisce.SlikaPlezalisca != "images/Default.jpg")
                {
                    File.Delete(filePath);
                }
            }
        }
        protected void HoverEffect()
        {
            CSSShadow = "sencaOff";
        }
        protected void NormalEffect()
        {
            CSSShadow = "sencaOn";
        }
    }
}
