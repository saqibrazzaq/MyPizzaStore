namespace cities.Dtos.TimeZone
{
    public class SearchTimeZoneRequestDto : PagedRequestParameters
    {
        public string? SearchText { get; set; }
    }
}
