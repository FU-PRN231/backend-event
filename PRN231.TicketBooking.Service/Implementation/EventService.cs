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
                return BuildAppActionResultError(result, "Get list event successfully!");
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
                var data = await eventRepository.GetEventById(id);
                if (data == null)
                {
                    result = new AppActionResult()
                    {
                        Result = data,
                        IsSuccess = false
                    };
                    return BuildAppActionResultError(result, "Event not found!");
                }
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultError(result, "Get event successfully!");
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
                var resulAddEvent = await eventRepository.AddEvent(eventEntity);
                if (resulAddEvent == null || !resulAddEvent.IsSuccess)
                {
                    return resulAddEvent;
                }
                await _unitOfWork.SaveChangeAsync();
                if (dto.createSeatRankDtoRequests!=null && dto.createSeatRankDtoRequests.Count > 0)
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
                result.Result = _mapper.Map<CreateEventResponse>(eventEntity);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message, true);
            }
            return result;
        }
    }
}