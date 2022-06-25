using System.ComponentModel.DataAnnotations;

namespace cities.Dtos.State
{
    public class ManipulateStateDto
    {
        [Required]
        public string? StateCode { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public Guid? CountryId { get; set; }
    }
}
