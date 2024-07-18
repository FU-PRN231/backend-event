using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IEventSponsorRepository : IRepository<EventSponsor>
    {
        public Task<EventSponsor> AddEventSponsorFromEvent(EventSponsor eventSponsor);
        public Task<List<Event>> GetEventsBySponsorId(Guid sponsorId, int pageNumber, int pageSize);
    }
}
