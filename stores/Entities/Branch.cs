using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stores.Entities
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        public Guid BranchId { get; set; }
        [Required, MaxLength(500)]
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }


        // Foreign keys
        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        // Microservice Api keys from
        public Guid? CityId { get; set; }

        // Child tables
        public IEnumerable<Department>? Departments { get; set; }
    }
}
