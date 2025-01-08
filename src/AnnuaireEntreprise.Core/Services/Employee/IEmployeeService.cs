// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Core
{
    // Interface for EmployeeService. The methods are implemented in EmployeeService.cs
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<EmployeeDTO> AddEmployee(CreateEmployeeDTO employee);
        Task<bool> UpdateEmployee(ModifyEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);
        Task<List<EmployeeDTO>> SearchEmployeeByArg(SearchEmployeeDTO searchEmployeeDTO);
        Task<int> GetTotalEmployeeCount();
    }
}