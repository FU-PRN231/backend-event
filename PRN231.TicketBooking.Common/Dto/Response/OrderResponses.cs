using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class OrderResponses
    {
        public Order Order { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = null!;
    }

    public class OrderResponse
    {
        public Order Order { get; set; } = null!;
        public OrderDetail OrderDetail { get; set; } = null!;
    }
}
