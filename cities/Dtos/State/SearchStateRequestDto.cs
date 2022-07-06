namespace cities.Dtos.State
{
    public class SearchStateRequestDto : PagedRequestParameters
    {
        public Guid? CountryId { get; set; }
        public Guid? StateId { get; set; }
    }
}
