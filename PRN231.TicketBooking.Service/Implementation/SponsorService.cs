using AutoMapper;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office.Word;
using Humanizer;
using Microsoft.Extensions.Logging;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System.Transactions;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SponsorService : GenericBackendService, ISponsorService
    {
        private BackEndLogger _logger;
        private IUnitOfWork _unitOfWork;
        private ISponsorRepository _repository;
        private IFirebaseService _firebaseService;
        private IMapper _mapper;

        public SponsorService(BackEndLogger logger, IMapper mapper, IUnitOfWork unitOfWork, ISponsorRepository repository, IFirebaseService firebaseService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _firebaseService = firebaseService;
        }

        public async Task<AppActionResult> AddSponsorMoneyToEvent(AddSponsorMoneyDto dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var sponsorHistoryRepository = Resolve<ISponsorMoneyHistoryRepository>();
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository!.GetById(dto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {dto.EventId}");
                    return result;
                }
                var sponsorRepository = Resolve<ISponsorRepository>();
                foreach (var item in dto.SponsorItems)
                {
                    var sponsorDb = await sponsorRepository.GetByExpression(p => p.Id == item.SponsorId);
                    if (sponsorDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Không tồn tại nhà tài trợ với Id {item.SponsorId}");
                        return result;
                    }
                    var eventSponsor = new EventSponsor
                    {
                        Id = Guid.NewGuid(),
                        EventId = dto.EventId,
                        MoneySponsorAmount = item.MoneySponsorAmount,
                        SponsorDescription = item.SponsorDescription,
                        SponsorType = item.SponsorType,
                        SponsorId = item.SponsorId,
                    };
                    await eventSponsorRepository.Insert(eventSponsor);

                    var sponsorHistory = new SponsorMoneyHistory
                    {
                        Id = Guid.NewGuid(),
                        Amount = eventSponsor.MoneySponsorAmount ?? 0,
                        Date = DateTime.Now,
                        EventSponsorId = eventSponsor.Id,
                    };
                    await sponsorHistoryRepository.Insert(sponsorHistory);
                }
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

       

        public async Task<AppActionResult> GetAllSponsor(int pageNumber, int pagesize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                result.Result = await _repository.GetAllDataByExpression(null, pageNumber, pagesize, null, false, null);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllSponsorItemOfAnEvent(Guid eventId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {eventId}");
                    return result;
                }
                result.Result = await eventSponsorRepository.GetAllDataByExpression(p => p.EventId == eventId, pageNumber, pageSize, null, false, p => p.Sponsor!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAttendeeInformation(string qr)
        {
            AppActionResult result = new AppActionResult();
            try
            {

            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSponsorHistoryByEventId(Guid eventId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var sponsorHistoryRepository = Resolve<ISponsorMoneyHistoryRepository>();
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository!.GetById(eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {eventId}");
                    return result;
                }
                var sponsorHistoryDb = await sponsorHistoryRepository.GetAllDataByExpression(p => p.EventSponsor!.EventId == eventId, pageNumber, pageNumber, null, false, p => p.EventSponsor!.Event!, p => p.EventSponsor!.Sponsor!);
                result.Result = sponsorHistoryDb;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
        
    }
}