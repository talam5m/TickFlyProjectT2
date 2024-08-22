using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Enum;
using DataAccessLayer.Repositories;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<ResponseDto<bool>> ConfirmTicketAsync(Guid clientId, Guid ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdWithCommentsAsync(ticketId);

            if (ticket == null || ticket.UserId != clientId)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "Ticket not found or the client does not match"
                };
            }

            ticket.IsConfirmedFromClient = true;
            await _ticketRepository.EditTicketAsync(ticket);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Result = true
            };
        }


        public async Task<ResponseDto<Guid>> CloseTicketAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetTicketByIdWithCommentsAsync(ticketId);

            if (ticket == null)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "Ticket is not found"
                };
            }

            if (!ticket.IsConfirmedFromClient == true)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "Ticket has not been confirmed by client! "
                };
            }

            ticket.Status = Status.Closed;
            await _ticketRepository.EditTicketAsync(ticket);

            return new ResponseDto<Guid>

            {
                IsSuccess = true,
                Result = ticket.Id
            };
        }

        public async Task<ResponseDto<object>> GetCommentsForClientAsync(Guid ticketId)
        {
            var comments = await _ticketRepository.GetCommentsByTicketAndClientAsync(ticketId);

            if (comments == null)
            {
                return new ResponseDto<object>
                {
                    IsSuccess = false,
                    ErrorMessage = " Comments not found for such ticket or user"
                };
            }


            return new ResponseDto<object>

            {
                IsSuccess = true,
                Result = comments.Select(c => new
                {
                    UserId = c.UserId,
                    TicketId = c.TicketId,
                    Comment = c.Comment,
                    CommentedBy = c.CommentedBy,
                    CommentDate = c.CommentDate,
                    UserFirstName = c.User.FirstName,
                    UserLastName = c.User.LastName,
                    UserFullName = c.User.FirstName + c.User.LastName,
                }),
            };
        }

        public async Task<ResponseDto<Guid>> AddCommentAsync(CommentDTO commentDto)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(commentDto.TicketId);
            if (ticket == null)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "The Ticket not found"
                };
            }

            var comment = new TicketComment
            {
                Id = Guid.NewGuid(),
                TicketId = commentDto.TicketId,
                Comment = commentDto.Comment,
                CreatedDate = DateTime.Now,
                UserId = commentDto.UserId,
                CommentedBy = commentDto.CommentedBy
            };

            await _ticketRepository.AddCommentAsync(comment);

            return new ResponseDto<Guid>
            {
                IsSuccess = true,
                Result = comment.Id
            };
        }

        public async Task<ResponseDto<string>> AddTicketAsync(TicketDTO ticket)
        {

            var newTicket = new Ticket
            {
                Id = ticket.Id,
                UserId = ticket.UserId,
                Description = ticket.Description,
                Status = Status.New,
                CreatedDate = DateTime.Now,
                ProductId = ticket.ProductId
            };

            await _ticketRepository.AddTicketAsync(newTicket);
            return new ResponseDto<string>
            {
                IsSuccess = true,
                Result = "newTicket",
            };
        }

        public async Task<ResponseDto<string>> EditTicketAsync(UpdateTicketDTO ticketDto)
        {
            var editTicket = await _ticketRepository.GetTicketByIdAsync(ticketDto.Id);
            if (editTicket == null)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "Ticket not found"
                };

            }

            editTicket.Description = ticketDto.Description;
            editTicket.Status = ticketDto.Status;
            editTicket.Priority = ticketDto.Priority;

            await _ticketRepository.EditTicketAsync(editTicket);
            return new ResponseDto<string>
            {
                IsSuccess = true

            };
        }

        public async Task<ResponseDto<object>> GetAllTicketsAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var tickets = await _ticketRepository.GetTicketsAsync();
            return new ResponseDto<object>
            {
                IsSuccess = true,
                StatusCode = 200,
                TotalCount = tickets.Count(),
                Result = tickets.Select(a => new
                {
                    Id = a.Id,
                    Status = a.Status,
                    UserId = a.UserId,
                    Priority = a.Priority,
                    ProductId = a.ProductId,
                    Description = a.Description,
                    Comments = a.TicketComments?.Select(b => new
                    {
                        Comment = b.Comment,
                        CommentDate = b.CommentDate,
                        CommentedBy = b.CommentedBy,
                        TicketId = b.TicketId,
                        UserId = b.UserId
                    }).ToList(),
                    createdByUserId = a.User.Id,
                    CreatedByUserName = a.User.FirstName + " " + a.User.LastName,
                    AssignToUserId = a.AssignedTo,
                    AssignToUserName = users.FirstOrDefault(u => u.Id == a.AssignedTo)?.FirstName + " " + users.FirstOrDefault(u => u.Id == a.AssignedTo)?.LastName
                }).ToList()
            };
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            return await _ticketRepository.GetTicketByIdWithCommentsAsync(id);
        }

        public async Task<ResponseDto<ICollection<TicketDTO>>> GetTicketsForUserAsync(Guid id)
        {
            var tickets = await _ticketRepository.GetTicketsForUserAsync(id);
            return new ResponseDto<ICollection<TicketDTO>>
            {
                IsSuccess = true,
                StatusCode = 200,
                TotalCount = tickets.Count(),
                Result = tickets.Select(a => new TicketDTO
                {
                    Id = a.Id,
                    Status = a.Status,
                    UserId = a.UserId,
                    Priority = a.Priority,
                    ProductId = a.ProductId,
                    Description = a.Description
                }).ToList()

            };
        }

        public async Task<ResponseDto<string>> UploadAttachmentAsync(AttachmentDTO attachmentDto)
        {


            var ticket = await _ticketRepository.GetTicketByIdWithCommentsAsync(attachmentDto.TicketId);
            if (ticket == null)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "The ticket is not found "
                };
            }

            if (string.IsNullOrWhiteSpace(attachmentDto.FilePath))
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "The File Path field is required! "
                };
            }

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = $"{attachmentDto.TicketId}";
            var filePath = Path.Combine(path, "ّStaticFile", "Attachment");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Console.WriteLine($"Directory created at: {filePath}");
            }


            System.IO.File.WriteAllBytes(filePath + "/" + attachmentDto.TicketId, Convert.FromBase64String(attachmentDto.FilePath));


            var attachment = new Attachment
            {
                Id = Guid.NewGuid(),
                TicketId = attachmentDto.TicketId,
                FilePath = filePath,
                FileName = fileName,
            };

            await _ticketRepository.AddAttachmentAsync(attachment);

            return new ResponseDto<string>
            {
                IsSuccess = true,
                Result = filePath
            };
        }

        public async Task<ResponseDto<object>> DownloadAttachmentAsync(Guid ticketId)
        {
            var attachment = await _ticketRepository.GetAttachmentByIdAsync(ticketId);
            if (attachment == null)
            {
                return new ResponseDto<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "Attachment not found"
                };
            }

            var at = "";
            var filePath = attachment.FilePath + @"\" + attachment.FileName;
            if (File.Exists(filePath))
            {
                at = Convert.ToBase64String(File.ReadAllBytes(filePath));
            }

            var attachments = new 
            {
                FilePath = filePath,     
                attachment = at,                
            };

            return new ResponseDto<object>
            {
                IsSuccess = true,
                Result = attachments,
                StatusCode = 200
            };
        }
    }
}
