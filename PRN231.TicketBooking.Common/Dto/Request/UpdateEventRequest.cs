using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class UpdateEventRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<UpdateSeatRankEventRequest> SeatRanks { get; set; }
        public List<UpdateStaticFileEventRequest> StaticFiles { get; set; }
        public List<UpdateSpeakerEventRequest> Speakers { get; set; }
    }

    public class UpdateSeatRankEventRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RemainingCapacity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class UpdateStaticFileEventRequest
    {
        public Guid Id { get; set; }
        public IFormFile ImgFormFile { get; set; } = null!;
    }

    public class UpdateSpeakerEventRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile ImgFormFile { get; set; } = null!;
    }
}
