using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class AttendeeRepository : GenericRepository<Attendee>, IAttendeeRepostory
    {
        IGenericDAO<Attendee> _attendeeDao;
        public AttendeeRepository(IGenericDAO<Attendee> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _attendeeDao = dao;
        }

        public async Task<Attendee> CheckInAttendee(Attendee attendee)
        {
            attendee.CheckedIn = true;
            var item = await _attendeeDao.Update(attendee);
            return item;
        }

        public async Task<Attendee> GetAttendeeByAccountIdAndEventId(string accountId, Guid eventId)
        {
            return await _attendeeDao.GetByExpression(a => a.OrderDetail.Order.AccountId == accountId 
                                                        && a.OrderDetail.SeatRank.EventId == eventId, a => a.OrderDetail)!;
        }

        public async Task<PagedResult<Attendee>> GetAttendeeByEvent(Guid eventId)
        {
            return await _attendeeDao.GetAllDataByExpression(a => a.OrderDetail.SeatRank.EventId == eventId, 0, 0, null, false, a => a.OrderDetail.Order.Account, a => a.OrderDetail.SeatRank.Event);
        }
    }
}
