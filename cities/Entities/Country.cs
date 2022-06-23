using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cities.Entities
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }
        [Required]
        public string? CountryCode { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? PhoneCode { get; set; }
        public string? Capital { get; set; }
        public string? Currency { get; set; }
        public string? CurrencyName { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? NativeName { get; set; }
        public string? Region { get; set; }
        public string? SubRegion { get; set; }

        // Child tables
        public IEnumerable<TimeZone>? TimeZones { get; set; }
        public IEnumerable<State>? States { get; set; }
    }
}
