using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IUserService userService,
                                  IEmailService emailService,
                                  ILogger<AccountsController> logger)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpPost("register-client")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for registration attempt.");
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterClientAsync(userRegister);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            var result = await _userService.ResetPasswordAsync(email);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }


            return Ok(result);
        }

        [HttpPost("support-register")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> RegisterSupportTeam([FromBody] UserRegister userRegister)
        {
            var result = await _userService.RegisterSupportTeamAsync(userRegister);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(ImageUpload image)
        {
            var result = await _userService.UploadImageAsync(image);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("User-Info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = GetUserIdFromToken();

            var userInfo = await _userService.GetUserInfoAsync(userId);

            if (userInfo == null)
            {
                return NotFound("User not found");
            }

            return Ok(userInfo);
        }

        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim.Value);
        }


    }


}


