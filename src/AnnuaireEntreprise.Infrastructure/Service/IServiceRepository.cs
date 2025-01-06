// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    public interface IServiceRepository
    {
        Task<List<ServiceDTO>> FetchAllServices();
        Task<ServiceDTO> FetchServiceById(int id);
        Task<bool> InsertService(ServiceDTO service);
        Task<bool> UpdateService(ServiceDTO service);
        Task<bool> DeleteService(ServiceDTO service);
    }
}