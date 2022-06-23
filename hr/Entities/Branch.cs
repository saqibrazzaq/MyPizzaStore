using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        public Guid BranchId { get; set; }
        [Required, MaxLength(500)]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }

        // Foreign keys
        [ForeignKey("CompanyId")]
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        // Child tables
        public IEnumerable<Department>? Departments { get; set; }
    }
}
