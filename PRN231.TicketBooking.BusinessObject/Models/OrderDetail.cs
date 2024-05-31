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
        public int Quantity { get; set; }
        public Guid SeatRankId { get; set; }

        [ForeignKey(nameof(SeatRankId))]
        public SeatRank? SeatRank { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
