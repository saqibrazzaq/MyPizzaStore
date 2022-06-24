using cities.ActionFilters;
using cities.Dtos.Country;
using cities.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cities.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("all")]
        public IActionResult GetAllCountries([FromQuery] GetAllCountriesRequestDto dto)
        {
            var countryDtos = _countryService.GetAllCountries(dto);
            return Ok(countryDtos.Data);
        }

        [HttpGet("details")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetCountryByCode([FromQuery] GetCountryByCodeRequestDto dto)
        {
            var countryDto = _countryService.GetCountryByCode(dto);
            return Ok(countryDto.Data);
        }
    }
}
