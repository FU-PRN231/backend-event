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
    public class SeatRankRepository : GenericRepository<SeatRank>, ISeatRankRepository
    {
        private readonly IGenericDAO<SeatRank> _seatRankDAO;
        public SeatRankRepository(IGenericDAO<SeatRank> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _seatRankDAO = dao;
        }
    }
}
