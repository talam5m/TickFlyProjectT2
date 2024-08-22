using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class TicketWithCommentsDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public string CommentedBy { get; set; }

        //   public string Status { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
