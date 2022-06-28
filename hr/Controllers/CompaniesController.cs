using hr.ActionFilters;
using hr.Dtos.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hr.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetAll([FromQuery] GetAllCompaniesRequestDto dto)
        {
            return Ok();
        }

        [HttpGet("{companyId}")]
        [ServiceFilter(typeof (ValidationFilterAttribute))]
        public IActionResult FindByCompanyId(Guid companyId, [FromQuery] FindByCompanyIdRequestDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromBody] CreateCompanyRequestDto dto)
        {
            return NoContent();
        }

        [HttpPut("{companyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Update(Guid companyId, [FromBody] UpdateCompanyRequestDto dto)
        {
            return NoContent();
        }

        [HttpDelete("{companyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Delete(Guid companyId, [FromQuery] DeleteCompanyRequestDto dto)
        {
            return NoContent();
        }
    }
}
