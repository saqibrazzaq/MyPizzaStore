using cities.Entities;
using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public StateRepository(AppDbContext context) : base(context)
        {
        }
    }
}
