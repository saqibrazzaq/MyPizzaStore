using cities.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cities.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataResetController : ControllerBase
    {
        private readonly IDataSeedService _dataSeedService;

        public DataResetController(IDataSeedService dataSeedService)
        {
            _dataSeedService = dataSeedService;
        }

        [HttpGet("reset-data")]
        public async Task<IActionResult> ResetData()
        {
            await _dataSeedService.ResetCityStateCountries();
            return Ok();
        }
    }
}
