// Author: Salar
// Created Date: 06/01/2025
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Maui.Services.Service
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