// Author: Salar
// Created: 06/01/2024

using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class SiteService(ISiteRepository siteRepository) : ISiteService
    {
        private readonly ISiteRepository _siteRepository = siteRepository;

        public async Task<List<SiteDTO>> GetAllSites()
        {
            return await _siteRepository.GetAllSites();
        }

        public async Task<SiteDTO> GetSiteById(int id)
        {
            return await _siteRepository.GetSiteById(id);
        }

        public async Task<bool> AddSite(SiteDTO site)
        {
            return await _siteRepository.AddSite(site);
        }

        public async Task<bool> UpdateSite(SiteDTO site)
        {
            return await _siteRepository.UpdateSite(site);
        }

        public async Task<bool> DeleteSite(SiteDTO site)
        {
            return await _siteRepository.DeleteSite(site);
        }
    }
}