using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Required, MaxLength(500)]
        public string? FirstName { get; set; }
        [MaxLength(500)]
        public string? MiddleName { get; set; }
        [Required, MaxLength(500)]
        public string? LastName { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(500)]
        public string? Address1 { get; set; }
        [MaxLength(500)]
        public string? Address2 { get; set; }
        public string? CityId { get; set; }


        // Foreign keys
        [ForeignKey("DepartmentId")]
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        [ForeignKey("JobId")]
        public Guid? JobId { get; set; }
        public Job? Job { get; set; }

        [ForeignKey("GenderCode")]
        public char GenderCode { get; set; }
        public Gender? Gender { get; set; }
    }
}
