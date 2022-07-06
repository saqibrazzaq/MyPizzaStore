using common.Models.Parameters;

namespace cities.Dtos.Country
{
    public class SearchCountryRequestDto : PagedRequestParametersPublic
    {
        public Guid? CountryId { get; set; }
    }
}
