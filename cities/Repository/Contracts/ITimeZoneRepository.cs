using cities.Dtos.TimeZone;
using common.Models.Parameters;

namespace cities.Repository.Contracts
{
    public interface ITimeZoneRepository : IRepositoryBase<Entities.TimeZone>
    {
        PagedList<Entities.TimeZone> SearchTimeZones(
            SearchTimeZoneRequestDto dto, bool trackChanges);
    }
}
