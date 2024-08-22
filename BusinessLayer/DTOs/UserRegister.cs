
namespace BusinessLayer.DTOs
{
    public class UserRegister
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string MobileNumber { get; set; } = null!;
    }
}
