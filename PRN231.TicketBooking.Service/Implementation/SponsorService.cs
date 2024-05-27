using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;

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

        public async Task<AppActionResult> AddSponsorToEvent(CreateSponsorDto dto)
        {
            //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            AppActionResult result = new AppActionResult();
            try
            {
                // Save Account => Save Sponsor =>
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository!.GetById(dto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {dto.EventId}");
                    return result;
                }
                var accountService = Resolve<IAccountService>();
                var staticFileRepository = Resolve<IRepository<StaticFile>>();
                var data = await accountService.AddSponsor(dto);
                List<Sponsor> sponsors = await _repository.CreateSponsor((Dictionary<string, SponsorDto>)data.Result!);
                string pathName;
                foreach (var sponsor in dto.SponsorDtos!)
                {
                    var sponsorDb = sponsors.Where(s => s.Name == sponsor.Name).FirstOrDefault();
                    pathName = SD.FirebasePathName.SPONSOR_PREFIX + sponsorDb!.Id;
                    var upload = await _firebaseService.UploadFileToFirebase(sponsor.Img, pathName);
                    sponsorDb!.Img = upload.Result!.ToString()!;
                    await _repository.Update(sponsorDb);
                }
                await _unitOfWork.SaveChangeAsync();
                var eventSponsorRepository = Resolve<IRepository<EventSponsor>>();
                foreach (var sponsor in sponsors)
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
                //scope.Complete();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
            //}
        }

        public async Task<AppActionResult> GetAllSponsor()
        {
            AppActionResult result = new AppActionResult();
            try
            {
                result.Result = await _repository.GetAllDataByExpression(null, 0, 0, null, false, null);
            }
            catch (Exception ex)
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