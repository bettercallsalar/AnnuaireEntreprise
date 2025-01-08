// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class EmployeeService : IEmployeeService // Implementing the methods from the interface
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Get all employees. Includes pagination
        public async Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize)
        {
            return await _employeeRepository.GetAllEmployees(page, pageSize);
        }

        // Get employee by id
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        // Add employee
        public async Task<EmployeeDTO> AddEmployee(CreateEmployeeDTO employee)
        {
            return await _employeeRepository.AddEmployee(employee);
        }

        // Update employee
        public async Task<bool> UpdateEmployee(ModifyEmployeeDTO employee)
        {
            return await _employeeRepository.UpdateEmployee(employee);
        }

        // Delete employee
        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        // Search employee by argument. Used for search functionality
        public async Task<List<EmployeeDTO>> SearchEmployeeByArg(SearchEmployeeDTO searchEmployeeDTO)
        {
            return await _employeeRepository.FetchEmployeesByArg(searchEmployeeDTO);
        }

        // Get total employee count. Used for pagination
        public async Task<int> GetTotalEmployeeCount()
        {
            return await _employeeRepository.FetchTotalEmployeeCount();
        }
    }
}