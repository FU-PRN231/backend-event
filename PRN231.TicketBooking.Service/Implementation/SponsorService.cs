using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SponsorService : GenericBackendService, ISponsorService
    {

        private BackEndLogger _logger;
        private IUnitOfWork _unitOfWork;
        private ISponsorRepository _repository;
        private IMapper _mapper;

        public SponsorService(BackEndLogger logger, IMapper mapper, IUnitOfWork unitOfWork, ISponsorRepository repository, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<AppActionResult> AddSponsorToEvent(CreateSponsorDto dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var accountService = Resolve<IAccountService>();
                var data = await accountService.AddSponsor(dto);
                List<Sponsor> sponsors = await _repository.CreateSponsor((Dictionary<string, SponsorDto>)data.Result);
                await _unitOfWork.SaveChangeAsync();
                var eventSponsorRepository = Resolve<IRepository<EventSponsor>>();
                foreach(var sponsor in sponsors)
                {
                    await eventSponsorRepository.Insert(
                        new EventSponsor
                        {
                            Id = Guid.NewGuid(),
                            SponsorId = sponsor.Id,
                            EventId = dto.EventId
                        });
                };
                await _unitOfWork.SaveChangeAsync();
            } catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public Task<AppActionResult> GetAttendeeInformation(string qr)
        {
            throw new NotImplementedException();
        }
    }
}
