using PRN231.TicketBooking.BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        public double Price { get; set; }
        public string? Content { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}