using auth.Dtos;
using auth.Dtos.User;
using auth.Entities.Database;
using auth.Repository.Contracts;
using auth.Repository.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore;

namespace auth.Repository.SqlServer
{
    public class UserRepository : RepositoryBase<AppIdentityUser>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<AppIdentityUser>> SearchUsersAsync(
            UserParameters userParameters, bool trackChanges)
        {
            // Find users
            var users = await FindAll(trackChanges)
                .Search(userParameters)
                .Sort(userParameters.OrderBy)
                .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
                .Take(userParameters.PageSize)
                .ToListAsync();
            // Get count by using the same search parameters
            var count = await FindAll(trackChanges)
                .Search(userParameters)
                .CountAsync();
            return new PagedList<AppIdentityUser>(users, count, userParameters.PageNumber,
                userParameters.PageSize);
        }
    }
}
