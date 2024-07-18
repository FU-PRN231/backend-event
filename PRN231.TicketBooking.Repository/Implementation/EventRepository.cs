using Humanizer;
using Microsoft.Extensions.Logging;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
	public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly IGenericDAO<Event> _eventDAO;
        private readonly IUnitOfWork _unitOfWork;
        IGenericDAO<StaticFile> _staticFileDAO;

        public EventRepository(IGenericDAO<Event> dao, IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IGenericDAO<StaticFile> staticFileDAO) : base(dao, serviceProvider)
        {
            _eventDAO = dao;
            _unitOfWork = unitOfWork;
            _staticFileDAO = staticFileDAO;
        }

        public async Task<AppActionResult> AddEvent(Event evnt)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var data = await _eventDAO.Insert(evnt);
                result.Result = data;
                result.Messages.Add("Insert event successfully!");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages.Add(ex.Message);
            }
            return result;
        }

		public async Task<Event> GetEventById(Guid id)
        {
            Event result = null;
            try
            {
                result = new Event();
                result = await _eventDAO.GetByExpression(
                                        filter: x => x.Id == id,
                                        includeProperties: new Expression<Func<Event, object>>[] {
                                        e => e.Location,
                                        e => e.Organization,
                                    });
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

		public Task<AppActionResult> GetEventReport(Guid? EventId, int timePeriod)
		{
			throw new NotImplementedException();
		}

		public async Task<PagedResult<Event>> GetEvents(int pageNumber, int pageSize)
        {
            PagedResult<Event> result = null;
            try
            {
                result = new PagedResult<Event>();
                result = await _eventDAO.GetAllDataByExpression(
                    filter: null,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    includes: new Expression<Func<Event, object>>[] {
                        e => e.Location,
                        e => e.Organization,
                    }
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<List<StaticFile>> GetStaticFilesByEventId(Guid eventId)
        {
            try
            {
                var eventEntity = _eventDAO.GetById(eventId);
                if (eventEntity == null)
                {
                    throw new Exception($"Event not found with id: {eventId}");
                }
                var staticFilesPaging = await _staticFileDAO.GetAllDataByExpression(
                                        filter: x => x.EventId == eventId,
                                        pageNumber: 0,
                                        pageSize: 0,
                                        orderBy: null,
                                        isAscending: true,
                                        includes: null);
                return staticFilesPaging.Items ??= new List<StaticFile>();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AppActionResult> UpdateEvent(Event eventEntity)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var data = await _eventDAO.Update(eventEntity);
                result.Result = data;
                result.Messages.Add("Update event successfully!");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages.Add(ex.Message);
            }
            return result;
        }

		public async Task<int[]> CountingEventsWithStatus(Guid? organizationId, DateTime today)
		{
			int[] data = new int[3];
			try
			{
                var eventDb = await this.GetAllDataByExpression(null, 0, 0, null, false, null);
                data[0] = eventDb.Items.Where(e => (organizationId == null || (organizationId != null && e.OrganizationId == organizationId)) && e.StartEventDate > today).Count();   
                data[1] = eventDb.Items.Where(e => (organizationId == null || (organizationId != null && e.OrganizationId == organizationId)) && e.StartEventDate <= today && e.EndEventDate >= today).Count();
				data[2] = eventDb.Items.Where(e => (organizationId == null || (organizationId != null && e.OrganizationId == organizationId)) && e.EndEventDate < today).Count();
			}
			catch (Exception ex)
			{
                data = new int[3];
			}
			return data;
		}
        //0 past, 1 current, 2 future
        public async Task<PagedResult<Event>> GetEventsWithStatus(Guid? organizationId, DateTime today, int happened, int pageNumber, int pageSize)
        {
            PagedResult<Event> result = null;
            try
            {
                result = new PagedResult<Event>();
                result = await _eventDAO.GetAllDataByExpression(
                    x => (organizationId == null || (organizationId != null && x.OrganizationId == organizationId)
                    && (
                    happened == 0 && today > x.EndEventDate 
                    || happened == 1 && today >=  x.StartEventDate && today <= x.EndEventDate
                    || happened == 2 && today < x.StartEventDate
                    )),
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    includes: new Expression<Func<Event, object>>[] {
                        e => e.Location,
                        e => e.Organization,
                    }
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<PagedResult<Event>> GetAvailableEvents(DateTime today, int pageNumber, int pageSize)
        {
            PagedResult<Event> result = null;
            try
            {
                result = new PagedResult<Event>();
                result = await _eventDAO.GetAllDataByExpression(
                    filter: x => today.Date <= x.EndEventDate.Date,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    includes: new Expression<Func<Event, object>>[] {
                        e => e.Location,
                        e => e.Organization,
                    }
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<PagedResult<Event>> GetEventsByOrganizationId(Guid organizationId, int pageNumber, int pageSize)
        {
            PagedResult<Event> result = null;
            try
            {
                result = new PagedResult<Event>();
                result = await _eventDAO.GetAllDataByExpression(
                    filter: e => e.OrganizationId == organizationId,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    includes: new Expression<Func<Event, object>>[] {
             e => e.Location,
             e => e.Organization,
                    }
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}