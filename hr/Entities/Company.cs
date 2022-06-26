using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }

        // Microservice Api keys from
        public Guid? CityId { get; set; }

        // Child tables
        public IEnumerable<Branch>? Branches { get; set; }
    }
}
