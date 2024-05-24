﻿using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Common.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<SponsorDto, Account>()
                   .ForMember(desc => desc.UserName, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.Email, act => act.MapFrom(src => src.Email))
                   .ForMember(desc => desc.PhoneNumber, act => act.MapFrom(src => src.PhoneNumber))
                   .ForMember(desc => desc.FirstName, act => act.MapFrom(src => src.Name));
            });
            return mappingConfig;
        }
    }
}