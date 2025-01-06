// Author: Salar
// Created: 06/01/2024

using Microsoft.AspNetCore.Mvc;
using AnnuaireEntreprise.Model.Models;
using AnnuaireEntreprise.Core;

namespace WebAPI
{
    [Route("api/[controller]")]
    [ApiController]

    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        /// <summary>
        /// Get all sites
        /// </summary>
        /// <returns>
        /// List of sites
        /// </returns>
        /// <response code="200">Returns the list of sites</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        [HttpGet("get-all-sites")]
        public async Task<IActionResult> GetAllSites()
        {
            var sites = await _siteService.GetAllSites();
            return Ok(sites);
        }

        /// <summary>
        /// Get site by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Site
        /// </returns>
        /// <response code="200">Returns the site</response>
        /// <response code="200">If the site is not found, a site with no info and Id : 0.</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpGet("get-site-by-id/{id}")]
        public async Task<IActionResult> GetSiteById(int id)
        {
            var site = await _siteService.GetSiteById(id);
            if (site == null)
            {
                return NotFound();
            }
            return Ok(site);
        }

        /// <summary>
        /// Add new site
        /// </summary>
        /// <param name="site"></param>
        /// <returns>
        /// Site
        /// </returns>
        /// <response code="200">Returns the site</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpPost("add-site")]
        public async Task<IActionResult> AddSite(SiteDTO site)
        {
            var id = await _siteService.AddSite(site);
            return Ok(id);
        }

        /// <summary>
        /// Update site
        /// </summary>
        /// <param name="site"></param>
        /// <returns>
        /// Site
        /// </returns>
        /// <response code="200">Returns the site</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpPut("update-site")]
        public async Task<IActionResult> UpdateSite(SiteDTO site)
        {
            var result = await _siteService.UpdateSite(site);
            if (result)
            {
                return Ok(site);
            }
            return BadRequest();
        }

        /// <summary>
        /// Delete site
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Site
        /// </returns>
        /// <response code="200">Returns the site</response>
        /// <response code="500">If an error occurs</response>
        /// <returns></returns>
        /// 
        [HttpDelete("delete-site/{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var result = await _siteService.DeleteSite(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}