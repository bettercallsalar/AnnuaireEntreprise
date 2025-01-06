// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<bool> AddEmployee(EmployeeDTO employee);
        Task<bool> UpdateEmployee(EmployeeDTO employee);
        Task<bool> DeleteEmployee(EmployeeDTO employee);

        // methods chercher par nom, prenom, email, telephone, site, service
        Task<List<EmployeeDTO>> SearchEmployeeByArg(string? nom, string? prenom, string? email, string? telephone, string? site, string? service);
    }
}