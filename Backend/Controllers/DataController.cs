using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _dataService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetUsers: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching users");
            }
        }
    }
}
