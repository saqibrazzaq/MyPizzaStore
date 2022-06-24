using AutoMapper;
using cities.Dtos.Country;
using cities.Models.Exceptions;
using cities.Models.Responses;
using cities.Repository.Contracts;
using cities.Services.Contracts;

namespace cities.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CountryService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public ApiOkResponse<IEnumerable<CountryResponseDto>> GetAllCountries(GetAllCountriesRequestDto dto)
        {
            var countryEntities = _repositoryManager.CountryRepository.FindAll(trackChanges: false);
            var countryDtos = _mapper.Map<IEnumerable<CountryResponseDto>>(countryEntities);
            return new ApiOkResponse<IEnumerable<CountryResponseDto>>(countryDtos);
        }

        public ApiOkResponse<CountryDetailResponseDto> GetCountryByCode(GetCountryByCodeRequestDto dto)
        {
            var countryEntity = _repositoryManager.CountryRepository.FindByCondition(
                expression: x => x.CountryCode == dto.CountryCode,
                trackChanges: false)
                .FirstOrDefault();

            if (countryEntity == null)
                throw new NotFoundException("No country found with code " + dto.CountryCode);

            var countryDto = _mapper.Map<CountryDetailResponseDto>(countryEntity);
            return new ApiOkResponse<CountryDetailResponseDto>(countryDto);
        }
    }
}
