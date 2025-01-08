// Author: Salar
// Created: 06/01/2024

using Microsoft.AspNetCore.Mvc;
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Core;

namespace WebAPI
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>
        /// List of employees
        /// </returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        [HttpGet("get-all-employees/{page}/{pageSize}")]
        public async Task<IActionResult> GetAllEmployees(int page, int pageSize)
        {
            Console.WriteLine("Get all employees");
            var employees = await _employeeService.GetAllEmployees(page, pageSize);

            return Ok(employees);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Employee
        /// </returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="200">If the employee is not found, an employee qith no info and Id : 0.</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpGet("get-employee-by-id/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>
        /// Employee
        /// </returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDTO employee)
        {
            Console.WriteLine("Add employee");
            Console.WriteLine(employee);
            var result = await _employeeService.AddEmployee(employee);
            Console.WriteLine(result);
            return Ok(result);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>
        /// Employee
        /// </returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        ///    
        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] ModifyEmployeeDTO employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            if (result)
            {
                return Ok(employee);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>
        /// Employee
        /// </returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpDelete("delete-employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Search employee by arguments
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="siteId"></param>
        /// <param name="serviceId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// List of employees
        /// </returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpPost("search-employee-by-arg")]
        public async Task<IActionResult> SearchEmployeeByArg([FromBody] SearchEmployeeDTO searchEmployeeDTO)
        {
            Console.WriteLine("Search employee by arg");
            Console.WriteLine(searchEmployeeDTO.Nom);
            var employees = await _employeeService.SearchEmployeeByArg(searchEmployeeDTO);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Nom);
            }
            return Ok(employees);
        }
        /// <summary>
        /// Get total employee count
        /// </summary>
        /// <returns>
        /// Total employee count
        /// </returns>
        /// <response code="200">Returns the total employee count</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpGet("get-total-employee-count")]
        public async Task<IActionResult> GetTotalEmployeeCount()
        {
            var count = await _employeeService.GetTotalEmployeeCount();
            return Ok(count);
        }
    }
}
