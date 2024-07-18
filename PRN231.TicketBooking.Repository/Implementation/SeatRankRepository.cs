using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
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

        public async Task<AppActionResult> AddSeatRankFromEvent(SeatRank seatRank)
        {
            var data = await _seatRankDAO.Insert(seatRank);
            if (data == null)
            {
                return new AppActionResult()
                {
                    IsSuccess = false,
                };
            }
            return new AppActionResult() { Result = data};
        }

        public async Task<SeatRank> GetSeatRankById(Guid id)
        {
            SeatRank result = null;
            try
            {
                result = new SeatRank();
                result = await _seatRankDAO.GetById(id);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<PagedResult<SeatRank>> GetSeatRanks(int pageNumber, int pageSize)
        {
            PagedResult<SeatRank> result = null;
            try
            {
                result = new PagedResult<SeatRank>();
                result = await _seatRankDAO.GetAllDataByExpression(null, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<SeatRank> UpdateSeatRank(SeatRank seatRank)
        {
            try
            {
               return await _seatRankDAO.Update(seatRank);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedResult<SeatRank>> GetAllSeatRank(Expression<Func<SeatRank, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SeatRank, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SeatRank, object>>[]? includes)
        {
            PagedResult<SeatRank> result = null;
            try
            {
                result = new PagedResult<SeatRank>();
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
