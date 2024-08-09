using System.ComponentModel.DataAnnotations;

namespace Backend.Model.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Phone]
        public string Contact_Number { get; set; }

        public bool IsActive { get; set; }
    }
}