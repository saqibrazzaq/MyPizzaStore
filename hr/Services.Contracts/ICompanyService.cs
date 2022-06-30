using common.Models.Responses;
using hr.Dtos.Company;

namespace hr.Services.Contracts
{
    public interface ICompanyService
    {
        ApiOkResponse<IEnumerable<CompanyResponseDto>> GetAll(GetAllCompaniesRequestDto dto);
        ApiOkResponse<CompanyDetailResponseDto> FindByCompanyId(Guid companyId, FindByCompanyIdRequestDto dto);
        void Create (CreateCompanyRequestDto dto);
        void Update (Guid companyId, UpdateCompanyRequestDto dto);
        void Delete (Guid companyId, DeleteCompanyRequestDto dto);
    }
}
