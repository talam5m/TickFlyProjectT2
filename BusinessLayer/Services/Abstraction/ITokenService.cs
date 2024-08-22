using DataAccessLayer.Models;

namespace BusinessLayer.Services.Abstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
