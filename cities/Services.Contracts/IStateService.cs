using cities.Dtos.PagedRequest;
using cities.Dtos.State;
using common.Models.Responses;

namespace cities.Services.Contracts
{
    public interface IStateService
    {
        ApiOkResponse<StateResponseDto> FindById(Guid stateId);
        ApiOkResponse<StateResponseDto> FindByStateCode(string stateCode);
        ApiOkResponse<IEnumerable<StateResponseDto>> FindByCountryCode(string countryCode);
        ApiOkResponse<IEnumerable<StateResponseDto>> FindByCountryId(Guid countryId);
        ApiOkPagedResponse<IEnumerable<StateResponseDto>, MetaData> Search(SearchStateRequestDto dto);
        void Create(CreateStateRequestDto dto);
        void Update(Guid stateId, UpdateStateRequestDto dto);
        void Delete(Guid stateId);
    }
}
