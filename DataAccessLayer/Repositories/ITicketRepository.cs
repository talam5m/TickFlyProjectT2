using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public interface ITicketRepository
    {
        public  Task<ICollection<Ticket>> GetTicketsForUserAsync(Guid id);
        public  Task<ICollection<Ticket>> GetTicketsAsync();
        public Task<Ticket> AddTicketAsync(Ticket ticket);
        public Task EditTicketAsync(Ticket ticket);
        public Task AddCommentAsync(TicketComment comment);
        public Task<Ticket> GetTicketByIdWithCommentsAsync(Guid id);
        public Task<Ticket> GetTicketByIdAsync(Guid id);
        Task<bool> AssignTicketAsync(Guid ticketId, Guid supportEmployeeId);
        public Task<ICollection<TicketComment>> GetCommentsByTicketAndClientAsync( Guid ticketId);
        public Task AddAttachmentAsync(Attachment attachment);
        IQueryable<Ticket> GetTicketsAsQueryable();
        public Task<Attachment?> GetAttachmentByIdAsync(Guid attachmentId);
    }
}
