using System.Data;

namespace Backend.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public string Name { get; set; }
        public string Contact_Number { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        //hi
    }
}
