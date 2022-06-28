using System.ComponentModel.DataAnnotations;

namespace hr.Dtos
{
    public class RequestDto
    {
        [Required]
        public Guid? AccountId { get; set; }
    }
}
