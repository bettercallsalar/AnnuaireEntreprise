// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class ServiceService(IServiceRepository serviceRepository) : IServiceService
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        public async Task<List<ServiceDTO>> GetAllServices()
        {
            return await _serviceRepository.FetchAllServices();
        }

        public async Task<ServiceDTO> GetServiceById(int id)
        {
            return await _serviceRepository.FetchServiceById(id);
        }

        public async Task<bool> AddService(ServiceDTO service)
        {
            return await _serviceRepository.InsertService(service);
        }

        public async Task<bool> ModifyService(ServiceDTO service)
        {
            return await _serviceRepository.UpdateService(service);
        }

        public async Task<bool> RemoveService(ServiceDTO service)
        {
            return await _serviceRepository.DeleteService(service);
        }
    }
}