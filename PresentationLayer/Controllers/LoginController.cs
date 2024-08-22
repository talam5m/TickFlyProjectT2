using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserService userService, ILogger<LoginController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for login attempt.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Login attempt for user with email: {Email}", user.Email);
                var token = await _userService.LoginAsync(user);
                _logger.LogInformation("Login successful for user with email: {Email}", user.Email);
                return Ok(new { Token = token });
            }

            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Unauthorized login attempt for user with email: {Email}", user.Email);
                return Unauthorized("There is no user with the same password and email!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user with email: {Email}", user.Email);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
