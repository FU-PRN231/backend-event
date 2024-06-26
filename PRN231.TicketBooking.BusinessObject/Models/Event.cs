using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Event : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
		public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location? Location { get; set; }

        public Guid OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }
        public EventCensorStatus? Status { get; set; }
    }
}