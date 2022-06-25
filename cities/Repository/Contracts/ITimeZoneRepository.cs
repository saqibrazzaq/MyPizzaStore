using cities.Dtos.PagedRequest;
using cities.Dtos.TimeZone;

namespace cities.Repository.Contracts
{
    public interface ITimeZoneRepository : IRepositoryBase<Entities.TimeZone>
    {
        PagedList<Entities.TimeZone> SearchTimeZones(
            SearchTimeZoneRequestDto dto, bool trackChanges);
    }
}
