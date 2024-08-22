using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class UpdateSupportEmployeeDto {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!; 
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
