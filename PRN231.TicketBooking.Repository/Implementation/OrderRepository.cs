using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
