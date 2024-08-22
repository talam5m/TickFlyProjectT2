using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpGet("assigned-tickets")]
        [Authorize(Roles = RolesConstent.Support)]
        public async Task<IActionResult> GetAssignedTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found.");
            }

            Guid.TryParse(userId, out var Supportid);

            var result = await _supportService.GetAssignedTicketsAsync(Supportid);
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result.Result);
        }
    }
}
