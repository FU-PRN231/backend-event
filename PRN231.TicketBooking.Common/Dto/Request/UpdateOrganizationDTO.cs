using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class UpdateOrganizationDTO
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime FoundedDate { get; set; }
        public string ContactEmail { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Img { get; set; } = null!;
        public IFormFile? File { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
