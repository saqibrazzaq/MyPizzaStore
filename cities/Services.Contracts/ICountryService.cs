using cities.Dtos.Country;
using cities.Models.Responses;

namespace cities.Services.Contracts
{
    public interface ICountryService
    {
        ApiOkResponse<IEnumerable<CountryResponseDto>> GetAllCountries();
        ApiOkResponse<CountryDetailResponseDto> GetCountryByCode(string countryCode);
        ApiOkResponse<CountryDetailResponseDto> GetCountry(Guid countryId);
        void CreateCountry(CreateCountryRequestDto dto);
        void UpdateCountry(Guid countryId, UpdateCountryRequestDto dto);
        void DeleteCountry(Guid countryId);
    }
}
