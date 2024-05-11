using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class EventSponsor
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }
        public Guid SponsorId { get; set; }
        [ForeignKey(nameof(SponsorId))]
        public Sponsor? Sponsor { get; set; }
    }
}
