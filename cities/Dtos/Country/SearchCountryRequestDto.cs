using common.Models.Parameters;

namespace cities.Dtos.Country
{
    public class SearchCountryRequestDto : PagedRequestParameters
    {
        public Guid? CountryId { get; set; }
    }
}
