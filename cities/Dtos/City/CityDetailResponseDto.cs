namespace cities.Dtos.City
{
    public class CityDetailResponseDto
    {
        public Guid CityId { get; set; }
        public string? Name { get; set; }
        public Guid? StateId { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public Guid? CountryId { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
    }
}
