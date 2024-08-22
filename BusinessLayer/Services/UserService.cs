using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ITokenService tokenService,
                           IEmailService emailSender,
                           IConfiguration configuration,
                           IRoleRepository roleRepository,
                           IUserRepository userRepository,
                           ILogger<UserService> logger,
                           IPasswordHasher<User> passwordHasher)
        {
            _logger = logger;
            _emailService = emailSender;
            _tokenService = tokenService;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<string>> LoginAsync(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLogin.Email);
            if (user == null)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid Email ",
                    StatusCode = 400
                };
            }

            if (!user.IsActive)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "You can not login because the user does not activated you should contact Support team ",
                    StatusCode = 400
                };

            }

            var passwordVerify = _passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);
            if (passwordVerify == PasswordVerificationResult.Failed)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid  Password.",
                    StatusCode = 400
                };
            }

            var token = _tokenService.GenerateToken(user);
            return new ResponseDto<string>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = token
            };
        }

        public async Task<ResponseDto<Guid>> RegisterClientAsync(UserRegister userDto)
        {

            var existUser = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (existUser != null)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "Email already in use!",
                };
            }

            var role = await _roleRepository.GetRoleAsync(RolesConstent.Client);

            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                IsActive = true,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                MobileNumber = userDto.MobileNumber,
                DateOfBirth = userDto.DateOfBirth,
                Address = userDto.Address,
                Password = userDto.Password,

                Role = role
            };

            user.Password = _passwordHasher.HashPassword(user, userDto.Password);

            await _userRepository.AddUserAsync(user);

            return new ResponseDto<Guid>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = userId
            };
        }

        public async Task<ResponseDto<Guid>> RegisterSupportTeamAsync(UserRegister userDto)
        {
            var existUser = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (existUser != null)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "Email already in use!",
                };
            }

            var role = await _roleRepository.GetRoleAsync(RolesConstent.Support);

            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                IsActive = true,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                MobileNumber = userDto.MobileNumber,
                DateOfBirth = userDto.DateOfBirth,
                Address = userDto.Address,
                Password = userDto.Password,
                Role = role
            };

            user.Password = _passwordHasher.HashPassword(user, userDto.Password);

            await _userRepository.AddUserAsync(user);

            return new ResponseDto<Guid>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = userId
            };
        }

        public async Task<ResponseDto<string>> UploadImageAsync(ImageUpload imageUpload)
        {
            var user = await _userRepository.GetUserByIdAsync(imageUpload.UserId);
            if (user == null)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "The User is not found "
                };
            }
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(path, "ّStaticFile", "Images");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Console.WriteLine($"Directory created at: {filePath}");
            }

            var fullPath = filePath + "/" + imageUpload.UserId + "." + imageUpload.Type;
            System.IO.File.WriteAllBytes(filePath + "/" + imageUpload.UserId + "." + imageUpload.Type, Convert.FromBase64String(imageUpload.Image));

            user.ImagePath = fullPath;
            await _userRepository.UpdateUserAsync(user);

            return new ResponseDto<string>
            {
                IsSuccess = true,
                Result = filePath
            };
        }

        public async Task<ResponseDto<UserInfoDTO>> GetUserInfoAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<UserInfoDTO>
                {
                    IsSuccess = false,
                    ErrorMessage = "The User is not found "
                };
            }

            var image = "";
            var filePath = user.ImagePath  ;
            if (File.Exists(filePath))
            {
                image = Convert.ToBase64String(File.ReadAllBytes(filePath));
            }

            _logger.LogInformation(filePath);

            
            var userInfoDto = new UserInfoDTO
            {
                Id = user.Id,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNumber = user.MobileNumber,
                IsActive = user.IsActive,
                Email = user.Email,
                Role = user.Role.Name,
                Image = image,
            };

            return new ResponseDto<UserInfoDTO>
            {
                IsSuccess = true,
                Result = userInfoDto,
                StatusCode = 200
            };

        }

        public async Task<ResponseDto<object>> ResetPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if(user==null)
            {
                return new ResponseDto<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "user Not found"
                };
            }

            user.Password= _passwordHasher.HashPassword(user, "1234+Zxcv");
            await _userRepository.UpdateUserAsync(user);
            var message = $"We have generated a new password for your account as requested. Please find your new login details below:\r\n\r\nEmail: {user.Email} \r\nNew Password: \"1234+Zxcv\"";
            await _emailService.SendEmailAsync(email, "Password Reset Request", message);

            return new ResponseDto<object>
            {
                IsSuccess=true,
                StatusCode=200,  
            };
        }
    }
}
