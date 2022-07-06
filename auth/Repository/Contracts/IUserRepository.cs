using auth.Dtos;
using auth.Dtos.User;
using auth.Entities.Database;
using common.Models.Parameters;

namespace auth.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<PagedList<AppIdentityUser>> SearchUsersAsync(
            UserParameters userParameters, bool trackChanges);
    }
}
