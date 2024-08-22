using DataAccessLayer.Models.Enum;

namespace BusinessLayer.DTOs
{
    public class UpdateTicketDTO
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
