using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly HtmlEncoder _htmlEncoder;

        public AdminController(IAdminService adminService, HtmlEncoder htmlEncoder)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _htmlEncoder = htmlEncoder ?? throw new ArgumentNullException(nameof(htmlEncoder));
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("users")]
        public async Task<ActionResult<User>> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            try
            {
                var user = new User
                {
                    Email = _htmlEncoder.Encode(userDto.Email),
                    RoleId = userDto.RoleId,
                    Contact_Number = _htmlEncoder.Encode(userDto.Contact_Number),
                    Name = _htmlEncoder.Encode(userDto.Name),
                    IsActive = true,
                };

                var addedUser = await _adminService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetAllUsers), new { id = addedUser.UserId }, addedUser);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("users/{userId}")]
        public async Task<ActionResult<User>> UpdateUser(int userId, [FromBody] UserDto userDto)
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
                    Name = _htmlEncoder.Encode(userDto.Name),
                    Contact_Number = _htmlEncoder.Encode(userDto.Contact_Number),
                    IsActive = userDto.IsActive,
                    RoleId = userDto.RoleId
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}