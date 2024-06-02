using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class EventService : GenericBackendService, IEventService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISeatRankRepository _seatRankRepository;

        public EventService(IServiceProvider serviceProvider, IMapper mapper, IUnitOfWork unitOfWork, ISeatRankRepository seatRankRepository) : base(serviceProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _seatRankRepository = seatRankRepository;
        }

        public async Task<AppActionResult> GetAllEvent(int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var data = await eventRepository.GetEvents(pageNumber, pageSize);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get list event successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetEventById(Guid id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventEntity = await eventRepository.GetEventById(id);
                if (eventEntity == null)
                {
                    result = new AppActionResult()
                    {
                        Result = eventEntity,
                        IsSuccess = false
                    };
                    return BuildAppActionResultError(result, "Event not found!");
                }
                var data = _mapper.Map<GetEventByIdResponse>(eventEntity);
                data.StaticFiles = await eventRepository.GetStaticFilesByEventId(eventEntity.Id);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get event successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> AddEvent(CreateEventRequest dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventEntity = _mapper.Map<Event>(dto);
                eventEntity.Id = Guid.NewGuid();
                eventEntity.CreateBy = dto.UserId;
                eventEntity.CreateDate = DateTime.Now;
                var resultAddEvent = await eventRepository.AddEvent(eventEntity);
                if (resultAddEvent == null || !resultAddEvent.IsSuccess)
                {
                    return resultAddEvent;
                }
                if (dto.createSeatRankDtoRequests != null && dto.createSeatRankDtoRequests.Count > 0)
                {
                    foreach (var item in dto.createSeatRankDtoRequests)
                    {
                        var seatRank = _mapper.Map<SeatRank>(item);
                        var data = await _seatRankRepository.AddSeatRankFromEvent(seatRank);
                        if (!data.IsSuccess)
                        {
                            return BuildAppActionResultError(data, "Cannot add seat rank!");
                        }
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                result.Result = _mapper.Map<CreateEventResponse>(dto);
                return BuildAppActionResultSuccess(result, "Add event and seat rank successfully!");
            }
            catch (Exception ex)
            {
                return BuildAppActionResultError(result, ex.Message, true);
            }
        }

        public async Task<AppActionResult> UpdateEvent(Guid id, UpdateEventRequest request)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var item = await eventRepository.GetEventById(id);
                if (item == null)
                {
                    BuildAppActionResultError(result, $"Event not found with id: {id}", true);
                }
                item.Title = request.Title;
                item.Description = request.Description;
                item.EventDate = request.EventDate;
                item.StartTime = request.StartTime;
                item.EndTime = request.EndTime;
                item.UpdateDate = DateTime.Now;
                item.UpdateBy = request.UserId;
                var resultUpdateEvent = await eventRepository.UpdateEvent(item);
                if (resultUpdateEvent == null || !resultUpdateEvent.IsSuccess)
                {
                    return BuildAppActionResultError(result, $"Cannot update event with id: {id}", true);
                }
                await _unitOfWork.SaveChangeAsync();
                result.Result = resultUpdateEvent;
                return BuildAppActionResultSuccess(result, "Update event successfully!");
            }
            catch (Exception ex)
            {
                return BuildAppActionResultError(result, ex.Message, true);
            }
        }
    }
}