namespace UserManagement.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
