// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Infrastructure
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<CreateEmployeeDTO> AddEmployee(CreateEmployeeDTO employee);
        Task<bool> UpdateEmployee(CreateEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);

        // methods chercher par nom, prenom, email, telephone, site, service
        Task<List<EmployeeDTO>> FetchEmployeesByArg(SearchEmployeeDTO searchEmployeeDTO);

        Task<int> FetchTotalEmployeeCount();
    }
}