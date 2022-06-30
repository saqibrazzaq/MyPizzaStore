using AutoMapper;
using hr.Dtos.Company;
using hr.Entities;

namespace hr
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Company
            CreateMap<CreateCompanyRequestDto, Company>();
            CreateMap<UpdateCompanyRequestDto, Company>();
            CreateMap<Company, CompanyResponseDto>();
            CreateMap<Company, CompanyDetailResponseDto>();
        }
    }
}
