using cities.ActionFilters;
using cities.Dtos.TimeZone;
using cities.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cities.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimeZonesController : ControllerBase
    {
        private readonly ITimeZoneService _timeZoneService;

        public TimeZonesController(ITimeZoneService timeZoneService)
        {
            _timeZoneService = timeZoneService;
        }

        [HttpGet]
        public IActionResult GetAllTimeZones()
        {
            var timeZoneDtos = _timeZoneService.GetAllTimeZones();
            return Ok(timeZoneDtos.Data);
        }

        [HttpGet("search")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult SearchTimeZones([FromQuery] SearchTimeZoneRequestDto dto)
        {
            var res = _timeZoneService.SearchTimeZones(dto);
            return Ok(res);
        }

        [HttpGet("{timeZoneId}")]
        public IActionResult GetTimeZone(Guid timeZoneId)
        {
            var res = _timeZoneService.GetTimeZone(timeZoneId);
            return Ok(res.Data);
        }

        [HttpGet("getByCountryId/{countryId}")]
        public IActionResult GetTimeZoneByCountryId(Guid countryId)
        {
            var res = _timeZoneService.GetTimeZoneByCountryId(countryId);
            return Ok(res.Data);
        }

        [HttpGet("getByCountryCode/{countryCode}")]
        public IActionResult GetTimeZoneByCountryCode(string countryCode)
        {
            var res = _timeZoneService.GetTimeZoneByCountryCode(countryCode);
            return Ok(res.Data);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateTimeZone([FromBody] CreateTimeZoneRequestDto dto)
        {
            _timeZoneService.CreateTimeZone(dto);
            return NoContent();
        }

        [HttpPut("{timeZoneId}")]
        [ServiceFilter(typeof (ValidationFilterAttribute))]
        public IActionResult UpdateTimeZone(Guid timeZoneId, [FromBody] UpdateTimeZoneRequestDto dto)
        {
            _timeZoneService.UpdateTimeZone(timeZoneId, dto);
            return NoContent();
        }

        [HttpDelete("{timeZoneId}")]
        public IActionResult DeleteTimeZone(Guid timeZoneId)
        {
            _timeZoneService.DeleteTimeZone(timeZoneId);
            return NoContent();
        }
    }
}
