// Author: Salar
// Created: 06/01/2024

using Microsoft.AspNetCore.Mvc;
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Core;

namespace WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("get-all-services")]
        public async Task<ActionResult<List<ServiceDTO>>> GetAllServices()
        {
            return await _serviceService.GetAllServices();
        }

        [HttpGet("get-service-by-id/{id}")]
        public async Task<ActionResult<ServiceDTO>> GetServiceById(int id)
        {
            return await _serviceService.GetServiceById(id);
        }

        [HttpPost("add-service")]
        public async Task<ActionResult<ServiceDTO>> AddService(ServiceDTO service)
        {
            return await _serviceService.AddService(service);
        }

        [HttpPut("modify-service")]
        public async Task<ActionResult<bool>> ModifyService(ServiceDTO service)
        {
            return await _serviceService.ModifyService(service);
        }

        [HttpDelete("remove-service/{id}")]
        public async Task<ActionResult<bool>> RemoveService(int id)
        {
            return await _serviceService.RemoveService(id);
        }
    }
}