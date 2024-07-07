namespace Backend.Model
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Contact_Number { get; set; }

        public bool IsActive { get; set; }
    }
}