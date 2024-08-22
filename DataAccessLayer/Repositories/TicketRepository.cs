using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(TicketComment comment)
        {
            await _context.TicketComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task EditTicketAsync(Ticket ticket)
        {


            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> GetTicketByIdWithCommentsAsync(Guid id)
        {
            return await _context.Tickets.Include(t => t.TicketComments)
                                        .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<ICollection<TicketComment>> GetCommentsByTicketAndClientAsync(Guid ticketId)
        {
            return await _context.TicketComments.Include(a => a.User)
                                 .Where(c => c.TicketId == ticketId)
                                 .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsForUserAsync(Guid id)
        {
            return await _context.Tickets.Where(t => t.UserId == id).ToListAsync();

        }

        public async Task<bool> AssignTicketAsync(Guid ticketId, Guid supportEmployeeId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            ticket.AssignedTo = supportEmployeeId;
            _context.Tickets.Update(ticket);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.TicketComments)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task AddAttachmentAsync(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Ticket> GetTicketsAsQueryable()
        {
            return _context.Tickets
                .Include(t => t.User)
                .AsQueryable();
        }

        public async Task<Attachment?> GetAttachmentByIdAsync(Guid ticketId)
        {
            
            return await _context.Attachments
                .FirstOrDefaultAsync(a => a.TicketId == ticketId);
        }
    }
}
