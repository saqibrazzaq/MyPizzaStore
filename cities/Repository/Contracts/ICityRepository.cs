using cities.Dtos.City;
using cities.Dtos.PagedRequest;
using cities.Entities;

namespace cities.Repository.Contracts
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        PagedList<City> SearchTimeZones(
            SearchCityRequestDto dto, bool trackChanges);
    }
}
