using cities.Dtos.PagedRequest;
using cities.Dtos.State;
using cities.Entities;

namespace cities.Repository.Contracts
{
    public interface IStateRepository : IRepositoryBase<State>
    {
        PagedList<State> SearchStates(SearchStateRequestDto dto, bool trackChanges);
    }
}
