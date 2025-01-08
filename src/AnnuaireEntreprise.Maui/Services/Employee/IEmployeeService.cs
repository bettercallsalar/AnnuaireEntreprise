// Author: Salar
// Created Date: 06/01/2025
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Maui.Services.Employee
{
    // Interface for Employee Service
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<CreateEmployeeDTO> AddEmployee(CreateEmployeeDTO employee);
        Task<bool> UpdateEmployee(ModifyEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);
        Task<List<EmployeeDTO>?> SearchEmployeeByArg(SearchEmployeeDTO searchEmployeeDTO);
        Task<int> GetTotalEmployeeCount();
    }
}