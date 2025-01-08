// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    // Interface for Service Repository
    public interface IServiceRepository
    {
        Task<List<ServiceDTO>> FetchAllServices();
        Task<ServiceDTO> FetchServiceById(int id);
        Task<ServiceDTO> InsertService(ServiceDTO service);
        Task<bool> UpdateService(ServiceDTO service);
        Task<bool> DeleteService(int id);
    }
}