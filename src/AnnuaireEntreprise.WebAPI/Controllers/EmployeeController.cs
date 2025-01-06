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
        [HttpGet("get-all-employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
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
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO employee)
        {
            var result = await _employeeService.AddEmployee(employee);
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
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDTO employee)
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
    }
}
