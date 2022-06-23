using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cities.Entities
{
    [Table("City")]
    public class City
    {
        [Key]
        public Guid CityId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
