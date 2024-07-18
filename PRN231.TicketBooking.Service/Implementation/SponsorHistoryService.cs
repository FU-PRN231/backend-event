using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SponsorHistoryService : GenericBackendService, ISponsorEventHistoryService
    {
        private readonly ISponsorMoneyHistoryRepository _repository;    
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SponsorHistoryService(
            IServiceProvider serviceProvider,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ISponsorMoneyHistoryRepository repository   
            ) : base(serviceProvider)
        {
            _repository = repository;   
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task<AppActionResult> AddSponsorHistory(AddSponsorMoneyHistoryRequestDto dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                if(dto.TransferredDate == null) {
                    var utility = Resolve<Utility>(); 
                    dto.TransferredDate = utility.GetCurrentDateInTimeZone();
                }

                var sponsorMoneyHistory = _mapper.Map<SponsorMoneyHistory>(dto);
                sponsorMoneyHistory.Id = Guid.NewGuid();
                await _repository.Insert(sponsorMoneyHistory);
                await _unitOfWork.SaveChangeAsync();

            } catch(Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSponsorHistoryById(Guid id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                result.Result = await _repository.GetAllDataByExpression(p => p.Id == id, 0, 0, null, false, p => p.EventSponsor!.Sponsor!, p => p.EventSponsor!.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSponsorHistoryBySponsorId(Guid sponsorId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var sponsorRepository = Resolve<ISponsorRepository>();
                var sponsorDb = await sponsorRepository.GetByExpression(p => p!.Id == sponsorId);
                if (sponsorDb == null)
                {
                    result = BuildAppActionResultError(result, $"Nhà đầu tư với id {sponsorId} không tồn tại");
                }
                result.Result = await _repository.GetAllSponsorMoneyHistory(p => p.EventSponsor!.SponsorId == sponsorId, pageNumber, pageSize, null, false, p => p.EventSponsor!.Sponsor!, p => p.EventSponsor!.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSponsorHistoryByType(SponsorType sponsorType, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                result.Result = await _repository.GetAllSponsorMoneyHistory(p => p.EventSponsor!.SponsorType == sponsorType, 0, 0, null, false, p => p.EventSponsor!.Sponsor!, p => p.EventSponsor!.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSponsorHistoryOfEvent(Guid eventId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = eventRepository.GetEventById(eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với id {eventId} không tồn tại");
                }
                result.Result = await _repository.GetAllSponsorMoneyHistory(p => p.EventSponsor!.EventId == eventId, pageNumber, pageSize, null, false, p => p.EventSponsor!.Sponsor!, p => p.EventSponsor!.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
