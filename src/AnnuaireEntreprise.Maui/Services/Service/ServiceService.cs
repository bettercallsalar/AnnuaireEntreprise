// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Maui.Services;

namespace AnnuaireEntreprise.Maui.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly ApiService _apiService;
        public ServiceService()
        {
            _apiService = new ApiService();
        }

        public async Task<List<ServiceDTO>> GetAllServices()
        {
            try
            {
                var result = await _apiService.GetAsync<List<ServiceDTO>>("Service/get-all-services");
                return result ?? new List<ServiceDTO>();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ServiceDTO>();
            }
        }

        public async Task<ServiceDTO> GetServiceById(int id)
        {
            try
            {
                var result = await _apiService.GetAsync<ServiceDTO>($"Service/get-service-by-id/{id}");
                if (result != null)
                {
                    return result;
                }
                return new ServiceDTO
                {

                    Nom = string.Empty,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ServiceDTO
                {

                    Nom = string.Empty,
                };
            }
        }

        public async Task<ServiceDTO> AddService(ServiceDTO service)
        {
            try
            {
                var result = await _apiService.PostAsync<ServiceDTO, bool>("Service/add-service", service);
                if (result)
                {
                    return service;
                }
                return new ServiceDTO
                {
                    Nom = string.Empty,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ServiceDTO
                {
                    Nom = string.Empty,
                };
            }
        }

        public async Task<bool> ModifyService(ServiceDTO service)
        {
            try
            {
                return await _apiService.PutAsync<ServiceDTO>("Service/modify-service", service);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> RemoveService(int id)
        {
            try
            {
                return await _apiService.DeleteAsync($"Service/remove-service/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}