using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;

namespace VPlati.Services
{
    public class VPlatiRepository : IVPlatiRepository
    {
        private readonly AppDbContext appDbContext;
        public VPlatiRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddKomentar(Komentar komentar)
        {
            await appDbContext.Komentarji.AddAsync(komentar);
            await appDbContext.SaveChangesAsync();
        }

        public async Task AddOcena(OcenaSmeri novaOcena)
        {
            await appDbContext.OcenaSmeri.AddAsync(novaOcena);
            await appDbContext.SaveChangesAsync();
        }

        public async Task AddOpozorilo(Opozorilo novoOpozorilo)
        {
            await appDbContext.Opozorila.AddAsync(novoOpozorilo);
            await appDbContext.SaveChangesAsync();
            //return result;
        }

        public async Task<Plezalisce> AddPlezalisce(Plezalisce plezalisce)
        {
            var result = await appDbContext.Plezalisca.AddAsync(plezalisce);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddSektor(Sektor sektor)
        {
            await appDbContext.Sektorji.AddAsync(sektor);
            await appDbContext.SaveChangesAsync();
        }

        public async Task AddSmer(Smer smer)
        {
            await appDbContext.Smeri.AddAsync(smer);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteKomentar(int komentarId)
        {
            var result = await appDbContext.Komentarji.FirstOrDefaultAsync(k => k.Id == komentarId);
            if (result != null)
            {
                appDbContext.Komentarji.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteOpozorilo(int opozoriloId)
        {
            var result = await appDbContext.Opozorila.FirstOrDefaultAsync(o => o.Id == opozoriloId);
            if (result != null)
            {
                appDbContext.Opozorila.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Plezalisce> DeletePlezalisce(int plezalisceId)
        {
            var result = await appDbContext.Plezalisca.FirstOrDefaultAsync(e => e.Id == plezalisceId);
            if (result != null)
            {
                appDbContext.Plezalisca.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Sektor> DeleteSektor(int sektorId)
        {
            var result = await appDbContext.Sektorji.FirstOrDefaultAsync(s => s.Id == sektorId);
            if (result != null)
            {
                appDbContext.Sektorji.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Smer> DeleteSmer(int smerId)
        {
            var result = await appDbContext.Smeri.FirstOrDefaultAsync(s => s.Id == smerId);
            if(result != null)
            {
                appDbContext.Smeri.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Ocena>> GetOcene()
        {
            return await appDbContext.Ocena.ToListAsync();
        }

        public async Task<IEnumerable<OcenaSmeri>> GetOceneSmeri()
        {
            return await appDbContext.OcenaSmeri
                .Include(s => s.Smer)
                .ThenInclude(s=>s.Sektor)
                .ThenInclude(p=>p.Plezalisce)
                .Include(n=>n.NacinPreplezanjaSmeri)
                .Include(o=>o.Ocena)
                .Include(p=>p.Plezalec)
                .ToListAsync();
        }

        public async Task<IEnumerable<Opozorilo>> GetOpozorila()
        {
            return await appDbContext.Opozorila
                .Include(p => p.Plezalisce)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plezalisce>> GetPlezalisca()
        {
            return await appDbContext.Plezalisca
                .Include(s=>s.Sektorji)
                .ThenInclude(s=>s.Smeri)
                .ThenInclude(o=>o.OceneSmeri)
                .ToListAsync();
        }

        public async Task<Plezalisce> GetPlezalisce(int plezalisceId)
        {
            return await appDbContext.Plezalisca
                .Include(o => o.Opozorila)
                .Include(s => s.Sektorji)
                .ThenInclude(s => s.Smeri)
                .ThenInclude(o => o.OceneSmeri)
                .FirstOrDefaultAsync(p => p.Id == plezalisceId);
        }

        public async Task<Smer> GetSmer(int smerId)
        {
            return await appDbContext.Smeri
                .Include(s=>s.Sektor)
                .Include(o=>o.OceneSmeri)
                .ThenInclude(p=>p.Plezalec)
                .Include(o => o.OceneSmeri)
                .ThenInclude(n=>n.NacinPreplezanjaSmeri)
                .Include(k=>k.Komentarji)
                .ThenInclude(p=>p.Plezalec)             
                .FirstOrDefaultAsync(s => s.Id == smerId);
        }

        public async Task<IEnumerable<NacinPreplezanjaSmeri>> GetVsiNacini()
        {
            return await appDbContext.NacinPreplezanjaSmeri.ToListAsync();
        }

        public async Task<IEnumerable<Plezalisce>> Search(string searchString)
        {
            IQueryable<Plezalisce> query = appDbContext.Plezalisca;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.ImePlezalisca.Contains(searchString));
            }
            return await query.ToListAsync();
        }

        public async Task<Plezalisce> UpdatePlezalisce(Plezalisce plezalisce)
        {
            var result = await appDbContext.Plezalisca.FirstOrDefaultAsync(p => p.Id == plezalisce.Id);
            if (result != null)
            {
                result.ImePlezalisca = plezalisce.ImePlezalisca;
                result.OpisDostopa = plezalisce.OpisDostopa;
                result.Opozorila = plezalisce.Opozorila;
                result.Sektorji = plezalisce.Sektorji;
                result.SlikaPlezalisca = plezalisce.SlikaPlezalisca;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }

        public async Task<Smer> UpdateSmer(Smer smer)
        {
            var result = await appDbContext.Smeri.FirstOrDefaultAsync(s => s.Id == smer.Id);
            if(result != null)
            {
                result.ImeSmeri = smer.ImeSmeri;
                result.Opremjevalec = smer.Opremjevalec;
                result.PrviVzpon = smer.PrviVzpon;
                result.Dolzina = smer.Dolzina;

                await appDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}
