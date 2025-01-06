// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Core
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<bool> AddEmployee(EmployeeDTO employee);
        Task<bool> UpdateEmployee(EmployeeDTO employee);
        Task<bool> DeleteEmployee(EmployeeDTO employee);
    }
}