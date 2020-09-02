using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPlati.Models;

namespace VPlati.Services
{
    public interface IVPlatiRepository
    {
        Task<IEnumerable<Plezalisce>> GetPlezalisca();
        Task<IEnumerable<Plezalisce>> Search(string ime);
        Task<Plezalisce> GetPlezalisce(int plezalisceId);
        Task<Plezalisce> AddPlezalisce(Plezalisce plezalisce);
        Task<Plezalisce> UpdatePlezalisce(Plezalisce plezalisce);
        Task<Plezalisce> DeletePlezalisce(int plezalisceId);
        Task<IEnumerable<Ocena>> GetOcene();
        Task<IEnumerable<NacinPreplezanjaSmeri>> GetVsiNacini();
        Task AddOpozorilo(Opozorilo novoOpozorilo);
        Task DeleteOpozorilo(int opozoriloId);
        Task<Sektor> DeleteSektor(int sektorId);
        Task AddSektor(Sektor sektor);
        Task AddSmer(Smer smer);
        Task<Smer> DeleteSmer(int smerId);
        Task AddOcena(OcenaSmeri ocenaSmeri);
        Task<Smer> GetSmer(int smerId);
        Task AddKomentar(Komentar komentar);
        Task DeleteKomentar(int komentarId);
        Task<IEnumerable<OcenaSmeri>> GetOceneSmeri();
        Task<IEnumerable<Opozorilo>> GetOpozorila();
        Task<Smer> UpdateSmer(Smer smer);
    }
}
