using cities.Dtos.City;
using cities.Dtos.PagedRequest;
using common.Models.Responses;

namespace cities.Services.Contracts
{
    public interface ICityService
    {
        ApiOkResponse<CityDetailResponseDto> FindById(Guid cityId);
        ApiOkPagedResponse<IEnumerable<CityResponseDto>, MetaData> Search(
            SearchCityRequestDto dto);
        void Create(CreateCityRequestDto dto);
        void Update(Guid cityId, UpdateCityRequestDto dto);
        void Delete(Guid cityId);
    }
}
