using common.Models.Parameters;

namespace cities.Dtos.City
{
    public class SearchCityRequestDto : PagedRequestParameters
    {
        public Guid? StateId { get; set; }
        public Guid? CityId { get; set; }
    }
}
