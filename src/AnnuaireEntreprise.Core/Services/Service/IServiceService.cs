// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
namespace AnnuaireEntreprise.Core
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAllServices();
        Task<ServiceDTO> GetServiceById(int id);
        Task<ServiceDTO> AddService(ServiceDTO service);
        Task<bool> ModifyService(ServiceDTO service);
        Task<bool> RemoveService(int id);
    }
}