using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;

namespace PRN231.TicketBooking.Common.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Account, AccountResponse>()
                .ForMember(desc => desc.Id, act => act.MapFrom(src => src.Id))
                .ForMember(desc => desc.Email, act => act.MapFrom(src => src.Email))
                .ForMember(desc => desc.Gender, act => act.MapFrom(src => src.Gender))
                .ForMember(desc => desc.IsVerified, act => act.MapFrom(src => src.IsVerified))
                .ForMember(desc => desc.FirstName, act => act.MapFrom(src => src.FirstName))
                .ForMember(desc => desc.LastName, act => act.MapFrom(src => src.LastName))
                .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                .ForMember(desc => desc.UserName, act => act.MapFrom(src => src.UserName))
                ;

                config.CreateMap<BusinessObject.Models.Task, TaskDetails>()
               .ForMember(desc => desc.Name, act => act.MapFrom(src => src.Name))
               .ForMember(desc => desc.Description, act => act.MapFrom(src => src.Description))
               .ForMember(desc => desc.PersonInChargeName, act => act.MapFrom(src => src.PersonInChargeName))
               .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
               .ForMember(desc => desc.Cost, act => act.MapFrom(src => src.Cost))
               .ForMember(desc => desc.TaskStatus, act => act.MapFrom(src => src.Status))
               .ForMember(desc => desc.PersonInChargeName, act => act.MapFrom(src => src.PersonInChargeName))
               .ForMember(desc => desc.CreatedDate, act => act.MapFrom(src => src.CreatedDate))
               .ForMember(desc => desc.EndDate, act => act.MapFrom(src => src.EndDate))
               .ReverseMap()
               ;

                config.CreateMap<Post, UpdatePostDto>()
                .ForMember(desc => desc.Id, act => act.MapFrom(src => src.Id))
                .ForMember(desc => desc.EventId, act => act.MapFrom(src => src.EventId))
                .ForMember(desc => desc.Title, act => act.MapFrom(src => src.Title))
                .ForMember(desc => desc.Content, act => act.MapFrom(src => src.Content))
                .ReverseMap();

                config.CreateMap<Post, CreatePostDto>()
            .ForMember(desc => desc.EventId, act => act.MapFrom(src => src.EventId))
            .ForMember(desc => desc.Title, act => act.MapFrom(src => src.Title))
            .ForMember(desc => desc.Content, act => act.MapFrom(src => src.Content))
            .ReverseMap();


                config.CreateMap<BusinessObject.Models.Task, UpdateTask>()
              .ForMember(desc => desc.Id, act => act.MapFrom(src => src.Id))
              .ForMember(desc => desc.Name, act => act.MapFrom(src => src.Name))
              .ForMember(desc => desc.Description, act => act.MapFrom(src => src.Description))
              .ForMember(desc => desc.PersonInChargeName, act => act.MapFrom(src => src.PersonInChargeName))
              .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
              .ForMember(desc => desc.Cost, act => act.MapFrom(src => src.Cost))
              .ForMember(desc => desc.TaskStatus, act => act.MapFrom(src => src.Status))
              .ForMember(desc => desc.PersonInChargeName, act => act.MapFrom(src => src.PersonInChargeName))
              .ForMember(desc => desc.CreatedDate, act => act.MapFrom(src => src.CreatedDate))
              .ForMember(desc => desc.EndDate, act => act.MapFrom(src => src.EndDate))
              .ReverseMap()
              ;

                config.CreateMap<SeatRank, UpdateSeatRankDto>()
              .ForMember(desc => desc.Id, act => act.MapFrom(src => src.Id))
              .ForMember(desc => desc.Name, act => act.MapFrom(src => src.Name))
              .ForMember(desc => desc.EventId, act => act.MapFrom(src => src.EventId))
              .ForMember(desc => desc.Quantity, act => act.MapFrom(src => src.Quantity))
              .ForMember(desc => desc.Price, act => act.MapFrom(src => src.Price))
              .ForMember(desc => desc.StartTime, act => act.MapFrom(src => src.StartTime))
              .ForMember(desc => desc.EndTime, act => act.MapFrom(src => src.EndTime))
              .ForMember(desc => desc.Description, act => act.MapFrom(src => src.Description))
              .ForMember(desc => desc.RemainingCapacity, act => act.MapFrom(src => src.RemainingCapacity))
              .ForMember(desc => desc.RemainingCapacity, act => act.MapFrom(src => src.RemainingCapacity))
              .ReverseMap();

                config.CreateMap<SponsorDto, Account>()
                   .ForMember(desc => desc.UserName, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.Email, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                   .ForMember(desc => desc.FirstName, act => act.MapFrom(src => src.Name));

                config.CreateMap<QuestionDetailRequest, SurveyQuestionDetail>()
                   .ForMember(desc => desc.Question, act => act.MapFrom(src => src.Question))
                   .ForMember(desc => desc.No, act => act.MapFrom(src => src.No))
                   .ForMember(desc => desc.AnswerType, act => act.MapFrom(src => src.AnswerType))
                   .ForMember(desc => desc.RatingMax, act => act.MapFrom(src => src.RatingMax));
                config.CreateMap<CreateEventRequest, Event>().ReverseMap();
                config.CreateMap<CreateSeatRankEventRequest, SeatRank>().ReverseMap();
                config.CreateMap<CreateSeatRankEventResponse, SeatRank>().ReverseMap();
                config.CreateMap<CreateEventRequest, CreateEventResponse>().ReverseMap();
                config.CreateMap<GetEventByIdResponse, Event>().ReverseMap();

                config.CreateMap<UpdateEventRequest, Event>().ReverseMap();
                config.CreateMap<UpdateSeatRankEventRequest, SeatRank>().ReverseMap();
                config.CreateMap<UpdateSpeakerEventRequest, Speaker>().ReverseMap();
                config.CreateMap<CreateEventSponsorEvent, EventSponsor>().ReverseMap();
                config.CreateMap<CreateSpeakerEvent, Speaker>().ReverseMap();

            });
            return mappingConfig;

        }
    }
}