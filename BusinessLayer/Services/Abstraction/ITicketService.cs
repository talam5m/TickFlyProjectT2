using BusinessLayer.DTOs;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Abstraction
{
    public interface ITicketService
    {
        public Task<ResponseDto<object>> GetAllTicketsAsync();
        public Task<ResponseDto<ICollection<TicketDTO>>> GetTicketsForUserAsync(Guid id);
        public Task<ResponseDto<string>> AddTicketAsync(TicketDTO ticket);
        public Task<ResponseDto<string>> EditTicketAsync(UpdateTicketDTO ticket);
        public Task<ResponseDto<Guid>> AddCommentAsync(CommentDTO commentDto);
        public Task<Ticket> GetTicketByIdAsync(Guid id);

        public Task<ResponseDto<object>> GetCommentsForClientAsync(Guid ticketId);

        public Task<ResponseDto<bool>> ConfirmTicketAsync(Guid clientId, Guid ticketId);

        public Task<ResponseDto<Guid>> CloseTicketAsync(Guid ticketId);
        public Task<ResponseDto<string>> UploadAttachmentAsync(AttachmentDTO attachmentDto);
        public Task<ResponseDto<object>> DownloadAttachmentAsync(Guid attachmentId);
    }
}
