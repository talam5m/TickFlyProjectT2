using DataAccessLayer.Models.Enum;

namespace BusinessLayer.DTOs
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Guid ProductId { get; set; }
       public List<CommentDTO>? Comments { get; set; }

    }
}
