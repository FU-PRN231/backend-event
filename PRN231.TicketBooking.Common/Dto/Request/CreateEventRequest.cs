using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CreateEventRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LocationId { get; set; }
        public Guid OrganizationId { get; set; }
        public List<CreateSeatRankEventRequest> createSeatRankDtoRequests { get; set; }
    }

    public class CreateSeatRankEventRequest
    {
        public string Name { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RemainingCapacity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
