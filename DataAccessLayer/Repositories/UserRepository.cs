using DataAccessLayer.Configuration;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.DataAccessRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DataContext context
                             , ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ICollection<User>> GetAllExternalClientsAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Tickets)
                .Where(u => u.Role.Name == RolesConstent.Client)
                .ToListAsync();
        }

        public async Task<ICollection<User>> GetAllTeamSupportAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.Name == RolesConstent.Support)
                .ToListAsync();
        }

        public IQueryable<User> GetAllUsersAsQueryable()
        {
            return _context.Users
                .AsQueryable();
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Tickets)
                .ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            _logger.LogInformation("Fetching user by email: {Email}", email);

            var user = await _context.Users.Include(t => t.Role)
                                           .SingleOrDefaultAsync(t => t.Email == email);
            if (user == null)
            {
                _logger.LogWarning("No user found with email: {Email}", email);
            }
            return user;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
