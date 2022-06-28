using hr.ActionFilters;
using hr.Dtos.Branch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hr.Controllers
{
    [Route("api/v1/Companies/{companyId}/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetAllBranches(Guid companyId, [FromQuery] GetAllBranchesRequestDto dto)
        {
            return Ok();
        }

        [HttpGet("{branchId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult FindByBranchId([FromQuery] FindByBranchIdRequestDto dto)
        {
            return Ok();
        }
    }
}
