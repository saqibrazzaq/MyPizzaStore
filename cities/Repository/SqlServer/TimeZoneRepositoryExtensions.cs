using cities.Dtos.TimeZone;
using System.Linq.Dynamic.Core;

namespace cities.Repository.SqlServer
{
    public static class TimeZoneRepositoryExtensions
    {
        public static IQueryable<Entities.TimeZone> Search(this IQueryable<Entities.TimeZone> persons,
            SearchTimeZoneRequestDto personParameters)
        {
            // Empty search term
            if (string.IsNullOrWhiteSpace(personParameters.SearchText))
                return persons;

            // Convert to lower case
            var searchTerm = personParameters.SearchText.Trim().ToLower();

            // Search in different properties
            var personsToReturn = persons.Where(
                // Name
                x => (x.Name ?? "").ToLower().Contains(searchTerm) ||
                (x.Abbreviation ?? "").ToLower().Contains(searchTerm) ||
                (x.TimeZoneName ?? "").ToLower().Contains(searchTerm) ||
                (x.GmtOffsetName ?? "").ToLower().Contains(searchTerm)
                );

            return personsToReturn;
        }

        public static IQueryable<Entities.TimeZone> Sort(this IQueryable<Entities.TimeZone> persons,
            string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return persons.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Entities.TimeZone>(orderBy);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return persons.OrderBy(e => e.Name);

            return persons.OrderBy(orderQuery);
        }
    }
}
