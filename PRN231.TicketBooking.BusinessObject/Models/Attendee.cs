using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Attendee
    {
        [Key]
        public Guid Id { get; set; }
        public string? QR {  get; set; }
        public bool CheckedIn { get; set; }
        public Guid OrderDetailId { get; set; }

        [ForeignKey(nameof(OrderDetailId))]
        public OrderDetail? OrderDetail { get; set; }
    }
}