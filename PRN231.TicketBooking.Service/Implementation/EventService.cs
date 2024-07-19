using AutoMapper;
using Microsoft.Extensions.Logging;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using ZXing;

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
        public async Task<AppActionResult> GetAvailableEvent(int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var utility = Resolve<Utility>();
                var data = await eventRepository.GetAvailableEvents(utility.GetCurrentDateInTimeZone(), pageNumber, pageSize);
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
                var eventResponse = new EventResponse();
                var eventRepository = Resolve<IEventRepository>();

                var eventDb = await eventRepository.GetAllDataByExpression(
                    p => p!.Id == id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Organization!,
                    p => p.Location!
                );
                if (eventDb == null || eventDb.Items == null || eventDb.Items.Count == 0)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện này không tồn tại với {id}");
                    return result;
                }

                var eventItem = eventDb.Items.First();

                var seatRankDb = await seatRankRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Event!
                );

                var speakerDb = await speakerRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Event!
                );

                var eventSponsorDb = await eventSponsorRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Sponsor!
                );

                var staticFileDb = await staticFileRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Event!
                );

                var surveyDb = await surveyRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Event!
                );

                var postDb = await postRepository.GetAllDataByExpression(
                    p => p.EventId == eventItem.Id,
                    0,
                    0,
                    null,
                    false,
                    p => p.Event!
                );

                eventResponse.StaticFiles = staticFileDb.Items!;
                eventResponse.Speakers = speakerDb.Items!;
                eventResponse.SeatRanks = seatRankDb.Items!;
                eventResponse.Surveys = surveyDb.Items!;
                eventResponse.Event = eventItem;
                eventResponse.EventSponsors = eventSponsorDb.Items!;
                eventResponse.Posts = postDb.Items!;
                result.Result = eventResponse;
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
                var utility = Resolve<Utility>();
                var eventRepository = Resolve<IEventRepository>();
                var eventSponsorRepository = Resolve<IEventSponsorRepository>();
                var sponsorRepository = Resolve<ISponsorRepository>();
                var staticFileRepository = Resolve<IStaticFileRepository>();
                var speakerRepository = Resolve<ISpeakerRepository>();
                var organizationRepository = Resolve<IOrganizationRepository>();
                var locationRepository = Resolve<ILocationRepository>();

                var existLocation = await locationRepository.GetById(dto.LocationId);
                if (existLocation == null)
                {
                    return BuildAppActionResultError(new AppActionResult(), $"Không tìm thấy địa điểm với id {dto.LocationId}!");
                }
                var existOrganization = await organizationRepository.GetById(dto.OrganizationId);
                if (existOrganization == null)
                {
                    return BuildAppActionResultError(new AppActionResult(), $"Không tìm thấy tổ chức với id {dto.OrganizationId}!");
                }
                var eventEntity = _mapper.Map<Event>(dto);
                eventEntity.Id = Guid.NewGuid();
                eventEntity.CreateBy = dto.UserId;
                eventEntity.CreateDate = utility.GetCurrentDateTimeInTimeZone();
                eventEntity.UpdateDate = utility.GetCurrentDateTimeInTimeZone();
                eventEntity.Status = EventCensorStatus.PENDING;

                //Create SeatRank
                if (dto.CreateSeatRankDtoRequests != null && dto.CreateSeatRankDtoRequests.Count > 0)
                {
                    foreach (var item in dto.CreateSeatRankDtoRequests)
                    {
                        if (item.StartTime>=dto.StartTime && item.EndTime<=dto.EndTime)
                        {
                            var seatRank = _mapper.Map<SeatRank>(item);
                            seatRank.Id = Guid.NewGuid();
                            seatRank.EventId = eventEntity.Id;
                            seatRank.RemainingCapacity = seatRank.Quantity; //define Reamining capacity of seatrank
                            var data = await _seatRankRepository.AddSeatRankFromEvent(seatRank);
                            if (!data.IsSuccess)
                            {
                                return BuildAppActionResultError(data, "Không thể tạo hạng vé");
                            }
                        }
                        else
                        {
                            return BuildAppActionResultError(new AppActionResult(), $"Thời gian bán vé hạng vé {item.Name} không nằm trong thời gian từ {dto.StartTime.Hour}:{dto.StartTime.Minute} đến {dto.EndTime.Hour}:{dto.EndTime.Minute}");
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
                                       new AppActionResult(), $"Không tìm thấy nhà tài trợ với id: {item.SponsorId}!"
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
                if (dto.CreateSpeakerEvents != null && dto.CreateSpeakerEvents.Count > 0)
                {
                    foreach (var item in dto.CreateSpeakerEvents)
                    {
                        var speaker = _mapper.Map<Speaker>(item);
                        speaker.Id = Guid.NewGuid();
                        speaker.EventId = eventEntity.Id;
                        await _firebaseService.DeleteFileFromFirebase($"{SD.FirebasePathName.SPEAKER}{speaker.Id}");
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
                if (dto.Img.Count > 0)
                {
                    await _firebaseService.DeleteFileFromFirebase($"{SD.FirebasePathName.EVENT}{eventEntity.Id}");

                    var mainUrl = await _firebaseService
                                    .UploadFileToFirebase(dto.Img[0], $"{SD.FirebasePathName.EVENT}{eventEntity.Id}");
                    if (!mainUrl.IsSuccess)
                    {
                        return BuildAppActionResultError(mainUrl, "Cannot upload file!");
                    }                  

                    foreach (var item in dto.Img.Skip(0).Take(dto.Img.Count))
                    {
                        var staticFile = new StaticFile();
                        staticFile.Id = Guid.NewGuid();
                        staticFile.EventId = eventEntity.Id;
                        await _firebaseService.DeleteFileFromFirebase($"{SD.FirebasePathName.EVENT}{staticFile.Id}");

                        var url = await _firebaseService
                                        .UploadFileToFirebase(item, $"{SD.FirebasePathName.EVENT}{staticFile.Id}");
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
                await eventRepository.Insert(eventEntity);
                await _unitOfWork.SaveChangeAsync();
                result.Result = _mapper.Map<CreateEventResponse>(dto);
                return BuildAppActionResultSuccess(result, "Tạo sự kiện thành công");
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
                var locationRepository = Resolve<ILocationRepository>();
                var organizationRepository = Resolve<IOrganizationRepository>();
                var eventEntity = await eventRepository.GetEventById(id);
                var utility = Resolve<Utility>();
                if (eventEntity == null)
                {
                    BuildAppActionResultSuccess(result, $"Event not found with id {id}");
                }
                var existLocation = await locationRepository.GetById(request.LocationId);
                if (existLocation == null)
                {
                    return BuildAppActionResultSuccess(result, $"Not found location with id: {request.LocationId}");
                }
                Organization existOrganization = await organizationRepository.GetById(request.OrganizationId);
                if (existLocation == null)
                {
                    return BuildAppActionResultSuccess(result, $"Not found organization with id: {request.OrganizationId}");
                }
                _mapper.Map(request, eventEntity);
                eventEntity.UpdateDate = utility.GetCurrentDateTimeInTimeZone();
                eventEntity.UpdateBy = request.UserId;
                var resultUpdateEvent = await eventRepository.UpdateEvent(eventEntity);
                if (resultUpdateEvent == null || !resultUpdateEvent.IsSuccess)
                {
                    return BuildAppActionResultSuccess(result, $"Cannot update event with id: {id}");
                }
                //Update SeatRank
                foreach (var seatRank in request.SeatRanks)
                {
                    var seatRankEntity = await seatRankRepository.GetSeatRankById(seatRank.Id);
                    if (seatRankEntity == null)
                    {
                        return BuildAppActionResultSuccess(result, $"Not found seat rank with id: {seatRank.Id}");
                    }
                    if (seatRank.StartTime >= request.StartTime && seatRank.EndTime <= request.EndTime)
                    {
                        _mapper.Map(seatRank, seatRankEntity);
                        seatRankEntity.EventId = eventEntity.Id;
                        var resultUpdate = await seatRankRepository.UpdateSeatRank(seatRankEntity);
                        if (resultUpdate == null)
                        {
                            return BuildAppActionResultSuccess(result, $"Cannot update seat rank with id: {seatRank.Id}");
                        }
                    }
                    else
                    {
                        return BuildAppActionResultError(new AppActionResult(), $"Thời gian bán vé hạng vé {seatRank.Name} không nằm trong thời gian từ {request.StartTime.Hour}:{request.StartTime.Minute} đến {request.EndTime.Hour}:{request.EndTime.Minute}");
                    }
                }
                //Update Static file
                foreach (var staticFile in request.StaticFiles)
                    if (staticFile.ImgFormFile != null)
                    {
                        var staticFileEntity = await staticFileRepository.GetStaticFileById(staticFile.Id);
                        if (staticFileEntity == null)
                        {
                            return BuildAppActionResultError(result, $"không tìm thấy static file với id: {staticFile.Id}");
                        }
                        await _firebaseService.DeleteFileFromFirebase($"{SD.FirebasePathName.EVENT}{staticFile.Id}");
                        var url = await _firebaseService.UploadFileToFirebase(staticFile.ImgFormFile, $"{SD.FirebasePathName.EVENT}{staticFile.Id}");
                        if (!url.IsSuccess)
                        {
                            return BuildAppActionResultError(new AppActionResult() { IsSuccess=false}, $"Cannot upload file with static file id {staticFile.Id}!");
                        }
                        staticFileEntity.Img = (string)url.Result;
                        staticFileEntity.EventId = eventEntity.Id;
                        var resultUpdate = await staticFileRepository.UploadStaticFile(staticFileEntity);
                        if (resultUpdate == null)
                        {
                            return BuildAppActionResultError(new AppActionResult() { IsSuccess = false }, $"Cannot update static file with id: {staticFile.Id}");
                        }
                    }
                //Update speaker event
                foreach (var speaker in request.Speakers)
                {
                    var speakerEntity = await speakerRepository.GetById(speaker.Id);
                    if (speakerEntity == null)
                    {
                        return BuildAppActionResultSuccess(new AppActionResult() { IsSuccess = false }, $"Not found speaker with id: {speaker.Id}");
                    }
                    await _firebaseService.DeleteFileFromFirebase($"{SD.FirebasePathName.SPEAKER}{speaker.Id}");
                    var url = await _firebaseService.UploadFileToFirebase(speaker.ImgFormFile, $"{SD.FirebasePathName.SPEAKER}{speaker.Id}");
                    if (!url.IsSuccess)
                    {
                        return BuildAppActionResultError(new AppActionResult() { IsSuccess = false }, $"Cannot upload file with speaker id {speaker.Id}!");
                    }
                    _mapper.Map(speaker, speakerEntity);
                    speakerEntity.EventId = eventEntity.Id;
                    speakerEntity.Img = (string)url.Result;
                    var resultUpdate = await speakerRepository.Update(speakerEntity);
                    if (resultUpdate == null)
                    {
                        return BuildAppActionResultError(new AppActionResult() { IsSuccess = false }, $"Cannot update speaker with id: {speaker.Id}");
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                result.Result = resultUpdateEvent;
                return BuildAppActionResultSuccess(new AppActionResult() { IsSuccess = false }, "Update event successfully!");
            }
            catch (Exception ex)
            {
                return BuildAppActionResultError(result, ex.Message, true);
            }
        }

		public async Task<AppActionResult> GetEventByStatus(Guid? organizationId, int happened, int pageNumber, int pageSize)
		{
			AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var utility = Resolve<Utility>();
				PagedResult<Event> eventDb = await eventRepository.GetEventsWithStatus(organizationId, utility.GetCurrentDateInTimeZone(), happened, pageNumber, pageSize);
                result.Result = eventDb;
            } catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
		}

		public async Task<AppActionResult> CountingEventByStatus(Guid? organizationId)
		{
            AppActionResult result = new();
            try
            {
				var eventRepository = Resolve<IEventRepository>();
				var utility = Resolve<Utility>();
				int[] eventDb = await eventRepository.CountingEventsWithStatus(organizationId, utility.GetCurrentDateInTimeZone());
				result.Result = eventDb;
			} catch(Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
		}

        public async Task<AppActionResult> GetAllEventByOrganizationId(Guid organizationId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var data = await eventRepository.GetEventsByOrganizationId(organizationId, pageNumber, pageSize);
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

        public async Task<AppActionResult> GetAllEventBySponsorId(Guid sponsorId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventSponsorRepository>();
                var data = await eventRepository.GetEventsBySponsorId(sponsorId, pageNumber, pageSize);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get list sponsor event successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateEventStatus(Guid eventId, EventCensorStatus status)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetById(eventId);
                if(eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy sư kiện với id {eventId}");
                    return result;
                }
                if(eventDb.Status != EventCensorStatus.PENDING)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện {eventDb.Title} đã được duyệt, không thể chuyển trạng thái");
                    return result;
                }

                eventDb.Status = status;
                await eventRepository.Update(eventDb);
                await _unitOfWork.SaveChangeAsync();
            } catch(Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }


        public async Task<AppActionResult> GetAllEventByStatus(EventCensorStatus status, int pageNumber, int pageSize)
        {
            var result = new AppActionResult(); 
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetAllDataByExpression(p => p.Status == status, pageNumber, pageSize, null, false, p => p.Organization!, p => p.Location! );
                result.Result = eventDb;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}