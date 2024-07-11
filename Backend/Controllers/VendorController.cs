using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var vendors = await _vendorService.GetAllVendorsAsync();
            return Ok(vendors);
        }
    }
}
