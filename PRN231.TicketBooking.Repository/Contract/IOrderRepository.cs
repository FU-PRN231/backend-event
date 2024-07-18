using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<string> GetAccountId(Guid OrderId);
        public Task<PagedResult<Order>> GetAllOrder(Expression<Func<Order, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Order, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Order, object>>[]? includes);
    }
}
