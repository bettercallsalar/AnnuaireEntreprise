// Author: Salar
// Created: 06/01/2024

using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class SiteService(ISiteRepository siteRepository) : ISiteService // Implementing the methods from the interface
    {
        private readonly ISiteRepository _siteRepository = siteRepository;

        // Get all sites
        public async Task<List<SiteDTO>> GetAllSites()
        {
            return await _siteRepository.GetAllSites();
        }

        // Get site by id
        public async Task<SiteDTO> GetSiteById(int id)
        {
            return await _siteRepository.GetSiteById(id);
        }

        // Add site
        public async Task<SiteDTO> AddSite(SiteDTO site)
        {
            return await _siteRepository.AddSite(site);
        }

        // Update site
        public async Task<bool> UpdateSite(SiteDTO site)
        {
            return await _siteRepository.UpdateSite(site);
        }

        // Delete site
        public async Task<bool> DeleteSite(int id)
        {
            return await _siteRepository.DeleteSite(id);
        }
    }
}