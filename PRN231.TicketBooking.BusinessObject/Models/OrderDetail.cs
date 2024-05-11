using PRN231.TicketBooking.BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public double Total {  get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
        public Guid SeatId { get; set; }
        [ForeignKey(nameof(SeatId))]
        public Seat? Seat { get; set; }

    }
}
