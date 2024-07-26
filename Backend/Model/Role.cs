using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}