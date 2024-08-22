namespace BusinessLayer.DTOs
{
    public class SupportTeamDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!; 
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int TicketCount { get; set; }
    }
}
