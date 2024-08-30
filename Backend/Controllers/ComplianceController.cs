using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [Route("api/[controller]")]
    [ApiController]

    public class ComplianceController : ControllerBase
    {
        private readonly ComplianceService _complianceService;

        public ComplianceController(ComplianceService complianceService)
        {
            _complianceService = complianceService;
        }

        [HttpGet("compliance-data")]
        public IActionResult GetComplianceData()
        {
            var result = _complianceService.GetComplianceDataForPastFiveYears();
            return Ok(result);
        }
    }
}