using cities.Entities;
using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
