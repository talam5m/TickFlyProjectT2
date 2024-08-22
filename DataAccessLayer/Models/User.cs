namespace DataAccessLayer.Models
{
    public class User
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
        public string? ImagePath { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        public ICollection<TicketComment> TicketComments { get; set; } = new HashSet<TicketComment>();

        public UserRole Role { get; set; } = null!;
        public Guid RoleId { get; set; }
    }
}
