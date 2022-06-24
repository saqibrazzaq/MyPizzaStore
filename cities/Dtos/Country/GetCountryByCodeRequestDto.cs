using System.ComponentModel.DataAnnotations;

namespace cities.Dtos.Country
{
    public class GetCountryByCodeRequestDto
    {
        [Required, MinLength(3), MaxLength(3)]
        public string? CountryCode { get; set; }
    }
}
