using hr.Entities;
using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class GenderRepository : RepositoryBase<Gender>, IGenderRepository
    {
        public GenderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
