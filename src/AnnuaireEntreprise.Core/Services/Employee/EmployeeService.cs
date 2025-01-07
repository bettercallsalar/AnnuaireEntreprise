// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Infrastructure;

namespace AnnuaireEntreprise.Core
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize)
        {
            return await _employeeRepository.GetAllEmployees(page, pageSize);
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<CreateEmployeeDTO> AddEmployee(CreateEmployeeDTO employee)
        {
            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<bool> UpdateEmployee(CreateEmployeeDTO employee)
        {
            return await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<List<EmployeeDTO>> SearchEmployeeByArg(SearchEmployeeDTO searchEmployeeDTO)
        {
            return await _employeeRepository.FetchEmployeesByArg(searchEmployeeDTO);
        }

        public async Task<int> GetTotalEmployeeCount()
        {
            return await _employeeRepository.FetchTotalEmployeeCount();
        }
    }
}