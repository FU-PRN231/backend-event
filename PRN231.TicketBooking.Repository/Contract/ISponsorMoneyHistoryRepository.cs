using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISponsorMoneyHistoryRepository : IRepository<SponsorMoneyHistory>
    {
        public Task<List<SponsorMoneyHistory>> GetAllSponsorMoneyHistoryByEventId(Guid eventId);
    }
}
