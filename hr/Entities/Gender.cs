using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.Entities
{
    [Table("Gender")]
    public class Gender
    {
        [Key]
        public char GenderCode { get; set; }
        [Required, MaxLength(6)]
        public string? Name { get; set; }
    }
}
