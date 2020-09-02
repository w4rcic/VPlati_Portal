using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using VPlati.Components.Plezalisca;
using VPlati.Models;
using VPlati.Services;
using VPlatiComponents;

namespace VPlati.Pages.Plezalisca
{
    public class PlezalisceEditBase : ComponentBase
    {
        public bool NovaSlika { get; set; } = false;
        string fileName = string.Empty;
        Stream slikaStream = null;
        public string ImageContent { get; set; } = string.Empty;
        public ElementReference InputReference { get; set; }
        public string ImeSektorja { get; set; } = "";
        public string Notification { get; set; } = "SkrijElement";
        [Inject]
        IFileReaderService FileReaderService { get; set; }
        [Inject]
        IWebHostEnvironment WebHostEnvironment { get; set; }
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]     
        public NavigationManager NavigationManager { get; set; }
        public Plezalisce Plezalisce { get; set; } = new Plezalisce();
        public PotrdiComponent PotrdiDelete { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Plezalisce = await DataRepository.GetPlezalisce(int.Parse(Id));
        }
        protected async Task UpdatePlezalisce()
        {
            if (NovaSlika)
            {
                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "", Plezalisce.SlikaPlezalisca);
                if (Plezalisce.SlikaPlezalisca != "Default.jpg")
                {
                    File.Delete(filePath);
                }
                Plezalisce.SlikaPlezalisca = ShraniSliko();
            }

            Plezalisce result = await DataRepository.UpdatePlezalisce(Plezalisce);

            if (result != null)
            {
                NavigationManager.NavigateTo($"/plezalisce/{Plezalisce.Id}");
            }
        }

        protected async Task IzberiSliko()
        {
            Notification = "SkrijElement";
            var file = (await FileReaderService.CreateReference(InputReference).EnumerateFilesAsync()).FirstOrDefault();

            var fileInfo = await file.ReadFileInfoAsync();
            string extension = Path.GetExtension(fileInfo.Name);
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png"};
            if (!allowExtensions.Contains(extension))
            {
                Notification = "PokaziElement";
            }
            else
            {
                fileName = fileInfo.Name;
                using (var memoryStream = await file.CreateMemoryStreamAsync())
                {
                    slikaStream = new MemoryStream(memoryStream.ToArray());
                    ImageContent = $"data:{fileInfo.Type}; base64, {Convert.ToBase64String(memoryStream.ToArray())}";
                }
                NovaSlika = true;
            }
        }
        protected string ShraniSliko()
        {
            string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                slikaStream.CopyTo(fs);
            }
            return "images/"+uniqueFileName;
        }
        protected async Task IzbrisiPlezalisce_Klik(bool potrdiIzbrisi)
        {
            if (potrdiIzbrisi)
            {
                await DataRepository.DeletePlezalisce(Plezalisce.Id);
                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "", Plezalisce.SlikaPlezalisca);
                if (Plezalisce.SlikaPlezalisca != "images/Default.jpg")
                {
                    File.Delete(filePath);
                }
                NavigationManager.NavigateTo($"/plezalisca");
            }
        }
        protected void IzbrisiSektor_Klik() {}

        protected async Task DodajSektor_Klik()
        {
            Sektor Sektor = new Sektor();
            if(ImeSektorja != "")
            {
                Sektor.ImeSektorja = ImeSektorja;
                Sektor.PlezalisceId = Plezalisce.Id;
                await DataRepository.AddSektor(Sektor);
                ImeSektorja = "";
            }
        }

        protected void Izbrisi_Klik()
        {
            PotrdiDelete.Show();
        }
    }
}
