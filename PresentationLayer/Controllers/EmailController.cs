using BusinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailSender;

        public EmailController(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromQuery] string toEmail, [FromQuery] string subject, [FromQuery] string message)
        {
            await _emailSender.SendEmailAsync(toEmail, subject, message);
            return Ok("Email sent successfully!");
        }
    }
}
