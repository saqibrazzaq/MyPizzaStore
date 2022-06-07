using auth.Entities.Database;
using auth.Repository.Contracts;

namespace auth.Repository.SqlServer
{
    public class UserRepository : RepositoryBase<AppIdentityUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
