using hr.Entities;
using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
