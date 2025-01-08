// Author: Salar
// Created: 06/01/2024
using AnnuaireEntreprise.Model.Models;

namespace AnnuaireEntreprise.Maui.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApiService _apiService;

        // Constructor
        public EmployeeService()
        {
            _apiService = new ApiService();
        }

        // Ferch All Employees from API. 
        public async Task<List<EmployeeDTO>> GetAllEmployees(int page, int pageSize)
        {
            try
            {
                Console.WriteLine($"GET Request: Employee/get-all-employees/{page}/{pageSize}");
                var result = await _apiService.GetAsync<List<EmployeeDTO>>($"Employee/get-all-employees/{page}/{pageSize}");
                if (result == null || !result.Any())
                {
                    Console.WriteLine("No employees found or response is empty.");
                    return new List<EmployeeDTO>();
                }

                Console.WriteLine($"Loaded {result.Count} employees.");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
                return new List<EmployeeDTO>();
            }
        }

        // Fetch Employee by Id
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            try
            {
                var result = await _apiService.GetAsync<EmployeeDTO>($"Employee/get-employee-by-id/{id}");
                if (result != null)
                {
                    return result;
                }
                return new EmployeeDTO
                {
                    Nom = string.Empty,
                    Prenom = string.Empty,
                    Email = string.Empty,
                    TelephonePortable = string.Empty,
                    Site = new SiteDTO
                    {
                        Id = 0,
                        Ville = string.Empty
                    },
                    Service = new ServiceDTO
                    {
                        Id = 0,
                        Nom = string.Empty
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new EmployeeDTO
                {
                    Nom = string.Empty,
                    Prenom = string.Empty,
                    Email = string.Empty,
                    TelephonePortable = string.Empty,
                    Site = new SiteDTO
                    {
                        Id = 0,
                        Ville = string.Empty
                    },
                    Service = new ServiceDTO
                    {
                        Id = 0,
                        Nom = string.Empty
                    }
                };
            }
        }

        // Add Employee
        public async Task<CreateEmployeeDTO> AddEmployee(CreateEmployeeDTO employee)
        {
            try
            {
                Console.WriteLine($"POST Request: Employee/add-employee");
                Console.WriteLine($"Employee: {employee}");
                var result = await _apiService.PostAsync<CreateEmployeeDTO, CreateEmployeeDTO>("Employee/add-employee", employee);
                if (result != null)
                {
                    return result;
                }
                return new CreateEmployeeDTO
                {
                    Nom = string.Empty,
                    Prenom = string.Empty,
                    Email = string.Empty,
                    TelephonePortable = string.Empty,
                    SiteId = 0,
                    ServiceId = 0
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CreateEmployeeDTO
                {
                    Nom = string.Empty,
                    Prenom = string.Empty,
                    Email = string.Empty,
                    TelephonePortable = string.Empty,
                    SiteId = 0,
                    ServiceId = 0
                };
            }
        }

        // Update Employee
        public async Task<bool> UpdateEmployee(ModifyEmployeeDTO employee)
        {
            try
            {
                var result = await _apiService.PutAsync("Employee/update-employee", employee);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
        }

        // Delete Employee
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                var result = await _apiService.DeleteAsync($"Employee/delete-employee/{id}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Search Employee by Argument. 
        public async Task<List<EmployeeDTO>?> SearchEmployeeByArg(SearchEmployeeDTO searchEmployeeDTO)
        {
            try
            {
                var result = await _apiService.PostAsync<SearchEmployeeDTO, List<EmployeeDTO>>("Employee/search-employee-by-arg", searchEmployeeDTO);
                if (result != null)
                {
                    return result;
                }
                return new List<EmployeeDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
                return new List<EmployeeDTO>();
            }
        }

        // Get Total Employee Count
        public async Task<int> GetTotalEmployeeCount()
        {
            try
            {
                var result = await _apiService.GetAsync<int>("Employee/get-total-employee-count");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }

}