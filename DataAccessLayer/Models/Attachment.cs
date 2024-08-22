namespace DataAccessLayer.Models
{
    public class Attachment : EntityBase
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}
