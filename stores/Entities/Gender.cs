using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stores.Entities
{
    [Table("Gender")]
    public class Gender
    {
        [Key, Required, MaxLength(1)]
        public char GenderCode { get; set; }
        [Required, MaxLength(6)]
        public string? Name { get; set; }
    }
}
