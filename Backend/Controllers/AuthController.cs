using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;
using System.Security.Cryptography;
using Backend.Model.DTOs; // Ensure the correct namespace is used for ChangePasswordDto
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IAuthService _authService; // Add IAuthService to the controller

        public AuthController(ApplicationDbContext context, IConfiguration configuration, HtmlEncoder htmlEncoder, IAuthService authService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _htmlEncoder = htmlEncoder ?? throw new ArgumentNullException(nameof(htmlEncoder));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService)); // Initialize IAuthService
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Login data is null.");
            }

            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !VerifyPassword(model.Password, user.PasswordHash) || !user.IsActive)
            {
                return Unauthorized("Invalid credentials or inactive account.");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", _htmlEncoder.Encode(user.Name)),
                new Claim("contact_number", _htmlEncoder.Encode(user.Contact_Number)),
                new Claim("is_active", user.IsActive.ToString()),
                new Claim("user_id", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.OldPassword) || string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return BadRequest("Invalid password data.");
            }

            // Find the user by UserId
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Verify old password
            if (!VerifyPassword(dto.OldPassword, user.PasswordHash))
            {
                return BadRequest("Old password is incorrect.");
            }

            // Hash the new password
            var newPasswordHash = HashPassword(dto.NewPassword);

            // Update password in database
            user.PasswordHash = newPasswordHash;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Password changed successfully.");
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedInputPassword = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword));
                var hashedInputPasswordBase64 = Convert.ToBase64String(hashedInputPassword);
                return hashedInputPasswordBase64 == storedHash;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedPassword);
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
