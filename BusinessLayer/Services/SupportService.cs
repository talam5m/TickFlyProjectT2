using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class SupportService : ISupportService
    {
        private readonly ITicketRepository _ticketRepository;

        public SupportService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<ResponseDto<IEnumerable<TicketDTO>>> GetAssignedTicketsAsync(Guid supportEmployeeId)
        {

            var tickets = await _ticketRepository.GetTicketsAsQueryable()
                .Where(t => t.AssignedTo == supportEmployeeId)
                .ToListAsync();

            var assignedTickets = tickets.Select(t => new TicketDTO
            {
                Id = t.Id,
                UserId = t.User.Id,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority
            });

            return new ResponseDto<IEnumerable<TicketDTO>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = assignedTickets
            };
        }
    }
}
