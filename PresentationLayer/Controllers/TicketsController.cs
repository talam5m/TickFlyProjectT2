using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("all-tickets")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _ticketService.GetAllTicketsAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("client's-tickets")]
        public async Task<IActionResult> GetTicketsByClientId(Guid id)
        {
            var result = await _ticketService.GetTicketsForUserAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket([FromBody] TicketDTO addTicket)
        {
            var result = await _ticketService.AddTicketAsync(addTicket);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO comment)
        {
            var result = await _ticketService.AddCommentAsync(comment);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("Update-Ticket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketDTO ticket)
        {
            var result = await _ticketService.EditTicketAsync(ticket);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [Authorize]
        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments(Guid ticketId)
        {
            var result = await _ticketService.GetCommentsForClientAsync(ticketId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles =RolesConstent.Client)]
        [HttpPost("ConfirmTicketByClient")]
        public async Task<IActionResult> ConfirmTicketByClient(Guid clientId, Guid ticketId)
        {
            var result = await _ticketService.ConfirmTicketAsync(clientId, ticketId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles =RolesConstent.Support)]
        [HttpPost("CloseTicketBySupport")]
        public async Task<IActionResult> CloseTicketBySupport(Guid ticketId)
        {
            var result = await _ticketService.CloseTicketAsync(ticketId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("upload-attachment")]
        public async Task<IActionResult> UploadAttachment([FromBody] AttachmentDTO uploadDto)
        {
            var result = await _ticketService.UploadAttachmentAsync(uploadDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("download-attachment")]
        public async Task<IActionResult> DownloadAttachment(Guid ticketId)
        {
            var result = await _ticketService.DownloadAttachmentAsync(ticketId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
