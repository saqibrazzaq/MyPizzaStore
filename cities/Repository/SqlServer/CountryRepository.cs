using cities.Dtos.Country;
using cities.Entities;
using cities.Repository.Contracts;
using common.Models.Parameters;

namespace cities.Repository.SqlServer
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Country> SearchCountries(
            SearchCountryRequestDto dto, bool trackChanges)
        {
            var countryEntities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<Country>(countryEntities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
