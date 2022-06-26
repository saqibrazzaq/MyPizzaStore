using hr.Entities;
using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
