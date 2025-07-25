using Microsoft.EntityFrameworkCore.Storage;

namespace UserManagement.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; } = true; // Default true
    }  
}


