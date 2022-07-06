using AutoMapper;
using cities.Dtos.State;
using cities.Entities;
using cities.Models.Exceptions;
using cities.Repository.Contracts;
using cities.Services.Contracts;
using common.Models.Parameters;
using common.Models.Responses;

namespace cities.Services
{
    public class StateService : IStateService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public StateService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Create(CreateStateRequestDto dto)
        {
            var stateEntity = _mapper.Map<State>(dto);
            _repositoryManager.StateRepository.Create(stateEntity);
            _repositoryManager.Save();
        }

        public void Delete(Guid stateId)
        {
            var stateEntity = FindByIdIfExists(stateId, true);
            _repositoryManager.StateRepository.Delete(stateEntity);
            _repositoryManager.Save();
        }

        private State FindByIdIfExists(Guid stateId, bool trackChanges)
        {
            var stateEntity = _repositoryManager.StateRepository.FindByCondition(
                x => x.StateId == stateId,
                trackChanges)
                .FirstOrDefault();
            if (stateEntity == null)
                throw new NotFoundException("No state found with id " + stateId);

            return stateEntity;
        }

        public ApiOkResponse<IEnumerable<StateResponseDto>> FindByCountryCode(string countryCode)
        {
            var countryId = _repositoryManager.CountryRepository.FindByCondition(
                x => x.CountryCode == countryCode, trackChanges: false)
                .Select(x => x.CountryId)
                .FirstOrDefault();
            if (countryId == Guid.Empty)
                throw new NotFoundException("No country found with code " + countryCode);

            return FindByCountryId(countryId);
        }

        public ApiOkResponse<IEnumerable<StateResponseDto>> FindByCountryId(Guid countryId)
        {
            var stateEntities = _repositoryManager.StateRepository.FindByCondition(
                x => x.CountryId == countryId, trackChanges: false);
            if (stateEntities == null)
                throw new NotFoundException("No state found with country id " + countryId);

            var stateDtos = _mapper.Map<IEnumerable<StateResponseDto>>(stateEntities);
            return new ApiOkResponse<IEnumerable<StateResponseDto>>(stateDtos);
        }

        public ApiOkResponse<StateResponseDto> FindById(Guid stateId)
        {
            var stateEntity = FindByIdIfExists(stateId, false);
            var stateDto = _mapper.Map<StateResponseDto>(stateEntity);
            return new ApiOkResponse<StateResponseDto>(stateDto);
        }

        public ApiOkResponse<StateResponseDto> FindByStateCode(string stateCode)
        {
            var stateEntity = _repositoryManager.StateRepository.FindByCondition(
                x => x.StateCode == stateCode, false)
                .FirstOrDefault();
            if (stateEntity == null)
                throw new NotFoundException("No state found with code " + stateCode);

            var stateDto = _mapper.Map<StateResponseDto>(stateEntity);
            return new ApiOkResponse<StateResponseDto>(stateDto);
        }

        public ApiOkPagedResponse<IEnumerable<StateResponseDto>, MetaData> Search(SearchStateRequestDto dto)
        {
            var statePagedEntities = _repositoryManager.StateRepository.SearchStates(dto, false);
            var stateDtos = _mapper.Map<IEnumerable<StateResponseDto>>(statePagedEntities);
            return new ApiOkPagedResponse<IEnumerable<StateResponseDto>, MetaData>(
                stateDtos, statePagedEntities.MetaData);
        }

        public void Update(Guid stateId, UpdateStateRequestDto dto)
        {
            var stateEntity = FindByIdIfExists(stateId, true);
            _mapper.Map(dto, stateEntity);
            _repositoryManager.Save();
        }
    }
}
