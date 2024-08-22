using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<ICollection<User>> GetAllUsersAsync();
        Task<ICollection<User>> GetAllTeamSupportAsync();
        Task<ICollection<User>> GetAllExternalClientsAsync();
        IQueryable<User> GetAllUsersAsQueryable();


    }
}
