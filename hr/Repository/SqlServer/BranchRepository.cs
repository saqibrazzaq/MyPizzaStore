using hr.Entities;
using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext context) : base(context)
        {
        }
    }
}
