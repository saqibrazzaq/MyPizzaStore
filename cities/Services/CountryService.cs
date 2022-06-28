using AutoMapper;
using cities.Dtos.Country;
using cities.Entities;
using cities.Models.Exceptions;
using cities.Repository.Contracts;
using cities.Services.Contracts;
using common.Models.Responses;

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

        public void CreateCountry(CreateCountryRequestDto dto)
        {
            var countryEntity = _mapper.Map<Country>(dto);
            _repositoryManager.CountryRepository.Create(countryEntity);
            _repositoryManager.Save();
        }

        public ApiOkResponse<IEnumerable<CountryResponseDto>> GetAllCountries()
        {
            var countryEntities = _repositoryManager.CountryRepository.FindAll(trackChanges: false);
            var countryDtos = _mapper.Map<IEnumerable<CountryResponseDto>>(countryEntities);
            return new ApiOkResponse<IEnumerable<CountryResponseDto>>(countryDtos);
        }

        public ApiOkResponse<CountryDetailResponseDto> GetCountry(Guid countryId)
        {
            var countryEntity = GetCountryIfExists(countryId, false);

            var countryDto = _mapper.Map<CountryDetailResponseDto>(countryEntity);
            return new ApiOkResponse<CountryDetailResponseDto>(countryDto);
        }

        private Country GetCountryIfExists(Guid countryId, bool trackChanges)
        {
            var countryEntity = _repositoryManager.CountryRepository.FindByCondition(
                expression: x => x.CountryId == countryId,
                trackChanges)
                .FirstOrDefault();

            if (countryEntity == null)
                throw new NotFoundException("No country found with id " + countryId);

            return countryEntity;
        }

        public ApiOkResponse<CountryDetailResponseDto> GetCountryByCode(string countryCode)
        {
            var countryEntity = _repositoryManager.CountryRepository.FindByCondition(
                expression: x => x.CountryCode == countryCode,
                trackChanges: false)
                .FirstOrDefault();

            if (countryEntity == null)
                throw new NotFoundException("No country found with code " + countryCode);

            var countryDto = _mapper.Map<CountryDetailResponseDto>(countryEntity);
            return new ApiOkResponse<CountryDetailResponseDto>(countryDto);
        }

        public void UpdateCountry(Guid countryId, UpdateCountryRequestDto dto)
        {
            var countryEntity = GetCountryIfExists(countryId, true);
            _mapper.Map(dto, countryEntity);
            _repositoryManager.Save();
        }

        public void DeleteCountry(Guid countryId)
        {
            var countryEntity = GetCountryIfExists(countryId, true);
            _repositoryManager.CountryRepository.Delete(countryEntity);
            _repositoryManager.Save();
        }
    }
}
