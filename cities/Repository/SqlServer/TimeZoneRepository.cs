using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class TimeZoneRepository : RepositoryBase<Entities.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(AppDbContext context) : base(context)
        {
        }
    }
}
