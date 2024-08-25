using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("vendors")]
        public async Task<IActionResult> GetAllVendors()
        {
            try
            {
                var vendors = await _vendorService.GetAllVendorsAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("add")]
        public async Task<IActionResult> AddVendor([FromBody] VendorDto vendorDto)
        {
            try
            {
                if (vendorDto == null)
                {
                    return BadRequest("Vendor data is null");
                }

                var addedVendor = await _vendorService.AddVendorAsync(vendorDto);
                if (addedVendor.TierID != 1) { 
                addedVendor = await _vendorService.GetVendorHierarchyAsync(vendorDto.parentVendorIDs,addedVendor);
                }
                return CreatedAtAction(nameof(GetVendorById), new { id = addedVendor.VendorID }, addedVendor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorById(int id)
        {
            try
            {
                var vendor = await _vendorService.GetVendorByIdAsync(id);
                if (vendor == null)
                {
                    return NotFound($"Vendor with ID {id} not found");
                }

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _vendorService.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("tiers")]
        public async Task<IActionResult> GetTiers()
        {
            try
            {
                var tiers = await _vendorService.GetTiersAsync();
                return Ok(tiers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("vendors/byTier/{tierId}")]
        public async Task<IActionResult> GetVendorsByTier(int tierId)
        {
            try
            {
                var vendors = await _vendorService.GetVendorsByTierAsync(tierId);
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("byUserId/{userId}")]
        public async Task<IActionResult> GetVendorIdByUserId(int userId)
        {
            try
            {
                var vendorId = await _vendorService.GetVendorIdByUserIdAsync(userId);
                if (!vendorId.HasValue)
                {
                    return NotFound($"No vendor found for user with ID {userId}");
                }
                return Ok(vendorId.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

    }
}
