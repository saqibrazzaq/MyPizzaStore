namespace cities.Dtos.TimeZone
{
    public class TimeZoneResponseDto
    {
        public Guid TimeZoneId { get; set; }
        public string? Name { get; set; }
        public int GmtOffset { get; set; }
        public string? GmtOffsetName { get; set; }
        public string? Abbreviation { get; set; }
        public string? TimeZoneName { get; set; }
        public Guid? CountryId { get; set; }
    }
}
