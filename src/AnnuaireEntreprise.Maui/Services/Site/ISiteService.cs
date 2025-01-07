// Author: Salar
// Created Date: 06/01/2025
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Maui.Services.Site
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