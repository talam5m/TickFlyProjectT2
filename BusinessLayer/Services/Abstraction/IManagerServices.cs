using BusinessLayer.DTOs;

namespace BusinessLayer.Services.Abstraction
{
    public interface IManagerServices
    {
        Task<ResponseDto<IEnumerable<SupportTeamDto>>> GetSupportTeamMembersAsync();
        Task<ResponseDto<IEnumerable<ExternalClientDto>>> GetExternalClientAsync();
        Task<ResponseDto<bool>> UpdateExternalClientAsync(Guid id, UpdateExternalClientDto dto);
        Task<ResponseDto<bool>> UpdateSupportEmployeeAsync(Guid id, UpdateSupportEmployeeDto dto);
        Task<ResponseDto<bool>> ChangeUserStatusAsync(Guid id, ChangeUserStatusDto dto);
        Task<ResponseDto<bool>> AssignTicketAsync(AssignTicketDto dto);
        Task<ResponseDto<IEnumerable<TicketDTO>>> GetTicketsAsync(TicketFilterDto filterDto);
        Task<ResponseDto<IEnumerable<TicketStatusCountDto>>> GetTicketStatusCountsAsync();
        Task<ResponseDto<IEnumerable<EmployeeProductivityDto>>> GetMostProductiveEmployeesAsync();
    }
}
