// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class ServiceService(IServiceRepository serviceRepository) : IServiceService // Implementing the methods from the interface
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        // Get all services
        public async Task<List<ServiceDTO>> GetAllServices()
        {
            return await _serviceRepository.FetchAllServices();
        }

        // Get service by id
        public async Task<ServiceDTO> GetServiceById(int id)
        {
            return await _serviceRepository.FetchServiceById(id);
        }

        // Add service
        public async Task<ServiceDTO> AddService(ServiceDTO service)
        {
            return await _serviceRepository.InsertService(service);
        }

        // Update service
        public async Task<bool> ModifyService(ServiceDTO service)
        {
            return await _serviceRepository.UpdateService(service);
        }

        // Delete service
        public async Task<bool> RemoveService(int id)
        {
            return await _serviceRepository.DeleteService(id);
        }
    }
}