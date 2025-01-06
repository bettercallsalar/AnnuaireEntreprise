// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Core
{
    public interface ISiteService
    {
        Task<List<SiteDTO>> GetAllSites();
        Task<SiteDTO> GetSiteById(int id);
        Task<SiteDTO> AddSite(SiteDTO site);
        Task<bool> UpdateSite(SiteDTO site);
        Task<bool> DeleteSite(int id);
    }
}