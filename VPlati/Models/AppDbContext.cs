using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace VPlati.Models
{
    public class AppDbContext : IdentityDbContext<Plezalec, ApplicationRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Plezalisce> Plezalisca { get; set; }
        public DbSet<Sektor> Sektorji { get; set; }
        public DbSet<Smer> Smeri { get; set; }
        public DbSet<Komentar> Komentarji { get; set; }
        public DbSet<Opozorilo> Opozorila { get; set; }
        public DbSet<Ocena> Ocena { get; set; }
        public DbSet<OcenaSmeri> OcenaSmeri { get; set; }
        public DbSet<NacinPreplezanjaSmeri> NacinPreplezanjaSmeri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 1,
                OcenaSmeri = "4a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 2,
                OcenaSmeri = "4b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 3,
                OcenaSmeri = "4c"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 4,
                OcenaSmeri = "5a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 5,
                OcenaSmeri = "5b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 6,
                OcenaSmeri = "5c"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 7,
                OcenaSmeri = "6a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 8,
                OcenaSmeri = "6a+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 9,
                OcenaSmeri = "6b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 10,
                OcenaSmeri = "6b+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 11,
                OcenaSmeri = "6c"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 12,
                OcenaSmeri = "6c+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 13,
                OcenaSmeri = "7a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 14,
                OcenaSmeri = "7a+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 15,
                OcenaSmeri = "7b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 16,
                OcenaSmeri = "7b+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 17,
                OcenaSmeri = "7c"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 18,
                OcenaSmeri = "7c+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 19,
                OcenaSmeri = "8a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 20,
                OcenaSmeri = "8a+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 21,
                OcenaSmeri = "8b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 22,
                OcenaSmeri = "8b+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 23,
                OcenaSmeri = "8c"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 24,
                OcenaSmeri = "8c+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 25,
                OcenaSmeri = "9a"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 26,
                OcenaSmeri = "9a+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 27,
                OcenaSmeri = "9b"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 28,
                OcenaSmeri = "9b+"
            });
            modelBuilder.Entity<Ocena>().HasData(new Ocena
            {
                Id = 29,
                OcenaSmeri = "9c"
            });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id=1,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = 2,
                Name = "user",
                NormalizedName = "USER"
            });

            modelBuilder.Entity<NacinPreplezanjaSmeri>().HasData(new NacinPreplezanjaSmeri
            {
                Id = 1,
                Nacin = "Flash"
            });
            modelBuilder.Entity<NacinPreplezanjaSmeri>().HasData(new NacinPreplezanjaSmeri
            {
                Id = 2,
                Nacin = "Na Pogled"
            });
            modelBuilder.Entity<NacinPreplezanjaSmeri>().HasData(new NacinPreplezanjaSmeri
            {
                Id = 3,
                Nacin = "Rdeča Pika"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
