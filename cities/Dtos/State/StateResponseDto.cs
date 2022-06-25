namespace cities.Dtos.State
{
    public class StateResponseDto
    {
        public Guid StateId { get; set; }
        public string? StateCode { get; set; }
        public string? Name { get; set; }
        public Guid? CountryId { get; set; }
    }
}
