using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Enum;
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
        public List<CreateSeatRankEventRequest> CreateSeatRankDtoRequests { get; set; } = new List<CreateSeatRankEventRequest>();
        public List<CreateEventSponsorEvent> CreateEventSponsorEvents { get; set; } = new List<CreateEventSponsorEvent>();
        public List<CreateSpeakerEvent> createSpeakerEvents { get; set; } = new List<CreateSpeakerEvent>();
        public List<IFormFile> Img { get; set; } = null!;
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

    public class CreateEventSponsorEvent
    {
        public SponsorType SponsorType { get; set; }
        public string SponsorDescription { get; set; } = null!;
        public double? MoneySponsorAmount { get; set; }
        public Guid SponsorId { get; set; }
    }

    public class CreateSpeakerEvent
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Img { get; set; } = null!;
    }
}
