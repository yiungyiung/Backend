using Backend.Model.DTOs;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Manager,Analyst")]
    [ApiController]
    public class VendorHierarchyController : ControllerBase
    {
        private readonly IVendorHierarchy _vendorHierarchy;

        public VendorHierarchyController(IVendorHierarchy vendorHierarchy)
        {
            _vendorHierarchy = vendorHierarchy;
        }

        [HttpGet]
        public async Task<ActionResult<List<VendorHierarchyDto>>> GetVendorHierarchy()
        {
            var hierarchy = await _vendorHierarchy.GetVendorHierarchyAsync();
            return Ok(hierarchy);
        }
    }
}
