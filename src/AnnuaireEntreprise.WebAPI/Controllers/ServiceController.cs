// Author: Salar
// Created: 06/01/2024

using Microsoft.AspNetCore.Mvc;
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Core;

namespace WebAPI
{
    // Service Controller
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns>
        /// List of services
        /// </returns>
        /// <returns></returns>
        [HttpGet("get-all-services")]
        public async Task<ActionResult<List<ServiceDTO>>> GetAllServices()
        {
            return await _serviceService.GetAllServices();
        }

        /// <summary>
        /// Get service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Service
        /// </returns>
        /// <returns></returns>
        /// 

        [HttpGet("get-service-by-id/{id}")]
        public async Task<ActionResult<ServiceDTO>> GetServiceById(int id)
        {
            return await _serviceService.GetServiceById(id);
        }

        /// <summary>
        /// // Add Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// 
        [HttpPost("add-service")]
        public async Task<ActionResult<ServiceDTO>> AddService(ServiceDTO service)
        {
            return await _serviceService.AddService(service);
        }

        /// <summary>
        ///  Modify Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        /// 
        [HttpPut("modify-service")]
        public async Task<ActionResult<bool>> ModifyService(ServiceDTO service)
        {
            return await _serviceService.ModifyService(service);
        }

        /// <summary>
        /// Remove Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("remove-service/{id}")]
        public async Task<ActionResult<bool>> RemoveService(int id)
        {
            return await _serviceService.RemoveService(id);
        }
    }
}