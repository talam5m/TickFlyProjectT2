namespace BusinessLayer.DTOs
{
    public class CommentDTO
    {
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string CommentedBy { get; set; }
        public DateTime CommentDate { get; set; }
       
    }
}
