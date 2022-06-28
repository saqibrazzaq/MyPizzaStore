using common.Models.Responses;
using hr.Dtos.Company;

namespace hr.Services.Contracts
{
    public interface ICompanyService
    {
        ApiOkResponse<IEnumerable<CompanyResponseDto>> GetAll(GetAllCompaniesRequestDto dto);

    }
}
