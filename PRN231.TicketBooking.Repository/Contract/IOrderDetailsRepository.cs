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
    public interface IOrderDetailsRepository : IRepository<OrderDetail>
    {
        public Task<PagedResult<OrderDetail>> GetAllOrderDetail(Expression<Func<OrderDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<OrderDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<OrderDetail, object>>[]? includes);
    }
}
