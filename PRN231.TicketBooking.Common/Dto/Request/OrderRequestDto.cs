using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class OrderRequestDto
    {
        public Guid SeatRankId { get; set; }
        public double Total { get; set; }
        public string AccountId { get; set; } = null!;
    }
}
