using Backend.Model;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

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
                addedVendor = await _vendorService.GetVendorHierarchyAsync(vendorDto.parentVendorIDs,addedVendor);
                return CreatedAtAction(nameof(GetVendorById), new { id = addedVendor.VendorID }, addedVendor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

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
    }
}
