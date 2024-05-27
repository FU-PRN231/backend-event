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
        private readonly IUnitOfWork _unitOfWork;
        public OrderRepository(IUnitOfWork unitOfWork,IGenericDAO<Order> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;   
        }
    }
}
