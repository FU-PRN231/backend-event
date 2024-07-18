using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN231.TicketBooking.Common.Dto.Response;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class EventSponsorRepository : GenericRepository<EventSponsor>, IEventSponsorRepository
    {
        private readonly IGenericDAO<EventSponsor> _dao;
        public EventSponsorRepository(IGenericDAO<EventSponsor> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao;
        }

        public async Task<EventSponsor> AddEventSponsorFromEvent(EventSponsor eventSponsor)
        {
            var data = await _dao.Insert(eventSponsor);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public async Task<List<Event>> GetEventsBySponsorId(Guid sponsorId, int pageNumber, int pageSize)
        {
            List<Event> result = null;
            try
            {
                result = new List<Event>();
                var eventSponsorDB = await _dao.GetAllDataByExpression(
                    filter: e => e.SponsorId == sponsorId,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    orderBy: null,
                    false,
                    e => e.Event
                );

                if (eventSponsorDB.Items!.Count > 0)
                {
                    result = eventSponsorDB.Items.DistinctBy(e => e.EventId).Select(e=>e.Event).ToList()!;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
