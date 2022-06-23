using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cities.Entities
{
    [Table("TimeZone")]
    public class TimeZone
    {
        [Key]
        public Guid TimeZoneId { get; set; }
        [Required]
        public string? Name { get; set; }
        public int GmtOffset { get; set; }
        public string? GmtOffsetName { get; set; }
        public string? Abbreviation { get; set; }
        public string? TimeZoneName { get; set; }

        // Foreign keys
        public Guid? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }
    }
}
