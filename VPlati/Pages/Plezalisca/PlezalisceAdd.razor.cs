using Microsoft.AspNetCore.Components;
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
using VPlati.Models;
using VPlati.Services;

namespace VPlati.Pages.Plezalisca
{
    public class PlezalisceAddBase : ComponentBase
    {
        bool novaSlika = false;
        string fileName = string.Empty;
        Stream slikaStream = null;
        public string ImageContent { get; set; } = string.Empty;
        public ElementReference InputReference { get; set; }
        public string Notification { get; set; } = "SkrijElement";
        public Plezalisce Plezalisce { get; set; } = new Plezalisce();

        [Inject]
        IFileReaderService FileReaderService { get; set; }
        [Inject]
        IWebHostEnvironment WebHostEnvironment { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task AddPlezalisce()
        {
            Plezalisce result;

            if (!novaSlika)
            {
                Plezalisce.SlikaPlezalisca = "images/Default.jpg";
            }
            else
            {
                Plezalisce.SlikaPlezalisca = ShraniSliko();
            }

            result = await DataRepository.AddPlezalisce(Plezalisce);

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
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
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
                novaSlika = true;
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
            return "images/" + uniqueFileName;
        }
    }
}
