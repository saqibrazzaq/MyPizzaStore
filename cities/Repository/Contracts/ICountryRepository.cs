using cities.Dtos.Country;
using cities.Dtos.PagedRequest;
using cities.Entities;

namespace cities.Repository.Contracts
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        PagedList<Country> SearchCountries(SearchCountryRequestDto dto, bool trackChanges);
    }
}
