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
                config.CreateMap<SponsorDto, Account>()
                   .ForMember(desc => desc.UserName, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.Email, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                   .ForMember(desc => desc.FirstName, act => act.MapFrom(src => src.Name));

                config.CreateMap<QuestionDetailRequest, SurveyQuestionDetail>()
                   .ForMember(desc => desc.Question, act => act.MapFrom(src => src.Question))
                   .ForMember(desc => desc.AnswerType, act => act.MapFrom(src => src.AnswerType))
                   .ForMember(desc => desc.RatingMax, act => act.MapFrom(src => src.RatingMax));
            });
            return mappingConfig;
        }
    }
}