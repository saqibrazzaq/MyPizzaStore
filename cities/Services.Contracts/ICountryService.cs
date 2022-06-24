using cities.Dtos.Country;
using cities.Models.Responses;

namespace cities.Services.Contracts
{
    public interface ICountryService
    {
        ApiOkResponse<IEnumerable<CountryResponseDto>> GetAllCountries(
            GetAllCountriesRequestDto dto);
        ApiOkResponse<CountryDetailResponseDto> GetCountryByCode(
            GetCountryByCodeRequestDto dto);
    }
}
