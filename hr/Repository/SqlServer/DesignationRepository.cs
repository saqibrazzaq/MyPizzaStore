using hr.Entities;
using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class DesignationRepository : RepositoryBase<Designation>, IDesignationRepository
    {
        public DesignationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
