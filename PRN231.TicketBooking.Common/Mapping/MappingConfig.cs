using AutoMapper;


namespace PRN231.TicketBooking.Common.Mapping
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<BlogRequest, Blog>()
                //   .ForMember(desc => desc.Id, act => act.MapFrom(src => src.Id))

            });
            return mappingConfig;
        }
    }
}