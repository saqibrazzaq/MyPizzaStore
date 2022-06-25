using System.ComponentModel.DataAnnotations;

namespace cities.Dtos.City
{
    public  class ManipulateCityDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public Guid? StateId { get; set; }
    }
}
