using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace UserManagement.DTOs
{
    public class UserUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        
    }
}
