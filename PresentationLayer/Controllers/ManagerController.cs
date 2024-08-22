using BusinessLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerServices;
        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        [HttpGet("support-Team")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetSupportTeamMembers() 
        {
            var result = await _managerServices.GetSupportTeamMembersAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpGet("external-Clients")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetExternalClients()
        {
            var result = await _managerServices.GetExternalClientAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpPut("support-employees/{id}")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> EditSupportEmployee(Guid id, [FromBody] UpdateSupportEmployeeDto dto)
        {
            var response = await _managerServices.UpdateSupportEmployeeAsync(id, dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("external-clients/{id}")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> EditExternalClient(Guid id, [FromBody] UpdateExternalClientDto dto)
        {
            var response = await _managerServices.UpdateExternalClientAsync(id, dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("assign-ticket")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketDto dto)
        {
            var response = await _managerServices.AssignTicketAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("support-employees-status/{id}")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> SupportEmployee(Guid id, [FromBody] ChangeUserStatusDto dto)
        {
            var response = await _managerServices.ChangeUserStatusAsync(id, dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("tickets")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetTickets([FromQuery] TicketFilterDto filterDto)
        {
            var result = await _managerServices.GetTicketsAsync(filterDto);
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result.Result);
        }
        [HttpGet("ticket-status-counts")]
        [Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetTicketStatusCounts()
        {
            var result = await _managerServices.GetTicketStatusCountsAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpGet("most-productive-employees")]
        //[Authorize(Roles = RolesConstent.SupportManager)]
        public async Task<IActionResult> GetMostProductiveEmployees()
        {
            var result = await _managerServices.GetMostProductiveEmployeesAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return Ok(result.Result);
        }
    }
}
