using common.Models.Parameters;
using hr.Dtos.Company;
using hr.Entities;

namespace hr.Repository.Contracts
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        PagedList<Company> SearchCompanies(
            SearchCompaniesRequestDto dto, bool trackChanges);
    }
}
