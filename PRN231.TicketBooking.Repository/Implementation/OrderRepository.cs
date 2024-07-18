using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly IGenericDAO<Order> _oderDAO;

        public OrderRepository(IGenericDAO<Order> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _oderDAO = dao;
        }

        public async Task<string> GetAccountId(Guid orderId)
        {
             var item = await _oderDAO.GetByExpression(filter: x => x.Id == orderId);
            if(item == null)
            {
                return null;
            }
            return item.AccountId;
        }


        public async Task<PagedResult<Order>> GetAllOrder(Expression<Func<Order, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Order, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Order, object>>[]? includes)
        {
            PagedResult<Order> result = null;
            try
            {
                result = new PagedResult<Order>();
                result = await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
