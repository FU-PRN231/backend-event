using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CheckInEventRequest
    {
        [Required]
        public String AccountId { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}
