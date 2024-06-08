using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class FilterSeatRankDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
    }
}
