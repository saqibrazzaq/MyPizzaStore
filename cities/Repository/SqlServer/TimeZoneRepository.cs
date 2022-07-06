using cities.Dtos.TimeZone;
using cities.Repository.Contracts;
using common.Models.Parameters;

namespace cities.Repository.SqlServer
{
    public class TimeZoneRepository : RepositoryBase<Entities.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Entities.TimeZone> SearchTimeZones(
            SearchTimeZoneRequestDto dto, bool trackChanges)
        {
            var timeZoneEntities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<Entities.TimeZone>(timeZoneEntities, count, 
                dto.PageNumber, dto.PageSize);
        }
    }
}
