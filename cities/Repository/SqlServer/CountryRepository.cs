using cities.Entities;
using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
