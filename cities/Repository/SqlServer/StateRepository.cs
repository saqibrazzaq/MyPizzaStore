using cities.Dtos.PagedRequest;
using cities.Dtos.State;
using cities.Entities;
using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public StateRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<State> SearchStates(
            SearchStateRequestDto dto, bool trackChanges)
        {
            var searchEntities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<State>(searchEntities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
