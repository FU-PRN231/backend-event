using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class StaticFile
    {
        [Key]
        public Guid Id { get; set; }

        public string Img { get; set; } = null!;
        public Guid? EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }

        public Guid? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }
    }
}