using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; } = true;

    }
}
