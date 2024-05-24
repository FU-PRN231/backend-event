using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Img { get; set; } = null!;
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }
    }
}