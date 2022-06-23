using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        [Required, MaxLength(500)]
        public string? Name { get; set; }
        public string? Location { get; set; }

        // Foreign keys
        [ForeignKey("BranchId")]
        public Guid? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
