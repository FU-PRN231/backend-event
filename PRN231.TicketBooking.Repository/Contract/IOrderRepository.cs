using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<string> GetAccountId(Guid OrderId);

    }
}
