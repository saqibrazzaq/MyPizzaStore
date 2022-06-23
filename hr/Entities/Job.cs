using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public Guid JobId { get; set; }
        [Required, MaxLength(500)]
        public string? Name { get; set; }

    }
}
