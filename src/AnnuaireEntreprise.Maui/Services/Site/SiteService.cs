// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Maui.Services;

namespace AnnuaireEntreprise.Maui.Services.Site
{
    // Interface for Site Service
    public class SiteService : ISiteService
    {
        private readonly ApiService _apiService;


        public SiteService()
        {
            _apiService = new ApiService();
        }

        // Fetch All Sites from API
        public async Task<List<SiteDTO>> GetAllSites()
        {
            try
            {
                var result = await _apiService.GetAsync<List<SiteDTO>>("Site/get-all-sites");
                return result ?? new List<SiteDTO>();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<SiteDTO>();
            }
        }

        // Fetch Site by Id
        public async Task<SiteDTO> GetSiteById(int id)
        {
            try
            {
                var result = await _apiService.GetAsync<SiteDTO>($"Site/get-site-by-id/{id}");
                if (result != null)
                {
                    return result;
                }
                return new SiteDTO
                {

                    Ville = string.Empty,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SiteDTO
                {

                    Ville = string.Empty,
                };
            }
        }

        // Add Site
        public async Task<SiteDTO> AddSite(SiteDTO site)
        {
            try
            {
                Console.WriteLine("Adding Site", site);
                var result = await _apiService.PostAsync<SiteDTO, bool>("Site/add-site", site);
                if (result)
                {
                    return site;
                }
                return new SiteDTO
                {
                    Ville = string.Empty,
                };
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SiteDTO
                {
                    Ville = string.Empty,
                };
            }
        }

        // Update Site
        public async Task<bool> UpdateSite(SiteDTO site)
        {
            try
            {
                var result = await _apiService.PutAsync<SiteDTO>("Site/update-site", site);
                return result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Delete Site
        public async Task<bool> DeleteSite(int id)
        {
            try
            {
                var result = await _apiService.DeleteAsync($"Site/delete-site/{id}");
                return result;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}