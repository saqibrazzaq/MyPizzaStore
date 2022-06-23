using AutoMapper;

namespace cities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // City, state, countries reset data
            CreateMap<Models.DataSeed.Cities.Country, Entities.Country>()
                .ForMember(dest => dest.NativeName, opt => opt.MapFrom(src => src.native))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.iso3))
                .ForMember(dest => dest.PhoneCode, opt => opt.MapFrom(src => src.phone_code))
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.currency_name))
                .ForMember(dest => dest.CurrencySymbol, opt => opt.MapFrom(src => src.currency_symbol));
            CreateMap<Models.DataSeed.Cities.TimeZone, Entities.TimeZone>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.zoneName))
                .ForMember(dest => dest.TimeZoneName, opt => opt.MapFrom(src => src.tzName));
            CreateMap<Models.DataSeed.Cities.State, Entities.State>()
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.state_code));
            CreateMap<Models.DataSeed.Cities.City, Entities.City>();
        }
    }
}
