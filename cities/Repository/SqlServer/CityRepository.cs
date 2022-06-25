using cities.Dtos.City;
using cities.Dtos.PagedRequest;
using cities.Entities;
using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<City> SearchTimeZones(
            SearchCityRequestDto dto, bool trackChanges)
        {
            var cityEntities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges: false)
                .Search(dto)
                .Count();
            return new PagedList<City>(cityEntities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
