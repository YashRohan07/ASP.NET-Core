using Microsoft.AspNetCore.Identity;   // Inherit from IdentityUser to extend user properties

namespace UserManagement.Models
{
    // Extend IdentityUser with additional custom properties
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;    
    }
}
