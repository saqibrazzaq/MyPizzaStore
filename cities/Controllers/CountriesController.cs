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

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countryDtos = _countryService.GetAllCountries();
            return Ok(countryDtos.Data);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchCountryRequestDto dto)
        {
            var res = _countryService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(Guid countryId)
        {
            var countryDto = _countryService.GetCountry(countryId);
            return Ok(countryDto.Data);
        }

        [HttpGet("getByCode/{countryCode}")]
        public IActionResult GetCountryByCode(string countryCode)
        {
            var countryDto = _countryService.GetCountryByCode(countryCode);
            return Ok(countryDto.Data);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateCountry([FromBody] CreateCountryRequestDto dto)
        {
            _countryService.CreateCountry(dto);
            return NoContent();
        }

        [HttpPut("{countryId}")]
        public IActionResult UpdateCountry(Guid countryId, [FromBody] UpdateCountryRequestDto dto)
        {
            _countryService.UpdateCountry(countryId, dto);
            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(Guid countryId)
        {
            _countryService.DeleteCountry(countryId);
            return NoContent();
        }
    }
}
