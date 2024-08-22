using DataAccessLayer.Models.Enum;

namespace DataAccessLayer.Models
{
    public class Ticket : EntityBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? ClosedAt { get; set; }
        public Guid AssignedTo { get; set; }
        public bool IsConfirmedFromClient { get; set; }
        public User User { get; set; } = null!;

        public ICollection<TicketComment> TicketComments { get; set; } = new HashSet<TicketComment>();
        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
        public Product? Product { get; set; }
        public Guid ProductId { get; set; }

    }
}
