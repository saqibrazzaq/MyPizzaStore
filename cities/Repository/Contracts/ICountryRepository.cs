using cities.Dtos.Country;
using cities.Entities;
using common.Models.Parameters;

namespace cities.Repository.Contracts
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        PagedList<Country> SearchCountries(SearchCountryRequestDto dto, bool trackChanges);
    }
}
