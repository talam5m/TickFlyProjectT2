using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class TicketFilterDto
    {
        public DataAccessLayer.Models.Enum.Status? Status { get; set; }
        public Guid? AssignedToId { get; set; }
        public Guid? ClientId { get; set; }
    }
}
