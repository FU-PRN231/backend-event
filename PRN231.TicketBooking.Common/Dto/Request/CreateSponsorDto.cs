using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CreateSponsorDto
    {
        public Guid EventId { get; set; }
        public List<SponsorDto> SponsorDtos { get; set; } = new List<SponsorDto>();
    }

    public class SponsorDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Img { get; set; } = null!;
    }
}
