using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = null!;
        public string? Image { get; set; }

    }
}
