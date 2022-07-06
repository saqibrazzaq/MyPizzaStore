using common.Models.Parameters;
using System.ComponentModel.DataAnnotations;

namespace hr.Dtos.Branch
{
    public class ManipulateBranchDto : AccountIdRequestParameters
    {
        [Required, MaxLength(500)]
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Address1 { get; set; }
        [MaxLength(500)]
        public string? Address2 { get; set; }
        [Required]
        public Guid? CompanyId { get; set; }
        public Guid? CityId { get; set; }
    }
}
