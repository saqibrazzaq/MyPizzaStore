using cities.Dtos.State;
using cities.Entities;
using System.Linq.Dynamic.Core;

namespace cities.Repository.SqlServer
{
    public static class StateRepositoryExtensions
    {
        public static IQueryable<State> Search(this IQueryable<State> items,
            SearchStateRequestDto searchParams)
        {
            // Empty search term
            if (string.IsNullOrWhiteSpace(searchParams.SearchText))
                return items;

            // Convert to lower case
            var searchTerm = searchParams.SearchText.Trim().ToLower();

            // Search in different properties
            var itemsToReturn = items.Where(
                // Name
                x => (x.Name ?? "").ToLower().Contains(searchTerm) ||
                (x.StateCode ?? "").ToLower().Contains(searchTerm)
                );

            return itemsToReturn;
        }

        public static IQueryable<State> Sort(this IQueryable<State> items,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return items.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<State>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return items.OrderBy(e => e.Name);

            return items.OrderBy(orderQuery);
        }
    }
}
