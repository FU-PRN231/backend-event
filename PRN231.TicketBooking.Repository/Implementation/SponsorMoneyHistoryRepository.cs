using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class SponsorMoneyHistoryRepository : GenericRepository<SponsorMoneyHistory>, ISponsorMoneyHistoryRepository
    {
        private readonly IGenericDAO<SponsorMoneyHistory> _dao;
        public SponsorMoneyHistoryRepository(IGenericDAO<SponsorMoneyHistory> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao;
        }

        public async Task<List<SponsorMoneyHistory>> GetAllSponsorMoneyHistoryByEventId(Guid eventId)
        {
            var moneyHistory = await GetAllDataByExpression(m => m.EventSponsor.EventId == eventId, 0, 0, null, false, m => m.EventSponsor);
            if (moneyHistory.Items.Count == 0)
            {
                return new List<SponsorMoneyHistory>();
            }
            return moneyHistory.Items;
        }
        public async Task<PagedResult<SponsorMoneyHistory>> GetAllSponsorMoneyHistory(Expression<Func<SponsorMoneyHistory, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SponsorMoneyHistory, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SponsorMoneyHistory, object>>[]? includes)
        {
            PagedResult<SponsorMoneyHistory> result = null;
            try
            {
                result = new PagedResult<SponsorMoneyHistory>();
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
