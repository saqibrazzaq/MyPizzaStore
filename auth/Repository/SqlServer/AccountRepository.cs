using auth.Entities.Database;
using auth.Repository.Contracts;

namespace auth.Repository.SqlServer
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
