using BusinessLayer.DTOs;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Abstraction
{
    public interface IUserService
    {
        Task<ResponseDto<string>> LoginAsync(UserLogin userLogin);
        Task<ResponseDto<Guid>> RegisterClientAsync(UserRegister user);
        Task<ResponseDto<Guid>> RegisterSupportTeamAsync(UserRegister user);
        Task<ResponseDto<string>> UploadImageAsync(ImageUpload imageUpload);
        public Task<ResponseDto<UserInfoDTO>> GetUserInfoAsync(Guid userId);
        Task<ResponseDto<object>> ResetPasswordAsync(string email);

    }

}
