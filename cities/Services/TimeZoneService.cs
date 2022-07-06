using AutoMapper;
using cities.Dtos.TimeZone;
using cities.Models.Exceptions;
using cities.Repository.Contracts;
using cities.Services.Contracts;
using common.Models.Parameters;
using common.Models.Responses;

namespace cities.Services
{
    public class TimeZoneService : ITimeZoneService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public TimeZoneService(IRepositoryManager repositoryManager, 
            IMapper mapper, 
            ICountryService countryService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void CreateTimeZone(CreateTimeZoneRequestDto dto)
        {
            var timeZoneEntity = _mapper.Map<Entities.TimeZone>(dto);
            _repositoryManager.TimeZoneRepository.Create(timeZoneEntity);
            _repositoryManager.Save();
        }

        public void DeleteTimeZone(Guid timeZoneId)
        {
            var timeZoneEntity = GetTimeZoneIfExists(timeZoneId, true);

            _repositoryManager.TimeZoneRepository.Delete(timeZoneEntity);
            _repositoryManager.Save();
        }

        public ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetAllTimeZones()
        {
            var timeZoneEntities = _repositoryManager.TimeZoneRepository.FindAll(false);
            var timeZoneDtos = _mapper.Map<IEnumerable<TimeZoneResponseDto>>(timeZoneEntities);
            return new ApiOkResponse<IEnumerable<TimeZoneResponseDto>>(timeZoneDtos);
        }

        public ApiOkResponse<TimeZoneResponseDto> GetTimeZone(Guid timeZoneId)
        {
            var timeZoneEntity = GetTimeZoneIfExists(timeZoneId, false);
            var timeZoneDto = _mapper.Map<TimeZoneResponseDto>(timeZoneEntity);
            return new ApiOkResponse<TimeZoneResponseDto>(timeZoneDto);
        }

        private Entities.TimeZone GetTimeZoneIfExists(Guid timeZoneId, bool trackChanges)
        {
            var timeZoneEntity = _repositoryManager.TimeZoneRepository.FindByCondition(
                x => x.TimeZoneId == timeZoneId, trackChanges)
                .FirstOrDefault();

            if (timeZoneEntity == null)
                throw new NotFoundException("No TimeZone found with id " + timeZoneId);

            return timeZoneEntity;
        }

        public void UpdateTimeZone(Guid timeZoneId, UpdateTimeZoneRequestDto dto)
        {
            var timeZoneEntity = GetTimeZoneIfExists(timeZoneId, true);
            _mapper.Map(dto, timeZoneEntity);
            _repositoryManager.Save();
        }

        public ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetTimeZoneByCountryId(
            Guid countryId)
        {
            if (countryId == Guid.Empty)
                throw new BadRequestException("Invalid country id " + countryId);

            var timeZoneEntities = _repositoryManager.TimeZoneRepository.FindByCondition(
                x => x.CountryId == countryId, trackChanges: false);
            var timeZoneDtos = _mapper.Map<IEnumerable<TimeZoneResponseDto>>(timeZoneEntities);
            return new ApiOkResponse<IEnumerable<TimeZoneResponseDto>>(timeZoneDtos);
        }

        public ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetTimeZoneByCountryCode(
            string countryCode)
        {
            var countryId = _repositoryManager.CountryRepository.FindByCondition(
                x => x.CountryCode == countryCode, trackChanges: false)
                .Select(x => x.CountryId)
                .FirstOrDefault();
            if (countryId == Guid.Empty)
                throw new NotFoundException("No time zones found with country code " + countryCode);

            return GetTimeZoneByCountryId(countryId);
        }

        public ApiOkPagedResponse<IEnumerable<TimeZoneResponseDto>, MetaData> 
            SearchTimeZones(SearchTimeZoneRequestDto dto)
        {
            var timeZonePagedEntities = _repositoryManager.TimeZoneRepository.SearchTimeZones(
                dto, trackChanges: false);
            var timeZoneDtos = _mapper.Map<IEnumerable<TimeZoneResponseDto>>(timeZonePagedEntities);
            return new ApiOkPagedResponse<IEnumerable<TimeZoneResponseDto>, MetaData>(
                timeZoneDtos, timeZonePagedEntities.MetaData);
        }
    }
}
