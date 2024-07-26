using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public int? RoleId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Phone]
        public string Contact_Number { get; set; }

        public Role Role { get; set; }

        public bool IsActive { get; set; }
    }
}