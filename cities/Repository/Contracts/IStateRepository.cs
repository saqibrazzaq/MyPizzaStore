using cities.Dtos.State;
using cities.Entities;
using common.Models.Parameters;

namespace cities.Repository.Contracts
{
    public interface IStateRepository : IRepositoryBase<State>
    {
        PagedList<State> SearchStates(SearchStateRequestDto dto, bool trackChanges);
    }
}
