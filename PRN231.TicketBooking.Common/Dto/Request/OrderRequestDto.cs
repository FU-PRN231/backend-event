using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class OrderRequestDto
    {
        public List<SeatRankDto> SeatRank { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string? Content { get; set; }
    }
}
