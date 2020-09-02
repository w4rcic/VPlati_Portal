using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;
using VPlati.Services;

namespace VPlati.Pages.Plezalci
{
    public class PlezalciIndexBase:ComponentBase
    {
        [Inject]
        public UserManager<Plezalec> UserManager { get; set; }
        [Inject]
        protected IVPlatiRepository DataRepository { get; set; }
        public IEnumerable<Plezalec> Plezalci { get; set; }
        public string SearchTerm { get; set; } = "";

        protected override void OnInitialized()
        {
            Plezalci = UserManager.Users.ToList();
        }
        protected void Search_Klik(string searchTerm)
        {
            SearchTerm = searchTerm;
            Plezalci = UserManager.Users.Where(u => u.Ime.Contains(searchTerm) || u.Priimek.Contains(searchTerm) || u.UserName.Contains(searchTerm));
            Plezalci = Plezalci.OrderByDescending(p => p.Ime);
        }
    }
}
