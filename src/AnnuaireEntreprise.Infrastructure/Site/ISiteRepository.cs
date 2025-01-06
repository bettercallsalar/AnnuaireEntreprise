// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    public interface ISiteRepository
    {
        Task<List<SiteDTO>> GetAllSites();
        Task<SiteDTO> GetSiteById(int id);
        Task<bool> AddSite(SiteDTO site);
        Task<bool> UpdateSite(SiteDTO site);
        Task<bool> DeleteSite(SiteDTO site);
    }
}