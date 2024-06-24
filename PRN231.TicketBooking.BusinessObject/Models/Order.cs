using PRN231.TicketBooking.BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Total { get; set; }
        public string? Content { get; set; }
        public string AccountId { get; set; } = null!;
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
    }
}