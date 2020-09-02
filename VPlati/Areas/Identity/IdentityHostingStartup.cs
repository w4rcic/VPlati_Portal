using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPlati.Models;

[assembly: HostingStartup(typeof(VPlati.Areas.Identity.IdentityHostingStartup))]
namespace VPlati.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DBConnection")));

                services.AddIdentity<Plezalec, ApplicationRole>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<AppDbContext>();
            });
        }
    }
}