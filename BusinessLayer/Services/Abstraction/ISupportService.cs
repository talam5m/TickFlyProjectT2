using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstraction
{
    public interface ISupportService
    {
        Task<ResponseDto<IEnumerable<TicketDTO>>> GetAssignedTicketsAsync(Guid supportEmployeeId);
    }
}
