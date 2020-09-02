using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using VPlati.Models;
using VPlati.Services;

namespace VPlati.Pages.Plezalci
{
    public class PlezalecEditBase : ComponentBase
    {
        public bool NovaSlika { get; set; } = false;
        string fileName = string.Empty;
        Stream slikaStream = null;

        [Inject]
        public IVPlatiRepository DataRepository { get; set; }
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IFileReaderService FileReaderService { get; set; }
        [Inject]
        IWebHostEnvironment WebHostEnvironment { get; set; }

        public Plezalec Plezalec { get; set; } = new Plezalec();
        public ElementReference InputReference { get; set; }
        public string ImageContent { get; set; } = string.Empty;
        public string Notification { get; set; } = "SkrijElement";

        protected async override Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var currentUser = authState.User;
            Plezalec = await UserManager.GetUserAsync(currentUser);
        }
        protected async Task UpdatePlezalec()
        {
            if (NovaSlika)
            {
                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "", Plezalec.SlikaPlezalca);
                if (Plezalec.SlikaPlezalca != "images/DefaultUser.jpg")
                {
                    File.Delete(filePath);
                }
                Plezalec.SlikaPlezalca = ShraniSliko();
            }
            await UserManager.UpdateAsync(Plezalec);
            NavigationManager.NavigateTo($"/plezalec/{Plezalec.Id}");
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
            return "images/" + uniqueFileName;
        }
    }
}
