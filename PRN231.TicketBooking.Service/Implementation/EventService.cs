using AutoMapper;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class EventService : GenericBackendService, IEventService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IServiceProvider serviceProvider, IMapper mapper, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
    }
}