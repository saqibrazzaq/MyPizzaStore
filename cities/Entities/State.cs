using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cities.Entities
{
    [Table("State")]
    public class State
    {
        [Required]
        public string? StateCode { get; set; }
        [Required]
        public string? CountryCode { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}
