using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CreatePostDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid EventId { get; set; }
    }
}
