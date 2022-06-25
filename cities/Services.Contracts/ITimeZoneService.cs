using cities.Dtos.PagedRequest;
using cities.Dtos.TimeZone;
using cities.Models.Responses;

namespace cities.Services.Contracts
{
    public interface ITimeZoneService
    {
        ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetAllTimeZones();
        ApiOkPagedResponse<IEnumerable<TimeZoneResponseDto>, MetaData> SearchTimeZones(SearchTimeZoneRequestDto dto);
        ApiOkResponse<TimeZoneResponseDto> GetTimeZone(Guid timeZoneId);
        ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetTimeZoneByCountryId(Guid countryId);
        ApiOkResponse<IEnumerable<TimeZoneResponseDto>> GetTimeZoneByCountryCode(string countryCode);
        void CreateTimeZone(CreateTimeZoneRequestDto dto);
        void UpdateTimeZone(Guid timeZoneId, UpdateTimeZoneRequestDto dto);
        void DeleteTimeZone(Guid timeZoneId);
    }
}
