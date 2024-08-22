namespace DataAccessLayer.Models
{
    public class TicketComment : EntityBase
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CommentDate { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public string? CommentedBy { get; set; }
    }
}
