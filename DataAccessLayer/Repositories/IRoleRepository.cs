using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public interface IRoleRepository
    {
        Task<UserRole> GetRoleAsync(string name);
    }
}
