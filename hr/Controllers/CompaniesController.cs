using hr.ActionFilters;
using hr.Dtos.Company;
using hr.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hr.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetAll([FromQuery] GetAllCompaniesRequestDto dto)
        {
            var res = _companyService.GetAll(dto);
            return Ok(res.Data);
        }

        [HttpGet("search")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Search([FromQuery] SearchCompaniesRequestDto dto)
        {
            var res = _companyService.Search(dto);
            return Ok(res);
        }

        [HttpGet("{companyId}", Name = "FindByCompanyId")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult FindByCompanyId(Guid companyId, [FromQuery] FindByCompanyIdRequestDto dto)
        {
            var res = _companyService.FindByCompanyId(companyId, dto);
            return Ok(res.Data);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromBody] CreateCompanyRequestDto dto)
        {
            var res = _companyService.Create(dto);
            return CreatedAtAction(nameof(FindByCompanyId), new { res.CompanyId, res.AccountId }, res);
        }

        [HttpPut("{companyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Update(Guid companyId, [FromBody] UpdateCompanyRequestDto dto)
        {
            _companyService.Update(companyId, dto);
            return NoContent();
        }

        [HttpDelete("{companyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Delete(Guid companyId, [FromQuery] DeleteCompanyRequestDto dto)
        {
            _companyService.Delete(companyId, dto);
            return NoContent();
        }
    }
}
