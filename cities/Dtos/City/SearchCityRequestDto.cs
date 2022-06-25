namespace cities.Dtos.City
{
    public class SearchCityRequestDto : PagedRequestParameters
    {
        public Guid? StateId { get; set; }
    }
}
