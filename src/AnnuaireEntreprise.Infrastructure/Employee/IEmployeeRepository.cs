// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    // Interface for EmployeeRepository. The methods are implemented in EmployeeRepository.cs
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> AddEmployee(CreateEmployeeDTO employee);
        Task<bool> UpdateEmployee(ModifyEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);

        // methods chercher par nom, prenom, email, telephone, site, service
        Task<List<EmployeeDTO>> FetchEmployeesByArg(SearchEmployeeDTO searchEmployeeDTO);

        Task<int> FetchTotalEmployeeCount();
    }
}