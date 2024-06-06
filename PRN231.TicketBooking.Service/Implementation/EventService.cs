using AutoMapper;
using Org.BouncyCastle.Asn1.Ocsp;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class EventService : GenericBackendService, IEventService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISeatRankRepository _seatRankRepository;
        private readonly IFirebaseService _firebaseService;

        public EventService(IServiceProvider serviceProvider, IMapper mapper, IUnitOfWork unitOfWork,
            ISeatRankRepository seatRankRepository, IFirebaseService firebaseService) : base(serviceProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _seatRankRepository = seatRankRepository;
            _firebaseService = firebaseService;
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
                var seatRankRepository = Resolve<ISeatRankRepository>();
                var speakerRepository = Resolve<ISpeakerRepository>();
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var staticFileRepository = Resolve<IStaticFileRepository>();
                var surveyRepository = Resolve<ISurveyRepository>();
                var postRepository = Resolve<IPostRepository>();
                var eventResponse = new EvenetResponse();
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetAllDataByExpression(p => p!.Id == id, 0, 0, null, false, p => p.Organization!, p => p.Location!);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện này không tồn tại với {id}");
                }
                if (eventDb!.Items!.Count > 0 && eventDb.Items != null)
                {
                    var eventItem = eventDb.Items.First();
                    var seatRankDb = await seatRankRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, null);
                    var speakerDb = await speakerRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, null);
                    var eventSponsorDb = await eventSponsorRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, p => p.Sponsor!);
                    var staticFileDb = await staticFileRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, null);
                    var surveyDb = await surveyRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, null);
                    var postDb = await postRepository.GetAllDataByExpression(p => p.EventId == eventItem.Id, 0, 0, null, false, null);
                    eventResponse.StaticFiles = staticFileDb.Items!;
                    eventResponse.Speakers = speakerDb.Items!;
                    eventResponse.SeatRanks = seatRankDb.Items!;
                    eventResponse.Surveys = surveyDb.Items!;
                    eventResponse.Event = eventItem;
                    eventResponse.EventSponsors = eventSponsorDb.Items!;
                    eventResponse.Posts = postDb.Items!;
                    result.Result = eventResponse;
                }
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
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var sponsorRepository = Resolve<ISponsorRepository>();
                var staticFileRepository = Resolve<IStaticFileRepository>();
                var speakerRepository = Resolve<ISpeakerRepository>();

                var eventEntity = _mapper.Map<Event>(dto);
                eventEntity.Id = Guid.NewGuid();
                eventEntity.CreateBy = dto.UserId;
                eventEntity.CreateDate = DateTime.Now;
                var resultAddEvent = await eventRepository.AddEvent(eventEntity);
                if (resultAddEvent == null || !resultAddEvent.IsSuccess)
                {
                    return resultAddEvent;
                }
                //Create SeatRank
                if (dto.CreateSeatRankDtoRequests != null && dto.CreateSeatRankDtoRequests.Count > 0)
                {
                    foreach (var item in dto.CreateSeatRankDtoRequests)
                    {
                        var seatRank = _mapper.Map<SeatRank>(item);
                        seatRank.Id = Guid.NewGuid();
                        seatRank.EventId = eventEntity.Id;
                        var data = await _seatRankRepository.AddSeatRankFromEvent(seatRank);
                        if (!data.IsSuccess)
                        {
                            return BuildAppActionResultError(data, "Cannot add seat rank!");
                        }
                    }
                }
                //Create Event Sponsor
                if (dto.CreateEventSponsorEvents != null && dto.CreateEventSponsorEvents.Count > 0)
                {
                    foreach (var item in dto.CreateEventSponsorEvents)
                    {
                        var sponsor = sponsorRepository.GetById(item.SponsorId);
                        if (sponsor == null)
                        {
                            return BuildAppActionResultError(
                                       new AppActionResult(), $"Sponsor not found by id: {item.SponsorId}!"
                                   );
                        }
                        var eventSponsor = _mapper.Map<EventSponsor>(item);
                        eventSponsor.Id = Guid.NewGuid();
                        eventSponsor.SponsorId = item.SponsorId;
                        eventSponsor.EventId = eventEntity.Id;
                        var resultEventSponsor = await eventSponsorRepository.AddEventSponsorFromEvent(eventSponsor);
                        if (resultEventSponsor == null)
                        {
                            return BuildAppActionResultError(new AppActionResult(), "Cannot add event sponsor!");
                        }
                    }
                }
                //Create Speaker
                if (dto.createSpeakerEvents != null && dto.createSpeakerEvents.Count > 0)
                {
                    foreach (var item in dto.createSpeakerEvents)
                    {
                        var speaker = _mapper.Map<Speaker>(item);
                        speaker.Id = Guid.NewGuid();
                        speaker.EventId = eventEntity.Id;
                        var url = await _firebaseService
                                        .UploadFileToFirebase(item.Img, $"{SD.FirebasePathName.SPEAKER}{speaker.Id}");
                        if (!url.IsSuccess)
                        {
                            return BuildAppActionResultError(url, "Cannot upload file!");
                        }
                        speaker.Img = (string)url.Result;
                        var saveSuccess = await speakerRepository.AddSpeakerFromEvent(speaker);
                        if (saveSuccess == null)
                        {
                            return BuildAppActionResultError(new AppActionResult(), "Cannot add static File!");
                        }
                    }
                }
                //Create StaticFile
                if (dto.CreateStaticFilesEvent != null && dto.CreateStaticFilesEvent.Count > 0)
                {
                    foreach (var item in dto.CreateStaticFilesEvent)
                    {
                        var staticFile = new StaticFile();
                        staticFile.Id = Guid.NewGuid();
                        staticFile.EventId = eventEntity.Id;
                        var url = await _firebaseService
                                        .UploadFileToFirebase(item.Img, $"{SD.FirebasePathName.EVENT}{staticFile.Id}");
                        if (!url.IsSuccess)
                        {
                            return BuildAppActionResultError(url, "Cannot upload file!");
                        }
                        staticFile.Img = (string)url.Result;
                        var saveSuccess = await staticFileRepository.AddStaticFileFromEvent(staticFile);
                        if (!saveSuccess)
                        {
                            return BuildAppActionResultError(new AppActionResult(), "Cannot add static File!");
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
                var seatRankRepository = Resolve<ISeatRankRepository>();
                var staticFileRepository = Resolve<IStaticFileRepository>();
                var speakerRepository = Resolve<ISpeakerRepository>();
                var eventEntity = await eventRepository.GetEventById(id);
                if (eventEntity == null)
                {
                    BuildAppActionResultError(result, $"Event not found with id: {id}", true);
                }
                var updateEvent = _mapper.Map<Event>(request);
                eventEntity.UpdateDate = DateTime.Now;
                eventEntity.UpdateBy = request.UserId;
                var resultUpdateEvent = await eventRepository.UpdateEvent(eventEntity);
                if (resultUpdateEvent == null || !resultUpdateEvent.IsSuccess)
                {
                    return BuildAppActionResultError(result, $"Cannot update event with id: {id}", true);
                }
                //Update SeatRank
                foreach (var seatRank in request.SeatRanks)
                {
                    var seatRankEntity = await seatRankRepository.GetSeatRankById(seatRank.Id);
                    if (seatRankEntity == null)
                    {
                        return BuildAppActionResultError(result, $"Not found seat rank with id: {seatRank.Id}", true);
                    }
                    var updateSeatRankentity = _mapper.Map<SeatRank>(seatRank);
                    updateSeatRankentity.EventId = eventEntity.Id;
                    var resultUpdate = await seatRankRepository.UpdateSeatRank(seatRankEntity);
                    if (resultUpdate == null)
                    {
                        return BuildAppActionResultError(result, $"Cannot update seat rank with id: {seatRank.Id}", true);
                    }
                }
                //Update Static file
                foreach (var staticFile in request.StaticFiles)
                    if (staticFile.ImgFormFile != null)
                    {
                        var staticFileEntity = await staticFileRepository.GetStaticFileById(staticFile.Id);
                        if (staticFileEntity == null)
                        {
                            return BuildAppActionResultError(result, $"Not found static file with id: {staticFile.Id}", true);
                        }
                        var url = await _firebaseService.UploadFileToFirebase(staticFile.ImgFormFile, $"{SD.FirebasePathName.EVENT}{staticFile.Id}");
                        if (!url.IsSuccess)
                        {
                            return BuildAppActionResultError(url, $"Cannot upload file with static file id {staticFile.Id}!");
                        }
                        staticFileEntity.Img = (string)url.Result;
                        var resultUpdate = await staticFileRepository.UploadStaticFile(staticFileEntity);
                        if (resultUpdate == null)
                        {
                            return BuildAppActionResultError(result, $"Cannot update static file with id: {staticFile.Id}", true);
                        }
                    }
                //Update speaker event
                foreach (var speaker in request.Speakers)
                {
                    var speakerEntity = await speakerRepository.GetById(speaker.Id);
                    if (speakerEntity == null)
                    {
                        return BuildAppActionResultError(result, $"Not found speaker with id: {speaker.Id}", true);
                    }
                    var url = await _firebaseService.UploadFileToFirebase(speaker.ImgFormFile, $"{SD.FirebasePathName.SPEAKER}{speaker.Id}");
                    if (!url.IsSuccess)
                    {
                        return BuildAppActionResultError(url, $"Cannot upload file with speaker id {speaker.Id}!");
                    }
                    var updateSpeakertity = _mapper.Map<Speaker>(speaker);
                    updateSpeakertity.EventId = eventEntity.Id;
                    updateSpeakertity.Img = (string)url.Result;
                    var resultUpdate = await speakerRepository.Update(updateSpeakertity);
                    if (resultUpdate == null)
                    {
                        return BuildAppActionResultError(result, $"Cannot update speaker with id: {speaker.Id}", true);
                    }
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