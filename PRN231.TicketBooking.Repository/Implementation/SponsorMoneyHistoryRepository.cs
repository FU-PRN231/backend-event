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
            if(moneyHistory.Items.Count == 0)
            {
                return new List<SponsorMoneyHistory>();
            }
            return moneyHistory.Items;
		}
	}
}
