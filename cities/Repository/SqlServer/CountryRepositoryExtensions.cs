using cities.Dtos.Country;
using cities.Entities;
using System.Linq.Dynamic.Core;

namespace cities.Repository.SqlServer
{
    public static class CountryRepositoryExtensions
    {
        public static IQueryable<Country> Search(this IQueryable<Country> items,
            SearchCountryRequestDto searchParams)
        {
            var itemsToReturn = items;

            if (string.IsNullOrWhiteSpace(searchParams.SearchText) == false)
            {
                itemsToReturn = itemsToReturn.Where(
                    x => (x.Name ?? "").ToLower().Contains(searchParams.SearchText) ||
                        (x.CountryCode ?? "").ToLower().Contains(searchParams.SearchText)
                );
            }

            return itemsToReturn;
        }

        public static IQueryable<Country> Sort(this IQueryable<Country> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Country>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
