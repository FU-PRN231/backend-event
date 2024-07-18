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
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository
    {
        private readonly IGenericDAO<OrderDetail> _genericDAO;
        public OrderDetailsRepository(IGenericDAO<OrderDetail> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _genericDAO = dao;  
        }

        public async Task<PagedResult<OrderDetail>> GetAllOrderDetail(Expression<Func<OrderDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<OrderDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<OrderDetail, object>>[]? includes)
        {
            PagedResult<OrderDetail> result = null;
            try
            {
                result = new PagedResult<OrderDetail>();
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
