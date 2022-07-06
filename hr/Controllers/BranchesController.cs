using hr.ActionFilters;
using hr.Dtos.Branch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hr.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        [HttpGet("all-branches")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult FindAll([FromQuery] FindAllBranchesRequestDto dto)
        {
            return Ok();
        }
        
        [HttpGet("all-branches/{companyId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult FindAllByCompanyId(Guid companyId, [FromQuery] FindAllBranchesByCompanyIdRequestDto dto)
        {
            return Ok();
        }

        [HttpGet("{branchId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult FindByBranchId(Guid branchId, [FromQuery] FindByBranchIdRequestDto dto)
        {
            return Ok();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromBody] CreateBranchRequestDto dto)
        {
            return Ok();
        }

        [HttpPut("{branchId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Update(Guid branchId, [FromBody] UpdateBranchRequestDto dto)
        {
            return NoContent();
        }

        [HttpDelete("{branchId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Delete(Guid branchId, [FromQuery] DeleteBranchRequestDto dto)
        {
            return NoContent();
        }
    }
}
