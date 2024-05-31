using PRN231.TicketBooking.BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class EventSponsor
    {
        [Key]
        public Guid Id { get; set; }
        public SponsorType SponsorType { get; set; }
        public string SponsorDescription { get; set; } = null!;
        public double? MoneySponsorAmount { get; set; }
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }

        public Guid SponsorId { get; set; }

        [ForeignKey(nameof(SponsorId))]
        public Sponsor? Sponsor { get; set; }
    }
}