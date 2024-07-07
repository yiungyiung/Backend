using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Backend.Services;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            try
            {
                var user = new User
                {
                    Email = userDto.Email,
                    RoleId = userDto.RoleId,
                    Contact_Number = userDto.Contact_Number,
                    Name = userDto.Name,
                    IsActive = true,
                };

                var addedUser = await _adminService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetAllUsers), new { id = addedUser.UserId }, addedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (userDto == null || userId != userDto.UserId)
            {
                return BadRequest("User data is invalid.");
            }

            try
            {
                var user = new User
                {
                    UserId = userDto.UserId,
                    Name = userDto.Name,
                    Contact_Number = userDto.Contact_Number,
                    IsActive = userDto.IsActive,
                    RoleId = userDto.RoleId
                    // Don't include Email or PasswordHash here
                };

                var updatedUser = await _adminService.UpdateUserAsync(user);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateUser: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
