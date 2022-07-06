using cities.Dtos.City;
using cities.Entities;
using common.Models.Parameters;

namespace cities.Repository.Contracts
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        PagedList<City> SearchTimeZones(
            SearchCityRequestDto dto, bool trackChanges);
    }
}
